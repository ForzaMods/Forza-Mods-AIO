using System;
using System.Collections.Generic;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Damping.SubMenus;

public abstract class BumpStiffness
{
    private static readonly MenuOption FrontBumpStiffnessMinValue = new ("Min Value", 0f);
    private static readonly MenuOption FrontBumpStiffnessMaxValue = new ("Max Value", 0f);
    
    private static readonly MenuOption RearBumpStiffnessMinValue = new ("Min Value", 0f);
    private static readonly MenuOption RearBumpStiffnessMaxValue = new ("Max Value", 0f);
    
    private static readonly MenuOption FrontBumpStiffnessPull = new ("Pull values",  () =>
    {
        var damping = Tabs.Tuning.DropDownTabs.Damping.D;
        
        damping.Dispatcher.Invoke(() =>
        {
            if (damping.FrontBumpStiffnessMinBox.Value == null || damping.FrontBumpStiffnessMaxBox.Value == null)
                return;

            FrontBumpStiffnessMinValue.Value = (float)damping.FrontBumpStiffnessMinBox.Value;
            FrontBumpStiffnessMaxValue.Value = (float)damping.FrontBumpStiffnessMaxBox.Value;
        });
    });
    
    private static readonly MenuOption RearBumpStiffnessPull = new ("Pull values",  () =>
    {
        var damping = Tabs.Tuning.DropDownTabs.Damping.D;
        
        damping.Dispatcher.Invoke(() =>
        {
            if (damping.RearBumpStiffnessMinBox.Value == null || damping.RearBumpStiffnessMaxBox.Value == null)
                return;

            RearBumpStiffnessMinValue.Value = (float)damping.RearBumpStiffnessMinBox.Value;
            RearBumpStiffnessMaxValue.Value = (float)damping.RearBumpStiffnessMaxBox.Value;
        });
    });

    private static void FrontBumpStiffnessMinValueChanged(object s, EventArgs e)
    {
        var damping = Tabs.Tuning.DropDownTabs.Damping.D;

        damping.Dispatcher.Invoke(() =>
        {
            damping.FrontBumpStiffnessMinBox.Value = (float)FrontBumpStiffnessMinValue.Value;
        });
    }
    
    private static void FrontBumpStiffnessMaxValueChanged(object s, EventArgs e)
    {
        var damping = Tabs.Tuning.DropDownTabs.Damping.D;

        damping.Dispatcher.Invoke(() =>
        {
            damping.FrontBumpStiffnessMaxBox.Value = (float)FrontBumpStiffnessMaxValue.Value;
        });
    }
    
    private static void RearBumpStiffnessMinValueChanged(object s, EventArgs e)
    {
        var damping = Tabs.Tuning.DropDownTabs.Damping.D;

        damping.Dispatcher.Invoke(() =>
        {
            damping.RearBumpStiffnessMinBox.Value = (float)RearBumpStiffnessMinValue.Value;
        });
    }
    
    private static void RearBumpStiffnessMaxValueChanged(object s, EventArgs e)
    {
        var damping = Tabs.Tuning.DropDownTabs.Damping.D;

        damping.Dispatcher.Invoke(() =>
        {
            damping.RearBumpStiffnessMaxBox.Value = (float)RearBumpStiffnessMaxValue.Value;
        });
    }
    
    public static readonly List<MenuOption> BumpStiffnessOptions = new()
    {
        new("Front Bump Stiffness", OptionType.SubHeader),
        FrontBumpStiffnessMinValue,
        FrontBumpStiffnessMaxValue,
        FrontBumpStiffnessPull,
        new("Rear Bump Stiffness", OptionType.SubHeader),
        RearBumpStiffnessMinValue,
        RearBumpStiffnessMaxValue,
        RearBumpStiffnessPull
    };

    public static void InitiateSubMenu()
    {
        FrontBumpStiffnessMinValue.ValueChangedHandler += FrontBumpStiffnessMinValueChanged;
        FrontBumpStiffnessMaxValue.ValueChangedHandler += FrontBumpStiffnessMaxValueChanged;
        RearBumpStiffnessMinValue.ValueChangedHandler += RearBumpStiffnessMinValueChanged;
        RearBumpStiffnessMaxValue.ValueChangedHandler += RearBumpStiffnessMaxValueChanged;
    }
}