using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Forza_Mods_AIO.Overlay;
using IniParser;
using SharpDX.XInput;
using static System.Enum;
using static System.Environment;
using static Forza_Mods_AIO.Overlay.OverlayHandling;
using static Forza_Mods_AIO.MainWindow;
using Gamepad = Forza_Mods_AIO.Resources.Gamepad;

namespace Forza_Mods_AIO.Tabs.Keybindings.DropDownTabs;

public partial class OverlayKeybindings
{
    private readonly string _settingsFilePath = GetFolderPath(SpecialFolder.MyDocuments) + @"\Forza Mods AIO\Overlay Settings.ini";
    
    public OverlayKeybindings()
    {
        InitializeComponent();
        UpdateKeybinds();
        UpdateKeybindButtons();
    }

    private void KBButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (Mw.Grabbing) return;
        Mw.IsClicked = Mw.Grabbing = true;
        Mw.ClickedButton = (Button)sender;
        Mw.ClickedButton.Content = "Change Key";   
    }
    
    private void CTButton_OnClick(object sender, RoutedEventArgs e)
    {
        Mw.Gamepad.InitializeControllersAndInput();

        ((Button)sender).Content = "Change Key";
        
        if (Mw.Gamepad.GetXInputController() != null!)
        {
            GetXInputKey((Button)sender);
            return;
        }

        if (Mw.Gamepad.GetDInputController() == null!) return;
        GetDInputKey((Button)sender);
    }

    private static async void GetXInputKey(ContentControl button)
    {
        var keyBuffer = string.Empty;
        while (keyBuffer.Length == 0)
        {
            Mw.Gamepad.InitializeControllersAndInput();

            if (!Mw.Gamepad.IsControllerConnected)
            {
                return;
            }

            foreach (GamepadButtonFlags buttonFlag in GetValues(typeof(GamepadButtonFlags)))
            {
                if (buttonFlag == GamepadButtonFlags.None) continue;

                var isPressed = IsXInputButtonPressed(buttonFlag);

                if (!isPressed) continue;
                keyBuffer = buttonFlag.ToString();
                break;
            }

            await Task.Delay(5);
        }
        
        foreach (var field in typeof(OverlayHandling).GetFields(BindingFlags.Public | BindingFlags.Static).Where(f => f.FieldType == typeof(string)))
        {
            if (field.Name != button?.Name.Replace("Button", string.Empty))
            {
                continue;
            }
            
            field.SetValue(Overlay.Overlay.Oh, keyBuffer);
            button.Content = keyBuffer;
            SaveKeybinds();
            return;
        }
    }
    
    private static async void GetDInputKey(ContentControl button)
    {
        var keyBuffer = string.Empty;
        while (keyBuffer.Length == 0)
        {
            Mw.Gamepad.GetDInputController().Acquire();
            var data = Mw.Gamepad.GetDInputController().GetCurrentState();
            var controllerButtonState = data.Buttons;
            var indices = new List<int>();
            string? analogue = null;
            for (var i = 0; i < 9; ++i)
            {
                analogue = data.PointOfViewControllers[0] switch
                {
                    0 => "DpadUp",
                    9000 => "DpadRight",
                    18000 => "DpadDown",
                    27000 => "DpadLeft",
                    _ => data.Z switch
                    {
                        > 50000 => "LeftTrigger",
                        < 20000 => "RightTrigger",
                        _ => analogue
                    }
                };
                if (controllerButtonState[i])
                {
                    indices.Add(i);
                }
            }
            
            if (indices.Count == 1)
            {
                var xbButtonIndex = indices[0];
                if (xbButtonIndex <= 9)
                {
                    button.Content = Gamepad.DInputMap[xbButtonIndex];
                    keyBuffer = Gamepad.DInputMap[xbButtonIndex];
                    break;
                }
            }
            else if (analogue != null)
            {
                button.Content = analogue;
                keyBuffer = analogue;
                break;
            }

            await Task.Delay(5);
        }
        
        foreach (var field in typeof(OverlayHandling).GetFields(BindingFlags.Public | BindingFlags.Static).Where(f => f.FieldType == typeof(string)))
        {
            if (field.Name != button?.Name.Replace("Button", string.Empty))
            {
                continue;
            }
            
            field.SetValue(Overlay.Overlay.Oh, keyBuffer);
            button.Content = keyBuffer;
            SaveKeybinds();
            return;
        }
    }
    
    private static bool IsXInputButtonPressed(GamepadButtonFlags button)
    {
        var state = Mw.Gamepad.GetXInputController().GetState();
        return (state.Gamepad.Buttons & button) != 0;
    }
    
    private void UpdateKeybinds() 
    {
        if (!File.Exists(_settingsFilePath))
        {
            return;
        }

        try
        {
            var parser = new FileIniDataParser();
            var iniData = parser.ReadFile(_settingsFilePath);
            TryParse(iniData["Keybinds"]["Up"], out Up);
            TryParse(iniData["Keybinds"]["Down"], out Down);
            TryParse(iniData["Keybinds"]["Left"], out Left);
            TryParse(iniData["Keybinds"]["Right"], out Right);
            TryParse(iniData["Keybinds"]["Confirm"], out Confirm);
            TryParse(iniData["Keybinds"]["Leave"], out Leave);
            TryParse(iniData["Keybinds"]["Visibility"], out OverlayVisibility);            
            TryParse(iniData["Keybinds"]["RapidAdjust"], out OverlayHandling.RapidAdjust);

            /*if (iniData["Controller Keybinds"]["Up"] == null)
            {
                return;
            }

            ControllerUp = iniData["Controller Keybinds"]["Up"];
            ControllerDown = iniData["Controller Keybinds"]["Down"];
            ControllerLeft = iniData["Controller Keybinds"]["Left"];
            ControllerRight = iniData["Controller Keybinds"]["Right"];
            ControllerLeave = iniData["Controller Keybinds"]["Confirm"];
            ControllerConfirm = iniData["Controller Keybinds"]["Leave"];
            ControllerOverlayVisibility = iniData["Controller Keybinds"]["Visibility"];
            OverlayHandling.ControllerRapidAdjust = iniData["Controller Keybinds"]["RapidAdjust"];*/
        }
        catch {}
    }

    private void UpdateKeybindButtons()
    {
        UpButton.Content = Up;
        DownButton.Content = Down;
        LeftButton.Content = Left;
        RightButton.Content = Right;
        LeaveButton.Content = Leave;
        ConfirmButton.Content = Confirm;
        OverlayVisibilityButton.Content = OverlayVisibility;
        RapidAdjust.Content = OverlayHandling.RapidAdjust;

        ControllerUpButton.Content = ControllerUp;
        ControllerDownButton.Content = ControllerDown;
        ControllerLeftButton.Content = ControllerLeft;
        ControllerRightButton.Content = ControllerRight;
        ControllerLeaveButton.Content = ControllerLeave;
        ControllerConfirmButton.Content = ControllerConfirm;
        ControllerOverlayVisibilityButton.Content = ControllerOverlayVisibility;
        ControllerRapidAdjust.Content = OverlayHandling.ControllerRapidAdjust;
    }

    public static void SaveKeybinds()
    {
        var aioFolderPath = GetFolderPath(SpecialFolder.MyDocuments) + @"\Forza Mods AIO";

        if (!Directory.Exists(aioFolderPath))
        {
            Directory.CreateDirectory(aioFolderPath);
        }

        var configFile = aioFolderPath + @"\Overlay Settings.ini";

        if (!File.Exists(configFile))
        {
            var configFileCreation = File.Create(configFile);
            configFileCreation.Close();
        }

        var parser = new FileIniDataParser();
        var iniData = parser.ReadFile(configFile);
        iniData["Keybinds"]["Up"] = Up.ToString();
        iniData["Keybinds"]["Down"] = Down.ToString();
        iniData["Keybinds"]["Left"] = Left.ToString();
        iniData["Keybinds"]["Right"] = Right.ToString();
        iniData["Keybinds"]["Confirm"] = Confirm.ToString();
        iniData["Keybinds"]["Leave"] = Leave.ToString();
        iniData["Keybinds"]["Visibility"] = OverlayVisibility.ToString();
        iniData["Keybinds"]["RapidAdjust"] = OverlayHandling.RapidAdjust.ToString();

        /*iniData["Controller Keybinds"]["Up"] = ControllerUp;
        iniData["Controller Keybinds"]["Down"] = ControllerDown;
        iniData["Controller Keybinds"]["Left"] = ControllerLeft;
        iniData["Controller Keybinds"]["Right"] = ControllerRight;
        iniData["Controller Keybinds"]["Confirm"] = ControllerConfirm;
        iniData["Controller Keybinds"]["Leave"] = ControllerLeave;
        iniData["Controller Keybinds"]["Visibility"] = ControllerOverlayVisibility;
        iniData["Controller Keybinds"]["RapidAdjust"] = OverlayHandling.ControllerRapidAdjust;  */
        parser.WriteFile(configFile, iniData);
    }
}