using System.Windows;
using System.Windows.Controls;
using Forza_Mods_AIO.Cheats.ForzaHorizon5;
using static Forza_Mods_AIO.Resources.Memory;

namespace Forza_Mods_AIO.Views.SubPages.Tuning;

public partial class Gearing
{
    public Gearing()
    {
        InitializeComponent();
    }

    private static TuningCheats TuningCheatsFh5 => Forza_Mods_AIO.Resources.Cheats.GetClass<TuningCheats>();
    private static CarCheats CarCheatsFh5 => Forza_Mods_AIO.Resources.Cheats.GetClass<CarCheats>();
    private static UIntPtr Ptr => GetInstance()
        .ReadMemory<UIntPtr>(CarCheatsFh5.LocalPlayerHookDetourAddress + CarCheats.LocalPlayerOffset);

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
                GetInstance().WriteMemory(Ptr + TuningOffsets.FinalDriveOffset, newValue);                
                break;
            }
            case 1:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.ReverseGearOffset, newValue);                
                break;
            }
            case 2:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.FifthGearOffset, newValue);                
                break;
            }
            case 3:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.SecondGearOffset, newValue);                
                break;
            }
            case 4:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.ThirdGearOffset, newValue);                
                break;
            }
            case 5:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.FourthGearOffset, newValue);                
                break;
            }
            case 6:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.FifthGearOffset, newValue);                
                break;
            }
            case 7:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.SixthGearOffset, newValue);                
                break;
            }
            case 8:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.SeventhGearOffset, newValue);                
                break;
            }
            case 9:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.EighthGearOffset, newValue);                
                break;
            }
            case 10:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.NinthGearOffset, newValue);                
                break;
            }
            case 11:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.TenthGearOffset, newValue);                
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
            0 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.FinalDriveOffset),
            1 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.ReverseGearOffset),
            2 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.FirstGearOffset),
            3 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.SecondGearOffset),
            4 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.ThirdGearOffset),
            5 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.FourthGearOffset),
            6 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.FifthGearOffset),
            7 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.SixthGearOffset),
            8 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.SeventhGearOffset),
            9 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.EighthGearOffset),
            10 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.NinthGearOffset),
            11 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.TenthGearOffset),
            _ => ValueBox.Value
        };
    }
}