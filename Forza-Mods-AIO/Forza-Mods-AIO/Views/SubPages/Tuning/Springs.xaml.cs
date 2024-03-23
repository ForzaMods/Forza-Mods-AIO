using System.Windows;
using System.Windows.Controls;
using Forza_Mods_AIO.Cheats.ForzaHorizon5;
using static Forza_Mods_AIO.Resources.Memory;

namespace Forza_Mods_AIO.Views.SubPages.Tuning;

public partial class Springs : Page
{
    public Springs()
    {
        InitializeComponent();
    }

    private static TuningCheats TuningCheatsFh5 => Forza_Mods_AIO.Resources.Cheats.GetClass<TuningCheats>();
    private static readonly int[] Offsets = [0x330, 0x8, 0x1E0, 0x0];
    private static UIntPtr Ptr => GetInstance().FollowMultiLevelPointer(TuningCheatsFh5.Base2, Offsets);
    private bool _codeChange;

    private void MainComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (MainComboBox == null || MainValueBox == null || !TuningCheatsFh5.WasScanSuccessful)
        {
            return;
        }

        MainValueBox.Value = MainComboBox.SelectedIndex switch
        {
            0 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.FrontSpringMinOffset),
            1 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.FrontSpringMaxOffset),
            2 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.RearSpringMinOffset),
            3 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.RearSpringMaxOffset),
            _ => MainValueBox.Value
        };
    }

    private void MainValueBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!TuningCheatsFh5.WasScanSuccessful)
        {
            return;
        }

        var newValue = Convert.ToSingle(e.NewValue.GetValueOrDefault());
        switch (MainComboBox.SelectedIndex)
        {
            case 0:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.FrontSpringMinOffset, newValue);
                break;
            }
            case 1:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.FrontSpringMaxOffset, newValue);
                break;
            }
            case 2:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.RearSpringMinOffset, newValue);
                break;
            }
            case 3:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.RearSpringMaxOffset, newValue);
                break;
            }
        }
    }

    private void MainButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (MainComboBox == null || MainValueBox == null || !TuningCheatsFh5.WasScanSuccessful)
        {
            return;
        }

        MainValueBox.Value = MainComboBox.SelectedIndex switch
        {
            0 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.FrontSpringMinOffset),
            1 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.FrontSpringMaxOffset),
            2 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.RearSpringMinOffset),
            3 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.RearSpringMaxOffset),
            _ => MainValueBox.Value
        };
    }

    private void ComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        UpdateValue();
    }

    private void UnitBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        UpdateValue();
    }

    private void ValueBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (_codeChange || !TuningCheatsFh5.WasScanSuccessful)
        {
            return;
        }

        var newValue = ConvertRideHeightToGameValue(UnitBox.SelectedIndex, Convert.ToSingle(e.NewValue));
        switch (ComboBox.SelectedIndex)
        {
            case 0:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.FrontRideHeightMinOffset, newValue);
                break;
            }
            case 1:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.FrontRideHeightMaxOffset, newValue);
                break;
            }
            case 2:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.FrontRestrictionOffset, newValue);
                break;
            }
            case 3:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.RearRideHeightMinOffset, newValue);
                break;
            }
            case 4:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.RearRideHeightMaxOffset, newValue);
                break;
            }
            case 5:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.RearRestrictionOffset, newValue);
                break;
            }
        }
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        UpdateValue();
    }

    private void UpdateValue()
    {
        if (!TuningCheatsFh5.WasScanSuccessful)
        {
            return;
        }

        _codeChange = true;
        ValueBox.Value = ComboBox.SelectedIndex switch
        {
            0 => ConvertGameValueToUnit(UnitBox.SelectedIndex, Ptr + TuningOffsets.FrontRideHeightMinOffset),
            1 => ConvertGameValueToUnit(UnitBox.SelectedIndex, Ptr + TuningOffsets.FrontRideHeightMaxOffset),
            2 => ConvertGameValueToUnit(UnitBox.SelectedIndex, Ptr + TuningOffsets.FrontRestrictionOffset),
            3 => ConvertGameValueToUnit(UnitBox.SelectedIndex, Ptr + TuningOffsets.RearRideHeightMinOffset),
            4 => ConvertGameValueToUnit(UnitBox.SelectedIndex, Ptr + TuningOffsets.RearRideHeightMaxOffset),
            5 => ConvertGameValueToUnit(UnitBox.SelectedIndex, Ptr + TuningOffsets.RearRestrictionOffset),
            _ => ValueBox.Value
        };
        _codeChange = false;
    }

    private static float ConvertGameValueToUnit(int selectedIndex, UIntPtr address)
    {
        var value = GetInstance().ReadMemory<float>(address);
        
        return selectedIndex switch
        {
            0 => RideHeightToCentimeters(value),
            1 => RideHeightToInches(value),
            _ => 0
        };
    }
    
    private static float ConvertRideHeightToGameValue(int comboBoxIndex, float value)
    {
        return comboBoxIndex switch
        {
            0 => ConvertToOriginalValue(value),
            1 => ConvertToOriginalValue(ConvertToCentimetersFromInches(value)),
            _ => 0
        };
    }
    
    private static float ConvertToCentimetersFromInches(float inchesValue)
    {
        return inchesValue * 2.54f;
    }

    private static float ConvertToOriginalValue(float valueInCentimeters)
    {
        return valueInCentimeters / 100f;
    }
    
    private static float RideHeightToInches(float rideHeightValue)
    {
        return RideHeightToCentimeters(rideHeightValue) / 2.54f;
    }
    
    private static float RideHeightToCentimeters(float rideHeightValue)
    {
        return rideHeightValue * 100;
    }
}