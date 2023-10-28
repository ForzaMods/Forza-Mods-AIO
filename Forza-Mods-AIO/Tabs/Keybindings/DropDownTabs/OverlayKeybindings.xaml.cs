using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using IniParser;
using static System.Enum;

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
        if (MainWindow.Grabbing) return;
        MainWindow.IsClicked = MainWindow.Grabbing = true;
        MainWindow.ClickedButton = (Button)sender;
        MainWindow.ClickedButton.Content = "Change Key";   
    }
    
    private void CTButton_OnClick(object sender, RoutedEventArgs e)
    {
        // TODO: Implement
    }
    
    private void UpdateKeybinds() 
    {
        var SettingsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Forza Mods AIO\Overlay Settings.ini";
        
        if (!File.Exists(SettingsFilePath))
        {
            Overlay.SettingsMenu.SettingsMenu.LoadSettings.IsEnabled = false;
            return;
        }

        try
        {
            var Parser = new FileIniDataParser();
            var IniData = Parser.ReadFile(SettingsFilePath);
            TryParse(IniData["Keybinds"]["Up"], out Overlay.OverlayHandling.Up);
            TryParse(IniData["Keybinds"]["Down"], out Overlay.OverlayHandling.Down);
            TryParse(IniData["Keybinds"]["Left"], out Overlay.OverlayHandling.Left);
            TryParse(IniData["Keybinds"]["Right"], out Overlay.OverlayHandling.Right);
            TryParse(IniData["Keybinds"]["Confirm"], out Overlay.OverlayHandling.Confirm);
            TryParse(IniData["Keybinds"]["Leave"], out Overlay.OverlayHandling.Leave);
            TryParse(IniData["Keybinds"]["Visibility"], out Overlay.OverlayHandling.OverlayVisibility);            
            
        }
        catch {}
    }

    private void UpdateKeybindButtons()
    {
        UpButton.Content = Overlay.OverlayHandling.Up;
        DownButton.Content = Overlay.OverlayHandling.Down;
        LeftButton.Content = Overlay.OverlayHandling.Left;
        RightButton.Content = Overlay.OverlayHandling.Right;
        LeaveButton.Content = Overlay.OverlayHandling.Leave;
        ConfirmButton.Content = Overlay.OverlayHandling.Confirm;
        OverlayVisibilityButton.Content = Overlay.OverlayHandling.OverlayVisibility;
    }
}