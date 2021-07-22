using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ContainerReader
{
    class BinaryReaderHelper
    {
        /// <summary>
        /// Reads a unicode string from a binary file.
        /// </summary>
        /// <param name="reader">The reader that will be used to read the Unicode string.</param>
        /// <param name="count">The number of characters in the string.</param>
        /// <returns>The Unicode string.</returns>
        public static string ReadUnicodeString(BinaryReader reader, int count)
        {
            // Multiply count by two because each unicode char is two bytes, at least in the context of containers.index
            byte[] buff = reader.ReadBytes(count * 2);
            return Encoding.Unicode.GetString(buff);
        }
    }
}
