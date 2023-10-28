using System;
using System.Collections.Generic;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Damping.SubMenus;

public abstract class ReboundStiffness
{
    private static readonly Overlay.MenuOption FrontReboundStiffnessMinValue = new ("Min Value", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption FrontReboundStiffnessMaxValue = new ("Max Value", Overlay.MenuOption.OptionType.Float, 0f);
    
    private static readonly Overlay.MenuOption RearReboundStiffnessMinValue = new ("Min Value", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption RearReboundStiffnessMaxValue = new ("Max Value", Overlay.MenuOption.OptionType.Float, 0f);
    
    private static readonly Overlay.MenuOption FrontReboundStiffnessPull = new ("Pull values", Overlay.MenuOption.OptionType.Button, new Action(() =>
    {
        var Damping = Tabs.Tuning.DropDownTabs.Damping.d;
        
        Damping.Dispatcher.Invoke(() =>
        {
            if (Damping.FrontReboundStiffnessMinBox.Value == null || Damping.FrontReboundStiffnessMaxBox.Value == null)
                return;

            FrontReboundStiffnessMinValue.Value = (float)Damping.FrontReboundStiffnessMinBox.Value;
            FrontReboundStiffnessMaxValue.Value = (float)Damping.FrontReboundStiffnessMaxBox.Value;
        });
    }));
    
    private static readonly Overlay.MenuOption RearReboundStiffnessPull = new ("Pull values", Overlay.MenuOption.OptionType.Button, new Action(() =>
    {
        var Damping = Tabs.Tuning.DropDownTabs.Damping.d;
        
        Damping.Dispatcher.Invoke(() =>
        {
            if (Damping.RearReboundStiffnessMinBox.Value == null || Damping.RearReboundStiffnessMaxBox.Value == null)
                return;

            RearReboundStiffnessMinValue.Value = (float)Damping.RearReboundStiffnessMinBox.Value;
            RearReboundStiffnessMaxValue.Value = (float)Damping.RearReboundStiffnessMaxBox.Value;
        });
    }));

    private static void FrontReboundStiffnessMinValueChanged(object s, EventArgs e)
    {
        var Damping = Tabs.Tuning.DropDownTabs.Damping.d;

        Damping.Dispatcher.Invoke(() =>
        {
            Damping.FrontReboundStiffnessMinBox.Value = (float)FrontReboundStiffnessMinValue.Value;
        });
    }
    
    private static void FrontReboundStiffnessMaxValueChanged(object s, EventArgs e)
    {
        var Damping = Tabs.Tuning.DropDownTabs.Damping.d;

        Damping.Dispatcher.Invoke(() =>
        {
            Damping.FrontReboundStiffnessMaxBox.Value = (float)FrontReboundStiffnessMaxValue.Value;
        });
    }
    
    private static void RearReboundStiffnessMinValueChanged(object s, EventArgs e)
    {
        var Damping = Tabs.Tuning.DropDownTabs.Damping.d;

        Damping.Dispatcher.Invoke(() =>
        {
            Damping.RearReboundStiffnessMinBox.Value = (float)RearReboundStiffnessMinValue.Value;
        });
    }
    
    private static void RearReboundStiffnessMaxValueChanged(object s, EventArgs e)
    {
        var Damping = Tabs.Tuning.DropDownTabs.Damping.d;

        Damping.Dispatcher.Invoke(() =>
        {
            Damping.RearReboundStiffnessMaxBox.Value = (float)RearReboundStiffnessMaxValue.Value;
        });
    }
    
    public static readonly List<Overlay.MenuOption> ReboundStiffnessOptions = new()
    {
        new("Front Rebound Stiffness", Overlay.MenuOption.OptionType.SubHeader),
        FrontReboundStiffnessMinValue,
        FrontReboundStiffnessMaxValue,
        FrontReboundStiffnessPull,
        new("Rear Rebound Stiffness", Overlay.MenuOption.OptionType.SubHeader),
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