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
    private const string Series = "4C 39 C0 74 13 80 78 10 1D 75 02 EB 04 8B 40 14 C3 8B 05 09 00 00 00 C3 31 C0 C3";
    private const string Spins = "31 D2 50 80 BC 24 10 01 00 00 01 75 12 80 3D 35 00 00 00 01 75 09 48 8B 05 2D 00 00 00 EB 1A 80 BC 24 10 01 00 00 02 75 17 80 3D 1E 00 00 00 01 75 0E 48 8B 05 16 00 00 00 48 FF C0 48 89 47 08 58 8B 5F 08";
    private const string SkillPoints = "31 ED 8B 1D 0B 00 00 00 29 EB 31 D2 85 DB";
    
    public UnlocksPage()
    {
        InitializeComponent();
        Unlocks = this;
    }
    
    private void CreditsSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        const string main = "89 84 24 80 00 00 00";
        
        if (Mw.Gvp.Name.Contains('4') && !CrDetour.Setup(sender, CreditsHookAddr, main, CrDetourBytesFh4, 7, true))
        {
            Detour.FailedHandler(sender, CreditsSwitch_OnToggled);
            CrDetour.Clear();
            return;
        }

        const string compare = "FF 43 08 48 8D 44 24 20";
        if (Mw.Gvp.Name.Contains('5') && 
            !CrCompareDetour.Setup(sender,CreditsCompareAddr, compare, CrCompareBytes, 8, true, 0, true))
        {
            Detour.FailedHandler(sender, CreditsSwitch_OnToggled);
            CrCompareDetour.Clear();
            return;
        }

        if (Mw.Gvp.Name.Contains('5') && !CrDetour.Setup(sender, CreditsHookAddr,main, CrDetourBytesFh5, 7, true))
        {
            Detour.FailedHandler(sender, CreditsSwitch_OnToggled);
            CrDetour.Clear();
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
        CrCompareDetour.Toggle();
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
        const string fh5 = "F3 0F 2C C6 89 45 B0";
        
        if (!XpDetour.Setup(sender, XpAddr, fh5, xpDetourBytes, 7, true, 19))
        {
            Detour.FailedHandler(XpSwitch, XpSwitch_OnToggled);
            XpDetour.Clear();
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
                ? new byte[] { 0x8B, 0x89, 0x88, 0x00, 0x00, 0x00 }
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
        
        switch (SeasonalToggle.IsOn)
        {
            case true when Mw.Gvp.Name == "Forza Horizon 4":
            {
                Detour.FailedHandler(SeasonalToggle, SeasonalToggle_OnToggled, true);
                return;
            }
            case true:
            {
                MessageBox.Show("In order to get target points you need to complete any series challenge");
                break;
            }
        }

        const string orig = "F3 0F11 71 28";
        if (!SeasonalDetour.Setup(sender, SeasonalAddr, orig, Seasonal, 5, true))
        {
            Detour.FailedHandler(SeasonalToggle, SeasonalToggle_OnToggled);
            SeasonalDetour.Clear();
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
            Detour.FailedHandler(SeriesToggle, SeriesToggle_OnToggled, true);
            return;
        }

        const string orig = "49 3B C0 74 04";
        if (!SeriesDetour.Setup(sender, SeriesAddr,orig, Series, 5, true))
        {
            Detour.FailedHandler(SeriesToggle, SeriesToggle_OnToggled);
            SeriesDetour.Clear();
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

        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }
        
        if (toggleSwitch.IsOn && Mw.Gvp.Name == "Forza Horizon 4")
        {            
            Detour.FailedHandler(toggleSwitch, SpinsToggle_OnToggled, true);
            return;
        }
        
        const string orig = "33 D2 8B 5F 08";
        if (!SpinsDetour.Setup(toggleSwitch, SpinsAddr, orig, Spins, 5, useVarAddress: true))
        {
            Detour.FailedHandler(toggleSwitch, SpinsToggle_OnToggled);
            SpinsDetour.Clear();
            return;
        }

        if (toggleSwitch.IsOn)
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


        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        if (toggleSwitch.IsOn && Mw.Gvp.Name == "Forza Horizon 4")
        {            
            Detour.FailedHandler(toggleSwitch, SpinsToggle_OnToggled, true);
            return;
        }

        const string orig = "2B DD 33 D2 85 DB";
        if (!SkillPointsDetour.Setup(toggleSwitch, SkillPointsAddr, orig, SkillPoints, 6, useVarAddress: true))
        {
            Detour.FailedHandler(toggleSwitch, SpinsToggle_OnToggled);
            SkillPointsDetour.Clear();
            return;
        }

        if (SkillPointsToggle.IsOn)
        {
            SkillPointsNum_OnValueChanged(SkillPointsNum, new(SkillPointsNum.Value, SkillPointsNum.Value));
        }
        
        SkillPointsDetour.Toggle();
    }
}