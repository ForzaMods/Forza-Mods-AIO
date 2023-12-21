using System;
using System.Collections.Generic;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Steering;

public abstract class Steering
{
    
    private static readonly MenuOption AngleMax1Value = new ("Max 1", 0f);
    private static readonly MenuOption AngleMax2Value = new ("Max 2", 0f);
    
    private static readonly MenuOption AnglePull = new ("Pull values",  () =>
    {
        var steering = Tabs.Tuning.DropDownTabs.Steering.St;
        
        steering.Dispatcher.Invoke(() =>
        {
            AngleMax1Value.Value = (float)steering.AngleMaxBox.Value;
            AngleMax2Value.Value = (float)steering.AngleMax2Box.Value;
        });
    });
    
        
    private static readonly MenuOption VelCountersteerValue = new ("Countersteer Value", 0f);
    private static readonly MenuOption VelStraightValue = new ("Straight Value", 0f);
    private static readonly MenuOption VelTurningValue = new ("Turning Value", 0f);
    private static readonly MenuOption VelDynamicPeekValue = new ("Dynamic Peek Value", 0f);
    private static readonly MenuOption VelTimeValue = new ("Time Value", 0f);
    
    private static readonly MenuOption VelocityPull = new ("Pull values",  () =>
    {
        var steering = Tabs.Tuning.DropDownTabs.Steering.St;
        
        steering.Dispatcher.Invoke(() =>
        {
            VelCountersteerValue.Value = (float)steering.VelocityCountersteerBox.Value;
            VelStraightValue.Value = (float)steering.VelocityStraightBox.Value;
            VelTurningValue.Value = (float)steering.VelocityTurningBox.Value;
            VelDynamicPeekValue.Value = (float)steering.VelocityDynamicPeekBox.Value;
            VelTimeValue.Value = (float)steering.TimeToMaxSteeringBox.Value;
        });
    });
    
    public static readonly List<MenuOption> SteeringOptions = new()
    {
        new("Angle", OptionType.SubHeader),
        AngleMax1Value,
        AngleMax2Value,
        AnglePull,
        new("Velocity", OptionType.SubHeader),
        VelCountersteerValue,
        VelStraightValue,
        VelTurningValue,
        VelDynamicPeekValue,
        VelTimeValue,
        VelocityPull
    };

    private static void AngleMax1ValueChanged(object s, EventArgs e)
    {
        var steering = Tabs.Tuning.DropDownTabs.Steering.St;

        steering.Dispatcher.Invoke(() =>
        {
            steering.AngleMaxBox.Value = (float)AngleMax1Value.Value;
        });
    }
    
    private static void AngleMax2ValueChanged(object s, EventArgs e)
    {
        var steering = Tabs.Tuning.DropDownTabs.Steering.St;

        steering.Dispatcher.Invoke(() =>
        {
            steering.AngleMax2Box.Value = (float)AngleMax2Value.Value;
        });
    }
    
    private static void VelCountersteerValueChanged(object s, EventArgs e)
    {
        var steering = Tabs.Tuning.DropDownTabs.Steering.St;

        steering.Dispatcher.Invoke(() =>
        {
            steering.VelocityCountersteerBox.Value = (float)VelCountersteerValue.Value;
        });
    }
    
    private static void VelTurningValueChanged(object s, EventArgs e)
    {
        var steering = Tabs.Tuning.DropDownTabs.Steering.St;

        steering.Dispatcher.Invoke(() =>
        {
            steering.VelocityTurningBox.Value = (float)VelTurningValue.Value;
        });
    }
    
    private static void VelDynamicPeekValueChanged(object s, EventArgs e)
    {
        var steering = Tabs.Tuning.DropDownTabs.Steering.St;

        steering.Dispatcher.Invoke(() =>
        {
            steering.VelocityDynamicPeekBox.Value = (float)VelDynamicPeekValue.Value;
        });
    }
    
    private static void VelStraightValueChanged(object s, EventArgs e)
    {
        var steering = Tabs.Tuning.DropDownTabs.Steering.St;

        steering.Dispatcher.Invoke(() =>
        {
            steering.VelocityCountersteerBox.Value = (float)VelStraightValue.Value;
        });
    }
    
    private static void VelTimeValueChanged(object s, EventArgs e)
    {
        var steering = Tabs.Tuning.DropDownTabs.Steering.St;

        steering.Dispatcher.Invoke(() =>
        {
            steering.TimeToMaxSteeringBox.Value = (float)VelTimeValue.Value;
        });
    }
    
    public static void InitiateSubMenu()
    {
        AngleMax1Value.ValueChangedHandler += AngleMax1ValueChanged;
        AngleMax2Value.ValueChangedHandler += AngleMax2ValueChanged;
        VelCountersteerValue.ValueChangedHandler += VelCountersteerValueChanged;
        VelStraightValue.ValueChangedHandler += VelStraightValueChanged;
        VelTurningValue.ValueChangedHandler += VelTurningValueChanged;
        VelDynamicPeekValue.ValueChangedHandler += VelDynamicPeekValueChanged;
        VelTimeValue.ValueChangedHandler += VelTimeValueChanged;
    }
}