using System.Windows;
using System.Windows.Controls;
using Forza_Mods_AIO.Resources;
using Forza_Mods_AIO.Tabs.AutoShowTab;
using MahApps.Metro.Controls;
using static System.Convert;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.SelfVehicleAddresses;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.Features.EncryptedValues;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

/// <summary>
///     Interaction logic for UnlocksPage.xaml
/// </summary>
public partial class UnlocksPage
{
    public static UnlocksPage Unlocks { get; private set; } = null!;
    public static readonly Detour SeasonalDetour = new(), SeriesDetour = new();
    public static readonly Detour SpinsDetour = new(), SkillPointsDetour = new();
    public static EncryptedEntry XpEntry { get; set; } = null!; 
    public static EncryptedEntry CrEntry { get; set; } = null!;
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

        Setup(sender, CreditsSwitch_OnToggled);
        
        if (CrEntry == null!)
        {
            CrEntry = new EncryptedEntry("Credits");
        }
        
        CrEntry.SetState(CreditsSwitch.IsOn);
        CrEntry.SetValue(ToInt32(CreditsNum.Value));
    }
    
    private void CreditsNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        if (CrEntry == null!)
        {
            CrEntry = new EncryptedEntry("Credits");
        }
        
        CrEntry.SetValue(ToInt32(e.NewValue));
    }
    
    private void XpSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        Setup(sender, XpSwitch_OnToggled);
        
        if (XpEntry == null!)
        {
            XpEntry = new EncryptedEntry("XP");
        }
        
        XpEntry.SetState(XpSwitch.IsOn);
        XpEntry.SetValue(ToInt32(XpNum.Value));
    }

    private void XpNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached)
        {
            return;
        }
        
        if (XpEntry == null!)
        {
            XpEntry = new EncryptedEntry("XP");
        }
        
        XpEntry.SetValue(ToInt32(e.NewValue));
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
            SkillPointsDetour.Clear();
            return;
        }

        if (SkillPointsToggle.IsOn)
        {
            SkillPointsNum_OnValueChanged(SkillPointsNum, new(SkillPointsNum.Value, SkillPointsNum.Value));
        }
        
        SkillPointsDetour.Toggle();
    }

    private bool _wasIdListCreated;

    private void Unlock_OnClick(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button)
        {
            return;
        }

        if (MessageBox.Show(
                "This feature wasnt tested at all, it should work in theory though. Do you want to continue?",
                "Warning", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
        {
            return;
        }
        
        if (!_wasIdListCreated)
        {
            const string makeIdList = "CREATE TABLE IF NOT EXISTS id_list (id INTEGER PRIMARY KEY); WITH RECURSIVE Counter(id) AS (SELECT 1 UNION SELECT id + 1 FROM Counter WHERE id < 5000) INSERT INTO id_list (id) SELECT id FROM Counter;";
            AutoshowVars.ExecSql(button, Unlock_OnClick, makeIdList);
            _wasIdListCreated = true;
        }

        var insert = $"INSERT INTO ContentOffersMapping (OfferId,ContentId,ContentType,IsPromo,IsAutoRedeem,Quantity) SELECT 3, id, {button.Tag}, 0, 1, 1 FROM id_list;";
        AutoshowVars.ExecSql(button, Unlock_OnClick,insert);
    }

}