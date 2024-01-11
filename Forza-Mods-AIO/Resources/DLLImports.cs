using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Forza_Mods_AIO.Resources;

internal abstract partial class DllImports
{
    [LibraryImport("user32.dll", EntryPoint = "GetAsyncKeyState")]
    public static partial short GetAsyncKeyState(Keys vKey);

    [LibraryImport("User32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool PrintWindow(IntPtr hwnd, IntPtr hDc, uint nFlags);
    
    [LibraryImport("user32.dll", SetLastError = false)]
    public static partial IntPtr GetShellWindow();
    
    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial void EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
    
    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial void EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);
    
    
    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
    
    [DllImport("user32.dll", SetLastError = true)]
    public static extern int GetWindowTextLength(IntPtr hWnd);
    
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern void GetWindowRect(IntPtr handle, ref Rectangle rect);

    public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
    
    [LibraryImport("user32.dll")]
    public static partial IntPtr GetForegroundWindow();

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial void GetClientRect(IntPtr hWnd, out Rect lpRect);

    [LibraryImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial void GetWindowRect(IntPtr hWnd, ref Rect lpRect);

    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

}