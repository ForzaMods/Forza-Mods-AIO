using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.UnlocksMenu;

public abstract class CurrencyMenu
{
    private static readonly Overlay.MenuOption XpValue = new("Value", Overlay.MenuOption.OptionType.Int, 0, isEnabled: false);
    private static readonly Overlay.MenuOption XpToggle = new("Toggle", Overlay.MenuOption.OptionType.Bool, false);
    private static readonly Overlay.MenuOption XpPull = new("Pull", Overlay.MenuOption.OptionType.Button, () =>
    {
        UnlocksPage.Up.Dispatcher.Invoke(() =>
        {
            XpValue.Value = Convert.ToInt32(UnlocksPage.Up.XpNum.Value);
        });
    });
    
    private static readonly Overlay.MenuOption CreditsValue = new("Value", Overlay.MenuOption.OptionType.Int, 0);
    private static readonly Overlay.MenuOption CreditsToggle = new("Toggle", Overlay.MenuOption.OptionType.Bool, false);
    private static readonly Overlay.MenuOption CreditsPull = new("Pull", Overlay.MenuOption.OptionType.Button, () =>
    {
        UnlocksPage.Up.Dispatcher.Invoke(() =>
        {
            CreditsValue.Value = Convert.ToInt32(UnlocksPage.Up.CreditsNum.Value);
        });
    });
    
    public static readonly List<Overlay.MenuOption> CurrencyMenuOptions = new()
    {
        new("XP", Overlay.MenuOption.OptionType.SubHeader),
        XpValue,
        XpToggle,
        XpPull,
        new("Credits", Overlay.MenuOption.OptionType.SubHeader),
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