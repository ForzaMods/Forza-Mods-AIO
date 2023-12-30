using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Menus.AutoShowMenu.SubMenus;
using Forza_Mods_AIO.Overlay.Options;

namespace Forza_Mods_AIO.Overlay.Menus.AutoShowMenu;

public abstract class AutoShowMenu
{
    public static readonly List<MenuOption> AutoShowOptions = new()
    {
        new MenuButtonOption("Autoshow Filters"),
        new MenuButtonOption("Garage Modifications"),
        new MenuButtonOption("Others Modifications")
    };

    public static void InitiateSubMenu()
    {
        AutoshowFilters.InitiateSubMenu();
        GarageModifications.InitiateSubMenu();
    }
}