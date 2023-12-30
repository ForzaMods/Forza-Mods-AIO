using System;
using System.Windows;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;

using static Forza_Mods_AIO.Tabs.AutoShowTab.AutoShow;

namespace Forza_Mods_AIO.Overlay.Menus.AutoShowMenu.SubMenus;

public abstract class GarageModifications
{
    private static readonly ToggleOption UnlockHiddenPresetsToggle = new("Unlock Hidden Presets", false, "This option unlocks hidden presets from the games missions where you drive a pretuned car");
    private static readonly ToggleOption RemoveAnyCarToggle = new("Remove Any Car", false, "This option allows you to remove any car in your garage.");
    private static readonly ToggleOption CarPassDateBypassToggle = new("Car Pass Date Bypass", false, "This option bypasses date requirements on car pass cars.");
    private static readonly ButtonOption FixThumbnailsButton = new("Fix Thumbnails", () => As.FixThumbnails_OnClicked(As.FixThumbnails, new RoutedEventArgs()), "This option will revert every thumbnail of every car in ur garage to its default");
    private static readonly ButtonOption ClearTagButton = new("Clear \"new\" tag on cars", () => As.ClearTag_OnClicked(As.ClearTag, new RoutedEventArgs()), "This option will remove every the “new” tag and the annoying loading into a new car animation on every car in ur garage");

    public static readonly List<MenuOption> GarageModificationsOptions = new()
    {
        UnlockHiddenPresetsToggle,
        RemoveAnyCarToggle,
        CarPassDateBypassToggle,
        FixThumbnailsButton,
        ClearTagButton
    };

    public static void InitiateSubMenu()
    {
        UnlockHiddenPresetsToggle.Toggled += UnlockHiddenPresetsToggled;
        RemoveAnyCarToggle.Toggled += RemoveAnyCarToggled;
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