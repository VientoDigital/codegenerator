using System;
using System.Collections;

namespace CodeGenerator.Data.Structure
{
    public class KeyCollection : CollectionBase
    {
        public KeyCollection()
        {
        }

        public Key this[int index]
        {
            get => (Key)List[index];
            set => List[index] = value;
        }

        public void Add(Key key) => List.Add(key);

        public int IndexOf(Key key) => List.IndexOf(key);

        public void Remove(Key key) => List.Remove(key);

        public bool Contains(Key key) => List.Contains(key);

        protected override void OnValidate(object value)
        {
            base.OnValidate(value);
            if (value.GetType() != typeof(Key))
            {
                throw new ArgumentException("value must be of type " + typeof(Key).FullName);
            }
        }
    }
}