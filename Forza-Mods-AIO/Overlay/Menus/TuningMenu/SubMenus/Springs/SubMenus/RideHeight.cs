using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;
using static Forza_Mods_AIO.Tabs.Tuning.DropDownTabs.Springs;

namespace Forza_Mods_AIO.Overlay.Menus.TuningMenu.SubMenus.Springs.SubMenus;

public abstract class RideHeight
{
    private static readonly FloatOption FrontRideHeightMinValue = new("Min Value", 0f);
    private static readonly FloatOption FrontRideHeightMaxValue = new("Max Value", 0f);
    private static readonly FloatOption RearRideHeightMinValue = new("Min Value", 0f);
    private static readonly FloatOption RearRideHeightMaxValue = new("Max Value", 0f);
    
    private static readonly ToggleOption FrontRideHeightRestriction = new("Restriction", false);
    private static readonly ToggleOption RearRideHeightRestriction = new("Restriction", false);
    
    private static readonly ButtonOption FrontRideHeightPull = new("Pull values", () =>
    {
        FrontRideHeightMinValue.Value = Convert.ToSingle(Sp.FrontRideHeightMinBox.Value);
        FrontRideHeightMaxValue.Value = Convert.ToSingle(Sp.FrontRideHeightMaxBox.Value);
    });
    
    private static readonly ButtonOption RearRideHeightPull = new("Pull values", () =>
    {
        RearRideHeightMinValue.Value = Convert.ToSingle(Sp.RearRideHeightMinBox.Value);
        RearRideHeightMaxValue.Value = Convert.ToSingle(Sp.RearRideHeightMaxBox.Value);
    });
    
    public static readonly List<MenuOption> RideHeightOptions = new()
    {
        new SubHeaderOption("Front Ride Height"),
        FrontRideHeightMinValue,
        FrontRideHeightMaxValue,
        FrontRideHeightRestriction,
        FrontRideHeightPull,
        new SubHeaderOption("Rear Ride Height"),
        RearRideHeightMinValue,
        RearRideHeightMaxValue,
        RearRideHeightRestriction,
        RearRideHeightPull
    };
    
    public static void InitiateSubMenu()
    {
        FrontRideHeightMinValue.ValueChanged += FrontRideHeightMinValueChanged;
        FrontRideHeightMaxValue.ValueChanged += FrontRideHeightMaxValueChanged;
        RearRideHeightMinValue.ValueChanged += RearRideHeightMinValueChanged;
        RearRideHeightMaxValue.ValueChanged += RearRideHeightMaxValueChanged;

        FrontRideHeightRestriction.Toggled += FrontRideHeightRestrictionValueChanged;
        RearRideHeightRestriction.Toggled += RearRideHeightRestrictionValueChanged;
    }

    private static void FrontRideHeightMinValueChanged(object s, EventArgs e)
    {
        Sp.FrontRideHeightMinBox.Value = FrontRideHeightMinValue.Value;
    }
    
    private static void FrontRideHeightMaxValueChanged(object s, EventArgs e)
    {
        Sp.FrontRideHeightMaxBox.Value = FrontRideHeightMaxValue.Value;
    }
    
    private static void RearRideHeightMinValueChanged(object s, EventArgs e)
    {
        Sp.RearRideHeightMinBox.Value = RearRideHeightMinValue.Value;
    }
    
    private static void RearRideHeightMaxValueChanged(object s, EventArgs e)
    {
        Sp.RearRideHeightMaxBox.Value = RearRideHeightMaxValue.Value;
    }
    
    private static void FrontRideHeightRestrictionValueChanged(object s, EventArgs e)
    {
        Sp.FrontRestriction.IsOn = FrontRideHeightRestriction.IsOn;
    }
    
    private static void RearRideHeightRestrictionValueChanged(object s, EventArgs e)
    {
        Sp.RearRestriction.IsOn = RearRideHeightRestriction.IsOn;
    }
}