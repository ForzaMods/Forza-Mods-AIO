using System.Windows;
using System.Windows.Controls;
using Forza_Mods_AIO.Cheats.ForzaHorizon5;
using static Forza_Mods_AIO.Resources.Memory;

namespace Forza_Mods_AIO.Views.SubPages.Tuning;

public partial class Others
{
    public Others()
    {
        InitializeComponent();
    }
    
    private static TuningCheats TuningCheatsFh5 => Forza_Mods_AIO.Resources.Cheats.GetClass<TuningCheats>();
    private static readonly int[] Offsets1 = [0x330, 0x8, 0x1E0, 0x0];
    private static UIntPtr Ptr1 => GetInstance().FollowMultiLevelPointer(TuningCheatsFh5.Base2, Offsets1);
    private static readonly int[] Offsets2 = [0x150, 0x300, 0x0];
    private static UIntPtr Ptr2 => GetInstance().FollowMultiLevelPointer(TuningCheatsFh5.Base3, Offsets2);

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
                GetInstance().WriteMemory(Ptr1 + TuningOffsets.WheelbaseOffset, newValue);                
                break;
            }
            case 1:
            {
                GetInstance().WriteMemory(Ptr1 + TuningOffsets.FrontWidthOffset, newValue);                
                break;
            }
            case 2:
            {
                GetInstance().WriteMemory(Ptr1 + TuningOffsets.FrontSpacerOffset, newValue);                
                break;
            }
            case 3:
            {
                GetInstance().WriteMemory(Ptr1 + TuningOffsets.RearWidthOffset, newValue);                
                break;
            }
            case 4:
            {
                GetInstance().WriteMemory(Ptr1 + TuningOffsets.RearSpacerOffset, newValue);                
                break;
            }
            case 5:
            {
                GetInstance().WriteMemory(Ptr2 + TuningOffsets.RimSizeFrontOffset, newValue);                
                break;
            }
            case 6:
            {
                GetInstance().WriteMemory(Ptr2 + TuningOffsets.RimRadiusFrontOffset, newValue);                
                break;
            }
            case 7:
            {
                GetInstance().WriteMemory(Ptr2 + TuningOffsets.RimSizeRearOffset, newValue);                
                break;
            }
            case 8:
            {
                GetInstance().WriteMemory(Ptr2 + TuningOffsets.RimRadiusRearOffset, newValue);                
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
            0 => GetInstance().ReadMemory<float>(Ptr1 + TuningOffsets.WheelbaseOffset),
            1 => GetInstance().ReadMemory<float>(Ptr1 + TuningOffsets.FrontWidthOffset),
            2 => GetInstance().ReadMemory<float>(Ptr1 + TuningOffsets.FrontSpacerOffset),
            3 => GetInstance().ReadMemory<float>(Ptr1 + TuningOffsets.RearWidthOffset),
            4 => GetInstance().ReadMemory<float>(Ptr1 + TuningOffsets.RearSpacerOffset),
            5 => GetInstance().ReadMemory<float>(Ptr2 + TuningOffsets.RimSizeFrontOffset),
            6 => GetInstance().ReadMemory<float>(Ptr2 + TuningOffsets.RimRadiusFrontOffset),
            7 => GetInstance().ReadMemory<float>(Ptr2 + TuningOffsets.RimSizeRearOffset),
            8 => GetInstance().ReadMemory<float>(Ptr2 + TuningOffsets.RimRadiusRearOffset),
            _ => ValueBox.Value
        };
    }
}