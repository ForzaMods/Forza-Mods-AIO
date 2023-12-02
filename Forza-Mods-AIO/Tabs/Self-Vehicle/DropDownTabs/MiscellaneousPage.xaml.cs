using System;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Forza_Mods_AIO.Resources;
using MahApps.Metro.Controls;
using static System.Convert;
using static System.Math;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.SelfVehicleAddresses;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

public partial class MiscellaneousPage
{
    public static MiscellaneousPage? MiscPage;
    public static readonly Detour Build1Detour = new(), Build2Detour = new(), ScaleDetour = new(), SellDetour = new();
    public static readonly Detour SkillTreeDetour = new(), CleanlinessDetour = new(), ScoreDetour = new();
    public static readonly Detour SkillCostDetour = new(), DriftDetour = new();
    private static readonly Detour UnbSkillDetour = new();
    public bool WasSkillDetoured;

    private double _mudValue, _dirtValue;
    private bool _mudToggled, _dirtToggled;
    
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
    private const string CleanlinessFh4 = "53 80 3D 3E 00 00 00 01 75 0E 48 8B 1D 2C 00 00 00 48 89 98 7C 8C 00 00 80 3D 26 00 00 00 01 75 0E 48 8B 1D 19 00 00 00 48 89 98 80 8C 00 00 5B F3 0F 10 88 80 8C 00 00";
    private const string CleanlinessFh5 = "53 80 3D 3E 00 00 00 01 75 0E 48 8B 1D 2C 00 00 00 48 89 98 04 8A 00 00 80 3D 26 00 00 00 01 75 0E 48 8B 1D 19 00 00 00 48 89 98 08 8A 00 00 5B F3 0F 10 88 80 8C 00 00";
    private const string ScoreFh4 = "8B 78 04 0F AF 3D 08 00 00 00 48 85 DB";
    private const string ScoreFh5 = "0F AF 3D 05 00 00 00";
    private const string Drift = "80 3D 4B 00 00 00 01 75 20 53 48 8B 1D 39 00 00 00 48 89 9F D8 00 00 00 48 8B 1D 2F 00 00 00 48 89 9F DC 00 00 00 5B EB 14 C7 87 D8 00 00 00 00 00 34 42 C7 87 DC 00 00 00 00 00 5C 42 F3 0F 10 9F D8 00 00 00";
    
    public MiscellaneousPage()
    {
        InitializeComponent();
        MiscPage = this;
    }

    private void RemoveBuildCapSwitch_OnToggled(object? sender, RoutedEventArgs e)
    {
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

    private async void Skill_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!WasSkillDetoured)
        {
            GetSkillAddresses(sender);
            WasSkillDetoured = true;
        }

        Mw.M.WriteMemory(WorldCollisionThreshold, SkillToggle.IsOn ? 9999999999f : 12f);
        Mw.M.WriteMemory(CarCollisionThreshold,SkillToggle.IsOn ? 9999999999f : 12f);
        Mw.M.WriteMemory(SmashableCollisionTolerance,SkillToggle.IsOn ? 9999999999f : 22f);
    }

    private void GetSkillAddresses(object sender)
    {
        var bytes = Mw.Gvp.Name.Contains('4') ? SkillDetourFh4 : SkillDetourFh5;
        var replace = Mw.Gvp.Name.Contains('4') ? 6 : 5;
        
        if (!UnbSkillDetour.Setup(SkillToggle, UnbSkillHook, bytes, replace, true, 0, true))
        {
            FailedHandler((ToggleSwitch)sender, Skill_OnToggled);
            UnbSkillDetour.Clear();
            MessageBox.Show("Failed");
            return;
        }

        Task.Run(() =>
        {
            while ((WorldCollisionThreshold = UnbSkillDetour.ReadVariable<UIntPtr>()) == 0)
            {
                Task.Delay(1).Wait();
            }

            WorldCollisionThreshold += 0x2C;
        
            CarCollisionThreshold = WorldCollisionThreshold + 4;
            SmashableCollisionTolerance = WorldCollisionThreshold + 8;
        
            UnbSkillDetour.Destroy();
            UnbSkillDetour.Clear();
        });
    }

    private void SellFactorSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        var sell = Mw.Gvp.Name == "Forza Horizon 5" ? SellFh5 : SellFh4;
        var count = Mw.Gvp.Name == "Forza Horizon 5" ? 7 : 6;
        
        if (!SellDetour.Setup(SellFactorSwitch, SellFactorAddr, sell, count, true, 0, true))
        {
            FailedHandler((ToggleSwitch)sender, SellFactorSwitch_OnToggled);
            SellDetour.Clear();
            MessageBox.Show("Failed");
            return;
        }

        SellDetour.UpdateVariable(ToSingle(SellFactorNum.Value));
        SellDetour.Toggle();
    }

    private void SellFactorNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (SellFactorSwitch == null)
        {
            return;
        }
        
        if (Mw.Gvp.Name!.Contains('5'))
        {
            SellDetour.UpdateVariable(ToInt32(e.NewValue));
            return;
        }
            
        SellDetour.UpdateVariable(ToSingle(e.NewValue));
    }

    private void ScaleSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
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
        if (ScaleSwitch == null)
        {
            return;
        }
        
        ScaleDetour.UpdateVariable(ToSingle(e.NewValue));
    }
    
    private void SkillTreeEditToggle_OnToggled(object sender, RoutedEventArgs e)
    {
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

    private void FailedHandler(ToggleSwitch? @switch, RoutedEventHandler action)
    {
        @switch!.Toggled -= action;
        @switch.IsOn = false;
        @switch.Toggled += action;
    }

    private void SkillTreeNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (SkillTreeEditToggle == null)
        {
            return;
        }
        
        SkillTreeDetour.UpdateVariable(ToSingle(SkillTreeNum.Value));
    }

    private void CleanlinessNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        var selected = ((ComboBoxItem)CleanlinessComboBox.SelectedItem).Content.ToString();
        if (selected == "Dirt")
        {
            _dirtValue = ToDouble(e.NewValue);
        }
        else
        {
            _mudValue = ToDouble(e.NewValue);
        }

        if (!CleanlinessDetour.IsSetup)
        {
            return;
        }

        if (selected == "Dirt")
        {
            CleanlinessDetour.UpdateVariable(ToSingle(e.NewValue));
        }
        else
        {
            CleanlinessDetour.UpdateVariable(ToSingle(e.NewValue), 4);
        }
    }

    private void CleanlinessSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        var cleanlinessBytes = Mw.Gvp.Name!.Contains('5') ? CleanlinessFh5 : CleanlinessFh4;
        
        if (!CleanlinessDetour.Setup(sender, CleanlinessAddr, cleanlinessBytes, 8, true))
        {
            CleanlinessSwitch.Toggled -= CleanlinessSwitch_OnToggled;
            CleanlinessSwitch.IsOn = false;
            CleanlinessSwitch.Toggled -= CleanlinessSwitch_OnToggled;
            MessageBox.Show("Failed");
            return;
        }
        
        var selected = ((ComboBoxItem)CleanlinessComboBox.SelectedItem).Content.ToString();
        if (selected == "Dirt")
        {
            CleanlinessDetour.UpdateVariable(ToSingle(CleanlinessNum.Value));
            CleanlinessDetour.UpdateVariable(CleanlinessSwitch.IsOn ? (byte)1 : (byte)0, 9);
            _dirtToggled = CleanlinessDetour.ReadVariable<byte>(9) == 1;
        }
        else
        {
            CleanlinessDetour.UpdateVariable(ToSingle(CleanlinessNum.Value), 4);
            CleanlinessDetour.UpdateVariable(CleanlinessSwitch.IsOn ? (byte)1 : (byte)0, 8);
            _mudToggled = CleanlinessDetour.ReadVariable<byte>(8) == 1;
        }
    }

    private void CleanlinessSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (CleanlinessSwitch == null)
        {
            return;
        }
        
        CleanlinessNum.Value = Round(e.NewValue, 5);
    }

    private void CleanlinessComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (CleanlinessSwitch == null)
        {
            return;
        }

        var selected = ((ComboBoxItem)CleanlinessComboBox.SelectedItem).Content.ToString();
        CleanlinessSwitch.Content = $"{selected} Level";

        switch (selected)
        {
            case "Dirt":
            {
                CleanlinessNum.Value = _dirtValue;
                CleanlinessSlider.Value = _dirtValue;
                CleanlinessSwitch.IsOn = _dirtToggled;
                break;
            }
            
            case "Mud":
            {
                CleanlinessNum.Value = _mudValue;
                CleanlinessSlider.Value = _mudValue;                
                CleanlinessSwitch.IsOn = _mudToggled;
                break;
            }
        }
    }

    private void ScoreSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (ScoreToggle == null)
        {
            return;
        }
        
        ScoreSlider.Value = Round(ScoreSlider.Value);
        ScoreNum.Value = ScoreSlider.Value;
    }

    private void ScoreToggle_OnToggled(object sender, RoutedEventArgs e)
    {
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
        
        ScoreDetour.UpdateVariable(ToInt32(e.NewValue));
    }

    private void SkillCostToggle_OnToggled(object sender, RoutedEventArgs e)
    {
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
        if (SkillCostToggle == null)
        {
            return;
        }
        
        SkillCostDetour.UpdateVariable(ToInt32(e.NewValue));
    }

    private void DriftNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
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
}