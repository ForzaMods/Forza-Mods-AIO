using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Overlay.Options;
using static System.Convert;
using static Forza_Mods_AIO.Overlay.Options.FloatOption;
using static Forza_Mods_AIO.Tabs.Tuning.DropDownTabs.Alignment;

namespace Forza_Mods_AIO.Overlay.Menus.TuningMenu.SubMenus.Alignment;

public abstract class Alignment
{
    private static readonly FloatOption CamberNegValue = CreateWithMaximum("Negative Value", -5f, Al.CamberNegBox.Maximum);
    private static readonly FloatOption CamberPosValue = CreateWithMinimum("Positive Value", 5f, Al.CamberPosBox.Minimum);
    
    private static readonly FloatOption ToeNegValue = CreateWithMaximum("Negative Value", -5f, Al.ToeNegBox.Maximum);
    private static readonly FloatOption ToePosValue = CreateWithMinimum("Positive Value", 5f, Al.ToePosBox.Maximum);
    
    private static readonly ButtonOption CamberPull = new("Pull values", () =>
    {
        CamberNegValue.Value = ToSingle(Al.CamberNegBox.Value);
        CamberPosValue.Value = ToSingle(Al.CamberPosBox.Value);
    });
    
    private static readonly ButtonOption ToePull = new ("Pull values",  () =>
    {                                                     
        ToeNegValue.Value = ToSingle(Al.ToeNegBox.Value);                      
        ToePosValue.Value = ToSingle(Al.ToePosBox.Value);                      
    });

    private static void CamberNegValueChanged(object s, EventArgs e)
    {
        Al.CamberNegBox.Value = CamberNegValue.Value;
    }
    
    private static void CamberPosValueChanged(object s, EventArgs e)
    {
        Al.CamberPosBox.Value = CamberPosValue.Value;
    }
    
    private static void ToeNegValueChanged(object s, EventArgs e)
    {
        Al.ToeNegBox.Value = ToeNegValue.Value;
    }
    
    private static void ToePosValueChanged(object s, EventArgs e)
    {
        Al.ToePosBox.Value = ToePosValue.Value;
    }
    
    public static readonly List<MenuOption> AlignmentOptions = new()
    {
        new SubHeaderOption("Camber"),
        CamberNegValue,
        CamberPosValue,
        CamberPull,
        new SubHeaderOption("Toe"),
        ToeNegValue,
        ToePosValue,
        ToePull
    };

    public static void InitiateSubMenu()
    {
        CamberNegValue.ValueChanged += CamberNegValueChanged;
        CamberPosValue.ValueChanged += CamberPosValueChanged;
        ToeNegValue.ValueChanged += ToeNegValueChanged;
        ToePosValue.ValueChanged += ToePosValueChanged;
    }
}