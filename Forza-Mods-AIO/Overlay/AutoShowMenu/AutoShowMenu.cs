using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.AutoShowMenu.SubMenus;

namespace Forza_Mods_AIO.Overlay.AutoShowMenu;

public abstract class AutoShowMenu
{
    public static readonly List<Overlay.MenuOption> AutoShowOptions = new()
    {
        new("Autoshow Filters", "MenuButton"),
        new("Garage Modifications", "MenuButton"),
        new("Others Modifications", "MenuButton")
    };

    public static void InitiateSubMenu()
    {
        AutoshowFilters.InitiateSubMenu();
        GarageModifications.InitiateSubMenu();
        OthersModifications.InitiateSubMenu();
    }
}