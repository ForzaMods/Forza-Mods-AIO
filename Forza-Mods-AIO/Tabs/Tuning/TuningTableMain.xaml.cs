using Forza_Mods_AIO.Tabs.TuningTablePort.DropDownTabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Forza_Mods_AIO.Tabs.TuningTablePort
{
    public partial class TuningTableMain : Page
    {
        public static TuningTableMain TBM;

        readonly Dictionary<string, double> Sizes = new Dictionary<string, double>()
        {
            { "TiresButton" , 150}, // Button name for page, height of page
            { "GearingButton" , 250},
            { "AlignmentButton" , 150},
            { "SpringsButton" , 525},
            { "DampingButton" , 585},
            { "AeroButton", 150 },
            { "SteeringButton", 395 },
            { "OthersButton", 430 }
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
            TBM = this;
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
        private async void ScanButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.mw.gvp.Name == "Forza Horizon 5")
                await Task.Run(() => Addresses.TuningTable(5));
            
            else 
                await Task.Run(() => Addresses.TuningTable(4));
            
            Task.Run(ReadValues);
        }
        #endregion
        #region Functions
        private void Animate(object sender, bool AlreadyOpen)
        {
            AnimCompleted = false;
            foreach (FrameworkElement Element in this.GetChildren(true))
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

        public void ReadValues()
        {
            //These can be only read once as they dont change when a car is switched
            Dispatcher.BeginInvoke((Action)delegate ()
            {
                Alignment.al.CamberNegBox.Value = MainWindow.mw.m.ReadFloat(Addresses.CamberNegStatic);
                Alignment.al.CamberPosBox.Value = MainWindow.mw.m.ReadFloat(Addresses.CamberPosStatic);
                Alignment.al.ToeNegBox.Value = MainWindow.mw.m.ReadFloat(Addresses.ToeNegStatic);
                Alignment.al.ToePosBox.Value = MainWindow.mw.m.ReadFloat(Addresses.ToePosStatic);
            });

            //Rest requires a constant reading
            while (true)
            {
                Thread.Sleep(500);
                Dispatcher.BeginInvoke((Action)delegate ()
                {
                    #region Aero
                    Aero.ae.FrontAeroMinBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FrontAeroMin);
                    Aero.ae.FrontAeroMaxBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FrontAeroMax);
                    Aero.ae.RearAeroMinBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RearAeroMin);
                    Aero.ae.RearAeroMaxBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RearAeroMax);
                    #endregion
                    #region Gearing
                    Gearing.g.FinalDriveRatioBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FinalDrive);
                    Gearing.g.ReverseGearBox.Value = MainWindow.mw.m.ReadFloat(Addresses.ReverseGear);
                    Gearing.g.FirstGearBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FirstGear);
                    Gearing.g.SecondGearBox.Value = MainWindow.mw.m.ReadFloat(Addresses.SecondGear);
                    Gearing.g.ThirdGearBox.Value = MainWindow.mw.m.ReadFloat(Addresses.ThirdGear);
                    Gearing.g.FourthGearBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FourthGear);

                    if (MainWindow.mw.m.ReadFloat(Addresses.FifthGear) != 0)
                    {
                        Gearing.g.FifthGearBox.IsEnabled = true;
                        Gearing.g.FifthGearBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FifthGear);
                    }
                    else
                        Gearing.g.FifthGearBox.IsEnabled = false;

                    if (MainWindow.mw.m.ReadFloat(Addresses.SixthGear) != 0)
                    {
                        Gearing.g.SixthGearBox.IsEnabled = true;
                        Gearing.g.SixthGearBox.Value = MainWindow.mw.m.ReadFloat(Addresses.SixthGear);
                    }
                    else
                        Gearing.g.SixthGearBox.IsEnabled = false;

                    if (MainWindow.mw.m.ReadFloat(Addresses.SeventhGear) != 0)
                    {
                        Gearing.g.SeventhGearBox.IsEnabled = true;
                        Gearing.g.SeventhGearBox.Value = MainWindow.mw.m.ReadFloat(Addresses.SeventhGear);
                    }
                    else
                        Gearing.g.SeventhGearBox.IsEnabled = false;

                    if (MainWindow.mw.m.ReadFloat(Addresses.EighthGear) != 0)
                    {
                        Gearing.g.EighthBox.IsEnabled = true;
                        Gearing.g.EighthBox.Value = MainWindow.mw.m.ReadFloat(Addresses.EighthGear);
                    }
                    else
                        Gearing.g.EighthBox.IsEnabled = false;

                    if (MainWindow.mw.m.ReadFloat(Addresses.NinthGear) != 0)
                    {
                        Gearing.g.NinthGearBox.IsEnabled = true;
                        Gearing.g.NinthGearBox.Value = MainWindow.mw.m.ReadFloat(Addresses.NinthGear);
                    }
                    else
                        Gearing.g.NinthGearBox.IsEnabled = false;

                    if (MainWindow.mw.m.ReadFloat(Addresses.TenthGear) != 0)
                    {
                        Gearing.g.TenthGearBox.IsEnabled = true;
                        Gearing.g.TenthGearBox.Value = MainWindow.mw.m.ReadFloat(Addresses.TenthGear);
                    }
                    else
                        Gearing.g.TenthGearBox.IsEnabled = false;
                    #endregion
                    #region Damping
                    Damping.d.FrontAntirollBarsMinBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FrontAntirollMin);
                    Damping.d.FrontAntirollBarsMaxBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FrontAntirollMax);
                    Damping.d.RearAntirollBarsMinBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RearAntirollMin);
                    Damping.d.RearAntirollBarsMaxBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RearAntirollMax);

                    Damping.d.FrontBumpStiffnessMinBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FrontBumpStiffnessMin);
                    Damping.d.FrontBumpStiffnessMaxBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FrontBumpStiffnessMax);
                    Damping.d.RearBumpStiffnessMinBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RearBumpStiffnessMin);
                    Damping.d.RearBumpStiffnessMaxBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RearBumpStiffnessMax);

                    Damping.d.FrontReboundStiffnessMinBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FrontReboundStiffnesMin);
                    Damping.d.FrontReboundStiffnessMaxBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FrontReboundStiffnessMax);
                    Damping.d.RearReboundStiffnessMinBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RearReboundStiffnessMin);
                    Damping.d.RearReboundStiffnessMaxBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RearReboundStiffnessMax);
                    #endregion
                    #region Others
                    Others.o.WheelbaseBox.Value = MainWindow.mw.m.ReadFloat(Addresses.Wheelbase);
                    Others.o.RimSizeFrontBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RimSizeFront);
                    Others.o.RimSizeRearBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RimSizeRear);
                    Others.o.RimRadiusFrontBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RimRadiusFront);
                    Others.o.RimRadiusRearBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RimRadiusRear);
                    Others.o.FrontWidthBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FrontWidth);
                    Others.o.RearWidthBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RearWidth);
                    Others.o.FrontSpacerBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FrontSpacer);
                    Others.o.RearSpacerBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RearSpacer);
                    #endregion
                    #region Springs
                    Springs.sp.FrontSpringsMinBox.Value = MainWindow.mw.m.ReadFloat(Addresses.SpringFrontMin);
                    Springs.sp.FrontSpringsMaxBox.Value = MainWindow.mw.m.ReadFloat(Addresses.SpringFrontMax);
                    Springs.sp.RearSpringsMinBox.Value = MainWindow.mw.m.ReadFloat(Addresses.SpringRearMin);
                    Springs.sp.RearSpringsMaxBox.Value = MainWindow.mw.m.ReadFloat(Addresses.SpringRearMax);

                    Springs.sp.FrontRideHeightMinBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FrontRideHeightMin);
                    Springs.sp.FrontRideHeightMaxBox.Value = MainWindow.mw.m.ReadFloat(Addresses.FrontRideHeightMax);
                    Springs.sp.RearRideHeightMinBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RearRideHeightMin);
                    Springs.sp.RearRideHeightMaxBox.Value = MainWindow.mw.m.ReadFloat(Addresses.RearRideHeightMax);
                    #endregion
                    #region Steering
                    Steering.st.AngleBox.Value = MainWindow.mw.m.ReadFloat(Addresses.AngleMax);
                    Steering.st.Angle2Box.Value = MainWindow.mw.m.ReadFloat(Addresses.AngleMax2);
                    Steering.st.VelocityCountersteerBox.Value = MainWindow.mw.m.ReadFloat(Addresses.AngleVelocityCountersteer);
                    Steering.st.VelocityDynamicPeekBox.Value = MainWindow.mw.m.ReadFloat(Addresses.AngleVelocityDynamicPeek);
                    Steering.st.VelocityStraightBox.Value = MainWindow.mw.m.ReadFloat(Addresses.AngleVelocityStraight);
                    Steering.st.VelocityTurningBox.Value = MainWindow.mw.m.ReadFloat(Addresses.AngleVelocityTurning);
                    Steering.st.VelocityTimeBox.Value = MainWindow.mw.m.ReadFloat(Addresses.AngleTimeToMaxSteering);
                    #endregion
                    #region Tires
                    Tires.t.FrontLeftTirePressureBox.Value = MainWindow.mw.m.ReadFloat(Addresses.TireFrontLeft);
                    Tires.t.FrontRightTirePressureBox.Value = MainWindow.mw.m.ReadFloat(Addresses.TireFrontRight);
                    Tires.t.RearLeftTirePressureBox.Value = MainWindow.mw.m.ReadFloat(Addresses.TireRearLeft);
                    Tires.t.RearRightTirePressureBox.Value = MainWindow.mw.m.ReadFloat(Addresses.TireRearRight);
                    #endregion
                });
            }
        }
        #endregion
    }
}
