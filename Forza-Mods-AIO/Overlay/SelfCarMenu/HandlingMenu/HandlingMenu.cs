using System;
using System.Collections.Generic;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.HandlingMenu;

public class HandlingMenu
{
    // Velocity menu options
    static MenuOption _velocityValue = new("Value", OptionType.Float, 0f);
    static MenuOption _velocityToggle = new("Enable", OptionType.Bool, false);
        

    // Subscribes menu options to event handlers
    public static void InitiateSubMenu()
    {
        _velocityValue.ValueChangedHandler += VelocityValueChanged;
        _velocityToggle.ValueChangedHandler += VelocityToggleChanged;

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
    public static List<MenuOption> HandlingOptions = new()
    {
        new("Velocity", OptionType.MenuButton),
        new("Wheel Speed", OptionType.MenuButton),
        new("Turn Assist", OptionType.MenuButton),
        new("Modifiers", OptionType.MenuButton),
        new("Flyhack", OptionType.MenuButton),
        new("Handling Toggles", OptionType.MenuButton)
    };

    // Submenu lists
    public static List<MenuOption> VelocityOptions = new()
    {
        new ("Velocity", OptionType.SubHeader),
        _velocityValue,
        _velocityToggle
    };
}