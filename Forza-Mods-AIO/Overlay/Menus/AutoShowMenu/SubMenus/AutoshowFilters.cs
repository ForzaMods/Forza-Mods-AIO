using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;

using static Forza_Mods_AIO.Tabs.AutoShowTab.AutoShow;

namespace Forza_Mods_AIO.Overlay.Menus.AutoShowMenu.SubMenus;

public abstract class AutoshowFilters
{
    private static readonly ToggleOption AllCarsToggle = new("All Cars", false, "This option will show every car in the autoshow. Cannot be utilized simultaneously with rare cars toggle");
    private static readonly ToggleOption RareCarsToggle = new("Rare Cars", false, "This option will show every rare car in the autoshow. Cannot be utilized simultaneously with all cars toggle");
    private static readonly ToggleOption FreeCarsToggle = new("Free Cars", false, "This option will make every car in the autoshow free.");
    private static readonly ToggleOption ShowTrafficHsNullToggle = new("Show Traffic/HS/Null", false, "This option enables seeing all cars in the autoshow. You must enable either all or rare cars for it to take effect.");

    public static readonly List<MenuOption> AutoShowFiltersOptions = new()
    {
        AllCarsToggle,
        RareCarsToggle,
        FreeCarsToggle,
        ShowTrafficHsNullToggle
    };

    public static void InitiateSubMenu()
    {
        AllCarsToggle.Toggled += AllCarsChanged;
        RareCarsToggle.Toggled += RareCarsChanged;
        FreeCarsToggle.Toggled += FreeCarsChanged;
        ShowTrafficHsNullToggle.Toggled += ShowTrafficHsNullToggled;
    }
    private static void ShowTrafficHsNullToggled(object s, EventArgs e)
    {
        As.ShowTrafficHsNull.IsOn = ShowTrafficHsNullToggle.IsOn; 
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