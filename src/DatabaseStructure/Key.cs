namespace iCodeGenerator.DatabaseStructure
{
    public class Key
    {
        public Key()
        {
        }

        public string ColumnName { get; set; }

        public string Name { get; set; }

        public bool IsPrimary { get; set; }
    }
}