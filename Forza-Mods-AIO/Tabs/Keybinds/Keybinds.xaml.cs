using Forza_Mods_AIO.Overlay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Keys = System.Windows.Forms.Keys;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ControlzEx.Standard;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Windows.Media.Animation;

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
        private bool Grabbing = false;

        public Keybinds()
        {
            InitializeComponent();
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
                                KeyGrabber(UpButton);

                            Reading = false;
                        }

                        break;
                    case "DownButton":
                        while (Reading)
                        {
                            Thread.Sleep(50);
                            if (!Grabbing)
                                KeyGrabber(DownButton);

                            Reading = false;
                        }

                        break;
                    case "LeftButton":
                        while (Reading)
                        {
                            Thread.Sleep(50);
                            if (!Grabbing)
                                KeyGrabber(LeftButton);

                            Reading = false;
                        }

                        break;
                    case "RightButton":
                        while (Reading)
                        {
                            Thread.Sleep(50);
                            if (!Grabbing)
                                KeyGrabber(RightButton);

                            Reading = false;
                        }

                        break;
                    case "ConfirmButton":
                        while (Reading)
                        {
                            Thread.Sleep(50);
                            if (!Grabbing)
                                KeyGrabber(ConfirmButton);

                            Reading = false;
                        }

                        break;
                    case "LeaveButton":
                        while (Reading)
                        {
                            Thread.Sleep(50);
                            if (!Grabbing)
                                KeyGrabber(LeaveButton);

                            Reading = false;
                        }

                        break;
                    case "VisibilityButton":
                        while (Reading)
                        {
                            Thread.Sleep(50);
                            if (!Grabbing)
                                KeyGrabber(VisibilityButton);

                            Reading = false;
                        }

                        break;
                }
            });

            Clicked = false;
        }

        private void KeyGrabber(Button sender)
        {
            Grabbing = true;

            string keyBuffer = string.Empty;
            while (keyBuffer.Length == 0)
            {
                foreach (Int32 i in Enum.GetValues(typeof(Keys)))
                {
                    int x = GetAsyncKeyState(i);
                    if ((x == 1) || (x == Int16.MinValue))
                    {
                        if (i != 0 && i != 1 && i != 2 && i != 3 && i != 4 && i != 12)
                        {
                            keyBuffer += Enum.GetName(typeof(Keys), i);
                        }
                    }
                }
            }

            Dispatcher.BeginInvoke((Action)delegate ()
            {
                sender.Content = keyBuffer;

                if (sender.Name == "UpButton")
                    OverlayHandling.Up = (Keys)Enum.Parse(typeof(Keys), keyBuffer);

                if (sender.Name == "DownButton")
                    OverlayHandling.Down = (Keys)Enum.Parse(typeof(Keys), keyBuffer);

                if (sender.Name == "LeftButton")
                    OverlayHandling.Left = (Keys)Enum.Parse(typeof(Keys), keyBuffer);

                if (sender.Name == "RightButton")
                    OverlayHandling.Right = (Keys)Enum.Parse(typeof(Keys), keyBuffer);

                if (sender.Name == "ConfirmButton")
                    OverlayHandling.Confirm = (Keys)Enum.Parse(typeof(Keys), keyBuffer);

                if (sender.Name == "LeaveButton")
                    OverlayHandling.Leave = (Keys)Enum.Parse(typeof(Keys), keyBuffer);

                if (sender.Name == "VisibilityButton")
                    OverlayHandling.OverlayVisibility = (Keys)Enum.Parse(typeof(Keys), keyBuffer);
            });
        }

        private void KeyBinds_Loaded(object sender, RoutedEventArgs e)
        {
            UpButton.Content = OverlayHandling.Up;
            DownButton.Content = OverlayHandling.Down;
            LeftButton.Content = OverlayHandling.Left;
            RightButton.Content = OverlayHandling.Right;
            ConfirmButton.Content = OverlayHandling.Confirm;
            LeaveButton.Content = OverlayHandling.Leave;
            VisibilityButton.Content = OverlayHandling.OverlayVisibility;
        }
    }
}
