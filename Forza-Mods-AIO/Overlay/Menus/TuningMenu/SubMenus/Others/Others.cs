using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;

namespace Forza_Mods_AIO.Overlay.Menus.TuningMenu.SubMenus.Others;

public abstract class Others
{
    public static readonly List<MenuOption> OthersOptions = new()
    {
        new MenuButtonOption("Wheelbase"),
        new MenuButtonOption("Rims")
    };
}