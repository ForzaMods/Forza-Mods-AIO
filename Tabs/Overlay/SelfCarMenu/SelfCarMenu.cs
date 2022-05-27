using System.Collections.Generic;

namespace WPF_Mockup.Tabs.Overlay.SelfCarMenu
{
    public class SelfCarMenu
    {
        public static List<Overlay.MenuOption> SelfCarsOptions = new List<Overlay.MenuOption>()
        {
            new Overlay.MenuOption("Speedhacks", "MenuButton", null),
            new Overlay.MenuOption("Unlocks", "MenuButton", null),
            new Overlay.MenuOption("Camera", "MenuButton", null),
            new Overlay.MenuOption("Modifiers", "MenuButton", null),
            new Overlay.MenuOption("Stats", "MenuButton", null),
            new Overlay.MenuOption("Teleports", "MenuButton", null),
            new Overlay.MenuOption("Environments", "MenuButton", null),
            new Overlay.MenuOption("Tuning", "MenuButton", null)
        };
    }
}
