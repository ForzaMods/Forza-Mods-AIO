using System;
using System.Collections.Generic;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Others.SubMenu;

public abstract class Wheelbase
{
    private static readonly MenuOption WheelbaseValue = new ("Wheelbase", 0f);
    private static readonly MenuOption FrontWidthValue = new ("Front Width", 0f);
    private static readonly MenuOption RearWidthValue = new ("Rear Width", 0f);
    private static readonly MenuOption FrontSpacerValue = new ("Front Spacer", 0f);
    private static readonly MenuOption RearSpacerValue = new ("Rear Spacer", 0f);
    
    private static readonly MenuOption WheelbasePull = new ("Pull values",  () =>
    {
        var others = Tabs.Tuning.DropDownTabs.Others.O;
        
        others.Dispatcher.Invoke(() =>
        {
            WheelbaseValue.Value = (float)others.WheelbaseBox.Value!;
            FrontWidthValue.Value = (float)others.FrontWidthBox.Value!;
            RearWidthValue.Value = (float)others.RearWidthBox.Value!;
            FrontSpacerValue.Value = (float)others.FrontSpacerBox.Value!;
            RearSpacerValue.Value = (float)others.RearSpacerBox.Value!;
        });
    });

    public static readonly List<MenuOption> WheelbaseOptions = new()
    {
        WheelbaseValue,
        FrontWidthValue,
        RearWidthValue,
        FrontSpacerValue,
        RearSpacerValue,
        WheelbasePull
    };

    public static void InitiateSubMenu()
    {
        WheelbaseValue.ValueChangedHandler += WheelbaseValueChanged;
        FrontWidthValue.ValueChangedHandler += FrontWidthValueChanged;
        RearWidthValue.ValueChangedHandler += RearWidthValueChanged;
        FrontSpacerValue.ValueChangedHandler += FrontSpacerValueChanged;
        RearSpacerValue.ValueChangedHandler += RearSpacerValueChanged;
    }

    private static void WheelbaseValueChanged(object s, EventArgs e)
    {
        var others = Tabs.Tuning.DropDownTabs.Others.O;

        others.Dispatcher.Invoke(() =>
        {
            others.WheelbaseBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void FrontWidthValueChanged(object s, EventArgs e)
    {
        var others = Tabs.Tuning.DropDownTabs.Others.O;

        others.Dispatcher.Invoke(() =>
        {
            others.FrontWidthBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void RearWidthValueChanged(object s, EventArgs e)
    {
        var others = Tabs.Tuning.DropDownTabs.Others.O;

        others.Dispatcher.Invoke(() =>
        {
            others.RearWidthBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void FrontSpacerValueChanged(object s, EventArgs e)
    {
        var others = Tabs.Tuning.DropDownTabs.Others.O;

        others.Dispatcher.Invoke(() =>
        {
            others.FrontSpacerBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void RearSpacerValueChanged(object s, EventArgs e)
    {
        var others = Tabs.Tuning.DropDownTabs.Others.O;

        others.Dispatcher.Invoke(() =>
        {
            others.RearSpacerBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
}