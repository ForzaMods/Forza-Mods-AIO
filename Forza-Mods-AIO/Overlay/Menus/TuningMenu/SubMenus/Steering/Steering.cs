using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;
using static Forza_Mods_AIO.Tabs.Tuning.DropDownTabs.Steering;

namespace Forza_Mods_AIO.Overlay.Menus.TuningMenu.SubMenus.Steering;

public abstract class Steering
{
    
    private static readonly FloatOption AngleMax1Value = new("Max 1", 0f);
    private static readonly FloatOption AngleMax2Value = new("Max 2", 0f);
    
    private static readonly ButtonOption AnglePull = new("Pull values", () =>
    {
        AngleMax1Value.Value = Convert.ToSingle(St.AngleMaxBox.Value);
        AngleMax2Value.Value = Convert.ToSingle(St.AngleMax2Box.Value);
    });
    
        
    private static readonly FloatOption VelCountersteerValue = new("Countersteer Value", 0f);
    private static readonly FloatOption VelStraightValue = new("Straight Value", 0f);
    private static readonly FloatOption VelTurningValue = new("Turning Value", 0f);
    private static readonly FloatOption VelDynamicPeekValue = new("Dynamic Peek Value", 0f);
    private static readonly FloatOption VelTimeValue = new("Time Value", 0f);
    
    private static readonly ButtonOption VelocityPull = new("Pull values", () =>
    {
        VelCountersteerValue.Value = Convert.ToSingle(St.VelocityCountersteerBox.Value);
        VelStraightValue.Value = Convert.ToSingle(St.VelocityStraightBox.Value);
        VelTurningValue.Value = Convert.ToSingle(St.VelocityTurningBox.Value);
        VelDynamicPeekValue.Value = Convert.ToSingle(St.VelocityDynamicPeekBox.Value);
        VelTimeValue.Value = Convert.ToSingle(St.TimeToMaxSteeringBox.Value);
    });
    
    public static readonly List<MenuOption> SteeringOptions = new()
    {
        new SubHeaderOption("Angle"),
        AngleMax1Value,
        AngleMax2Value,
        AnglePull,
        new SubHeaderOption("Velocity"),
        VelCountersteerValue,
        VelStraightValue,
        VelTurningValue,
        VelDynamicPeekValue,
        VelTimeValue,
        VelocityPull
    };

    private static void AngleMax1ValueChanged(object s, EventArgs e)
    {
        St.AngleMaxBox.Value = AngleMax1Value.Value;
    }
    
    private static void AngleMax2ValueChanged(object s, EventArgs e)
    {
        St.AngleMax2Box.Value = AngleMax2Value.Value;
    }
    
    private static void VelCountersteerValueChanged(object s, EventArgs e)
    {
        St.VelocityCountersteerBox.Value = VelCountersteerValue.Value;
    }
    
    private static void VelTurningValueChanged(object s, EventArgs e)
    {
        St.VelocityTurningBox.Value = VelTurningValue.Value;
    }
    
    private static void VelDynamicPeekValueChanged(object s, EventArgs e)
    {
        St.VelocityDynamicPeekBox.Value = VelDynamicPeekValue.Value;
    }
    
    private static void VelStraightValueChanged(object s, EventArgs e)
    {
        St.VelocityCountersteerBox.Value = VelStraightValue.Value;
    }
    
    private static void VelTimeValueChanged(object s, EventArgs e)
    {
        St.TimeToMaxSteeringBox.Value = VelTimeValue.Value;
    }
    
    public static void InitiateSubMenu()
    {
        AngleMax1Value.ValueChangedEventHandler += AngleMax1ValueChanged;
        AngleMax2Value.ValueChangedEventHandler += AngleMax2ValueChanged;
        VelCountersteerValue.ValueChangedEventHandler += VelCountersteerValueChanged;
        VelStraightValue.ValueChangedEventHandler += VelStraightValueChanged;
        VelTurningValue.ValueChangedEventHandler += VelTurningValueChanged;
        VelDynamicPeekValue.ValueChangedEventHandler += VelDynamicPeekValueChanged;
        VelTimeValue.ValueChangedEventHandler += VelTimeValueChanged;
    }
}