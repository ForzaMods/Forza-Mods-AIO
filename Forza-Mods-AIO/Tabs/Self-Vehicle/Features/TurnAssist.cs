using System.Numerics;
using System.Windows.Forms;
using System.Threading.Tasks;
using Forza_Mods_AIO.Tabs.Self_Vehicle.Entities;

using static System.Math;
using static System.Convert;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Resources.DllImports;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs.HandlingPage;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.Features;

public abstract class TurnAssist : FeatureBase
{
    private static int _interval;
    private static float _ratio;
    private static float _strength;

    public static void SetInterval(double? newValue) => _interval = ToInt32(newValue);
    public static void SetRatio(double? newValue) => _ratio = ToSingle(newValue);
    public static void SetStrength(double? newValue) => _strength = ToSingle(newValue);
    
    public static void Run()
    {
        while (true)
        {
            if (!IsProcessValid() || !Shp.Dispatcher.Invoke(() => Shp.TurnAssistSwitch.IsOn))
            {
                return;
            }

            if (Mw.Gvp.Process.MainWindowHandle != GetForegroundWindow())
            {
                Task.Delay(_interval).Wait();
                continue;
            }

            float frontLeft = CarEntity.WheelSpeed.X, frontRight = CarEntity.WheelSpeed.Y; // Front
            float backLeft = CarEntity.WheelSpeed.Z, backRight = CarEntity.WheelSpeed.W; // Rear
                    
            if (GetAsyncKeyState(Keys.A) is 1 or short.MinValue)
            {
                if (Abs(frontRight - frontLeft) < frontRight / _ratio && Abs(backRight - frontLeft) < backRight / _ratio)
                {
                    frontLeft -= _strength;
                    backLeft -= _strength;
                    frontRight += _strength;
                    backRight += _strength;
                }
            }
            else if (GetAsyncKeyState(Keys.D) is 1 or short.MinValue)
            {
                if (Abs(frontLeft - frontRight) < frontLeft / _ratio && Abs(backLeft - frontRight) < backLeft / _ratio)
                {
                    frontRight -= _strength;
                    backRight -= _strength;
                    frontLeft += _strength;
                    backLeft += _strength;
                }
            }

            CarEntity.WheelSpeed = new Vector4(frontLeft, frontRight, backLeft, backRight);
            Task.Delay(_interval).Wait();
        }
    }
}