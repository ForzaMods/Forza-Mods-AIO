using System.Windows;
using System.Windows.Controls;

namespace Forza_Mods_AIO.Tabs.TuningTablePort.DropDownTabs
{
    public partial class Steering : Page
    {
        public static Steering st;
        public Steering()
        {
            InitializeComponent();
            st = this;
        }

        private void AngleBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.AngleMax, "float", AngleBox.Value.ToString()); } catch { }
        }

        private void Angle2Box_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.AngleMax2, "float", Angle2Box.Value.ToString()); } catch { }
        }

        private void VelocityTimeBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.AngleTimeToMaxSteering, "float", VelocityTimeBox.Value.ToString()); } catch { }
        }

        private void VelocityStraightBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.AngleVelocityStraight, "float", VelocityStraightBox.Value.ToString()); } catch { }
        }

        private void VelocityTurningBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.AngleVelocityTurning, "float", VelocityTurningBox.Value.ToString()); } catch { }
        }

        private void VelocityCountersteerBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.AngleVelocityCountersteer, "float", VelocityCountersteerBox.Value.ToString()); } catch { }
        }

        private void VelocityDynamicPeekBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.AngleVelocityDynamicPeek, "float", VelocityDynamicPeekBox.Value.ToString()); } catch { }
        }
    }
}
