using System;
using System.Collections;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.Generator
{
	public class Client
	{
		private readonly Context _Context;
		private Table _Table;
		private IDictionary _CustomValues;

		public string StartDelimiter
		{
			get{ return Context.StartDelimeter; }
			set{ Context.StartDelimeter = value; }
		}
		public string EndingDelimiter
		{
			get{ return Context.EndingDelimiter; }
			set{ Context.EndingDelimiter = value; }
		}
		public IDictionary CustomValues
		{
			get { return _CustomValues; }
			set { _CustomValues = value; }
		}
		public Table Table
		{
			get { return _Table; }
			set { _Table = value; }
		}
		public string Input
		{
			set{ _Context.Input = value; }
		}
		public Client()
		{
			_Context = new Context();
		}
		public event EventHandler OnComplete;
		protected void CompleteNotifier(EventArgs e)
		{
			if (OnComplete != null)
			{
				OnComplete(this, e);
			}
		}
		public string Parse()
		{
			var s = Intrepret();
			CompleteNotifier(new EventArgs());
			return s;			
		}
		public string Parse(Table table,string inputString)
		{
			_Table = table;
			_Context.Input = inputString;
			string s = Intrepret();
			CompleteNotifier(new EventArgs());
			return s;
		}
		private string Intrepret()
		{
			var parser = new Parser(_Table);						
			var columnsExp = new ColumnsExpression();
			columnsExp.AddExpression(new ColumnIfTypeExpression());
			columnsExp.AddExpression(new ColumnNameExpression());
			columnsExp.AddExpression(new ColumnTypeExpression());
			columnsExp.AddExpression(new ColumnLengthExpression());
			columnsExp.AddExpression(new ColumnDefaultExpression());
			columnsExp.AddExpression(new ColumnMapTypeExpression());
			columnsExp.AddExpression(new ColumnIfExpression());
			columnsExp.AddExpression(new ColumnIfNullableExpression());
			columnsExp.AddExpression(new ColumnNameMatchesExpression()); 
			parser.AddExpression(columnsExp);
			parser.AddExpression(new TableNameExpression());
            parser.AddExpression(new TableSchemaExpression());
			parser.AddExpression(new DatabaseNameExpression());
			if(_CustomValues != null)
			{
				foreach(DictionaryEntry entry in _CustomValues)
				{
					parser.AddExpression(new LiteralExpression(entry.Key.ToString(),entry.Value.ToString()));		
				}
			}
			parser.Interpret(_Context);
			return _Context.Output;
		}
	}
}