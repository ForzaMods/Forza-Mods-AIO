using System.Collections.Generic;
using System.Windows;
using Forza_Mods_AIO.Resources;

namespace Forza_Mods_AIO.Tabs.Keybindings;

/// <summary>
///     Interaction logic for Keybindings.xaml
/// </summary>
public partial class Keybindings
{
    private readonly UiManager _uiManager;
    
    public Keybindings()
    {
        InitializeComponent();
        _uiManager = new UiManager(this, Sizes, IsClicked);
    }
    
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        _uiManager.ToggleDropDown(sender);
    }

    private static readonly Dictionary<string, double> Sizes = new()
    {
        { "OverlayButton", 450 }, // Button name for page, height of page
        { "HandlingButton", 230 }
    };

    private static readonly Dictionary<string, bool> IsClicked = new()
    {
        { "OverlayButton", false },
        { "HandlingButton", false }
    };
}