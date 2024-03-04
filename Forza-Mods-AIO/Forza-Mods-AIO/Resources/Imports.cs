using System.Runtime.InteropServices;

namespace Forza_Mods_AIO.Resources;

public static class Imports
{
    [DllImport("kernel32", CharSet = CharSet.Auto,SetLastError = true)]
    public static extern bool CloseHandle(IntPtr handle);
}