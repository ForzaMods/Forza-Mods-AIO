using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static WPF_Mockup.Tabs.Self_Vehicle.Self_Vehicle_Addrs;
using static WPF_Mockup.MainWindow;
using System.Threading;

namespace WPF_Mockup.Tabs.Self_Vehicle.DropDownTabs
{
    /// <summary>
    /// Interaction logic for SpeedHacksPage.xaml
    /// </summary>
    public partial class SpeedHacksPage : Page
    {
        static SpeedHacksPage shp;
        public SpeedHacksPage()
        {
            InitializeComponent();
            shp = this;
        }

        private void VelocitySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            VelocityValueNum.Value = Math.Round(e.NewValue,5);
        }
        private void VelocityValueNum_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try { shp.VelocitySlider.Value = (float)e.NewValue; } catch( Exception a ) { }
        }

        private void VelocitySwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if(VelocitySwitch.IsOn)
            {
                Task.Run(() =>
                {
                    while(true)
                    {
                        // TODO wrap for controlls / implement controls
                        bool Toggled = true;
                        shp.Dispatcher.Invoke(delegate () { Toggled = (bool)shp.VelocitySwitch.GetType().GetProperty("IsOn").GetValue(shp.VelocitySwitch); });
                        if (!Toggled)
                            break;

                        float xVelocityVal = mw.m.ReadFloat(xVelocityAddr) * (float)VelocityValueNum.Value;
                        float zVelocityVal = mw.m.ReadFloat(zVelocityAddr) * (float)VelocityValueNum.Value;
                        mw.m.WriteMemory(xVelocityAddr, "float", xVelocityVal.ToString());
                        mw.m.WriteMemory(zVelocityAddr, "float", zVelocityVal.ToString());
                        Thread.Sleep(50);
                    }
                });
            }
        }
    }
}
