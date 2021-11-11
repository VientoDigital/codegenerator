using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CodeGenerator.Shared.Extensions
{
    public static class ObjectExtensions
    {
        // From: https://github.com/gordon-matt/Extenso/blob/master/Extenso.Core/ObjectExtensions.cs
        /// <summary>
        /// Serializes the specified object and writes the XML document to a file.
        /// </summary>
        /// <typeparam name="T">The type of source.</typeparam>
        /// <param name="source">The object to serialize.</param>
        /// <param name="fileName">The full path of the file to be written to.</param>
        /// <param name="omitXmlDeclaration">Indicates whether to omit an XML declaration.</param>
        /// <param name="removeNamespaces">
        /// Indicates whether to remove the XML namespaces during serialization. If any of the properties on the object
        /// are decorated with an XmlIncludeAttribute, then set this to false.
        /// </param>
        /// <param name="xmlns">
        /// XML namespaces and prefixes that the serializer should use to generate qualified names.
        /// If not null, removeNamespaces is ignored.
        /// </param>
        /// <param name="encoding">Specifies the character encoding to use.</param>
        /// <returns>A string containing the XML.</returns>
        /// <returns>true if successful; otherwise false.</returns>
        public static bool XmlSerialize<T>(
            this T source,
            string fileName,
            bool omitXmlDeclaration = true,
            bool removeNamespaces = true,
            XmlSerializerNamespaces xmlns = null,
            Encoding encoding = null)
        {
            string xml = source.XmlSerialize(omitXmlDeclaration, removeNamespaces, xmlns, encoding);
            return xml.ToFile(fileName);
        }

        // From: https://github.com/gordon-matt/Extenso/blob/master/Extenso.Core/ObjectExtensions.cs
        /// <summary>
        /// Serializes the specified object and writes the XML document to a string.
        /// </summary>
        /// <typeparam name="T">The type of source.</typeparam>
        /// <param name="source">The object to serialize.</param>
        /// <param name="omitXmlDeclaration">Indicates whether to omit an XML declaration.</param>
        /// <param name="removeNamespaces">
        /// Indicates whether to remove the XML namespaces during serialization. If any of the properties on the object
        /// are decorated with an XmlIncludeAttribute, then set this to false.
        /// </param>
        /// <param name="xmlns">
        /// XML namespaces and prefixes that the serializer should use to generate qualified names.
        /// If not null, removeNamespaces is ignored.
        /// </param>
        /// <param name="encoding">Specifies the character encoding to use.</param>
        /// <returns>A string containing the XML.</returns>
        public static string XmlSerialize<T>(
            this T source,
            bool omitXmlDeclaration = true,
            bool removeNamespaces = true,
            XmlSerializerNamespaces xmlns = null,
            Encoding encoding = null)
        {
            object locker = new object();

            var xmlSerializer = new XmlSerializer(source.GetType());

            var settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = omitXmlDeclaration
            };

            lock (locker)
            {
                var stringBuilder = new StringBuilder();
                using (var stringWriter = new CustomEncodingStringWriter(encoding, stringBuilder))
                using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
                {
                    if (xmlns != null)
                    {
                        xmlSerializer.Serialize(xmlWriter, source, xmlns);
                    }
                    else
                    {
                        if (removeNamespaces)
                        {
                            xmlns = new XmlSerializerNamespaces();
                            xmlns.Add(string.Empty, string.Empty);

                            xmlSerializer.Serialize(xmlWriter, source, xmlns);
                        }
                        else { xmlSerializer.Serialize(xmlWriter, source); }
                    }

                    return stringBuilder.ToString();
                }
            }
        }
    }
}