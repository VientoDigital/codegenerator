namespace iCodeGenerator.DatabaseStructure
{
	public class Key
	{
		private string _columnName;
		private string _name;
		private bool _isPrimary;

		public Key()
		{
		}

		public string ColumnName
		{
			get { return _columnName; }
			set { _columnName = value; }
		}

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public bool IsPrimary
		{
			get { return _isPrimary; }
			set { _isPrimary = value; }
		}
	}
}
