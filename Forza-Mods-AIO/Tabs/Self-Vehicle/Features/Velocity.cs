using System.Threading.Tasks;
using System.Windows.Controls;
using Forza_Mods_AIO.Resources;

using static System.Convert;
using static Forza_Mods_AIO.Resources.KeyStates;
using static Forza_Mods_AIO.Tabs.Keybindings.DropDownTabs.HandlingKeybindings;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.Entities.CarEntity;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs.HandlingPage;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.Features;

public abstract class Velocity : FeatureBase
{
    public static void Run()
    {
        if (!IsProcessValid())
        {
            return;
        }

        while (true)
        {
            if (!Shp.Dispatcher.Invoke(() => Shp.VelocitySwitch.IsOn))
            {
                return;
            }
        
            if (MainWindow.Mw.Gvp.Process.MainWindowHandle != DllImports.GetForegroundWindow())
            {
                Task.Delay(25).Wait();
                continue;
            }
        
            if (!IsKeyPressed(Hk.VelHack) && !MainWindow.Mw.Gamepad.IsButtonPressed(VelHackController))
            {
                Task.Delay(25).Wait();
                continue;
            }
        
            var limit = Shp.Dispatcher.Invoke(() => Shp.VelLimitBox.Value);
            if (CarSpeed > limit)
            {
                Task.Delay(25).Wait();
                continue;   
            }
                        
            var multiply = 1f;
            var mode = Shp.Dispatcher.Invoke(() => ((ComboBoxItem)Shp.VelModeBox.SelectedItem).Content.ToString());
        
            switch (mode)
            {
                case "Dynamic":
                {
                    multiply += Shp.Dispatcher.Invoke(() =>
                        ToSingle(Shp.VelValueNum.Value) / ToSingle(Shp.VelLimitBox.Value / 3));
                    break;
                }
                case "Direct":
                {
                    multiply += Shp.Dispatcher.Invoke(() => ToSingle(Shp.VelValueNum.Value) / 10);
                    break;
                }
            }  
                        
            LinearVelocity = LinearVelocity with
            {
                X = LinearVelocity.X * multiply,
                Y = LinearVelocity.Y - 0.05f,
                Z = LinearVelocity.Z * multiply
            };
        
            const int interval = 100;
            Task.Delay(interval).Wait();
        }
    }
}