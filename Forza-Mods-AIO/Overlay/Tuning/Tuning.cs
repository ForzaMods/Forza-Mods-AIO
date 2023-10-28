using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Tuning.SubMenus.Aero;
using Forza_Mods_AIO.Overlay.Tuning.SubMenus.Alignment;
using Forza_Mods_AIO.Overlay.Tuning.SubMenus.Damping.SubMenus;
using Forza_Mods_AIO.Overlay.Tuning.SubMenus.Gearing;
using Forza_Mods_AIO.Overlay.Tuning.SubMenus.Others.SubMenu;
using Forza_Mods_AIO.Overlay.Tuning.SubMenus.Springs.SubMenus;
using Forza_Mods_AIO.Overlay.Tuning.SubMenus.Steering;
using Forza_Mods_AIO.Overlay.Tuning.SubMenus.Tires;

namespace Forza_Mods_AIO.Overlay.Tuning;

public abstract class Tuning
{
    // Menu lists for this section (i.e. Modifiers and its sub menus)
    // All of these are submenus, so they have their own folders
    public static readonly List<Overlay.MenuOption> TuningOptions = new()
    {
        new Overlay.MenuOption("Aero", Overlay.MenuOption.OptionType.MenuButton),
        new Overlay.MenuOption("Alignment", Overlay.MenuOption.OptionType.MenuButton),
        new Overlay.MenuOption("Damping", Overlay.MenuOption.OptionType.MenuButton),
        new Overlay.MenuOption("Gearing", Overlay.MenuOption.OptionType.MenuButton),
        new Overlay.MenuOption("Others", Overlay.MenuOption.OptionType.MenuButton),
        new Overlay.MenuOption("Springs", Overlay.MenuOption.OptionType.MenuButton),
        new Overlay.MenuOption("Steering", Overlay.MenuOption.OptionType.MenuButton),
        new Overlay.MenuOption("Tires", Overlay.MenuOption.OptionType.MenuButton)
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