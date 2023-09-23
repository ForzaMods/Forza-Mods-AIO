using System;
using System.Collections.Generic;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.HandlingMenu
{
    public class HandlingMenu
    {
        private ModifiersMenu _modifiersMenu = new();
        
        // Velocity menu options
        static Overlay.MenuOption VelocityValue = new Overlay.MenuOption("Value", "Float", 0);
        static Overlay.MenuOption VelocityToggle = new Overlay.MenuOption("Enable", "Bool", false);
        static Overlay.MenuOption SuperCarToggle = new Overlay.MenuOption("Super Car", "Bool", false);

        // Subscribes menu options to event handlers
        public void InitiateSubMenu()
        {
            VelocityValue.ValueChangedHandler += new EventHandler(VelocityValueChanged);
            VelocityToggle.ValueChangedHandler += new EventHandler(VelocityToggleChanged);
            SuperCarToggle.ValueChangedHandler += new EventHandler(SuperCarToggleChanged);
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
        };
    }
}
