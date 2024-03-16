using System.Text;
using System.Windows;
using System.Windows.Controls;
using Forza_Mods_AIO.Cheats.ForzaHorizon5;
using Forza_Mods_AIO.ViewModels.SubPages.SelfVehicle;
using MahApps.Metro.Controls;
using static Forza_Mods_AIO.Resources.Memory;

namespace Forza_Mods_AIO.Views.SubPages.SelfVehicle;

public partial class Misc
{
    public Misc()
    {
        ViewModel = new MiscViewModel();
        DataContext = this;
        
        InitializeComponent();
    }

    public MiscViewModel ViewModel { get; }
    private static MiscCheats MiscCheatsFh5 => Forza_Mods_AIO.Resources.Cheats.GetClass<MiscCheats>();
    private static CarCheats CarCheatsFh5 => Forza_Mods_AIO.Resources.Cheats.GetClass<CarCheats>();
    
    private async void NameSpooferSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        ViewModel.SpooferUiElementsEnabled = false;
        if (MiscCheatsFh5.NameDetourAddress == 0)
        {
            await MiscCheatsFh5.CheatName();
        }
        ViewModel.SpooferUiElementsEnabled = true;

        if (MiscCheatsFh5.NameDetourAddress == 0) return;
        GetInstance().WriteMemory(MiscCheatsFh5.NameDetourAddress + 0x72, toggleSwitch.IsOn ? (byte)1 : (byte)0);
        if (string.IsNullOrEmpty(NameBox.Text)) return;
        var name = Encoding.Unicode.GetBytes(NameBox.Text);
        var newName = new byte[64];
        Array.Copy(name, newName, Math.Min(name.Length, newName.Length));
        GetInstance().WriteArrayMemory(MiscCheatsFh5.NameDetourAddress + 0x73, newName);
    }

    private void NameBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (MiscCheatsFh5.NameDetourAddress == 0) return;
        if (string.IsNullOrEmpty(NameBox.Text)) return;
        var name = Encoding.Unicode.GetBytes(NameBox.Text);
        var newName = new byte[64];
        Array.Copy(name, newName, Math.Min(name.Length, newName.Length));
        GetInstance().WriteArrayMemory(MiscCheatsFh5.NameDetourAddress + 0x73, newName);
    }

    private async void TpSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        toggleSwitch.IsEnabled = false;
        if (CarCheatsFh5.WaypointDetourAddress == 0)
        {
            await CarCheatsFh5.CheatWaypoint();
        }
        toggleSwitch.IsEnabled = true;

        if (CarCheatsFh5.WaypointDetourAddress == 0) return;
        GetInstance().WriteMemory(CarCheatsFh5.WaypointDetourAddress + 0x32, toggleSwitch.IsOn ? (byte)1 : (byte)0);
    }

    private void MainComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is not ComboBox comboBox || MainToggleSwitch == null || MainValueBox == null)
        {
            return;
        }

        MainToggleSwitch.Toggled -= MainToggleSwitch_OnToggled;
        MainValueBox.ValueChanged -= MainToggleSwitch_OnToggled;
        
        switch (comboBox.SelectedIndex)
        {
            case 0:
            {        
                MainValueBox.Value = ViewModel.SpinPrizeScaleValue;
                MainToggleSwitch.IsOn = ViewModel.SpinPrizeScaleEnabled;
                break;
            }
            case 1:
            {            
                MainValueBox.Value = ViewModel.SpinSellFactorValue;
                MainToggleSwitch.IsOn = ViewModel.SpinSellFactorEnabled;
                break;
            }
            case 2:
            {              
                MainValueBox.Value = ViewModel.SkillScoreMultiplierValue;
                MainToggleSwitch.IsOn = ViewModel.SkillScoreMultiplierEnabled;
                break;
            }
            case 3:
            {           
                MainValueBox.Value = ViewModel.DriftScoreMultiplierValue;
                MainToggleSwitch.IsOn = ViewModel.DriftScoreMultiplierEnabled;
                break;
            }
            case 4:
            {              
                MainValueBox.Value = ViewModel.SkillTreeWideEditValue;
                MainToggleSwitch.IsOn = ViewModel.SkillTreeWideEditEnabled;
                break;
            }
            case 5:
            {             
                MainValueBox.Value = ViewModel.SkillTreeCostValue;
                MainToggleSwitch.IsOn = ViewModel.SkillTreeCostEnabled;
                break;
            }
            case 6:
            {            
                MainValueBox.Value = ViewModel.MissionTimeScaleValue;
                MainToggleSwitch.IsOn = ViewModel.MissionTimeScaleEnabled;
                break;
            }
            case 7:
            {           
                MainValueBox.Value = ViewModel.TrailblazerTimeScaleValue;
                MainToggleSwitch.IsOn = ViewModel.TrailblazerTimeScaleEnabled;
                break;
            }
        }

        MainValueBox.Minimum = comboBox.SelectedIndex switch
        { 
            0 or 1 or 2 or 3 or 4 or 6 or 7 => 0,
            5 => int.MinValue,
            _ => throw new IndexOutOfRangeException()
        };

        MainValueBox.Maximum = comboBox.SelectedIndex switch
        { 
            0 or 1 or 2 or 4 or 5 => int.MaxValue,
            3 => 10,
            6 or 7 => 1,
            _ => throw new IndexOutOfRangeException()
        };

        MainValueBox.Interval = comboBox.SelectedIndex switch
        { 
            0 or 1 or 2 or 3 or 4 or 5 => 1,
            6 or 7 => 0.1,
            _ => throw new IndexOutOfRangeException()
        };
        
        MainValueBox.ValueChanged -= MainToggleSwitch_OnToggled;
        MainToggleSwitch.Toggled += MainToggleSwitch_OnToggled;
    }

    private void MainValueBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        switch (MainComboBox.SelectedIndex)
        {
            case 0:
            {        
                ViewModel.SpinPrizeScaleValue = Convert.ToSingle(e.NewValue);
                if (MiscCheatsFh5.PrizeScaleDetourAddress == 0) return;
                GetInstance().WriteMemory(MiscCheatsFh5.PrizeScaleDetourAddress + 0x1C, ViewModel.SpinPrizeScaleValue);
                break;
            }
            case 1:
            {            
                ViewModel.SpinSellFactorValue = Convert.ToInt32(e.NewValue);
                if (MiscCheatsFh5.SellFactorDetourAddress == 0) return;
                GetInstance().WriteMemory(MiscCheatsFh5.SellFactorDetourAddress + 0x1D, ViewModel.SpinSellFactorValue);
                break;
            }
            case 2:
            {              
                ViewModel.SkillScoreMultiplierValue = Convert.ToInt32(e.NewValue);
                if (MiscCheatsFh5.SkillScoreMultiplierDetourAddress == 0) return;
                GetInstance().WriteMemory(MiscCheatsFh5.SkillScoreMultiplierDetourAddress + 0x1D, ViewModel.SkillScoreMultiplierValue);
                break;
            }
            case 3:
            {           
                ViewModel.DriftScoreMultiplierValue = Convert.ToSingle(e.NewValue);
                if (MiscCheatsFh5.DriftScoreMultiplierDetourAddress == 0) return;
                GetInstance().WriteMemory(MiscCheatsFh5.DriftScoreMultiplierDetourAddress + 0x20, ViewModel.DriftScoreMultiplierValue);
                break;
            }
            case 4:
            {              
                ViewModel.SkillTreeWideEditValue = Convert.ToSingle(e.NewValue);
                if (MiscCheatsFh5.SkillTreeWideEditDetourAddress == 0) return;
                GetInstance().WriteMemory(MiscCheatsFh5.SkillTreeWideEditDetourAddress + 0x1C, ViewModel.SkillTreeWideEditValue);
                break;
            }
            case 5:
            {             
                ViewModel.SkillTreeCostValue = Convert.ToInt32(e.NewValue);
                if (MiscCheatsFh5.SkillTreePerksCostDetourAddress == 0) return;
                GetInstance().WriteMemory(MiscCheatsFh5.SkillTreePerksCostDetourAddress + 0x1B, ViewModel.SkillTreeCostValue);
                break;
            }
            case 6:
            {            
                ViewModel.MissionTimeScaleValue = Convert.ToSingle(e.NewValue);
                if (MiscCheatsFh5.MissionTimeScaleDetourAddress == 0) return;
                GetInstance().WriteMemory(MiscCheatsFh5.MissionTimeScaleDetourAddress + 0x23, ViewModel.MissionTimeScaleValue);
                break;
            }
            case 7:
            {           
                ViewModel.TrailblazerTimeScaleValue = Convert.ToSingle(e.NewValue);
                if (MiscCheatsFh5.TrailblazerTimeScaleDetourAddress == 0) return;
                GetInstance().WriteMemory(MiscCheatsFh5.TrailblazerTimeScaleDetourAddress + 0x23, ViewModel.TrailblazerTimeScaleValue);
                break;
            }
        }
    }
    
    private async void MainToggleSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        ViewModel.MainUiElementsEnabled = false;
        switch (MainComboBox.SelectedIndex)
        {
            case 0:
            {
                await PrizeScale(toggleSwitch.IsOn);
                break;
            }
            case 1:
            {
                await SellFactor(toggleSwitch.IsOn);
                break;
            }
            case 2:
            {
                await SkillScoreMultiplier(toggleSwitch.IsOn);
                break;
            }
            case 3:
            {
                await DriftScoreMultiplier(toggleSwitch.IsOn);
                break;
            }
            case 4:
            {
                await SkillTreeWideEdit(toggleSwitch.IsOn);
                break;
            }
            case 5:
            {
                await SkillTreePerksCost(toggleSwitch.IsOn);
                break;
            }
            case 6:
            {
                await MissionTimeScale(toggleSwitch.IsOn);
                break;
            }
            case 7:
            {
                await TrailblazerTimeScale(toggleSwitch.IsOn);
                break;
            }
        }
        ViewModel.MainUiElementsEnabled = true;
    }

    private async Task PrizeScale(bool toggled)
    {
        if (MiscCheatsFh5.PrizeScaleDetourAddress == 0)
        {
            await MiscCheatsFh5.CheatPrizeScale();
        }

        if (MiscCheatsFh5.PrizeScaleDetourAddress == 0) return;
        GetInstance().WriteMemory(MiscCheatsFh5.PrizeScaleDetourAddress + 0x1B, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(MiscCheatsFh5.PrizeScaleDetourAddress + 0x1C, Convert.ToSingle(MainValueBox.Value));
        ViewModel.SpinPrizeScaleEnabled = toggled;
    }

    private async Task SellFactor(bool toggled)
    {
        if (MiscCheatsFh5.SellFactorDetourAddress == 0)
        {
            await MiscCheatsFh5.CheatSellFactor();
        }
        
        if (MiscCheatsFh5.SellFactorDetourAddress == 0) return;
        GetInstance().WriteMemory(MiscCheatsFh5.SellFactorDetourAddress + 0x1C, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(MiscCheatsFh5.SellFactorDetourAddress + 0x1D, Convert.ToInt32(MainValueBox.Value));
        ViewModel.SpinSellFactorEnabled = toggled;
    }

    private async Task SkillScoreMultiplier(bool toggled)
    {
        if (MiscCheatsFh5.SkillScoreMultiplierDetourAddress == 0)
        {
            await MiscCheatsFh5.CheatSkillScoreMultiplier();
        }
        
        if (MiscCheatsFh5.SkillScoreMultiplierDetourAddress == 0) return;
        GetInstance().WriteMemory(MiscCheatsFh5.SkillScoreMultiplierDetourAddress + 0x1C, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(MiscCheatsFh5.SkillScoreMultiplierDetourAddress + 0x1D, Convert.ToInt32(MainValueBox.Value));
        ViewModel.SkillScoreMultiplierEnabled = toggled;
    }
    
    private async Task DriftScoreMultiplier(bool toggled)
    {
        if (MiscCheatsFh5.DriftScoreMultiplierDetourAddress == 0)
        {
            await MiscCheatsFh5.CheatDriftScoreMultiplier();
        }
        
        if (MiscCheatsFh5.DriftScoreMultiplierDetourAddress == 0) return;
        GetInstance().WriteMemory(MiscCheatsFh5.DriftScoreMultiplierDetourAddress + 0x1F, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(MiscCheatsFh5.DriftScoreMultiplierDetourAddress + 0x20, Convert.ToSingle(MainValueBox.Value));
        ViewModel.DriftScoreMultiplierEnabled = toggled;
    }
    
    private async Task SkillTreeWideEdit(bool toggled)
    {
        if (MiscCheatsFh5.SkillTreeWideEditDetourAddress == 0)
        {
            await MiscCheatsFh5.CheatSkillTreeWideEdit();
        }
        
        if (MiscCheatsFh5.SkillTreeWideEditDetourAddress == 0) return;
        GetInstance().WriteMemory(MiscCheatsFh5.SkillTreeWideEditDetourAddress + 0x1B, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(MiscCheatsFh5.SkillTreeWideEditDetourAddress + 0x1C, Convert.ToSingle(MainValueBox.Value));
        ViewModel.SkillTreeWideEditEnabled = toggled;
    }
    
    private async Task SkillTreePerksCost(bool toggled)
    {
        if (MiscCheatsFh5.SkillTreePerksCostDetourAddress == 0)
        {
            await MiscCheatsFh5.CheatSkillTreePerksCost();
        }
        
        if (MiscCheatsFh5.SkillTreePerksCostDetourAddress == 0) return;
        GetInstance().WriteMemory(MiscCheatsFh5.SkillTreePerksCostDetourAddress + 0x1A, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(MiscCheatsFh5.SkillTreePerksCostDetourAddress + 0x1B, Convert.ToInt32(MainValueBox.Value));
        ViewModel.SkillTreeCostEnabled = toggled;
    }
    
    private async Task MissionTimeScale(bool toggled)
    {
        if (MiscCheatsFh5.MissionTimeScaleDetourAddress == 0)
        {
            await MiscCheatsFh5.CheatMissionTimeScale();
        }
        
        if (MiscCheatsFh5.MissionTimeScaleDetourAddress == 0) return;
        GetInstance().WriteMemory(MiscCheatsFh5.MissionTimeScaleDetourAddress + 0x22, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(MiscCheatsFh5.MissionTimeScaleDetourAddress + 0x23, Convert.ToSingle(MainValueBox.Value));
        ViewModel.MissionTimeScaleEnabled = toggled;
    }
    
    private async Task TrailblazerTimeScale(bool toggled)
    {
        if (MiscCheatsFh5.TrailblazerTimeScaleDetourAddress == 0)
        {
            await MiscCheatsFh5.CheatTrailblazerTimeScale();
        }
        
        if (MiscCheatsFh5.TrailblazerTimeScaleDetourAddress == 0) return;
        GetInstance().WriteMemory(MiscCheatsFh5.TrailblazerTimeScaleDetourAddress + 0x22, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(MiscCheatsFh5.TrailblazerTimeScaleDetourAddress + 0x23, Convert.ToSingle(MainValueBox.Value));
        ViewModel.TrailblazerTimeScaleEnabled = toggled;
    }

    private async void UnbreakableSkillScoreSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        toggleSwitch.IsEnabled = false;
        if (MiscCheatsFh5.UnbreakableSkillScoreDetourAddress == 0)
        {
            await MiscCheatsFh5.CheatUnbreakableSkillScore();
        }
        toggleSwitch.IsEnabled = true;
        
        if (MiscCheatsFh5.UnbreakableSkillScoreDetourAddress == 0) return;
        GetInstance().WriteMemory(MiscCheatsFh5.UnbreakableSkillScoreDetourAddress + 0x1A, toggleSwitch.IsOn ? (byte)1 : (byte)0);
    }

    private async void RemoveBuildCapSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        toggleSwitch.IsEnabled = false;
        if (MiscCheatsFh5.RemoveBuildCapDetourAddress == 0)
        {
            await MiscCheatsFh5.CheatRemoveBuildCap();
        }
        toggleSwitch.IsEnabled = true;
        
        if (MiscCheatsFh5.RemoveBuildCapDetourAddress == 0) return;
        GetInstance().WriteMemory(MiscCheatsFh5.RemoveBuildCapDetourAddress + 0x16, toggleSwitch.IsOn ? (byte)1 : (byte)0);
    }
}