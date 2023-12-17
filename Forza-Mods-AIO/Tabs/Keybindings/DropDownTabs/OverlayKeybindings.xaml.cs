using System.IO;
using System.Windows;
using System.Windows.Controls;
using Forza_Mods_AIO.Overlay;
using IniParser;
using static System.Enum;
using static System.Environment;
using static Forza_Mods_AIO.Overlay.OverlayHandling;
using static Forza_Mods_AIO.MainWindow;

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
        // TODO: Implement
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
    }

    public static void SaveKeybinds()
    {
        var settingsFilePath = GetFolderPath(SpecialFolder.MyDocuments) + @"\Forza Mods AIO\Overlay Settings.ini";
        var parser = new FileIniDataParser();
        var iniData = parser.ReadFile(settingsFilePath);
        iniData["Keybinds"]["Up"] = Up.ToString();
        iniData["Keybinds"]["Down"] = Down.ToString();
        iniData["Keybinds"]["Left"] = Left.ToString();
        iniData["Keybinds"]["Right"] = Right.ToString();
        iniData["Keybinds"]["Confirm"] = Confirm.ToString();
        iniData["Keybinds"]["Leave"] = Leave.ToString();
        iniData["Keybinds"]["Visibility"] = OverlayVisibility.ToString();            
        iniData["Keybinds"]["RapidAdjust"] = OverlayHandling.RapidAdjust.ToString();     
        parser.WriteFile(settingsFilePath, iniData);
    }
}