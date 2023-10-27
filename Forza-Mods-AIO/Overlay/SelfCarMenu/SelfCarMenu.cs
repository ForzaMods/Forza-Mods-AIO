using System.Collections.Generic;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu;

public class SelfCarMenu
{
    // Menu lists for this section (i.e. Modifiers and its sub menus)
    // All of these are submenus, so they have their own folders
    public static readonly List<Overlay.MenuOption> SelfCarsOptions = new()
    {
        new("Handling", "MenuButton"),
        new("Unlocks", "MenuButton"),
        new("Photomode", "MenuButton"),
        new("Customization", "MenuButton"),
        new("Miscellaneous", "MenuButton"),
        new("FOV", "MenuButton")
    };
}