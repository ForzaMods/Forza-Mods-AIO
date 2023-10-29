using System.Collections.Generic;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu;

public abstract class SelfCarMenu
{
    // Menu lists for this section (i.e. Modifiers and its sub menus)
    // All of these are submenus, so they have their own folders
    public static readonly List<Overlay.MenuOption> SelfCarsOptions = new()
    {
        new("Handling", Overlay.MenuOption.OptionType.MenuButton),
        new("Unlocks", Overlay.MenuOption.OptionType.MenuButton),
        new("Photomode", Overlay.MenuOption.OptionType.MenuButton),
        new("Customization", Overlay.MenuOption.OptionType.MenuButton),
        new("Miscellaneous", Overlay.MenuOption.OptionType.MenuButton),
        new("FOV", Overlay.MenuOption.OptionType.MenuButton)
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