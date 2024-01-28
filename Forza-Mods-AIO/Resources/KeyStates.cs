using System.Windows.Input;

namespace Forza_Mods_AIO.Resources;

public abstract class KeyStates
{
    public static void UpdateKeyState(Key key, ref bool keyDownBool)
    {
        keyDownBool = IsKeyPressed(key) switch
        {
            true when !keyDownBool => true,
            false when keyDownBool => false,
            _ => keyDownBool
        };
    }

    public static bool IsKeyPressed(Key key)
    {
        return MainWindow.Mw.Dispatcher.Invoke(() => Keyboard.IsKeyDown(key));
    }
}