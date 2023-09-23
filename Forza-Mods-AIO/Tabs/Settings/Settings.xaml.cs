using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Forza_Mods_AIO.Overlay;
using IniParser;

namespace Forza_Mods_AIO.Tabs.Settings;

/// <summary>
///     Interaction logic for Settings.xaml
/// </summary>
public partial class Settings : Page
{
    public static Settings S;
    private bool Clicked;

    public Settings()
    {
        InitializeComponent();
        S = this;
        
        KeybindsHandling.UpdateKeybindingOnLaunch();
                
        UpButton.Content = OverlayHandling.Up;
        DownButton.Content = OverlayHandling.Down;
        LeftButton.Content = OverlayHandling.Left;
        RightButton.Content = OverlayHandling.Right;
        ConfirmButton.Content = OverlayHandling.Confirm;
        LeaveButton.Content = OverlayHandling.Leave;
        OverlayVisibilityButton.Content = OverlayHandling.OverlayVisibility;
    }
    
    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        if (Clicked)
            return;

        Clicked = true;
        ((Button)sender).Content = "Press some key";

        await Task.Run(() => { KeybindsHandling.KeyGrabber((Button)sender); });

        Clicked = false;
    }

    private void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        var SettingsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Forza Mods AIO\Overlay_Settings.ini";

        try
        {
            var Parser = new FileIniDataParser();
            var Data = Parser.ReadFile(SettingsFilePath);
            Data["Keybinds"]["Up"] = OverlayHandling.Up.ToString();
            Data["Keybinds"]["Down"] = OverlayHandling.Down.ToString();
            Data["Keybinds"]["Left"] = OverlayHandling.Left.ToString();
            Data["Keybinds"]["Right"] = OverlayHandling.Right.ToString();
            Data["Keybinds"]["Confirm"] = OverlayHandling.Confirm.ToString();
            Data["Keybinds"]["Leave"] = OverlayHandling.Leave.ToString();
            Data["Keybinds"]["Visibility"] = OverlayHandling.OverlayVisibility.ToString();

            Parser.WriteFile(SettingsFilePath, Data);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Failed to save keybinds.");
            Console.WriteLine(ex.StackTrace);
        }
    }
}