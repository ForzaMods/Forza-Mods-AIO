using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Forza_Mods_AIO.Tabs.AutoShowTab;

namespace Forza_Mods_AIO.Overlay.AutoShowMenu.SubMenus
{
    public class GarageModifications
    {
        static Dispatcher dispatcher = Application.Current.Dispatcher;
        public static Overlay.MenuOption ShowTrafficHsNullToggle = new Overlay.MenuOption("Show Traffic/HS/Null", "Bool", false);
        public static Overlay.MenuOption UnlockHiddenDecalsToggle = new Overlay.MenuOption("Unlock Hidden Decals", "Bool", false);
        public static Overlay.MenuOption UnlockHiddenPresetsToggle = new Overlay.MenuOption("Unlock Hidden Presets", "Bool", false);
        public static Overlay.MenuOption RemoveAnyCarToggle = new Overlay.MenuOption("Remove Any Car", "Bool", false);
        public static Overlay.MenuOption PaintLegoCarsToggle = new Overlay.MenuOption("Paint Lego Cars", "Bool", false);
        public static Overlay.MenuOption FixThumbnailsToggle = new Overlay.MenuOption("Fix Thumbnails", "Bool", false);

        public static List<Overlay.MenuOption> GarageModificationsOptions = new List<Overlay.MenuOption>()
        {
            ShowTrafficHsNullToggle,
            UnlockHiddenDecalsToggle,
            UnlockHiddenPresetsToggle,
            RemoveAnyCarToggle,
            PaintLegoCarsToggle,
            FixThumbnailsToggle
        };

        public void Initalize()
        {
            ShowTrafficHsNullToggle.ValueChangedHandler += new EventHandler(ShowTrafficHsNullToggled);
            UnlockHiddenDecalsToggle.ValueChangedHandler += new EventHandler(UnlockHiddenDecalsToggled);
            UnlockHiddenPresetsToggle.ValueChangedHandler += new EventHandler(UnlockHiddenPresetsToggled);
            RemoveAnyCarToggle.ValueChangedHandler += new EventHandler(RemoveAnyCarToggled);
            PaintLegoCarsToggle.ValueChangedHandler += new EventHandler(PaintLegoCarsToggled);
            FixThumbnailsToggle.ValueChangedHandler += new EventHandler(FixThumbnailsToggled);
        }

        void ShowTrafficHsNullToggled(object s, EventArgs e)
        {
            Task.Run(() =>
            {
                AutoshowFilters.FreeCarsToggle.Value = false;
                GarageModifications.FixThumbnailsToggle.Value = false;
                GarageModifications.UnlockHiddenDecalsToggle.Value = false;
                GarageModifications.UnlockHiddenPresetsToggle.Value = false;

                dispatcher.BeginInvoke((Action)(() => { AutoShow.AS.ShowTrafficHSNull.IsOn = (bool)ShowTrafficHsNullToggle.Value; }));
            });
        }

        void UnlockHiddenDecalsToggled(object s, EventArgs e)
        {
            Task.Run(() =>
            {
                AutoshowFilters.FreeCarsToggle.Value = false;
                GarageModifications.FixThumbnailsToggle.Value = false;
                GarageModifications.UnlockHiddenPresetsToggle.Value = false;
                GarageModifications.ShowTrafficHsNullToggle.Value = false;

                dispatcher.BeginInvoke((Action)(() => { AutoShow.AS.UnlockHiddenDecals.IsOn = (bool)UnlockHiddenDecalsToggle.Value; }));
            });
        }

        void UnlockHiddenPresetsToggled(object s, EventArgs e)
        {
            Task.Run(() =>
            {
                AutoshowFilters.FreeCarsToggle.Value = false;
                GarageModifications.FixThumbnailsToggle.Value = false;
                GarageModifications.ShowTrafficHsNullToggle.Value = false;
                GarageModifications.UnlockHiddenPresetsToggle.Value = false;
                

                dispatcher.BeginInvoke((Action)(() => { AutoShow.AS.UnlockHiddenPresets.IsOn = (bool)UnlockHiddenPresetsToggle.Value; }));
            });

        }

        void RemoveAnyCarToggled(object s, EventArgs e)
        {
            Task.Run(() =>
            {
                dispatcher.BeginInvoke((Action)(() => { AutoShow.AS.RemoveAnyCar.IsOn = (bool)RemoveAnyCarToggle.Value; }));
            });
        }
        void PaintLegoCarsToggled(object s, EventArgs e)
        {
            Task.Run(() =>
            {
                dispatcher.BeginInvoke((Action)(() => { AutoShow.AS.PaintLegoCars.IsOn = (bool)PaintLegoCarsToggle.Value; }));
            });
        }

        void FixThumbnailsToggled(object s, EventArgs e)
        {
            Task.Run(() =>
            {
                AutoshowFilters.FreeCarsToggle.Value = false;
                GarageModifications.ShowTrafficHsNullToggle.Value = false;
                GarageModifications.UnlockHiddenPresetsToggle.Value = false;
                GarageModifications.UnlockHiddenDecalsToggle.Value = false;
                
                dispatcher.BeginInvoke((Action)(() => { AutoShow.AS.FixThumbnails.IsOn = (bool)FixThumbnailsToggle.Value; }));
            });
        }
    }
}
