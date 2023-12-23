using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;
using static Forza_Mods_AIO.Tabs.Tuning.DropDownTabs.Damping;

namespace Forza_Mods_AIO.Overlay.Menus.TuningMenu.SubMenus.Damping.SubMenus;

public abstract class ReboundStiffness
{
    private static readonly FloatOption FrontReboundStiffnessMinValue = new("Min Value", 0f);
    private static readonly FloatOption FrontReboundStiffnessMaxValue = new("Max Value", 0f);
    
    private static readonly FloatOption RearReboundStiffnessMinValue = new("Min Value", 0f);
    private static readonly FloatOption RearReboundStiffnessMaxValue = new("Max Value", 0f);
    
    private static readonly ButtonOption FrontReboundStiffnessPull = new("Pull values", () =>
    {
        FrontReboundStiffnessMinValue.Value = Convert.ToSingle(D.FrontReboundStiffnessMinBox.Value);
        FrontReboundStiffnessMaxValue.Value = Convert.ToSingle(D.FrontReboundStiffnessMaxBox.Value);
    });
    
    private static readonly ButtonOption RearReboundStiffnessPull = new("Pull values", () =>
    {
        RearReboundStiffnessMinValue.Value = Convert.ToSingle(D.RearReboundStiffnessMinBox.Value);
        RearReboundStiffnessMaxValue.Value = Convert.ToSingle(D.RearReboundStiffnessMaxBox.Value);
    });

    private static void FrontReboundStiffnessMinValueChanged(object s, EventArgs e)
    {
        D.FrontReboundStiffnessMinBox.Value = FrontReboundStiffnessMinValue.Value;
    }
    
    private static void FrontReboundStiffnessMaxValueChanged(object s, EventArgs e)
    {
        D.FrontReboundStiffnessMaxBox.Value = FrontReboundStiffnessMaxValue.Value;
    }
    
    private static void RearReboundStiffnessMinValueChanged(object s, EventArgs e)
    {
        D.RearReboundStiffnessMinBox.Value = RearReboundStiffnessMinValue.Value;
    }
    
    private static void RearReboundStiffnessMaxValueChanged(object s, EventArgs e)
    {
        D.RearReboundStiffnessMaxBox.Value = RearReboundStiffnessMaxValue.Value;
    }
    
    public static readonly List<MenuOption> ReboundStiffnessOptions = new()
    {
        new SubHeaderOption("Front Rebound Stiffness"),
        FrontReboundStiffnessMinValue,
        FrontReboundStiffnessMaxValue,
        FrontReboundStiffnessPull,
        new SubHeaderOption("Rear Rebound Stiffness"),
        RearReboundStiffnessMinValue,
        RearReboundStiffnessMaxValue,
        RearReboundStiffnessPull
    };

    public static void InitiateSubMenu()
    {
        FrontReboundStiffnessMinValue.ValueChanged += FrontReboundStiffnessMinValueChanged;
        FrontReboundStiffnessMaxValue.ValueChanged += FrontReboundStiffnessMaxValueChanged;
        RearReboundStiffnessMinValue.ValueChanged += RearReboundStiffnessMinValueChanged;
        RearReboundStiffnessMaxValue.ValueChanged += RearReboundStiffnessMaxValueChanged;
    }
}