using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using Forza_Mods_AIO.Models;

namespace Forza_Mods_AIO.Resources.Keybinds;

// https://github.com/AngryCarrot789/KeyDownTester/blob/master/KeyDownTester/Keys/HotkeysManager.cs
public static class HotkeysManager
{
    private const int WhKeyboardLl = 13;
    private static readonly LowLevelKeyboardProc LowLevelProc = HookCallback;
    private static IntPtr _hookId = IntPtr.Zero;
    private static readonly DebugSession HotkeyDebugSession = new("Hotkey Debug Session", [], []);
    
    static HotkeysManager()
    {
        Hotkeys = new List<GlobalHotkey>();
        DebugSessions.GetInstance().EveryDebugSession.Add(HotkeyDebugSession);
    }

    private static List<GlobalHotkey> Hotkeys { get; }

    public static void SetupSystemHook()
    {
        _hookId = SetHook(LowLevelProc);
    }

    public static void ShutdownSystemHook()
    {
        UnhookWindowsHookEx(_hookId);
    }

    public static void AddHotkey(GlobalHotkey hotkey)
    {
        Hotkeys.Add(hotkey);
        HotkeyDebugSession.DebugInfoReports.Add(new DebugInfoReport("Added hotkey"));
    }

    public static void RemoveHotkey(GlobalHotkey hotkey)
    {
        try
        {
            foreach (var globalHotkey in Hotkeys.Where(globalHotkey => hotkey.Modifier == globalHotkey.Modifier && hotkey.Key == globalHotkey.Key))
            {
                Hotkeys.Remove(globalHotkey);
                HotkeyDebugSession.DebugInfoReports.Add(new DebugInfoReport("Removed hotkey"));
            }
        }
        catch
        {
            // ignored
        }
    }

    private static void CheckHotkeys()
    {
        Task.Run(async () =>
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                foreach (var hotkey in Hotkeys
                             .Where(hotkey =>
                                 Keyboard.Modifiers == hotkey.Modifier && hotkey.Key != Key.None &&
                                 Keyboard.IsKeyDown(hotkey.Key))
                             .Where(hotkey => hotkey.CanExecute))
                {
                    hotkey.Callback();
                }
            });
        });
    }

    private static IntPtr SetHook(LowLevelKeyboardProc proc)
    {
        using var curProcess = Process.GetCurrentProcess();
        using var curModule = curProcess.MainModule;
        return curModule == null ? 0 : SetWindowsHookEx(WhKeyboardLl, proc, Imports.GetModuleHandle(curModule.ModuleName), 0);
    }

    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0) CheckHotkeys();
        return CallNextHookEx(_hookId, nCode, wParam, lParam);
    }

    private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

    #region Native Methods

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    #endregion
}