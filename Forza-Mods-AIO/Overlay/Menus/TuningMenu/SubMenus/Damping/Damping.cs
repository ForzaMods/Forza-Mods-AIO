using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;

namespace Forza_Mods_AIO.Overlay.Menus.TuningMenu.SubMenus.Damping;

public abstract class Damping
{
    public static readonly List<MenuOption> DampingOptions = new()
    {
        new MenuButtonOption("Antiroll Bars/Damping"),
        new MenuButtonOption("Rebound Stiffness"),
        new MenuButtonOption("Bump Stiffness")
    };
}