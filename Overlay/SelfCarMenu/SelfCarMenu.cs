using System.Collections.Generic;

namespace WPF_Mockup.Overlay.SelfCarMenu
{
    public class SelfCarMenu
    {
        // Menu lists for this section (i.e. Modifiers and its sub menus)
        // All of these are submenus, so they have their own folders
        public static List<Overlay.MenuOption> SelfCarsOptions = new List<Overlay.MenuOption>()
        {
            new Overlay.MenuOption("Speedhacks", "MenuButton"),
            new Overlay.MenuOption("Unlocks", "MenuButton"),
            new Overlay.MenuOption("Camera", "MenuButton"),
            new Overlay.MenuOption("Modifiers", "MenuButton"),
            new Overlay.MenuOption("Stats", "MenuButton"),
            new Overlay.MenuOption("Teleports", "MenuButton"),
            new Overlay.MenuOption("Environments", "MenuButton"),
            new Overlay.MenuOption("Tuning", "MenuButton")
        };
    }
}
