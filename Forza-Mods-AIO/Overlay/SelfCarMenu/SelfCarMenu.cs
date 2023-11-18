using System.Collections.Generic;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu;

public abstract class SelfCarMenu
{
    // Menu lists for this section (i.e. Modifiers and its sub menus)
    // All of these are submenus, so they have their own folders
    public static readonly List<MenuOption> SelfCarsOptions = new()
    {
        new("Handling", OptionType.MenuButton),
        new("Unlocks", OptionType.MenuButton),
        new("Photomode", OptionType.MenuButton),
        new("Customization", OptionType.MenuButton),
        new("Miscellaneous", OptionType.MenuButton),
        new("FOV", OptionType.MenuButton)
    };

    public static void InitiateSubMenu()
    {
        FovMenu.FovLock.InitiateSubMenu();
        FovMenu.FovLimiters.InitiateSubMenu();
        MiscMenu.MiscMenu.InitiateSubMenu();
        PhotomodeMenu.PhotomodeMenu.InitiateSubMenu();
        CustomizationMenu.CustomizationMenu.InitiateSubMenu();
        HandlingMenu.HandlingMenu.InitiateSubMenu();
        UnlocksMenu.CurrencyMenu.InitiateSubMenu();
    }
}