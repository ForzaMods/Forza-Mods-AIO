using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Forza_Mods_AIO.Resources;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle;

/// <summary>
///     Interaction logic for Self_Vehicle.xaml
/// </summary>
public partial class Self_Vehicle : Page
{
    public static Self_Vehicle sv;

    public Self_Vehicle()
    {
        InitializeComponent();
        sv = this;
        UpdateUi.UpdateUI(false, this);
    }
    
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (!UpdateUi.AnimCompleted) return;
        UpdateUi.Animate(sender, IsClicked[sender.GetType().GetProperty("Name").GetValue(sender).ToString()], Sizes, IsClicked, this);
        IsClicked[sender.GetType().GetProperty("Name").GetValue(sender).ToString()] = !IsClicked[sender.GetType().GetProperty("Name").GetValue(sender).ToString()];
    }

    private static readonly Dictionary<string, double> Sizes = new()
    {
        { "HandlingButton", 464 }, // Button name for page, height of page
        { "UnlocksButton", 180 },
        { "PhotomodeButton", 285 },
        { "StatsButton", 70 },
        { "TeleportsButton", 70 },
        { "EnvironmentButton", 235 },
        { "CustomizationButton", 70 },
        { "MiscellaneousButton", 70 },
        { "FovButton", 347.5 }
    };

    private static Dictionary<string, bool> IsClicked = new()
    {
        { "HandlingButton", false },
        { "UnlocksButton", false },
        { "PhotomodeButton", false },
        { "StatsButton", false },
        { "TeleportsButton", false },
        { "EnvironmentButton", false },
        { "CustomizationButton", false },
        { "MiscellaneousButton", false },
        { "FovButton", false }
    };
}