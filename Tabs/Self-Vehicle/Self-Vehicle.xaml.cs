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
using System.Windows.Media.Animation;
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
        readonly Dictionary<string, double> Sizes = new Dictionary<string, double>()
        {
            { "SpeedHacksButton" , 200}
        };
        Dictionary<string, bool> IsClicked = new Dictionary<string,bool>()
        {
            {"SpeedHacksButton", false },
            {"CameraButton", false },
            {"ModifiersButton", false },
            {"StatsButton", false },
            {"TeleportsButton", false },
            {"EnvironmentButton", false },
            {"LiveTuningButton", false }
        };
        
        public Self_Vehicle()
        {
            InitializeComponent();
            sv = this;
        }

        private void ScanButton_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => sva.AoBscan());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Animate(sender, IsClicked[sender.GetType().GetProperty("Name").GetValue(sender).ToString()]);
            IsClicked[sender.GetType().GetProperty("Name").GetValue(sender).ToString()] = !IsClicked[sender.GetType().GetProperty("Name").GetValue(sender).ToString()];
        }

        private void Animate(object sender, bool AlreadyOpen)
        {
            foreach (FrameworkElement Element in this.GetChildren(true))
            {
                DoubleAnimation DanimationPage;
                ThicknessAnimation TanimationPage;
                ThicknessAnimation TanimationButton;
                Storyboard storyboard = new Storyboard();
                if (Element.GetType().GetProperty("Name").GetValue(Element).ToString().Contains("Page"))
                {
                    Thickness Start = (Thickness)Element.GetType().GetProperty("Margin").GetValue(Element);
                    Thickness End = new Thickness(Start.Left, Start.Top + 25, Start.Right, Start.Bottom);
                    if (AlreadyOpen)
                        End = new Thickness(Start.Left, Start.Top - 25, Start.Right, Start.Bottom);
                    TanimationPage = new ThicknessAnimation(End, new Duration(TimeSpan.FromSeconds(0.5)));
                    DanimationPage = new DoubleAnimation(Sizes[sender.GetType().GetProperty("Name").GetValue(sender).ToString()], new Duration(TimeSpan.FromSeconds(0.5)));
                    if (AlreadyOpen)
                        DanimationPage = new DoubleAnimation(25, new Duration(TimeSpan.FromSeconds(0.5)));
                    
                    Storyboard.SetTargetName(TanimationPage, Element.GetType().GetProperty("Name").GetValue(Element).ToString());
                    Storyboard.SetTargetProperty(TanimationPage, new PropertyPath(Frame.MarginProperty));
                    storyboard.Children.Add(TanimationPage);

                    Storyboard.SetTargetName(DanimationPage, Element.GetType().GetProperty("Name").GetValue(Element).ToString());
                    Storyboard.SetTargetProperty(DanimationPage, new PropertyPath(Frame.HeightProperty));
                    storyboard.Children.Add(DanimationPage);
                    storyboard.Begin(Element);
                }
                else if (Element.GetType() == typeof(Button) && (object)Element != sender && !Element.GetType().GetProperty("Name").GetValue(Element).ToString().Contains("ScanButton"))
                {
                    Thickness Start = (Thickness)Element.GetType().GetProperty("Margin").GetValue(Element);
                    Thickness End = new Thickness(Start.Left, Start.Top + Sizes[sender.GetType().GetProperty("Name").GetValue(sender).ToString()], Start.Right, Start.Bottom);
                    if (AlreadyOpen)
                        End = new Thickness(Start.Left, Start.Top - Sizes[sender.GetType().GetProperty("Name").GetValue(sender).ToString()], Start.Right, Start.Bottom);
                    TanimationButton = new ThicknessAnimation(End, new Duration(TimeSpan.FromSeconds(0.5)));

                    Storyboard.SetTargetName(TanimationButton, Element.GetType().GetProperty("Name").GetValue(Element).ToString());
                    Storyboard.SetTargetProperty(TanimationButton, new PropertyPath(Button.MarginProperty));
                    storyboard.Children.Add(TanimationButton);
                    storyboard.Begin(Element);
                }

            }
        }
    }
}
