using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.HandlingMenu;

public abstract class TurnAssistMenu
{
    private static readonly Overlay.MenuOption TurnAssistRatio = new("Ratio", Overlay.MenuOption.OptionType.Int, 1);
    private static readonly Overlay.MenuOption TurnAssistStrength = new("Strength", Overlay.MenuOption.OptionType.Int, 10);
    private static readonly Overlay.MenuOption TurnAssistInterval = new("Interval", Overlay.MenuOption.OptionType.Int, 1);
    private static readonly Overlay.MenuOption TurnAssistEnable = new("Enable", Overlay.MenuOption.OptionType.Bool, false);

    public static readonly List<Overlay.MenuOption> TurnAssistOptions = new()
    {
        new ("Turn Assist", Overlay.MenuOption.OptionType.SubHeader),
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
        HandlingPage.shp.Dispatcher.Invoke(delegate
        {
            if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! > HandlingPage.shp.TurnAssistRatioBox.Maximum)
                TurnAssistStrength.Value = (int)HandlingPage.shp.TurnAssistRatioBox.Maximum;
            else if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! < HandlingPage.shp.TurnAssistRatioBox.Minimum)
                TurnAssistStrength.Value = (int)HandlingPage.shp.TurnAssistRatioBox.Minimum;
            else
                HandlingPage.shp.TurnAssistRatioBox.Value = Convert.ToSingle(s.GetType().GetProperty("Value")!.GetValue(s)!);
        });
    }

    private static void TurnAssistStrengthChanged(object s, EventArgs e)
    {
        HandlingPage.shp.Dispatcher.Invoke(delegate
        {
            if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! > HandlingPage.shp.TurnAssistStrengthBox.Maximum)
                TurnAssistStrength.Value = (int)HandlingPage.shp.TurnAssistStrengthBox.Maximum;
            else if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! < HandlingPage.shp.TurnAssistStrengthBox.Minimum)
                TurnAssistStrength.Value = (int)HandlingPage.shp.TurnAssistStrengthBox.Minimum;
            else
                HandlingPage.shp.TurnAssistStrengthBox.Value = Convert.ToSingle(s.GetType().GetProperty("Value")!.GetValue(s)!);
        });
    }

    private static void TurnAssistIntervalChanged(object s, EventArgs e)
    {
        HandlingPage.shp.Dispatcher.Invoke(delegate
        {
            if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! > HandlingPage.shp.TurnAssistIntervalBox.Maximum)
                TurnAssistInterval.Value = (int)HandlingPage.shp.TurnAssistIntervalBox.Maximum;
            else if ((int)s.GetType().GetProperty("Value")!.GetValue(s)! < HandlingPage.shp.TurnAssistIntervalBox.Minimum)
                TurnAssistInterval.Value = (int)HandlingPage.shp.TurnAssistIntervalBox.Minimum;
            else
                HandlingPage.shp.TurnAssistIntervalBox.Value = (int)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }

    private static void TurnAssistToggled(object s, EventArgs e)
    {
        HandlingPage.shp.Dispatcher.Invoke(() =>
        {
            HandlingPage.shp.WheelSpeedSwitch.IsOn = (bool)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
}