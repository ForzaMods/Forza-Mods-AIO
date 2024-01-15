namespace Forza_Mods_AIO.Resources;

public static class Utils
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
}