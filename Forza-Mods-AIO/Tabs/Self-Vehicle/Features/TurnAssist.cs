using System.Numerics;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using Forza_Mods_AIO.Tabs.Self_Vehicle.Entities;

using static System.Math;
using static System.Convert;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Resources.DllImports;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs.HandlingPage;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.Features;

public abstract class TurnAssist : FeatureBase
{
    public static void Run()
    {
        if (!IsProcessValid())
        {
            return;
        }
        
        while (true)
        {
            if (!Shp.Dispatcher.Invoke(() => Shp.TurnAssistSwitch.IsOn))
            {
                break;
            }

            if (Mw.Gvp.Process.MainWindowHandle != GetForegroundWindow())
            {
                Task.Delay(25).Wait();
                continue;
            }

            float frontLeft = CarEntity.WheelSpeed.X, frontRight = CarEntity.WheelSpeed.Y; // Front
            float backLeft = CarEntity.WheelSpeed.Z, backRight = CarEntity.WheelSpeed.W; // Rear
                    
            var interval = Shp.Dispatcher.Invoke(() => ToInt32(Shp.TurnAssistIntervalBox.Value));
            var ratio = Shp.Dispatcher.Invoke(() => ToSingle(Shp.TurnAssistRatioBox.Value)); 
            var strength = Shp.Dispatcher.Invoke(() => ToSingle(Shp.TurnAssistStrengthBox.Value));
                    
            if (GetAsyncKeyState(Keys.A) is 1 or short.MinValue)
            {
                if (Abs(frontRight - frontLeft) < frontRight / ratio && Abs(backRight - frontLeft) < backRight / ratio)
                {
                    frontLeft -= strength;
                    backLeft -= strength;
                    frontRight += strength;
                    backRight += strength;
                }
            }
            else if (GetAsyncKeyState(Keys.D) is 1 or short.MinValue)
            {
                if (Abs(frontLeft - frontRight) < frontLeft / ratio && Abs(backLeft - frontRight) < backLeft / ratio)
                {
                    frontRight -= strength;
                    backRight -= strength;
                    frontLeft += strength;
                    backLeft += strength;
                }
            }

            CarEntity.WheelSpeed = new Vector4(frontLeft, frontRight, backLeft, backRight);
            Task.Delay(interval).Wait();
        }
    }
}