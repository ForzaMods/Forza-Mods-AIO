using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;
using static Forza_Mods_AIO.Tabs.Tuning.DropDownTabs.Springs;

namespace Forza_Mods_AIO.Overlay.Menus.TuningMenu.SubMenus.Springs.SubMenus;

public abstract class SpringsValues
{
    private static readonly FloatOption FrontSpringsMinValue = new("Min Value", 0f);
    private static readonly FloatOption FrontSpringsMaxValue = new("Max Value", 0f);
    private static readonly FloatOption RearSpringsMinValue = new("Min Value", 0f);
    private static readonly FloatOption RearSpringsMaxValue = new("Max Value", 0f);
    
    private static readonly ButtonOption FrontSpringsPull = new("Pull values", () =>
    {
        FrontSpringsMinValue.Value = (float)Sp.SpringFrontMinBox.Value!;
        FrontSpringsMaxValue.Value = (float)Sp.SpringFrontMaxBox.Value!;
    });
    
    private static readonly ButtonOption RearSpringsPull = new("Pull values", () =>
    {
        RearSpringsMinValue.Value = (float)Sp.SpringRearMinBox.Value!;
        RearSpringsMaxValue.Value = (float)Sp.SpringRearMaxBox.Value!;
    });
    
    public static readonly List<MenuOption> SpringsSubMenuOptions = new()
    {
        new SubHeaderOption("Front Springs"),
        FrontSpringsMinValue,
        FrontSpringsMaxValue,
        FrontSpringsPull,
        new SubHeaderOption("Rear Springs"),
        RearSpringsMinValue,
        RearSpringsMaxValue,
        RearSpringsPull
    };
    
    public static void InitiateSubMenu()
    {
        FrontSpringsMinValue.ValueChangedEventHandler += FrontSpringsMinValueChanged;
        FrontSpringsMaxValue.ValueChangedEventHandler += FrontSpringsMaxValueChanged;
        RearSpringsMinValue.ValueChangedEventHandler += RearSpringsMinValueChanged;
        RearSpringsMaxValue.ValueChangedEventHandler += RearSpringsMaxValueChanged;
    }

    private static void FrontSpringsMinValueChanged(object s, EventArgs e)
    {
        Sp.SpringFrontMinBox.Value = FrontSpringsMinValue.Value;
    }
    
    private static void FrontSpringsMaxValueChanged(object s, EventArgs e)
    {
        Sp.SpringFrontMaxBox.Value = FrontSpringsMaxValue.Value;
    }
    
    private static void RearSpringsMinValueChanged(object s, EventArgs e)
    {
        Sp.SpringRearMinBox.Value = RearSpringsMinValue.Value;
    }
    
    private static void RearSpringsMaxValueChanged(object s, EventArgs e)
    {
        Sp.SpringRearMaxBox.Value = RearSpringsMaxValue.Value;
    }
}