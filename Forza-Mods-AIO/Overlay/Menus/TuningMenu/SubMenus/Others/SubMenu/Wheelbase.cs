using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;
using static Forza_Mods_AIO.Tabs.Tuning.DropDownTabs.Others;

namespace Forza_Mods_AIO.Overlay.Menus.TuningMenu.SubMenus.Others.SubMenu;

public abstract class Wheelbase
{
    private static readonly FloatOption WheelbaseValue = new("Wheelbase", 0f);
    private static readonly FloatOption FrontWidthValue = new("Front Width", 0f);
    private static readonly FloatOption RearWidthValue = new("Rear Width", 0f);
    private static readonly FloatOption FrontSpacerValue = new("Front Spacer", 0f);
    private static readonly FloatOption RearSpacerValue = new("Rear Spacer", 0f);
    
    private static readonly ButtonOption WheelbasePull = new("Pull values",  () =>
    {
        WheelbaseValue.Value = Convert.ToSingle(O.WheelbaseBox.Value);
        FrontWidthValue.Value = Convert.ToSingle(O.FrontWidthBox.Value);
        RearWidthValue.Value = Convert.ToSingle(O.RearWidthBox.Value);
        FrontSpacerValue.Value = Convert.ToSingle(O.FrontSpacerBox.Value);
        RearSpacerValue.Value = Convert.ToSingle(O.RearSpacerBox.Value);
    });

    public static readonly List<MenuOption> WheelbaseOptions = new()
    {
        new SubHeaderOption("Wheelbase"),
        WheelbaseValue,
        FrontWidthValue,
        RearWidthValue,
        FrontSpacerValue,
        RearSpacerValue,
        WheelbasePull
    };

    public static void InitiateSubMenu()
    {
        WheelbaseValue.ValueChanged += WheelbaseValueChanged;
        FrontWidthValue.ValueChanged += FrontWidthValueChanged;
        RearWidthValue.ValueChanged += RearWidthValueChanged;
        FrontSpacerValue.ValueChanged += FrontSpacerValueChanged;
        RearSpacerValue.ValueChanged += RearSpacerValueChanged;
    }

    private static void WheelbaseValueChanged(object s, EventArgs e)
    {
        O.WheelbaseBox.Value = WheelbaseValue.Value;
    }
    
    private static void FrontWidthValueChanged(object s, EventArgs e)
    {
        O.FrontWidthBox.Value = FrontWidthValue.Value;
    }
    
    private static void RearWidthValueChanged(object s, EventArgs e)
    {
        O.RearWidthBox.Value = RearWidthValue.Value;
    }
    
    private static void FrontSpacerValueChanged(object s, EventArgs e)
    {
        O.FrontSpacerBox.Value = FrontSpacerValue.Value;
    }
    
    private static void RearSpacerValueChanged(object s, EventArgs e)
    {
        O.RearSpacerBox.Value = RearSpacerValue.Value;
    }
}