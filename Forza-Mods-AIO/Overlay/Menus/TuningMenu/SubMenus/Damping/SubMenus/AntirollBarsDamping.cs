using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;
using static Forza_Mods_AIO.Tabs.Tuning.DropDownTabs.Damping;

namespace Forza_Mods_AIO.Overlay.Menus.TuningMenu.SubMenus.Damping.SubMenus;

public abstract class AntirollBarsDamping
{
    private static readonly FloatOption FrontAntirollBarMinValue = new("Min Value", 0f);
    private static readonly FloatOption FrontAntirollBarMaxValue = new("Max Value", 0f);
    
    private static readonly FloatOption RearAntirollBarMinValue = new("Min Value", 0f);
    private static readonly FloatOption RearAntirollBarMaxValue = new("Max Value", 0f);
    
    private static readonly ButtonOption FrontAntirollBarsPull = new("Pull values", FrontAntirollBarsPullAction);
    private static readonly ButtonOption RearAntirollBarsPull = new("Pull values", RearAntirollBarsPullAction);
    
    private static void FrontAntirollBarsPullAction()
    {
        FrontAntirollBarMinValue.Value = Convert.ToSingle(D.FrontAntirollMinBox.Value);
        FrontAntirollBarMaxValue.Value = Convert.ToSingle(D.FrontAntirollMaxBox.Value);
    }

    private static void RearAntirollBarsPullAction()
    {
        RearAntirollBarMinValue.Value = Convert.ToSingle(D.RearAntirollMinBox.Value);
        RearAntirollBarMaxValue.Value = Convert.ToSingle(D.RearAntirollMaxBox.Value);
    }
    
    private static void FrontAntirollBarMinValueChanged(object s, EventArgs e)
    {
        D.FrontAntirollMinBox.Value = FrontAntirollBarMinValue.Value;
    }
    
    private static void FrontAntirollBarMaxValueChanged(object s, EventArgs e)
    {
        D.FrontAntirollMaxBox.Value = FrontAntirollBarMaxValue.Value;
    }
    
    private static void RearAntirollBarMinValueChanged(object s, EventArgs e)
    {
        D.RearAntirollMinBox.Value = RearAntirollBarMinValue.Value;
    }
    
    private static void RearAntirollBarMaxValueChanged(object s, EventArgs e)
    {
        D.RearAntirollMaxBox.Value = RearAntirollBarMaxValue.Value;
    }
    
    public static readonly List<MenuOption> AntirollBarsDampingOptions = new()
    {
        new SubHeaderOption("Front Antiroll Bars"),
        FrontAntirollBarMinValue,
        FrontAntirollBarMaxValue,
        FrontAntirollBarsPull,
        new SubHeaderOption("Rear Antiroll Bars"),
        RearAntirollBarMinValue,
        RearAntirollBarMaxValue,
        RearAntirollBarsPull
    };

    public static void InitiateSubMenu()
    {
        FrontAntirollBarMinValue.ValueChanged += FrontAntirollBarMinValueChanged;
        FrontAntirollBarMaxValue.ValueChanged += FrontAntirollBarMaxValueChanged;
        RearAntirollBarMinValue.ValueChanged += RearAntirollBarMinValueChanged;
        RearAntirollBarMaxValue.ValueChanged += RearAntirollBarMaxValueChanged;
    }
}