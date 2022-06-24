using System;
using System.Collections.Generic;
using System.Windows;

namespace WPF_Mockup.Overlay.SelfCarMenu.ModifiersMenu
{
    public class ModifiersMenu
    {
        // Gravity menu options
        static Overlay.MenuOption GravityValue = new Overlay.MenuOption("Value", "Float", 0f);
        static Overlay.MenuOption GravityToggle = new Overlay.MenuOption("Enable", "Bool", false);
        static Overlay.MenuOption GravityPull = new Overlay.MenuOption("Pull", "Button", new Action(() =>  // This one has a method for its value
        {
            Tabs.Self_Vehicle.DropDownTabs.ModifiersPage.mp.Dispatcher.Invoke(delegate () {
                object sender = Tabs.Self_Vehicle.DropDownTabs.ModifiersPage.mp.GravityPullButton;
                RoutedEventArgs e = new RoutedEventArgs();
                Tabs.Self_Vehicle.DropDownTabs.ModifiersPage.mp.PullButton_Click(sender, e);
                GravityValue.Value = (float)Tabs.Self_Vehicle.DropDownTabs.ModifiersPage.mp.GravityValueNum.Value;
            });
        }));
        
        // Acceleration menu options
        static Overlay.MenuOption AccelerationValue = new Overlay.MenuOption("Value", "Float", 0f);
        static Overlay.MenuOption AccelerationToggle = new Overlay.MenuOption("Enable", "Bool", false);
        static Overlay.MenuOption AccelerationPull = new Overlay.MenuOption("Pull", "Button", new Action(() =>
        {
            Tabs.Self_Vehicle.DropDownTabs.ModifiersPage.mp.Dispatcher.Invoke(delegate () {
                object sender = Tabs.Self_Vehicle.DropDownTabs.ModifiersPage.mp.AccelerationPullButton;
                RoutedEventArgs e = new RoutedEventArgs();
                Tabs.Self_Vehicle.DropDownTabs.ModifiersPage.mp.PullButton_Click(sender, e);
                AccelerationValue.Value = (float)Tabs.Self_Vehicle.DropDownTabs.ModifiersPage.mp.AccelerationValueNum.Value;
            });
        }));

        // Subscribes menu options to event handlers
        public void InitiateSubMenu()
        {
            GravityValue.ValueChangedHandler += new EventHandler(GravityValueChanged);
            GravityToggle.ValueChangedHandler += new EventHandler(GravityToggleChanged);
            AccelerationValue.ValueChangedHandler += new EventHandler(AccelerationValueChanged);
            AccelerationToggle.ValueChangedHandler += new EventHandler(AccelerationToggleChanged);
        }
        
        // Event handlers
        void GravityValueChanged(object s, EventArgs e)
        {
            Tabs.Self_Vehicle.DropDownTabs.ModifiersPage.mp.Dispatcher.Invoke(delegate () {
                if ((float)s.GetType().GetProperty("Value").GetValue(s) > Tabs.Self_Vehicle.DropDownTabs.ModifiersPage.mp.GravityValueNum.Maximum)
                    GravityValue.Value = (float)Tabs.Self_Vehicle.DropDownTabs.ModifiersPage.mp.GravityValueNum.Maximum;
                else if ((float)s.GetType().GetProperty("Value").GetValue(s) < Tabs.Self_Vehicle.DropDownTabs.ModifiersPage.mp.GravityValueNum.Minimum)
                    GravityValue.Value = (float)Tabs.Self_Vehicle.DropDownTabs.ModifiersPage.mp.GravityValueNum.Minimum;
                else
                    Tabs.Self_Vehicle.DropDownTabs.ModifiersPage.mp.GravityValueNum.Value = (float)s.GetType().GetProperty("Value").GetValue(s);
            });
        }
        void GravityToggleChanged(object s, EventArgs e)
        {
            Tabs.Self_Vehicle.DropDownTabs.ModifiersPage.mp.Dispatcher.Invoke(delegate () {
                if((bool)GravityToggle.Value)
                    Tabs.Self_Vehicle.DropDownTabs.ModifiersPage.mp.GravityValueNum.Value = (float)GravityValue.Value;
                Tabs.Self_Vehicle.DropDownTabs.ModifiersPage.mp.GravitySetSwitch.IsOn = (bool)s.GetType().GetProperty("Value").GetValue(s);
            });
        }
        void AccelerationValueChanged(object s, EventArgs e)
        {
            Tabs.Self_Vehicle.DropDownTabs.ModifiersPage.mp.Dispatcher.Invoke(delegate () {
                if ((float)s.GetType().GetProperty("Value").GetValue(s) > Tabs.Self_Vehicle.DropDownTabs.ModifiersPage.mp.AccelerationValueNum.Maximum)
                    AccelerationValue.Value = (float)Tabs.Self_Vehicle.DropDownTabs.ModifiersPage.mp.AccelerationValueNum.Maximum;
                else if ((float)s.GetType().GetProperty("Value").GetValue(s) < Tabs.Self_Vehicle.DropDownTabs.ModifiersPage.mp.AccelerationValueNum.Minimum)
                    AccelerationValue.Value = (float)Tabs.Self_Vehicle.DropDownTabs.ModifiersPage.mp.AccelerationValueNum.Minimum;
                else
                    Tabs.Self_Vehicle.DropDownTabs.ModifiersPage.mp.AccelerationValueNum.Value = (float)s.GetType().GetProperty("Value").GetValue(s);
            });
        }
        void AccelerationToggleChanged(object s, EventArgs e)
        {
            Tabs.Self_Vehicle.DropDownTabs.ModifiersPage.mp.Dispatcher.Invoke(delegate () {
                if ((bool)AccelerationToggle.Value)
                    Tabs.Self_Vehicle.DropDownTabs.ModifiersPage.mp.AccelerationValueNum.Value = (float)AccelerationValue.Value;
                Tabs.Self_Vehicle.DropDownTabs.ModifiersPage.mp.AccelerationSetSwitch.IsOn = (bool)s.GetType().GetProperty("Value").GetValue(s);
            });
        }

        // Menu list for this section
        public static List<Overlay.MenuOption> ModifiersOptions = new List<Overlay.MenuOption>()
        {
            new Overlay.MenuOption("Gravity", "MenuButton"),
            new Overlay.MenuOption("Acceleration", "MenuButton")
        };

        // Submenu lists
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
    }
}
