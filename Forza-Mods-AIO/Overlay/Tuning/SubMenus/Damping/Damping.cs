using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Tuning.SubMenus.Damping.SubMenus;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Damping;

public abstract class Damping
{
    public static readonly List<Overlay.MenuOption> DampingOptions = new()
    {
        new Overlay.MenuOption("Antiroll Bars/Damping", "MenuButton"),
        new Overlay.MenuOption("Rebound Stiffness", "MenuButton"),
        new Overlay.MenuOption("Bump Stiffness", "MenuButton")
    };
}