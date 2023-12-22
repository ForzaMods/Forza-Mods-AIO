using System;
using System.Collections.Generic;
using System.Windows;
using Forza_Mods_AIO.Overlay.Options;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs.HandlingPage;

namespace Forza_Mods_AIO.Overlay.Menus.SelfCarMenu.HandlingMenu;

public abstract class ModifiersMenu
{
    public static void InitiateSubMenu()
    {
        GravityValue.ValueChangedEventHandler += GravityValueChanged;
        GravityToggle.ToggledEventHandler += GravityToggleChanged;
        AccelerationValue.ValueChangedEventHandler += AccelerationValueChanged;
        AccelerationToggle.ToggledEventHandler += AccelerationToggleChanged;
    }
    
    private static readonly FloatOption GravityValue = new("Value", 0f, Shp.GravityValueNum.Minimum, Shp.GravityValueNum.Maximum);
    private static readonly ToggleOption GravityToggle = new("Enable", false);
    private static readonly ButtonOption GravityPull = new("Pull", () => 
    {
        Shp.PullButton_Click(Shp.GravityPullButton, new RoutedEventArgs());
        GravityValue.Value = Convert.ToSingle(Shp.GravityValueNum.Value);
    });

    private static readonly FloatOption AccelerationValue = new("Value", 0f, Shp.AccelValueNum.Minimum, Shp.AccelValueNum.Maximum);
    private static readonly ToggleOption AccelerationToggle = new("Enable", false);
    private static readonly ButtonOption AccelerationPull = new("Pull", () => 
    {
        Shp.PullButton_Click(Shp.AccelPullButton, new RoutedEventArgs());
        AccelerationValue.Value = Convert.ToSingle(Shp.AccelValueNum.Value);
    });

    
    public static readonly List<MenuOption> ModifiersOptions = new()
    {
        new SubHeaderOption("Gravity"),
        GravityValue,
        GravityToggle,
        GravityPull,
        new SubHeaderOption("Acceleration"),
        AccelerationValue,
        AccelerationToggle,
        AccelerationPull
    };
    
    private static void GravityValueChanged(object s, EventArgs e)
    {
        Shp.GravityValueNum.Value = Convert.ToSingle(Math.Round(GravityValue.Value, 1));
    }
    private static void GravityToggleChanged(object s, EventArgs e)
    {            
        if (GravityToggle.IsOn)
        {
            Shp.GravityValueNum.Value = GravityValue.Value;
        }

        Shp.GravitySetSwitch.IsOn = GravityToggle.IsOn;
    }
    private static void AccelerationValueChanged(object s, EventArgs e)
    {            
        Shp.AccelValueNum.Value = Convert.ToSingle(Math.Round(AccelerationValue.Value, 1));
    }
    private static void AccelerationToggleChanged(object s, EventArgs e)
    {            
        if (AccelerationToggle.IsOn)
        {
            Shp.AccelValueNum.Value = AccelerationValue.Value;
        }

        Shp.AccelSetSwitch.IsOn = AccelerationToggle.IsOn;
    }
}