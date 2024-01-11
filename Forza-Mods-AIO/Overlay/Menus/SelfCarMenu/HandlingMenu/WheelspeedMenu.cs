using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs.HandlingPage;

namespace Forza_Mods_AIO.Overlay.Menus.SelfCarMenu.HandlingMenu;

public abstract class WheelspeedMenu
{
    private static readonly string[] WheelspeedSelections = { "Static", "Linear", "Power", "Random", "Jitter", "Pulse", "Sway", "Surge", "Mixed"  };
    private static readonly SelectionOption WheelspeedMode = new("Mode", 0, WheelspeedSelections);
    private static readonly IntOption WheelspeedStrength = new("Strength", 10, Shp.StrengthBox.Minimum, Shp.StrengthBox.Maximum);
    private static readonly IntOption WheelspeedInterval = new("Interval", 1, Shp.IntervalBox.Minimum, Shp.IntervalBox.Maximum);
    private static readonly ToggleOption WheelspeedEnable = new("Enable", false);

    public static readonly List<MenuOption> WheelSpeedOptions = new()
    {
        new SubHeaderOption("Wheelspeed"),
        WheelspeedMode,
        WheelspeedStrength,
        WheelspeedInterval,
        WheelspeedEnable
    };

    public static void InitiateSubMenu()
    {
        WheelspeedMode.SelectionChanged += WheelspeedModeChanged;
        WheelspeedStrength.ValueChanged += WheelspeedStrengthChanged;
        WheelspeedInterval.ValueChanged += WheelspeedIntervalChanged;
        WheelspeedEnable.Toggled += WheelspeedToggled;
    }

    private static void WheelspeedModeChanged(object s, EventArgs e)
    {
        Shp.WheelSpeedModeBox.SelectedIndex = WheelspeedMode.Index;
    }

    private static void WheelspeedStrengthChanged(object s, EventArgs e)
    {
        Shp.StrengthBox.Value = Convert.ToSingle(s.GetType().GetProperty("Value")!.GetValue(s)!);
    }

    private static void WheelspeedIntervalChanged(object s, EventArgs e)
    {
        Shp.IntervalBox.Value = (int)s.GetType().GetProperty("Value")!.GetValue(s)!;
    }

    private static void WheelspeedToggled(object s, EventArgs e)
    {
        Shp.WheelSpeedSwitch.IsOn = (bool)s.GetType().GetProperty("Value")!.GetValue(s)!;
    }
}