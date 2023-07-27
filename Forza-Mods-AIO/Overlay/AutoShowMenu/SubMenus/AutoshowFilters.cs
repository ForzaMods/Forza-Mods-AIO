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
    public class AutoshowFilters
    {
        static Dispatcher dispatcher = Application.Current.Dispatcher;
        static Overlay.MenuOption AllCarsToggle = new Overlay.MenuOption("All Cars", "Bool", false, "Toggles all cars in the autoshow");
        static Overlay.MenuOption RareCarsToggle = new Overlay.MenuOption("Rare Cars", "Bool", false, "Toggles rare cars in the autoshow");
        static Overlay.MenuOption FreeCarsToggle = new Overlay.MenuOption("Free Cars", "Bool", false, "Toggles free cars in the autoshow");

        public void Initalize()
        {
            AllCarsToggle.ValueChangedHandler += new EventHandler(AllCarsChanged);
            RareCarsToggle.ValueChangedHandler += new EventHandler(RareCarsChanged);
            FreeCarsToggle.ValueChangedHandler += new EventHandler(FreeCarsChanged);
        }

        public static List<Overlay.MenuOption> AutoShowFiltersOptions = new List<Overlay.MenuOption>()
        {
            AllCarsToggle,
            RareCarsToggle,
            FreeCarsToggle
        };

        // Event handlers
        void AllCarsChanged(object s, EventArgs e)
        {
        }
        void RareCarsChanged(object s, EventArgs e)
        {
        }
        void FreeCarsChanged(object s, EventArgs e)
        {
        }
    }
}
