using System;
using System.Windows;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;

using static Forza_Mods_AIO.Tabs.AutoShowTab.AutoShow;

namespace Forza_Mods_AIO.Overlay.Menus.AutoShowMenu.SubMenus;

public abstract class GarageModifications
{
    private static readonly ToggleOption ShowTrafficHsNullToggle = new("Show Traffic/HS/Null", false);
    private static readonly ToggleOption UnlockHiddenPresetsToggle = new("Unlock Hidden Presets", false);
    private static readonly ToggleOption RemoveAnyCarToggle = new("Remove Any Car", false);
    private static readonly ButtonOption FixThumbnailsToggle = new("Fix Thumbnails", () => As.FixThumbnails_OnToggled(As.FixThumbnails, new RoutedEventArgs()));
    private static readonly ButtonOption ClearTagToggle = new("Clear \"new\" tag on cars", () => As.ClearTag_OnToggled(As.ClearTag, new RoutedEventArgs()));

    public static readonly List<MenuOption> GarageModificationsOptions = new()
    {
        ShowTrafficHsNullToggle,
        UnlockHiddenPresetsToggle,
        RemoveAnyCarToggle,
        FixThumbnailsToggle,
        ClearTagToggle
    };

    public static void InitiateSubMenu()
    {
        ShowTrafficHsNullToggle.Toggled += ShowTrafficHsNullToggled;
        UnlockHiddenPresetsToggle.Toggled += UnlockHiddenPresetsToggled;
        RemoveAnyCarToggle.Toggled += RemoveAnyCarToggled;
    }

    private static void ShowTrafficHsNullToggled(object s, EventArgs e)
    {
        As.ShowTrafficHsNull.IsOn = ShowTrafficHsNullToggle.IsOn; 
    }

    private static void UnlockHiddenPresetsToggled(object s, EventArgs e)
    {
        As.UnlockHiddenPresets.IsOn = UnlockHiddenPresetsToggle.IsOn;
    }

    private static void RemoveAnyCarToggled(object s, EventArgs e)
    {
        As.RemoveAnyCar.IsOn = RemoveAnyCarToggle.IsOn;
    }
}