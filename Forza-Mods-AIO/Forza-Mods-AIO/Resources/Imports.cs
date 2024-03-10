using System.Runtime.InteropServices;

namespace Forza_Mods_AIO.Resources;

public static partial class Imports
{
    [LibraryImport("kernel32", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool CloseHandle(IntPtr handle);
    
    [LibraryImport("kernel32", SetLastError = true)]
    public static partial int WaitForSingleObject(IntPtr handle, int milliseconds);
    
    [LibraryImport("kernel32")]
    public static partial nint CreateRemoteThread(nint hProcess, nint lpThreadAttributes, uint dwStackSize, nuint lpStartAddress, nuint lpParameter, uint dwCreationFlags, out nint lpThreadId);
}