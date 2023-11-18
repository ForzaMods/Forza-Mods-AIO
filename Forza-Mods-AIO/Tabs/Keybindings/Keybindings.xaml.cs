using System.Collections.Generic;
using System.Windows;
using Forza_Mods_AIO.Resources;

namespace Forza_Mods_AIO.Tabs.Keybindings;

/// <summary>
///     Interaction logic for Settings.xaml
/// </summary>
public partial class Keybindings
{
    public Keybindings()
    {
        InitializeComponent();
    }
    
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (!UpdateUi.AnimCompleted) return;
        UpdateUi.Animate(sender, _isClicked[sender.GetType().GetProperty("Name").GetValue(sender).ToString()], Sizes, _isClicked, this);
        _isClicked[sender.GetType().GetProperty("Name").GetValue(sender).ToString()] = !_isClicked[sender.GetType().GetProperty("Name").GetValue(sender).ToString()];
    }

    private static readonly Dictionary<string, double> Sizes = new()
    {
        { "OverlayButton", 400 }, // Button name for page, height of page
        { "HandlingButton", 180 }
    };

    private static Dictionary<string, bool> _isClicked = new()
    {
        { "OverlayButton", false },
        { "HandlingButton", false }
    };
}