using System.Windows;
using MahApps.Metro.Controls;
using System.Windows.Controls;
using Forza_Mods_AIO.Cheats.ForzaHorizon5;
using static Forza_Mods_AIO.Resources.Cheats;
using static Forza_Mods_AIO.Resources.Memory;
using Forza_Mods_AIO.ViewModels.SubPages.SelfVehicle;

namespace Forza_Mods_AIO.Views.SubPages.SelfVehicle;

public partial class Handling
{
    public Handling()
    {
        ViewModel = new HandlingViewModel();
        DataContext = this;
        
        InitializeComponent();
    }

    private static CarCheats CarCheatsFh5 => GetClass<CarCheats>();
    public HandlingViewModel ViewModel { get; }

    private async void ModifierToggleSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        ViewModel.AreModifierUiElementsEnabled = false;
        
        if (ModifierModeBox.SelectedIndex == 0)
        {
            await Accel(toggleSwitch.IsOn);
        }
        else
        {
            await Gravity(toggleSwitch.IsOn);
        }

        ViewModel.AreModifierUiElementsEnabled = true;
    }

    private async Task Accel(bool toggled)
    {
        if (CarCheatsFh5.AccelDetourAddress == 0)
        {
            await CarCheatsFh5.CheatAccel();
        }

        if (CarCheatsFh5.AccelDetourAddress <= 0) return;
        GetInstance().WriteMemory(CarCheatsFh5.AccelDetourAddress + 0x2C, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(CarCheatsFh5.AccelDetourAddress + 0x2D, Convert.ToSingle(ModifierValueBox.Value));  
        ViewModel.IsAccelEnabled = toggled;
    }
    
    private async Task Gravity(bool toggled)
    {
        if (CarCheatsFh5.GravityDetourAddress == 0)
        {
            await CarCheatsFh5.CheatGravity();
        }
            
        if (CarCheatsFh5.GravityDetourAddress <= 0) return;
        GetInstance().WriteMemory(CarCheatsFh5.GravityDetourAddress + 0x2E, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(CarCheatsFh5.GravityDetourAddress + 0x2F,Convert.ToSingle(ModifierValueBox.Value));   
        ViewModel.IsGravityEnabled = toggled;
    }

    private void ModifierModeBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ModifierToggleSwitch == null)
        {
            return;
        }
        
        ModifierToggleSwitch.Toggled -= ModifierToggleSwitch_OnToggled;
        
        if (ModifierModeBox.SelectedIndex == 0)
        {
            ModifierValueBox.Value = ViewModel.AccelValue;
            ModifierToggleSwitch.IsOn = ViewModel.IsAccelEnabled;
        }
        else
        {
            ModifierValueBox.Value = ViewModel.GravityValue;
            ModifierToggleSwitch.IsOn = ViewModel.IsGravityEnabled;
        }
        
        ModifierToggleSwitch.Toggled += ModifierToggleSwitch_OnToggled;
    }

    private void ModifierValueBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (ModifierModeBox.SelectedIndex == 0)
        {
            ViewModel.AccelValue = Convert.ToDouble(e.NewValue);
            if (CarCheatsFh5.AccelDetourAddress <= UIntPtr.Zero) return;
            GetInstance().WriteMemory(CarCheatsFh5.AccelDetourAddress + 0x2D, Convert.ToSingle(e.NewValue));  
        }
        else
        {
            ViewModel.GravityValue = Convert.ToDouble(e.NewValue);
            if (CarCheatsFh5.GravityDetourAddress <= UIntPtr.Zero) return;
            GetInstance().WriteMemory(CarCheatsFh5.GravityDetourAddress + 0x2F,Convert.ToSingle(e.NewValue));   
        }
    }

    private async void PullButton_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.AreModifierUiElementsEnabled = false;
        
        var localPlayer= await CarCheatsFh5.GetLocalPlayer();

        if (ModifierModeBox.SelectedIndex == 0)
        {
            ViewModel.AccelValue = Convert.ToDouble(GetInstance().ReadMemory<float>(localPlayer + 0xC));
            ModifierValueBox.Value = ViewModel.AccelValue;
        }
        else
        {
            ViewModel.GravityValue = Convert.ToDouble(GetInstance().ReadMemory<float>(localPlayer + 0x8));
            ModifierValueBox.Value = ViewModel.GravityValue;
        }
        
        ViewModel.AreModifierUiElementsEnabled = true;
    }
}