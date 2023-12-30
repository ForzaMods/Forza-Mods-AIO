using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;
using static System.Convert;
using static System.Math;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs.BackFirePage;

namespace Forza_Mods_AIO.Overlay.Menus.SelfCarMenu;

public abstract class BackfireMenu
{
    private static readonly IntOption MinTime = IntOption.CreateWithMinimum("Min Value", 1000, 0);
    private static readonly IntOption MaxTime = IntOption.CreateWithMinimum("Max Value", 2500, 0);
    private static readonly ToggleOption TimeToggle = new("Toggle",  false, "Toggle to enable or disable custom backfire time settings");
    
    private static readonly ToggleOption AntiLagToggle = new("Force Anti-lag Style",false);
    private static readonly ToggleOption NormalToggle = new("Force Normal Style",false);
    
    public static readonly List<MenuOption> BackfireMenuOptions = new()
    {
        new SubHeaderOption("Backfire Time"),
        MinTime,
        MaxTime,
        TimeToggle,
        new SubHeaderOption("Backfire Toggles"),
        AntiLagToggle,
        NormalToggle
    };
    
    public static void InitiateSubMenu()
    {
        MinTime.ValueChanged += Time_OnValueChanged;
        MaxTime.ValueChanged += Time_OnValueChanged;
        TimeToggle.Toggled += TimeToggle_OnToggled;
        AntiLagToggle.Toggled += AntiLagToggle_OnToggled;
        NormalToggle.Toggled += NormalToggle_OnToggled;
    }

    private static void Time_OnValueChanged(object? s, EventArgs e)
    {
        if (s is not IntOption intOption)
        {
            return;
        }
        
        switch (intOption.Name)
        {
            case "Min Value":
            {
                BackFire.MinTime.Value = Round(ToDouble(MinTime.Value));
                break;
            }
            case "Max Value":
            {
                BackFire.MaxTime.Value = Round(ToDouble(MaxTime.Value));
                break;
            }
        }
    }
    
    private static void TimeToggle_OnToggled(object? s, EventArgs e)
    {
        BackFire.BackfireToggle.IsOn = TimeToggle.IsOn;
    }
    
    private static void NormalToggle_OnToggled(object? s, EventArgs e)
    {
        AntiLagToggle.IsEnabled = !AntiLagToggle.IsEnabled;
        BackFire.ForceNormal.IsOn = NormalToggle.IsOn;
    }
    
    private static void AntiLagToggle_OnToggled(object? s, EventArgs e)
    {
        NormalToggle.IsEnabled = !NormalToggle.IsEnabled;
        BackFire.ForceAntiLag.IsOn = AntiLagToggle.IsOn;
    }
    
}