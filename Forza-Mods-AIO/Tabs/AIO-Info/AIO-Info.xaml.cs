using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
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
        if (Overlay.Overlay.O == null)
            _ = new Overlay.Overlay();
        Overlay.Overlay.O.OverlayToggle(OverlaySwitch.IsOn);
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
            case "DraffCoffee":
            {
                Process.Start("explorer.exe", "https://www.buymeacoffee.com/comamnds");
                break;
            }
            case "EthanPaypal":
            {
                Process.Start("explorer.exe", "https://www.paypal.com/donate?hosted_button_id=DACQKRJ4HTZR");
                break;
            }
            case "DraffPaypal":
            {
                Process.Start("explorer.exe", "https://www.paypal.com/donate?hosted_button_id=H37GURADQ2SX");
                break;
            }
            case "DraffYoutube":
            {
                Process.Start("explorer.exe", "https://www.youtube.com/c/comamnds/");
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
}