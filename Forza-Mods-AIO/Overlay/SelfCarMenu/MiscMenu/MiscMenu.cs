using System;
using System.Collections.Generic;
using Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

namespace Forza_Mods_AIO.Overlay.SelfCarMenu.MiscMenu;

public abstract class MiscMenu
{
    private static readonly Overlay.MenuOption UnlimitedBuildBudgetToggle = new("Unlimited Build Budget", Overlay.MenuOption.OptionType.Bool, false);
    private static readonly Overlay.MenuOption UnbreakableSkillScoreToggle = new("Unbreakable Skill Score", Overlay.MenuOption.OptionType.Bool, false);

    public static readonly List<Overlay.MenuOption> MiscMenuOptions = new()
    {
        UnlimitedBuildBudgetToggle,
        UnbreakableSkillScoreToggle
    };

    public static void InitiateSubMenu()
    {
        UnlimitedBuildBudgetToggle.ValueChangedHandler += UnlimitedBuildBudgetToggleChanged;
        UnbreakableSkillScoreToggle.ValueChangedHandler += UnbreakableSkillScoreToggleChanged;
    }

    // Event handlers
    private static void UnlimitedBuildBudgetToggleChanged(object s, EventArgs e)
    {
        MiscellaneousPage._MiscPage.Dispatcher.Invoke(() =>
        {
            MiscellaneousPage._MiscPage.RemoveBuildCapSwitch.IsOn = (bool)UnlimitedBuildBudgetToggle.Value;
        });
    }

    private static void UnbreakableSkillScoreToggleChanged(object s, EventArgs e)
    {
        MiscellaneousPage._MiscPage.Dispatcher.Invoke(() =>
        {
            MiscellaneousPage._MiscPage.UnbreakableSkillScoreSwitch.IsOn = (bool)UnbreakableSkillScoreToggle.Value;
        });
    }
}