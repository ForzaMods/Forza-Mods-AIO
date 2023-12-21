using System;
using System.Collections.Generic;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Alignment;

public abstract class Alignment
{
    private static readonly MenuOption CamberNegValue = new ("Negative Value", -5f);
    private static readonly MenuOption CamberPosValue = new ("Positive Value", 5f);
    
    private static readonly MenuOption ToeNegValue = new ("Negative Value", -5f);
    private static readonly MenuOption ToePosValue = new ("Positive Value", 5f);
    
    private static readonly MenuOption CamberPull = new ("Pull values", () =>
    {
        var alignment = Tabs.Tuning.DropDownTabs.Alignment.Al;
        
        alignment.Dispatcher.Invoke(() =>
        {
            if (alignment.CamberNegBox.Value == null || alignment.CamberPosBox.Value == null)
                return;

            CamberNegValue.Value = (float)alignment.CamberNegBox.Value;
            CamberPosValue.Value = (float)alignment.CamberPosBox.Value;
        });
    });
    
    private static readonly MenuOption ToePull = new ("Pull values",  () =>
    {
        var alignment = Tabs.Tuning.DropDownTabs.Alignment.Al;
        
        alignment.Dispatcher.Invoke(() =>
        {
            if (alignment.ToeNegBox.Value == null || alignment.ToePosBox.Value == null)
                return;

            ToeNegValue.Value = (float)alignment.ToeNegBox.Value;
            ToePosValue.Value = (float)alignment.ToePosBox.Value;
        });
    });

    private static void CamberNegValueChanged(object s, EventArgs e)
    {
        var alignment = Tabs.Tuning.DropDownTabs.Alignment.Al;

        alignment.Dispatcher.Invoke(() =>
        {
            if ((float)s.GetType().GetProperty("Value")!.GetValue(s)! > alignment.CamberNegBox.Maximum)
                CamberNegValue.Value = (float)alignment.CamberNegBox.Maximum;
            else
                alignment.CamberNegBox.Value = (float)Math.Round((float)s.GetType().GetProperty("Value")!.GetValue(s)!, 1);
        });
    }
    
    private static void CamberPosValueChanged(object s, EventArgs e)
    {
        var alignment = Tabs.Tuning.DropDownTabs.Alignment.Al;

        alignment.Dispatcher.Invoke(() =>
        {
            if ((float)s.GetType().GetProperty("Value")!.GetValue(s)! < alignment.CamberPosBox.Minimum)
                CamberPosValue.Value = (float)alignment.CamberPosBox.Minimum;
            else
                alignment.CamberPosBox.Value = (float)Math.Round((float)s.GetType().GetProperty("Value")!.GetValue(s)!, 1);
        });
    }
    
    private static void ToeNegValueChanged(object s, EventArgs e)
    {
        var alignment = Tabs.Tuning.DropDownTabs.Alignment.Al;

        alignment.Dispatcher.Invoke(() =>
        {
            if ((float)s.GetType().GetProperty("Value")!.GetValue(s)! > alignment.ToeNegBox.Maximum)
                ToeNegValue.Value = (float)alignment.ToeNegBox.Maximum;
            else
                alignment.ToeNegBox.Value = (float)Math.Round((float)s.GetType().GetProperty("Value")!.GetValue(s)!, 1);
        });
    }
    
    private static void ToePosValueChanged(object s, EventArgs e)
    {
        var alignment = Tabs.Tuning.DropDownTabs.Alignment.Al;

        alignment.Dispatcher.Invoke(() =>
        {
            if ((float)s.GetType().GetProperty("Value")!.GetValue(s)! < alignment.ToePosBox.Minimum)
                ToePosValue.Value = (float)alignment.ToePosBox.Minimum;
            else
                alignment.ToePosBox.Value = (float)Math.Round((float)s.GetType().GetProperty("Value")!.GetValue(s)!, 1);
        });
    }
    
    public static readonly List<MenuOption> AlignmentOptions = new()
    {
        new("Camber", OptionType.SubHeader),
        CamberNegValue,
        CamberPosValue,
        CamberPull,
        new("Toe", OptionType.SubHeader),
        ToeNegValue,
        ToePosValue,
        ToePull
    };

    public static void InitiateSubMenu()
    {
        CamberNegValue.ValueChangedHandler += CamberNegValueChanged;
        CamberPosValue.ValueChangedHandler += CamberPosValueChanged;
        ToeNegValue.ValueChangedHandler += ToeNegValueChanged;
        ToePosValue.ValueChangedHandler += ToePosValueChanged;
    }
}