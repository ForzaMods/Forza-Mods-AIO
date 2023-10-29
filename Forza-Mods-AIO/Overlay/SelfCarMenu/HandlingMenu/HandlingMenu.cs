using System;
using System.Collections.Generic;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.HandlingMenu
{
    public class HandlingMenu
    {
        // Velocity menu options
        static Overlay.MenuOption VelocityValue = new("Value", Overlay.MenuOption.OptionType.Float, 0f);
        static Overlay.MenuOption VelocityToggle = new("Enable", Overlay.MenuOption.OptionType.Bool, false);
        

        // Subscribes menu options to event handlers
        public static void InitiateSubMenu()
        {
            VelocityValue.ValueChangedHandler += VelocityValueChanged;
            VelocityToggle.ValueChangedHandler += VelocityToggleChanged;

            WheelspeedMenu.InitiateSubMenu();
            ModifiersMenu.InitiateSubMenu();
            FlyhackMenu.InitiateSubMenu();
            TurnAssistMenu.InitiateSubMenu();
            HandlingTogglesMenu.InitiateSubMenu();
        }

        // Event handlers
        private static void VelocityValueChanged(object s, EventArgs e)
        {
        }

        private static void VelocityToggleChanged(object s, EventArgs e)
        {
        }
        
        // Menu list for this section
        public static List<Overlay.MenuOption> HandlingOptions = new()
        {
            new("Velocity", Overlay.MenuOption.OptionType.MenuButton),
            new("Wheel Speed", Overlay.MenuOption.OptionType.MenuButton),
            new("Turn Assist", Overlay.MenuOption.OptionType.MenuButton),
            new("Modifiers", Overlay.MenuOption.OptionType.MenuButton),
            new("Flyhack", Overlay.MenuOption.OptionType.MenuButton),
            new("Handling Toggles", Overlay.MenuOption.OptionType.MenuButton)
        };

        // Submenu lists
        public static List<Overlay.MenuOption> VelocityOptions = new()
        {
            new ("Velocity", Overlay.MenuOption.OptionType.SubHeader),
            VelocityValue,
            VelocityToggle
        };
    }
}
