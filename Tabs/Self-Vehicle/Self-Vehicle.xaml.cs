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

namespace WPF_Mockup.Tabs.Self_Vehicle
{
    /// <summary>
    /// Interaction logic for Self_Vehicle.xaml
    /// </summary>
    public partial class Self_Vehicle : Page
    {
        public static Self_Vehicle sv;
        Self_Vehicle_Addrs sva = new Self_Vehicle_Addrs();
        public Self_Vehicle()
        {
            InitializeComponent();
            sv = this;
        }

        private void ScanButton_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => sva.AoBscan());
        }
    }
}
