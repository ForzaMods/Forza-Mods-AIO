using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs.HandlingPage;

namespace Forza_Mods_AIO.Overlay.Menus.SelfCarMenu.HandlingMenu;

public abstract class TurnAssistMenu
{
    private static readonly IntOption TurnAssistRatio = new("Ratio", 1, Shp.TurnAssistRatioBox.Minimum, Shp.TurnAssistRatioBox.Maximum);
    private static readonly IntOption TurnAssistStrength = new("Strength", 10, Shp.TurnAssistStrengthBox.Minimum, Shp.TurnAssistStrengthBox.Maximum);
    private static readonly IntOption TurnAssistInterval = new("Interval", 1, Shp.TurnAssistIntervalBox.Minimum, Shp.TurnAssistIntervalBox.Maximum);
    private static readonly ToggleOption TurnAssistEnable = new("Enable", false);

    public static readonly List<MenuOption> TurnAssistOptions = new()
    {
        new SubHeaderOption("Turn Assist"),
        TurnAssistRatio,
        TurnAssistStrength,
        TurnAssistInterval,
        TurnAssistEnable
    };

    public static void InitiateSubMenu()
    {
        TurnAssistRatio.ValueChangedEventHandler += TurnAssistRatioChanged;
        TurnAssistStrength.ValueChangedEventHandler += TurnAssistStrengthChanged;
        TurnAssistInterval.ValueChangedEventHandler += TurnAssistIntervalChanged;
        TurnAssistEnable.ToggledEventHandler += TurnAssistToggled;
    }

    private static void TurnAssistRatioChanged(object s, EventArgs e)
    {
        if (s is not IntOption intOption)
        {
            return;
        }

        Shp.TurnAssistRatioBox.Value = intOption.Value;
    }

    private static void TurnAssistStrengthChanged(object s, EventArgs e)
    {
        if (s is not IntOption intOption)
        {
            return;
        }

        Shp.TurnAssistStrengthBox.Value = intOption.Value;
    }

    private static void TurnAssistIntervalChanged(object s, EventArgs e)
    {
        if (s is not IntOption intOption)
        {
            return;
        }

        Shp.TurnAssistIntervalBox.Value = intOption.Value;
    }

    private static void TurnAssistToggled(object s, EventArgs e)
    {
        if (s is not ToggleOption toggleOption)
        {
            return;
        }
        
        Shp.WheelSpeedSwitch.IsOn = toggleOption.IsOn;
    }
}