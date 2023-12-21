using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.HandlingMenu;

public abstract class WheelspeedMenu
{
    private static readonly MenuOption WheelspeedMode = new("Mode", 1);
    private static readonly MenuOption WheelspeedStrength = new("Strength", 10);
    private static readonly MenuOption WheelspeedInterval = new("Interval", 1);
    private static readonly MenuOption WheelspeedEnable = new("Enable", false);

    public static readonly List<MenuOption> WheelSpeedOptions = new()
    {
        new ("Wheelspeed", OptionType.SubHeader),
        WheelspeedMode,
        WheelspeedStrength,
        WheelspeedInterval,
        WheelspeedEnable
    };

    public static void InitiateSubMenu()
    {
        WheelspeedMode.ValueChangedHandler += WheelspeedModeChanged;
        WheelspeedStrength.ValueChangedHandler += WheelspeedStrengthChanged;
        WheelspeedInterval.ValueChangedHandler += WheelspeedIntervalChanged;
        WheelspeedEnable.ValueChangedHandler += WheelspeedToggled;
    }

    private static void WheelspeedModeChanged(object s, EventArgs e)
    {
        HandlingPage.Shp.Dispatcher.Invoke(delegate
        {
            if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! > 2)
                WheelspeedMode.Value = 1;
            else if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! < 1)
                WheelspeedMode.Value = 2;
            else
                HandlingPage.Shp.WheelSpeedModeComboBox.SelectedIndex = (int)s.GetType().GetProperty("Value")!.GetValue(s)! - 1;
        });
    }

    private static void WheelspeedStrengthChanged(object s, EventArgs e)
    {
        HandlingPage.Shp.Dispatcher.Invoke(delegate
        {
            if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! > HandlingPage.Shp.StrengthBox.Maximum)
                WheelspeedStrength.Value = (int)HandlingPage.Shp.StrengthBox.Maximum;
            else if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! < HandlingPage.Shp.StrengthBox.Minimum)
                WheelspeedStrength.Value = (int)HandlingPage.Shp.StrengthBox.Minimum;
            else
                HandlingPage.Shp.StrengthBox.Value = Convert.ToSingle(s.GetType().GetProperty("Value")!.GetValue(s)!);
        });
    }

    private static void WheelspeedIntervalChanged(object s, EventArgs e)
    {
        HandlingPage.Shp.Dispatcher.Invoke(delegate
        {
            if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! > HandlingPage.Shp.IntervalBox.Maximum)
                WheelspeedInterval.Value = (int)HandlingPage.Shp.IntervalBox.Maximum;
            else if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! < HandlingPage.Shp.IntervalBox.Minimum)
                WheelspeedInterval.Value = (int)HandlingPage.Shp.IntervalBox.Minimum;
            else
                HandlingPage.Shp.IntervalBox.Value = (int)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void WheelspeedToggled(object s, EventArgs e)
    {
        HandlingPage.Shp.Dispatcher.Invoke(() =>
        {
            HandlingPage.Shp.WheelSpeedSwitch.IsOn = (bool)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
}