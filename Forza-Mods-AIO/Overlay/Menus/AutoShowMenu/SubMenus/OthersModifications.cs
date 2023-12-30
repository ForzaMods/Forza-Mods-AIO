using System.Windows;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;

using static Forza_Mods_AIO.Tabs.AutoShowTab.AutoShow;

namespace Forza_Mods_AIO.Overlay.Menus.AutoShowMenu.SubMenus;

public abstract class OthersModifications
{
    private static readonly ButtonOption FreePerfUpgradesToggle = new ("Free Perf Upgrades", () => As.FreePerfUpgrades_OnToggled(As.FreePerfUpgrades, new RoutedEventArgs()), "This option will make every performance upgrade free. Its not revert able, unless you restart the game");
    private static readonly ButtonOption FreeVisualUpgradeToggle = new ("Free Visual Upgrades", () => As.FreeVisualUpgrades_OnToggled(As.FreeVisualUpgrades, new RoutedEventArgs()), "This option will make every performance upgrade free. Its not revert able, unless you restart the game");
    private static readonly ButtonOption QuickAddAllCarsToggle = new ("Quick Add All Cars", () => As.AddAllCars_OnToggled(As.QuickAddAllCars, new RoutedEventArgs()), "This option will add every car from the autoshow into ur garage, hidden, rare - it doesnt matter");
    private static readonly ButtonOption QuickAddRareCarsToggle = new ("Quick Add Rare Cars", () => As.AddRareCars_OnToggled(As.QuickAddRareCars, new RoutedEventArgs()), "This option will add rare car from the autoshow into ur garage");

    public static readonly List<MenuOption> OthersModificationsOptions = new()
    {
        FreePerfUpgradesToggle,
        FreeVisualUpgradeToggle,
        QuickAddAllCarsToggle,
        QuickAddRareCarsToggle
    };
}