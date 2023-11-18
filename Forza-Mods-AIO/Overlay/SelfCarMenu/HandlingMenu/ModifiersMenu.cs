using System;
using System.Collections.Generic;
using System.Windows;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.HandlingMenu;

public abstract class ModifiersMenu
{
    public static void InitiateSubMenu()
    {
        _gravityValue.ValueChangedHandler += GravityValueChanged;
        _gravityToggle.ValueChangedHandler += GravityToggleChanged;
        _accelerationValue.ValueChangedHandler += AccelerationValueChanged;
        _accelerationToggle.ValueChangedHandler += AccelerationToggleChanged;
    }
    
    // Gravity menu options
    static MenuOption _gravityValue = new("Value", OptionType.Float, 0f);
    static MenuOption _gravityPull = new("Pull", OptionType.Button, () => 
    {
        Tabs.Self_Vehicle.DropDownTabs.HandlingPage.Shp.Dispatcher.Invoke(delegate ()
        {
            Tabs.Self_Vehicle.DropDownTabs.HandlingPage.Shp.PullButton_Click(Tabs.Self_Vehicle.DropDownTabs.HandlingPage.Shp.GravityPullButton, new RoutedEventArgs());
            _gravityValue.Value = (float)Tabs.Self_Vehicle.DropDownTabs.HandlingPage.Shp.GravityValueNum.Value;
        });
    });
    static MenuOption _gravityToggle = new("Enable", OptionType.Bool, false);

    // Acceleration menu options
    static MenuOption _accelerationValue = new("Value", OptionType.Float, 0f);
    static MenuOption _accelerationPull = new("Pull", OptionType.Button, () => 
    {
        Tabs.Self_Vehicle.DropDownTabs.HandlingPage.Shp.Dispatcher.Invoke(delegate ()
        {
            Tabs.Self_Vehicle.DropDownTabs.HandlingPage.Shp.PullButton_Click(Tabs.Self_Vehicle.DropDownTabs.HandlingPage.Shp.AccelPullButton, new RoutedEventArgs());
            _accelerationValue.Value = (float)Tabs.Self_Vehicle.DropDownTabs.HandlingPage.Shp.AccelValueNum.Value;
        });
    });
    static MenuOption _accelerationToggle = new("Enable", OptionType.Bool, false);
    
    public static readonly List<MenuOption> ModifiersOptions = new()
    {
        new ("Gravity", OptionType.SubHeader),
        _gravityValue,
        _gravityPull,
        _gravityToggle,
        new ("Acceleration", OptionType.SubHeader),
        _accelerationValue,
        _accelerationPull,
        _accelerationToggle
    };
    
    private static void GravityValueChanged(object s, EventArgs e)
    {
        Tabs.Self_Vehicle.DropDownTabs.HandlingPage.Shp.Dispatcher.Invoke(delegate ()
        {
            if ((float)s.GetType().GetProperty("Value").GetValue(s) > Tabs.Self_Vehicle.DropDownTabs.HandlingPage.Shp.GravityValueNum.Maximum)
                _gravityValue.Value = (float)Tabs.Self_Vehicle.DropDownTabs.HandlingPage.Shp.GravityValueNum.Maximum;
            else if ((float)s.GetType().GetProperty("Value").GetValue(s) < Tabs.Self_Vehicle.DropDownTabs.HandlingPage.Shp.GravityValueNum.Minimum)
                _gravityValue.Value = (float)Tabs.Self_Vehicle.DropDownTabs.HandlingPage.Shp.GravityValueNum.Minimum;
            else
                Tabs.Self_Vehicle.DropDownTabs.HandlingPage.Shp.GravityValueNum.Value = (float)Math.Round((float)s.GetType().GetProperty("Value").GetValue(s), 1);
        });
    }
    private static void GravityToggleChanged(object s, EventArgs e)
    {            
        Tabs.Self_Vehicle.DropDownTabs.HandlingPage.Shp.Dispatcher.Invoke(delegate ()
        {
            if ((bool)_gravityToggle.Value)
                Tabs.Self_Vehicle.DropDownTabs.HandlingPage.Shp.GravityValueNum.Value = (float)_gravityValue.Value;
            Tabs.Self_Vehicle.DropDownTabs.HandlingPage.Shp.GravitySetSwitch.IsOn = (bool)s.GetType().GetProperty("Value").GetValue(s);
        });;
    }
    private static void AccelerationValueChanged(object s, EventArgs e)
    {            
        Tabs.Self_Vehicle.DropDownTabs.HandlingPage.Shp.Dispatcher.Invoke(delegate ()
        {
            if ((float)s.GetType().GetProperty("Value").GetValue(s) > Tabs.Self_Vehicle.DropDownTabs.HandlingPage.Shp.AccelValueNum.Maximum)
                _accelerationValue.Value = (float)Tabs.Self_Vehicle.DropDownTabs.HandlingPage.Shp.AccelValueNum.Maximum;
            else if ((float)s.GetType().GetProperty("Value").GetValue(s) < Tabs.Self_Vehicle.DropDownTabs.HandlingPage.Shp.AccelValueNum.Minimum)
                _accelerationValue.Value = (float)Tabs.Self_Vehicle.DropDownTabs.HandlingPage.Shp.AccelValueNum.Minimum;
            else
                Tabs.Self_Vehicle.DropDownTabs.HandlingPage.Shp.AccelValueNum.Value = (float)Math.Round((float)s.GetType().GetProperty("Value").GetValue(s), 1);
        });
    }
    private static void AccelerationToggleChanged(object s, EventArgs e)
    {            
        Tabs.Self_Vehicle.DropDownTabs.HandlingPage.Shp.Dispatcher.Invoke(delegate ()
        {
            if ((bool)_accelerationToggle.Value)
                Tabs.Self_Vehicle.DropDownTabs.HandlingPage.Shp.AccelValueNum.Value = (float)_accelerationValue.Value;
            Tabs.Self_Vehicle.DropDownTabs.HandlingPage.Shp.AccelSetSwitch.IsOn = (bool)s.GetType().GetProperty("Value").GetValue(s);
        });
    }
}