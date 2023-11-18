using System;
using System.Collections.Generic;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Springs.SubMenus;

public abstract class RideHeight
{
    private static readonly MenuOption FrontRideHeightMinValue = new ("Min Value", OptionType.Float, 0f);
    private static readonly MenuOption FrontRideHeightMaxValue = new ("Max Value", OptionType.Float, 0f);
    private static readonly MenuOption RearRideHeightMinValue = new ("Min Value", OptionType.Float, 0f);
    private static readonly MenuOption RearRideHeightMaxValue = new ("Max Value", OptionType.Float, 0f);
    
    private static readonly MenuOption FrontRideHeightRestriction = new ("Restriction", OptionType.Bool, false);
    private static readonly MenuOption RearRideHeightRestriction = new ("Restriction", OptionType.Bool, false);
    
    private static readonly MenuOption FrontRideHeightPull = new ("Pull values", OptionType.Button, () =>
    {
        var springs = Tabs.Tuning.DropDownTabs.Springs.Sp;
        
        springs.Dispatcher.Invoke(() =>
        {
            FrontRideHeightMinValue.Value = (float)springs.FrontRideHeightMinBox.Value!;
            FrontRideHeightMaxValue.Value = (float)springs.FrontRideHeightMaxBox.Value!;
        });
    });
    
    private static readonly MenuOption RearRideHeightPull = new ("Pull values", OptionType.Button, () =>
    {
        var springs = Tabs.Tuning.DropDownTabs.Springs.Sp;
        
        springs.Dispatcher.Invoke(() =>
        {
            RearRideHeightMinValue.Value = (float)springs.RearRideHeightMinBox.Value!;
            RearRideHeightMaxValue.Value = (float)springs.RearRideHeightMaxBox.Value!;
        });
    });
    
    public static readonly List<MenuOption> RideHeightOptions = new()
    {
        new ("Front Ride Height", OptionType.SubHeader),
        FrontRideHeightMinValue,
        FrontRideHeightMaxValue,
        FrontRideHeightRestriction,
        FrontRideHeightPull,
        new ("Rear Ride Height", OptionType.SubHeader),
        RearRideHeightMinValue,
        RearRideHeightMaxValue,
        RearRideHeightRestriction,
        RearRideHeightPull
    };
    
    public static void InitiateSubMenu()
    {
        FrontRideHeightMinValue.ValueChangedHandler += FrontRideHeightMinValueChanged;
        FrontRideHeightMaxValue.ValueChangedHandler += FrontRideHeightMaxValueChanged;
        RearRideHeightMinValue.ValueChangedHandler += RearRideHeightMinValueChanged;
        RearRideHeightMaxValue.ValueChangedHandler += RearRideHeightMaxValueChanged;

        FrontRideHeightRestriction.ValueChangedHandler += FrontRideHeightRestrictionValueChanged;
        RearRideHeightRestriction.ValueChangedHandler += RearRideHeightRestrictionValueChanged;
    }

    private static void FrontRideHeightMinValueChanged(object s, EventArgs e)
    {
        var springs = Tabs.Tuning.DropDownTabs.Springs.Sp;

        springs.Dispatcher.Invoke(() =>
        {
            springs.FrontRideHeightMinBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void FrontRideHeightMaxValueChanged(object s, EventArgs e)
    {
        var springs = Tabs.Tuning.DropDownTabs.Springs.Sp;

        springs.Dispatcher.Invoke(() =>
        {
            springs.FrontRideHeightMaxBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void RearRideHeightMinValueChanged(object s, EventArgs e)
    {
        var springs = Tabs.Tuning.DropDownTabs.Springs.Sp;

        springs.Dispatcher.Invoke(() =>
        {
            springs.RearRideHeightMinBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void RearRideHeightMaxValueChanged(object s, EventArgs e)
    {
        var springs = Tabs.Tuning.DropDownTabs.Springs.Sp;

        springs.Dispatcher.Invoke(() =>
        {
            springs.RearRideHeightMaxBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void FrontRideHeightRestrictionValueChanged(object s, EventArgs e)
    {
        var springs = Tabs.Tuning.DropDownTabs.Springs.Sp;

        springs.Dispatcher.Invoke(() =>
        {
            springs.FrontRestriction.IsOn = (bool)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void RearRideHeightRestrictionValueChanged(object s, EventArgs e)
    {
        var springs = Tabs.Tuning.DropDownTabs.Springs.Sp;

        springs.Dispatcher.Invoke(() =>
        {
            springs.RearRestriction.IsOn = (bool)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
}