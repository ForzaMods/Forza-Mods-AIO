using System.Windows;
using System.Windows.Controls;
using Forza_Mods_AIO.Cheats.ForzaHorizon5;
using static Forza_Mods_AIO.Resources.Memory;

namespace Forza_Mods_AIO.Views.SubPages.Tuning;

public partial class Steering : Page
{
    public Steering()
    {
        InitializeComponent();
    }

    private static TuningCheats TuningCheatsFh5 => Forza_Mods_AIO.Resources.Cheats.GetClass<TuningCheats>();
    private static readonly int[] Offsets = [0x330, 0x8, 0x1E0, 0x0];
    private static UIntPtr Ptr => GetInstance().FollowMultiLevelPointer(TuningCheatsFh5.Base2, Offsets);

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        if (ComboBox == null || ValueBox == null || !TuningCheatsFh5.WasScanSuccessful)
        {
            return;
        }

        ValueBox.Value = ComboBox.SelectedIndex switch
        {
            0 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.AngleMaxOffset),
            1 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.AngleMax2Offset),
            2 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.VelocityStraightOffset),
            3 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.VelocityTurningOffset),
            4 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.VelocityCountersteerOffset),
            5 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.VelocityDynamicPeekOffset),
            6 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.TimeToMaxSteeringOffset),
            _ => ValueBox.Value
        };
    }

    private void ValueBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!TuningCheatsFh5.WasScanSuccessful)
        {
            return;
        }

        var newValue = Convert.ToSingle(e.NewValue.GetValueOrDefault());
        switch (ComboBox.SelectedIndex)
        {
            case 0:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.AngleMaxOffset, newValue);                
                break;
            }
            case 1:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.AngleMax2Offset, newValue);                
                break;
            }
            case 2:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.VelocityStraightOffset, newValue);                
                break;
            }
            case 3:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.VelocityTurningOffset, newValue);                
                break;
            }
            case 4:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.VelocityCountersteerOffset, newValue);                
                break;
            }
            case 5:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.VelocityDynamicPeekOffset, newValue);                
                break;
            }
            case 6:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.TimeToMaxSteeringOffset, newValue);                
                break;
            }
        }
    }

    private void ComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ValueBox == null || !TuningCheatsFh5.WasScanSuccessful)
        {
            return;
        }

        ValueBox.Value = ComboBox.SelectedIndex switch
        {
            0 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.AngleMaxOffset),
            1 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.AngleMax2Offset),
            2 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.VelocityStraightOffset),
            3 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.VelocityTurningOffset),
            4 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.VelocityCountersteerOffset),
            5 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.VelocityDynamicPeekOffset),
            6 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.TimeToMaxSteeringOffset),
            _ => ValueBox.Value
        };
    }
}