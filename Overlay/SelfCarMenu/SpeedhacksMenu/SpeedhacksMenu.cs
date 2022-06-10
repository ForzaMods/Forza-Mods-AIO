using System;
using System.Collections.Generic;

namespace WPF_Mockup.Overlay.SelfCarMenu.SpeedhacksMenu
{
    public class SpeedhacksMenu
    {
        // Velocity menu options
        static Overlay.MenuOption VelocityValue = new Overlay.MenuOption("Value", "Float", 0);
        static Overlay.MenuOption VelocityToggle = new Overlay.MenuOption("Enable", "Bool", false);


        static Overlay.MenuOption SuperCarToggle = new Overlay.MenuOption("Super Car", "Bool", false);

        // Subscribes menu options to event handlers
        public void InitiateSubMenu()
        {
            VelocityValue.ValueChangedHandler += new EventHandler(VelocityValueChanged);
            VelocityToggle.ValueChangedHandler += new EventHandler(VelocityToggleChanged);
            SuperCarToggle.ValueChangedHandler += new EventHandler(VelocityToggleChanged);
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

        // Menu list for this section
        public static List<Overlay.MenuOption> SpeedhacksOptions = new List<Overlay.MenuOption>()
        {
            new Overlay.MenuOption("Velocity", "MenuButton"),
            new Overlay.MenuOption("Wheel Speed", "MenuButton"),
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
        };
    }
}
