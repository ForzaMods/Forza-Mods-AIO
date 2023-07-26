using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Keys = System.Windows.Forms.Keys;
using System.Windows.Media;

namespace Forza_Mods_AIO.Tabs.Settings
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        private bool Clicked = false;
        public static bool Grabbing = false;
        public static Settings S;
        
        public Settings()
        {
            InitializeComponent();
            S = this;
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            KeybindsHandling.UpdateKeybindingOnLaunch();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Clicked)
                return;

            Clicked = true;
            Grabbing = false;
            Button SenderButton = (Button)sender;
            SenderButton.Content = "Press some key";
            bool Reading = true;
            
            await Task.Run(() =>
            {
                while (Reading)
                {
                    Thread.Sleep(50);
                    if (!Grabbing)
                        KeybindsHandling.KeyGrabber(SenderButton);

                    Reading = false;
                }
            });

            Clicked = false;
        }
    }
}
