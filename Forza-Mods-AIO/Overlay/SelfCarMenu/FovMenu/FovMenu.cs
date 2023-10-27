using System.Collections.Generic;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.FovMenu;

public abstract class FovMenu
{
    public static Overlay.MenuOption FovLock = new("Fov Lock", "MenuButton");
    
    public static readonly List<Overlay.MenuOption> FovOptions = new()
    {
        FovLock,
        new ("Fov Limiters", "MenuButton")
    };
}