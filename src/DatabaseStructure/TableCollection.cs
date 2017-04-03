using System;
using System.Collections;

namespace iCodeGenerator.DatabaseStructure
{
    public class TableCollection : CollectionBase
    {
        public Table this[int index]
        {
            get { return (Table)List[index]; }
            set { List[index] = value; }
        }

        public void Add(Table table)
        {
            List.Add(table);
        }

        public int IndexOf(Table table)
        {
            return List.IndexOf(table);
        }

        public void Remove(Table table)
        {
            List.Remove(table);
        }

        public bool Contains(Table table)
        {
            return List.Contains(table);
        }

        protected override void OnValidate(object value)
        {
            base.OnValidate(value);
            if (value.GetType() != typeof(Table))
            {
                throw new ArgumentException("value must be of type " + typeof(Table).FullName);
            }
        }
    }
}