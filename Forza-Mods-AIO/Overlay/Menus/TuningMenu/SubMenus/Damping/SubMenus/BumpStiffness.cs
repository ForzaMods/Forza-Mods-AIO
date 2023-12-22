using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;
using static Forza_Mods_AIO.Tabs.Tuning.DropDownTabs.Damping;

namespace Forza_Mods_AIO.Overlay.Menus.TuningMenu.SubMenus.Damping.SubMenus;

public abstract class BumpStiffness
{
    private static readonly FloatOption FrontBumpStiffnessMinValue = new("Min Value", 0f);
    private static readonly FloatOption FrontBumpStiffnessMaxValue = new("Max Value", 0f);
    
    private static readonly FloatOption RearBumpStiffnessMinValue = new("Min Value", 0f);
    private static readonly FloatOption RearBumpStiffnessMaxValue = new("Max Value", 0f);
    
    private static readonly ButtonOption FrontBumpStiffnessPull = new("Pull values", () =>
    {
        FrontBumpStiffnessMinValue.Value = Convert.ToSingle(D.FrontBumpStiffnessMinBox.Value);
        FrontBumpStiffnessMaxValue.Value = Convert.ToSingle(D.FrontBumpStiffnessMaxBox.Value);
    });
    
    private static readonly ButtonOption RearBumpStiffnessPull = new("Pull values", () =>
    {
        RearBumpStiffnessMinValue.Value = Convert.ToSingle(D.RearBumpStiffnessMinBox.Value);
        RearBumpStiffnessMaxValue.Value = Convert.ToSingle(D.RearBumpStiffnessMaxBox.Value);
    });

    private static void FrontBumpStiffnessMinValueChanged(object s, EventArgs e)
    {
        D.FrontBumpStiffnessMinBox.Value = FrontBumpStiffnessMinValue.Value;
    }
    
    private static void FrontBumpStiffnessMaxValueChanged(object s, EventArgs e)
    {
        D.FrontBumpStiffnessMaxBox.Value = FrontBumpStiffnessMaxValue.Value;
    }
    
    private static void RearBumpStiffnessMinValueChanged(object s, EventArgs e)
    {
        D.RearBumpStiffnessMinBox.Value = RearBumpStiffnessMinValue.Value;
    }
    
    private static void RearBumpStiffnessMaxValueChanged(object s, EventArgs e)
    {
        D.RearBumpStiffnessMaxBox.Value = RearBumpStiffnessMaxValue.Value;
    }
    
    public static readonly List<MenuOption> BumpStiffnessOptions = new()
    {
        new SubHeaderOption("Front Bump Stiffness"),
        FrontBumpStiffnessMinValue,
        FrontBumpStiffnessMaxValue,
        FrontBumpStiffnessPull,
        new SubHeaderOption("Rear Bump Stiffness"),
        RearBumpStiffnessMinValue,
        RearBumpStiffnessMaxValue,
        RearBumpStiffnessPull
    };

    public static void InitiateSubMenu()
    {
        FrontBumpStiffnessMinValue.ValueChangedEventHandler += FrontBumpStiffnessMinValueChanged;
        FrontBumpStiffnessMaxValue.ValueChangedEventHandler += FrontBumpStiffnessMaxValueChanged;
        RearBumpStiffnessMinValue.ValueChangedEventHandler += RearBumpStiffnessMinValueChanged;
        RearBumpStiffnessMaxValue.ValueChangedEventHandler += RearBumpStiffnessMaxValueChanged;
    }
}