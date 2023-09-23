using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Forza_Mods_AIO.Overlay;
using IniParser;
using static System.Enum;
using Application = System.Windows.Application;
using Button = System.Windows.Controls.Button;

namespace Forza_Mods_AIO.Tabs.Settings;

internal abstract partial class KeybindsHandling
{
    [LibraryImport("User32.dll")]
    private static partial short GetAsyncKeyState(int vKey);

    public static void KeyGrabber(Button sender)
    {
        var keyBuffer = string.Empty;
        var pressedKey = 0;

        while (keyBuffer.Length == 0)
        {
            foreach (int i in GetValues(typeof(Keys)))
            {
                int x = GetAsyncKeyState(i);
                if (x is not (1 or short.MinValue)) continue;
                if (i is 0 or 1 or 2 or 3 or 4 or 12) continue;
                pressedKey = i;
                break;
            }

            if (pressedKey != 0)
                keyBuffer += GetName(typeof(Keys), pressedKey);
        }

        Application.Current.Dispatcher.Invoke(() =>
        {
            sender.Content = keyBuffer;
            
            foreach (var field in typeof(OverlayHandling).GetFields())
            {
                if (field.Name != sender.Name.Replace("Button", String.Empty)) continue;
                field.SetValue(new OverlayHandling(), (Keys)Parse(typeof(Keys), keyBuffer));
            }
        });
    }

    public static void UpdateKeybindingOnLaunch()
    {
        var SettingsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Forza Mods AIO\Overlay_Settings.ini";

        if (!File.Exists(SettingsFilePath) || new FileInfo(SettingsFilePath).Length == 0)
            using (File.Create(SettingsFilePath)) { }

        var Parser = new FileIniDataParser();
        var IniData = Parser.ReadFile(SettingsFilePath);
        TryParse(IniData["Keybinds"]["Up"], out OverlayHandling.Up);
        TryParse(IniData["Keybinds"]["Down"], out OverlayHandling.Down);
        TryParse(IniData["Keybinds"]["Left"], out OverlayHandling.Left);
        TryParse(IniData["Keybinds"]["Right"], out OverlayHandling.Right);
        TryParse(IniData["Keybinds"]["Confirm"], out OverlayHandling.Confirm);
        TryParse(IniData["Keybinds"]["Leave"], out OverlayHandling.Leave);
        TryParse(IniData["Keybinds"]["Visibility"], out OverlayHandling.OverlayVisibility);
    }
}