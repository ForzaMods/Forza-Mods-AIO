using System.Windows;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;

using static Forza_Mods_AIO.Tabs.AutoShowTab.AutoShow;

namespace Forza_Mods_AIO.Overlay.Menus.AutoShowMenu.SubMenus;

public abstract class OthersModifications
{
    private static readonly ButtonOption FreePerfUpgradesToggle = new ("Free Perf Upgrades", () => As.FreePerfUpgrades_OnToggled(As.FreePerfUpgrades, new RoutedEventArgs()));
    private static readonly ButtonOption FreeVisualUpgradeToggle = new ("Free Visual Upgrades", () => As.FreeVisualUpgrades_OnToggled(As.FreeVisualUpgrades, new RoutedEventArgs()));
    private static readonly ButtonOption QuickAddAllCarsToggle = new ("Quick Add All Cars", () => As.AddAllCars_OnToggled(As.QuickAddAllCars, new RoutedEventArgs()));
    private static readonly ButtonOption QuickAddRareCarsToggle = new ("Quick Add Rare Cars", () => As.AddRareCars_OnToggled(As.QuickAddRareCars, new RoutedEventArgs()));

    public static readonly List<MenuOption> OthersModificationsOptions = new()
    {
        FreePerfUpgradesToggle,
        FreeVisualUpgradeToggle,
        QuickAddAllCarsToggle,
        QuickAddRareCarsToggle
    };
}