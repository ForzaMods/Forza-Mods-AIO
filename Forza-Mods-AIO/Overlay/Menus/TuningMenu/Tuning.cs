using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Menus.TuningMenu.SubMenus.Aero;
using Forza_Mods_AIO.Overlay.Menus.TuningMenu.SubMenus.Alignment;
using Forza_Mods_AIO.Overlay.Menus.TuningMenu.SubMenus.Damping.SubMenus;
using Forza_Mods_AIO.Overlay.Menus.TuningMenu.SubMenus.Gearing;
using Forza_Mods_AIO.Overlay.Menus.TuningMenu.SubMenus.Others.SubMenu;
using Forza_Mods_AIO.Overlay.Menus.TuningMenu.SubMenus.Springs.SubMenus;
using Forza_Mods_AIO.Overlay.Menus.TuningMenu.SubMenus.Steering;
using Forza_Mods_AIO.Overlay.Menus.TuningMenu.SubMenus.Tires;
using Forza_Mods_AIO.Overlay.Options;

namespace Forza_Mods_AIO.Overlay.Menus.TuningMenu;

public abstract class Tuning
{
    public static readonly List<MenuOption> TuningOptions = new()
    {
        new MenuButtonOption("Aero"),
        new MenuButtonOption("Alignment"),
        new MenuButtonOption("Damping"),
        new MenuButtonOption("Gearing", Gearing.PullValues),
        new MenuButtonOption("Others"),
        new MenuButtonOption("Springs"),
        new MenuButtonOption("Steering"),
        new MenuButtonOption("Tires")
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