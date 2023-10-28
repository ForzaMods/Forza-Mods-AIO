using System.Collections.Generic;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Springs;

public abstract class Springs
{
    public static readonly List<Overlay.MenuOption> SpringsOptions = new()
    {
        new Overlay.MenuOption("Springs Values", Overlay.MenuOption.OptionType.MenuButton),
        new Overlay.MenuOption("Ride Height", Overlay.MenuOption.OptionType.MenuButton)
    };
}