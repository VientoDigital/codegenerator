using System;
using System.IO;
using System.Text;

namespace CodeGenerator.Shared.Extensions
{
    // From: https://github.com/gordon-matt/Extenso/blob/master/Extenso.Core/IO/CustomEncodingStringWriter.cs

    /// <summary>
    /// Inherits from System.IO.StringWriter, but allows the encoding to be specified.
    /// </summary>
    public class CustomEncodingStringWriter : StringWriter
    {
        private Encoding encoding;

        /// <summary>
        /// Gets the System.Text.Encoding in which the output is written.
        /// </summary>
        public override Encoding Encoding => encoding ?? base.Encoding;

        /// <summary>
        /// Initializes a new instance of the Extenso.IO.CustomEncodingStringWriter class with the specified System.Text.Encoding.
        /// </summary>
        /// <param name="encoding">The System.Text.Encoding in which the output should be written.</param>
        public CustomEncodingStringWriter(Encoding encoding)
            : base()
        {
            this.encoding = encoding;
        }

        /// <summary>
        /// Initializes a new instance of the Extenso.IO.CustomEncodingStringWriter class with the specified System.Text.Encoding and format provider.
        /// </summary>
        /// <param name="encoding">The System.Text.Encoding in which the output should be written.</param>
        /// <param name="formatProvider">An System.IFormatProvider object that controls formatting.</param>
        public CustomEncodingStringWriter(Encoding encoding, IFormatProvider formatProvider)
            : base(formatProvider)
        {
            this.encoding = encoding;
        }

        /// <summary>
        /// Initializes a new instance of the Extenso.IO.CustomEncodingStringWriter class with the specified System.Text.Encoding that writes to the specified System.Text.StringBuilder.
        /// </summary>
        /// <param name="encoding">The System.Text.Encoding in which the output should be written.</param>
        /// <param name="sb">The System.Text.StringBuilder object to write to.</param>
        public CustomEncodingStringWriter(Encoding encoding, StringBuilder sb)
            : base(sb)
        {
            this.encoding = encoding;
        }

        /// <summary>
        /// Initializes a new instance of the Extenso.IO.CustomEncodingStringWriter class that writes to the specified System.Text.StringBuilder and has the specified System.Text.Encoding and format provider.
        /// </summary>
        /// <param name="encoding">The System.Text.Encoding in which the output should be written.</param>
        /// <param name="sb">The System.Text.StringBuilder object to write to.</param>
        /// <param name="formatProvider">An System.IFormatProvider object that controls formatting.</param>
        public CustomEncodingStringWriter(Encoding encoding, StringBuilder sb, IFormatProvider formatProvider)
            : base(sb, formatProvider)
        {
            this.encoding = encoding;
        }
    }
}