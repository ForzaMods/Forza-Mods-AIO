using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Tabs.AutoShowTab;

namespace Forza_Mods_AIO.Overlay.AutoShowMenu.SubMenus;

public abstract class GarageModifications
{
    public static readonly Overlay.MenuOption ShowTrafficHsNullToggle = new("Show Traffic/HS/Null", Overlay.MenuOption.OptionType.Bool, false);
    public static readonly Overlay.MenuOption UnlockHiddenDecalsToggle = new("Unlock Hidden Decals", Overlay.MenuOption.OptionType.Bool, false);
    public static readonly Overlay.MenuOption UnlockHiddenPresetsToggle = new("Unlock Hidden Presets", Overlay.MenuOption.OptionType.Bool, false);
    public static readonly Overlay.MenuOption RemoveAnyCarToggle = new("Remove Any Car", Overlay.MenuOption.OptionType.Bool, false);
    public static readonly Overlay.MenuOption FixThumbnailsToggle = new("Fix Thumbnails", Overlay.MenuOption.OptionType.Bool, false);
    public static readonly Overlay.MenuOption PaintLegoCarsToggle = new("Paint Lego Cars", Overlay.MenuOption.OptionType.Bool, false);

    public static readonly List<Overlay.MenuOption> GarageModificationsOptions = new()
    {
        ShowTrafficHsNullToggle,
        UnlockHiddenDecalsToggle,
        UnlockHiddenPresetsToggle,
        RemoveAnyCarToggle,
        PaintLegoCarsToggle,
        FixThumbnailsToggle
    };

    public static void InitiateSubMenu()
    {
        ShowTrafficHsNullToggle.ValueChangedHandler += ShowTrafficHsNullToggled;
        UnlockHiddenDecalsToggle.ValueChangedHandler += UnlockHiddenDecalsToggled;
        UnlockHiddenPresetsToggle.ValueChangedHandler += UnlockHiddenPresetsToggled;
        RemoveAnyCarToggle.ValueChangedHandler += RemoveAnyCarToggled;
        PaintLegoCarsToggle.ValueChangedHandler += PaintLegoCarsToggled;
        FixThumbnailsToggle.ValueChangedHandler += FixThumbnailsToggled;
    }

    private static void ShowTrafficHsNullToggled(object s, EventArgs e)
    {
        AutoShow.AS.Dispatcher.Invoke((Action)(() =>
        {
            if (MainWindow.mw.gvp.Name == "Forza Horizon 4") 
            {
                AutoshowFilters.FreeCarsToggle.IsEnabled = !AutoshowFilters.FreeCarsToggle.IsEnabled;
                FixThumbnailsToggle.IsEnabled = !FixThumbnailsToggle.IsEnabled;
                UnlockHiddenDecalsToggle.IsEnabled = !UnlockHiddenDecalsToggle.IsEnabled;
                UnlockHiddenPresetsToggle.IsEnabled = !UnlockHiddenPresetsToggle.IsEnabled;
            }            
            
            AutoShow.AS.ShowTrafficHSNull.IsOn = (bool)ShowTrafficHsNullToggle.Value; 
        }));
    }

    private static void UnlockHiddenDecalsToggled(object s, EventArgs e)
    {
        AutoShow.AS.Dispatcher.Invoke(() =>
        {
            if (MainWindow.mw.gvp.Name == "Forza Horizon 4") 
            {
                AutoshowFilters.FreeCarsToggle.IsEnabled = !AutoshowFilters.FreeCarsToggle.IsEnabled;
                FixThumbnailsToggle.IsEnabled = !FixThumbnailsToggle.IsEnabled;
                ShowTrafficHsNullToggle.IsEnabled = !ShowTrafficHsNullToggle.IsEnabled;
                UnlockHiddenPresetsToggle.IsEnabled = !UnlockHiddenPresetsToggle.IsEnabled;
            }
            
            AutoShow.AS.UnlockHiddenDecals.IsOn = (bool)UnlockHiddenDecalsToggle.Value;
        });
    }

    private static void UnlockHiddenPresetsToggled(object s, EventArgs e)
    {
        AutoShow.AS.Dispatcher.Invoke(() =>
        {
            if (MainWindow.mw.gvp.Name == "Forza Horizon 4") 
            {
                AutoshowFilters.FreeCarsToggle.IsEnabled = !AutoshowFilters.FreeCarsToggle.IsEnabled;
                FixThumbnailsToggle.IsEnabled = !FixThumbnailsToggle.IsEnabled;
                ShowTrafficHsNullToggle.IsEnabled = !ShowTrafficHsNullToggle.IsEnabled;
                UnlockHiddenDecalsToggle.IsEnabled = !UnlockHiddenDecalsToggle.IsEnabled;
            }
                
            AutoShow.AS.UnlockHiddenPresets.IsOn = (bool)UnlockHiddenPresetsToggle.Value;
        });
    }

    private static void RemoveAnyCarToggled(object s, EventArgs e)
    {
        AutoShow.AS.Dispatcher.Invoke(() =>
        {
            AutoShow.AS.RemoveAnyCar.IsOn = (bool)RemoveAnyCarToggle.Value;
        });
    }

    private static void PaintLegoCarsToggled(object s, EventArgs e)
    {
        AutoShow.AS.Dispatcher.Invoke(() =>
        {
            AutoShow.AS.PaintLegoCars.IsOn = (bool)PaintLegoCarsToggle.Value; 
        });
    }

    private static void FixThumbnailsToggled(object s, EventArgs e)
    {
        AutoShow.AS.Dispatcher.Invoke(() =>
        {
            if (MainWindow.mw.gvp.Name == "Forza Horizon 4") 
            {
                AutoshowFilters.FreeCarsToggle.IsEnabled = !AutoshowFilters.FreeCarsToggle.IsEnabled;
                UnlockHiddenPresetsToggle.IsEnabled = !UnlockHiddenPresetsToggle.IsEnabled;
                ShowTrafficHsNullToggle.IsEnabled = !ShowTrafficHsNullToggle.IsEnabled;
                UnlockHiddenDecalsToggle.IsEnabled = !UnlockHiddenDecalsToggle.IsEnabled;
            }
            
            AutoShow.AS.FixThumbnails.IsOn = (bool)FixThumbnailsToggle.Value;
        });
    }
}