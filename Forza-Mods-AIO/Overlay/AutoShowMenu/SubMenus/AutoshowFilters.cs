using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Tabs.AutoShowTab;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.AutoShowMenu.SubMenus;

public abstract class AutoshowFilters
{
    private static readonly MenuOption AllCarsToggle = new("All Cars", OptionType.Bool, false);
    private static readonly MenuOption RareCarsToggle = new("Rare Cars", OptionType.Bool, false);
    public static readonly MenuOption FreeCarsToggle = new("Free Cars", OptionType.Bool, false);

    public static readonly List<MenuOption> AutoShowFiltersOptions = new()
    {
        AllCarsToggle,
        RareCarsToggle,
        FreeCarsToggle
    };

    public static void InitiateSubMenu()
    {
        AllCarsToggle.ValueChangedHandler += AllCarsChanged;
        RareCarsToggle.ValueChangedHandler += RareCarsChanged;
        FreeCarsToggle.ValueChangedHandler += FreeCarsChanged;
    }

    // Event handlers
    private static void AllCarsChanged(object s, EventArgs e)
    {
        AutoShow.As.Dispatcher.Invoke(() =>
        {
            RareCarsToggle.IsEnabled = !RareCarsToggle.IsEnabled;
            AutoShow.As.ToggleAllCars.IsOn = (bool)AllCarsToggle.Value;
        });
    }

    private static void RareCarsChanged(object s, EventArgs e)
    {
        AutoShow.As.Dispatcher.Invoke(() =>
        {
            AllCarsToggle.IsEnabled = !AllCarsToggle.IsEnabled;
            AutoShow.As.ToggleRareCars.IsOn = (bool)RareCarsToggle.Value;
        });
    }

    private static void FreeCarsChanged(object s, EventArgs e)
    {
        AutoShow.As.Dispatcher.Invoke(() =>
        {
            if (MainWindow.Mw.Gvp.Name == "Forza Horizon 4") 
            {
                GarageModifications.FixThumbnailsToggle.IsEnabled = !GarageModifications.FixThumbnailsToggle.IsEnabled;
                GarageModifications.ShowTrafficHsNullToggle.IsEnabled = !GarageModifications.ShowTrafficHsNullToggle.IsEnabled;
                GarageModifications.UnlockHiddenPresetsToggle.IsEnabled = !GarageModifications.UnlockHiddenPresetsToggle.IsEnabled;
                GarageModifications.UnlockHiddenDecalsToggle.IsEnabled = !GarageModifications.UnlockHiddenDecalsToggle.IsEnabled;
            }
            
            AutoShow.As.ToggleFreeCars.IsOn = (bool)FreeCarsToggle.Value;
        });
    }
}