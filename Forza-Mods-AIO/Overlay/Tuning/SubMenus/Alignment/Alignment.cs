using System;
using System.Collections.Generic;

namespace Forza_Mods_AIO.Overlay.Tuning.SubMenus.Alignment;

public abstract class Alignment
{
    private static readonly Overlay.MenuOption CamberNegValue = new ("Negative Value", Overlay.MenuOption.OptionType.Float, -5f);
    private static readonly Overlay.MenuOption CamberPosValue = new ("Positive Value", Overlay.MenuOption.OptionType.Float, 5f);
    
    private static readonly Overlay.MenuOption ToeNegValue = new ("Negative Value", Overlay.MenuOption.OptionType.Float, -5f);
    private static readonly Overlay.MenuOption ToePosValue = new ("Positive Value", Overlay.MenuOption.OptionType.Float, 5f);
    
    private static readonly Overlay.MenuOption CamberPull = new ("Pull values",Overlay.MenuOption.OptionType.Button, new Action(() =>
    {
        var Alignment = Tabs.Tuning.DropDownTabs.Alignment.al;
        
        Alignment.Dispatcher.Invoke(() =>
        {
            if (Alignment.CamberNegBox.Value == null || Alignment.CamberPosBox.Value == null)
                return;

            CamberNegValue.Value = (float)Alignment.CamberNegBox.Value;
            CamberPosValue.Value = (float)Alignment.CamberPosBox.Value;
        });
    }));
    
    private static readonly Overlay.MenuOption ToePull = new ("Pull values", Overlay.MenuOption.OptionType.Button, new Action(() =>
    {
        var Alignment = Tabs.Tuning.DropDownTabs.Alignment.al;
        
        Alignment.Dispatcher.Invoke(() =>
        {
            if (Alignment.ToeNegBox.Value == null || Alignment.ToePosBox.Value == null)
                return;

            ToeNegValue.Value = (float)Alignment.ToeNegBox.Value;
            ToePosValue.Value = (float)Alignment.ToePosBox.Value;
        });
    }));

    private static void CamberNegValueChanged(object s, EventArgs e)
    {
        var Alignment = Tabs.Tuning.DropDownTabs.Alignment.al;

        Alignment.Dispatcher.Invoke(() =>
        {
            if ((float)s.GetType().GetProperty("Value")!.GetValue(s)! > Alignment.CamberNegBox.Maximum)
                CamberNegValue.Value = (float)Alignment.CamberNegBox.Maximum;
            else
                Alignment.CamberNegBox.Value = (float)Math.Round((float)s.GetType().GetProperty("Value")!.GetValue(s)!, 1);
        });
    }
    
    private static void CamberPosValueChanged(object s, EventArgs e)
    {
        var Alignment = Tabs.Tuning.DropDownTabs.Alignment.al;

        Alignment.Dispatcher.Invoke(() =>
        {
            if ((float)s.GetType().GetProperty("Value")!.GetValue(s)! < Alignment.CamberPosBox.Minimum)
                CamberPosValue.Value = (float)Alignment.CamberPosBox.Minimum;
            else
                Alignment.CamberPosBox.Value = (float)Math.Round((float)s.GetType().GetProperty("Value")!.GetValue(s)!, 1);
        });
    }
    
    private static void ToeNegValueChanged(object s, EventArgs e)
    {
        var Alignment = Tabs.Tuning.DropDownTabs.Alignment.al;

        Alignment.Dispatcher.Invoke(() =>
        {
            if ((float)s.GetType().GetProperty("Value")!.GetValue(s)! > Alignment.ToeNegBox.Maximum)
                ToeNegValue.Value = (float)Alignment.ToeNegBox.Maximum;
            else
                Alignment.ToeNegBox.Value = (float)Math.Round((float)s.GetType().GetProperty("Value")!.GetValue(s)!, 1);
        });
    }
    
    private static void ToePosValueChanged(object s, EventArgs e)
    {
        var Alignment = Tabs.Tuning.DropDownTabs.Alignment.al;

        Alignment.Dispatcher.Invoke(() =>
        {
            if ((float)s.GetType().GetProperty("Value")!.GetValue(s)! < Alignment.ToePosBox.Minimum)
                ToePosValue.Value = (float)Alignment.ToePosBox.Minimum;
            else
                Alignment.ToePosBox.Value = (float)Math.Round((float)s.GetType().GetProperty("Value")!.GetValue(s)!, 1);
        });
    }
    
    public static readonly List<Overlay.MenuOption> AlignmentOptions = new()
    {
        new("Camber", Overlay.MenuOption.OptionType.SubHeader),
        CamberNegValue,
        CamberPosValue,
        CamberPull,
        new("Toe", Overlay.MenuOption.OptionType.SubHeader),
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