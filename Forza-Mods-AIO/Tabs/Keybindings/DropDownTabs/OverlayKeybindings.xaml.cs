using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

using static Forza_Mods_AIO.MainWindow;

namespace Forza_Mods_AIO.Tabs.Keybindings.DropDownTabs;

public partial class OverlayKeybindings
{
    public OverlayKeybindings()
    {
        InitializeComponent();
        Loaded += (_,_ ) => UpdateButtons();
    }

    private void ControllerButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button)
        {
            return;
        }
        
        Mw.Gamepad.GetAndSetXInputKey(button);
    }
    
    private void UpdateButtons()
    {
        var buttons = this.GetChildren().Where(f => f.GetType() == typeof(Button)).Cast<Button>();
        Forza_Mods_AIO.Resources.Keybindings.UpdateButtons(buttons);
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