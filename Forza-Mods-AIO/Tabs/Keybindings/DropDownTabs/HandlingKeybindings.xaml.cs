using IniParser;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Forza_Mods_AIO.Overlay;
using SharpDX.XInput;
using static System.Enum;
using static System.Environment;
using static System.Windows.Forms.Keys;
using static Forza_Mods_AIO.MainWindow;

using Forms = System.Windows.Forms;

namespace Forza_Mods_AIO.Tabs.Keybindings.DropDownTabs;

public partial class HandlingKeybindings
{
    public Forms.Keys JmpHack = LControlKey, BrakeHack = Space, VelHack = LShiftKey, WheelspeedHack = W;
    public static string JmpHackController = "RightThumb",
        BrakeHackController = "A",
        VelHackController = "LeftShoulder",
        WheelspeedHackController = "LeftShoulder";
    
    
    public static HandlingKeybindings Hk { get; private set; } = null!;
    private bool _controllerReading;
    
    public HandlingKeybindings()
    {
        InitializeComponent();
        LoadKeybindings();
        Hk = this;
    }

    private void KBButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (Mw.IsClicked)
        {
            return;
        }
        
        Mw.IsClicked = true;
        Mw.ClickedButton = (Button)sender;
        Mw.ClickedButton.Content = "Change Key"; 
    }
    
    private void CTButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (_controllerReading)
        {
            return;
        }
        
        Mw.Gamepad.InitializeControllersAndInput();
        if (Mw.Gamepad.GetXInputController() == null!) return;
        ((Button)sender).Content = "Change Key";
        _controllerReading = true;
        GetXInputKey((Button)sender);
    }
    
    private async void GetXInputKey(ContentControl button)
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

                var isPressed = Mw.Gamepad.IsButtonPressed(buttonFlag.ToString());

                if (!isPressed) continue;
                keyBuffer = buttonFlag.ToString();
                break;
            }

            await Task.Delay(5);
        }
        
        foreach (var field in typeof(HandlingKeybindings).GetFields(BindingFlags.Public | BindingFlags.Static).Where(f => f.FieldType == typeof(string)))
        {
            if (field.Name != button?.Name.Replace("Button", string.Empty))
            {
                continue;
            }
            
            field.SetValue(Hk, keyBuffer);
            button.Content = keyBuffer;
            SaveKeybindings();
            _controllerReading = false;
            return;
        }
    }

    public void SaveKeybindings()
    {
        var aioFolderPath = GetFolderPath(SpecialFolder.MyDocuments) + @"\Forza Mods AIO";

        if (!Directory.Exists(aioFolderPath))
        {
            Directory.CreateDirectory(aioFolderPath);
        }

        var configFile = aioFolderPath + @"\Keybindings.ini";

        if (!File.Exists(configFile))
        {
            var configFileCreation = File.Create(configFile);
            configFileCreation.Close();
        }

        var parser = new FileIniDataParser();
        var iniData = parser.ReadFile(configFile);
        iniData["Keybinds"]["Jump Hack"] = JmpHack.ToString();
        iniData["Keybinds"]["Brake Hack"] = BrakeHack.ToString();
        iniData["Keybinds"]["Vel Hack"] = VelHack.ToString();
        iniData["Keybinds"]["Wheelspeed Hack"] = WheelspeedHack.ToString();
        
        iniData["Controller Keybinds"]["Jump Hack"] = JmpHackController;
        iniData["Controller Keybinds"]["Brake Hack"] = BrakeHackController;
        iniData["Controller Keybinds"]["Vel Hack"] = VelHackController;
        iniData["Controller Keybinds"]["Wheelspeed Hack"] = WheelspeedHackController;
        parser.WriteFile(configFile, iniData);
    }

    private void LoadKeybindings()
    {
        var aioFolderPath = GetFolderPath(SpecialFolder.MyDocuments) + @"\Forza Mods AIO";
        var configFile = aioFolderPath + @"\Keybindings.ini";

        if (!File.Exists(configFile))
        {
            return;
        }

        try
        {
            var parser = new FileIniDataParser();
            var iniData = parser.ReadFile(configFile);
            TryParse(iniData["Keybinds"]["Jump Hack"], out JmpHack);
            TryParse(iniData["Keybinds"]["Brake Hack"], out BrakeHack);
            TryParse(iniData["Keybinds"]["Vel Hack"], out VelHack);
            TryParse(iniData["Keybinds"]["Wheelspeed Hack"], out WheelspeedHack);

            JmpHackController = iniData["Controller Keybinds"]["Jump Hack"];
            BrakeHackController = iniData["Controller Keybinds"]["Brake Hack"];
            VelHackController = iniData["Controller Keybinds"]["Vel Hack"];
            WheelspeedHackController = iniData["Controller Keybinds"]["Wheelspeed Hack"];

            JmpHackButton.Content = JmpHack.ToString();
            BrakeHackButton.Content = BrakeHack.ToString();
            VelHackButton.Content = VelHack.ToString();
            WheelspeedHackButton.Content = WheelspeedHack.ToString();
            
            JmpHackControllerButton.Content = JmpHackController;
            BrakeHackControllerButton.Content = BrakeHackController;
            VelHackControllerButton.Content = VelHackController;
            WheelspeedHackControllerButton.Content = WheelspeedHackController;
        }
        catch {}
    }
}