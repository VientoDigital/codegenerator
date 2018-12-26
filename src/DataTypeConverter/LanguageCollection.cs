using System.Collections;

namespace iCodeGenerator.DataTypeConverter
{
    public class LanguageCollection : CollectionBase
    {
        public LanguageCollection()
        {
        }

        public Language this[int index]
        {
            get
            {
                return ((Language)List[index]);
            }
        }

        internal void Add(Language lang)
        {
            List.Add(lang);
        }

        internal void Remove(Language lang)
        {
            List.Remove(lang);
        }

        public int IndexOf(Language value)
        {
            return (List.IndexOf(value));
        }

        public bool Contains(Language value)
        {
            return (List.Contains(value));
        }
    }
}