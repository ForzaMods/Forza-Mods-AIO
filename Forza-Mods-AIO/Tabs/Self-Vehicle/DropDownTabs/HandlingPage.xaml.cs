using System;
using System.Windows;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using Forza_Mods_AIO.Tabs.Self_Vehicle.Features;

using static System.Math;
using static System.Convert;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.Entities.CarEntity;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.SelfVehicleAddresses;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

/// <summary>
///     Interaction logic for SpeedHacksPage.xaml
/// </summary>
public partial class HandlingPage
{
    public static HandlingPage Shp { get; private set; } = null!;

    private readonly byte[] _before1 = { 0x0F, 0x11, 0x41, 0x10 },
        _before2 = { 0x0F, 0x11, 0x49, 0x20 },
        _before3 = { 0x0F, 0x11, 0x41, 0x30 },
        _before4 = { 0x0F, 0x11, 0x49, 0x40 };
    private readonly byte[] _nop = { 0x90, 0x90, 0x90, 0x90 };
        
    public HandlingPage()
    {
        InitializeComponent();
        Shp = this;
        DataContext = this;
    }
    
    private void VelocitySwitch_Toggled(object sender, RoutedEventArgs e)
    {
        if (VelocitySwitch is not { IsOn:true })
        {
            return;
        }
            
        Task.Run(Velocity.Run);
    }
    
    private void WheelSpeedSwitch_Toggled(object sender, RoutedEventArgs e)
    {
        if (WheelSpeedSwitch is not { IsOn: true })
        {
            return;
        }
            
        Task.Run(Features.WheelSpeed.Run);
    }

    public async void PullButton_Click(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached || sender is not Button button)
        {
            return;
        }
        
        if (button.Name.Contains("Gravity"))
        {
            GravityValueNum.Value = Gravity;
        }
        else
        {
            AccelValueNum.Value = Acceleration;
        }
    }

    private void SetSwitch_Toggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch { IsOn: true } toggleSwitch)
        {
            return;
        }

        Task.Run(() => Modifiers.Run(toggleSwitch));
    }
    
    private void TurnAssistSwitch_Toggled(object sender, RoutedEventArgs e)
    {
        if (!TurnAssistSwitch.IsOn)
        {
            return;
        }

        Task.Run(TurnAssist.Run);
    }

    private void SuperCarSwitch_Toggled(object sender, RoutedEventArgs e)
    {
        var process = Mw.Gvp.Process;
        if (!Mw.Attached || process.MainModule == null || SuperCarAddr < (UIntPtr)process.MainModule.BaseAddress)
        {
            return;
        }
        
        if (SuperCarSwitch.IsOn)
        {
            Mw.M.WriteArrayMemory(SuperCarAddr + 4, _nop);
            Mw.M.WriteArrayMemory(SuperCarAddr + 12, _nop);
            Mw.M.WriteArrayMemory(SuperCarAddr + 20, _nop);
            Mw.M.WriteArrayMemory(SuperCarAddr + 32, _nop);
        }
        else
        {
            Mw.M.WriteArrayMemory(SuperCarAddr + 4, _before1);
            Mw.M.WriteArrayMemory(SuperCarAddr + 12, _before2);
            Mw.M.WriteArrayMemory(SuperCarAddr + 20, _before3);
            Mw.M.WriteArrayMemory(SuperCarAddr + 32, _before4);
        }
    }

    private void WaterDragSwitch_Toggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        Mw.M.WriteMemory(WaterAddr, WaterDragSwitch.IsOn ? new Vector3(0f, 0f, 0f) : new Vector3(0f, 3700f, 13500f));
    }

    private void SuperBrakeSwitch_Toggled(object sender, RoutedEventArgs e)
    {
        if (!SuperBrakeSwitch.IsOn)
        {
            return;
        }

        Task.Run(Braking.SuperBrake.Run);
    }

    private void StopAllWheelsSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!StopAllWheelsSwitch.IsOn)
        {
            return;
        }

        Task.Run(Braking.StopAllWheels.Run);
    }
        
        
    private void FlyHackSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }
        
        Task.Run(FlyHack.Run);
    }
    
    private void CarNoClipSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (CarNoClipSwitch == null)
        {
            return;
        }

        CarNoClip.Toggle(CarNoClipSwitch.IsOn);
    }

    private void WallNoClipSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (WallNoClipSwitch == null)
        {
            return;
        }

        WallNoClip.Toggle(WallNoClipSwitch.IsOn);
    }

    private void JumpHackSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (JumpHackVelocityNum == null)
        {
            return;
        }
        
        JumpHackVelocityNum.Value = Round(e.NewValue, 4);
    }

    private void JumpHackVelocityNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (JumpHackSwitch == null)
        {
            return;
        }
        
        JumpHackSlider.Value = ToDouble(JumpHackVelocityNum.Value);
        JumpHack.SetBoost(JumpHackVelocityNum.Value);
    }

    private void JumpHackSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!JumpHackSwitch.IsOn)
        {
            return;
        }

        Task.Run(JumpHack.Run);
    }

    private void VelValueNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (VelValueNum == null)
        {
            return;
        }
        
        VelValueNum.Value = Round(ToDouble(e.NewValue), 2);
        Velocity.SetBoost(VelValueNum.Value);
    }

    private void StopAllWheelsModeBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is not ComboBox comboBox)
        {
            return;
        }
        
        Braking.StopAllWheels.SetMode(comboBox.SelectedIndex);
    }

    private void StopAllWheelsIntervalBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (sender is not NumericUpDown numericUpDown)
        {
            return;
        }
        
        Braking.StopAllWheels.SetInterval(numericUpDown.Value);
    }

    private void StopAllWheelsStrengthBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (sender is not NumericUpDown numericUpDown)
        {
            return;
        }
        
        Braking.StopAllWheels.SetStrength(numericUpDown.Value);
    }

    private void SuperBrakeVelocity_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (sender is not NumericUpDown numericUpDown || SuperBrakeSlider == null)
        {
            return;
        }

        var newValue = ToDouble(numericUpDown.Value);

        numericUpDown.Value = Round(newValue, 4);
        SuperBrakeSlider.Value = newValue;
        Braking.SuperBrake.SetStrength(newValue);
    }

    private void SuperBrakeSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (sender is not Slider slider || SuperBrakeVelocity == null)
        {
            return;
        }
        
        SuperBrakeVelocity.Value = Round(slider.Value, 4);
        Braking.SuperBrake.SetStrength(slider.Value);
    }

    private void FlyHackMoveSpeedNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (sender is not NumericUpDown numericUpDown)
        {
            return;
        }

        FlyHack.SetMoveSpeed(numericUpDown.Value);
    }

    private void FlyHackRotSpeedNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (sender is not NumericUpDown numericUpDown)
        {
            return;
        }

        FlyHack.SetRotSpeed(numericUpDown.Value);
    }

    private void StrengthBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (sender is not NumericUpDown numericUpDown)
        {
            return;
        }

        Features.WheelSpeed.SetBoostFactor(numericUpDown.Value);
    }

    private void IntervalBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (sender is not NumericUpDown numericUpDown)
        {
            return;
        }

        Features.WheelSpeed.SetInterval(numericUpDown.Value);
    }

    private void WheelSpeedModeBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is not ComboBox comboBox)
        {
            return;
        }

        Features.WheelSpeed.SetMode(comboBox.SelectedIndex);
    }

    private void VelModeBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is not ComboBox comboBox)
        {
            return;
        }

        Velocity.SetMode(comboBox.SelectedIndex);
    }

    private void VelLimitBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (sender is not NumericUpDown numericUpDown)
        {
            return;
        }

        Velocity.SetLimit(numericUpDown.Value);
    }

    private void TurnAssistStrengthBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (sender is not NumericUpDown numericUpDown)
        {
            return;
        }

        TurnAssist.SetStrength(numericUpDown.Value);
    }

    private void TurnAssistIntervalBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (sender is not NumericUpDown numericUpDown)
        {
            return;
        }

        TurnAssist.SetInterval(numericUpDown.Value);
    }

    private void TurnAssistRatioBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (sender is not NumericUpDown numericUpDown)
        {
            return;
        }

        TurnAssist.SetRatio(numericUpDown.Value);
    }
}