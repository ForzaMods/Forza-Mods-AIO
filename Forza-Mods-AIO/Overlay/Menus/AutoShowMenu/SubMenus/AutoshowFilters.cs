using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;

using static Forza_Mods_AIO.Tabs.AutoShowTab.AutoShow;

namespace Forza_Mods_AIO.Overlay.Menus.AutoShowMenu.SubMenus;

public abstract class AutoshowFilters
{
    private static readonly ToggleOption AllCarsToggle = new("All Cars", false);
    private static readonly ToggleOption RareCarsToggle = new("Rare Cars", false);
    private static readonly ToggleOption FreeCarsToggle = new("Free Cars", false);

    public static readonly List<MenuOption> AutoShowFiltersOptions = new()
    {
        AllCarsToggle,
        RareCarsToggle,
        FreeCarsToggle
    };

    public static void InitiateSubMenu()
    {
        AllCarsToggle.Toggled += AllCarsChanged;
        RareCarsToggle.Toggled += RareCarsChanged;
        FreeCarsToggle.Toggled += FreeCarsChanged;
    }

    private static void AllCarsChanged(object s, EventArgs e)
    {
        RareCarsToggle.IsEnabled = !RareCarsToggle.IsEnabled;
        As.ToggleAllCars.IsOn = AllCarsToggle.IsOn;
    }

    private static void RareCarsChanged(object s, EventArgs e)
    {
        AllCarsToggle.IsEnabled = !AllCarsToggle.IsEnabled;
        As.ToggleRareCars.IsOn = RareCarsToggle.IsOn;
    }

    private static void FreeCarsChanged(object s, EventArgs e)
    {
        As.ToggleFreeCars.IsOn = FreeCarsToggle.IsOn;
    }
}