using Forza_Mods_AIO.Tabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Forza_Mods_AIO.Overlay.AutoShowMenu.SubMenus
{
    public class OthersModifications
    {
        static Dispatcher dispatcher = Application.Current.Dispatcher;
        static Overlay.MenuOption FreePerfUpgradesToggle = new Overlay.MenuOption("Free Perf Upgrades", "Bool", false);
        static Overlay.MenuOption FreeVisualUpgradeToggle = new Overlay.MenuOption("Free Visual Upgrades", "Bool", false);
        static Overlay.MenuOption QuickAddAllCarsToggle = new Overlay.MenuOption("Quick Add All Cars", "Bool", false);
        static Overlay.MenuOption QuickAddRareCarsToggle = new Overlay.MenuOption("Quick Add Rare Cars", "Bool", false);

        public static List<Overlay.MenuOption> OthersModificationsOptions = new List<Overlay.MenuOption>()
        {
            FreePerfUpgradesToggle,
            FreeVisualUpgradeToggle,
            QuickAddAllCarsToggle,
            QuickAddRareCarsToggle
        };
        public void Initalize()
        {
            FreePerfUpgradesToggle.ValueChangedHandler += new EventHandler(FreePerfUpgradesToggled);
            FreeVisualUpgradeToggle.ValueChangedHandler += new EventHandler(FreeVisualUpgradesToggled);
            QuickAddAllCarsToggle.ValueChangedHandler += new EventHandler(QuickAddAllCarsToggled);
            QuickAddRareCarsToggle.ValueChangedHandler += new EventHandler(QuickAddRareCarsToggled);
        }

        void FreePerfUpgradesToggled(object s, EventArgs e)
        {
            Task.Run(() =>
            {
                dispatcher.BeginInvoke((Action)(() => { AutoShow.AS.FreePerfUpgrades.IsOn = (bool)FreePerfUpgradesToggle.Value; }));

                if ((bool)FreeVisualUpgradeToggle.Value && (bool)FreePerfUpgradesToggle.Value)
                    FreeVisualUpgradeToggle.Value = false;
            });
        }

        void FreeVisualUpgradesToggled(object s, EventArgs e)
        {
            Task.Run(() =>
            {
                dispatcher.BeginInvoke((Action)(() => { AutoShow.AS.FreeVisualUpgrades.IsOn = (bool)FreeVisualUpgradeToggle.Value; }));

                if ((bool)FreePerfUpgradesToggle.Value && (bool)FreeVisualUpgradeToggle.Value)
                    FreePerfUpgradesToggle.Value = false;
            });
        }

        void QuickAddAllCarsToggled(object s, EventArgs e)
        {
            Task.Run(() =>
            {
                dispatcher.BeginInvoke((Action)(() => { AutoShow.AS.QuickAddAllCars.IsOn = (bool)QuickAddAllCarsToggle.Value; }));

                if ((bool)QuickAddRareCarsToggle.Value && (bool)QuickAddAllCarsToggle.Value)
                    QuickAddRareCarsToggle.Value = false;
            });

        }

        void QuickAddRareCarsToggled(object s, EventArgs e)
        {
            Task.Run(() =>
            {
                dispatcher.BeginInvoke((Action)(() => { AutoShow.AS.QuickAddRareCars.IsOn = (bool)QuickAddRareCarsToggle.Value; }));

                if ((bool)QuickAddAllCarsToggle.Value && (bool)QuickAddRareCarsToggle.Value)
                    QuickAddAllCarsToggle.Value = false;
            });

        }
    }
}
