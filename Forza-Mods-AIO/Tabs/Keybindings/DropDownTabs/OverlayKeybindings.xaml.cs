using System.Windows;
using System.Windows.Controls;

namespace Forza_Mods_AIO.Tabs.Keybindings.DropDownTabs;

public partial class OverlayKeybindings
{
    public OverlayKeybindings()
    {
        InitializeComponent();
    }

    private void KBButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (MainWindow.Grabbing) return;
        MainWindow.IsClicked = MainWindow.Grabbing = true;
        MainWindow.ClickedButton = (Button)sender;
        MainWindow.ClickedButton.Content = "Change Key";   
    }
    
    private void CTButton_OnClick(object sender, RoutedEventArgs e)
    {
        // TODO: Implement
    }
}