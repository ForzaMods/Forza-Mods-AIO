using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Forza_Mods_AIO.Cheats.ForzaHorizon5;
using Forza_Mods_AIO.ViewModels.SubPages.SelfVehicle;
using MahApps.Metro.Controls;
using static Forza_Mods_AIO.Resources.Cheats;
using static Forza_Mods_AIO.Resources.Memory;

namespace Forza_Mods_AIO.Views.SubPages.SelfVehicle;

public partial class Customization
{
    public Customization()
    {
        ViewModel = new CustomizationViewModel();
        DataContext = this;
        
        InitializeComponent();
    }
    
    public CustomizationViewModel ViewModel { get; }
    private static CustomizationCheats CustomizationCheatsFh5 => GetClass<CustomizationCheats>();
    
    private async void PaintSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        ViewModel.ArePaintUiElementsEnabled = false;
        
        if (CustomizationCheatsFh5.PaintDetourAddress == 0)
        {
            await CustomizationCheatsFh5.CheatGlowingPaint();
        }

        ViewModel.ArePaintUiElementsEnabled = true;
        
        if (CustomizationCheatsFh5.PaintDetourAddress == 0) return;
        var write = toggleSwitch.IsOn ? (byte)1 : (byte)0;
        GetInstance().WriteMemory(CustomizationCheatsFh5.PaintDetourAddress + 0x36, write);
        GetInstance().WriteMemory(CustomizationCheatsFh5.PaintDetourAddress + 0x37, ViewModel.GlowingPaintValue);
    }

    private void PaintSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (CustomizationCheatsFh5.PaintDetourAddress == 0) return;
        GetInstance().WriteMemory(CustomizationCheatsFh5.PaintDetourAddress + 0x37, ViewModel.GlowingPaintValue);
    }

    private async void HeadlightSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        ViewModel.AreHeadlightUiElementsEnabled = false;
        
        if (CustomizationCheatsFh5.HeadlightColourDetourAddress == 0)
        {
            await CustomizationCheatsFh5.CheatHeadlightColour();
        }

        ViewModel.AreHeadlightUiElementsEnabled = true;
        
        if (CustomizationCheatsFh5.HeadlightColourDetourAddress == 0) return;
        var toggled = toggleSwitch.IsOn ? (byte)1 : (byte)0;
        var write = ConvertUiColorToGameValues((Color)ColorPicker.SelectedColor!);
        GetInstance().WriteMemory(CustomizationCheatsFh5.HeadlightColourDetourAddress + 0x22, toggled);
        GetInstance().WriteMemory(CustomizationCheatsFh5.HeadlightColourDetourAddress + 0x23, write);
    }
    
    private void ColorPickerBase_OnSelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
    {
        if (ColorPicker == null)
        {
            return;
        }

        if (CustomizationCheatsFh5.HeadlightColourDetourAddress == 0) return;
        var write = ConvertUiColorToGameValues(e.NewValue.GetValueOrDefault());
        GetInstance().WriteMemory(CustomizationCheatsFh5.HeadlightColourDetourAddress + 0x23, write);
    }

    private static Vector3 ConvertUiColorToGameValues(Color uiColor)
    {
        var alpha = uiColor.A / 255f;
        var red = uiColor.R / 255f * alpha;
        var green = uiColor.G / 255f * alpha;
        var blue = uiColor.B / 255f * alpha;
        return new Vector3(red, green, blue);        
    }

    private async void CleanlinessSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        ViewModel.AreCleanlinessUiElementsEnabled = false;
        
        if (CustomizationCheatsFh5.CleanlinessDetourAddress == 0)
        {
            await CustomizationCheatsFh5.CheatCleanliness();
        }
        
        ViewModel.AreCleanlinessUiElementsEnabled = true;
        if (CustomizationCheatsFh5.CleanlinessDetourAddress == 0) return;

        switch (CleanlinessMode.SelectedIndex)
        {
            case 0:
            {
                ViewModel.DirtEnabled = toggleSwitch.IsOn;
                GetInstance().WriteMemory(CustomizationCheatsFh5.CleanlinessDetourAddress + 0x37, toggleSwitch.IsOn ? (byte)1 : (byte)0);
                GetInstance().WriteMemory(CustomizationCheatsFh5.CleanlinessDetourAddress + 0x38, Convert.ToSingle(CleanlinessSlider.Value));
                break;
            }
            default:
            {
                ViewModel.MudEnabled = toggleSwitch.IsOn;
                GetInstance().WriteMemory(CustomizationCheatsFh5.CleanlinessDetourAddress + 0x3C, toggleSwitch.IsOn ? (byte)1 : (byte)0);
                GetInstance().WriteMemory(CustomizationCheatsFh5.CleanlinessDetourAddress + 0x3D, Convert.ToSingle(CleanlinessSlider.Value));
                break;
            }
        }
        
    }

    private void CleanlinessSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        switch (CleanlinessMode.SelectedIndex)
        {
            case 0:
            {
                ViewModel.DirtValue = Convert.ToSingle(e.NewValue);
                if (CustomizationCheatsFh5.CleanlinessDetourAddress == 0) return;
                GetInstance().WriteMemory(CustomizationCheatsFh5.CleanlinessDetourAddress + 0x38, Convert.ToSingle(CleanlinessSlider.Value));
                break;
            }
            default:
            {
                ViewModel.MudValue = Convert.ToSingle(e.NewValue);
                if (CustomizationCheatsFh5.CleanlinessDetourAddress == 0) return;
                GetInstance().WriteMemory(CustomizationCheatsFh5.CleanlinessDetourAddress + 0x3D, Convert.ToSingle(CleanlinessSlider.Value));
                break;
            }
        }
    }

    private void CleanlinessMode_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is not ComboBox comboBox || CleanlinessSwitch == null)
        {
            return;
        }

        CleanlinessSwitch.Toggled -= CleanlinessSwitch_OnToggled;
        
        CleanlinessSwitch.IsOn = comboBox.SelectedIndex switch
        {
            0 => ViewModel.DirtEnabled,
            _ => ViewModel.MudEnabled
        };
        
        CleanlinessSwitch.Toggled += CleanlinessSwitch_OnToggled;
        
        CleanlinessSlider.Value = comboBox.SelectedIndex switch
        {
            0 => ViewModel.DirtValue,
            _ => ViewModel.MudValue
        };
    }

    private void LodSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (CustomizationCheatsFh5.ForceLodDetourAddress == 0) return;
        GetInstance().WriteMemory(CustomizationCheatsFh5.ForceLodDetourAddress + 0x53, Convert.ToInt32(e.NewValue));
    }

    private async void LodSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }
        
        ViewModel.AreForceLodUiElementsEnabled = false;
        if (CustomizationCheatsFh5.ForceLodDetourAddress == 0)
        {
            await CustomizationCheatsFh5.CheatForceLod();
        }
        ViewModel.AreForceLodUiElementsEnabled = true;
        
        if (CustomizationCheatsFh5.ForceLodDetourAddress == 0) return;
        GetInstance().WriteMemory(CustomizationCheatsFh5.ForceLodDetourAddress + 0x52, toggleSwitch.IsOn ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(CustomizationCheatsFh5.ForceLodDetourAddress + 0x53, Convert.ToInt32(LodSlider.Value));
    }


    private async void BackfireSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }
        
        ViewModel.AreBackfireUiElementsEnabled = false;
        if (CustomizationCheatsFh5.BackfireTimeDetourAddress == 0)
        {
            await CustomizationCheatsFh5.CheatBackfireTime();
        }
        ViewModel.AreBackfireUiElementsEnabled = true;
        
        if (CustomizationCheatsFh5.BackfireTimeDetourAddress == 0) return;
        GetInstance().WriteMemory(CustomizationCheatsFh5.BackfireTimeDetourAddress + 0x26, toggleSwitch.IsOn ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(CustomizationCheatsFh5.BackfireTimeDetourAddress + 0x27, Convert.ToSingle(MinBackfire.Value));
        GetInstance().WriteMemory(CustomizationCheatsFh5.BackfireTimeDetourAddress + 0x2B, Convert.ToSingle(MaxBackfire.Value));
    }

    private void MinBackfire_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (CustomizationCheatsFh5.BackfireTimeDetourAddress == 0) return;
        GetInstance().WriteMemory(CustomizationCheatsFh5.BackfireTimeDetourAddress + 0x27, Convert.ToSingle(e.NewValue));
    }

    private void MaxBackfire_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (CustomizationCheatsFh5.BackfireTimeDetourAddress == 0) return;
        GetInstance().WriteMemory(CustomizationCheatsFh5.BackfireTimeDetourAddress + 0x2B, Convert.ToSingle(e.NewValue));
    }
}