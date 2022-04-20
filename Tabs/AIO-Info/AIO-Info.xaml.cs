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

namespace WPF_Mockup.Tabs.AIO_Info
{
    /// <summary>
    /// Interaction logic for AIO_Info.xaml
    /// </summary>
    public partial class AIO_Info : Page
    {
        public static AIO_Info ai;
        public AIO_Info()
        {
            InitializeComponent();
            ai = this;
        }
        private void WallButton_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => { CustomTheming.Monet.ApplyMonet(); });
        }
    }
}
