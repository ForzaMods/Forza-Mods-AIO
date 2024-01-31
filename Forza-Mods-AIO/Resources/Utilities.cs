using System.Linq;
using System.Collections.Generic;

namespace Forza_Mods_AIO.Resources;

public static class Utilities
{
    public static nuint GetBasePtrFromFunc(nuint sigResult, nuint sigResultOffset, nuint ptrOffset)
    {
        var baseAddress = sigResult + sigResultOffset;
        var readAddress = MainWindow.Mw.M.ReadMemory<int>(baseAddress);
        return sigResult + (nuint)readAddress + ptrOffset;
    }
    
    public static nuint GetPtrFromFunc(nuint sigResult, nuint sigResultOffset, nuint ptrOffset)
    {
        var baseAddress = sigResult + sigResultOffset;
        var readAddress = MainWindow.Mw.M.ReadMemory<int>(baseAddress);
        var ptrAddress = sigResult + (nuint)readAddress + ptrOffset;
        return MainWindow.Mw.M.ReadMemory<nuint>(ptrAddress);
    }

    public static nuint FollowMultiLevelPointer(nuint address, IEnumerable<int> offsets)
    {
        var enumerable = offsets as int[] ?? offsets.ToArray();
        if (!enumerable.Any())
        {
            return 0;
        }
        
        var finalAddress = address;
        foreach (var offset in enumerable)
        {
            finalAddress = MainWindow.Mw.M.ReadMemory<nuint>(finalAddress);
            finalAddress += (nuint)(offset >= 0 ? offset : -offset);
        }
        
        return finalAddress;
    }
}