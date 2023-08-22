using MahApps.Metro.Controls;
using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Linq;
using System.Threading;

namespace Forza_Mods_AIO.Resources
{
    internal class UpdateUi
    {
        static Dispatcher dispatcher = Application.Current.Dispatcher;

        public static void UpdateUI(bool IsEnabled, Page page)
        {
            dispatcher.BeginInvoke(delegate ()
            {
                foreach (FrameworkElement element in page.GetChildren(true))
                    if (element.GetType() == typeof(ToggleSwitch) || (element.GetType() == typeof(Button) && element.Name != "ScanButton"))
                        element.IsEnabled = IsEnabled;
            });
        }

        public static readonly Dictionary<string, double> Sizes = new Dictionary<string, double>()
        {
            // Tuning
            { "TiresButton" , 160}, // Button name for page, height of page
            { "GearingButton" , 250},
            { "AlignmentButton" , 200},
            { "SpringsButton" , 525},
            { "DampingButton" , 585},
            { "AeroButton", 160 },
            { "SteeringButton", 395 },
            { "OthersButton", 430 },

            // Self-Vehicle
            { "HandlingButton" , 464},
            { "UnlocksButton" , 200},
            { "CameraButton" , 225},
            { "TeleportsButton" , 100}
        };

        public static Dictionary<string, bool> IsClicked = new Dictionary<string, bool>()
        {
            // Tuning
            {"TiresButton", false },
            {"GearingButton", false },
            {"AlignmentButton", false },
            {"SpringsButton", false },
            {"DampingButton", false },
            {"AeroButton", false },
            {"SteeringButton", false },
            {"OthersButton", false },

            // Self-Vehicle
            {"HandlingButton", false },
            {"UnlocksButton", false },
            {"CameraButton", false },
            {"ModifiersButton", false },
            {"StatsButton", false },
            {"TeleportsButton", false },
            {"EnvironmentButton", false },
            {"LiveTuningButton", false }
        };

        public static bool AnimCompleted = true;

        public static void Animate(object sender, bool AlreadyOpen, Page page)
        {
            AnimCompleted = false;
            foreach (FrameworkElement Element in page.GetChildren(true))
            {
                //Thread.Sleep(1);
                string SenderName = sender.GetType().GetProperty("Name").GetValue(sender).ToString();
                string ElementName = Element.GetType().GetProperty("Name").GetValue(Element).ToString();
                Type Type = Element.GetType();

                if (ElementName == "PART_ClearText" || ElementName == "ScanButton" || (Type != typeof(Page) && Type != typeof(Button) && Type != typeof(Frame)))
                    continue;

                DoubleAnimation DanimationPage;
                ThicknessAnimation TanimationPage;
                ThicknessAnimation TanimationButton;
                Storyboard storyboard = new Storyboard();

                double Duration = 0.1;

                if (ElementName.Contains("Page") && ElementName.Contains(SenderName.Replace("Button", String.Empty)))
                {
                    storyboard.Completed += (s, e) =>
                    {
                        AnimCompleted = true;
                        if (AlreadyOpen)
                            Element.Visibility = Visibility.Hidden;
                    };
                    Element.Visibility = Visibility.Visible;

                    //Page move height of button
                    Thickness Start = (Thickness)Element.GetType().GetProperty("Margin").GetValue(Element);
                    Thickness End = new Thickness(Start.Left, Start.Top + 25, Start.Right, Start.Bottom);
                    if (AlreadyOpen)
                        End = new Thickness(Start.Left, Start.Top - 25, Start.Right, Start.Bottom);
                    TanimationPage = new ThicknessAnimation(End, new Duration(TimeSpan.FromSeconds(Duration)));

                    //Page change height
                    DanimationPage = new DoubleAnimation(Sizes[SenderName], new Duration(TimeSpan.FromSeconds(Duration)));
                    if (AlreadyOpen)
                        DanimationPage = new DoubleAnimation(25, new Duration(TimeSpan.FromSeconds(Duration)));

                    Storyboard.SetTargetName(TanimationPage, ElementName);
                    Storyboard.SetTargetProperty(TanimationPage, new PropertyPath(Frame.MarginProperty));
                    storyboard.Children.Add(TanimationPage);

                    Storyboard.SetTargetName(DanimationPage, ElementName);
                    Storyboard.SetTargetProperty(DanimationPage, new PropertyPath(Frame.HeightProperty));
                    storyboard.Children.Add(DanimationPage);
                    storyboard.Begin(Element);
                }
                else if ((Type == typeof(Button)
                    && (object)Element != sender                                                                                                 // Button is not the scan button
                    && IsClicked.Keys.ToList().IndexOf(ElementName) > IsClicked.Keys.ToList().IndexOf(SenderName))                              // Button is below the button that was clicked
                    || (Type == typeof(Frame)
                    && !ElementName.Contains(SenderName.Replace("Button", String.Empty))                                                        // Page isnt the one being shown
                    && IsClicked.Keys.ToList().IndexOf(ElementName.Replace("Page", "Button")) > IsClicked.Keys.ToList().IndexOf(SenderName)))   // Page is below the button that was clicked
                {
                    //Move all buttons down by size of page opened
                    Thickness Start = (Thickness)Type.GetProperty("Margin").GetValue(Element);
                    Thickness End = new Thickness(Start.Left, Start.Top + Sizes[SenderName], Start.Right, Start.Bottom);
                    if (AlreadyOpen)
                        End = new Thickness(Start.Left, Start.Top - Sizes[SenderName], Start.Right, Start.Bottom);
                    TanimationButton = new ThicknessAnimation(End, new Duration(TimeSpan.FromSeconds(Duration)));

                    Storyboard.SetTargetName(TanimationButton, ElementName);
                    Storyboard.SetTargetProperty(TanimationButton, new PropertyPath(Button.MarginProperty));
                    storyboard.Children.Add(TanimationButton);
                    storyboard.Begin(Element);
                }
            }
        }

        public static void AddProgress(int ScanAmount, int index, MetroProgressBar progressBar)
        {
            int Prog = (int)(Math.Round((decimal)100 / ScanAmount) * index);
            if (Prog > 100)
                Prog = 100;
            int CurrentProg = 0;
            Application.Current.Dispatcher.Invoke(delegate () { CurrentProg = (int)progressBar.Value; });
            for (int i = CurrentProg; i <= Prog; i++)
            {
                Application.Current.Dispatcher.Invoke(delegate () { progressBar.Value = i; });
                Thread.Sleep(15);
            }
        }
    }
}
