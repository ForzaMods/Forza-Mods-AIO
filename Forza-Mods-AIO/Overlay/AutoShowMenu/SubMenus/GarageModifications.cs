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
    public class GarageModifications
    {
        static Dispatcher dispatcher = Application.Current.Dispatcher;
        static Overlay.MenuOption ShowTrafficHsNullToggle = new Overlay.MenuOption("Show Traffic/HS/Null", "Bool", false);
        static Overlay.MenuOption UnlockHiddenDecalsToggle = new Overlay.MenuOption("Unlock Hidden Decals", "Bool", false);
        static Overlay.MenuOption UnlockHiddenPresetsToggle = new Overlay.MenuOption("Unlock Hidden Presets", "Bool", false);
        static Overlay.MenuOption RemoveAnyCarToggle = new Overlay.MenuOption("Remove Any Car", "Bool", false);
        static Overlay.MenuOption PaintLegoCarsToggle = new Overlay.MenuOption("Paint Lego Cars", "Bool", false);
        static Overlay.MenuOption ClearGarageToggle = new Overlay.MenuOption("Clear Garage", "Bool", false);
        static Overlay.MenuOption FixThumbnailsToggle = new Overlay.MenuOption("Fix Thumbnails", "Bool", false);

        public static List<Overlay.MenuOption> GarageModificationsOptions = new List<Overlay.MenuOption>()
        {
            ShowTrafficHsNullToggle,
            UnlockHiddenDecalsToggle,
            UnlockHiddenPresetsToggle,
            RemoveAnyCarToggle,
            PaintLegoCarsToggle,
            ClearGarageToggle,
            FixThumbnailsToggle
        };

        public void Initalize()
        {
            ShowTrafficHsNullToggle.ValueChangedHandler += new EventHandler(ShowTrafficHsNullToggled);
            UnlockHiddenDecalsToggle.ValueChangedHandler += new EventHandler(UnlockHiddenDecalsToggled);
            UnlockHiddenPresetsToggle.ValueChangedHandler += new EventHandler(UnlockHiddenPresetsToggled);
            RemoveAnyCarToggle.ValueChangedHandler += new EventHandler(RemoveAnyCarToggled);
            PaintLegoCarsToggle.ValueChangedHandler += new EventHandler(PaintLegoCarsToggled);
            ClearGarageToggle.ValueChangedHandler += new EventHandler(ClearGarageToggled);
            FixThumbnailsToggle.ValueChangedHandler += new EventHandler(FixThumbnailsToggled);
        }

        void ShowTrafficHsNullToggled(object s, EventArgs e)
        {
        }

        void UnlockHiddenDecalsToggled(object s, EventArgs e)
        {

        }

        void UnlockHiddenPresetsToggled(object s, EventArgs e)
        {

        }

        void RemoveAnyCarToggled(object s, EventArgs e)
        {

        }
        void PaintLegoCarsToggled(object s, EventArgs e)
        {

        }

        void ClearGarageToggled(object s, EventArgs e)
        {

        }

        void FixThumbnailsToggled(object s, EventArgs e)
        {

        }
    }
}
