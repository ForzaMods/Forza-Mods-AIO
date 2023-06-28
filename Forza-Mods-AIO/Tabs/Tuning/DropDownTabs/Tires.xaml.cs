using System.Windows;
using System.Windows.Controls;

namespace Forza_Mods_AIO.Tabs.TuningTablePort.DropDownTabs
{
    public partial class Tires : Page
    {
        public static Tires t;
        public Tires()
        {
            InitializeComponent();
            t = this;
        }

        private void FrontLeftTirePressureBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.TireFrontLeft, "float", FrontLeftTirePressureBox.Value.ToString()); } catch { }
        }

        private void FrontRightTirePressureBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.TireFrontRight, "float", FrontRightTirePressureBox.Value.ToString()); } catch { }
        }

        private void RearLeftTirePressureBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.TireRearLeft, "float", RearLeftTirePressureBox.Value.ToString()); } catch { }
        }

        private void RearRightTirePressureBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Addresses.TireRearRight, "float", RearRightTirePressureBox.Value.ToString()); } catch { }
        }
    }
}
