using System.Windows.Forms;
using Forza_Mods_AIO.Resources;

using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.SelfVehicleAddresses;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.Features;

public abstract class CarNoClip : FeatureBase
{
    public static void Toggle(bool isOn)
    {
        if (!IsProcessValid())
        {
            return;
        }
        
        var isFm8 = Mw.Gvp.Type == GameVerPlat.GameType.Fm8;
        var isFh5 = Mw.Gvp.Type == GameVerPlat.GameType.Fh5;
        var bypassAntiCheat = isFm8 || isFh5;
        
        if (bypassAntiCheat && !Bypass.DisableAntiCheat())
        {
            MessageBox.Show(@"Failed");
            return;
        }

        if (isFm8)
        {
            Bypass.AddProtectAddress(Car1Addr);
        }

        switch (isOn)
        {
            case true when isFm8:
            {
                Mw.M.WriteArrayMemory(Car1Addr, new byte[] { 0xE9, 0x6F, 0x03, 0x00, 0x00, 0x90 });
                break;
            }
            
            case true when isFh5:
            {
                Mw.M.WriteArrayMemory(Car1Addr, new byte[] { 0xE9, 0x66, 0x03, 0x00, 0x00, 0x90 });
                break;
            }

            case true:
            {
                Mw.M.WriteArrayMemory(Car1Addr, new byte[] { 0xE9, 0xB6, 0x01, 0x00, 0x00, 0x90 });
                Mw.M.WriteArrayMemory(Car2Addr, new byte[] { 0xE9, 0x3B, 0x03, 0x00, 0x00, 0x90 });
                break;
            }

            case false when isFm8:
            {
                Mw.M.WriteArrayMemory(Car1Addr, new byte[] { 0x0F, 0x84, 0x6E, 0x03, 0x00, 0x00 });
                break;
            }
            
            case false when isFh5:
            {
                Mw.M.WriteArrayMemory(Car1Addr, new byte[] { 0x0F, 0x84, 0x65, 0x03, 0x00, 0x00 });
                break;
            }
            
            case false:
            {
                Mw.M.WriteArrayMemory(Car1Addr, new byte[] { 0x0F, 0x84, 0xB5, 0x01, 0x00, 0x00 });
                Mw.M.WriteArrayMemory(Car2Addr, new byte[] { 0x0F, 0x84, 0x3A, 0x03, 0x00, 0x00 });
                break;
            }
        }
    }
}