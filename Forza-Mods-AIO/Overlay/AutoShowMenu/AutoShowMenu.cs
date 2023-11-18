using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.AutoShowMenu.SubMenus;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.AutoShowMenu;

public abstract class AutoShowMenu
{
    public static readonly List<MenuOption> AutoShowOptions = new()
    {
        new("Autoshow Filters", OptionType.MenuButton),
        new("Garage Modifications", OptionType.MenuButton),
        new("Others Modifications", OptionType.MenuButton)
    };

    public static void InitiateSubMenu()
    {
        AutoshowFilters.InitiateSubMenu();
        GarageModifications.InitiateSubMenu();
        OthersModifications.InitiateSubMenu();
    }
}