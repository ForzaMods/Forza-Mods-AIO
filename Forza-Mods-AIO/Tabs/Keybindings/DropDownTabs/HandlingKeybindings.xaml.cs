using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Forza_Mods_AIO.MainWindow;

namespace Forza_Mods_AIO.Tabs.Keybindings.DropDownTabs;

public partial class HandlingKeybindings
{
    public HandlingKeybindings()
    {
        InitializeComponent();
        LoadKeybindings();
    }
    
    private void CTButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button)
        {
            return;
        }
        
        button.Content = "Change Key";
        Mw.Gamepad.GetAndSetXInputKey(button);
    }

    private void LoadKeybindings()
    {

    }

    private void KeybindingButton_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (sender is not Button button)
        {
            return;
        }

        Forza_Mods_AIO.Resources.Keybindings.ChangeKeybinding(ref button, e.Key);
    }
}