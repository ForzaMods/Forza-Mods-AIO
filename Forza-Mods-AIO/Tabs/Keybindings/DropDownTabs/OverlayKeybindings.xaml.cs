using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Forza_Mods_AIO.Overlay;
using IniParser;
using static System.Enum;
using static Forza_Mods_AIO.Overlay.OverlayHandling;
using static Forza_Mods_AIO.MainWindow;

namespace Forza_Mods_AIO.Tabs.Keybindings.DropDownTabs;

public partial class OverlayKeybindings
{
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
        var settingsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Forza Mods AIO\Overlay Settings.ini";
        
        if (!File.Exists(settingsFilePath))
        {
            Overlay.SettingsMenu.SettingsMenu.LoadSettings.IsEnabled = false;
            return;
        }

        try
        {
            var parser = new FileIniDataParser();
            var iniData = parser.ReadFile(settingsFilePath);
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
}