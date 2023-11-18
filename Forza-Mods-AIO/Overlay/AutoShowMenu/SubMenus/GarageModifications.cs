using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Tabs.AutoShowTab;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.AutoShowMenu.SubMenus;

public abstract class GarageModifications
{
    public static readonly MenuOption ShowTrafficHsNullToggle = new("Show Traffic/HS/Null", OptionType.Bool, false);
    public static readonly MenuOption UnlockHiddenDecalsToggle = new("Unlock Hidden Decals", OptionType.Bool, false);
    public static readonly MenuOption UnlockHiddenPresetsToggle = new("Unlock Hidden Presets", OptionType.Bool, false);
    public static readonly MenuOption RemoveAnyCarToggle = new("Remove Any Car", OptionType.Bool, false);
    public static readonly MenuOption FixThumbnailsToggle = new("Fix Thumbnails", OptionType.Bool, false);
    public static readonly MenuOption PaintLegoCarsToggle = new("Paint Lego Cars", OptionType.Bool, false);
    public static readonly MenuOption ClearTagToggle = new("Clear \"new\" tag on cars", OptionType.Bool, false);

    public static readonly List<MenuOption> GarageModificationsOptions = new()
    {
        ShowTrafficHsNullToggle,
        UnlockHiddenDecalsToggle,
        UnlockHiddenPresetsToggle,
        RemoveAnyCarToggle,
        PaintLegoCarsToggle,
        FixThumbnailsToggle,
        ClearTagToggle
    };

    public static void InitiateSubMenu()
    {
        ShowTrafficHsNullToggle.ValueChangedHandler += ShowTrafficHsNullToggled;
        UnlockHiddenDecalsToggle.ValueChangedHandler += UnlockHiddenDecalsToggled;
        UnlockHiddenPresetsToggle.ValueChangedHandler += UnlockHiddenPresetsToggled;
        RemoveAnyCarToggle.ValueChangedHandler += RemoveAnyCarToggled;
        PaintLegoCarsToggle.ValueChangedHandler += PaintLegoCarsToggled;
        FixThumbnailsToggle.ValueChangedHandler += FixThumbnailsToggled;
        ClearTagToggle.ValueChangedHandler += ClearTagToggled;
    }

    private static void ShowTrafficHsNullToggled(object s, EventArgs e)
    {
        AutoShow.As.Dispatcher.Invoke((Action)(() =>
        {
            if (MainWindow.Mw.Gvp.Name == "Forza Horizon 4") 
            {
                AutoshowFilters.FreeCarsToggle.IsEnabled = !AutoshowFilters.FreeCarsToggle.IsEnabled;
                FixThumbnailsToggle.IsEnabled = !FixThumbnailsToggle.IsEnabled;
                UnlockHiddenDecalsToggle.IsEnabled = !UnlockHiddenDecalsToggle.IsEnabled;
                UnlockHiddenPresetsToggle.IsEnabled = !UnlockHiddenPresetsToggle.IsEnabled;
            }            
            
            AutoShow.As.ShowTrafficHsNull.IsOn = (bool)ShowTrafficHsNullToggle.Value; 
        }));
    }

    private static void UnlockHiddenDecalsToggled(object s, EventArgs e)
    {
        AutoShow.As.Dispatcher.Invoke(() =>
        {
            if (MainWindow.Mw.Gvp.Name == "Forza Horizon 4") 
            {
                AutoshowFilters.FreeCarsToggle.IsEnabled = !AutoshowFilters.FreeCarsToggle.IsEnabled;
                FixThumbnailsToggle.IsEnabled = !FixThumbnailsToggle.IsEnabled;
                ShowTrafficHsNullToggle.IsEnabled = !ShowTrafficHsNullToggle.IsEnabled;
                UnlockHiddenPresetsToggle.IsEnabled = !UnlockHiddenPresetsToggle.IsEnabled;
            }
            
            AutoShow.As.UnlockHiddenDecals.IsOn = (bool)UnlockHiddenDecalsToggle.Value;
        });
    }

    private static void UnlockHiddenPresetsToggled(object s, EventArgs e)
    {
        AutoShow.As.Dispatcher.Invoke(() =>
        {
            if (MainWindow.Mw.Gvp.Name == "Forza Horizon 4") 
            {
                AutoshowFilters.FreeCarsToggle.IsEnabled = !AutoshowFilters.FreeCarsToggle.IsEnabled;
                FixThumbnailsToggle.IsEnabled = !FixThumbnailsToggle.IsEnabled;
                ShowTrafficHsNullToggle.IsEnabled = !ShowTrafficHsNullToggle.IsEnabled;
                UnlockHiddenDecalsToggle.IsEnabled = !UnlockHiddenDecalsToggle.IsEnabled;
            }
                
            AutoShow.As.UnlockHiddenPresets.IsOn = (bool)UnlockHiddenPresetsToggle.Value;
        });
    }

    private static void RemoveAnyCarToggled(object s, EventArgs e)
    {
        AutoShow.As.Dispatcher.Invoke(() =>
        {
            AutoShow.As.RemoveAnyCar.IsOn = (bool)RemoveAnyCarToggle.Value;
        });
    }

    private static void PaintLegoCarsToggled(object s, EventArgs e)
    {
        AutoShow.As.Dispatcher.Invoke(() =>
        {
            AutoShow.As.PaintLegoCars.IsOn = (bool)PaintLegoCarsToggle.Value; 
        });
    }

    private static void FixThumbnailsToggled(object s, EventArgs e)
    {
        AutoShow.As.Dispatcher.Invoke(() =>
        {
            if (MainWindow.Mw.Gvp.Name == "Forza Horizon 4") 
            {
                AutoshowFilters.FreeCarsToggle.IsEnabled = !AutoshowFilters.FreeCarsToggle.IsEnabled;
                UnlockHiddenPresetsToggle.IsEnabled = !UnlockHiddenPresetsToggle.IsEnabled;
                ShowTrafficHsNullToggle.IsEnabled = !ShowTrafficHsNullToggle.IsEnabled;
                UnlockHiddenDecalsToggle.IsEnabled = !UnlockHiddenDecalsToggle.IsEnabled;
            }
            
            AutoShow.As.FixThumbnails.IsOn = (bool)FixThumbnailsToggle.Value;
        });
    }
    
    private static void ClearTagToggled(object s, EventArgs e)
    {
        AutoShow.As.Dispatcher.Invoke(() =>
        {
            if (MainWindow.Mw.Gvp.Name == "Forza Horizon 4") 
            {
                AutoshowFilters.FreeCarsToggle.IsEnabled = !AutoshowFilters.FreeCarsToggle.IsEnabled;
                UnlockHiddenPresetsToggle.IsEnabled = !UnlockHiddenPresetsToggle.IsEnabled;
                ShowTrafficHsNullToggle.IsEnabled = !ShowTrafficHsNullToggle.IsEnabled;
                UnlockHiddenDecalsToggle.IsEnabled = !UnlockHiddenDecalsToggle.IsEnabled;
            }
            
            AutoShow.As.ClearTag.IsOn = (bool)ClearTagToggle.Value;
        });
    }
}