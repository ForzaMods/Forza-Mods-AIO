using System;
using System.Collections.Generic;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Others.SubMenu;

public abstract class Wheelbase
{
    private static readonly Overlay.MenuOption WheelbaseValue = new ("Wheelbase", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption FrontWidthValue = new ("Front Width", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption RearWidthValue = new ("Rear Width", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption FrontSpacerValue = new ("Front Spacer", Overlay.MenuOption.OptionType.Float, 0f);
    private static readonly Overlay.MenuOption RearSpacerValue = new ("Rear Spacer", Overlay.MenuOption.OptionType.Float, 0f);
    
    private static readonly Overlay.MenuOption WheelbasePull = new ("Pull values", Overlay.MenuOption.OptionType.Button, new Action(() =>
    {
        var Others = Tabs.Tuning.DropDownTabs.Others.o;
        
        Others.Dispatcher.Invoke(() =>
        {
            WheelbaseValue.Value = (float)Others.WheelbaseBox.Value!;
            FrontWidthValue.Value = (float)Others.FrontWidthBox.Value!;
            RearWidthValue.Value = (float)Others.RearWidthBox.Value!;
            FrontSpacerValue.Value = (float)Others.FrontSpacerBox.Value!;
            RearSpacerValue.Value = (float)Others.RearSpacerBox.Value!;
        });
    }));

    public static readonly List<Overlay.MenuOption> WheelbaseOptions = new()
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
        var Others = Tabs.Tuning.DropDownTabs.Others.o;

        Others.Dispatcher.Invoke(() =>
        {
            Others.WheelbaseBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void FrontWidthValueChanged(object s, EventArgs e)
    {
        var Others = Tabs.Tuning.DropDownTabs.Others.o;

        Others.Dispatcher.Invoke(() =>
        {
            Others.FrontWidthBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void RearWidthValueChanged(object s, EventArgs e)
    {
        var Others = Tabs.Tuning.DropDownTabs.Others.o;

        Others.Dispatcher.Invoke(() =>
        {
            Others.RearWidthBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void FrontSpacerValueChanged(object s, EventArgs e)
    {
        var Others = Tabs.Tuning.DropDownTabs.Others.o;

        Others.Dispatcher.Invoke(() =>
        {
            Others.FrontSpacerBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
    
    private static void RearSpacerValueChanged(object s, EventArgs e)
    {
        var Others = Tabs.Tuning.DropDownTabs.Others.o;

        Others.Dispatcher.Invoke(() =>
        {
            Others.RearSpacerBox.Value = (float)s.GetType().GetProperty("Value")!.GetValue(s)!;
        });
    }
}