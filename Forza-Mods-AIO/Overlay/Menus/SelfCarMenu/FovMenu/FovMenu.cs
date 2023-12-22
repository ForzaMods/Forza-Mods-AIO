using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;

namespace Forza_Mods_AIO.Overlay.Menus.SelfCarMenu.FovMenu;

public abstract class FovMenu
{
    public static MenuButtonOption FovLock { get; set; } = new("Fov Lock");
    
    public static readonly List<MenuOption> FovOptions = new()
    {
        FovLock,
        new MenuButtonOption("Fov Limiters")
    };
}