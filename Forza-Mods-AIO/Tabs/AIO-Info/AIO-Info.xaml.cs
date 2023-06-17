using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Forza_Mods_AIO.Tabs.AIO_Info
{
    /// <summary>
    /// Interaction logic for AIO_Info.xaml
    /// </summary>
    public partial class AIO_Info : Page
    {
        public static AIO_Info ai;
        //static Overlay.Overlay o = new Overlay.Overlay();
        public AIO_Info()
        {
            InitializeComponent();
            ai = this;
        }

        private void WallButton_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => { CustomTheming.Monet.ApplyMonet(); });
        }

        private void OverlaySwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (Overlay.Overlay.o == null)
                _ = new Overlay.Overlay();
            if (OverlaySwitch.IsOn)
                Overlay.Overlay.o.OverlayToggle(true);
            else
                Overlay.Overlay.o.OverlayToggle(false);
        }
    }
}
