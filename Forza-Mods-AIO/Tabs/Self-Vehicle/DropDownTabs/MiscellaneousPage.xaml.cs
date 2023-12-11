using System;
using System.Threading.Tasks;
using System.Windows;
using Forza_Mods_AIO.Resources;
using MahApps.Metro.Controls;
using static System.Convert;
using static System.Math;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.SelfVehicleAddresses;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

public partial class MiscellaneousPage
{
    public static MiscellaneousPage MiscPage { get; private set; } = null!;
    public static readonly Detour Build1Detour = new(), Build2Detour = new(), ScaleDetour = new(), SellDetour = new();
    public static readonly Detour SkillTreeDetour = new(), ScoreDetour = new();
    public static readonly Detour SkillCostDetour = new(), DriftDetour = new(), TimeScaleDetour = new();
    private static readonly Detour UnbSkillDetour = new();
    public bool WasSkillDetoured;
    
    private const string Build1Fh4 = "F3 0F 11 B3 DC 03 00 00 C7 83 DC 03 00 00 00 00 00 00";
    private const string Build1Fh5 = "F3 0F 11 83 4C 04 00 00 C7 83 4C 04 00 00 00 00 00 00";
    private const string Build2Fh4 = "F3 0F 11 43 44 C7 43 44 00 00 00 00";
    private const string Build2Fh5 = "F3 0F 11 43 30 C7 43 30 00 00 00 00";
    private const string SkillDetourFh4 = "48 89 1D 05 00 00 00";
    private const string SkillDetourFh5 = "48 89 0D 05 00 00 00";
    private const string ScaleFh5 = "F3 0F 10 35 05 00 00 00";
    private const string ScaleFh4 = "F3 0F 59 05 05 00 00 00";
    private const string SellFh5 = "44 8B 35 05 00 00 00";
    private const string SellFh4 = "8B 3D 05 00 00 00";
    private const string SkillTree = "50 48 8B 05 0F 00 00 00 48 89 43 48 58 F3 0F 10 73 48";
    private const string SkillCost = "8B 05 05 00 00 00";
    private const string ScoreFh4 = "8B 78 04 0F AF 3D 08 00 00 00 48 85 DB";
    private const string ScoreFh5 = "0F AF 3D 05 00 00 00";
    private const string Drift = "80 3D 4B 00 00 00 01 75 20 53 48 8B 1D 39 00 00 00 48 89 9F D8 00 00 00 48 8B 1D 2F 00 00 00 48 89 9F DC 00 00 00 5B EB 14 C7 87 D8 00 00 00 00 00 34 42 C7 87 DC 00 00 00 00 00 5C 42 F3 0F 10 9F D8 00 00 00";
    private const string TimeScale = "F3 0F 59 3D 11 00 00 00 F3 0F 5C C7 F3 0F 11 87 0C 04 00 00";
        
    public MiscellaneousPage()
    {
        InitializeComponent();
        MiscPage = this;
    }

    private void RemoveBuildCapSwitch_OnToggled(object? sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        var fh5 = Mw.Gvp.Name!.Contains('5');
        var build1 = fh5 ? Build1Fh5 : Build1Fh4;
        var build2 = fh5 ? Build2Fh5 : Build2Fh4;
        if (!Build1Detour.Setup(sender, BuildAddr1, build1, 8) || !Build2Detour.Setup(sender, BuildAddr2, build2, 8))
        {
            FailedHandler(sender as ToggleSwitch, RemoveBuildCapSwitch_OnToggled);
            Build1Detour.Clear();
            Build2Detour.Clear();
            MessageBox.Show("Failed");
            return;
        }
        
        Build1Detour.Toggle();
        Build2Detour.Toggle();
    }

    private void Skill_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        if (!WasSkillDetoured)
        {
            GetSkillAddresses(sender);
            WasSkillDetoured = true;
            return;
        }

        Mw.M.WriteMemory(WorldCollisionThreshold, SkillToggle.IsOn ? 9999999999f : 12f);
        Mw.M.WriteMemory(CarCollisionThreshold,SkillToggle.IsOn ? 9999999999f : 12f);
        Mw.M.WriteMemory(SmashableCollisionTolerance,SkillToggle.IsOn ? 9999999999f : 22f);
    }

    private async void GetSkillAddresses(object sender)
    {
        if (!Mw.Attached)
        {
            return;
        }

        var bytes = Mw.Gvp.Name.Contains('4') ? SkillDetourFh4 : SkillDetourFh5;
        var replace = Mw.Gvp.Name.Contains('4') ? 6 : 5;
        
        if (!UnbSkillDetour.Setup(SkillToggle, UnbSkillHook, bytes, replace, true, 0, true))
        {
            FailedHandler((ToggleSwitch)sender, Skill_OnToggled);
            UnbSkillDetour.Clear();
            MessageBox.Show("Failed");
            return;
        }

        SkillToggle.IsEnabled = false;

        await Task.Run(() =>
        {
            while ((WorldCollisionThreshold = UnbSkillDetour.ReadVariable<UIntPtr>()) == 0)
            {
                Task.Delay(25).Wait();
            }

            WorldCollisionThreshold += 0x2C;
        
            CarCollisionThreshold = WorldCollisionThreshold + 4;
            SmashableCollisionTolerance = WorldCollisionThreshold + 8;
        
            Mw.M.WriteMemory(WorldCollisionThreshold, 9999999999f);
            Mw.M.WriteMemory(CarCollisionThreshold,9999999999f);
            Mw.M.WriteMemory(SmashableCollisionTolerance,9999999999f);
            
            UnbSkillDetour.Destroy();
            UnbSkillDetour.Clear();
        });
        
        SkillToggle.IsEnabled = true;
    }

    private void SellFactorSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        var sell = Mw.Gvp.Name == "Forza Horizon 5" ? SellFh5 : SellFh4;
        var count = Mw.Gvp.Name == "Forza Horizon 5" ? 7 : 6;
        
        if (!SellDetour.Setup(SellFactorSwitch, SellFactorAddr, sell, count, true, 0, true))
        {
            FailedHandler((ToggleSwitch)sender, SellFactorSwitch_OnToggled);
            SellDetour.Clear();
            MessageBox.Show("Failed");
            return;
        }

        if (Mw.Gvp.Name.Contains('5'))
        {
            SellDetour.UpdateVariable(ToInt32(SellFactorNum.Value) * 50);
        }
        else
        {
            SellDetour.UpdateVariable(ToSingle(SellFactorNum.Value) * 50);
        }
        
        SellDetour.Toggle();
    }

    private void SellFactorNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        if (Mw.Gvp.Name.Contains('5'))
        {
            SellDetour.UpdateVariable(ToInt32(SellFactorNum.Value) * 50);
            return;
        }
        
        SellDetour.UpdateVariable(ToSingle(SellFactorNum.Value) * 50);
    }

    private void ScaleSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        var scale = Mw.Gvp.Name == "Forza Horizon 5" ? ScaleFh5 : ScaleFh4;
        
        if (!ScaleDetour.Setup(ScaleSwitch, ScaleAddr, scale, 5, true, 0, true))
        {
            FailedHandler((ToggleSwitch)sender, ScaleSwitch_OnToggled);
            ScaleDetour.Clear();
            MessageBox.Show("Failed");
            return;
        }

        ScaleDetour.UpdateVariable(ToSingle(ScaleNum.Value));
        ScaleDetour.Toggle();
    }

    private void ScaleNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached)
        {
            return;
        }
        
        ScaleDetour.UpdateVariable(ToSingle(e.NewValue));
    }
    
    private void SkillTreeEditToggle_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        if (Mw.Gvp.Name == "Forza Horizon 4" && SkillTreeEditToggle.IsOn)
        {
            FailedHandler((ToggleSwitch)sender, SkillTreeEditToggle_OnToggled);
            MessageBox.Show("This feature was never ported to FH4");
            return;
        }
        
        if (!SkillTreeDetour.Setup(sender, SkillTreeAddr, SkillTree, 5, true))
        {
            FailedHandler((ToggleSwitch)sender, SkillTreeEditToggle_OnToggled);
            SkillTreeDetour.Clear();
            MessageBox.Show("Failed");
            return;
        }
        
        SkillTreeDetour.UpdateVariable(ToSingle(SkillTreeNum.Value));
        SkillTreeDetour.Toggle();
    }

    private static void FailedHandler(ToggleSwitch? @switch, RoutedEventHandler action)
    {
        @switch!.Toggled -= action;
        @switch.IsOn = false;
        @switch.Toggled += action;
    }

    private void SkillTreeNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached)
        {
            return;
        }
        
        SkillTreeDetour.UpdateVariable(ToSingle(SkillTreeNum.Value));
    }


    private void ScoreSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        ScoreSlider.Value = Round(ScoreSlider.Value);
        ScoreNum.Value = ScoreSlider.Value;
    }

    private void ScoreToggle_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        var fh4 = Mw.Gvp.Name == "Forza Horizon 4";
        var bytes = fh4 ? ScoreFh4 : ScoreFh5;
        var replace = fh4 ? 6 : 7;
        var save = !fh4;
        
        if (!ScoreDetour.Setup(sender, SkillScoreAddr, bytes, replace, true, 0, save))
        {
            FailedHandler((ToggleSwitch)sender, ScoreToggle_OnToggled);
            ScoreDetour.Clear();
            MessageBox.Show("Failed");
            return;
        }
        
        ScoreDetour.UpdateVariable(ToInt32(ScoreNum.Value));
        ScoreDetour.Toggle();
    }

    private void ScoreNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (ScoreToggle == null)
        {
            return;
        }
        
        ScoreSlider.Value = ToDouble(e.NewValue);

        if (!Mw.Attached)
        {
            return;
        }
        
        ScoreDetour.UpdateVariable(ToInt32(e.NewValue));
    }

    private void SkillCostToggle_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        if (Mw.Gvp.Name == "Forza Horizon 4" && SkillCostToggle.IsOn)
        {
            FailedHandler((ToggleSwitch)sender, SkillCostToggle_OnToggled);
            MessageBox.Show("This feature was never ported to FH4");
            return;
        }
        
        if (!SkillCostDetour.Setup(sender, SkillCostAddr, SkillCost, 7, true, 0, true))
        {
            FailedHandler((ToggleSwitch)sender, SkillCostToggle_OnToggled);
            SkillCostDetour.Clear();
            MessageBox.Show("Failed");
            return;
        }
        
        SkillCostDetour.UpdateVariable(ToInt32(SkillCostNum.Value));
        SkillCostDetour.Toggle();
    }

    private void SkillCostNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached)
        {
            return;
        }
        
        SkillCostDetour.UpdateVariable(ToInt32(e.NewValue));
    }

    private void DriftNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        switch (sender.GetType().GetProperty("Name").GetValue(sender))
        {
            case "DriftMinNum":
            {
                DriftDetour.UpdateVariable(ToSingle(45 * DriftMinNum.Value));
                break;
            }
            case "DriftMaxNum":
            {
                DriftDetour.UpdateVariable(ToSingle(55 * DriftMaxNum.Value), 4);
                break;
            }
            default:
            {
                throw new ArgumentOutOfRangeException(nameof(sender));
            }
        }
    }

    private void DriftToggle_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }
        
        if (Mw.Gvp.Name == "Forza Horizon 4" && DriftToggle.IsOn)
        {
            FailedHandler((ToggleSwitch)sender, DriftToggle_OnToggled);
            MessageBox.Show("This feature was never ported to FH4");
            return;
        }

        if (!DriftDetour.Setup(DriftToggle, DriftScoreAddr, Drift, 8, true))
        {
            FailedHandler((ToggleSwitch)sender, DriftToggle_OnToggled);
            DriftDetour.Clear();
            MessageBox.Show("Failed");
            return;
        }
        
        DriftDetour.UpdateVariable(ToSingle(45 * DriftMinNum.Value));
        DriftDetour.UpdateVariable(ToSingle(55 * DriftMaxNum.Value), 4);
        DriftDetour.UpdateVariable(DriftToggle.IsOn ? (byte)1 : (byte)0, 8);
        
    }

    private void TimeScaleSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }
        
        if (Mw.Gvp.Name == "Forza Horizon 4" && TimeScaleSwitch.IsOn)
        {
            FailedHandler((ToggleSwitch)sender, TimeScaleSwitch_OnToggled);
            MessageBox.Show("This feature was never ported to FH4");
            return;
        }

        if (!TimeScaleDetour.Setup(TimeScaleAddr, TimeScale, 12, true))
        {
            FailedHandler(sender as ToggleSwitch, TimeScaleSwitch_OnToggled);
            TimeScaleDetour.Clear();
            MessageBox.Show("Failed");
            return;
        }
        
        TimeScaleDetour.UpdateVariable(ToSingle(TimeScaleNum.Value));
        TimeScaleDetour.Toggle();
    }

    private void TimeScaleNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (TimeScaleSwitch == null || TimeScaleSlider == null)
        {
            return;
        }
        
        TimeScaleSlider.Value = ToDouble(e.NewValue);
        
        if (!Mw.Attached)
        {
            return;
        }

        TimeScaleDetour.UpdateVariable(ToSingle(e.NewValue));
    }

    private void TimeScaleSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (TimeScaleSwitch == null)
        {
            return;
        }

        TimeScaleNum.Value = ToDouble(Round(e.NewValue, 4));
    }
}