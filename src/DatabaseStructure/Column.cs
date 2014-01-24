using System.ComponentModel;

namespace iCodeGenerator.DatabaseStructure
{
	public class Column
	{

		private string _name;
		private string _type;
		private int _length;
		private bool _nullable;
		private Table _parentTable;
		private bool _isPrimaryKey;
		private string _default;


		public Column()
		{
		}

		[CategoryAttribute("Column"),
		ReadOnlyAttribute(true)]
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		[CategoryAttribute("Column")]
		public string Type
		{
			get { return _type; }
			set { _type = value; }
		}

		[CategoryAttribute("Column")]
		public int Length
		{
			get { return _length; }
			set { _length = value; }
		}

		[CategoryAttribute("Column")]
		public bool Nullable
		{
			get { return _nullable; }
			set { _nullable = value; }
		}

		[CategoryAttribute("Column")]
		public string Default
		{
			get { return _default; }
			set { _default = value; }
		}
		
		internal void SetParentTable(Table table)
		{
			_parentTable = table;
		}

		[BrowsableAttribute(false),
		ReadOnlyAttribute(true)]
		public Table ParentTable
		{
			get { return _parentTable; }
		}

		[CategoryAttribute("Column"),
		ReadOnlyAttribute(true)]
		public bool IsPrimaryKey
		{
			set { _isPrimaryKey = value; }
			get { return _isPrimaryKey; }
		}
	}
}
