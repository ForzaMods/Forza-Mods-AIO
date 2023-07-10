using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Keys = System.Windows.Forms.Keys;

namespace Forza_Mods_AIO.Tabs.Keybinds
{
    /// <summary>
    /// Interaction logic for Keybinds.xaml
    /// </summary>
    public partial class Keybinds : Page
    {
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(Keys vKey);

        [DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(Int32 vKey);

        private bool Clicked = false;
        public static bool Grabbing = false;
        public static bool IsLoaded = false;
        public static Keybinds K;
        
        public Keybinds()
        {
            InitializeComponent();
            K = this;
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            KeybindsHandling.UpdateKeybindingOnLaunch();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Clicked)
                return;

            Clicked = true;
            Grabbing = false;
            Button SenderButton = (Button)sender;
            var SenderButtonName = SenderButton.Name;
            SenderButton.Content = "Press a key";
            bool Reading = true;
            
            Task.Run(() =>
            {
                switch (SenderButtonName)
                {
                    case "UpButton":
                        while (Reading)
                        {
                            Thread.Sleep(50);
                            if (!Grabbing)
                                KeybindsHandling.KeyGrabber(UpButton);

                            Reading = false;
                        }

                        break;
                    case "DownButton":
                        while (Reading)
                        {
                            Thread.Sleep(50);
                            if (!Grabbing)
                                KeybindsHandling.KeyGrabber(DownButton);

                            Reading = false;
                        }

                        break;
                    case "LeftButton":
                        while (Reading)
                        {
                            Thread.Sleep(50);
                            if (!Grabbing)
                                KeybindsHandling.KeyGrabber(LeftButton);

                            Reading = false;
                        }

                        break;
                    case "RightButton":
                        while (Reading)
                        {
                            Thread.Sleep(50);
                            if (!Grabbing)
                                KeybindsHandling.KeyGrabber(RightButton);

                            Reading = false;
                        }

                        break;
                    case "ConfirmButton":
                        while (Reading)
                        {
                            Thread.Sleep(50);
                            if (!Grabbing)
                                KeybindsHandling.KeyGrabber(ConfirmButton);

                            Reading = false;
                        }

                        break;
                    case "LeaveButton":
                        while (Reading)
                        {
                            Thread.Sleep(50);
                            if (!Grabbing)
                                KeybindsHandling.KeyGrabber(LeaveButton);

                            Reading = false;
                        }

                        break;
                    case "VisibilityButton":
                        while (Reading)
                        {
                            Thread.Sleep(50);
                            if (!Grabbing)
                                KeybindsHandling.KeyGrabber(VisibilityButton);

                            Reading = false;
                        }

                        break;
                }
            });

            Clicked = false;
        }
    }
}
