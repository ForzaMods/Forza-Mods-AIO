using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;
using static Forza_Mods_AIO.Tabs.AutoShowTab.AutoShow;

namespace Forza_Mods_AIO.Overlay.Menus.AutoShowMenu.SubMenus;

public abstract class OthersModifications
{
    private static readonly ToggleOption FreePerfUpgradesToggle = new ("Free Perf Upgrades", false);
    private static readonly ToggleOption FreeVisualUpgradeToggle = new ("Free Visual Upgrades", false);
    private static readonly ToggleOption QuickAddAllCarsToggle = new ("Quick Add All Cars", false);
    private static readonly ToggleOption QuickAddRareCarsToggle = new ("Quick Add Rare Cars", false);

    public static readonly List<MenuOption> OthersModificationsOptions = new()
    {
        FreePerfUpgradesToggle,
        FreeVisualUpgradeToggle,
        QuickAddAllCarsToggle,
        QuickAddRareCarsToggle
    };
    
    public static void InitiateSubMenu()
    {
        FreePerfUpgradesToggle.Toggled += FreePerfUpgradesToggled;
        FreeVisualUpgradeToggle.Toggled += FreeVisualUpgradesToggled;
        QuickAddAllCarsToggle.Toggled += QuickAddAllCarsToggled;
        QuickAddRareCarsToggle.Toggled += QuickAddRareCarsToggled;
    }

    private static void FreePerfUpgradesToggled(object s, EventArgs e)
    {
        if (MainWindow.Mw.Gvp.Name == "Forza Horizon 4")
        {
            FreeVisualUpgradeToggle.IsEnabled = !FreeVisualUpgradeToggle.IsEnabled;
        }
                
        As.FreePerfUpgrades.IsOn = FreePerfUpgradesToggle.IsOn;
    }

    private static void FreeVisualUpgradesToggled(object s, EventArgs e)
    {
        if (MainWindow.Mw.Gvp.Name == "Forza Horizon 4")
        {
            FreePerfUpgradesToggle.IsEnabled = !FreePerfUpgradesToggle.IsEnabled;
        }
                
        As.FreeVisualUpgrades.IsOn = FreeVisualUpgradeToggle.IsOn;
    }

    private static void QuickAddAllCarsToggled(object s, EventArgs e)
    {
        QuickAddRareCarsToggle.IsEnabled = !QuickAddRareCarsToggle.IsEnabled;
        As.QuickAddAllCars.IsOn = QuickAddAllCarsToggle.IsOn;
    }

    private static void QuickAddRareCarsToggled(object s, EventArgs e)
    {
        QuickAddAllCarsToggle.IsEnabled = !QuickAddAllCarsToggle.IsEnabled;
        As.QuickAddRareCars.IsOn = QuickAddRareCarsToggle.IsOn;
    }
}