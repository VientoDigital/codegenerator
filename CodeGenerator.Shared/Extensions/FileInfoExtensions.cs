using System.IO;

namespace CodeGenerator.Shared.Extensions
{
    public static class FileInfoExtensions
    {
        // From https://github.com/gordon-matt/Extenso/blob/master/Extenso.Core/IO/FileInfoExtensions.cs
        /// <summary>
        /// Opens the text file, reads all lines of the file, and then closes the file.
        /// </summary>
        /// <param name="fileInfo">The file to read all text from.</param>
        /// <returns>A string containing all lines of the file.</returns>
        public static string ReadAllText(this FileInfo fileInfo)
        {
            return File.ReadAllText(fileInfo.FullName);

            //using (var fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read))
            //using (var streamReader = new StreamReader(fileStream))
            //{
            //    return streamReader.ReadToEnd();
            //}
        }

        // From https://github.com/gordon-matt/Extenso/blob/master/Extenso.Core/IO/FileInfoExtensions.cs
        /// <summary>
        /// Deserializes the XML data contained in the given file to an object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize the XML data to.</typeparam>
        /// <param name="fileInfo">The XML file to deserialize.</param>
        /// <returns>The deserialized object from the XML data.</returns>
        public static T XmlDeserialize<T>(this FileInfo fileInfo)
        {
            using (var fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read))
            using (var streamReader = new StreamReader(fileStream))
            {
                return streamReader.ReadToEnd().XmlDeserialize<T>();
            }
        }
    }
}