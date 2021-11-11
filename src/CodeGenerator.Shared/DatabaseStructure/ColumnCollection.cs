using System;
using System.Collections;

namespace CodeGenerator.DatabaseStructure
{
    public class ColumnCollection : CollectionBase
    {
        public ColumnCollection()
        {
        }

        public Column this[int index]
        {
            get { return (Column)List[index]; }
            set { List[index] = value; }
        }

        public void Add(Column column) => List.Add(column);

        public int IndexOf(Column column) => List.IndexOf(column);

        public void Remove(Column column) => List.Remove(column);

        public bool Contains(Column column) => List.Contains(column);

        protected override void OnValidate(object value)
        {
            base.OnValidate(value);
            if (value.GetType() != typeof(Column))
            {
                throw new ArgumentException("value must be of type " + typeof(Column).FullName);
            }
        }
    }
}