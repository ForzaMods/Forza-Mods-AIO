using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;
using static Forza_Mods_AIO.Tabs.Tuning.DropDownTabs.Aero;

namespace Forza_Mods_AIO.Overlay.Menus.TuningMenu.SubMenus.Aero;

public abstract class Aero
{
    private static readonly FloatOption FrontAeroMinValue = new("Min Value", 0f);
    private static readonly FloatOption FrontAeroMaxValue = new("Max Value", 0f);
    
    private static readonly FloatOption RearAeroMinValue = new("Min Value", 0f);
    private static readonly FloatOption RearAeroMaxValue = new("Max Value", 0f);
    
    private static readonly ButtonOption FrontAeroPull = new("Pull values", () =>
    {
        FrontAeroMinValue.Value = Convert.ToSingle(Ae.FrontAeroMinBox.Value);
        FrontAeroMaxValue.Value = Convert.ToSingle(Ae.FrontAeroMaxBox.Value);
    });
    
    private static readonly ButtonOption RearAeroPull = new("Pull values", () =>
    {
        RearAeroMinValue.Value = Convert.ToSingle(Ae.RearAeroMinBox.Value);
        RearAeroMaxValue.Value = Convert.ToSingle(Ae.RearAeroMaxBox.Value);
    });

    private static void FrontAeroMinValueChanged(object s, EventArgs e)
    {
        Ae.FrontAeroMinBox.Value = FrontAeroMinValue.Value;
    }
    
    private static void FrontAeroMaxValueChanged(object s, EventArgs e)
    {
        Ae.FrontAeroMaxBox.Value = FrontAeroMaxValue.Value;
    }
    
    private static void RearAeroMinValueChanged(object s, EventArgs e)
    {
        Ae.RearAeroMinBox.Value = RearAeroMinValue.Value;
    }
    
    private static void RearAeroMaxValueChanged(object s, EventArgs e)
    {
        Ae.RearAeroMaxBox.Value = RearAeroMaxValue.Value;
    }
    
    public static readonly List<MenuOption> AeroOptions = new()
    {
        new SubHeaderOption("Front Aero"),
        FrontAeroMinValue,
        FrontAeroMaxValue,
        FrontAeroPull,
        new SubHeaderOption("Rear Aero"),
        RearAeroMinValue,
        RearAeroMaxValue,
        RearAeroPull
    };

    public static void InitiateSubMenu()
    {
        FrontAeroMinValue.ValueChangedEventHandler += FrontAeroMinValueChanged;
        FrontAeroMaxValue.ValueChangedEventHandler += FrontAeroMaxValueChanged;
        RearAeroMinValue.ValueChangedEventHandler += RearAeroMinValueChanged;
        RearAeroMaxValue.ValueChangedEventHandler += RearAeroMaxValueChanged;
    }
}