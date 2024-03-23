using System.Windows;
using System.Windows.Controls;
using Forza_Mods_AIO.Cheats.ForzaHorizon5;
using static Forza_Mods_AIO.Resources.Memory;

namespace Forza_Mods_AIO.Views.SubPages.Tuning;

public partial class Alignment
{
    public Alignment()
    {
        InitializeComponent();
    }
    
    private static TuningCheats TuningCheatsFh5 => Forza_Mods_AIO.Resources.Cheats.GetClass<TuningCheats>();
    private static readonly int[] Offsets = [0x8B0, 0x0];
    private static UIntPtr Ptr => GetInstance().FollowMultiLevelPointer(TuningCheatsFh5.Base1, Offsets);

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        if (ComboBox == null || ValueBox == null || !TuningCheatsFh5.WasScanSuccessful)
        {
            return;
        }

        ValueBox.Value = ComboBox.SelectedIndex switch
        {
            0 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.CamberNegOffset),
            1 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.CamberPosOffset),
            2 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.ToeNegOffset),
            3 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.ToePosOffset),
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
                GetInstance().WriteMemory(Ptr + TuningOffsets.CamberNegOffset, newValue);                
                GetInstance().WriteMemory(TuningCheatsFh5.Base4, newValue);                
                break;
            }
            case 1:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.CamberPosOffset, newValue);                
                GetInstance().WriteMemory(TuningCheatsFh5.Base4 + 0x4, newValue);                
                break;
            }
            case 2:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.ToeNegOffset, newValue);                
                GetInstance().WriteMemory(TuningCheatsFh5.Base4 + 0x8, newValue);                
                break;
            }
            case 3:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.ToePosOffset, newValue);                
                GetInstance().WriteMemory(TuningCheatsFh5.Base4 + 0xC, newValue);                
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
            0 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.CamberNegOffset),
            1 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.CamberPosOffset),
            2 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.ToeNegOffset),
            3 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.ToePosOffset),
            _ => ValueBox.Value
        };
    }
}