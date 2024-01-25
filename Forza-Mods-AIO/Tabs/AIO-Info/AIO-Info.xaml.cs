using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Forza_Mods_AIO.Resources;
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

    private readonly TranslateUtil _translateUtil = new();

    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is not ComboBox comboBox)
        {
            return;
        }

        switch (comboBox.SelectedIndex)
        {
            case 0: // english
            {
                _translateUtil.RevertToEnglish();
                break;
            }
            case 1: // chinese
            {
                _translateUtil.RevertToEnglish();
                _translateUtil.SetLanguage(Translations.ChineseTranslate);
                _translateUtil.Translate();
                break;
            }
            case 2: // polish
            {
                _translateUtil.RevertToEnglish();
                _translateUtil.SetLanguage(Translations.PolishTranslation);
                _translateUtil.Translate();
                break;
            }
            case 3: // german
            {
                _translateUtil.RevertToEnglish();
                _translateUtil.SetLanguage(Translations.GermanTranslation);
                _translateUtil.Translate();
                break;
            }
            case 4: // french
            {
                _translateUtil.RevertToEnglish();
                _translateUtil.SetLanguage(Translations.FrenchTranslation);
                _translateUtil.Translate();
                break;
            }
            case 5: // serbian
            {
                _translateUtil.RevertToEnglish();
                _translateUtil.SetLanguage(Translations.SerbianTranslation);
                _translateUtil.Translate();
                break;
            }
            case 6: // spanish
            {
                _translateUtil.RevertToEnglish();
                _translateUtil.SetLanguage(Translations.SpanishTranslation);
                _translateUtil.Translate();
                break;
            }
        }
    }
}