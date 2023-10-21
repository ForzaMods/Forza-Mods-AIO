using Forza_Mods_AIO.Tabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Forza_Mods_AIO.Tabs.AutoShowTab;

namespace Forza_Mods_AIO.Overlay.AutoShowMenu.SubMenus
{
    public class AutoshowFilters
    {
        static Dispatcher dispatcher = Application.Current.Dispatcher;
        private static readonly Overlay.MenuOption AllCarsToggle = new Overlay.MenuOption("All Cars", "Bool", false, "Toggles all cars in the autoshow");
        private static readonly Overlay.MenuOption RareCarsToggle = new Overlay.MenuOption("Rare Cars", "Bool", false, "Toggles rare cars in the autoshow");
        public static readonly Overlay.MenuOption FreeCarsToggle = new Overlay.MenuOption("Free Cars", "Bool", false, "Toggles free cars in the autoshow");

        public void Initalize()
        {
            AllCarsToggle.ValueChangedHandler += new EventHandler(AllCarsChanged);
            RareCarsToggle.ValueChangedHandler += new EventHandler(RareCarsChanged);
            FreeCarsToggle.ValueChangedHandler += new EventHandler(FreeCarsChanged);
        }

        public static readonly List<Overlay.MenuOption> AutoShowFiltersOptions = new List<Overlay.MenuOption>()
        {
            AllCarsToggle,
            RareCarsToggle,
            FreeCarsToggle
        };

        // Event handlers
        void AllCarsChanged(object s, EventArgs e)
        {
            Task.Run(() =>
            {
                if ((bool)RareCarsToggle.Value && (bool)AllCarsToggle.Value)
                    RareCarsToggle.Value = false;

                dispatcher.BeginInvoke((Action)(() => { AutoShow.AS.ToggleAllCars.IsOn = (bool)AllCarsToggle.Value; }));
            });
        }
        void RareCarsChanged(object s, EventArgs e)
        {
            Task.Run(() =>
            {
                if ((bool)AllCarsToggle.Value && (bool)RareCarsToggle.Value)
                    AllCarsToggle.Value = false;

                dispatcher.BeginInvoke((Action)(() => { AutoShow.AS.ToggleRareCars.IsOn = (bool)RareCarsToggle.Value; }));
            });
        }
        void FreeCarsChanged(object s, EventArgs e)
        {
            var GA_M = new GarageModifications();

            Task.Run(() =>
            {
                if (((bool)GarageModifications.UnlockHiddenDecalsToggle.Value || (bool)GarageModifications.FixThumbnailsToggle.Value || (bool)GarageModifications.ShowTrafficHsNullToggle.Value) && (bool)FreeCarsToggle.Value)
                {
                    GarageModifications.UnlockHiddenDecalsToggle.Value = false;
                    GarageModifications.FixThumbnailsToggle.Value = false;
                    GarageModifications.ShowTrafficHsNullToggle.Value = false;
                }

                dispatcher.BeginInvoke((Action)(() => { AutoShow.AS.ToggleFreeCars.IsOn = (bool)FreeCarsToggle.Value; }));
            });
        }
    }
}
