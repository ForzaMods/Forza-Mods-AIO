using System.Collections.Generic;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Springs;

public abstract class Springs
{
    public static readonly List<MenuOption> SpringsOptions = new()
    {
        new MenuOption("Springs Values", OptionType.MenuButton),
        new MenuOption("Ride Height", OptionType.MenuButton)
    };
}