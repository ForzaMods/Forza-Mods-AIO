using ControlzEx.Standard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs
{
    /// <summary>
    /// Interaction logic for CustomisationPage.xaml
    /// </summary>
    public partial class CustomisationPage : Page
    {
        public static CustomisationPage CSP;

        public CustomisationPage()
        {
            InitializeComponent();
            CSP = this;
        }

        private void GlowingPaintSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            assembly.GlowingPaint(Self_Vehicle_Addrs.CodeCave9);
        }

        private void GlowingPaintNum_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try 
            {
                if (CSP != null)
                {
                    CSP.GlowingPaintSlider.Value = (float)e.NewValue;
                    assembly.GlowingPaint(Self_Vehicle_Addrs.CodeCave9);
                }
            } catch { }
        }

        private void GlowingPaintSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            GlowingPaintNum.Value = Math.Round(e.NewValue, 4); 
            try
            {
                assembly.GlowingPaint(Self_Vehicle_Addrs.CodeCave9);
            }
            catch { }
        }
    }
}
