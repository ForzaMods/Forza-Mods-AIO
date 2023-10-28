using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Tabs.AutoShowTab;

namespace Forza_Mods_AIO.Overlay.AutoShowMenu.SubMenus;

public abstract class AutoshowFilters
{
    private static readonly Overlay.MenuOption AllCarsToggle = new("All Cars", Overlay.MenuOption.OptionType.Bool, false, "Toggles all cars in the autoshow");
    private static readonly Overlay.MenuOption RareCarsToggle = new("Rare Cars", Overlay.MenuOption.OptionType.Bool, false, "Toggles rare cars in the autoshow");
    public static readonly Overlay.MenuOption FreeCarsToggle = new("Free Cars", Overlay.MenuOption.OptionType.Bool, false, "Toggles free cars in the autoshow");

    public static readonly List<Overlay.MenuOption> AutoShowFiltersOptions = new()
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
        AutoShow.AS.Dispatcher.Invoke(() =>
        {
            RareCarsToggle.IsEnabled = !RareCarsToggle.IsEnabled;
            AutoShow.AS.ToggleAllCars.IsOn = (bool)AllCarsToggle.Value;
        });
    }

    private static void RareCarsChanged(object s, EventArgs e)
    {
        AutoShow.AS.Dispatcher.Invoke(() =>
        {
            AllCarsToggle.IsEnabled = !AllCarsToggle.IsEnabled;
            AutoShow.AS.ToggleRareCars.IsOn = (bool)RareCarsToggle.Value;
        });
    }

    private static void FreeCarsChanged(object s, EventArgs e)
    {
        AutoShow.AS.Dispatcher.Invoke(() =>
        {
            if (MainWindow.mw.gvp.Name == "Forza Horizon 4") 
            {
                GarageModifications.FixThumbnailsToggle.IsEnabled = !GarageModifications.FixThumbnailsToggle.IsEnabled;
                GarageModifications.ShowTrafficHsNullToggle.IsEnabled = !GarageModifications.ShowTrafficHsNullToggle.IsEnabled;
                GarageModifications.UnlockHiddenPresetsToggle.IsEnabled = !GarageModifications.UnlockHiddenPresetsToggle.IsEnabled;
                GarageModifications.UnlockHiddenDecalsToggle.IsEnabled = !GarageModifications.UnlockHiddenDecalsToggle.IsEnabled;
            }
            
            AutoShow.AS.ToggleFreeCars.IsOn = (bool)FreeCarsToggle.Value;
        });
    }
}