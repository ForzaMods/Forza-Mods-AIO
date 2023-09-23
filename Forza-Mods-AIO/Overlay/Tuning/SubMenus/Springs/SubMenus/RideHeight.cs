using System;
using System.Collections.Generic;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Springs.SubMenus;

public abstract class RideHeight
{
    private static readonly Overlay.MenuOption FrontRideHeightMinValue = new ("Min Value", "Float", 0f);
    private static readonly Overlay.MenuOption FrontRideHeightMaxValue = new ("Max Value", "Float", 0f);
    private static readonly Overlay.MenuOption RearRideHeightMinValue = new ("Min Value", "Float", 0f);
    private static readonly Overlay.MenuOption RearRideHeightMaxValue = new ("Max Value", "Float", 0f);
    
    private static readonly Overlay.MenuOption FrontRideHeightRestriction = new ("Restriction", "Bool", false);
    private static readonly Overlay.MenuOption RearRideHeightRestriction = new ("Restriction", "Bool", false);
    
    private static readonly Overlay.MenuOption FrontRideHeightPull = new ("Pull values", "Button", new Action(() =>
    {
        var Springs = Tabs.Tuning.DropDownTabs.Springs.sp;
        
        Springs.Dispatcher.Invoke(() =>
        {
            FrontRideHeightMinValue.Value = (float)Springs.FrontRideHeightMinBox.Value!;
            FrontRideHeightMaxValue.Value = (float)Springs.FrontRideHeightMaxBox.Value!;
        });
    }));
    
    private static readonly Overlay.MenuOption RearRideHeightPull = new ("Pull values", "Button", new Action(() =>
    {
        var Springs = Tabs.Tuning.DropDownTabs.Springs.sp;
        
        Springs.Dispatcher.Invoke(() =>
        {
            RearRideHeightMinValue.Value = (float)Springs.RearRideHeightMinBox.Value!;
            RearRideHeightMaxValue.Value = (float)Springs.RearRideHeightMaxBox.Value!;
        });
    }));
    
    public static readonly List<Overlay.MenuOption> RideHeightOptions = new()
    {
        new ("Front Ride Height", "SubHeader"),
        FrontRideHeightMinValue,
        FrontRideHeightMaxValue,
        FrontRideHeightRestriction,
        FrontRideHeightPull,
        new ("Rear Ride Height", "SubHeader"),
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
        var Springs = Tabs.Tuning.DropDownTabs.Springs.sp;

        Springs.Dispatcher.Invoke(() =>
        {
            Springs.FrontRideHeightMinBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void FrontRideHeightMaxValueChanged(object s, EventArgs e)
    {
        var Springs = Tabs.Tuning.DropDownTabs.Springs.sp;

        Springs.Dispatcher.Invoke(() =>
        {
            Springs.FrontRideHeightMaxBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void RearRideHeightMinValueChanged(object s, EventArgs e)
    {
        var Springs = Tabs.Tuning.DropDownTabs.Springs.sp;

        Springs.Dispatcher.Invoke(() =>
        {
            Springs.RearRideHeightMinBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void RearRideHeightMaxValueChanged(object s, EventArgs e)
    {
        var Springs = Tabs.Tuning.DropDownTabs.Springs.sp;

        Springs.Dispatcher.Invoke(() =>
        {
            Springs.RearRideHeightMaxBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void FrontRideHeightRestrictionValueChanged(object s, EventArgs e)
    {
        var Springs = Tabs.Tuning.DropDownTabs.Springs.sp;

        Springs.Dispatcher.Invoke(() =>
        {
            Springs.FrontRestriction.IsOn = (bool)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void RearRideHeightRestrictionValueChanged(object s, EventArgs e)
    {
        var Springs = Tabs.Tuning.DropDownTabs.Springs.sp;

        Springs.Dispatcher.Invoke(() =>
        {
            Springs.RearRestriction.IsOn = (bool)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
}