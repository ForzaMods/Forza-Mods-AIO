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

    [LibraryImport("kernel32.dll", EntryPoint = "GetModuleHandleW", StringMarshalling = StringMarshalling.Utf16)]
    public static partial nint GetModuleHandle(string lpModuleName);
    
#pragma warning disable CA2101
    [DllImport("kernel32.dll")]
#pragma warning restore CA2101
#pragma warning disable CA1401
#pragma warning disable SYSLIB1054
    public static extern nuint GetProcAddress(nint hModule, string procName);
#pragma warning restore SYSLIB1054
#pragma warning restore CA1401
}