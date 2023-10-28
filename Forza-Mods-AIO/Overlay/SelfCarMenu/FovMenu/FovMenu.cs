using System.Collections.Generic;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.FovMenu;

public abstract class FovMenu
{
    public static Overlay.MenuOption FovLock = new("Fov Lock", Overlay.MenuOption.OptionType.MenuButton);
    
    public static readonly List<Overlay.MenuOption> FovOptions = new()
    {
        FovLock,
        new ("Fov Limiters", Overlay.MenuOption.OptionType.MenuButton)
    };
}