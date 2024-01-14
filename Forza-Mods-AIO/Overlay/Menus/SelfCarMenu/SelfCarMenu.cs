using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Menus.SelfCarMenu.FovMenu;
using Forza_Mods_AIO.Overlay.Options;

namespace Forza_Mods_AIO.Overlay.Menus.SelfCarMenu;

public abstract class SelfCarMenu
{
    public static readonly List<MenuOption> SelfCarsOptions = new()
    {
        new MenuButtonOption("Handling"),
        new MenuButtonOption("Unlocks"),
        new MenuButtonOption("Photomode"),
        new MenuButtonOption("Customization"),
        new MenuButtonOption("Miscellaneous"),
        new MenuButtonOption("FOV"),
        new MenuButtonOption("Backfire")
    };

    public static void InitiateSubMenu()
    {
        FovLock.InitiateSubMenu();
        FovLimiters.InitiateSubMenu();
        MiscMenu.InitiateSubMenu();
        CustomizationMenu.InitiateSubMenu();
        HandlingMenu.HandlingMenu.InitiateSubMenu();
        UnlocksMenu.InitiateSubMenu();
        BackfireMenu.InitiateSubMenu();
    }
}