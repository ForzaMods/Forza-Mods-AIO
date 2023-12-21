using System;
using System.Collections.Generic;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Damping.SubMenus;

public abstract class AntirollBarsDamping
{
    private static readonly MenuOption FrontAntirollBarMinValue = new ("Min Value", 0f);
    private static readonly MenuOption FrontAntirollBarMaxValue = new ("Max Value", 0f);
    
    private static readonly MenuOption RearAntirollBarMinValue = new ("Min Value", 0f);
    private static readonly MenuOption RearAntirollBarMaxValue = new ("Max Value", 0f);
    
    private static readonly MenuOption FrontAntirollBarsPull = new ("Pull values",  new Action(() => { FrontAntirollBarsPullAction(); }));

    private static void FrontAntirollBarsPullAction()
    {
        var damping = Tabs.Tuning.DropDownTabs.Damping.D;
        
        damping.Dispatcher.Invoke(() =>
        {
            if (damping.FrontAntirollMinBox.Value == null || damping.FrontAntirollMaxBox.Value == null)
                return;

            FrontAntirollBarMinValue.Value = (float)damping.FrontAntirollMinBox.Value;
            FrontAntirollBarMaxValue.Value = (float)damping.FrontAntirollMaxBox.Value;
        });
    }

    private static readonly MenuOption RearAntirollBarsPull = new ("Pull values",  new Action(() => { RearAntirollBarsPullAction(); }));

    private static void RearAntirollBarsPullAction()
    {
        var damping = Tabs.Tuning.DropDownTabs.Damping.D;
        
        damping.Dispatcher.Invoke(() =>
        {
            if (damping.RearAntirollMinBox.Value == null || damping.RearAntirollMaxBox.Value == null)
                return;

            RearAntirollBarMinValue.Value = (float)damping.RearAntirollMinBox.Value;
            RearAntirollBarMaxValue.Value = (float)damping.RearAntirollMaxBox.Value;
        });
    }
    
    private static void FrontAntirollBarMinValueChanged(object s, EventArgs e)
    {
        var damping = Tabs.Tuning.DropDownTabs.Damping.D;

        damping.Dispatcher.Invoke(() =>
        {
            damping.FrontAntirollMinBox.Value = (float)FrontAntirollBarMinValue.Value;
        });
    }
    
    private static void FrontAntirollBarMaxValueChanged(object s, EventArgs e)
    {
        var damping = Tabs.Tuning.DropDownTabs.Damping.D;

        damping.Dispatcher.Invoke(() =>
        {
            damping.FrontAntirollMaxBox.Value = (float)FrontAntirollBarMaxValue.Value;
        });
    }
    
    private static void RearAntirollBarMinValueChanged(object s, EventArgs e)
    {
        var damping = Tabs.Tuning.DropDownTabs.Damping.D;

        damping.Dispatcher.Invoke(() =>
        {
            damping.RearAntirollMinBox.Value = (float)RearAntirollBarMinValue.Value;
        });
    }
    
    private static void RearAntirollBarMaxValueChanged(object s, EventArgs e)
    {
        var damping = Tabs.Tuning.DropDownTabs.Damping.D;

        damping.Dispatcher.Invoke(() =>
        {
            damping.RearAntirollMaxBox.Value = (float)RearAntirollBarMaxValue.Value;
        });
    }
    
    public static readonly List<MenuOption> AntirollBarsDampingOptions = new()
    {
        new("Front Antiroll Bars", OptionType.SubHeader),
        FrontAntirollBarMinValue,
        FrontAntirollBarMaxValue,
        FrontAntirollBarsPull,
        new("Rear Antiroll Bars", OptionType.SubHeader),
        RearAntirollBarMinValue,
        RearAntirollBarMaxValue,
        RearAntirollBarsPull
    };

    public static void InitiateSubMenu()
    {
        FrontAntirollBarMinValue.ValueChangedHandler += FrontAntirollBarMinValueChanged;
        FrontAntirollBarMaxValue.ValueChangedHandler += FrontAntirollBarMaxValueChanged;
        RearAntirollBarMinValue.ValueChangedHandler += RearAntirollBarMinValueChanged;
        RearAntirollBarMaxValue.ValueChangedHandler += RearAntirollBarMaxValueChanged;
    }
}