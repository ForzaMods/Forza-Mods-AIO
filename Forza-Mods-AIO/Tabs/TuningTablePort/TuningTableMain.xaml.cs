using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Forza_Mods_AIO.Tabs.TuningTablePort
{
    public partial class TuningTableMain : Page
    {
        readonly Dictionary<string, double> Sizes = new Dictionary<string, double>()
        {
            { "TiresButton" , 150}, // Button name for page, height of page
            { "GearingButton" , 240},
            { "AlignmentButton" , 150},
            { "SpringsButton" , 200},
            { "DampingButton" , 600},
            { "AeroButton", 100 },
            { "SteeringButton", 100 },
            { "OthersButton", 100 }
        };
        Dictionary<string, bool> IsClicked = new Dictionary<string, bool>()
        {
            {"TiresButton", false },
            {"GearingButton", false },
            {"AlignmentButton", false },
            {"SpringsButton", false },
            {"DampingButton", false },
            {"AeroButton", false },
            {"SteeringButton", false },
            {"OthersButton", false }
        };
        bool AnimCompleted = true;

        public TuningTableMain()
        {
            InitializeComponent();
        }

        #region Interaction
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (AnimCompleted)
            {
                Animate(sender, IsClicked[sender.GetType().GetProperty("Name").GetValue(sender).ToString()]);
                IsClicked[sender.GetType().GetProperty("Name").GetValue(sender).ToString()] = !IsClicked[sender.GetType().GetProperty("Name").GetValue(sender).ToString()];
            }
        }
        private void ScanButton_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(Addresses.TuningTable);
        }
        #endregion
        #region Functions
        private void Animate(object sender, bool AlreadyOpen)
        {
            AnimCompleted = false;
            foreach (FrameworkElement Element in this.GetChildren(true))
            {
                DoubleAnimation DanimationPage;
                ThicknessAnimation TanimationPage;
                ThicknessAnimation TanimationButton;
                Storyboard storyboard = new Storyboard();

                double Duration = 0.1;

                string SenderName = sender.GetType().GetProperty("Name").GetValue(sender).ToString();
                string ElementName = Element.GetType().GetProperty("Name").GetValue(Element).ToString();

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
                else if ((Element.GetType() == typeof(Button)
                    && (object)Element != sender                                                                                                // Button is not the button that was clicked
                    && !ElementName.Contains("ScanButton")                                                                                      // Button is not the scan button
                    && IsClicked.Keys.ToList().IndexOf(ElementName) > IsClicked.Keys.ToList().IndexOf(SenderName))                              // Button is below the button that was clicked
                    || (Element.GetType() == typeof(Frame)
                    && !ElementName.Contains(SenderName.Replace("Button", String.Empty))                                                        // Page isnt the one being shown
                    && IsClicked.Keys.ToList().IndexOf(ElementName.Replace("Page", "Button")) > IsClicked.Keys.ToList().IndexOf(SenderName)))   // Page is below the button that was clicked
                {
                    //Move all buttons down by size of page opened
                    Thickness Start = (Thickness)Element.GetType().GetProperty("Margin").GetValue(Element);
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
        #endregion
    }
}
