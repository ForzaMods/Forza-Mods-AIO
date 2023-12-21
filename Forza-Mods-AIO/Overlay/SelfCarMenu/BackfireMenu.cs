using System;
using System.Collections.Generic;
using static System.Convert;
using static System.Math;
using static Forza_Mods_AIO.Overlay.Overlay;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs.BackFirePage;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu;

public abstract class BackfireMenu
{
    private static readonly MenuOption MinTime = new("Min Value", 1000, 0);
    private static readonly MenuOption MaxTime = new("Max Value", 2500, 0);
    private static readonly MenuOption TimeToggle = new("Toggle",  false);
    
    private static readonly MenuOption AntiLagToggle = new("Force Anti-lag Style",false);
    private static readonly MenuOption NormalToggle = new("Force Normal Style",false);
    
    public static readonly List<MenuOption> BackfireMenuOptions = new()
    {
        new MenuOption("Backfire Time", OptionType.SubHeader),
        MinTime,
        MaxTime,
        TimeToggle,
        new MenuOption("Backfire Toggles", OptionType.SubHeader),
        AntiLagToggle,
        NormalToggle
    };
    
    public static void InitiateSubMenu()
    {
        MinTime.ValueChangedHandler += Time_OnValueChanged;
        MaxTime.ValueChangedHandler += Time_OnValueChanged;
        TimeToggle.ValueChangedHandler += TimeToggle_OnToggled;
        AntiLagToggle.ValueChangedHandler += AntiLagToggle_OnToggled;
        NormalToggle.ValueChangedHandler += NormalToggle_OnToggled;
    }

    private static void Time_OnValueChanged(object? s, EventArgs e)
    {
        switch (s.GetType().GetProperty("Name").GetValue(s))
        {
            case "Min Value":
            {
                BackFire.Dispatcher.Invoke(() => BackFire.MinTime.Value = Round(ToDouble(MinTime.Value)));
                break;
            }
            case "Max Value":
            {
                BackFire.Dispatcher.Invoke(() => BackFire.MaxTime.Value = Round(ToDouble(MaxTime.Value)));
                break;
            }
        }
    }
    
    private static void TimeToggle_OnToggled(object? s, EventArgs e)
    {
        BackFire.Dispatcher.Invoke(() => BackFire.BackfireToggle.IsOn = ToBoolean(TimeToggle.Value));
    }
    
    private static void NormalToggle_OnToggled(object? s, EventArgs e)
    {
        AntiLagToggle.IsEnabled = !AntiLagToggle.IsEnabled;
        BackFire.Dispatcher.Invoke(() => BackFire.ForceNormal.IsOn = ToBoolean(NormalToggle.Value));
    }
    
    private static void AntiLagToggle_OnToggled(object? s, EventArgs e)
    {
        NormalToggle.IsEnabled = !NormalToggle.IsEnabled;
        BackFire.Dispatcher.Invoke(() => BackFire.ForceAntiLag.IsOn = ToBoolean(AntiLagToggle.Value));
    }
    
}