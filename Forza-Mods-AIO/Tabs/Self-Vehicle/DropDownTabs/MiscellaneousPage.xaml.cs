using System.Threading.Tasks;
using System.Windows;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

public partial class MiscellaneousPage
{
    public MiscellaneousPage()
    {
        InitializeComponent();
    }

    private void RemoveBuildCapSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!RemoveBuildCapSwitch.IsOn)
        {
            MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.BuildCapAddrASM1, MainWindow.mw.gvp.Name == "Forza Horizon 5" ? new byte[] { 0xF3,0x0F,0x11,0x83,0x4C,0x04,0x00,0x00 } : new byte[] { 0xF3,0x0F,0x11,0xB3,0xDC,0x03,0x00,0x00 });
            MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.BuildCapAddrASM2, MainWindow.mw.gvp.Name == "Forza Horizon 5" ? new byte[] { 0xF3, 0x0F, 0x11, 0x43, 0x44 } : new byte[] { 0xF3, 0x0F, 0x11, 0x43, 0x30 });
            return;
        }
        
        ASM.RemoveBuildCap();
    }

    private void UnbreakableSkillScoreSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (Self_Vehicle_Addrs.UnbSkillAddrLong == 0)
        {
            Task.Run(() => ASM.GetUnbreakableSkillComboAddr());
            return;
        }
        
        if (!UnbreakableSkillScoreSwitch.IsOn)
        {
            MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.UnbSkillAddr, (float)12);
            MainWindow.mw.m.WriteMemory((Self_Vehicle_Addrs.UnbSkillAddrLong + 4).ToString("X"), (float)12);
            MainWindow.mw.m.WriteMemory((Self_Vehicle_Addrs.UnbSkillAddrLong + 8).ToString("X"), (float)22);
            return;
        }
        
        MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.UnbSkillAddr, (float)9999999999);
        MainWindow.mw.m.WriteMemory((Self_Vehicle_Addrs.UnbSkillAddrLong + 4).ToString("X"), (float)9999999999);
        MainWindow.mw.m.WriteMemory((Self_Vehicle_Addrs.UnbSkillAddrLong + 8).ToString("X"), (float)9999999999);
    }
}