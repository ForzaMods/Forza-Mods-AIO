using System.Windows;
using System.Windows.Controls;
using Forza_Mods_AIO.Cheats.ForzaHorizon5;
using static Forza_Mods_AIO.Resources.Memory;

namespace Forza_Mods_AIO.Views.SubPages.Tuning;

public partial class Tires
{
    public Tires()
    {
        InitializeComponent();
    }
    
    private static TuningCheats TuningCheatsFh5 => Forza_Mods_AIO.Resources.Cheats.GetClass<TuningCheats>();
    private static CarCheats CarCheatsFh5 => Forza_Mods_AIO.Resources.Cheats.GetClass<CarCheats>();
    private static UIntPtr Ptr => GetInstance()
        .ReadMemory<UIntPtr>(CarCheatsFh5.LocalPlayerHookDetourAddress + CarCheats.LocalPlayerOffset);

    private bool _codeChange;

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        UpdateValue();
    }

    private void ValueBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!TuningCheatsFh5.WasScanSuccessful || _codeChange)
        {
            return;
        }
        
        var divider = UnitBox.SelectedIndex == 0 ? 1f : 14.503773773f;
        var newValue = Convert.ToSingle(e.NewValue.GetValueOrDefault()) / divider;
        switch (ComboBox.SelectedIndex)
        {
            case 0:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.FrontLeftTirePressureOffset, newValue);                
                break;
            }
            case 1:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.FrontRightTirePressureOffset, newValue);                
                break;
            }
            case 2:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.RearLeftTirePressureOffset, newValue);                
                break;
            }
            case 3:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.RearRightTirePressureOffset, newValue);                
                break;
            }
        }
    }

    private void ComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        UpdateValue();
    }

    private void UnitBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        UpdateValue();
    }

    private void UpdateValue()
    {
        if (ValueBox == null || !TuningCheatsFh5.WasScanSuccessful)
        {
            return;
        }

        _codeChange = true;
        var divider = UnitBox.SelectedIndex == 0 ? 1f : 14.503773773f;
        ValueBox.Value = ComboBox.SelectedIndex switch
        {
            0 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.FrontLeftTirePressureOffset) / divider,
            1 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.FrontRightTirePressureOffset) / divider,
            2 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.RearLeftTirePressureOffset) / divider,
            3 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.RearRightTirePressureOffset) / divider,
            _ => ValueBox.Value
        };
        _codeChange = false;
    }
}