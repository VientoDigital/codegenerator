using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CodeGenerator.Data;
using CodeGenerator.Data.Structure;
using CodeGenerator.Generator;
using NUnit.Framework;

namespace CodeGenerator.UnitTests
{
    [TestFixture]
    public class TestParser
    {
        private readonly Context context = new();
        private Parser parser;

        [SetUp]
        public void SetUp()
        {
            Server.ConnectionString = @"Server=.;Database=master;Integrated Security=SSPI;";
            Server.ProviderType = DataSource.SqlServer;
            Context.StartDelimeter = "{";
            Context.EndingDelimiter = "}";
            parser = new Parser(new Server().Databases.First().Tables.First());
        }

        [TearDown]
        public void TearDown()
        {
            Console.WriteLine(context.Output);
        }

        [Test]
        public void TestConstructor()
        {
            Assert.IsNotNull(parser);
        }

        [Test]
        public void TestLiteral()
        {
            context.Input = "{LiteralExpression} {TABLE.NAME} Hello";
            string replacementText = "LiteralREPLACED";
            parser.AddExpression(new LiteralExpression("LiteralExpression", replacementText));
            parser.Interpret(context);
            Assert.IsTrue(Regex.IsMatch(context.Output, replacementText));
        }

        [Test]
        public void TestTableName()
        {
            context.Input = "{LiteralExpression} {TABLE.NAME} Hello";
            parser.AddExpression(new TableNameExpression());
            parser.Interpret(context);
        }

        public void TestTableColumnsClass()
        {
            context.Input = @"public class {TABLE.NAME}{
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
            var columns = new ColumnsExpression();
            columns.AddExpression(new ColumnNameExpression());
            columns.AddExpression(new ColumnTypeExpression());
            columns.AddExpression(new ColumnMapTypeExpression());
            //columns.AddExpression(new LiteralExpression("[A-Z. ]+", string.Empty));
            parser.AddExpression(columns);
            parser.AddExpression(new TableNameExpression());
            parser.Interpret(context);
        }

        [Test]
        public void TestTableColumnsSQL()
        {
            context.Input = @"CREATE PROCEDURE sp{TABLE.NAME}_Insert(
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
            var columns = new ColumnsExpression();
            columns.AddExpression(new ColumnNameExpression());
            columns.AddExpression(new ColumnTypeExpression());
            columns.AddExpression(new ColumnLengthExpression());
            columns.AddExpression(new ColumnIfExpression());
            parser.AddExpression(columns);
            parser.AddExpression(new TableNameExpression());
            parser.Interpret(context);
        }

        [Test]
        public void TestColumnsIfLast()
        {
            context.Input = @"{TABLE.COLUMNS}
{IF NOT LAST}NOT LAST{/IF}
{IF LAST}
LAST{/IF}
{/TABLE.COLUMNS}";

            var columns = new ColumnsExpression();
            columns.AddExpression(new ColumnIfExpression());
            parser.AddExpression(columns);
            parser.Interpret(context);
        }

        [Test]
        public void TestColumnsNoPrimary()
        {
            context.Input = @"{TABLE.COLUMNS NOPRIMARY}{COLUMN.NAME}
{/TABLE.COLUMNS}";

            var columns = new ColumnsExpression();
            columns.AddExpression(new ColumnNameExpression());
            parser.AddExpression(columns);
            parser.Interpret(context);
        }

        [Test]
        public void TestColumnsPrimary()
        {
            var client = new Client();
            string outputText = client.Parse(new Server().Databases.First().Tables.First(), @"{TABLE.COLUMNS PRIMARY}{COLUMN.NAME}
{/TABLE.COLUMNS}");

            Console.WriteLine(outputText);
        }

        [Test]
        public void TestIfType()
        {
            context.Input = @"
{TABLE.COLUMNS}
{IF COLUMN.TYPE EQ 'int|numeric()'}int _{COLUMN.NAME}{/IF}
{IF COLUMN.TYPE NE 'int'}string _{COLUMN.NAME}{/IF}
{/TABLE.COLUMNS}";

            var colsExpression = new ColumnsExpression();
            colsExpression.AddExpression(new ColumnIfTypeExpression());
            colsExpression.AddExpression(new ColumnNameExpression());
            parser.AddExpression(colsExpression);
            parser.Interpret(context);
        }

        [Test]
        public void TestColumnsIfTypeClient()
        {
            var client = new Client();
            string outputText = client.Parse(new Server().Databases.First().Tables.First(), @"
{TABLE.COLUMNS}
{IF COLUMN.TYPE EQ 'int'}int _{COLUMN.NAME}{/IF}
{IF COLUMN.TYPE NE 'int'}string _{COLUMN.NAME}{/IF}
{/TABLE.COLUMNS}");

            Console.WriteLine(outputText);
        }

        [Test]
        public void TestIfNullable()
        {
            context.Input = @"{TABLE.COLUMNS}{IF COLUMN.NULLABLE}{COLUMN.NAME}=NULL{/IF}{IF NOT COLUMN.NULLABLE}FALSE{/IF} {/TABLE.COLUMNS}";
            var columns = new ColumnsExpression();
            columns.AddExpression(new ColumnNameExpression());
            columns.AddExpression(new ColumnIfNullableExpression());
            parser.AddExpression(columns);
            parser.Interpret(context);
        }

        [Test]
        public void TestCustomValues()
        {
            var client = new Client
            {
                CustomValues = new Dictionary<string, string>
                {
                    { "NAMESPACE", "CodeGenerator.Bla" }
                }
            };

            Server.ConnectionString = ConfigFile.Instance.ConnectionStrings.First();
            Server.ProviderType = DataSource.SqlServer;
            client.Parse(new Server().Databases.First().Tables.First(), "{NAMESPACE}");
        }

        [Test]
        public void TestColumnNameMatchesExpression()
        {
            context.Input = @"{TABLE.COLUMNS}" +
                            @"{IF COLUMN.NAME =~ 'Id$'}{COLUMN.NAME} ES ID{/IF}\n" +
                            @"{IF COLUMN.NAME !~ 'Id$'}{COLUMN.NAME}{/IF}\n" +
                            "{/TABLE.COLUMNS}";

            var columns = new ColumnsExpression();
            columns.AddExpression(new ColumnNameExpression());
            columns.AddExpression(new ColumnNameMatchesExpression());
            parser.AddExpression(columns);
            parser.Interpret(context);
        }

        [Test]
        public void TestColumnNaming()
        {
            context.Input = @"{TABLE.COLUMNS}" +
                             @"Normal: {COLUMN.NAME}\n" +
                             //	@"Camel: {COLUMN.NAME CAMEL}\n" +
                             //	@"Pascal: {COLUMN.NAME PASCAL}\n" +
                             // @"Upper: {COLUMN.NAME UPPER}\n" +
                             // @"Lower: {COLUMN.NAME LOWER}\n" +
                             @"Underscore: {COLUMN.NAME UNDERSCORE}\n" +
                             @"Human: {COLUMN.NAME HUMAN}\n" +
                             @"{/TABLE.COLUMNS}";

            var columns = new ColumnsExpression();
            columns.AddExpression(new ColumnNameExpression());
            parser.AddExpression(columns);
            parser.Interpret(context);
            //Console.WriteLine(_context.Output);
        }
    }
}