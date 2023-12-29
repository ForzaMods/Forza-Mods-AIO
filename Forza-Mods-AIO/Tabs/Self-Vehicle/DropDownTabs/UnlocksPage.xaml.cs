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
    public static UnlocksPage Unlocks { get; private set; } = null!;
    public static readonly Detour CrDetour = new(), XpDetour = new(), SeasonalDetour = new(), SeriesDetour = new();
    public static readonly Detour CrCompareDetour = new(), SpinsDetour = new(), SkillPointsDetour = new();
    private const string CrDetourBytesFh4 = "48 8B 05 2E 00 00 00 89 84 24 80 00 00 00";
    private const string CrDetourBytesFh5 = "56 51 48 8B 35 29 00 00 00 48 8B 0E 48 83 F9 00 74 12 48 8D 49 70 39 01 75 0A 48 8B 0D 19 00 00 00 48 8B C1 5E 59 89 84 24 80 00 00 00";
    private const string CrCompareBytes = "48 89 1D 05 00 00 00";
    private const string XpDetourFh4 = "41 54 F3 0F 2C C6 4C 8B 25 1E 00 00 00 4C 89 65 B8 41 5C";
    private const string XpDetourFh5 = "41 54 F3 0F 2C C6 4C 8B 25 1E 00 00 00 4C 89 65 B0 41 5C";
    private const string Seasonal = "F3 0F 10 35 0A 00 00 00 F3 0F 11 71 28";
    private const string Series = "4C 39 C0 74 13 80 78 10 1C 75 02 EB 04 8B 40 14 C3 8B 05 09 00 00 00 C3 31 C0 C3";
    private const string Spins = "31 D2 50 80 BC 24 10 01 00 00 01 75 12 80 3D 35 00 00 00 01 75 09 48 8B 05 2D 00 00 00 EB 1A 80 BC 24 10 01 00 00 02 75 17 80 3D 1E 00 00 00 01 75 0E 48 8B 05 16 00 00 00 48 FF C0 48 89 47 08 58 8B 5F 08";
    private const string SkillPoints = "31 ED 8B 1D 0B 00 00 00 29 EB 31 D2 85 DB";
    
    public UnlocksPage()
    {
        InitializeComponent();
        Unlocks = this;
    }
    
    private void CreditsSwitch_OnToggled(object? sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        if (Mw.Gvp.Name.Contains('4') && !CrDetour.Setup(sender, CreditsHookAddr, CrDetourBytesFh4, 7, true, 34))
        {
            FailedHandler(CreditsSwitch, CreditsSwitch_OnToggled);
            CrDetour.Clear();
            MessageBox.Show("Failed");
            return;
        }
        if (Mw.Gvp.Name.Contains('5') && !CrCompareDetour.Setup(sender,CreditsCompareAddr,CrCompareBytes, 8, true, 0, true))
        {
            FailedHandler(CreditsSwitch, CreditsSwitch_OnToggled);
            CrCompareDetour.Clear();
            MessageBox.Show("Failed");
            return;
        }

        if (Mw.Gvp.Name.Contains('5') && !CrDetour.Setup(sender, CreditsHookAddr, CrDetourBytesFh5, 7, true))
        {
            FailedHandler(CreditsSwitch, CreditsSwitch_OnToggled);
            CrDetour.Clear();
            MessageBox.Show("Failed");
            return;
        }

        if (Mw.Gvp.Name.Contains('5'))
        {
            CrDetour.UpdateVariable(CrCompareDetour.VariableAddress);
            CrDetour.UpdateVariable(ToInt32(CreditsNum.Value), 8);
        }
        else
        {
            CrDetour.UpdateVariable(ToInt32(CreditsNum.Value));
        }
        
        CrDetour.Toggle();
    }
    
    
    private void CreditsNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        if (Mw.Gvp.Name.Contains('5'))
        {
            CrDetour.UpdateVariable(ToInt32(CreditsNum.Value), 8);
        }
        else
        {
            CrDetour.UpdateVariable(ToInt32(CreditsNum.Value));
        }
    }
    
    private void XpSwitch_OnToggled(object? sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }
        
        var xpDetourBytes = Mw.Gvp.Name!.Contains('5') ? XpDetourFh5 : XpDetourFh4;
        if (!XpDetour.Setup(sender, XpAddr, xpDetourBytes, 7, true, 19))
        {
            FailedHandler(XpSwitch, XpSwitch_OnToggled);
            XpDetour.Clear();
            MessageBox.Show("Failed.");
            return;
        }

        XpDetour.Toggle();

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
    }

    private void XpNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached)
        {
            return;
        }
        
        XpDetour.UpdateVariable(ToInt32(XpNum.Value));
    }

    private void DiscoverRoadsSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        MessageBox.Show("This isnt ported from AIO V1 yet");
    }

    private void SeasonalToggle_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }
        
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
        
        SeasonalDetour.UpdateVariable(ToSingle(SeasonalNum.Value));
        SeasonalDetour.Toggle();
    }

    private void SeriesToggle_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }
        
        if (SeriesToggle.IsOn && Mw.Gvp.Name == "Forza Horizon 4")
        {            
            FailedHandler(SeriesToggle, SeriesToggle_OnToggled);
            MessageBox.Show("This feature was never ported to fh4");
            return;
        }

        MessageBox.Show("In order to get target points you need to complete any series challenge");
        
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
        if (!Mw.Attached)
        {
            return;
        }
        
        SeasonalDetour.UpdateVariable(ToSingle(SeasonalNum.Value));
    }

    private void SeriesNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        SeriesDetour.UpdateVariable(ToInt32(SeriesNum.Value));
    }

    private static void FailedHandler(ToggleSwitch @switch, RoutedEventHandler action)
    {
        @switch.Toggled -= action;
        @switch.IsOn = false;
        @switch.Toggled += action;
    }

    private void SpinsNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached || SuperSpinsNum == null || !SpinsDetour.IsSetup)
        {
            return;
        }

        
        SpinsDetour.UpdateVariable(ToInt32(NormalSpinsNum.Value), 1);
        SpinsDetour.UpdateVariable(ToInt32(SuperSpinsNum.Value), 6);
    }
    
    private void SpinsToggle_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached || SuperSpinsSwitch == null)
        {
            return;
        }

        if (((ToggleSwitch)sender).IsOn && Mw.Gvp.Name == "Forza Horizon 4")
        {            
            FailedHandler((ToggleSwitch)sender, SpinsToggle_OnToggled);
            MessageBox.Show("This feature was never ported to FH4");
            return;
        }
        
        if (!SpinsDetour.Setup((ToggleSwitch)sender, SpinsAddr, Spins, 5, true))
        {
            FailedHandler((ToggleSwitch)sender, SpinsToggle_OnToggled);
            SpinsDetour.Clear();
            MessageBox.Show("Failed");
            return;
        }

        if (((ToggleSwitch)sender).IsOn)
        {
            SpinsNum_OnValueChanged(NormalSpinsNum, new(NormalSpinsNum.Value, NormalSpinsNum.Value));
        }
        
        SpinsDetour.UpdateVariable(NormalSpinsSwitch.IsOn ? (byte)1 : (byte)0);
        SpinsDetour.UpdateVariable(SuperSpinsSwitch.IsOn ? (byte)1 :(byte) 0, 5);
    }

    private void SkillPointsNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached || SkillPointsNum == null)
        {
            return;
        }
        
        SkillPointsDetour.UpdateVariable(ToInt32(SkillPointsNum.Value));
    }

    private void SkillPointsToggle_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached || SkillPointsToggle == null)
        {
            return;
        }

        
        if (((ToggleSwitch)sender).IsOn && Mw.Gvp.Name == "Forza Horizon 4")
        {            
            FailedHandler((ToggleSwitch)sender, SpinsToggle_OnToggled);
            MessageBox.Show("This feature was never ported to FH4");
            return;
        }
        
        if (!SkillPointsDetour.Setup((ToggleSwitch)sender, SkillPointsAddr, SkillPoints, 6, true))
        {
            FailedHandler((ToggleSwitch)sender, SpinsToggle_OnToggled);
            SkillPointsDetour.Clear();
            MessageBox.Show("Failed");
            return;
        }

        if (SkillPointsToggle.IsOn)
        {
            SkillPointsNum_OnValueChanged(SkillPointsNum, new(SkillPointsNum.Value, SkillPointsNum.Value));
        }
        
        SkillPointsDetour.Toggle();
    }
}