using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using Forza_Mods_AIO.Models;

namespace Forza_Mods_AIO.Resources.Keybinds;

// https://github.com/AngryCarrot789/KeyDownTester/blob/master/KeyDownTester/Keys/HotkeysManager.cs
public static partial class HotkeysManager
{
    private const int WhKeyboardLl = 13;
    private static readonly LowLevelKeyboardProc LowLevelProc = HookCallback;
    private static IntPtr _hookId = IntPtr.Zero;
    
    static HotkeysManager()
    {
        Hotkeys = new List<GlobalHotkey>();
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
    }

    public static void RemoveHotkey(GlobalHotkey hotkey)
    {
        try
        {
            foreach (var globalHotkey in Hotkeys.Where(globalHotkey => hotkey.Modifier == globalHotkey.Modifier && hotkey.Key == globalHotkey.Key))
            {
                Hotkeys.Remove(globalHotkey);
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

    [LibraryImport("user32.dll", EntryPoint = "SetWindowsHookExA", SetLastError = true)]
    private static partial IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

    [LibraryImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial void UnhookWindowsHookEx(IntPtr hhk);

    [LibraryImport("user32.dll", SetLastError = true)]
    private static partial IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    #endregion
}