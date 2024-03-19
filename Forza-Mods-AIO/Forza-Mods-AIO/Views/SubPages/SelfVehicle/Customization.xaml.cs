using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    
    private async void MainSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        ViewModel.AreMainUiElementsEnabled = false;

        switch (MainComboBox.SelectedIndex)
        {
            case 0:
            {
                if (CustomizationCheatsFh5.PaintDetourAddress == 0)
                {
                    await CustomizationCheatsFh5.CheatGlowingPaint();
                }
                if (CustomizationCheatsFh5.PaintDetourAddress == 0) break;
                ViewModel.GlowingPaintEnabled = toggleSwitch.IsOn;
                var write = toggleSwitch.IsOn ? (byte)1 : (byte)0;
                GetInstance().WriteMemory(CustomizationCheatsFh5.PaintDetourAddress + 0x36, write);
                GetInstance().WriteMemory(CustomizationCheatsFh5.PaintDetourAddress + 0x37, ViewModel.GlowingPaintValue);
                break;
            }
            case 1:
            {
                if (CustomizationCheatsFh5.CleanlinessDetourAddress == 0)
                {
                    await CustomizationCheatsFh5.CheatCleanliness();
                }
        
                if (CustomizationCheatsFh5.CleanlinessDetourAddress == 0) break;
                ViewModel.DirtEnabled = toggleSwitch.IsOn;
                GetInstance().WriteMemory(CustomizationCheatsFh5.CleanlinessDetourAddress + 0x37, toggleSwitch.IsOn ? (byte)1 : (byte)0);
                GetInstance().WriteMemory(CustomizationCheatsFh5.CleanlinessDetourAddress + 0x38, Convert.ToSingle(MainSlider.Value));
                break;
            }
            case 2:
            {
                if (CustomizationCheatsFh5.CleanlinessDetourAddress == 0)
                {
                    await CustomizationCheatsFh5.CheatCleanliness();
                }
        
                if (CustomizationCheatsFh5.CleanlinessDetourAddress == 0) break;                
                ViewModel.MudEnabled = toggleSwitch.IsOn;
                GetInstance().WriteMemory(CustomizationCheatsFh5.CleanlinessDetourAddress + 0x3C, toggleSwitch.IsOn ? (byte)1 : (byte)0);
                GetInstance().WriteMemory(CustomizationCheatsFh5.CleanlinessDetourAddress + 0x3D, Convert.ToSingle(MainSlider.Value));
                break;
            }
            case 3:
            {
                if (CustomizationCheatsFh5.ForceLodDetourAddress == 0)
                {
                    await CustomizationCheatsFh5.CheatForceLod();
                }
        
                if (CustomizationCheatsFh5.ForceLodDetourAddress == 0) break;                
                ViewModel.ForceLodEnabled = toggleSwitch.IsOn;
                GetInstance().WriteMemory(CustomizationCheatsFh5.ForceLodDetourAddress + 0x52, toggleSwitch.IsOn ? (byte)1 : (byte)0);
                GetInstance().WriteMemory(CustomizationCheatsFh5.ForceLodDetourAddress + 0x53, Convert.ToSingle(MainSlider.Value));
                break;
            }
        }

        ViewModel.AreMainUiElementsEnabled = true;
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

    private void MainSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        switch (MainComboBox.SelectedIndex)
        {
            case 0:
            {
                ViewModel.GlowingPaintValue = Convert.ToSingle(e.NewValue);
                if (CustomizationCheatsFh5.PaintDetourAddress == 0) return;
                GetInstance().WriteMemory(CustomizationCheatsFh5.PaintDetourAddress + 0x37, ViewModel.GlowingPaintValue);
                break;
            }
            case 1:
            {
                ViewModel.DirtValue = Convert.ToSingle(e.NewValue);
                if (CustomizationCheatsFh5.CleanlinessDetourAddress == 0) return;
                GetInstance().WriteMemory(CustomizationCheatsFh5.CleanlinessDetourAddress + 0x38, ViewModel.DirtValue);
                break;
            }
            case 2:
            {
                ViewModel.MudValue = Convert.ToSingle(e.NewValue);
                if (CustomizationCheatsFh5.CleanlinessDetourAddress == 0) return;
                GetInstance().WriteMemory(CustomizationCheatsFh5.CleanlinessDetourAddress + 0x3D, ViewModel.MudValue);
                break;
            }
            case 3:
            {
                ViewModel.ForceLodValue = Convert.ToInt32(e.NewValue);
                if (CustomizationCheatsFh5.ForceLodDetourAddress == 0) return;
                GetInstance().WriteMemory(CustomizationCheatsFh5.ForceLodDetourAddress + 0x53, ViewModel.ForceLodValue);
                break;
            }
        }
    }

    private void MainComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is not ComboBox comboBox || MainSwitch == null)
        {
            return;
        }

        MainSwitch.Toggled -= MainSwitch_OnToggled;
        
        MainSwitch.IsOn = comboBox.SelectedIndex switch
        {
            0 => ViewModel.GlowingPaintEnabled,
            1 => ViewModel.DirtEnabled,
            2 => ViewModel.MudEnabled,
            3 => ViewModel.ForceLodEnabled,
            _ => throw new IndexOutOfRangeException()
        };
        
        MainSwitch.Toggled += MainSwitch_OnToggled;
        
        MainSlider.Value = comboBox.SelectedIndex switch
        {
            0 => ViewModel.GlowingPaintValue,
            1 => ViewModel.DirtValue,
            2 => ViewModel.MudValue,
            3 => ViewModel.ForceLodValue,
            _ => throw new IndexOutOfRangeException()
        };
        
        MainSlider.Maximum = comboBox.SelectedIndex switch
        {
            0 => 100,
            1 or 2 => 1,
            3 => 4,
            _ => throw new IndexOutOfRangeException()
        };

        MainSlider.TickPlacement = comboBox.SelectedIndex == 3 ? TickPlacement.BottomRight : TickPlacement.None;
        MainSlider.IsSnapToTickEnabled = comboBox.SelectedIndex == 3;
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