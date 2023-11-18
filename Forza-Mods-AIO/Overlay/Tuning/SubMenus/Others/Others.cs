using System.Collections.Generic;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Others;

public abstract class Others
{
    public static readonly List<MenuOption> OthersOptions = new()
    {
        new MenuOption("Wheelbase", OptionType.MenuButton),
        new MenuOption("Rims", OptionType.MenuButton)
    };
}