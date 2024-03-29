﻿using System.Windows;
using System.Windows.Controls;
using Forza_Mods_AIO.Cheats.ForzaHorizon5;
using Forza_Mods_AIO.ViewModels.SubPages.SelfVehicle;
using MahApps.Metro.Controls;
using static Forza_Mods_AIO.Resources.Memory;

namespace Forza_Mods_AIO.Views.SubPages.SelfVehicle;

public partial class Unlocks
{
    public Unlocks()
    {
        ViewModel = new UnlocksViewModel();
        DataContext = this;
        
        InitializeComponent();
    }

    public UnlocksViewModel ViewModel { get; }
    private static UnlocksCheats UnlocksCheatsFh5 => Forza_Mods_AIO.Resources.Cheats.GetClass<UnlocksCheats>();
    
    private async void ToggleSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        ViewModel.AreUiElementsEnabled = false;

        switch (UnlockBox.SelectedIndex)
        {
            case 0:
            {
                await Credits(toggleSwitch.IsOn);
                break;
            }
            case 1:
            {
                await Xp(toggleSwitch.IsOn);
                break;
            }
            case 2:
            {
                await Wheelspins(toggleSwitch.IsOn);
                break;
            }
            case 3:
            {
                await SkillPoints(toggleSwitch.IsOn);
                break;
            }
            case 4:
            {
                await Series(toggleSwitch.IsOn);
                break;
            }
            case 5:
            {
                await Seasonal(toggleSwitch.IsOn);
                break;
            }
        }

        ViewModel.AreUiElementsEnabled = true;
    }

    private async Task Credits(bool toggled)
    {
        if (UnlocksCheatsFh5.CreditsDetourAddress == 0)
        {
            await UnlocksCheatsFh5.CheatCredits();
        }

        if (UnlocksCheatsFh5.CreditsDetourAddress <= 0) return;
        GetInstance().WriteMemory(UnlocksCheatsFh5.CreditsDetourAddress + 0x31, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(UnlocksCheatsFh5.CreditsDetourAddress + 0x32, Convert.ToInt32(ValueBox.Value));  
        ViewModel.IsCreditsEnabled = toggled;
    }

    private async Task Xp(bool toggled)
    {
        if (UnlocksCheatsFh5.XpDetourAddress == 0)
        {
            await UnlocksCheatsFh5.CheatXp();
        }

        if (UnlocksCheatsFh5.XpDetourAddress <= 0) return;
        GetInstance().WriteMemory(UnlocksCheatsFh5.XpPointsDetourAddress + 0x1B, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(UnlocksCheatsFh5.XpDetourAddress + 0x1B, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(UnlocksCheatsFh5.XpDetourAddress + 0x1C, Convert.ToInt32(ValueBox.Value));  
        ViewModel.IsXpEnabled = toggled;
    }

    private async Task Wheelspins(bool toggled)
    {
        if (UnlocksCheatsFh5.SpinsDetourAddress == 0)
        {
            await UnlocksCheatsFh5.CheatSpins();
        }

        if (UnlocksCheatsFh5.SpinsDetourAddress <= 0) return;
        GetInstance().WriteMemory(UnlocksCheatsFh5.SpinsDetourAddress + 0x1C, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(UnlocksCheatsFh5.SpinsDetourAddress + 0x1D, Convert.ToInt32(ValueBox.Value) + 1);  
        ViewModel.IsWheelspinsEnabled = toggled;
    }

    private async Task SkillPoints(bool toggled)
    {
        if (UnlocksCheatsFh5.SkillPointsDetourAddress == 0)
        {
            await UnlocksCheatsFh5.CheatSkillPoints();
        }

        if (UnlocksCheatsFh5.SkillPointsDetourAddress <= 0) return;
        GetInstance().WriteMemory(UnlocksCheatsFh5.SkillPointsDetourAddress + 0x19, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(UnlocksCheatsFh5.SkillPointsDetourAddress + 0x1A, Convert.ToInt32(ValueBox.Value));  
        ViewModel.IsSkillPointsEnabled = toggled;
    }
    
    private async Task Seasonal(bool toggled)
    {
        if (UnlocksCheatsFh5.SeasonalDetourAddress == 0)
        {
            await UnlocksCheatsFh5.CheatSeasonal();
        }

        if (UnlocksCheatsFh5.SeasonalDetourAddress <= 0) return;
        GetInstance().WriteMemory(UnlocksCheatsFh5.SeasonalDetourAddress + 0x23, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(UnlocksCheatsFh5.SeasonalDetourAddress + 0x24, Convert.ToInt32(ValueBox.Value));  
        ViewModel.IsSeasonalEnabled = toggled;
    }
    
    private async Task Series(bool toggled)
    {
        if (UnlocksCheatsFh5.SeriesDetourAddress == 0)
        {
            await UnlocksCheatsFh5.CheatSeries();
        }

        if (UnlocksCheatsFh5.SeriesDetourAddress <= 0) return;
        GetInstance().WriteMemory(UnlocksCheatsFh5.SeriesDetourAddress + 0x1B, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(UnlocksCheatsFh5.SeriesDetourAddress + 0x1C, Convert.ToInt32(ValueBox.Value));  
        ViewModel.IsSeriesEnabled = toggled;
    }

    private void UnlockBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (UnlockSwitch == null)
        {
            return;
        }
        
        UnlockSwitch.Toggled -= ToggleSwitch_OnToggled;

        switch (UnlockBox.SelectedIndex)
        {
            case 0:
            {
                ValueBox.Value = ViewModel.CreditsValue;
                UnlockSwitch.IsOn = ViewModel.IsCreditsEnabled;
                break;
            }
            case 1:
            {
                ValueBox.Value = ViewModel.XpValue;
                UnlockSwitch.IsOn = ViewModel.IsXpEnabled;
                break;
            }
            case 2:
            {
                ValueBox.Value = ViewModel.WheelspinsValue;
                UnlockSwitch.IsOn = ViewModel.IsWheelspinsEnabled;
                break;
            }
            case 3:
            {
                ValueBox.Value = ViewModel.SkillPointsValue;
                UnlockSwitch.IsOn = ViewModel.IsSkillPointsEnabled;
                break;
            }
            case 4:
            {
                ValueBox.Value = ViewModel.SeriesValue;
                UnlockSwitch.IsOn = ViewModel.IsSeriesEnabled;
                break;
            }
            case 5:
            {
                ValueBox.Value = ViewModel.SeasonalValue;
                UnlockSwitch.IsOn = ViewModel.IsSeasonalEnabled;
                break;
            }
        }
        
        UnlockSwitch.Toggled += ToggleSwitch_OnToggled;
    }
    
    private void UnlockBox_OnLoaded(object sender, RoutedEventArgs e)
    {
        if (ViewModel.WasComboBoxLoaded || sender is not ComboBox { HasItems: true } comboBox)
        {
            return;
        }
        
        for (var i = 0; i < comboBox.Items.Count; i++)
        {
            if (comboBox.Items[i] is not ComboBoxItem { Visibility: Visibility.Visible })
            {
                continue;
            }
            
            comboBox.SelectedIndex = i;
            break;
        }

        ViewModel.WasComboBoxLoaded = true;
    }

    private void ValueBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        switch (UnlockBox.SelectedIndex)
        {
            case 0:
            {
                ViewModel.CreditsValue = Convert.ToInt32(ValueBox.Value);
                if (UnlocksCheatsFh5.CreditsDetourAddress <= 0) return;
                GetInstance().WriteMemory(UnlocksCheatsFh5.CreditsDetourAddress + 0x32, Convert.ToInt32(ValueBox.Value));  
                break;
            }
            case 1:
            {
                ViewModel.XpValue = Convert.ToInt32(ValueBox.Value);
                if (UnlocksCheatsFh5.XpDetourAddress <= 0) return;
                GetInstance().WriteMemory(UnlocksCheatsFh5.XpDetourAddress + 0x1C, Convert.ToInt32(ValueBox.Value));  
                break;
            }
            case 2:
            {
                ViewModel.XpValue = Convert.ToInt32(ValueBox.Value);
                if (UnlocksCheatsFh5.SpinsDetourAddress <= 0) return;
                GetInstance().WriteMemory(UnlocksCheatsFh5.SpinsDetourAddress + 0x1D, Convert.ToInt32(ValueBox.Value));  
                break;
            }
            case 3:
            {
                ViewModel.SkillPointsValue = Convert.ToInt32(ValueBox.Value);
                if (UnlocksCheatsFh5.SkillPointsDetourAddress <= 0) return;
                GetInstance().WriteMemory(UnlocksCheatsFh5.SkillPointsDetourAddress + 0x1A, Convert.ToInt32(ValueBox.Value));  
                break;
            }
            case 4:
            {
                ViewModel.SeriesValue = Convert.ToInt32(ValueBox.Value);
                if (UnlocksCheatsFh5.SeriesDetourAddress <= 0) return;
                GetInstance().WriteMemory(UnlocksCheatsFh5.SeriesDetourAddress + 0x1C, Convert.ToInt32(ValueBox.Value));  
                break;
            }
            case 5:
            {
                ViewModel.SeasonalValue = Convert.ToInt32(ValueBox.Value);
                if (UnlocksCheatsFh5.SeasonalDetourAddress <= 0) return;
                GetInstance().WriteMemory(UnlocksCheatsFh5.SeasonalDetourAddress + 0x24, Convert.ToInt32(ValueBox.Value));  
                break;
            }
        }
    }
}