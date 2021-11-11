using System;
using System.Collections;

namespace CodeGenerator.Data.Structure
{
    public class DatabaseCollection : CollectionBase
    {
        public DatabaseCollection()
        {
        }

        public Database this[int index]
        {
            get { return (Database)List[index]; }
            set { List[index] = value; }
        }

        public void Add(Database database) => List.Add(database);

        public int IndexOf(Database database) => List.IndexOf(database);

        public void Remove(Database database) => List.Remove(database);

        public bool Contains(Database database) => List.Contains(database);

        protected override void OnValidate(object value)
        {
            base.OnValidate(value);
            if (value.GetType() != typeof(Database))
            {
                throw new ArgumentException("value must be of type " + typeof(Database).FullName);
            }
        }
    }
}