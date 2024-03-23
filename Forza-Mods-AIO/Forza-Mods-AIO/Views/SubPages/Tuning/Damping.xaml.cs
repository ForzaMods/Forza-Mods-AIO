using System.Windows;
using System.Windows.Controls;
using Forza_Mods_AIO.Cheats.ForzaHorizon5;
using static Forza_Mods_AIO.Resources.Memory;

namespace Forza_Mods_AIO.Views.SubPages.Tuning;

public partial class Damping
{
    public Damping()
    {
        InitializeComponent();
    }
    
    private static TuningCheats TuningCheatsFh5 => Forza_Mods_AIO.Resources.Cheats.GetClass<TuningCheats>();
    private static readonly int[] Offsets = [0x330, 0x8, 0x1E0, 0x0];
    private static UIntPtr Ptr => GetInstance().FollowMultiLevelPointer(TuningCheatsFh5.Base2, Offsets);

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        UpdateValue();
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
                GetInstance().WriteMemory(Ptr + TuningOffsets.FrontAntiRollMinOffset, newValue);                
                break;
            }
            case 1:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.FrontAntiRollMaxOffset, newValue);                
                break;
            }
            case 2:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.RearAntiRollMinOffset, newValue);                
                break;
            }
            case 3:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.RearAntiRollMaxOffset, newValue);                
                break;
            }
            case 4:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.FrontReboundStiffnessMinOffset, newValue);                
                break;
            }
            case 5:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.FrontReboundStiffnessMaxOffset, newValue);                
                break;
            }
            case 6:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.RearReboundStiffnessMinOffset, newValue);                
                break;
            }
            case 7:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.RearReboundStiffnessMaxOffset, newValue);                
                break;
            }
            case 8:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.FrontBumpStiffnessMinOffset, newValue);                
                break;
            }
            case 9:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.FrontBumpStiffnessMaxOffset, newValue);                
                break;
            }
            case 10:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.RearBumpStiffnessMinOffset, newValue);                
                break;
            }
            case 11:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.RearBumpStiffnessMaxOffset, newValue);                
                break;
            }
        }
    }

    private void ComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        UpdateValue();
    }

    private void UpdateValue()
    {
        if (ValueBox == null || !TuningCheatsFh5.WasScanSuccessful)
        {
            return;
        }

        ValueBox.Value = ComboBox.SelectedIndex switch
        {
            0 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.FrontAntiRollMinOffset),
            1 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.FrontAntiRollMaxOffset),
            2 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.RearAntiRollMinOffset),
            3 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.RearAntiRollMaxOffset),
            4 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.FrontReboundStiffnessMinOffset),
            5 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.FrontReboundStiffnessMaxOffset),
            6 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.RearReboundStiffnessMinOffset),
            7 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.RearReboundStiffnessMaxOffset),
            8 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.FrontBumpStiffnessMinOffset),
            9 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.FrontBumpStiffnessMaxOffset),
            10 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.RearBumpStiffnessMinOffset),
            11 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.RearBumpStiffnessMaxOffset),
            _ => ValueBox.Value
        };
    }
}