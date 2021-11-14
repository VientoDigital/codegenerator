using System.ComponentModel;

namespace CodeGenerator.Data.Structure
{
    public class Column
    {
        [Browsable(false), ReadOnly(true)]
        public Table ParentTable { get; set; }

        [Category("Column"), ReadOnly(true)]
        public string Name { get; set; }

        [Category("Column")]
        public string Type { get; set; }

        [Category("Column"), ReadOnly(true)]
        public bool IsPrimaryKey { get; set; }

        [Category("Column")]
        public int Length { get; set; }

        [Category("Column")]
        public bool Nullable { get; set; }

        [Category("Column")]
        public string Default { get; set; }
    }
}