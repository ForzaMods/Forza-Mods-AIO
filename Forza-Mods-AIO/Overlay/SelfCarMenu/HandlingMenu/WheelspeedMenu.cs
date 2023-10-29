using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.HandlingMenu;

public abstract class WheelspeedMenu
{
    private static readonly Overlay.MenuOption WheelspeedMode = new("Mode", Overlay.MenuOption.OptionType.Int, 1);
    private static readonly Overlay.MenuOption WheelspeedStrength = new("Strength", Overlay.MenuOption.OptionType.Int, 10);
    private static readonly Overlay.MenuOption WheelspeedInterval = new("Interval", Overlay.MenuOption.OptionType.Int, 1);
    private static readonly Overlay.MenuOption WheelspeedEnable = new("Enable", Overlay.MenuOption.OptionType.Bool, false);

    public static readonly List<Overlay.MenuOption> WheelSpeedOptions = new()
    {
        new ("Wheelspeed", Overlay.MenuOption.OptionType.SubHeader),
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
        HandlingPage.shp.Dispatcher.Invoke(delegate
        {
            if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! > 2)
                WheelspeedMode.Value = 1;
            else if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! < 1)
                WheelspeedMode.Value = 2;
            else
                HandlingPage.shp.WheelSpeedModeComboBox.SelectedIndex = (int)s.GetType().GetProperty("Value")!.GetValue(s)! - 1;
        });
    }

    private static void WheelspeedStrengthChanged(object s, EventArgs e)
    {
        HandlingPage.shp.Dispatcher.Invoke(delegate
        {
            if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! > HandlingPage.shp.Var1NumBox.Maximum)
                WheelspeedStrength.Value = (int)HandlingPage.shp.Var1NumBox.Maximum;
            else if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! < HandlingPage.shp.Var1NumBox.Minimum)
                WheelspeedStrength.Value = (int)HandlingPage.shp.Var1NumBox.Minimum;
            else
                HandlingPage.shp.Var1NumBox.Value = Convert.ToSingle(s.GetType().GetProperty("Value")!.GetValue(s)!);
        });
    }

    private static void WheelspeedIntervalChanged(object s, EventArgs e)
    {
        HandlingPage.shp.Dispatcher.Invoke(delegate
        {
            if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! > HandlingPage.shp.Var2NumBox.Maximum)
                WheelspeedInterval.Value = (int)HandlingPage.shp.Var2NumBox.Maximum;
            else if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! < HandlingPage.shp.Var2NumBox.Minimum)
                WheelspeedInterval.Value = (int)HandlingPage.shp.Var2NumBox.Minimum;
            else
                HandlingPage.shp.Var2NumBox.Value = (int)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void WheelspeedToggled(object s, EventArgs e)
    {
        HandlingPage.shp.Dispatcher.Invoke(() =>
        {
            HandlingPage.shp.WheelSpeedSwitch.IsOn = (bool)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
}