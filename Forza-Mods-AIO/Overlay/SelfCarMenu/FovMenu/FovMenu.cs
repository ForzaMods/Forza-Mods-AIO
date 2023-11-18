using System.Collections.Generic;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.FovMenu;

public abstract class FovMenu
{
    public static MenuOption FovLock = new("Fov Lock", OptionType.MenuButton);
    
    public static readonly List<MenuOption> FovOptions = new()
    {
        FovLock,
        new ("Fov Limiters", OptionType.MenuButton)
    };
}