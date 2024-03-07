using Memory.Types;
using System.Windows;
using Forza_Mods_AIO.Models;
using MahApps.Metro.Controls;
using static Forza_Mods_AIO.Resources.Memory;

namespace Forza_Mods_AIO.Cheats;

public class CheatsUtilities
{
    protected static async Task<nuint> SmartAobScan(string search)
    {
        Imps.GetSystemInfo(out var info);
        UIntPtr address = 0;

        var handle = GetInstance().MProc.Handle;
        var minRange = (long)GetInstance().MProc.Process.MainModule!.BaseAddress;
        var maxRange = minRange + GetInstance().MProc.Process.MainModule!.ModuleMemorySize;
        Imps.Native_VirtualQueryEx(handle, address, out Imps.MemoryBasicInformation64 memInfo, info.PageSize);

        var scanStartAddr = minRange;
        address = (UIntPtr)minRange;
        while (address < (ulong)maxRange)
        {
            Imps.Native_VirtualQueryEx(handle, address, out memInfo, info.PageSize);
            if (address == memInfo.BaseAddress + memInfo.RegionSize)
            {
                break;
            }

            var scanEndAddr = (long)memInfo.BaseAddress + (long)memInfo.RegionSize;

            nuint retAddress;
            if (scanEndAddr - scanStartAddr > 500000000)
            {
                retAddress = await ScanRange(search, scanStartAddr, scanEndAddr);
            }
            else
            {
                retAddress = (await GetInstance().AoBScan(scanStartAddr, scanEndAddr, search)).FirstOrDefault();
            }

            if (retAddress != 0)
            {
                return retAddress;
            }

            scanStartAddr = scanEndAddr;
            address = memInfo.BaseAddress + (UIntPtr)memInfo.RegionSize;
        }

        return 0;
    }

    private static async Task<nuint> ScanRange(string search, long startAddr, long endAddr)
    {
        var end = startAddr + (endAddr - startAddr) / 2;
        var retAddress = (await GetInstance().AoBScan(startAddr, end, search)).FirstOrDefault();
        return retAddress;
    }
    
    protected static void ShowError(string feature, string sig)
    {
        MessageBox.Show(
            $"Address for this feature wasn't found!\nPlease try to activate the cheat again or try to restart the game and the tool.\n\nIf this error still occurs, please (Press Ctrl+C) to copy, and make an issue on the github repository or post the copied text on the aio mega-thread in our discord server (discord.gg/forzamods).\n\nFeature: {feature}\nSignature: {sig}\n\nTool Version: {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}\nGame: {GameVerPlat.GetInstance().Name}\nGame Version: {GameVerPlat.GetInstance().Update}\nPlatform: {GameVerPlat.GetInstance().Platform}",
            $"{App.GetRequiredService<MetroWindow>().Title} - Error", 0, MessageBoxImage.Error);
    }

    protected static void Free(UIntPtr address)
    {
        if (address == 0) return;
        var handle = GetInstance().MProc.Handle;
        Imps.VirtualFreeEx(handle, address,0, Imps.MemRelease);
    }
}