using System;
using System.Collections.Generic;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Steering;

public abstract class Steering
{
    
    private static readonly Overlay.MenuOption AngleMax1Value = new ("Max 1", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption AngleMax2Value = new ("Max 2", Overlay.MenuOption.OptionType.Float, 0f);
    
    private static readonly Overlay.MenuOption AnglePull = new ("Pull values", Overlay.MenuOption.OptionType.Button, new Action(() =>
    {
        var Steering = Tabs.Tuning.DropDownTabs.Steering.st;
        
        Steering.Dispatcher.Invoke(() =>
        {
            if (Steering.AngleMaxBox.Value == null || Steering.AngleMax2Box.Value == null)
                return;

            AngleMax1Value.Value = (float)Steering.AngleMaxBox.Value;
            AngleMax2Value.Value = (float)Steering.AngleMax2Box.Value;
        });
    }));
    
        
    private static readonly Overlay.MenuOption VelCountersteerValue = new ("Countersteer Value", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption VelStraightValue = new ("Straight Value", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption VelTurningValue = new ("Turning Value", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption VelDynamicPeekValue = new ("Dynamic Peek Value", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption VelTimeValue = new ("Time Value", Overlay.MenuOption.OptionType.Float, 0f);
    
    private static readonly Overlay.MenuOption ToePull = new ("Pull values", Overlay.MenuOption.OptionType.Button, new Action(() =>
    {
        var Steering = Tabs.Tuning.DropDownTabs.Steering.st;
        
        Steering.Dispatcher.Invoke(() =>
        {
            if (Steering.VelocityCountersteerBox.Value == null || Steering.VelocityStraightBox.Value == null || Steering.VelocityTurningBox.Value == null || Steering.VelocityDynamicPeekBox.Value == null || Steering.TimeToMaxSteeringBox.Value == null)
                return;

            VelCountersteerValue.Value = (float)Steering.VelocityCountersteerBox.Value;
            VelStraightValue.Value = (float)Steering.VelocityStraightBox.Value;
            VelTurningValue.Value = (float)Steering.VelocityTurningBox.Value;
            VelDynamicPeekValue.Value = (float)Steering.VelocityDynamicPeekBox.Value;
            VelTimeValue.Value = (float)Steering.TimeToMaxSteeringBox.Value;
        });
    }));
    
    public static readonly List<Overlay.MenuOption> SteeringOptions = new()
    {
        new("Angle", Overlay.MenuOption.OptionType.SubHeader),
        AngleMax1Value,
        AngleMax2Value,
        AnglePull,
        new("Velocity", Overlay.MenuOption.OptionType.SubHeader),
        VelCountersteerValue,
        VelStraightValue,
        VelTurningValue,
        VelDynamicPeekValue,
        VelTimeValue,
        ToePull
    };

    private static void AngleMax1ValueChanged(object s, EventArgs e)
    {
        var Steering = Tabs.Tuning.DropDownTabs.Steering.st;

        Steering.Dispatcher.Invoke(() =>
        {
            Steering.AngleMaxBox.Value = (float)AngleMax1Value.Value;
        });
    }
    
    private static void AngleMax2ValueChanged(object s, EventArgs e)
    {
        var Steering = Tabs.Tuning.DropDownTabs.Steering.st;

        Steering.Dispatcher.Invoke(() =>
        {
            Steering.AngleMax2Box.Value = (float)AngleMax2Value.Value;
        });
    }
    
    private static void VelCountersteerValueChanged(object s, EventArgs e)
    {
        var Steering = Tabs.Tuning.DropDownTabs.Steering.st;

        Steering.Dispatcher.Invoke(() =>
        {
            Steering.VelocityCountersteerBox.Value = (float)VelCountersteerValue.Value;
        });
    }
    
    private static void VelTurningValueChanged(object s, EventArgs e)
    {
        var Steering = Tabs.Tuning.DropDownTabs.Steering.st;

        Steering.Dispatcher.Invoke(() =>
        {
            Steering.VelocityTurningBox.Value = (float)VelTurningValue.Value;
        });
    }
    
    private static void VelDynamicPeekValueChanged(object s, EventArgs e)
    {
        var Steering = Tabs.Tuning.DropDownTabs.Steering.st;

        Steering.Dispatcher.Invoke(() =>
        {
            Steering.VelocityDynamicPeekBox.Value = (float)VelDynamicPeekValue.Value;
        });
    }
    
    private static void VelStraightValueChanged(object s, EventArgs e)
    {
        var Steering = Tabs.Tuning.DropDownTabs.Steering.st;

        Steering.Dispatcher.Invoke(() =>
        {
            Steering.VelocityCountersteerBox.Value = (float)VelStraightValue.Value;
        });
    }
    
    private static void VelTimeValueChanged(object s, EventArgs e)
    {
        var Steering = Tabs.Tuning.DropDownTabs.Steering.st;

        Steering.Dispatcher.Invoke(() =>
        {
            Steering.TimeToMaxSteeringBox.Value = (float)VelTimeValue.Value;
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