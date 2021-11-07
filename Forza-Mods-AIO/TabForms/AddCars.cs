using System;
using System.Diagnostics;
using System.Windows.Forms;


namespace Forza_Mods_AIO
{
    public partial class AddCars : Form
    {
        public static AddCars a = new AddCars();
        public AddCars()
        {
            InitializeComponent();
            a = this;
        }
        public TabForms.PopupForms.AddCarsGuide AddCarsGuide = new TabForms.PopupForms.AddCarsGuide();
        private void AddCars_Load(object sender, EventArgs e)
        {

        }

        private void AddCars_Shown(object sender, EventArgs e)
        {
            if (MainWindow.m.OpenProcess("ForzaHorizon4"))
            {
                var TargetProcess = Process.GetProcessesByName("ForzaHorizon4")[0];
                if (TargetProcess.MainModule.FileName.Contains("Microsoft.SunriseBaseGame"))
                {
                    MainWindow.main.platform = 1;
                }
                else
                {
                    MainWindow.main.platform = 2;
                }
            }


            else if (MainWindow.m.OpenProcess("ForzaHorizon5"))
            {
                var TargetProcess = Process.GetProcessesByName("ForzaHorizon5")[0];
                if (TargetProcess.MainModule.FileName.Contains("Microsoft.624F8B84B80"))
                {
                    Box_LegoPaint.Enabled = false;
                    MainWindow.main.platform = 4;
                }
                else
                {
                    Box_LegoPaint.Enabled = false;
                    MainWindow.main.platform = 5;
                }
            }

        }
        private void Box_AllCars_CheckedChanged(object sender, EventArgs e)
        {
            if (Box_AllCars.Checked)
            {
                Box_Null.Enabled = false;
                Box_RareCars.Enabled = false;
                if (MainWindow.main.platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4CB0D91", "string", "                  ");
                    MainWindow.m.WriteMemory("base+4CB0D79", "string", "                     ");
                    MainWindow.m.WriteMemory("base+4CB0DE1", "string", "                            ");
                    MainWindow.m.WriteMemory("base+4CB0E29", "string", "                                           ");

                }
                else if (MainWindow.main.platform == 2)
                {
                    MainWindow.m.WriteMemory("base+4FA3C91", "string", "                  ");
                    MainWindow.m.WriteMemory("base+4FA3C79", "string", "                     ");
                    MainWindow.m.WriteMemory("base+4FA3CE1", "string", "                            ");
                    MainWindow.m.WriteMemory("base+4FA3D29", "string", "                                           ");
                }
                else if (MainWindow.main.platform == 4)
                {
                    MainWindow.m.WriteMemory("base+60BAAD9", "string", "                  ");
                    //MainWindow.m.WriteMemory("base+4CB0D79", "string", "                     ");
                    MainWindow.m.WriteMemory("base+60BAB11", "string", "                            ");
                    MainWindow.m.WriteMemory("base+60BAB59", "string", "                                           ");

                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+6370489", "string", "                  ");
                    //MainWindow.m.WriteMemory("base+4FA3C79", "string", "                     ");
                    MainWindow.m.WriteMemory("base+63704C1", "string", "                            ");
                    MainWindow.m.WriteMemory("base+6370509", "string", "                                           ");
                }
            }
            else if (!Box_AllCars.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_AllCars.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = System.Drawing.Color.FromArgb(45, 45, 48);
                Box_Null.Enabled = true;
                Box_RareCars.Enabled = true;
                if (MainWindow.main.platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4CB0D91", "string", "AND NOT IsBarnFind");
                    MainWindow.m.WriteMemory("base+4CB0D79", "string", "AND NOT IsMidnightCar");
                    MainWindow.m.WriteMemory("base+4CB0DE1", "string", "AND NotAvailableInAutoshow=0");
                    MainWindow.m.WriteMemory("base+4CB0E29", "string", "AND IsCarVisibleAndReleased(Garage.ModelId)");
                }
                else if (MainWindow.main.platform == 2)
                {
                    MainWindow.m.WriteMemory("base+4FA3C91", "string", "AND NOT IsBarnFind");
                    MainWindow.m.WriteMemory("base+4FA3C79", "string", "AND NOT IsMidnightCar");
                    MainWindow.m.WriteMemory("base+4FA3CE1", "string", "AND NotAvailableInAutoshow=0");
                    MainWindow.m.WriteMemory("base+4FA3D29", "string", "AND IsCarVisibleAndReleased(Garage.ModelId)");
                }
                else if (MainWindow.main.platform == 4)
                {
                    MainWindow.m.WriteMemory("base+60BAAD9", "string", "AND NOT IsBarnFind");
                    //MainWindow.m.WriteMemory("base+4CB0D79", "string", "AND NOT IsMidnightCar");
                    MainWindow.m.WriteMemory("base+60BAB11", "string", "AND NotAvailableInAutoshow=0");
                    MainWindow.m.WriteMemory("base+60BAB59", "string", "AND IsCarVisibleAndReleased(Garage.ModelId)");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+6370489", "string", "AND NOT IsBarnFind");
                    //MainWindow.m.WriteMemory("base+4FA3C79", "string", "AND NOT IsMidnightCar");
                    MainWindow.m.WriteMemory("base+63704C1", "string", "AND NotAvailableInAutoshow=0");
                    MainWindow.m.WriteMemory("base+6370509", "string", "AND IsCarVisibleAndReleased(Garage.ModelId)");
                }
            }
        }

        private void Box_RareCars_CheckedChanged(object sender, EventArgs e)
        {
            if (Box_RareCars.Checked)
            {
                Box_Null.Enabled = false;
                Box_AllCars.Enabled = false;
                if (MainWindow.main.platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4CB0D91", "string", "                  ");
                    MainWindow.m.WriteMemory("base+4CB0D79", "string", "                     ");
                    MainWindow.m.WriteMemory("base+4CB0DFB", "string", "=1                                    ");
                }
                else if (MainWindow.main.platform == 2)
                {
                    MainWindow.m.WriteMemory("base+4FA3C91", "string", "                  ");
                    MainWindow.m.WriteMemory("base+4FA3C79", "string", "                     ");
                    MainWindow.m.WriteMemory("base+4FA3CFB", "string", "=1                                    ");
                }
                else if (MainWindow.main.platform == 4)
                {
                    MainWindow.m.WriteMemory("base+60BAAD9", "string", "                  ");
                    //MainWindow.m.WriteMemory("base+4CB0D79", "string", "                     ");
                    MainWindow.m.WriteMemory("base+60BAB2B", "string", "=1                                    ");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+6370489", "string", "                  ");
                    //MainWindow.m.WriteMemory("base+4FA3C79", "string", "                     ");
                    MainWindow.m.WriteMemory("base+63704DB", "string", "=1                                    ");
                }
            }
            else if (!Box_RareCars.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_RareCars.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = System.Drawing.Color.FromArgb(45, 45, 48);
                Box_Null.Enabled = true;
                Box_AllCars.Enabled = true;
                if (MainWindow.main.platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4CB0D91", "string", "AND NOT IsBarnFind");
                    MainWindow.m.WriteMemory("base+4CB0D79", "string", "AND NOT IsMidnightCar");
                    MainWindow.m.WriteMemory("base+4CB0DFB", "string", "=0                                    ");
                }
                else if (MainWindow.main.platform == 2)
                {
                    MainWindow.m.WriteMemory("base+4FA3C91", "string", "AND NOT IsBarnFind");
                    MainWindow.m.WriteMemory("base+4FA3C79", "string", "AND NOT IsMidnightCar");
                    MainWindow.m.WriteMemory("base+4FA3CFB", "string", "=0                                    ");
                }
                else if (MainWindow.main.platform == 4)
                {
                    MainWindow.m.WriteMemory("base+60BAAD9", "string", "AND NOT IsBarnFind");
                    //MainWindow.m.WriteMemory("base+4CB0D79", "string", "AND NOT IsMidnightCar");
                    MainWindow.m.WriteMemory("base+60BAB2B", "string", "=0                                    ");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+6370489", "string", "AND NOT IsBarnFind");
                    //MainWindow.m.WriteMemory("base+4FA3C79", "string", "AND NOT IsMidnightCar");
                    MainWindow.m.WriteMemory("base+63704DB", "string", "=0                                    ");
                }
            }
        }
        private void Box_Null_CheckedChanged(object sender, EventArgs e)
        {
            if (Box_Null.Checked)
            {
                Box_AllCars.Enabled = false;
                Box_RareCars.Enabled = false;
                if (MainWindow.main.platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4CB1619", "string", "           1215=");
                    MainWindow.m.WriteMemory("base+4CB0DA9", "string", "      1215=");
                    MainWindow.m.WriteMemory("base+4CB0DE1", "string", "AND Garage.Id=1215          ");
                }
                else if (MainWindow.main.platform == 2)
                {
                    MainWindow.m.WriteMemory("base+4FA4519", "string", "           1215=");
                    MainWindow.m.WriteMemory("base+4FA3CA9", "string", "      1215=");
                    MainWindow.m.WriteMemory("base+4FA3CE1", "string", "AND Garage.Id=1215          ");
                }
                else if (MainWindow.main.platform == 4)
                {
                    MainWindow.m.WriteMemory("base+60BB2B9", "string", "           1215=");
                    MainWindow.m.WriteMemory("base+60BAAF1", "string", "      1215=");
                    MainWindow.m.WriteMemory("base+60BAB11", "string", "AND Garage.Id=1215          ");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+6370C69", "string", "           1215=");
                    MainWindow.m.WriteMemory("base+63704A1", "string", "      1215=");
                    MainWindow.m.WriteMemory("base+63704C1", "string", "AND Garage.Id=1215          ");
                }
            }
            else if (!Box_Null.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_Null.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = System.Drawing.Color.FromArgb(45, 45, 48);
                Box_AllCars.Enabled = true;
                Box_RareCars.Enabled = true;
                if (MainWindow.main.platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4CB1619", "string", "Garage.ModelId!=");
                    MainWindow.m.WriteMemory("base+4CB0DA9", "string", "Garage.Id!=");
                    MainWindow.m.WriteMemory("base+4CB0DE1", "string", "AND NotAvailableInAutoshow=0");
                }
                else if (MainWindow.main.platform == 2)
                {
                    MainWindow.m.WriteMemory("base+4FA4519", "string", "Garage.ModelId!=");
                    MainWindow.m.WriteMemory("base+4FA3CA9", "string", "Garage.Id!=");
                    MainWindow.m.WriteMemory("base+4FA3CE1", "string", "AND NotAvailableInAutoshow=0");
                }
                else if (MainWindow.main.platform == 4)
                {
                    MainWindow.m.WriteMemory("base+60BB2B9", "string", "Garage.ModelId!=");
                    MainWindow.m.WriteMemory("base+60BAAF1", "string", "Garage.Id!=");
                    MainWindow.m.WriteMemory("base+60BAB11", "string", "AND NotAvailableInAutoshow=0");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+6370C69", "string", "Garage.ModelId!=");
                    MainWindow.m.WriteMemory("base+63704A1", "string", "Garage.Id!=");
                    MainWindow.m.WriteMemory("base+63704C1", "string", "AND NotAvailableInAutoshow=0");
                }
            }
        }

        private void Box_LegoPaint_CheckedChanged(object sender, EventArgs e)
        {
            //4DE4FC7
            if (Box_LegoPaint.Checked)
            {
                if (MainWindow.main.platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4E05FC7", "string", "b");
                }
                else if (MainWindow.main.platform == 2)
                {
                    MainWindow.m.WriteMemory("base+52E41D7", "string", "b");

                }
                else if (MainWindow.main.platform == 4)
                {
                    MainWindow.m.WriteMemory("base+4E05FC7", "string", "b");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+52E41D7", "string", "b");

                }
            }
            else if (!Box_LegoPaint.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_LegoPaint.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = System.Drawing.Color.FromArgb(45, 45, 48);
                if (MainWindow.main.platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4E05FC7", "string", "H");
                }
                else if (MainWindow.main.platform == 2)
                {
                    MainWindow.m.WriteMemory("base+52E41D7", "string", "H");

                }
                else if (MainWindow.main.platform == 4)
                {
                    MainWindow.m.WriteMemory("base+4E05FC7", "string", "H");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+52E41D7", "string", "H");

                }
            }
        }
        private void Box_RemoveCars_CheckedChanged(object sender, EventArgs e)
        {
            if (Box_RemoveCars.Checked)
            {
                if (MainWindow.main.platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4CF79D8", "string", "b");
                    MainWindow.m.WriteMemory("base+4C88E68", "string", "b");
                }
                else if (MainWindow.main.platform == 2)
                {
                    MainWindow.m.WriteMemory("base+5038320", "string", "b");
                    MainWindow.m.WriteMemory("base+4F68808", "string", "b");
                }
                else if (MainWindow.main.platform == 4)
                {
                    MainWindow.m.WriteMemory("base+60F8090", "string", "b");
                    MainWindow.m.WriteMemory("base+606A970", "string", "b");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+63ADA40", "string", "b");
                    MainWindow.m.WriteMemory("base+6320320", "string", "b");
                }
            }
            else if (!Box_RemoveCars.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_RemoveCars.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = System.Drawing.Color.FromArgb(45, 45, 48);
                if (MainWindow.main.platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4CF79D8", "string", "D");
                    MainWindow.m.WriteMemory("base+4C88E68", "string", "I");
                }
                else if (MainWindow.main.platform == 2)
                {
                    MainWindow.m.WriteMemory("base+5038320", "string", "D");
                    MainWindow.m.WriteMemory("base+4F68808", "string", "I");
                }
                else if (MainWindow.main.platform == 4)
                {
                    MainWindow.m.WriteMemory("base+60F8090", "string", "D");
                    MainWindow.m.WriteMemory("base+606A970", "string", "I");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+63ADA40", "string", "D");
                    MainWindow.m.WriteMemory("base+6320320", "string", "I");
                }
            }
        }


        private void Box_FreeCars_CheckedChanged(object sender, EventArgs e)
        {
            if (Box_FreeCars.Checked)
            {
                Box_Presets.Enabled = false;
                Box_SeriesFix.Enabled = false;
                Box_ThumbsFix.Enabled = false;
                Box_Traffic.Enabled = false;
                Box_ClearGarage.Enabled = false;

                if (MainWindow.main.platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4DFCCB0", "string", "UPDATE Data_Car SET BaseCost = 0 WHERE BaseCost >0                                                                                                                                                                                                                                                                                                                         ");
                }
                else if (MainWindow.main.platform == 2)
                {
                    MainWindow.m.WriteMemory("base+52DA2F0", "string", "UPDATE Data_Car SET BaseCost = 0 WHERE BaseCost >0                                                                                                                                                                                                                                                                                                                         ");

                }
                else if (MainWindow.main.platform == 4)
                {
                    MainWindow.m.WriteMemory("base+630C020", "string", "UPDATE Data_Car SET BaseCost = 0 WHERE BaseCost >0                                                                                                                                                                                                                                                                                                                         ");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+65C1830", "string", "UPDATE Data_Car SET BaseCost = 0 WHERE BaseCost >0                                                                                                                                                                                                                                                                                                                         ");

                }
            }
            else if (!Box_FreeCars.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_FreeCars.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = System.Drawing.Color.FromArgb(45, 45, 48);
                Box_Presets.Enabled = true;
                Box_SeriesFix.Enabled = true;
                Box_ThumbsFix.Enabled = true;
                Box_Traffic.Enabled = true;
                Box_ClearGarage.Enabled = true;

                if (MainWindow.main.platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4DFCCB0", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");
                }
                else if (MainWindow.main.platform == 2)
                {
                    MainWindow.m.WriteMemory("base+52DA2F0", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");

                }
                else if (MainWindow.main.platform == 4)
                {
                    MainWindow.m.WriteMemory("base+630C020", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+65C1830", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");

                }
            }
        }

        private void Box_SeriesFix_CheckedChanged(object sender, EventArgs e)
        {
            if (Box_SeriesFix.Checked)
            {
                Box_FreeCars.Enabled = false;
                Box_Presets.Enabled = false;
                Box_ThumbsFix.Enabled = false;
                Box_Traffic.Enabled = false;

                Box_ClearGarage.Enabled = false;
                if (MainWindow.main.platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4DFCCB0", "string", "UPDATE Profile0_Career_Garage SET LiveryFileName='', VersionedLiveryId='00000000-0000-0000-0000-000000000000'; UPDATE Profile0_Career_Garage SET OriginalOwner='r/ForzaModding'; UPDATE Profile0_Career_Garage SET NumOwners=69                                                                                                                                            ");
                }
                else if (MainWindow.main.platform == 2)
                {
                    MainWindow.m.WriteMemory("base+52DA2F0", "string", "UPDATE Profile0_Career_Garage SET LiveryFileName='', VersionedLiveryId='00000000-0000-0000-0000-000000000000'; UPDATE Profile0_Career_Garage SET OriginalOwner='r/ForzaModding'; UPDATE Profile0_Career_Garage SET NumOwners=69                                                                                                                                            ");

                }
                else if (MainWindow.main.platform == 4)
                {
                    MainWindow.m.WriteMemory("base+630C020", "string", "UPDATE Profile0_Career_Garage SET LiveryFileName='', VersionedLiveryId='00000000-0000-0000-0000-000000000000'; UPDATE Profile0_Career_Garage SET OriginalOwner='r/ForzaModding'; UPDATE Profile0_Career_Garage SET NumOwners=69                                                                                                                                            ");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+65C1830", "string", "UPDATE Profile0_Career_Garage SET LiveryFileName='', VersionedLiveryId='00000000-0000-0000-0000-000000000000'; UPDATE Profile0_Career_Garage SET OriginalOwner='r/ForzaModding'; UPDATE Profile0_Career_Garage SET NumOwners=69                                                                                                                                            ");

                }
            }
            else if (!Box_SeriesFix.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_SeriesFix.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = System.Drawing.Color.FromArgb(45, 45, 48);
                Box_FreeCars.Enabled = true;
                Box_Presets.Enabled = true;
                Box_SeriesFix.Enabled = true;
                Box_ThumbsFix.Enabled = true;
                Box_Traffic.Enabled = true;

                Box_ClearGarage.Enabled = true;

                if (MainWindow.main.platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4DFCCB0", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");
                }
                else if (MainWindow.main.platform == 2)
                {
                    MainWindow.m.WriteMemory("base+52DA2F0", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");

                }
                else if (MainWindow.main.platform == 4)
                {
                    MainWindow.m.WriteMemory("base+630C020", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+65C1830", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");

                }
            }
        }



        private void Box_ThumbsFix_CheckedChanged(object sender, EventArgs e)
        {
            if (Box_ThumbsFix.Checked)
            {
                Box_FreeCars.Enabled = false;
                Box_Presets.Enabled = false;
                Box_SeriesFix.Enabled = false;
                Box_Traffic.Enabled = false;

                Box_ClearGarage.Enabled = false;
                if (MainWindow.main.platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4DFCCB0", "string", "UPDATE Profile0_Career_Garage SET Thumbnail=(SELECT Thumbnail FROM Data_Car WHERE Data_Car.Id = Profile0_Career_Garage.CarId); UPDATE Profile0_Career_Garage SET OriginalOwner='r/ForzaModding'; UPDATE Profile0_Career_Garage SET NumOwners=69                                                                                                                            ");
                }
                else if (MainWindow.main.platform == 2)
                {
                    MainWindow.m.WriteMemory("base+52DA2F0", "string", "UPDATE Profile0_Career_Garage SET Thumbnail=(SELECT Thumbnail FROM Data_Car WHERE Data_Car.Id = Profile0_Career_Garage.CarId); UPDATE Profile0_Career_Garage SET OriginalOwner='r/ForzaModding'; UPDATE Profile0_Career_Garage SET NumOwners=69                                                                                                                            ");

                }
                else if (MainWindow.main.platform == 4)
                {
                    MainWindow.m.WriteMemory("base+630C020", "string", "UPDATE Profile0_Career_Garage SET Thumbnail=(SELECT Thumbnail FROM Data_Car WHERE Data_Car.Id = Profile0_Career_Garage.CarId); UPDATE Profile0_Career_Garage SET OriginalOwner='r/ForzaModding'; UPDATE Profile0_Career_Garage SET NumOwners=69                                                                                                                            ");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+65C1830", "string", "UPDATE Profile0_Career_Garage SET Thumbnail=(SELECT Thumbnail FROM Data_Car WHERE Data_Car.Id = Profile0_Career_Garage.CarId); UPDATE Profile0_Career_Garage SET OriginalOwner='r/ForzaModding'; UPDATE Profile0_Career_Garage SET NumOwners=69                                                                                                                            ");

                }
            }
            else if (!Box_ThumbsFix.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_ThumbsFix.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = System.Drawing.Color.FromArgb(45, 45, 48);
                Box_FreeCars.Enabled = true;
                Box_Presets.Enabled = true;
                Box_SeriesFix.Enabled = true;
                Box_ThumbsFix.Enabled = true;
                Box_Traffic.Enabled = true;

                Box_ClearGarage.Enabled = true;

                if (MainWindow.main.platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4DFCCB0", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");
                }
                else if (MainWindow.main.platform == 2)
                {
                    MainWindow.m.WriteMemory("base+52DA2F0", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");

                }
                else if (MainWindow.main.platform == 4)
                {
                    MainWindow.m.WriteMemory("base+630C020", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+65C1830", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");

                }
            }
        }

        private void Box_Traffic_CheckedChanged(object sender, EventArgs e)
        {
            if (Box_Traffic.Checked)
            {
                Box_FreeCars.Enabled = false;
                Box_Presets.Enabled = false;
                Box_SeriesFix.Enabled = false;
                Box_ThumbsFix.Enabled = false;

                Box_ClearGarage.Enabled = false;
                if (MainWindow.main.platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4DFCCB0", "string", "DROP VIEW Drivable_Data_Car; CREATE VIEW Drivable_Data_Car AS SELECT Data_Car.* FROM Data_Car; INSERT INTO Data_Car_Buckets(CarId) SELECT Id FROM Data_Car WHERE Id NOT IN (SELECT CarId FROM Data_Car_Buckets); UPDATE Data_Car_Buckets SET CarBucket=0, BucketHero=0 WHERE CarBucket IS NULL                                                                             ");
                }
                else if (MainWindow.main.platform == 2)
                {
                    MainWindow.m.WriteMemory("base+52DA2F0", "string", "DROP VIEW Drivable_Data_Car; CREATE VIEW Drivable_Data_Car AS SELECT Data_Car.* FROM Data_Car; INSERT INTO Data_Car_Buckets(CarId) SELECT Id FROM Data_Car WHERE Id NOT IN (SELECT CarId FROM Data_Car_Buckets); UPDATE Data_Car_Buckets SET CarBucket=0, BucketHero=0 WHERE CarBucket IS NULL                                                                             ");

                }
                else if (MainWindow.main.platform == 4)
                {
                    MainWindow.m.WriteMemory("base+630C020", "string", "DROP VIEW Drivable_Data_Car; CREATE VIEW Drivable_Data_Car AS SELECT Data_Car.* FROM Data_Car; INSERT INTO Data_Car_Buckets(CarId) SELECT Id FROM Data_Car WHERE Id NOT IN (SELECT CarId FROM Data_Car_Buckets); UPDATE Data_Car_Buckets SET CarBucket=0, BucketHero=0 WHERE CarBucket IS NULL                                                                             ");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+65C1830", "string", "DROP VIEW Drivable_Data_Car; CREATE VIEW Drivable_Data_Car AS SELECT Data_Car.* FROM Data_Car; INSERT INTO Data_Car_Buckets(CarId) SELECT Id FROM Data_Car WHERE Id NOT IN (SELECT CarId FROM Data_Car_Buckets); UPDATE Data_Car_Buckets SET CarBucket=0, BucketHero=0 WHERE CarBucket IS NULL                                                                             ");

                }
            }
            else if (!Box_Traffic.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_Traffic.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = System.Drawing.Color.FromArgb(45, 45, 48);
                Box_FreeCars.Enabled = true;
                Box_Presets.Enabled = true;
                Box_SeriesFix.Enabled = true;
                Box_ThumbsFix.Enabled = true;
                Box_Traffic.Enabled = true;

                Box_ClearGarage.Enabled = true;

                if (MainWindow.main.platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4DFCCB0", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");
                }
                else if (MainWindow.main.platform == 2)
                {
                    MainWindow.m.WriteMemory("base+52DA2F0", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");

                }
                else if (MainWindow.main.platform == 4)
                {
                    MainWindow.m.WriteMemory("base+630C020", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+65C1830", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");

                }
            }
        }

        private void Box_Presets_CheckedChanged(object sender, EventArgs e)
        {
            if (Box_Presets.Checked)
            {
                Box_FreeCars.Enabled = false;
                Box_SeriesFix.Enabled = false;
                Box_ThumbsFix.Enabled = false;
                Box_Traffic.Enabled = false;

                Box_ClearGarage.Enabled = false;
                if (MainWindow.main.platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4DFCCB0", "string", "UPDATE UpgradePresetPackages SET Purchasable=1 WHERE Purchasable=0                                                                                                                                                                                                                                                                                                         ");
                }
                else if (MainWindow.main.platform == 2)
                {
                    MainWindow.m.WriteMemory("base+52DA2F0", "string", "UPDATE UpgradePresetPackages SET Purchasable=1 WHERE Purchasable=0                                                                                                                                                                                                                                                                                                         ");

                }
                else if (MainWindow.main.platform == 4)
                {
                    MainWindow.m.WriteMemory("base+630C020", "string", "UPDATE UpgradePresetPackages SET Purchasable=1 WHERE Purchasable=0                                                                                                                                                                                                                                                                                                         ");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+65C1830", "string", "UPDATE UpgradePresetPackages SET Purchasable=1 WHERE Purchasable=0                                                                                                                                                                                                                                                                                                         ");

                }
            }
            else if (!Box_Presets.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_Presets.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = System.Drawing.Color.FromArgb(45, 45, 48);
                Box_FreeCars.Enabled = true;
                Box_Presets.Enabled = true;
                Box_SeriesFix.Enabled = true;
                Box_ThumbsFix.Enabled = true;
                Box_Traffic.Enabled = true;

                Box_ClearGarage.Enabled = true;
                if (MainWindow.main.platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4DFCCB0", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");
                }
                else if (MainWindow.main.platform == 2)
                {
                    MainWindow.m.WriteMemory("base+52DA2F0", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");

                }
                else if (MainWindow.main.platform == 4)
                {
                    MainWindow.m.WriteMemory("base+630C020", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+65C1830", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");

                }
            }
        }

        private void Box_Decals_CheckedChanged(object sender, EventArgs e)
        {
            if (Box_Decals.Checked)
            {
                if (MainWindow.main.platform == 1)
                {

                    MainWindow.m.WriteMemory("base+4D1F5F9", "string", "WHERE Id >=0 ORDER BY Id                                                                                                                      ");

                }
                else if (MainWindow.main.platform == 2)
                {
                    MainWindow.m.WriteMemory("base+509DDE9", "string", "WHERE Id >=0 ORDER BY Id                                                                                                                      ");

                }
                else if (MainWindow.main.platform == 4)
                {

                    MainWindow.m.WriteMemory("base+60F0699", "string", "WHERE Id >=0 ORDER BY Id                                                                                                                      ");

                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+63A6049", "string", "WHERE Id >=0 ORDER BY Id                                                                                                                      ");

                }
            }
            else if (!Box_Decals.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_Decals.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = System.Drawing.Color.FromArgb(45, 45, 48);
                if (MainWindow.main.platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4D1F5F9", "string", "INNER JOIN Livery_DecalsSortOrder ON (Livery_Decals.ID = Livery_DecalsSortOrder.Livery_DecalID) WHERE MakeID = %d ORDER BY Sequence, AlphaSort");
                }
                else if (MainWindow.main.platform == 2)
                {
                    MainWindow.m.WriteMemory("base+509DDE9", "string", "INNER JOIN Livery_DecalsSortOrder ON (Livery_Decals.ID = Livery_DecalsSortOrder.Livery_DecalID) WHERE MakeID = %d ORDER BY Sequence, AlphaSort");

                }
                else if (MainWindow.main.platform == 4)
                {
                    MainWindow.m.WriteMemory("base+60F0699", "string", "INNER JOIN Livery_DecalsSortOrder ON (Livery_Decals.ID = Livery_DecalsSortOrder.Livery_DecalID) WHERE MakeID = %d ORDER BY Sequence, AlphaSort");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+63A6049", "string", "INNER JOIN Livery_DecalsSortOrder ON (Livery_Decals.ID = Livery_DecalsSortOrder.Livery_DecalID) WHERE MakeID = %d ORDER BY Sequence, AlphaSort");

                }
            }
        }
        private void Box_ClearGarage_CheckedChanged(object sender, EventArgs e)
        {
            if (Box_ClearGarage.Checked)
            {
                Box_FreeCars.Enabled = false;
                Box_Presets.Enabled = false;
                Box_SeriesFix.Enabled = false;
                Box_ThumbsFix.Enabled = false;
                Box_Traffic.Enabled = false;

                if (MainWindow.main.platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4DFCCB0", "string", "DELETE FROM Profile0_Career_Garage WHERE Id > 0                                                                                                                                                                                                                                                                                                                            ");
                }
                else if (MainWindow.main.platform == 2)
                {
                    MainWindow.m.WriteMemory("base+52DA2F0", "string", "DELETE FROM Profile0_Career_Garage WHERE Id > 0                                                                                                                                                                                                                                                                                                                            ");

                }
                else if (MainWindow.main.platform == 4)
                {
                    MainWindow.m.WriteMemory("base+630C020", "string", "DELETE FROM Profile0_Career_Garage WHERE Id > 0                                                                                                                                                                                                                                                                                                                            ");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+65C1830", "string", "DELETE FROM Profile0_Career_Garage WHERE Id > 0                                                                                                                                                                                                                                                                                                                            ");

                }
            }
            else if (!Box_ClearGarage.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_ClearGarage.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = System.Drawing.Color.FromArgb(45, 45, 48);
                Box_FreeCars.Enabled = true;
                Box_Presets.Enabled = true;
                Box_SeriesFix.Enabled = true;
                Box_ThumbsFix.Enabled = true;
                Box_Traffic.Enabled = true;

                if (MainWindow.main.platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4DFCCB0", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");
                }
                else if (MainWindow.main.platform == 2)
                {
                    MainWindow.m.WriteMemory("base+52DA2F0", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");

                }
                else if (MainWindow.main.platform == 4)
                {
                    MainWindow.m.WriteMemory("base+630C020", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+65C1830", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");

                }
            }
        }

        private void BTN_Help_Click(object sender, EventArgs e)
        {
            AddCarsGuide.Show();
        }
    }
}
