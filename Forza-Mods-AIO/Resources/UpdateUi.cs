using MahApps.Metro.Controls;
using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Forza_Mods_AIO.Resources
{
    internal abstract class UpdateUi
    {
        public static void UpdateUI(bool IsEnabled, Page page)
        {
            Application.Current.Dispatcher.BeginInvoke(delegate ()
            {
                foreach (FrameworkElement element in page.GetChildren(true))
                    if (element.GetType() == typeof(ToggleSwitch) || element.GetType() == typeof(ComboBox) || (element.GetType() == typeof(Button) && element.Name != "ScanButton"))
                        element.IsEnabled = IsEnabled;
            });
        }
        public static bool AnimCompleted = true;

        public static void Animate(object sender, bool AlreadyOpen, Dictionary<string, double> Sizes, Dictionary<string, bool> IsClicked, Page page)
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
            Task.Run(() =>
            {
                int Prog = (int)(Math.Round((decimal)100 / ScanAmount) * index);
                if (Prog > 100)
                    Prog = 100;
                int CurrentProg = 0;
                Application.Current.Dispatcher.Invoke(() => { CurrentProg = (int)progressBar.Value; });
                for (int i = CurrentProg; i <= Prog; i++)
                {
                    Application.Current.Dispatcher.Invoke(() => { progressBar.Value = i; });
                    Thread.Sleep(15);
                }
            });
        }
    }
}
