using System.Linq;
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
    public static readonly Detour ForceLodDetour = new(), LodCmpDetour = new();

    private const string HeadlightBytes = "0F 10 3D 05 00 00 00";
    private const string CleanlinessFh4 = "53 80 3D 3E 00 00 00 01 75 0E 48 8B 1D 2C 00 00 00 48 89 98 7C 8C 00 00 80 3D 26 00 00 00 01 75 0E 48 8B 1D 19 00 00 00 48 89 98 80 8C 00 00 5B F3 0F 10 88 80 8C 00 00";
    private const string CleanlinessFh5 = "53 80 3D 3E 00 00 00 01 75 0E 48 8B 1D 2C 00 00 00 48 89 98 04 8A 00 00 80 3D 26 00 00 00 01 75 0E 48 8B 1D 19 00 00 00 48 89 98 08 8A 00 00 5B F3 0F 10 88 08 8A 00 00";

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
    private void GlowingPaintSwitch_Toggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached || Mw.Gvp.Name == null)
        {
            return;
        }

        const string fh5 = "0F 59 0D 49 00 00 00 0F 11 0A C6 42 F0 01";
        const string fh5Orig = "0F 11 0A C6 42 F0 01";

        const string fh4 = "0F 59 0D 49 00 00 00 41 0F 11 4A 10";
        const string fh4Orig = "41 0F 11 4A 10";

        var isFh5 = Mw.Gvp.Name.Contains('5');
        var orig = isFh5 ? fh5Orig : fh4Orig;
        var detoured = isFh5 ? fh5 : fh4;
        var replace = isFh5 ? 7 : 5;
        var offset = isFh5 ? (uint)61 : 63;
        
        if (!GlowingPaintDetour.Setup(sender, GlowingPaintAddr, orig, detoured, replace, true, offset))
        {
            Detour.FailedHandler(sender, GlowingPaintSwitch_Toggled);
            return;
        }

        GlowingPaintDetour.UpdateVariable(new Vector3(ToSingle(GlowingPaintNum.Value)));
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

        GlowingPaintDetour.UpdateVariable(new Vector3(ToSingle(GlowingPaintNum.Value)));
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
            Detour.FailedHandler(sender, HeadlightColor_OnToggled, true);
            return;
        }

        const string orig = "0F10 7B 50 F3 44 0F10 83 84000000";
        if (!HeadlightDetour.Setup(HeadlightToggle, HeadlightAddr, orig, HeadlightBytes, 13, true, 0, true))
        {
            Detour.FailedHandler(sender, HeadlightColor_OnToggled);
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

        const string originalBytesFh5 = "F3 0F 10 88 0C 8A 00 00";
        const string originalBytesFh4 = "F3 0F 10 88 80 8C 00 00";
        var cleanlinessBytes = Mw.Gvp.Name!.Contains('5') ? CleanlinessFh5 : CleanlinessFh4;
        var original = Mw.Gvp.Name!.Contains('5') ? originalBytesFh5 : originalBytesFh4;
        
        if (!CleanlinessDetour.Setup(sender, CleanlinessAddr, original, cleanlinessBytes, 8, true))
        {
            Detour.FailedHandler(sender, HeadlightColor_OnToggled);
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

    private void ForceLodBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ForceLodSlider == null)
        {
            return;
        }
        
        ForceLodSlider.Value = ForceLodBox.SelectedIndex;
        SetLod();
    }

    private void SetLod()
    {
        switch (ForceLodBox.SelectedIndex)
        {
            case 0:
                ForceLodDetour.UpdateVariable((byte)0xFF, 8);
                break;
            case 1:
            case 2:
            case 3:
            case 4:
                ForceLodDetour.UpdateVariable(ToByte(ForceLodSlider.Value), 8);
                break;
        }
    }

    private void ForceLodSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (ForceLodSwitch == null)
        {
            return;
        }
        
        const string cmpOrig = "41 0F10 B7 D0070000";
        const string cmpDetoured = "51 48 8B 48 30 48 89 0D 06 00 00 00 59";
        if (!LodCmpDetour.Setup(ForceLodSwitch, LodCmp, cmpOrig, cmpDetoured, 8, true, 0, true))
        {
            Detour.FailedHandler(sender, ForceLodSwitch_OnToggled);
            return;
        }

        const string forceOrig = "40 88 B7 06010000";
        const string forceDetoured = "50 48 8B 05 25 00 00 00 80 38 00 74 0C 80 38 01 74 07 80 38 11 74 02 EB 07 40 8A 35 15 00 00 00 58 40 88 B7 06 01 00 00";
        if (!ForceLodDetour.Setup(ForceLodSwitch, ForceLod, forceOrig, forceDetoured, 7, true))
        {
            Detour.FailedHandler(sender, ForceLodSwitch_OnToggled);
            return;
        }

        ForceLodDetour.UpdateVariable(LodCmpDetour.VariableAddress);
        SetLod();
        ForceLodDetour.Toggle();
    }

    private void ForceLodSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        ForceLodBox.SelectedIndex = ToInt32(e.NewValue);
    }
}