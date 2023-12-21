using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.HandlingMenu;

public abstract class TurnAssistMenu
{
    private static readonly MenuOption TurnAssistRatio = new("Ratio", 1);
    private static readonly MenuOption TurnAssistStrength = new("Strength", 10);
    private static readonly MenuOption TurnAssistInterval = new("Interval", 1);
    private static readonly MenuOption TurnAssistEnable = new("Enable", false);

    public static readonly List<MenuOption> TurnAssistOptions = new()
    {
        new ("Turn Assist", OptionType.SubHeader),
        TurnAssistRatio,
        TurnAssistStrength,
        TurnAssistInterval,
        TurnAssistEnable
    };

    public static void InitiateSubMenu()
    {
        TurnAssistRatio.ValueChangedHandler += TurnAssistRatioChanged;
        TurnAssistStrength.ValueChangedHandler += TurnAssistStrengthChanged;
        TurnAssistInterval.ValueChangedHandler += TurnAssistIntervalChanged;
        TurnAssistEnable.ValueChangedHandler += TurnAssistToggled;
    }

    private static void TurnAssistRatioChanged(object s, EventArgs e)
    {
        HandlingPage.Shp.Dispatcher.Invoke(delegate
        {
            if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! > HandlingPage.Shp.TurnAssistRatioBox.Maximum)
                TurnAssistStrength.Value = (int)HandlingPage.Shp.TurnAssistRatioBox.Maximum;
            else if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! < HandlingPage.Shp.TurnAssistRatioBox.Minimum)
                TurnAssistStrength.Value = (int)HandlingPage.Shp.TurnAssistRatioBox.Minimum;
            else
                HandlingPage.Shp.TurnAssistRatioBox.Value = Convert.ToSingle(s.GetType().GetProperty("Value")!.GetValue(s)!);
        });
    }

    private static void TurnAssistStrengthChanged(object s, EventArgs e)
    {
        HandlingPage.Shp.Dispatcher.Invoke(delegate
        {
            if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! > HandlingPage.Shp.TurnAssistStrengthBox.Maximum)
                TurnAssistStrength.Value = (int)HandlingPage.Shp.TurnAssistStrengthBox.Maximum;
            else if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! < HandlingPage.Shp.TurnAssistStrengthBox.Minimum)
                TurnAssistStrength.Value = (int)HandlingPage.Shp.TurnAssistStrengthBox.Minimum;
            else
                HandlingPage.Shp.TurnAssistStrengthBox.Value = Convert.ToSingle(s.GetType().GetProperty("Value")!.GetValue(s)!);
        });
    }

    private static void TurnAssistIntervalChanged(object s, EventArgs e)
    {
        HandlingPage.Shp.Dispatcher.Invoke(delegate
        {
            if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! > HandlingPage.Shp.TurnAssistIntervalBox.Maximum)
                TurnAssistInterval.Value = (int)HandlingPage.Shp.TurnAssistIntervalBox.Maximum;
            else if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! < HandlingPage.Shp.TurnAssistIntervalBox.Minimum)
                TurnAssistInterval.Value = (int)HandlingPage.Shp.TurnAssistIntervalBox.Minimum;
            else
                HandlingPage.Shp.TurnAssistIntervalBox.Value = (int)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void TurnAssistToggled(object s, EventArgs e)
    {
        HandlingPage.Shp.Dispatcher.Invoke(() =>
        {
            HandlingPage.Shp.WheelSpeedSwitch.IsOn = (bool)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
}