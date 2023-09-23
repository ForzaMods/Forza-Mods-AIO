using System;
using System.Collections.Generic;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Damping.SubMenus;

public abstract class BumpStiffness
{
    private static readonly Overlay.MenuOption FrontBumpStiffnessMinValue = new ("Min Value", "Float", 0f);
    private static readonly Overlay.MenuOption FrontBumpStiffnessMaxValue = new ("Max Value", "Float", 0f);
    
    private static readonly Overlay.MenuOption RearBumpStiffnessMinValue = new ("Min Value", "Float", 0f);
    private static readonly Overlay.MenuOption RearBumpStiffnessMaxValue = new ("Max Value", "Float", 0f);
    
    private static readonly Overlay.MenuOption FrontBumpStiffnessPull = new ("Pull values", "Button", new Action(() =>
    {
        var Damping = Tabs.Tuning.DropDownTabs.Damping.d;
        
        Damping.Dispatcher.Invoke(() =>
        {
            if (Damping.FrontBumpStiffnessMinBox.Value == null || Damping.FrontBumpStiffnessMaxBox.Value == null)
                return;

            FrontBumpStiffnessMinValue.Value = (float)Damping.FrontBumpStiffnessMinBox.Value;
            FrontBumpStiffnessMaxValue.Value = (float)Damping.FrontBumpStiffnessMaxBox.Value;
        });
    }));
    
    private static readonly Overlay.MenuOption RearBumpStiffnessPull = new ("Pull values", "Button", new Action(() =>
    {
        var Damping = Tabs.Tuning.DropDownTabs.Damping.d;
        
        Damping.Dispatcher.Invoke(() =>
        {
            if (Damping.RearBumpStiffnessMinBox.Value == null || Damping.RearBumpStiffnessMaxBox.Value == null)
                return;

            RearBumpStiffnessMinValue.Value = (float)Damping.RearBumpStiffnessMinBox.Value;
            RearBumpStiffnessMaxValue.Value = (float)Damping.RearBumpStiffnessMaxBox.Value;
        });
    }));

    private static void FrontBumpStiffnessMinValueChanged(object s, EventArgs e)
    {
        var Damping = Tabs.Tuning.DropDownTabs.Damping.d;

        Damping.Dispatcher.Invoke(() =>
        {
            Damping.FrontBumpStiffnessMinBox.Value = (float)FrontBumpStiffnessMinValue.Value;
        });
    }
    
    private static void FrontBumpStiffnessMaxValueChanged(object s, EventArgs e)
    {
        var Damping = Tabs.Tuning.DropDownTabs.Damping.d;

        Damping.Dispatcher.Invoke(() =>
        {
            Damping.FrontBumpStiffnessMaxBox.Value = (float)FrontBumpStiffnessMaxValue.Value;
        });
    }
    
    private static void RearBumpStiffnessMinValueChanged(object s, EventArgs e)
    {
        var Damping = Tabs.Tuning.DropDownTabs.Damping.d;

        Damping.Dispatcher.Invoke(() =>
        {
            Damping.RearBumpStiffnessMinBox.Value = (float)RearBumpStiffnessMinValue.Value;
        });
    }
    
    private static void RearBumpStiffnessMaxValueChanged(object s, EventArgs e)
    {
        var Damping = Tabs.Tuning.DropDownTabs.Damping.d;

        Damping.Dispatcher.Invoke(() =>
        {
            Damping.RearBumpStiffnessMaxBox.Value = (float)RearBumpStiffnessMaxValue.Value;
        });
    }
    
    public static readonly List<Overlay.MenuOption> BumpStiffnessOptions = new()
    {
        new("Front Bump Stiffness", "SubHeader"),
        FrontBumpStiffnessMinValue,
        FrontBumpStiffnessMaxValue,
        FrontBumpStiffnessPull,
        new("Rear Bump Stiffness", "SubHeader"),
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