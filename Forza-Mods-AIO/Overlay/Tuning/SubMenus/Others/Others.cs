using System.Collections.Generic;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Others;

public abstract class Others
{
    public static readonly List<Overlay.MenuOption> OthersOptions = new()
    {
        new Overlay.MenuOption("Wheelbase", Overlay.MenuOption.OptionType.MenuButton),
        new Overlay.MenuOption("Rims", Overlay.MenuOption.OptionType.MenuButton)
    };
}