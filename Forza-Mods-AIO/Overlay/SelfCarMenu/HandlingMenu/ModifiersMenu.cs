using System;
using System.Collections.Generic;
using System.Windows;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.HandlingMenu;

public class ModifiersMenu
{
    public void InitiateSubMenu()
    {
        GravityValue.ValueChangedHandler += new EventHandler(GravityValueChanged);
        GravityToggle.ValueChangedHandler += new EventHandler(GravityToggleChanged);
        AccelerationValue.ValueChangedHandler += new EventHandler(AccelerationValueChanged);
        AccelerationToggle.ValueChangedHandler += new EventHandler(AccelerationToggleChanged);
    }
    
    // Gravity menu options
    static Overlay.MenuOption GravityValue = new Overlay.MenuOption("Value", "Float", 0);
    static Overlay.MenuOption GravityPull = new Overlay.MenuOption("Pull", "Button", new Action(() => 
    {
        Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.Dispatcher.Invoke(delegate ()
        {
            Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.PullButton_Click(Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.GravityPullButton, new RoutedEventArgs());
            GravityValue.Value = (float)Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.GravityValueNum.Value;
        });
    }));
    static Overlay.MenuOption GravityToggle = new Overlay.MenuOption("Enable", "Bool", false);

    // Acceleration menu options
    static Overlay.MenuOption AccelerationValue = new Overlay.MenuOption("Value", "Float", 0);
    static Overlay.MenuOption AccelerationPull = new Overlay.MenuOption("Pull", "Button", new Action(() => 
    {
        Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.Dispatcher.Invoke(delegate ()
        {
            Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.PullButton_Click(Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.AccelerationPullButton, new RoutedEventArgs());
            AccelerationValue.Value = (float)Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.AccelerationValueNum.Value;
        });
    }));
    static Overlay.MenuOption AccelerationToggle = new Overlay.MenuOption("Enable", "Bool", false);
    
    public static List<Overlay.MenuOption> GravityOptions = new List<Overlay.MenuOption>()
    {
        GravityValue,
        GravityPull,
        GravityToggle
    };
    public static List<Overlay.MenuOption> AccelerationOptions = new List<Overlay.MenuOption>()
    {
        AccelerationValue,
        AccelerationPull,
        AccelerationToggle
    };
    
    void GravityValueChanged(object s, EventArgs e)
    {
        Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.Dispatcher.Invoke(delegate ()
        {
            if ((float)s.GetType().GetProperty("Value").GetValue(s) > Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.GravityValueNum.Maximum)
                GravityValue.Value = (float)Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.GravityValueNum.Maximum;
            else if ((float)s.GetType().GetProperty("Value").GetValue(s) < Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.GravityValueNum.Minimum)
                GravityValue.Value = (float)Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.GravityValueNum.Minimum;
            else
                Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.GravityValueNum.Value = (float)s.GetType().GetProperty("Value").GetValue(s);
        });
    }
    void GravityToggleChanged(object s, EventArgs e)
    {            
        Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.Dispatcher.Invoke(delegate ()
        {
            if ((bool)GravityToggle.Value)
                Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.GravityValueNum.Value = (float)GravityValue.Value;
            Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.GravitySetSwitch.IsOn = (bool)s.GetType().GetProperty("Value").GetValue(s);
        });;
    }
    void AccelerationValueChanged(object s, EventArgs e)
    {            
        Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.Dispatcher.Invoke(delegate ()
        {
            if ((float)s.GetType().GetProperty("Value").GetValue(s) > Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.AccelerationValueNum.Maximum)
                AccelerationValue.Value = (float)Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.AccelerationValueNum.Maximum;
            else if ((float)s.GetType().GetProperty("Value").GetValue(s) < Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.AccelerationValueNum.Minimum)
                AccelerationValue.Value = (float)Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.AccelerationValueNum.Minimum;
            else
                Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.AccelerationValueNum.Value = (float)s.GetType().GetProperty("Value").GetValue(s);
        });
    }
    void AccelerationToggleChanged(object s, EventArgs e)
    {            
        Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.Dispatcher.Invoke(delegate ()
        {
            if ((bool)AccelerationToggle.Value)
                Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.AccelerationValueNum.Value = (float)AccelerationValue.Value;
            Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.AccelerationSetSwitch.IsOn = (bool)s.GetType().GetProperty("Value").GetValue(s);
        });
    }
}