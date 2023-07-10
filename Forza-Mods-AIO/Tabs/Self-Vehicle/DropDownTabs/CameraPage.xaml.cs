using System.Windows;
using System.Windows.Controls;


namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs
{
    public partial class CameraPage : Page
    {

        public CameraPage()
        {
            InitializeComponent();
        }

        private void NoClip_Toggled(object sender, RoutedEventArgs e)
        {

        }

        private void ZoomUnlocker_Toggled(object sender, RoutedEventArgs e)
        {
            
        }

        private void FocusBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {

        }

        private void ExposureBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {

        }

        private void ApertureBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {

        }

        private void ContrastBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {

        }

        private void ColourBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {

        }

        private void BrightnessBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {

        }

        private void SepiaBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {

        }

        private void VignetteBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {

        }

        private void TemperatureBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {

        }

        private void SpeedSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {   
            //MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.MovementSpeed, "float", SpeedSlider.Value.ToString());
        }

        private void SamplesMultiplierSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.SamplesMultiplier, "float", SamplesMultiplierSlider.Value.ToString());
        }

        private void TurnSpeed_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.TurnAndZoomSpeed, "float", SpeedSlider.Value.ToString());
        }
    }
}
