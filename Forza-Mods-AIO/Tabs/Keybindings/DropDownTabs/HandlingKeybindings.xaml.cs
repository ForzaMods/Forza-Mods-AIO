using System.Windows;
using System.Windows.Controls;
using Forms = System.Windows.Forms;

namespace Forza_Mods_AIO.Tabs.Keybindings.DropDownTabs;

public partial class HandlingKeybindings
{
    public static Forms.Keys JumpHackKey, BrakeHackKey, SpeedhackKey;
    
    public HandlingKeybindings()
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