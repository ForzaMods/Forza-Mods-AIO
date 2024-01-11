using System.Windows.Forms;
using Forza_Mods_AIO.Resources;

using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.SelfVehicleAddresses;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.Features;

public abstract class WallNoClip : FeatureBase
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
            Bypass.AddProtectAddress(Wall1Addr);
        }

        switch (isOn)
        {
            case true when isFm8:
            {
                Mw.M.WriteArrayMemory(Wall1Addr, new byte[] { 0xE9, 0x5F, 0x02, 0x00, 0x00, 0x90 });
                Mw.M.WriteArrayMemory(Wall2Addr, new byte[] { 0xE9, 0x60, 0x02, 0x00, 0x00, 0x90 });
                break;
            }
            
            case true when isFh5:
            {
                Mw.M.WriteArrayMemory(Wall1Addr, new byte[] { 0xE9, 0x61, 0x02, 0x00, 0x00, 0x90 });
                Mw.M.WriteArrayMemory(Wall2Addr, new byte[] { 0xE9, 0x7F, 0x02, 0x00, 0x00, 0x90 });
                break;
            }

            case true:
            {
                Mw.M.WriteArrayMemory(Wall1Addr, new byte[] { 0xE9, 0x2A, 0x02, 0x00, 0x00, 0x90 });
                Mw.M.WriteArrayMemory(Wall2Addr, new byte[] { 0xE9, 0x2B, 0x02, 0x00, 0x00, 0x90 });
                break;
            }

            case false when isFm8:
            {
                Mw.M.WriteArrayMemory(Wall1Addr, new byte[] { 0x0F, 0x84, 0x5E, 0x02, 0x00, 0x00 });
                Mw.M.WriteArrayMemory(Wall2Addr, new byte[] { 0x0F, 0x84, 0x5F, 0x02, 0x00, 0x00 });
                break;
            }
            
            case false when isFh5:
            {
                Mw.M.WriteArrayMemory(Wall1Addr, new byte[] { 0x0F, 0x84, 0x60, 0x02, 0x00, 0x00 });
                Mw.M.WriteArrayMemory(Wall2Addr, new byte[] { 0x0F, 0x84, 0x7E, 0x02, 0x00, 0x00 });
                break;
            }
            
            case false:
            {
                Mw.M.WriteArrayMemory(Wall1Addr, new byte[] { 0x0F, 0x84, 0x29, 0x02, 0x00, 0x00 });
                Mw.M.WriteArrayMemory(Wall2Addr, new byte[] { 0x0F, 0x84, 0x2A, 0x02, 0x00, 0x00 });
                break;
            }
        }        
    }
}