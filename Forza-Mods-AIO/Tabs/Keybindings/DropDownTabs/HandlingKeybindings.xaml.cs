using IniParser;
using System.IO;
using System.Windows;
using System.Windows.Controls;

using static System.Enum;
using static System.Environment;
using static System.Windows.Forms.Keys;
using static Forza_Mods_AIO.MainWindow;

using Forms = System.Windows.Forms;

namespace Forza_Mods_AIO.Tabs.Keybindings.DropDownTabs;

public partial class HandlingKeybindings
{
    public Forms.Keys JmpHack = LControlKey, BrakeHack = Space, VelHack = LShiftKey, WheelspeedHack = W;
    public static HandlingKeybindings Hk { get; private set; } = null!;
    
    public HandlingKeybindings()
    {
        InitializeComponent();
        LoadKeybindings();
        Hk = this;
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
        // TODO: Implement
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
            TryParse(iniData["Keybinds"]["Up"], out JmpHack);
            TryParse(iniData["Keybinds"]["Down"], out BrakeHack);
            TryParse(iniData["Keybinds"]["Left"], out VelHack);
            TryParse(iniData["Keybinds"]["Right"], out WheelspeedHack);
        }
        catch {}
    }
}