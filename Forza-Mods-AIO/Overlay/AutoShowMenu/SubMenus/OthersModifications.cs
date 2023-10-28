using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Tabs.AutoShowTab;

namespace Forza_Mods_AIO.Overlay.AutoShowMenu.SubMenus;

public abstract class OthersModifications
{
    private static readonly Overlay.MenuOption FreePerfUpgradesToggle = new ("Free Perf Upgrades", Overlay.MenuOption.OptionType.Bool, false);
    private static readonly Overlay.MenuOption FreeVisualUpgradeToggle = new ("Free Visual Upgrades", Overlay.MenuOption.OptionType.Bool, false);
    private static readonly Overlay.MenuOption QuickAddAllCarsToggle = new ("Quick Add All Cars", Overlay.MenuOption.OptionType.Bool, false);
    private static readonly Overlay.MenuOption QuickAddRareCarsToggle = new ("Quick Add Rare Cars", Overlay.MenuOption.OptionType.Bool, false);

    public static readonly List<Overlay.MenuOption> OthersModificationsOptions = new()
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
        AutoShow.AS.Dispatcher.Invoke(() =>
        {
            if (MainWindow.mw.gvp.Name == "Forza Horizon 4")
            {
                FreeVisualUpgradeToggle.IsEnabled = !FreeVisualUpgradeToggle.IsEnabled;
            }
                
            AutoShow.AS.FreePerfUpgrades.IsOn = (bool)FreePerfUpgradesToggle.Value;
        });
    }

    private static void FreeVisualUpgradesToggled(object s, EventArgs e)
    {
        AutoShow.AS.Dispatcher.Invoke(() =>
        {
            if (MainWindow.mw.gvp.Name == "Forza Horizon 4")
            {
                FreePerfUpgradesToggle.IsEnabled = !FreePerfUpgradesToggle.IsEnabled;
            }
                
            AutoShow.AS.FreeVisualUpgrades.IsOn = (bool)FreeVisualUpgradeToggle.Value;
        });
    }

    private static void QuickAddAllCarsToggled(object s, EventArgs e)
    {
        AutoShow.AS.Dispatcher.Invoke(() =>
        {
            AutoShow.AS.QuickAddAllCars.IsOn = (bool)QuickAddAllCarsToggle.Value;
            QuickAddRareCarsToggle.IsEnabled = !QuickAddRareCarsToggle.IsEnabled;
        });
    }

    private static void QuickAddRareCarsToggled(object s, EventArgs e)
    {
        AutoShow.AS.Dispatcher.Invoke(() =>
        {
            QuickAddAllCarsToggle.IsEnabled = !QuickAddAllCarsToggle.IsEnabled;
            AutoShow.AS.QuickAddRareCars.IsOn = (bool)QuickAddRareCarsToggle.Value;
        });
    }
}