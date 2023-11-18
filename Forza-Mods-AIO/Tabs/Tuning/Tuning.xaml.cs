using System.Collections.Generic;
using System.Windows;
using Forza_Mods_AIO.Resources;

namespace Forza_Mods_AIO.Tabs.Tuning;

public partial class Tuning
{
    public static Tuning? T;

    public Tuning()
    {
        InitializeComponent();
        T = this;
        UpdateUi.UpdateUI(false, this);
    }

    #region Interaction
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (!UpdateUi.AnimCompleted) return;
        var senderName = sender.GetType().GetProperty("Name")?.GetValue(sender)?.ToString();
        UpdateUi.Animate(sender, IsClicked[senderName], Sizes, IsClicked, this);
        IsClicked[senderName] = !IsClicked[senderName];
    }
        
    private static readonly Dictionary<string, double> Sizes = new()
    {
        { "TiresButton" , 240 }, // Button name for page, height of page
        { "GearingButton" , 275 },
        { "AlignmentButton" , 120 },
        { "SpringsButton" , 295 },
        { "DampingButton" , 460 },
        { "AeroButton", 120 },
        { "SteeringButton", 275 },
        { "OthersButton", 395 },
    };

    private static readonly Dictionary<string, bool> IsClicked = new()
    {
        // Tuning
        {"TiresButton", false },
        {"GearingButton", false },
        {"AlignmentButton", false },
        {"SpringsButton", false },
        {"DampingButton", false },
        {"AeroButton", false },
        {"SteeringButton", false },
        {"OthersButton", false }
    };
    #endregion
}