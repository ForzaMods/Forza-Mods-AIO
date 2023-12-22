using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;

namespace Forza_Mods_AIO.Overlay.Menus.TuningMenu.SubMenus.Springs;

public abstract class Springs
{
    public static readonly List<MenuOption> SpringsOptions = new()
    {
        new MenuButtonOption("Springs Values"),
        new MenuButtonOption("Ride Height")
    };
}