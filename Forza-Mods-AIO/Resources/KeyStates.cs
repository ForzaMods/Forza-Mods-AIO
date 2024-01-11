using System.Windows.Forms;

namespace Forza_Mods_AIO.Resources;

public abstract class KeyStates
{
    public static void UpdateKeyState(Keys key, ref bool keyDownBool)
    {
        keyDownBool = IsKeyPressed(key) switch
        {
            true when !keyDownBool => true,
            false when keyDownBool => false,
            _ => keyDownBool
        };
    }

    public static bool IsKeyPressed(Keys key)
    {
        return (DllImports.GetAsyncKeyState(key) & (1 | short.MinValue)) != 0;
    }
}