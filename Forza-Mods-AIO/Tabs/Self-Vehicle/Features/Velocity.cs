using System;
using System.Threading.Tasks;
using Forza_Mods_AIO.Resources;

using static System.Convert;
using static Forza_Mods_AIO.Resources.KeyStates;
using static Forza_Mods_AIO.Tabs.Keybindings.DropDownTabs.HandlingKeybindings;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.Entities.CarEntity;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs.HandlingPage;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.Features;

public abstract class Velocity : FeatureBase
{
    private enum Mode
    {
        Dynamic,
        Direct
    }
    
    private const int Interval = 50;

    private static float _limit = 300f;
    private static float _boost = 5f;
    private static Mode _mode;

    public static void SetLimit(double? newValue) => _limit = ToSingle(newValue);
    public static void SetBoost(double? newValue) => _boost = ToSingle(newValue);
    public static void SetMode(int newValue) => _mode = (Mode)newValue;
    
    public static void Run()
    {
        while (true)
        {
            if (!IsProcessValid())
            {
                return;
            }

            if (!Shp.Dispatcher.Invoke(() => Shp.VelocitySwitch.IsOn))
            {
                return;
            }
        
            if (MainWindow.Mw.Gvp.Process.MainWindowHandle != DllImports.GetForegroundWindow())
            {
                Task.Delay(Interval).Wait();
                continue;
            }
        
            if (!IsKeyPressed(Hk.VelHack) && !MainWindow.Mw.Gamepad.IsButtonPressed(VelHackController))
            {
                Task.Delay(Interval).Wait();
                continue;
            }
        
            if (CarSpeed > _limit)
            {
                Task.Delay(Interval).Wait();
                continue;   
            }
                        
            var multiply = _mode switch
            {
                Mode.Dynamic => 1f + _boost / (_limit / 3),
                Mode.Direct => 1f + _boost / 10,
                _ => throw new ArgumentOutOfRangeException()
            };

            LinearVelocity = LinearVelocity with
            {
                X = LinearVelocity.X * multiply,
                Z = LinearVelocity.Z * multiply
            };
        
            Task.Delay(Interval).Wait();
        }
    }
}