using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using CodeGenerator.Generator;
using Newtonsoft.Json;

namespace CodeGenerator.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string DelimeterWrap(this string source)
        {
            return $"{Context.StartDelimeter}{source}{Context.EndingDelimiter}";
        }

        /// <summary>
        /// Deserializes the JSON to the specified .NET type using Newtonsoft.Json.JsonSerializerSettings.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="source">The JSON to deserialize.</param>
        /// <param name="settings">
        /// The Newtonsoft.Json.JsonSerializerSettings used to deserialize the object. If
        /// this is null, default serialization settings will be used.
        /// </param>
        /// <returns>The deserialized object from the JSON string.</returns>
        public static T JsonDeserialize<T>(this string source, JsonSerializerSettings settings = null)
        {
            return JsonConvert.DeserializeObject<T>(source, settings);
        }

        /// <summary>
        /// Deserializes the JSON to the specified .NET type using Newtonsoft.Json.JsonSerializerSettings.
        /// </summary>
        /// <param name="source">The JSON to deserialize.</param>
        /// <param name="type">The type of the object to deserialize to.</param>
        /// <param name="settings">
        /// The Newtonsoft.Json.JsonSerializerSettings used to deserialize the object. If
        /// this is null, default serialization settings will be used.
        /// </param>
        /// <returns></returns>
        public static object JsonDeserialize(this string source, Type type, JsonSerializerSettings settings = null)
        {
            return JsonConvert.DeserializeObject(source, type, settings);
        }

        // From https://github.com/gordon-matt/Extenso/blob/master/Extenso.Core/StringExtensions.cs
        /// <summary>
        /// Creates a new file, writes the given string to the file, and then closes
        /// the file. If the target file already exists, it is overwritten.
        /// </summary>
        /// <param name="source">The string to write to file.</param>
        /// <param name="filePath">The file to write to.</param>
        /// <returns>true if successful; otherwise false.</returns>
        public static bool ToFile(this string source, string filePath)
        {
            try
            {
                var directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                using (var sw = new StreamWriter(fs))
                {
                    sw.Write(source);
                    sw.Flush();
                    return true;
                }
            }
            catch (IOException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // From https://github.com/gordon-matt/Extenso/blob/master/Extenso.Core/StringExtensions.cs
        /// <summary>
        /// Deserializes the XML contained within the given string to an object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize the XML to.</typeparam>
        /// <param name="source">The string to deserialize.</param>
        /// <returns>The deserialized object from the XML data in [source].</returns>
        public static T XmlDeserialize<T>(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return default(T);
            }

            var locker = new object();
            var stringReader = new StringReader(source);
            var reader = new XmlTextReader(stringReader);
            try
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                lock (locker)
                {
                    var item = (T)xmlSerializer.Deserialize(reader);
                    reader.Close();
                    return item;
                }
            }
            finally
            {
                reader.Close();
            }
        }

        // From https://github.com/gordon-matt/Extenso/blob/master/Extenso.Core/StringExtensions.cs
        /// <summary>
        /// Deserializes the XML contained within the given string to an object of the specified type.
        /// </summary>
        /// <param name="source">The string to deserialize.</param>
        /// <param name="type">The type of object to deserialize the XML to.</param>
        /// <returns>The deserialized object from the XML data in [source].</returns>
        public static object XmlDeserialize(this string source, Type type)
        {
            if (string.IsNullOrEmpty(source))
            {
                return null;
            }

            var locker = new object();
            var stringReader = new StringReader(source);
            var reader = new XmlTextReader(stringReader);
            try
            {
                var xmlSerializer = new XmlSerializer(type);
                lock (locker)
                {
                    var item = xmlSerializer.Deserialize(reader);
                    reader.Close();
                    return item;
                }
            }
            finally
            {
                reader.Close();
            }
        }
    }
}