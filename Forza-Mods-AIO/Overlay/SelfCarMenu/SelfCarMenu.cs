using System.Collections.Generic;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu
{
    public class SelfCarMenu
    {
        // Menu lists for this section (i.e. Modifiers and its sub menus)
        // All of these are submenus, so they have their own folders
        public static List<Overlay.MenuOption> SelfCarsOptions = new List<Overlay.MenuOption>()
        {
            new Overlay.MenuOption("Handling", "MenuButton"),
            new Overlay.MenuOption("Unlocks", "MenuButton"),
            new Overlay.MenuOption("Camera", "MenuButton"),
            new Overlay.MenuOption("Modifiers", "MenuButton"),
            new Overlay.MenuOption("Stats", "MenuButton"),
            new Overlay.MenuOption("Teleports", "MenuButton"),
            new Overlay.MenuOption("Environment", "MenuButton"),
            new Overlay.MenuOption("Tuning", "MenuButton")
        };
    }
}
