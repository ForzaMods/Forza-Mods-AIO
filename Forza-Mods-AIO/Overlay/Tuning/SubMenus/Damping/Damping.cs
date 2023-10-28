using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Tuning.SubMenus.Damping.SubMenus;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Damping;

public abstract class Damping
{
    public static readonly List<Overlay.MenuOption> DampingOptions = new()
    {
        new Overlay.MenuOption("Antiroll Bars/Damping", Overlay.MenuOption.OptionType.MenuButton),
        new Overlay.MenuOption("Rebound Stiffness", Overlay.MenuOption.OptionType.MenuButton),
        new Overlay.MenuOption("Bump Stiffness", Overlay.MenuOption.OptionType.MenuButton)
    };
}