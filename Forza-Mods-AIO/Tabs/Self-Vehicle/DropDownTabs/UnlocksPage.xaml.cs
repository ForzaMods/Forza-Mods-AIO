using System.Windows;
using Forza_Mods_AIO.Resources;
using MahApps.Metro.Controls;
using static System.Convert;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.SelfVehicleAddresses;
using static Forza_Mods_AIO.MainWindow;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

/// <summary>
///     Interaction logic for UnlocksPage.xaml
/// </summary>
public partial class UnlocksPage
{
    public static UnlocksPage Up;
    public static readonly Detour CrDetour = new(), XpDetour = new(), SeasonalDetour = new(), SeriesDetour = new();
    private const string CrDetourBytes = "48 8B 05 2E 00 00 00 89 84 24 80 00 00 00";
    private const string XpDetourFh4 = "41 54 F3 0F 2C C6 4C 8B 25 1E 00 00 00 4C 89 65 B8 41 5C";
    private const string XpDetourFh5 = "41 54 F3 0F 2C C6 4C 8B 25 1E 00 00 00 4C 89 65 B0 41 5C";
    private const string Seasonal = "53 48 8B 1D 0F 00 00 00 48 89 58 28 5B F3 0F 10 40 28";
    private const string Series = "F3 0F 10 35 0A 00 00 00 F3 0F 11 71 28";
    
    public UnlocksPage()
    {
        InitializeComponent();
        Up = this;
    }
    
    private void CreditsSwitch_OnToggled(object? sender, RoutedEventArgs e)
    {
        if (!CrDetour.Setup(sender, CreditsHookAddr, CrDetourBytes, 7, true, 34))
        {
            FailedHandler(CreditsSwitch, CreditsSwitch_OnToggled);
            CrDetour.Clear();
            MessageBox.Show("Failed");
            return;
        }
        
        CrDetour.Toggle();
    }
    
    private void CreditsNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        try
        {
            CrDetour.UpdateVariable(ToInt32(CreditsNum.Value));
        }
        catch { /* ignored */ }
    }
    
    private void XpSwitch_OnToggled(object? sender, RoutedEventArgs e)
    {
        var xpDetourBytes = Mw.Gvp.Name!.Contains('5') ? XpDetourFh4 : XpDetourFh5;
        if (!XpDetour.Setup(sender, XpAddr, xpDetourBytes, 7, true, 19))
        {
            FailedHandler(XpSwitch, XpSwitch_OnToggled);
            XpDetour.Clear();
            MessageBox.Show("Failed.");
            return;
        }

        XpNum.IsEnabled = true;

        if (XpDetour.IsHooked)
        {
            Mw.M.WriteArrayMemory(XpAmountAddr, new byte[] { 0xB9, 0x01, 0x00, 0x00, 0x00, 0x90 });
        }
        else
        {
            Mw.M.WriteArrayMemory(XpAmountAddr, Mw.Gvp.Name.Contains('5')
                    ? new byte[] { 0x8B, 0x89, 0xB8, 0x00, 0x00, 0x00 }
                    : new byte[] { 0x8B, 0x89, 0xC0, 0x00, 0x00, 0x00 });
        }
        XpDetour.Toggle();
    }

    private void XpNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        try
        {
            XpDetour.UpdateVariable(ToInt32(XpNum.Value));
        }
        catch { /* ignored */ }
    }

    private void HornUnlockerSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("This isnt implemented yet");
    }

    private void EmoteUnlockerSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("This isnt implemented yet");
    }

    private void QuickChatsUnlockerSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("This isnt implemented yet");
    }

    private void CosmeticUnlockerSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("This isnt implemented yet");
    }

    private void DiscoverRoadsSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("This isnt ported from AIO V1 yet");
    }

    private void SmashBoardsSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("This isnt implemented yet");
    }

    private void SeasonalToggle_OnToggled(object sender, RoutedEventArgs e)
    {
        if (SeasonalToggle.IsOn && Mw.Gvp.Name == "Forza Horizon 4")
        {
            FailedHandler(SeasonalToggle, SeriesToggle_OnToggled);
            MessageBox.Show("This feature was never ported to fh4");
            return;
        }
        
        if (!SeasonalDetour.Setup(sender, SeasonalAddr, Seasonal, 5, true))
        {
            FailedHandler(SeasonalToggle, SeriesToggle_OnToggled);
            SeasonalDetour.Clear();
            MessageBox.Show("Failed");
            return;
        }
        
        if (SeasonalToggle.IsOn)
        {
            SeasonalNum_OnValueChanged(SeasonalNum, new(SeasonalNum.Value, SeasonalNum.Value));
        }
        
        SeasonalDetour.Toggle();
    }

    private void SeriesToggle_OnToggled(object sender, RoutedEventArgs e)
    {
        if (SeriesToggle.IsOn && Mw.Gvp.Name == "Forza Horizon 4")
        {            
            FailedHandler(SeriesToggle, SeriesToggle_OnToggled);
            MessageBox.Show("This feature was never ported to fh4");
            return;
        }
        
        if (!SeriesDetour.Setup(sender, SeriesAddr, Series, 5, true))
        {
            FailedHandler(SeriesToggle, SeriesToggle_OnToggled);
            SeriesDetour.Clear();
            MessageBox.Show("Failed");
            return;
        }

        if (SeriesToggle.IsOn)
        {
            SeriesNum_OnValueChanged(SeasonalNum, new(SeasonalNum.Value, SeasonalNum.Value));
        }
        SeriesDetour.Toggle();
    }

    private void SeasonalNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        try
        {
            SeasonalDetour.UpdateVariable(ToSingle(SeasonalNum.Value));
        }
        catch
        {
            // ignored
        }
    }

    private void SeriesNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        try
        {
            SeriesDetour.UpdateVariable(ToInt32(SeriesNum.Value));
        }
        catch
        {
            // ignored
        }
    }

    private void FailedHandler(ToggleSwitch @switch, RoutedEventHandler action)
    {
        @switch.Toggled -= action;
        @switch.IsOn = false;
        @switch.Toggled += action;
    }
}