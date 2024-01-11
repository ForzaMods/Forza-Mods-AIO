using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forza_Mods_AIO.Resources;

public abstract class Asm
{
    protected static byte[] StringToBytes(string hex)
    {
        if (string.IsNullOrWhiteSpace(hex))
        {
            const string exception = "Hex cannot be null, empty, or whitespace";
            throw new ArgumentException(exception);
        }

        if (hex.Contains(' '))
        {
            var stringBuilder = new StringBuilder(hex.Length);
            foreach (var c in hex.Where(c => c != ' '))
            {
                stringBuilder.Append(c);
            }
            
            hex = stringBuilder.ToString();
        }

        var startIndex = hex.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ? 2 : 0;
        var length = (hex.Length - startIndex) / 2;
        var bytesArr = new byte[length];

        for (int i = startIndex, x = 0; i < hex.Length; i += 2, x++)
        {
            var left = hex[i];
            var right = hex[i + 1];
            bytesArr[x] = (byte)((HexMap[left] << 4) | HexMap[right]);
        }

        return bytesArr;
    }

    private static readonly Dictionary<char, byte> HexMap = new()
    {
        { 'a', 0xA },{ 'b', 0xB },{ 'c', 0xC },{ 'd', 0xD },
        { 'e', 0xE },{ 'f', 0xF },{ 'A', 0xA },{ 'B', 0xB },
        { 'C', 0xC },{ 'D', 0xD },{ 'E', 0xE },{ 'F', 0xF },
        { '0', 0x0 },{ '1', 0x1 },{ '2', 0x2 },{ '3', 0x3 },
        { '4', 0x4 },{ '5', 0x5 },{ '6', 0x6 },{ '7', 0x7 },
        { '8', 0x8 },{ '9', 0x9 }
    };

    protected const uint MemRelease = 0x8000;
}