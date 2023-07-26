using Forza_Mods_AIO.Resources;
using Forza_Mods_AIO.Tabs.TuningTablePort.DropDownTabs;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Forza_Mods_AIO.Tabs.TuningTablePort
{
    public partial class TuningTableMain : Page
    {
        public static TuningTableMain TBM;

        public TuningTableMain()
        {
            InitializeComponent();
            TBM = this;
            UpdateUi.UpdateUI(false, this);
        }

        #region Interaction
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (UpdateUi.AnimCompleted)
            {
                UpdateUi.Animate(sender, UpdateUi.IsClicked[sender.GetType().GetProperty("Name").GetValue(sender).ToString()], this);
                UpdateUi.IsClicked[sender.GetType().GetProperty("Name").GetValue(sender).ToString()] = !UpdateUi.IsClicked[sender.GetType().GetProperty("Name").GetValue(sender).ToString()];
            }
        }
        private async void ScanButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.mw.gvp.Name == "Forza Horizon 5")
                await Addresses.TuningTable(5);
            
            else
                await Addresses.TuningTable(4);

            UpdateUi.UpdateUI(true, this);
            Task.Run(ReadValues);
        }
        #endregion
        #region Functions
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
