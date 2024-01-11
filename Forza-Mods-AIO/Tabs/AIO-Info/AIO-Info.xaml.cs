using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Resources.DllImports;
using Monet = Forza_Mods_AIO.Resources.Theme.Monet;

namespace Forza_Mods_AIO.Tabs.AIO_Info;

/// <summary>
///     Interaction logic for AIO_Info.xaml
/// </summary>
public partial class AioInfo
{
    public static AioInfo Ai { get; private set; } = null!;

    public AioInfo()
    {
        InitializeComponent();
        VersionLabel.Content += Assembly.GetExecutingAssembly().GetName().Version!.ToString(); 
        Ai = this;
    }

    private void WallButton_Click(object sender, RoutedEventArgs e)
    {
        Task.Run(Monet.ApplyMonet);
    }

    private void OverlaySwitch_Toggled(object sender, RoutedEventArgs e)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (Overlay.Overlay.O == null)
            _ = new Overlay.Overlay();
        Overlay.Overlay.O?.OverlayToggle(OverlaySwitch.IsOn);
    }

    private void Button_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton != MouseButton.Left)
        {
            return;
        }

        switch ((string)sender.GetType().GetProperty("Name")?.GetValue(sender)!)
        {
            case "EthanCoffee":
            {
                Process.Start("explorer.exe", "https://www.buymeacoffee.com/Yeethan69");
                break;
            }
            case "EthanPaypal":
            {
                Process.Start("explorer.exe", "https://www.paypal.com/donate?hosted_button_id=DACQKRJ4HTZR");
                break;
            }
            case "ForzaModsDiscord":
            {
                Process.Start("explorer.exe", "https://discord.gg/forzamods");
                break;
            }
            case "MerikCoffee":
            {
                Process.Start("explorer.exe", "https://www.buymeacoffee.com/merika");
                break;
            }
        }
    }
    
    private string _originalTitle = string.Empty;
    
    private void CustomTitle_OnToggled(object sender, RoutedEventArgs e)
    {
        if (_originalTitle == string.Empty)
        {
            _originalTitle = GetWindowTitle(Mw.Gvp.Process.MainWindowHandle);
        }

        SetWindowText(Mw.Gvp.Process.MainWindowHandle, CustomTitle.IsOn ? CustomTitleText.Text : _originalTitle);
    }
    
    private static string GetWindowTitle(IntPtr hWnd)
    {
        var length = GetWindowTextLength(hWnd) + 1;
        var title = new StringBuilder(length);
        var windowText = GetWindowText(hWnd, title, length);
        return windowText == -1 ? string.Empty : title.ToString();
    }

    private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (!CustomTitle.IsOn)
        {
            return;
        }
        
        SetWindowText(Mw.Gvp.Process.MainWindowHandle, CustomTitleText.Text);
    }
}