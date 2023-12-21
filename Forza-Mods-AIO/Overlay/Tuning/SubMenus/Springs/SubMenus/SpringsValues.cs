using System;
using System.Collections.Generic;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Springs.SubMenus;

public abstract class SpringsValues
{
    private static readonly MenuOption FrontSpringsMinValue = new ("Min Value", 0f);
    private static readonly MenuOption FrontSpringsMaxValue = new ("Max Value", 0f);
    private static readonly MenuOption RearSpringsMinValue = new ("Min Value", 0f);
    private static readonly MenuOption RearSpringsMaxValue = new ("Max Value", 0f);
    
    private static readonly MenuOption FrontSpringsPull = new ("Pull values",  () =>
    {
        var springs = Tabs.Tuning.DropDownTabs.Springs.Sp;
        
        springs.Dispatcher.Invoke(() =>
        {
            FrontSpringsMinValue.Value = (float)springs.SpringFrontMinBox.Value!;
            FrontSpringsMaxValue.Value = (float)springs.SpringFrontMaxBox.Value!;
        });
    });
    
    private static readonly MenuOption RearSpringsPull = new ("Pull values",  () =>
    {
        var springs = Tabs.Tuning.DropDownTabs.Springs.Sp;
        
        springs.Dispatcher.Invoke(() =>
        {
            RearSpringsMinValue.Value = (float)springs.SpringRearMinBox.Value!;
            RearSpringsMaxValue.Value = (float)springs.SpringRearMaxBox.Value!;
        });
    });
    
    public static readonly List<MenuOption> SpringsSubMenuOptions = new()
    {
        new ("Front Springs", OptionType.SubHeader),
        FrontSpringsMinValue,
        FrontSpringsMaxValue,
        FrontSpringsPull,
        new ("Rear Springs", OptionType.SubHeader),
        RearSpringsMinValue,
        RearSpringsMaxValue,
        RearSpringsPull
    };
    
    public static void InitiateSubMenu()
    {
        FrontSpringsMinValue.ValueChangedHandler += FrontSpringsMinValueChanged;
        FrontSpringsMaxValue.ValueChangedHandler += FrontSpringsMaxValueChanged;
        RearSpringsMinValue.ValueChangedHandler += RearSpringsMinValueChanged;
        RearSpringsMaxValue.ValueChangedHandler += RearSpringsMaxValueChanged;
    }

    private static void FrontSpringsMinValueChanged(object s, EventArgs e)
    {
        var springs = Tabs.Tuning.DropDownTabs.Springs.Sp;

        springs.Dispatcher.Invoke(() =>
        {
            springs.SpringFrontMinBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void FrontSpringsMaxValueChanged(object s, EventArgs e)
    {
        var springs = Tabs.Tuning.DropDownTabs.Springs.Sp;

        springs.Dispatcher.Invoke(() =>
        {
            springs.SpringFrontMaxBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void RearSpringsMinValueChanged(object s, EventArgs e)
    {
        var springs = Tabs.Tuning.DropDownTabs.Springs.Sp;

        springs.Dispatcher.Invoke(() =>
        {
            springs.SpringRearMinBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void RearSpringsMaxValueChanged(object s, EventArgs e)
    {
        var springs = Tabs.Tuning.DropDownTabs.Springs.Sp;

        springs.Dispatcher.Invoke(() =>
        {
            springs.SpringRearMaxBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
}