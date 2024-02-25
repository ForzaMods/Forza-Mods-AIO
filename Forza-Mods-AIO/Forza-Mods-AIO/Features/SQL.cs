using System.Runtime.InteropServices;

namespace Forza_Mods_AIO.Features;

public class Sql
{
    #region DLL Imports

    [DllImport("kernel32", SetLastError = true)]
    private static extern int WaitForSingleObject(IntPtr handle, int milliseconds);

    #endregion
    
}