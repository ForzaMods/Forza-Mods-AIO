using System;
using System.Linq;
using System.Windows;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Forza_Mods_AIO.Resources;
using MahApps.Metro.Controls;
using static System.BitConverter;
using static System.Buffer;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Overlay.Overlay;
using static Forza_Mods_AIO.Tabs.AutoShowTab.AutoShow;
using static Memory.Imps;
using Utils = Forza_Mods_AIO.Resources.Utils;

namespace Forza_Mods_AIO.Tabs.AutoShowTab;

internal class AutoshowVars
{
    #region Variables
   
    private static nuint _ptr = nuint.Zero;
    private static nuint _callFunction = nuint.Zero;
    private const int VirtualFunctionIndex = 9;

    #endregion

    public static void Scan()
    {
        As.UiManager.Index = 0;
        As.UiManager.ScanAmount = 1;
            
        _ptr = nuint.Zero;
        _callFunction = nuint.Zero;
        
        /*Mw.Mapper = new LibraryMapper(Mw.M.MProc.Process, Properties.Resources.SQL_DLL);
        Mw.Mapper.MapLibrary();*/

        
#if !RELEASE
        var start = DateTime.Now;
        Mw.Dispatcher.Invoke(() => Mw.DebugLabel.Text = $"Starting autoshow scan at: {start}");
#endif
        
        var sqlExecAobScan = SqlExecAobScan();
        
        //AutoshowGarageOption.IsEnabled = Mw.Mapper.DllBaseAddress != IntPtr.Zero;
        AutoshowGarageOption.IsEnabled = sqlExecAobScan;
        
        As.UiManager.AddProgress();
        //As.UiManager.ToggleUiElements(Mw.Mapper.DllBaseAddress != IntPtr.Zero);
        As.UiManager.ToggleUiElements(sqlExecAobScan);
                                
#if !RELEASE
        Mw.Dispatcher.Invoke(() => Mw.DebugLabel.Text = $"Finished autoshow scan at: {DateTime.Now}. Scan took: {(DateTime.Now - start).Seconds + "," + (DateTime.Now - start).Milliseconds}s\nSQL Ptr: {_ptr:X}");
#endif
    }
    
    public static async void ExecSql(object button, RoutedEventHandler action, string sql)
    {
        if (!Mw.Attached)
        {
            return;
        }

        button.GetType().GetProperty("IsEnabled")?.SetValue(button, false);

        var start = DateTime.Now;
        
#if !RELEASE
        Mw.Dispatcher.Invoke(() => Mw.DebugLabel.Text = $"Executing SQL at: {start}");
#endif
        var retValue = await Task.Run(() => Query(sql));
      
#if !RELEASE
        Mw.Dispatcher.Invoke(() => Mw.DebugLabel.Text = $"Finished Executing SQL at: {DateTime.Now}.\nExecuting took: {(DateTime.Now - start).Milliseconds} MS");
#endif      
        /*await Task.Run(() =>
        {
            using var pipeClient = new NamedPipeClientStream("PogPipe");
            var count = 0;
            while (!pipeClient.IsConnected && count < 25)
            {
                try
                {
                    pipeClient.Connect(100);
                }
                catch
                {
                    // ignored
                }

                ++count;
                Task.Delay(10).Wait();
            }

            if (count == 25)
            {
                MessageBox.Show("Failed, sowwy oomfie :3");
                return;
            }

            using var streamWriter = new StreamWriter(pipeClient);
                
            if (streamWriter.AutoFlush == false)
            {
                streamWriter.AutoFlush = true;
            }

            streamWriter.WriteLine(sql);
          
            retValue = true;
        });*/

        if (button.GetType() == typeof(ToggleSwitch) && !retValue)
        {
            ((ToggleSwitch)button).Toggled -= action;
            ((ToggleSwitch)button).IsOn = false;
            ((ToggleSwitch)button).Toggled += action;
        }
            
        button.GetType().GetProperty("IsEnabled")?.SetValue(button, true);
    }
    
    private static bool SqlExecAobScan()
    {
        if (_ptr != nuint.Zero)
        {
            return true;
        }

        var signature = Mw.Gvp.Name.Contains('5')
            ? "0F 84 ? ? ? ? 48 8B 35 ? ? ? ? 48 85 F6 74"
            : "0F 84 ? ? ? ? 48 8B ? ? ? ? ? 48 8D ? ? ? ? ? 66 0F";
        
        var sigResult = Mw.M.ScanForSig(signature).FirstOrDefault();

        if (sigResult == 0)
        {
            return false;
        }

        _ptr = Utils.GetPtrFromFunc(sigResult, 9, 13);
        return true;
    }
    
    private static nuint GetVirtualFunctionPtr(nuint ptr, int index)
    {
        var pVtableBytes = new byte[8];
        var procHandle = Mw.Gvp.Process.Handle;
        ReadProcessMemory(procHandle, ptr, pVtableBytes, (nuint)pVtableBytes.Length, nint.Zero);

        var pVtable = (nuint)ToInt64(pVtableBytes, 0);
        var vTableBytes = new byte[8];
        var lpBaseAddress = pVtable + (nuint)nuint.Size * (nuint)index;
        ReadProcessMemory(procHandle, lpBaseAddress, vTableBytes, (nuint)vTableBytes.Length, nint.Zero);

        return (nuint)ToInt64(vTableBytes, 0);
    }
    
    [DllImport("kernel32", SetLastError = true)]
    private static extern int WaitForSingleObject(IntPtr handle, int milliseconds);
    
    private static bool Query(string command)
    {
        if (!SqlExecAobScan())
        {
            return false;
        }

        var procHandle = Mw.Gvp.Process.Handle;
        var allocShellCodeAddress = VirtualAllocEx(procHandle, nuint.Zero, 0x1000, 0x3000, 0x40);

        var rcx = _ptr;
        var rdx = VirtualAllocEx(procHandle, nuint.Zero, 0x1000, 0x3000, 0x40);
        var r8 = VirtualAllocEx(procHandle, nuint.Zero, 0x1000, 0x3000, 0x40);

        byte[] shellCode = {
            0x48,0xBA, 0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,                     
            0x49,0xB8, 0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,                    
            0xFF,0x25, 0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00 
        };

        BlockCopy(GetBytes(rdx.ToUInt64()), 0, shellCode, 0x02, 8);
        BlockCopy(GetBytes(r8.ToUInt64()), 0, shellCode, 0x0C, 8);

        if (_callFunction == nuint.Zero)
        {
            _callFunction = GetVirtualFunctionPtr(_ptr, VirtualFunctionIndex);
        }

        BlockCopy(GetBytes(_callFunction.ToUInt64()), 0, shellCode, shellCode.Length - 8, 8);

        Mw.M.WriteStringMemory(r8, command + "\0");

        WriteProcessMemory(procHandle, allocShellCodeAddress, shellCode, (nuint)shellCode.Length, nint.Zero);

        if (Mw.Gvp.Name.Contains('4'))
        {
            Bypass.DisableAntiCheat();
        }
        
        var handle = CreateRemoteThread(procHandle, (nint)null, 0, allocShellCodeAddress, rcx, 0, out _);

        WaitForSingleObject(handle, int.MaxValue);
        VirtualFreeEx(procHandle, allocShellCodeAddress, 0x1000, 0x4000);
        VirtualFreeEx(procHandle, r8, 0x1000, 0x4000);

        var resultBytes = new byte[8];
        ReadProcessMemory(procHandle, rdx, resultBytes, (nuint)resultBytes.Length, nint.Zero);
        return true;
    }
}