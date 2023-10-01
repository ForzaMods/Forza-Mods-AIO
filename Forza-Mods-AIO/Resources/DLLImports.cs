using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Forza_Mods_AIO.Resources
{
    internal partial class DLLImports
    {
        [LibraryImport("user32.dll", EntryPoint = "GetAsyncKeyState")]
        public static partial short GetAsyncKeyState(Keys vKey);
        
        [LibraryImport("user32.dll", EntryPoint = "GetAsyncKeyState")]
        public static partial short GetAsyncKeyState(int vKey);
        
        [DllImport("kernel32.dll")]
        public static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out Memory.Imps.MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);
    }
}
