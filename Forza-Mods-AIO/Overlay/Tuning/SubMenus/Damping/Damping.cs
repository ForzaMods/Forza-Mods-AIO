using System.Collections.Generic;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Damping;

public abstract class Damping
{
    public static readonly List<MenuOption> DampingOptions = new()
    {
        new MenuOption("Antiroll Bars/Damping", OptionType.MenuButton),
        new MenuOption("Rebound Stiffness", OptionType.MenuButton),
        new MenuOption("Bump Stiffness", OptionType.MenuButton)
    };
}