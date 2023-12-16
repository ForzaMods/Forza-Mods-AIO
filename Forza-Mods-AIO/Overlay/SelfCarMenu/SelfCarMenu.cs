using System.Collections.Generic;
using Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu;

public abstract class SelfCarMenu
{
    // Menu lists for this section (i.e. Modifiers and its sub menus)
    // All of these are submenus, so they have their own folders
    public static readonly List<MenuOption> SelfCarsOptions = new()
    {
        new MenuOption("Handling", OptionType.MenuButton),
        new MenuOption("Unlocks", OptionType.MenuButton),
        new MenuOption("Photomode", OptionType.MenuButton),
        new MenuOption("Customization", OptionType.MenuButton),
        new MenuOption("Miscellaneous", OptionType.MenuButton),
        new MenuOption("FOV", OptionType.MenuButton),
        new MenuOption("Backfire", OptionType.MenuButton),
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
        BackfireMenu.InitiateSubMenu();
    }
}