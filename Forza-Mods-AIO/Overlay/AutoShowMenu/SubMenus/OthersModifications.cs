using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Tabs.AutoShowTab;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.AutoShowMenu.SubMenus;

public abstract class OthersModifications
{
    private static readonly MenuOption FreePerfUpgradesToggle = new ("Free Perf Upgrades", OptionType.Bool, false);
    private static readonly MenuOption FreeVisualUpgradeToggle = new ("Free Visual Upgrades", OptionType.Bool, false);
    private static readonly MenuOption QuickAddAllCarsToggle = new ("Quick Add All Cars", OptionType.Bool, false);
    private static readonly MenuOption QuickAddRareCarsToggle = new ("Quick Add Rare Cars", OptionType.Bool, false);

    public static readonly List<MenuOption> OthersModificationsOptions = new()
    {
        FreePerfUpgradesToggle,
        FreeVisualUpgradeToggle,
        QuickAddAllCarsToggle,
        QuickAddRareCarsToggle
    };
    
    public static void InitiateSubMenu()
    {
        FreePerfUpgradesToggle.ValueChangedHandler += FreePerfUpgradesToggled;
        FreeVisualUpgradeToggle.ValueChangedHandler += FreeVisualUpgradesToggled;
        QuickAddAllCarsToggle.ValueChangedHandler += QuickAddAllCarsToggled;
        QuickAddRareCarsToggle.ValueChangedHandler += QuickAddRareCarsToggled;
    }

    private static void FreePerfUpgradesToggled(object s, EventArgs e)
    {
        AutoShow.As.Dispatcher.Invoke(() =>
        {
            if (MainWindow.Mw.Gvp.Name == "Forza Horizon 4")
            {
                FreeVisualUpgradeToggle.IsEnabled = !FreeVisualUpgradeToggle.IsEnabled;
            }
                
            AutoShow.As.FreePerfUpgrades.IsOn = (bool)FreePerfUpgradesToggle.Value;
        });
    }

    private static void FreeVisualUpgradesToggled(object s, EventArgs e)
    {
        AutoShow.As.Dispatcher.Invoke(() =>
        {
            if (MainWindow.Mw.Gvp.Name == "Forza Horizon 4")
            {
                FreePerfUpgradesToggle.IsEnabled = !FreePerfUpgradesToggle.IsEnabled;
            }
                
            AutoShow.As.FreeVisualUpgrades.IsOn = (bool)FreeVisualUpgradeToggle.Value;
        });
    }

    private static void QuickAddAllCarsToggled(object s, EventArgs e)
    {
        AutoShow.As.Dispatcher.Invoke(() =>
        {
            AutoShow.As.QuickAddAllCars.IsOn = (bool)QuickAddAllCarsToggle.Value;
            QuickAddRareCarsToggle.IsEnabled = !QuickAddRareCarsToggle.IsEnabled;
        });
    }

    private static void QuickAddRareCarsToggled(object s, EventArgs e)
    {
        AutoShow.As.Dispatcher.Invoke(() =>
        {
            QuickAddAllCarsToggle.IsEnabled = !QuickAddAllCarsToggle.IsEnabled;
            AutoShow.As.QuickAddRareCars.IsOn = (bool)QuickAddRareCarsToggle.Value;
        });
    }
}