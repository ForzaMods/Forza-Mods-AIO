using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;
using static Forza_Mods_AIO.Tabs.AutoShowTab.AutoShow;

namespace Forza_Mods_AIO.Overlay.Menus.AutoShowMenu.SubMenus;

public abstract class GarageModifications
{
    public static readonly ToggleOption ShowTrafficHsNullToggle = new("Show Traffic/HS/Null", false);
    public static readonly ToggleOption UnlockHiddenDecalsToggle = new("Unlock Hidden Decals", false);
    public static readonly ToggleOption UnlockHiddenPresetsToggle = new("Unlock Hidden Presets", false);
    public static readonly ToggleOption RemoveAnyCarToggle = new("Remove Any Car", false);
    public static readonly ToggleOption FixThumbnailsToggle = new("Fix Thumbnails", false);
    public static readonly ToggleOption PaintLegoCarsToggle = new("Paint Lego Cars", false);
    public static readonly ToggleOption ClearTagToggle = new("Clear \"new\" tag on cars", false);

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
        ShowTrafficHsNullToggle.Toggled += ShowTrafficHsNullToggled;
        UnlockHiddenDecalsToggle.Toggled += UnlockHiddenDecalsToggled;
        UnlockHiddenPresetsToggle.Toggled += UnlockHiddenPresetsToggled;
        RemoveAnyCarToggle.Toggled += RemoveAnyCarToggled;
        PaintLegoCarsToggle.Toggled += PaintLegoCarsToggled;
        FixThumbnailsToggle.Toggled += FixThumbnailsToggled;
        ClearTagToggle.Toggled += ClearTagToggled;
    }

    private static void ShowTrafficHsNullToggled(object s, EventArgs e)
    {
        if (MainWindow.Mw.Gvp.Name == "Forza Horizon 4") 
        {
            AutoshowFilters.FreeCarsToggle.IsEnabled = !AutoshowFilters.FreeCarsToggle.IsEnabled;
            FixThumbnailsToggle.IsEnabled = !FixThumbnailsToggle.IsEnabled;
            UnlockHiddenDecalsToggle.IsEnabled = !UnlockHiddenDecalsToggle.IsEnabled;
            UnlockHiddenPresetsToggle.IsEnabled = !UnlockHiddenPresetsToggle.IsEnabled;
        }            
            
        As.ShowTrafficHsNull.IsOn = ShowTrafficHsNullToggle.IsOn; 
    }

    private static void UnlockHiddenDecalsToggled(object s, EventArgs e)
    {
        if (MainWindow.Mw.Gvp.Name == "Forza Horizon 4") 
        {
            AutoshowFilters.FreeCarsToggle.IsEnabled = !AutoshowFilters.FreeCarsToggle.IsEnabled;
            FixThumbnailsToggle.IsEnabled = !FixThumbnailsToggle.IsEnabled;
            ShowTrafficHsNullToggle.IsEnabled = !ShowTrafficHsNullToggle.IsEnabled;
            UnlockHiddenPresetsToggle.IsEnabled = !UnlockHiddenPresetsToggle.IsEnabled;
        }
            
        As.UnlockHiddenDecals.IsOn = UnlockHiddenDecalsToggle.IsOn;
    }

    private static void UnlockHiddenPresetsToggled(object s, EventArgs e)
    {
        if (MainWindow.Mw.Gvp.Name == "Forza Horizon 4") 
        {
            AutoshowFilters.FreeCarsToggle.IsEnabled = !AutoshowFilters.FreeCarsToggle.IsEnabled;
            FixThumbnailsToggle.IsEnabled = !FixThumbnailsToggle.IsEnabled;
            ShowTrafficHsNullToggle.IsEnabled = !ShowTrafficHsNullToggle.IsEnabled;
            UnlockHiddenDecalsToggle.IsEnabled = !UnlockHiddenDecalsToggle.IsEnabled;
        }
                
        As.UnlockHiddenPresets.IsOn = UnlockHiddenPresetsToggle.IsOn;
    }

    private static void RemoveAnyCarToggled(object s, EventArgs e)
    {
        As.RemoveAnyCar.IsOn = RemoveAnyCarToggle.IsOn;
    }

    private static void PaintLegoCarsToggled(object s, EventArgs e)
    {
        As.PaintLegoCars.IsOn = PaintLegoCarsToggle.IsOn; 
    }

    private static void FixThumbnailsToggled(object s, EventArgs e)
    {
        if (MainWindow.Mw.Gvp.Name == "Forza Horizon 4") 
        {
            AutoshowFilters.FreeCarsToggle.IsEnabled = !AutoshowFilters.FreeCarsToggle.IsEnabled;
            UnlockHiddenPresetsToggle.IsEnabled = !UnlockHiddenPresetsToggle.IsEnabled;
            ShowTrafficHsNullToggle.IsEnabled = !ShowTrafficHsNullToggle.IsEnabled;
            UnlockHiddenDecalsToggle.IsEnabled = !UnlockHiddenDecalsToggle.IsEnabled;
        }
            
        As.FixThumbnails.IsOn = FixThumbnailsToggle.IsOn;
    }
    
    private static void ClearTagToggled(object s, EventArgs e)
    {
        if (MainWindow.Mw.Gvp.Name == "Forza Horizon 4") 
        {
            AutoshowFilters.FreeCarsToggle.IsEnabled = !AutoshowFilters.FreeCarsToggle.IsEnabled;
            UnlockHiddenPresetsToggle.IsEnabled = !UnlockHiddenPresetsToggle.IsEnabled;
            ShowTrafficHsNullToggle.IsEnabled = !ShowTrafficHsNullToggle.IsEnabled;
            UnlockHiddenDecalsToggle.IsEnabled = !UnlockHiddenDecalsToggle.IsEnabled;
        }
            
        As.ClearTag.IsOn = ClearTagToggle.IsOn;
    }
}