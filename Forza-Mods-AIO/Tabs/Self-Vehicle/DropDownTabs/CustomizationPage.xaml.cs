using System;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using Forza_Mods_AIO.Resources;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.SelfVehicleAddresses;
using static Forza_Mods_AIO.MainWindow;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

/// <summary>
///     Interaction logic for CustomisationPage.xaml
/// </summary>
public partial class CustomizationPage : Page
{
    internal static CustomizationPage _CustomizationPage;
    public static readonly Detour GlowingPaintDetour = new();
    
    public CustomizationPage()
    {
        InitializeComponent();
        _CustomizationPage = this;
    }

    /// <summary>
    ///     Glowing paint toggle.
    /// </summary>
    private void GlowingPaintSwitch_Toggled(object? sender, RoutedEventArgs e)
    {
        if (!GlowingPaintDetour.Setup(sender, GlowingPaintAddr,
                Mw.Gvp.Name.Contains('5') ? "0F590D490000000F110AC642F001" : "0F590D49000000410F114A10",
                Mw.Gvp.Name.Contains('5') ? 7 : 5,
                true, 63))
        {
            GlowingPaintSwitch.Toggled -= GlowingPaintSwitch_Toggled;
            GlowingPaintSwitch.IsOn = false;
            GlowingPaintSwitch.Toggled += GlowingPaintSwitch_Toggled;
            MessageBox.Show("Failed");
            return;
        }

        if (GlowingPaintDetour.IsHooked)
        {
            GlowingPaintNum_ValueChanged(new object(), new RoutedPropertyChangedEventArgs<double?>(GlowingPaintNum.Value, GlowingPaintNum.Value));
        }
        
        GlowingPaintDetour.Toggle();
    }

    /// <summary>
    ///     Change the multiplier for glowing paint.
    /// </summary>
    private void GlowingPaintNum_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        try
        {
            GlowingPaintSlider.Value = Convert.ToDouble(GlowingPaintNum.Value);
            var value = Convert.ToSingle(GlowingPaintNum.Value);
            Vector3 glowingPaintMultiplier = new() { X = value, Y = value, Z = value };
            GlowingPaintDetour.UpdateVariable(glowingPaintMultiplier);
        }
        catch
        {
            // ignored
        }
    }

    /// <summary>
    ///     Slider version of the multiplier changing.
    /// </summary>
    private void GlowingPaintSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        GlowingPaintNum.Value = Math.Round(e.NewValue, 4);
    }
}