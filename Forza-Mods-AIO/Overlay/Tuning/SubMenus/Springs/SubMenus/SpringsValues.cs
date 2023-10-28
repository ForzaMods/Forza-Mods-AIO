using System;
using System.Collections.Generic;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Springs.SubMenus;

public abstract class SpringsValues
{
    private static readonly Overlay.MenuOption FrontSpringsMinValue = new ("Min Value", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption FrontSpringsMaxValue = new ("Max Value", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption RearSpringsMinValue = new ("Min Value", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption RearSpringsMaxValue = new ("Max Value", Overlay.MenuOption.OptionType.Float, 0f);
    
    private static readonly Overlay.MenuOption FrontSpringsPull = new ("Pull values", Overlay.MenuOption.OptionType.Button, new Action(() =>
    {
        var Springs = Tabs.Tuning.DropDownTabs.Springs.sp;
        
        Springs.Dispatcher.Invoke(() =>
        {
            FrontSpringsMinValue.Value = (float)Springs.SpringFrontMinBox.Value!;
            FrontSpringsMaxValue.Value = (float)Springs.SpringFrontMaxBox.Value!;
        });
    }));
    
    private static readonly Overlay.MenuOption RearSpringsPull = new ("Pull values", Overlay.MenuOption.OptionType.Button, new Action(() =>
    {
        var Springs = Tabs.Tuning.DropDownTabs.Springs.sp;
        
        Springs.Dispatcher.Invoke(() =>
        {
            RearSpringsMinValue.Value = (float)Springs.SpringRearMinBox.Value!;
            RearSpringsMaxValue.Value = (float)Springs.SpringRearMaxBox.Value!;
        });
    }));
    
    public static readonly List<Overlay.MenuOption> SpringsSubMenuOptions = new()
    {
        new ("Front Springs", Overlay.MenuOption.OptionType.SubHeader),
        FrontSpringsMinValue,
        FrontSpringsMaxValue,
        FrontSpringsPull,
        new ("Rear Springs", Overlay.MenuOption.OptionType.SubHeader),
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
        var Springs = Tabs.Tuning.DropDownTabs.Springs.sp;

        Springs.Dispatcher.Invoke(() =>
        {
            Springs.SpringFrontMinBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void FrontSpringsMaxValueChanged(object s, EventArgs e)
    {
        var Springs = Tabs.Tuning.DropDownTabs.Springs.sp;

        Springs.Dispatcher.Invoke(() =>
        {
            Springs.SpringFrontMaxBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void RearSpringsMinValueChanged(object s, EventArgs e)
    {
        var Springs = Tabs.Tuning.DropDownTabs.Springs.sp;

        Springs.Dispatcher.Invoke(() =>
        {
            Springs.SpringRearMinBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void RearSpringsMaxValueChanged(object s, EventArgs e)
    {
        var Springs = Tabs.Tuning.DropDownTabs.Springs.sp;

        Springs.Dispatcher.Invoke(() =>
        {
            Springs.SpringRearMaxBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
}