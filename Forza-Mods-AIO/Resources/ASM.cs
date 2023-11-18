using System;
using System.Collections.Generic;

namespace Forza_Mods_AIO.Resources;

public abstract class Asm
{
    protected static byte[] StringToBytes(string hex)
    {
        if (string.IsNullOrWhiteSpace(hex))
        {
            throw new ArgumentException("Hex cannot be null/empty/whitespace");
        }

        if (hex.Length % 2 != 0)
        {
            throw new FormatException("Hex must have an even number of characters");
        }

        var startsWithHexStart = hex.StartsWith("0x", StringComparison.OrdinalIgnoreCase);

        if (startsWithHexStart && hex.Length == 2)
        {
            throw new ArgumentException("There are no characters in the hex string");
        }


        var startIndex = startsWithHexStart ? 2 : 0;

        var bytesArr = new byte[(hex.Length - startIndex) / 2];

        try
        {
            var x = 0;
            for (var i = startIndex; i < hex.Length; i += 2, x++)
            {
                var left = hex[i];
                var right = hex[i + 1];
                bytesArr[x] = (byte)((HexMap[left] << 4) | HexMap[right]);
            }
            return bytesArr;
        }
        catch (KeyNotFoundException)
        {
            throw new FormatException("Hex string has non-hex character");
        }
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