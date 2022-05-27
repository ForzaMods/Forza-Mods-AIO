using System.Collections.Generic;

namespace WPF_Mockup.Tabs.Overlay.SelfCarMenu.ModifiersMenu
{
    public class ModifiersMenu
    {
        public static List<Overlay.MenuOption> ModifiersOptions = new List<Overlay.MenuOption>()
        {
            new Overlay.MenuOption("Gravity", "MenuButton", null),
            new Overlay.MenuOption("Acceleration", "MenuButton", null)
        };

        public static List<Overlay.MenuOption> GravityOptions = new List<Overlay.MenuOption>()
        {
            new Overlay.MenuOption("Value", "Float", "0"),
            new Overlay.MenuOption("Pull", "Button", null),
            new Overlay.MenuOption("Set", "Bool", "False")
        };
    }
}
