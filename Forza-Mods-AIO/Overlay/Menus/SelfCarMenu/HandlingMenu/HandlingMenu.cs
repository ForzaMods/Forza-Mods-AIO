using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;

namespace Forza_Mods_AIO.Overlay.Menus.SelfCarMenu.HandlingMenu;

public class HandlingMenu
{
    public static void InitiateSubMenu()
    {
        WheelspeedMenu.InitiateSubMenu();
        ModifiersMenu.InitiateSubMenu();
        FlyhackMenu.InitiateSubMenu();
        TurnAssistMenu.InitiateSubMenu();
        HandlingTogglesMenu.InitiateSubMenu();
        VelocityMenu.InitiateSubMenu();
    }
        
    public static readonly List<MenuOption> HandlingOptions = new()
    {
        new MenuButtonOption("Velocity"),
        new MenuButtonOption("Wheel Speed"),
        new MenuButtonOption("Turn Assist"),
        new MenuButtonOption("Modifiers"),
        new MenuButtonOption("Flyhack"),
        new MenuButtonOption("Handling Toggles")
    };
}