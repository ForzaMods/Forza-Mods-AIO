using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using Forza_Mods_AIO.Resources;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.SelfVehicleAddresses;
using static Forza_Mods_AIO.MainWindow;
using static System.Convert;
using static System.Math;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

/// <summary>
///     Interaction logic for CustomisationPage.xaml
/// </summary>
public partial class CustomizationPage
{
    internal static CustomizationPage Customization { get; private set; } = null!;
    public static readonly Detour GlowingPaintDetour = new(), HeadlightDetour = new(), CleanlinessDetour = new();

    private const string HeadlightBytes = "0F 10 3D 05 00 00 00";
    private const string CleanlinessFh4 = "53 80 3D 3E 00 00 00 01 75 0E 48 8B 1D 2C 00 00 00 48 89 98 7C 8C 00 00 80 3D 26 00 00 00 01 75 0E 48 8B 1D 19 00 00 00 48 89 98 80 8C 00 00 5B F3 0F 10 88 80 8C 00 00";
    private const string CleanlinessFh5 = "53 80 3D 3E 00 00 00 01 75 0E 48 8B 1D 2C 00 00 00 48 89 98 04 8A 00 00 80 3D 26 00 00 00 01 75 0E 48 8B 1D 19 00 00 00 48 89 98 08 8A 00 00 5B F3 0F 10 88 80 8C 00 00";

    private double _mudValue, _dirtValue;
    private bool _mudToggled, _dirtToggled;
    
    public CustomizationPage()
    {
        InitializeComponent();
        Customization = this;
    }

    /// <summary>
    ///     Glowing paint toggle.
    /// </summary>
    private void GlowingPaintSwitch_Toggled(object? sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        if (!GlowingPaintDetour.Setup(sender, GlowingPaintAddr,
                Mw.Gvp.Name.Contains('5') ? "0F590D490000000F110AC642F001" : "0F590D49000000410F114A10",
                Mw.Gvp.Name.Contains('5') ? 7 : 5,
                true, Mw.Gvp.Name.Contains('5') ? (nuint)61 : 63))
        {
            GlowingPaintSwitch.Toggled -= GlowingPaintSwitch_Toggled;
            GlowingPaintSwitch.IsOn = false;
            GlowingPaintSwitch.Toggled += GlowingPaintSwitch_Toggled;
            MessageBox.Show("Failed");
            return;
        }

        var value = ToSingle(GlowingPaintNum.Value);
        GlowingPaintDetour.UpdateVariable(new Vector3(value));
        GlowingPaintDetour.Toggle();
    }

    /// <summary>
    ///     Change the multiplier for glowing paint.
    /// </summary>
    private void GlowingPaintNum_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (GlowingPaintSwitch == null)
        {
            return;
        }
        
        GlowingPaintSlider.Value = ToDouble(GlowingPaintNum.Value);
        
        if (!Mw.Attached)
        {
            return;
        }

        var value = ToSingle(GlowingPaintNum.Value);
        GlowingPaintDetour.UpdateVariable(new Vector3(value));
    }

    /// <summary>
    ///     Slider version of the multiplier changing.
    /// </summary>
    private void GlowingPaintSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        GlowingPaintNum.Value = Round(e.NewValue, 4);
    }

    private void HeadlightColour_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached || HeadlightToggle == null)
        {
            return;
        }
        
        HeadlightDetour.UpdateVariable(new Vector3(
            ToSingle(HeadlightRed.Value),
            ToSingle(HeadlightGreen.Value),
            ToSingle(HeadlightBlue.Value)));
    }

    private void HeadlightColor_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        if (Mw.Gvp.Name.Contains('4') && HeadlightToggle.IsOn)
        {
            MessageBox.Show("This wasnt ported to fh4.");
            HeadlightToggle.Toggled -= HeadlightColor_OnToggled;
            HeadlightToggle.IsOn = false;
            HeadlightToggle.Toggled += HeadlightColor_OnToggled;
            return;
        }
        
        if (!HeadlightDetour.Setup(HeadlightAddr, HeadlightBytes, 13, true, 0, true))
        {
            MessageBox.Show("Failed.");
            HeadlightToggle.Toggled -= HeadlightColor_OnToggled;
            HeadlightToggle.IsOn = false;
            HeadlightToggle.Toggled += HeadlightColor_OnToggled;
            return;
        }
        
        HeadlightDetour.UpdateVariable(new Vector3(
            ToSingle(HeadlightRed.Value),
            ToSingle(HeadlightGreen.Value),
            ToSingle(HeadlightBlue.Value)));
        HeadlightDetour.Toggle();
    }
    
    
    private void CleanlinessNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!Mw.Attached || CleanlinessSwitch == null)
        {
            return;
        }
        
        var selected = ((ComboBoxItem)CleanlinessComboBox.SelectedItem).Content.ToString();
        if (selected == "Dirt")
        {
            _dirtValue = ToDouble(e.NewValue);
        }
        else
        {
            _mudValue = ToDouble(e.NewValue);
        }
        
        CleanlinessSlider.Value = ToDouble(e.NewValue);
        
        if (!CleanlinessDetour.IsSetup)
        {
            return;
        }

        if (selected == "Dirt")
        {
            CleanlinessDetour.UpdateVariable(ToSingle(e.NewValue));
        }
        else
        {
            CleanlinessDetour.UpdateVariable(ToSingle(e.NewValue), 4);
        }
    }


    
    private void CleanlinessSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        var cleanlinessBytes = Mw.Gvp.Name!.Contains('5') ? CleanlinessFh5 : CleanlinessFh4;
        
        if (!CleanlinessDetour.Setup(sender, CleanlinessAddr, cleanlinessBytes, 8, true))
        {
            CleanlinessSwitch.Toggled -= CleanlinessSwitch_OnToggled;
            CleanlinessSwitch.IsOn = false;
            CleanlinessSwitch.Toggled -= CleanlinessSwitch_OnToggled;
            MessageBox.Show("Failed");
            return;
        }
        
        var selected = ((ComboBoxItem)CleanlinessComboBox.SelectedItem).Content.ToString();
        switch (selected)
        {
            case "Dirt":
            {
                CleanlinessDetour.UpdateVariable(ToSingle(CleanlinessNum.Value));
                CleanlinessDetour.UpdateVariable(CleanlinessSwitch.IsOn ? (byte)1 : (byte)0, 9);
                _dirtToggled = CleanlinessDetour.ReadVariable<byte>(9) == 1;
                break;
            }
            default:
            {
                CleanlinessDetour.UpdateVariable(ToSingle(CleanlinessNum.Value), 4);
                CleanlinessDetour.UpdateVariable(CleanlinessSwitch.IsOn ? (byte)1 : (byte)0, 8);
                _mudToggled = CleanlinessDetour.ReadVariable<byte>(8) == 1;
                break;
            }
        }
    }

    private void CleanlinessSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (!Mw.Attached)
        {
            return;
        }
        
        CleanlinessNum.Value = Round(e.NewValue, 5);
    }

    private void CleanlinessComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (!Mw.Attached || CleanlinessSwitch == null)
        {
            return;
        }

        var selected = ((ComboBoxItem)CleanlinessComboBox.SelectedItem).Content.ToString();
        CleanlinessSwitch.Content = $"{selected} Level";

        switch (selected)
        {
            case "Dirt":
            {
                CleanlinessNum.Value = _dirtValue;
                CleanlinessSlider.Value = _dirtValue;
                CleanlinessSwitch.IsOn = _dirtToggled;
                break;
            }
            
            case "Mud":
            {
                CleanlinessNum.Value = _mudValue;
                CleanlinessSlider.Value = _mudValue;                
                CleanlinessSwitch.IsOn = _mudToggled;
                break;
            }
        }
    }
}