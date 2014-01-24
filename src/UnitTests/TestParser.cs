using System;
using System.Collections;
using System.Text.RegularExpressions;
using iCodeGenerator.GenericDataAccess;
using iCodeGenerator.DatabaseStructure;
using iCodeGenerator.Generator;
using NUnit.Framework;

namespace iCodeGenerator.UnitTests
{
	[TestFixture]
	public class TestParser
	{
		Context _context = new Context();
		Parser _parser;

		[SetUp]
		public void SetUp()
		{
			Server.ConnectionString = @"SERVER=SAM\NETSDK;DATABASE=;UID=sa;PWD=s4ms4m;";
			Server.ProviderType = DataProviderType.SqlClient;
			Context.StartDelimeter = "{";
			Context.EndingDelimiter = "}";
			_parser = new Parser(new Server().Databases[0].Tables[0]);
		}

		[TearDown]
		public void TearDown()
		{
			Console.WriteLine(_context.Output);
		}

		[Test]
		public void TestConstructor()
		{
			Assert.IsNotNull(_parser);
		}

		[Test]
		public void TestLiteral()
		{
			_context.Input = "{LiteralExpression} {TABLE.NAME} Hello";
			string replacementText = "LiteralREPLACED";
			_parser.AddExpression(new LiteralExpression("LiteralExpression",replacementText));
			_parser.Interpret(_context);
			Assert.IsTrue(Regex.IsMatch(_context.Output,replacementText));
		}
	
		[Test]
		public void TestTableName()
		{
			_context.Input = "{LiteralExpression} {TABLE.NAME} Hello";			
			_parser.AddExpression(new TableNameExpression());
			_parser.Interpret(_context);
			
		}

		public void TestTableColumnsClass()
		{
			_context.Input = @"public class {TABLE.NAME}{
	{TABLE.COLUMNS}
	private {MAP COLUMN.TYPE} _{COLUMN.NAME}; //{COLUMN.LENGHT} {TABLE.NULLABLE}
	{/TABLE.COLUMNS}
	
	public void {TABLE.NAME}(){
	}

	{TABLE.COLUMNS}
	//{TABLE.NAME}
	public {MAP COLUMN.TYPE} {COLUMN.NAME}
	{
	set{ _{COLUMN.NAME} = value; }	
	get{ return _{COLUMN.NAME}; }
	}
	{/TABLE.COLUMNS}
}
";
			ColumnsExpression columns = new ColumnsExpression();			
			columns.AddExpression(new ColumnNameExpression());
			columns.AddExpression(new ColumnTypeExpression());
			columns.AddExpression(new ColumnMapTypeExpression());
			//columns.AddExpression(new LiteralExpression("[A-Z. ]+",""));
			_parser.AddExpression(columns);
			_parser.AddExpression(new TableNameExpression());
			_parser.Interpret(_context);
		}

		[Test]
		public void TestTableColumnsSQL()
		{
			_context.Input = @"CREATE PROCEDURE sp{TABLE.NAME}_Insert(
{TABLE.COLUMNS NOPRIMARY}	@{COLUMN.NAME}	{COLUMN.TYPE},
{/TABLE.COLUMNS}{TABLE.COLUMNS PRIMARY}@{COLUMN.NAME} {COLUMN.TYPE} OUTPUT{/TABLE.COLUMNS}
)
AS
INSERT INTO {TABLE.NAME} ({TABLE.COLUMNS}{COLUMN.NAME}{IF NOT LAST},{/IF}
{/TABLE.COLUMNS}) 
VALUES({TABLE.COLUMNS}@{COLUMN.NAME}{IF NOT LAST},{/IF}
{/TABLE.COLUMNS})
{TABLE.COLUMNS PRIMARY}
SET @{COLUMN.NAME} = @{COLUMN.NAME}
{/TABLE.COLUMNS}
";
			ColumnsExpression columns = new ColumnsExpression();			
			columns.AddExpression(new ColumnNameExpression());
			columns.AddExpression(new ColumnTypeExpression());
			columns.AddExpression(new ColumnLengthExpression());
			columns.AddExpression(new ColumnIfExpression());
			_parser.AddExpression(columns);
			_parser.AddExpression(new TableNameExpression());
			_parser.Interpret(_context);
		}

		[Test]
		public void TestColumnsIfLast()
		{
			_context.Input = @"{TABLE.COLUMNS}
{IF NOT LAST}NOT LAST{/IF}
{IF LAST}
LAST{/IF}
{/TABLE.COLUMNS}";
			ColumnsExpression columns = new ColumnsExpression();			
			columns.AddExpression(new ColumnIfExpression());
			_parser.AddExpression(columns);
			_parser.Interpret(_context);
		}

		[Test]
		public void TestColumnsNoPrimary()
		{
			_context.Input = @"{TABLE.COLUMNS NOPRIMARY}{COLUMN.NAME}
{/TABLE.COLUMNS}";
			ColumnsExpression columns = new ColumnsExpression();			
			columns.AddExpression(new ColumnNameExpression());
			_parser.AddExpression(columns);
			_parser.Interpret(_context);
		}

		[Test]
		public void TestColumnsPrimary()
		{
			Client client = new Client();
			string outputText = client.Parse(new Server().Databases[0].Tables[0],@"{TABLE.COLUMNS PRIMARY}{COLUMN.NAME}
{/TABLE.COLUMNS}");
			System.Console.WriteLine(outputText);
		}

		[Test]
		public void TestIfType()
		{
			_context.Input = @"
{TABLE.COLUMNS}
{IF COLUMN.TYPE EQ 'int|numeric()'}int _{COLUMN.NAME}{/IF}
{IF COLUMN.TYPE NE 'int'}string _{COLUMN.NAME}{/IF}
{/TABLE.COLUMNS}";
			ColumnsExpression colsExpression = new ColumnsExpression();
			colsExpression.AddExpression(new ColumnIfTypeExpression());
			colsExpression.AddExpression(new ColumnNameExpression());
			_parser.AddExpression(colsExpression);			
			_parser.Interpret(_context);
		}

		[Test]
		public void TestColumnsIfTypeClient()
		{
			Client client = new Client();
			string outputText = client.Parse(new Server().Databases[0].Tables[0],@"
{TABLE.COLUMNS}
{IF COLUMN.TYPE EQ 'int'}int _{COLUMN.NAME}{/IF}
{IF COLUMN.TYPE NE 'int'}string _{COLUMN.NAME}{/IF}
{/TABLE.COLUMNS}");
			System.Console.WriteLine(outputText);
		}

		[Test]
		public void TestIfNullable()
		{
			_context.Input = @"{TABLE.COLUMNS}{IF COLUMN.NULLABLE}{COLUMN.NAME}=NULL{/IF}{IF NOT COLUMN.NULLABLE}FALSE{/IF} {/TABLE.COLUMNS}";
			ColumnsExpression columns = new ColumnsExpression();	
			columns.AddExpression(new ColumnNameExpression());
			columns.AddExpression(new ColumnIfNullableExpression());
			_parser.AddExpression(columns);
			_parser.Interpret(_context);			
		}

		[Test]
		public void TestCustomValues()
		{
			Hashtable hs = new Hashtable();
			hs.Add("NAMESPACE","CodeGenerator.Bla");
			Client client = new Client();
			client.CustomValues = hs;
			Server.ConnectionString = ConnectionStringManager.Instance.GetConnectionStrings()[0];
			Server.ProviderType = DataProviderType.SqlClient;
			client.Parse(new Server().Databases[0].Tables[0],"{NAMESPACE}");
		}
		
		[Test]
		public void TestColumnNameMatchesExpression()
		{
			_context.Input = @"{TABLE.COLUMNS}"+
							@"{IF COLUMN.NAME =~ 'Id$'}{COLUMN.NAME} ES ID{/IF}" + 
							"\n"+
							@"{IF COLUMN.NAME !~ 'Id$'}{COLUMN.NAME}{/IF}"+
							"\n"+
							"{/TABLE.COLUMNS}";
			ColumnsExpression columns = new ColumnsExpression();	
			columns.AddExpression(new ColumnNameExpression());
			columns.AddExpression(new ColumnNameMatchesExpression());
			_parser.AddExpression(columns);
			_parser.Interpret(_context);
		}
		
		[Test]
		public void TestColumnNaming()
		{
			_context.Input = @"{TABLE.COLUMNS}" +
			                 @"Normal: {COLUMN.NAME}" + "\n" +
//			                 @"Camel: {COLUMN.NAME CAMEL}" + "\n" +
//			                 @"Pascal: {COLUMN.NAME PASCAL}" + "\n" +
//			                 @"Upper: {COLUMN.NAME UPPER}" + "\n" +
//			                 @"Lower: {COLUMN.NAME LOWER}" + "\n" +
			                 @"Underscore: {COLUMN.NAME UNDERSCORE}" + "\n" +
			                 @"Human: {COLUMN.NAME HUMAN}" + "\n" +
							 @"{/TABLE.COLUMNS}";
			ColumnsExpression columns = new ColumnsExpression();	
			columns.AddExpression(new ColumnNameExpression());
			_parser.AddExpression(columns);
			_parser.Interpret(_context);
//			Console.WriteLine(_context.Output);
		}
		
	}	
}
