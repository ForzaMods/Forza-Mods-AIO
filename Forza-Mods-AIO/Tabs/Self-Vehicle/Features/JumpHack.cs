using System.Threading.Tasks;

using static System.Convert;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Resources.KeyStates;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.Entities.CarEntity;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs.HandlingPage;
using static Forza_Mods_AIO.Tabs.Keybindings.DropDownTabs.HandlingKeybindings;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.Features;

public abstract class JumpHack : FeatureBase
{
    private static float _boost = 10f;
    public static void SetBoost(double? newValue) => _boost = ToSingle(newValue);
    
    public static void Run()
    {
        while (true)
        {
            if (!Shp.Dispatcher.Invoke(() => Shp.JumpHackSwitch.IsOn) || !IsProcessValid())
            {
                break;
            }
                
            if (!IsKeyPressed(Hk.JmpHack) && !Mw.Gamepad.IsButtonPressed(JmpHackController))
            {
                Task.Delay(25).Wait();
                continue;
            }
            
            LinearVelocity = LinearVelocity with { Y = LinearVelocity.Y + _boost };

            while (IsKeyPressed(Hk.JmpHack) || Mw.Gamepad.IsButtonPressed(JmpHackController))
            {
                Task.Delay(50).Wait();
            }
        }
    }
}