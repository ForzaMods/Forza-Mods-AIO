using System;
using System.Threading.Tasks;
using MahApps.Metro.Controls;

using static System.Convert;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.Entities.CarEntity;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs.HandlingPage;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.Features;

public abstract class Modifiers : FeatureBase
{
    public static void Run(ToggleSwitch setSwitch)
    {
        var isGravity = Shp.Dispatcher.Invoke(() => setSwitch.Name.Contains("Gravity"));
        var valueNum = isGravity ? Shp.GravityValueNum : Shp.AccelValueNum;
        var original = isGravity ? Gravity : Acceleration;
        var lastPlayerEnt = PlayerCarEntity;
        
        while (true)
        {
            if (!IsProcessValid())
            {
                return;
            }

            Task.Delay(100).Wait();
            if (!Shp.Dispatcher.Invoke(() => setSwitch.IsOn))
            {
                _ = isGravity ? Gravity = original : Acceleration = original;
                Shp.Dispatcher.Invoke(() => valueNum.Value = ToDouble(original));
                break;
            }
                
            if (lastPlayerEnt != BaseDetour.ReadVariable<UIntPtr>())
            {
                if (Acceleration is < 0.2f and > 0f)
                {
                    original = isGravity ? Gravity : Acceleration;
                    lastPlayerEnt = BaseDetour.ReadVariable<UIntPtr>();
                }
                continue;
            }
                
            float setValue = 0;
            Shp.Dispatcher.Invoke(() => setValue = ToSingle(valueNum.Value));
            _ = isGravity ? Gravity = setValue : Acceleration = setValue;
        }
    }
}