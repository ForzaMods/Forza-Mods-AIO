using System;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

/// <summary>
///     Interaction logic for CustomisationPage.xaml
/// </summary>
public partial class CustomizationPage : Page
{
    public CustomizationPage()
    {
        InitializeComponent();
    }

    /// <summary>
    ///     Glowing paint toggle.
    /// </summary>
    private void GlowingPaintSwitch_Toggled(object sender, RoutedEventArgs e)
    {
        if (((ToggleSwitch)sender).IsOn)
        {
            GlowingPaintNum_ValueChanged(new object(), new RoutedPropertyChangedEventArgs<double?>(GlowingPaintNum.Value, GlowingPaintNum.Value));
            assembly.GlowingPaint(Self_Vehicle_Addrs.CodeCave9);
            return;
        }

        MainWindow.mw.m.WriteBytes(Self_Vehicle_Addrs.GlowingPaintAddr, 
            MainWindow.mw.gvp.Name == "Forza Horizon 4" ? new byte[] { 0x41, 0x0F, 0x11, 0x4A, 0x10 } : new byte[] { 0x0F, 0x11, 0x0A, 0xC6, 0x42, 0xF0, 0x01 });
    }

    /// <summary>
    ///     Change the multiplier for glowing paint.
    /// </summary>
    private void GlowingPaintNum_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        try
        {
            GlowingPaintSlider.Value = Convert.ToDouble(GlowingPaintNum.Value);

            MainWindow.mw.m.WriteMemory((Self_Vehicle_Addrs.CodeCave9 + 0x50).ToString("X"), "Float", GlowingPaintNum.Value.ToString());
            MainWindow.mw.m.WriteMemory((Self_Vehicle_Addrs.CodeCave9 + 0x54).ToString("X"), "Float", GlowingPaintNum.Value.ToString());
            MainWindow.mw.m.WriteMemory((Self_Vehicle_Addrs.CodeCave9 + 0x58).ToString("X"), "Float", GlowingPaintNum.Value.ToString());
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