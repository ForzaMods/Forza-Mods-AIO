using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forza_Mods_AIO.Overlay.AutoShowMenu.SubMenus
{
    public class OthersModifications
    {
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

        }

        void FreeVisualUpgradesToggled(object s, EventArgs e)
        {

        }

        void QuickAddAllCarsToggled(object s, EventArgs e)
        {

        }

        void QuickAddRareCarsToggled(object s, EventArgs e)
        {

        }
    }
}
