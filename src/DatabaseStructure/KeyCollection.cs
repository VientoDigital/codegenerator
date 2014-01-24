using System;
using System.Collections;

namespace iCodeGenerator.DatabaseStructure
{
	public class KeyCollection : CollectionBase 
	{
		public KeyCollection()
		{
		}

		public Key this[int index]
		{
			get{ return (Key)List[index]; }
			set{ List[index] = value; }
		}

		public void Add(Key key)
		{
			List.Add(key);
		}
		public int IndexOf(Key key)
		{
			return List.IndexOf(key);
		}
		public void Remove(Key key)
		{
			List.Remove(key);
		}

		public bool Contains(Key key)
		{
			return List.Contains(key);
		}

		protected override void OnValidate(object value)
		{
			base.OnValidate (value);
			if ( value.GetType() != typeof(Key) )
				throw new ArgumentException( "value must be of type "+ typeof(Key).FullName);

		}


	}
}
