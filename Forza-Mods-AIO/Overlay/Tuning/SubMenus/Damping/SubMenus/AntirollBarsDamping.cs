using System;
using System.Collections.Generic;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Damping.SubMenus;

public abstract class AntirollBarsDamping
{
    private static readonly Overlay.MenuOption FrontAntirollBarMinValue = new ("Min Value", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption FrontAntirollBarMaxValue = new ("Max Value", Overlay.MenuOption.OptionType.Float, 0f);
    
    private static readonly Overlay.MenuOption RearAntirollBarMinValue = new ("Min Value", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption RearAntirollBarMaxValue = new ("Max Value", Overlay.MenuOption.OptionType.Float, 0f);
    
    private static readonly Overlay.MenuOption FrontAntirollBarsPull = new ("Pull values", Overlay.MenuOption.OptionType.Button, new Action(() => { FrontAntirollBarsPullAction(); }));

    private static void FrontAntirollBarsPullAction()
    {
        var Damping = Tabs.Tuning.DropDownTabs.Damping.d;
        
        Damping.Dispatcher.Invoke(() =>
        {
            if (Damping.FrontAntirollMinBox.Value == null || Damping.FrontAntirollMaxBox.Value == null)
                return;

            FrontAntirollBarMinValue.Value = (float)Damping.FrontAntirollMinBox.Value;
            FrontAntirollBarMaxValue.Value = (float)Damping.FrontAntirollMaxBox.Value;
        });
    }

    private static readonly Overlay.MenuOption RearAntirollBarsPull = new ("Pull values", Overlay.MenuOption.OptionType.Button, new Action(() => { RearAntirollBarsPullAction(); }));

    private static void RearAntirollBarsPullAction()
    {
        var Damping = Tabs.Tuning.DropDownTabs.Damping.d;
        
        Damping.Dispatcher.Invoke(() =>
        {
            if (Damping.RearAntirollMinBox.Value == null || Damping.RearAntirollMaxBox.Value == null)
                return;

            RearAntirollBarMinValue.Value = (float)Damping.RearAntirollMinBox.Value;
            RearAntirollBarMaxValue.Value = (float)Damping.RearAntirollMaxBox.Value;
        });
    }
    
    private static void FrontAntirollBarMinValueChanged(object s, EventArgs e)
    {
        var Damping = Tabs.Tuning.DropDownTabs.Damping.d;

        Damping.Dispatcher.Invoke(() =>
        {
            Damping.FrontAntirollMinBox.Value = (float)FrontAntirollBarMinValue.Value;
        });
    }
    
    private static void FrontAntirollBarMaxValueChanged(object s, EventArgs e)
    {
        var Damping = Tabs.Tuning.DropDownTabs.Damping.d;

        Damping.Dispatcher.Invoke(() =>
        {
            Damping.FrontAntirollMaxBox.Value = (float)FrontAntirollBarMaxValue.Value;
        });
    }
    
    private static void RearAntirollBarMinValueChanged(object s, EventArgs e)
    {
        var Damping = Tabs.Tuning.DropDownTabs.Damping.d;

        Damping.Dispatcher.Invoke(() =>
        {
            Damping.RearAntirollMinBox.Value = (float)RearAntirollBarMinValue.Value;
        });
    }
    
    private static void RearAntirollBarMaxValueChanged(object s, EventArgs e)
    {
        var Damping = Tabs.Tuning.DropDownTabs.Damping.d;

        Damping.Dispatcher.Invoke(() =>
        {
            Damping.RearAntirollMaxBox.Value = (float)RearAntirollBarMaxValue.Value;
        });
    }
    
    public static readonly List<Overlay.MenuOption> AntirollBarsDampingOptions = new()
    {
        new("Front Antiroll Bars", Overlay.MenuOption.OptionType.SubHeader),
        FrontAntirollBarMinValue,
        FrontAntirollBarMaxValue,
        FrontAntirollBarsPull,
        new("Rear Antiroll Bars", Overlay.MenuOption.OptionType.SubHeader),
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