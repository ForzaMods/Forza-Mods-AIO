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

        private void SpeedSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {   
            MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.MovementSpeed, "float", SpeedSlider.Value.ToString());
        }

        private void SamplesMultiplierSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.SamplesMultiplier, "float", SamplesMultiplierSlider.Value.ToString());
        }

        private void TurnSpeed_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.TurnAndZoomSpeed, "float", TurnSpeed.Value.ToString());
        }

        private void SamplesBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {

        }

        private void ShutterSpeedBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {

        }

        private void ApertureScaleBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {

        }

        private void CarInFocusBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {

        }

        private void TimeSliceBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {

        }
    }
}
