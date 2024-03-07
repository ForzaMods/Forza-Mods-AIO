using System.Numerics;
using System.Windows;
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
        
        if (CustomizationCheatsFh5.PaintAddress == 0)
        {
            await CustomizationCheatsFh5.CheatGlowingPaint();
        }

        ViewModel.ArePaintUiElementsEnabled = true;
        
        if (CustomizationCheatsFh5.PaintAddress == 0) return;
        var write = toggleSwitch.IsOn ? (byte)1 : (byte)0;
        GetInstance().WriteMemory(CustomizationCheatsFh5.PaintDetourAddress + 0x36, write);
        GetInstance().WriteMemory(CustomizationCheatsFh5.PaintDetourAddress + 0x37, ViewModel.GlowingPaintValue);
    }

    private void PaintSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (CustomizationCheatsFh5.PaintAddress == 0) return;
        GetInstance().WriteMemory(CustomizationCheatsFh5.PaintDetourAddress + 0x37, ViewModel.GlowingPaintValue);
    }

    private async void HeadlightSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        ViewModel.AreHeadlightUiElementsEnabled = false;
        
        if (CustomizationCheatsFh5.HeadlightColourAddress == 0)
        {
            await CustomizationCheatsFh5.CheatHeadlightColour();
        }

        ViewModel.AreHeadlightUiElementsEnabled = true;
        
        if (CustomizationCheatsFh5.HeadlightColourAddress == 0) return;
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

        if (CustomizationCheatsFh5.HeadlightColourAddress == 0) return;
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
}