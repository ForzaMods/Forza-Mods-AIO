using System.Windows;
using System.Windows.Controls;
using Forza_Mods_AIO.Cheats.ForzaHorizon5;
using Forza_Mods_AIO.ViewModels.SubPages.SelfVehicle;
using MahApps.Metro.Controls;
using static Forza_Mods_AIO.Resources.Cheats;
using static Forza_Mods_AIO.Resources.Memory;

namespace Forza_Mods_AIO.Views.SubPages.SelfVehicle;

public partial class PhotoMode
{
    public PhotoMode()
    {
        ViewModel = new PhotoModeViewModel();
        DataContext = this;
        
        InitializeComponent();
    }

    public PhotoModeViewModel ViewModel { get; }
    private static PhotomodeCheats PhotomodeCheatsFh5 => GetClass<PhotomodeCheats>();

    private async void NoClipSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        toggleSwitch.IsEnabled = false;
        if (PhotomodeCheatsFh5.NoClipDetourAddress == 0)
        {
            await PhotomodeCheatsFh5.CheatNoClip();
        }
        toggleSwitch.IsEnabled = true;

        if (PhotomodeCheatsFh5.NoClipDetourAddress == 0) return;
        GetInstance().WriteMemory(PhotomodeCheatsFh5.NoClipDetourAddress + 0x19, toggleSwitch.IsOn ? (byte)1 : (byte)0);
    }

    private async void NoHeightLimitsSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        toggleSwitch.IsEnabled = false;
        if (PhotomodeCheatsFh5.NoHeightLimitDetourAddress == 0)
        {
            await PhotomodeCheatsFh5.CheatNoHeightLimits();
        }
        toggleSwitch.IsEnabled = true;
        
        if (PhotomodeCheatsFh5.NoHeightLimitDetourAddress == 0) return;
        GetInstance().WriteMemory(PhotomodeCheatsFh5.NoHeightLimitDetourAddress + 0x24, toggleSwitch.IsOn ? (byte)1 : (byte)0);
    }

    private async void IncreasedZoomSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        toggleSwitch.IsEnabled = false;
        if (PhotomodeCheatsFh5.IncreasedZoomDetourAddress == 0)
        {
            await PhotomodeCheatsFh5.CheatIncreasedZoom();
        }
        toggleSwitch.IsEnabled = true;
        
        if (PhotomodeCheatsFh5.IncreasedZoomDetourAddress == 0) return;
        GetInstance().WriteMemory(PhotomodeCheatsFh5.IncreasedZoomDetourAddress + 0x21, toggleSwitch.IsOn ? (byte)1 : (byte)0);
    }

    private async void ModifiersScanButton_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.AreScanPromptLimiterUiElementsVisible = false;
        ViewModel.AreScanningLimiterUiElementsVisible = true;

        if (!PhotomodeCheatsFh5.WasModifiersScanSuccessful)
        {
            await PhotomodeCheatsFh5.CheatModifiers();
        }

        if (!PhotomodeCheatsFh5.WasModifiersScanSuccessful)
        {
            ViewModel.AreScanPromptLimiterUiElementsVisible = true;
            ViewModel.AreScanningLimiterUiElementsVisible = false;
            return;
        }

        ValueBox.Value = GetInstance().ReadMemory<int>(PhotomodeCheatsFh5.MainModifiersAddress);

        ViewModel.AreScanningLimiterUiElementsVisible = false;
        ViewModel.AreModifierUiElementsVisible = true;
    }

    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is not ComboBox comboBox || !PhotomodeCheatsFh5.WasModifiersScanSuccessful)
        {
            return;
        }

        ValueBox.Value = comboBox.SelectedIndex switch
        {
            0 => GetInstance().ReadMemory<int>(PhotomodeCheatsFh5.MainModifiersAddress),
            1 => GetInstance().ReadMemory<float>(PhotomodeCheatsFh5.MainModifiersAddress + 0x20),
            2 => GetInstance().ReadMemory<float>(PhotomodeCheatsFh5.MainModifiersAddress + 0x30),
            3 => GetInstance().ReadMemory<float>(PhotomodeCheatsFh5.MainModifiersAddress + 0x38),
            4 => GetInstance().ReadMemory<float>(PhotomodeCheatsFh5.MainModifiersAddress + 0xC),
            5 => GetInstance().ReadMemory<float>(PhotomodeCheatsFh5.SpeedAddress),
            6 => GetInstance().ReadMemory<float>(PhotomodeCheatsFh5.SpeedAddress + 0x4),
            _ => ValueBox.Value
        };
    }

    private void ValueBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        switch (MainComboBox.SelectedIndex)
        {
            case 0:
            {
                GetInstance().WriteMemory(PhotomodeCheatsFh5.MainModifiersAddress, Convert.ToInt32(e.NewValue.GetValueOrDefault()));
                break;
            }
            case 1:
            {
                GetInstance().WriteMemory(PhotomodeCheatsFh5.MainModifiersAddress + 0x20, Convert.ToSingle(e.NewValue.GetValueOrDefault()));
                break;
            }
            case 2:
            {
                GetInstance().WriteMemory(PhotomodeCheatsFh5.MainModifiersAddress + 0x30, Convert.ToSingle(e.NewValue.GetValueOrDefault()));
                break;
            }
            case 3:
            {
                GetInstance().WriteMemory(PhotomodeCheatsFh5.MainModifiersAddress + 0x38, Convert.ToSingle(e.NewValue.GetValueOrDefault()));
                break;
            }
            case 4:
            {
                GetInstance().WriteMemory(PhotomodeCheatsFh5.MainModifiersAddress + 0xC, Convert.ToSingle(e.NewValue.GetValueOrDefault()));
                break;
            }
            case 5:
            {
                GetInstance().WriteMemory(PhotomodeCheatsFh5.SpeedAddress, Convert.ToSingle(e.NewValue.GetValueOrDefault()));
                break;
            }
            case 6:
            {
                GetInstance().WriteMemory(PhotomodeCheatsFh5.SpeedAddress + 0x4, Convert.ToSingle(e.NewValue.GetValueOrDefault()));
                break;
            }
        }
    }
}