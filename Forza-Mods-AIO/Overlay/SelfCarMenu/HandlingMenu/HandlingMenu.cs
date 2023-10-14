using System;
using System.Collections.Generic;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.HandlingMenu
{
    public class HandlingMenu
    {
        // Velocity menu options
        static Overlay.MenuOption VelocityValue = new Overlay.MenuOption("Value", "Float", 0);
        static Overlay.MenuOption VelocityToggle = new Overlay.MenuOption("Enable", "Bool", false);
        static Overlay.MenuOption SuperCarToggle = new Overlay.MenuOption("Super Car", "Bool", false);
        
        // Wheelspeed menu options
        private static readonly Overlay.MenuOption WheelspeedMode = new Overlay.MenuOption("Mode", "Int", 1);
        private static readonly Overlay.MenuOption WheelspeedStrength = new Overlay.MenuOption("Strength", "Int", 10);
        private static readonly Overlay.MenuOption WheelspeedInterval = new Overlay.MenuOption("Interval", "Int", 1);
        private static readonly Overlay.MenuOption WheelspeedEnable = new Overlay.MenuOption("Enable", "Bool", false);
        

        // Subscribes menu options to event handlers
        public void InitiateSubMenu()
        {
            VelocityValue.ValueChangedHandler += new EventHandler(VelocityValueChanged);
            VelocityToggle.ValueChangedHandler += new EventHandler(VelocityToggleChanged);
            SuperCarToggle.ValueChangedHandler += new EventHandler(SuperCarToggleChanged);

            WheelspeedMode.ValueChangedHandler += WheelspeedModeChanged;
            WheelspeedStrength.ValueChangedHandler += WheelspeedStrengthChanged;
            WheelspeedInterval.ValueChangedHandler += WheelspeedIntervalChanged;
            WheelspeedEnable.ValueChangedHandler += WheelspeedToggled;
            
            (new ModifiersMenu()).InitiateSubMenu();
        }

        // Event handlers
        void VelocityValueChanged(object s, EventArgs e)
        {
        }
        void VelocityToggleChanged(object s, EventArgs e)
        {
        }
        void SuperCarToggleChanged(object s, EventArgs e)
        {
        }

        void WheelspeedModeChanged(object s, EventArgs e)
        {
            Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.Dispatcher.Invoke(delegate
            {
                if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! > 2)
                    WheelspeedMode.Value = 1;
                else if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! < 1)
                    WheelspeedMode.Value = 2;
                
                Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.WheelSpeedModeComboBox.SelectedIndex = (int)s.GetType().GetProperty("Value")!.GetValue(s)! - 1;
            });
        }

        static void WheelspeedStrengthChanged(object s, EventArgs e)
        {
            Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.Dispatcher.Invoke(delegate
            {
                if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! > Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.Var1NumBox.Maximum)
                    WheelspeedStrength.Value = (int)Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.Var1NumBox.Maximum;
                else if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! < Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.Var1NumBox.Minimum)
                    WheelspeedStrength.Value = (int)Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.Var1NumBox.Minimum;
                else
                    Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.Var1NumBox.Value = Convert.ToSingle(s.GetType().GetProperty("Value")!.GetValue(s)!);
            });
        }

        static void WheelspeedIntervalChanged(object s, EventArgs e)
        {
            Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.Dispatcher.Invoke(delegate
            {
                if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! > Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.Var2NumBox.Maximum)
                    WheelspeedInterval.Value = (int)Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.Var2NumBox.Maximum;
                else if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! <
                         Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.Var2NumBox.Minimum)
                    WheelspeedInterval.Value = (int)Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.Var2NumBox.Minimum;
                else
                    Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.Var2NumBox.Value = Convert.ToSingle(s.GetType().GetProperty("Value")!.GetValue(s)!);
            });
        }

        static void WheelspeedToggled(object s, EventArgs e)
        {
            Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.Dispatcher.Invoke(() =>
            {
                Tabs.Self_Vehicle.DropDownTabs.HandlingPage.shp.WheelSpeedSwitch.IsOn = (bool)s.GetType().GetProperty("Value")!.GetValue(s)!;
            });
        }
        
        // Menu list for this section
        public static List<Overlay.MenuOption> HandlingOptions = new List<Overlay.MenuOption>()
        {
            new Overlay.MenuOption("Velocity", "MenuButton"),
            new Overlay.MenuOption("Wheel Speed", "MenuButton"),
            new Overlay.MenuOption("Modifiers", "MenuButton"),
            SuperCarToggle
        };

        // Submenu lists
        public static List<Overlay.MenuOption> VelocityOptions = new List<Overlay.MenuOption>()
        {
            VelocityValue,
            VelocityToggle
        };
        public static List<Overlay.MenuOption> WheelSpeedOptions = new List<Overlay.MenuOption>()
        {
            WheelspeedMode,
            WheelspeedStrength,
            WheelspeedInterval,
            WheelspeedEnable
        };
    }
}
