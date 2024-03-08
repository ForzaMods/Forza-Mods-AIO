using System.Runtime.InteropServices;

namespace Forza_Mods_AIO.Resources;

public static class Imports
{
    [DllImport("kernel32", CharSet = CharSet.Auto,SetLastError = true)]
    public static extern bool CloseHandle(IntPtr handle);
    
    [DllImport("kernel32", SetLastError = true)]
    public static extern int WaitForSingleObject(IntPtr handle, int milliseconds);
    
    [DllImport("kernel32")]
    public static extern nint CreateRemoteThread(nint hProcess, nint lpThreadAttributes, uint dwStackSize, nuint lpStartAddress, nuint lpParameter, uint dwCreationFlags, out nint lpThreadId);
}