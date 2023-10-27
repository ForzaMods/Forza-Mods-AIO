using System.Collections.Generic;

namespace Forza_Mods_AIO.Overlay.Tuning;

public abstract class Tuning
{
    // Menu lists for this section (i.e. Modifiers and its sub menus)
    // All of these are submenus, so they have their own folders
    public static readonly List<Overlay.MenuOption> TuningOptions = new()
    {
        new Overlay.MenuOption("Aero", "MenuButton"),
        new Overlay.MenuOption("Alignment", "MenuButton"),
        new Overlay.MenuOption("Damping", "MenuButton"),
        new Overlay.MenuOption("Gearing", "MenuButton"),
        new Overlay.MenuOption("Others", "MenuButton"),
        new Overlay.MenuOption("Springs", "MenuButton"),
        new Overlay.MenuOption("Steering", "MenuButton"),
        new Overlay.MenuOption("Tires", "MenuButton")
    };
}