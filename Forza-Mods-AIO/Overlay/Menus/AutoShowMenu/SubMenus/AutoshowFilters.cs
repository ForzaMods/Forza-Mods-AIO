using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;
using static Forza_Mods_AIO.Tabs.AutoShowTab.AutoShow;

namespace Forza_Mods_AIO.Overlay.Menus.AutoShowMenu.SubMenus;

public abstract class AutoshowFilters
{
    private static readonly ToggleOption AllCarsToggle = new("All Cars", false);
    private static readonly ToggleOption RareCarsToggle = new("Rare Cars", false);
    public static readonly ToggleOption FreeCarsToggle = new("Free Cars", false);

    public static readonly List<MenuOption> AutoShowFiltersOptions = new()
    {
        AllCarsToggle,
        RareCarsToggle,
        FreeCarsToggle
    };

    public static void InitiateSubMenu()
    {
        AllCarsToggle.ToggledEventHandler += AllCarsChanged;
        RareCarsToggle.ToggledEventHandler += RareCarsChanged;
        FreeCarsToggle.ToggledEventHandler += FreeCarsChanged;
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
        if (MainWindow.Mw.Gvp.Name == "Forza Horizon 4") 
        {
            GarageModifications.FixThumbnailsToggle.IsEnabled = !GarageModifications.FixThumbnailsToggle.IsEnabled;
            GarageModifications.ShowTrafficHsNullToggle.IsEnabled = !GarageModifications.ShowTrafficHsNullToggle.IsEnabled;
            GarageModifications.UnlockHiddenPresetsToggle.IsEnabled = !GarageModifications.UnlockHiddenPresetsToggle.IsEnabled;
            GarageModifications.UnlockHiddenDecalsToggle.IsEnabled = !GarageModifications.UnlockHiddenDecalsToggle.IsEnabled;
        }
            
        As.ToggleFreeCars.IsOn = FreeCarsToggle.IsOn;
    }
}