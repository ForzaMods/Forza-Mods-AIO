using System;
using System.Collections.Generic;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Damping.SubMenus;

public abstract class ReboundStiffness
{
    private static readonly MenuOption FrontReboundStiffnessMinValue = new ("Min Value", 0f);
    private static readonly MenuOption FrontReboundStiffnessMaxValue = new ("Max Value", 0f);
    
    private static readonly MenuOption RearReboundStiffnessMinValue = new ("Min Value", 0f);
    private static readonly MenuOption RearReboundStiffnessMaxValue = new ("Max Value", 0f);
    
    private static readonly MenuOption FrontReboundStiffnessPull = new ("Pull values",  () =>
    {
        var damping = Tabs.Tuning.DropDownTabs.Damping.D;
        
        damping.Dispatcher.Invoke(() =>
        {
            if (damping.FrontReboundStiffnessMinBox.Value == null || damping.FrontReboundStiffnessMaxBox.Value == null)
                return;

            FrontReboundStiffnessMinValue.Value = (float)damping.FrontReboundStiffnessMinBox.Value;
            FrontReboundStiffnessMaxValue.Value = (float)damping.FrontReboundStiffnessMaxBox.Value;
        });
    });
    
    private static readonly MenuOption RearReboundStiffnessPull = new ("Pull values",  () =>
    {
        var damping = Tabs.Tuning.DropDownTabs.Damping.D;
        
        damping.Dispatcher.Invoke(() =>
        {
            if (damping.RearReboundStiffnessMinBox.Value == null || damping.RearReboundStiffnessMaxBox.Value == null)
                return;

            RearReboundStiffnessMinValue.Value = (float)damping.RearReboundStiffnessMinBox.Value;
            RearReboundStiffnessMaxValue.Value = (float)damping.RearReboundStiffnessMaxBox.Value;
        });
    });

    private static void FrontReboundStiffnessMinValueChanged(object s, EventArgs e)
    {
        var damping = Tabs.Tuning.DropDownTabs.Damping.D;

        damping.Dispatcher.Invoke(() =>
        {
            damping.FrontReboundStiffnessMinBox.Value = (float)FrontReboundStiffnessMinValue.Value;
        });
    }
    
    private static void FrontReboundStiffnessMaxValueChanged(object s, EventArgs e)
    {
        var damping = Tabs.Tuning.DropDownTabs.Damping.D;

        damping.Dispatcher.Invoke(() =>
        {
            damping.FrontReboundStiffnessMaxBox.Value = (float)FrontReboundStiffnessMaxValue.Value;
        });
    }
    
    private static void RearReboundStiffnessMinValueChanged(object s, EventArgs e)
    {
        var damping = Tabs.Tuning.DropDownTabs.Damping.D;

        damping.Dispatcher.Invoke(() =>
        {
            damping.RearReboundStiffnessMinBox.Value = (float)RearReboundStiffnessMinValue.Value;
        });
    }
    
    private static void RearReboundStiffnessMaxValueChanged(object s, EventArgs e)
    {
        var damping = Tabs.Tuning.DropDownTabs.Damping.D;

        damping.Dispatcher.Invoke(() =>
        {
            damping.RearReboundStiffnessMaxBox.Value = (float)RearReboundStiffnessMaxValue.Value;
        });
    }
    
    public static readonly List<MenuOption> ReboundStiffnessOptions = new()
    {
        new("Front Rebound Stiffness", OptionType.SubHeader),
        FrontReboundStiffnessMinValue,
        FrontReboundStiffnessMaxValue,
        FrontReboundStiffnessPull,
        new("Rear Rebound Stiffness", OptionType.SubHeader),
        RearReboundStiffnessMinValue,
        RearReboundStiffnessMaxValue,
        RearReboundStiffnessPull
    };

    public static void InitiateSubMenu()
    {
        FrontReboundStiffnessMinValue.ValueChangedHandler += FrontReboundStiffnessMinValueChanged;
        FrontReboundStiffnessMaxValue.ValueChangedHandler += FrontReboundStiffnessMaxValueChanged;
        RearReboundStiffnessMinValue.ValueChangedHandler += RearReboundStiffnessMinValueChanged;
        RearReboundStiffnessMaxValue.ValueChangedHandler += RearReboundStiffnessMaxValueChanged;
    }
}