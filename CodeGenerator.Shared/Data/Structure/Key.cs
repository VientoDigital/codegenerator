namespace CodeGenerator.Data.Structure
{
    public class Key
    {
        public string Name { get; set; }

        public string ColumnName { get; set; }

        public bool IsPrimary { get; set; }

        public override string ToString() => $"Name: {Name}, Col: {ColumnName}, Primary?: {IsPrimary}";
    }
}