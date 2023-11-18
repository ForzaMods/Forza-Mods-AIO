using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;
using static Forza_Mods_AIO.Overlay.Overlay;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.UnlocksMenu;

public abstract class CurrencyMenu
{
    private static readonly MenuOption XpValue = new("Value", OptionType.Int, 0, isEnabled: false);
    private static readonly MenuOption XpToggle = new("Toggle", OptionType.Bool, false);
    private static readonly MenuOption XpPull = new("Pull", OptionType.Button, () =>
    {
        UnlocksPage.Up.Dispatcher.Invoke(() =>
        {
            XpValue.Value = Convert.ToInt32(UnlocksPage.Up.XpNum.Value);
        });
    });
    
    private static readonly MenuOption CreditsValue = new("Value", OptionType.Int, 0);
    private static readonly MenuOption CreditsToggle = new("Toggle", OptionType.Bool, false);
    private static readonly MenuOption CreditsPull = new("Pull", OptionType.Button, () =>
    {
        UnlocksPage.Up.Dispatcher.Invoke(() =>
        {
            CreditsValue.Value = Convert.ToInt32(UnlocksPage.Up.CreditsNum.Value);
        });
    });
    
    public static readonly List<MenuOption> CurrencyMenuOptions = new()
    {
        new("XP", OptionType.SubHeader),
        XpValue,
        XpToggle,
        XpPull,
        new("Credits", OptionType.SubHeader),
        CreditsValue,
        CreditsToggle,
        CreditsPull
    };
    
    public static void InitiateSubMenu()
    {
        XpValue.ValueChangedHandler += XpValue_OnChanged;
        XpToggle.ValueChangedHandler += XpToggle_OnChanged;
        CreditsValue.ValueChangedHandler += CreditsValue_OnChanged;
        CreditsToggle.ValueChangedHandler += CreditsToggle_OnChanged;
    }

    private static void XpValue_OnChanged(object s, EventArgs e)
    {
        UnlocksPage.Up.Dispatcher.Invoke(() =>
        {
            UnlocksPage.Up.XpNum.Value = (int)XpValue.Value;
        });
    }
    
    private static void XpToggle_OnChanged(object s, EventArgs e)
    {
        UnlocksPage.Up.Dispatcher.Invoke(() =>
        {
            XpValue.IsEnabled = true;
            UnlocksPage.Up.XpSwitch.IsOn = (bool)XpToggle.Value;
        });
    }
    
    private static void CreditsValue_OnChanged(object s, EventArgs e)
    {
        UnlocksPage.Up.Dispatcher.Invoke(() =>
        {
            UnlocksPage.Up.CreditsNum.Value = (int)CreditsValue.Value;
        });
    }
    
    private static void CreditsToggle_OnChanged(object s, EventArgs e)
    {
        UnlocksPage.Up.Dispatcher.Invoke(() =>
        {
            UnlocksPage.Up.CreditsSwitch.IsOn = (bool)CreditsToggle.Value;
        });
    }
}