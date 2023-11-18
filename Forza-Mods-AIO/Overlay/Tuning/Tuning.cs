using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Tuning.SubMenus.Aero;
using Forza_Mods_AIO.Overlay.Tuning.SubMenus.Alignment;
using Forza_Mods_AIO.Overlay.Tuning.SubMenus.Damping.SubMenus;
using Forza_Mods_AIO.Overlay.Tuning.SubMenus.Gearing;
using Forza_Mods_AIO.Overlay.Tuning.SubMenus.Others.SubMenu;
using Forza_Mods_AIO.Overlay.Tuning.SubMenus.Springs.SubMenus;
using Forza_Mods_AIO.Overlay.Tuning.SubMenus.Steering;
using Forza_Mods_AIO.Overlay.Tuning.SubMenus.Tires;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.Tuning;

public abstract class Tuning
{
    // Menu lists for this section (i.e. Modifiers and its sub menus)
    // All of these are submenus, so they have their own folders
    public static readonly List<MenuOption> TuningOptions = new()
    {
        new MenuOption("Aero", OptionType.MenuButton),
        new MenuOption("Alignment", OptionType.MenuButton),
        new MenuOption("Damping", OptionType.MenuButton),
        new MenuOption("Gearing", OptionType.MenuButton),
        new MenuOption("Others", OptionType.MenuButton),
        new MenuOption("Springs", OptionType.MenuButton),
        new MenuOption("Steering", OptionType.MenuButton),
        new MenuOption("Tires", OptionType.MenuButton)
    };

    public static void InitiateSubMenu()
    {
        Aero.InitiateSubMenu();
        Alignment.InitiateSubMenu();
        AntirollBarsDamping.InitiateSubMenu();
        ReboundStiffness.InitiateSubMenu();
        BumpStiffness.InitiateSubMenu();
        Gearing.InitiateSubMenu();
        Wheelbase.InitiateSubMenu();
        Rims.InitiateSubMenu();
        SpringsValues.InitiateSubMenu();
        RideHeight.InitiateSubMenu();
        Steering.InitiateSubMenu();
        Tires.InitiateSubMenu();
    }
}