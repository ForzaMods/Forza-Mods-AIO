using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;

namespace Forza_Mods_AIO.Tabs.TuningTablePort.DropDownTabs
{
    public partial class Others : Page
    {
        public static Others o;
        public Others()
        {
            InitializeComponent();
            o = this;
        }

        private void WheelbaseBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.Wheelbase, "float", WheelbaseBox.Value.ToString()); } catch { }
        }

        private void FrontWidthBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.FrontWidth, "float", FrontWidthBox.Value.ToString()); } catch { }
        }

        private void RearWidthBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.RearWidth, "float", RearWidthBox.Value.ToString()); } catch { }
        }

        private void FrontSpacerBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.FrontSpacer, "float", FrontSpacerBox.Value.ToString()); } catch { }
        }

        private void RearSpacerBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.RearSpacer, "float", RearSpacerBox.Value.ToString()); } catch { }
        }

        private void RimSizeFrontBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.RimSizeFront, "float", RimSizeFrontBox.Value.ToString()); } catch { }
        }

        private void RimRadiusFrontBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.RimRadiusFront, "float", RimRadiusFrontBox.Value.ToString()); } catch { }
        }

        private void RimSizeRearBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.RimSizeRear, "float", RimSizeRearBox.Value.ToString()); } catch { }
        }

        private void RimRadiusRearBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { MainWindow.mw.m.WriteMemory(Tuning_Addresses.RimRadiusRear, "float", RimRadiusRearBox.Value.ToString()); } catch { }
        }

        private void FreezeToggled(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleSwitch).IsOn)
            {

            }
            else
            {

            }
        }
    }
}
