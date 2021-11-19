using Forza_Mods_AIO.TabForms.PopupForms;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace Forza_Mods_AIO
{
    public partial class AddCars : Form
    {
        public static AddCars a = new AddCars();
        public QuickAddCars QuickAddCars = new QuickAddCars();
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
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_AllCars.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = ColorTranslator.FromHtml(MainWindow.ThemeColour);
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
                    MainWindow.m.WriteMemory("base+60BB6C9", "string", "                  ");
                    //MainWindow.m.WriteMemory("base+4CB0D79", "string", "                     ");
                    MainWindow.m.WriteMemory("base+60BB701", "string", "                            ");
                    MainWindow.m.WriteMemory("base+60BB749", "string", "                                           ");
                    MainWindow.m.WriteMemory("base+6005CE0", "string", "    Garage.IsInstalled            AS PurchasableCar,");


                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+6371059", "string", "                  ");
                    //MainWindow.m.WriteMemory("base+4FA3C79", "string", "                     ");
                    MainWindow.m.WriteMemory("base+6371091", "string", "                            ");
                    MainWindow.m.WriteMemory("base+63710D9", "string", "                                           ");
                    MainWindow.m.WriteMemory("base+62BB670", "string", "    Garage.IsInstalled            AS PurchasableCar,");
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
                    MainWindow.m.WriteMemory("base+60BB6C9", "string", "AND NOT IsBarnFind");
                    //MainWindow.m.WriteMemory("base+4CB0D79", "string", "AND NOT IsMidnightCar");
                    MainWindow.m.WriteMemory("base+60BB701", "string", "AND NotAvailableInAutoshow=0");
                    MainWindow.m.WriteMemory("base+60BB749", "string", "AND IsCarVisibleAndReleased(Garage.ModelId)");
                    MainWindow.m.WriteMemory("base+6005CE0", "string", "NOT Garage.NotAvailableInAutoshow AS PurchasableCar,");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+6371059", "string", "AND NOT IsBarnFind");
                    //MainWindow.m.WriteMemory("base+4FA3C79", "string", "AND NOT IsMidnightCar");
                    MainWindow.m.WriteMemory("base+6371091", "string", "AND NotAvailableInAutoshow=0");
                    MainWindow.m.WriteMemory("base+63710D9", "string", "AND IsCarVisibleAndReleased(Garage.ModelId)");
                    MainWindow.m.WriteMemory("base+62BB670", "string", "NOT Garage.NotAvailableInAutoshow AS PurchasableCar,");
                }
            }
        }

        private void Box_RareCars_CheckedChanged(object sender, EventArgs e)
        {
            if (Box_RareCars.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_RareCars.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = ColorTranslator.FromHtml(MainWindow.ThemeColour);
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
                    MainWindow.m.WriteMemory("base+60BB6C9", "string", "                  ");
                    //MainWindow.m.WriteMemory("base+4CB0D79", "string", "                     ");
                    MainWindow.m.WriteMemory("base+60BB71B", "string", "=1                                    ");
                    MainWindow.m.WriteMemory("base+6005CE0", "string", "    Garage.IsInstalled            AS PurchasableCar,");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+6371059", "string", "                  ");
                    //MainWindow.m.WriteMemory("base+4FA3C79", "string", "                     ");
                    MainWindow.m.WriteMemory("base+63710AB", "string", "=1                                    ");
                    MainWindow.m.WriteMemory("base+62BB670", "string", "    Garage.IsInstalled            AS PurchasableCar,");
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
                    MainWindow.m.WriteMemory("base+60BB6C9", "string", "AND NOT IsBarnFind");
                    //MainWindow.m.WriteMemory("base+4CB0D79", "string", "AND NOT IsMidnightCar");
                    MainWindow.m.WriteMemory("base+60BB71B", "string", "=0                                    ");
                    MainWindow.m.WriteMemory("base+6005CE0", "string", "NOT Garage.NotAvailableInAutoshow AS PurchasableCar,");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+6371059", "string", "AND NOT IsBarnFind");
                    //MainWindow.m.WriteMemory("base+4FA3C79", "string", "AND NOT IsMidnightCar");
                    MainWindow.m.WriteMemory("base+63710AB", "string", "=0                                    ");
                    MainWindow.m.WriteMemory("base+62BB670", "string", "NOT Garage.NotAvailableInAutoshow AS PurchasableCar,");
                }
            }
        }
        private void Box_Null_CheckedChanged(object sender, EventArgs e)
        {
            if (Box_Null.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_Null.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = ColorTranslator.FromHtml(MainWindow.ThemeColour);
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
                    MainWindow.m.WriteMemory("base+60BBEA9", "string", "           1215=");
                    MainWindow.m.WriteMemory("base+60BB6E1", "string", "      1215=");
                    MainWindow.m.WriteMemory("base+60BB701", "string", "AND Garage.Id=1215          ");
                    MainWindow.m.WriteMemory("base+6005CE0", "string", "    Garage.IsInstalled            AS PurchasableCar,");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+6371839", "string", "           1215=");
                    MainWindow.m.WriteMemory("base+6371071", "string", "      1215=");
                    MainWindow.m.WriteMemory("base+6371091", "string", "AND Garage.Id=1215          ");
                    MainWindow.m.WriteMemory("base+62BB670", "string", "    Garage.IsInstalled            AS PurchasableCar,");
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
                    MainWindow.m.WriteMemory("base+60BBEA9", "string", "Garage.ModelId!=");
                    MainWindow.m.WriteMemory("base+60BB6E1", "string", "Garage.Id!=");
                    MainWindow.m.WriteMemory("base+60BB701", "string", "AND NotAvailableInAutoshow=0");
                    MainWindow.m.WriteMemory("base+6005CE0", "string", "NOT Garage.NotAvailableInAutoshow AS PurchasableCar,");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+6371839", "string", "Garage.ModelId!=");
                    MainWindow.m.WriteMemory("base+6371071", "string", "Garage.Id!=");
                    MainWindow.m.WriteMemory("base+6371091", "string", "AND NotAvailableInAutoshow=0");
                    MainWindow.m.WriteMemory("base+62BB670", "string", "NOT Garage.NotAvailableInAutoshow AS PurchasableCar,");
                }
            }
        }

        private void Box_LegoPaint_CheckedChanged(object sender, EventArgs e)
        {
            //4DE4FC7
            if (Box_LegoPaint.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_LegoPaint.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = ColorTranslator.FromHtml(MainWindow.ThemeColour);
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
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_RemoveCars.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = ColorTranslator.FromHtml(MainWindow.ThemeColour);
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
                    MainWindow.m.WriteMemory("base+60F8C80", "string", "b");
                    MainWindow.m.WriteMemory("base+606B560", "string", "b");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+63AE610", "string", "b");
                    MainWindow.m.WriteMemory("base+6320EF0", "string", "b");
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
                    MainWindow.m.WriteMemory("base+60F8C80", "string", "D");
                    MainWindow.m.WriteMemory("base+606B560", "string", "I");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+63AE610", "string", "D");
                    MainWindow.m.WriteMemory("base+6320EF0", "string", "I");
                }
            }
        }


        private void Box_FreeCars_CheckedChanged(object sender, EventArgs e)
        {
            if (Box_FreeCars.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_FreeCars.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = ColorTranslator.FromHtml(MainWindow.ThemeColour);
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
                    MainWindow.m.WriteMemory("base+630C540", "string", "UPDATE Data_Car SET BaseCost = 0 WHERE BaseCost >0                                                                                                                                                                                                                                                                                                                         ");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+65C1D80", "string", "UPDATE Data_Car SET BaseCost = 0 WHERE BaseCost >0                                                                                                                                                                                                                                                                                                                         ");

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
                    MainWindow.m.WriteMemory("base+630C540", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+65C1D80", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");

                }
            }
        }

        private void Box_SeriesFix_CheckedChanged(object sender, EventArgs e)
        {
            if (Box_SeriesFix.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_SeriesFix.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = ColorTranslator.FromHtml(MainWindow.ThemeColour);
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
                    MainWindow.m.WriteMemory("base+630C540", "string", "UPDATE Profile0_Career_Garage SET LiveryFileName='', VersionedLiveryId='00000000-0000-0000-0000-000000000000'; UPDATE Profile0_Career_Garage SET OriginalOwner='r/ForzaModding'; UPDATE Profile0_Career_Garage SET NumOwners=69                                                                                                                                            ");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+65C1D80", "string", "UPDATE Profile0_Career_Garage SET LiveryFileName='', VersionedLiveryId='00000000-0000-0000-0000-000000000000'; UPDATE Profile0_Career_Garage SET OriginalOwner='r/ForzaModding'; UPDATE Profile0_Career_Garage SET NumOwners=69                                                                                                                                            ");

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
                    MainWindow.m.WriteMemory("base+630C540", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+65C1D80", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");

                }
            }
        }



        private void Box_ThumbsFix_CheckedChanged(object sender, EventArgs e)
        {
            if (Box_ThumbsFix.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_ThumbsFix.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = ColorTranslator.FromHtml(MainWindow.ThemeColour);
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
                    MainWindow.m.WriteMemory("base+630C540", "string", "UPDATE Profile0_Career_Garage SET Thumbnail=(SELECT Thumbnail FROM Data_Car WHERE Data_Car.Id = Profile0_Career_Garage.CarId); UPDATE Profile0_Career_Garage SET OriginalOwner='r/ForzaModding'; UPDATE Profile0_Career_Garage SET NumOwners=69                                                                                                                            ");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+65C1D80", "string", "UPDATE Profile0_Career_Garage SET Thumbnail=(SELECT Thumbnail FROM Data_Car WHERE Data_Car.Id = Profile0_Career_Garage.CarId); UPDATE Profile0_Career_Garage SET OriginalOwner='r/ForzaModding'; UPDATE Profile0_Career_Garage SET NumOwners=69                                                                                                                            ");

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
                    MainWindow.m.WriteMemory("base+630C540", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+65C1D80", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");

                }
            }
        }

        private void Box_Traffic_CheckedChanged(object sender, EventArgs e)
        {
            if (Box_Traffic.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_Traffic.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = ColorTranslator.FromHtml(MainWindow.ThemeColour);
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
                    MainWindow.m.WriteMemory("base+630C540", "string", "DROP VIEW Drivable_Data_Car; CREATE VIEW Drivable_Data_Car AS SELECT Data_Car.* FROM Data_Car; INSERT INTO Data_Car_Buckets(CarId) SELECT Id FROM Data_Car WHERE Id NOT IN (SELECT CarId FROM Data_Car_Buckets); UPDATE Data_Car_Buckets SET CarBucket=0, BucketHero=0 WHERE CarBucket IS NULL                                                                             ");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+65C1D80", "string", "DROP VIEW Drivable_Data_Car; CREATE VIEW Drivable_Data_Car AS SELECT Data_Car.* FROM Data_Car; INSERT INTO Data_Car_Buckets(CarId) SELECT Id FROM Data_Car WHERE Id NOT IN (SELECT CarId FROM Data_Car_Buckets); UPDATE Data_Car_Buckets SET CarBucket=0, BucketHero=0 WHERE CarBucket IS NULL                                                                             ");

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
                    MainWindow.m.WriteMemory("base+630C540", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+65C1D80", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");

                }
            }
        }

        private void Box_Presets_CheckedChanged(object sender, EventArgs e)
        {
            if (Box_Presets.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_Presets.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = ColorTranslator.FromHtml(MainWindow.ThemeColour);
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
                    MainWindow.m.WriteMemory("base+630C540", "string", "UPDATE UpgradePresetPackages SET Purchasable=1 WHERE Purchasable=0                                                                                                                                                                                                                                                                                                         ");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+65C1D80", "string", "UPDATE UpgradePresetPackages SET Purchasable=1 WHERE Purchasable=0                                                                                                                                                                                                                                                                                                         ");

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
                    MainWindow.m.WriteMemory("base+630C540", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+65C1D80", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");

                }
            }
        }

        private void Box_Decals_CheckedChanged(object sender, EventArgs e)
        {
            if (Box_Decals.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_Decals.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = ColorTranslator.FromHtml(MainWindow.ThemeColour);
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

                    MainWindow.m.WriteMemory("base+630C540", "string", "WHERE Id >=0 ORDER BY Id                                                                                                                      ");

                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+63A6C19", "string", "WHERE Id >=0 ORDER BY Id                                                                                                                      ");

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
                    MainWindow.m.WriteMemory("base+630C540", "string", "INNER JOIN Livery_DecalsSortOrder ON (Livery_Decals.ID = Livery_DecalsSortOrder.Livery_DecalID) WHERE MakeID = %d ORDER BY Sequence, AlphaSort");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+63A6C19", "string", "INNER JOIN Livery_DecalsSortOrder ON (Livery_Decals.ID = Livery_DecalsSortOrder.Livery_DecalID) WHERE MakeID = %d ORDER BY Sequence, AlphaSort");

                }
            }
        }
        private void Box_ClearGarage_CheckedChanged(object sender, EventArgs e)
        {
            if (Box_ClearGarage.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_ClearGarage.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = ColorTranslator.FromHtml(MainWindow.ThemeColour);
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
                    MainWindow.m.WriteMemory("base+630C540", "string", "DELETE FROM Profile0_Career_Garage WHERE Id > 0                                                                                                                                                                                                                                                                                                                            ");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+65C1D80", "string", "DELETE FROM Profile0_Career_Garage WHERE Id > 0                                                                                                                                                                                                                                                                                                                            ");

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
                    MainWindow.m.WriteMemory("base+630C540", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+65C1D80", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");

                }
            }
        }

        private void BTN_Help_Click(object sender, EventArgs e)
        {
            AddCarsGuide.Show();
        }

        private void Url_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "\"https://youtu.be/hmMxhMHiLtg\"");
        }

        private void FreePerfBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (FreePerfBox.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)FreePerfBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = ColorTranslator.FromHtml(MainWindow.ThemeColour);
                FreeVisBox.Enabled = false;
                if (MainWindow.main.platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4DFD1A0", "string", "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ");
                    MainWindow.m.WriteBytes("base+4DFD99F", new byte[] { 0x00 });
                    MainWindow.m.WriteMemory("base+4DFD1A0", "string", "UPDATE List_UpgradeAntiSwayFront SET price=0;UPDATE List_UpgradeAntiSwayRear SET price=0;UPDATE List_UpgradeBrakes SET price=0;UPDATE List_UpgradeCarBodyChassisStiffness SET price=0;UPDATE List_UpgradeCarBody SET price=0;UPDATE List_UpgradeCarBodyTireWidthFront SET price=0;UPDATE List_UpgradeCarBodyTireWidthRear SET price=0;UPDATE List_UpgradeCarBodyTrackSpacingFront SET price=0;UPDATE List_UpgradeCarBodyTrackSpacingRear SET price=0;UPDATE List_UpgradeCarBodyWeight SET price=0;UPDATE List_UpgradeDrivetrain SET price=0;UPDATE List_UpgradeDrivetrainClutch SET price=0;UPDATE List_UpgradeDrivetrainDifferential  SET price=0;UPDATE List_UpgradeDrivetrainDriveline SET price=0;UPDATE List_UpgradeDrivetrainTransmission SET price=0;UPDATE List_UpgradeEngine SET price=0;UPDATE List_UpgradeEngineCamshaft SET price=0;UPDATE List_UpgradeEngineCSC SET price=0;UPDATE List_UpgradeEngineDisplacement SET price=0;UPDATE List_UpgradeEngineDSC SET price=0;UPDATE List_UpgradeEngineExhaust SET price=0;UPDATE List_UpgradeEngineFlywheel SET price=0;UPDATE List_UpgradeEngineFuelSystem SET price=0;UPDATE List_UpgradeEngineIgnition SET price=0;UPDATE List_UpgradeEngineIntake SET price=0;UPDATE List_UpgradeEngineIntercooler SET price=0;UPDATE List_UpgradeEngineManifold SET price=0;UPDATE List_UpgradeEngineOilCooling SET price=0;UPDATE List_UpgradeEnginePistonsCompression SET price=0;UPDATE List_UpgradeEngineRestrictorPlate SET price=0;UPDATE List_UpgradeEngineTurboQuad SET price=0;UPDATE List_UpgradeEngineTurboSingle SET price=0;UPDATE List_UpgradeEngineTurboTwin SET price=0;UPDATE List_UpgradeEngineValves SET price=0;UPDATE List_UpgradeMotor SET price=0;UPDATE List_UpgradeMotorParts SET price=0;UPDATE List_UpgradeSpringDamper SET price=0;UPDATE List_UpgradeTireCompound SET price=0;UPDATE List_VariableTiming SET price=0;UPDATE List_Wheels SET price=1;");
                }
                else if (MainWindow.main.platform == 2)
                {
                    MainWindow.m.WriteMemory("base+52DA7E0", "string", "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ");
                    MainWindow.m.WriteBytes("base+52DAFDF", new byte[] { 0x00 });
                    MainWindow.m.WriteMemory("base+52DA7E0", "string", "UPDATE List_UpgradeAntiSwayFront SET price=0;UPDATE List_UpgradeAntiSwayRear SET price=0;UPDATE List_UpgradeBrakes SET price=0;UPDATE List_UpgradeCarBodyChassisStiffness SET price=0;UPDATE List_UpgradeCarBody SET price=0;UPDATE List_UpgradeCarBodyTireWidthFront SET price=0;UPDATE List_UpgradeCarBodyTireWidthRear SET price=0;UPDATE List_UpgradeCarBodyTrackSpacingFront SET price=0;UPDATE List_UpgradeCarBodyTrackSpacingRear SET price=0;UPDATE List_UpgradeCarBodyWeight SET price=0;UPDATE List_UpgradeDrivetrain SET price=0;UPDATE List_UpgradeDrivetrainClutch SET price=0;UPDATE List_UpgradeDrivetrainDifferential  SET price=0;UPDATE List_UpgradeDrivetrainDriveline SET price=0;UPDATE List_UpgradeDrivetrainTransmission SET price=0;UPDATE List_UpgradeEngine SET price=0;UPDATE List_UpgradeEngineCamshaft SET price=0;UPDATE List_UpgradeEngineCSC SET price=0;UPDATE List_UpgradeEngineDisplacement SET price=0;UPDATE List_UpgradeEngineDSC SET price=0;UPDATE List_UpgradeEngineExhaust SET price=0;UPDATE List_UpgradeEngineFlywheel SET price=0;UPDATE List_UpgradeEngineFuelSystem SET price=0;UPDATE List_UpgradeEngineIgnition SET price=0;UPDATE List_UpgradeEngineIntake SET price=0;UPDATE List_UpgradeEngineIntercooler SET price=0;UPDATE List_UpgradeEngineManifold SET price=0;UPDATE List_UpgradeEngineOilCooling SET price=0;UPDATE List_UpgradeEnginePistonsCompression SET price=0;UPDATE List_UpgradeEngineRestrictorPlate SET price=0;UPDATE List_UpgradeEngineTurboQuad SET price=0;UPDATE List_UpgradeEngineTurboSingle SET price=0;UPDATE List_UpgradeEngineTurboTwin SET price=0;UPDATE List_UpgradeEngineValves SET price=0;UPDATE List_UpgradeMotor SET price=0;UPDATE List_UpgradeMotorParts SET price=0;UPDATE List_UpgradeSpringDamper SET price=0;UPDATE List_UpgradeTireCompound SET price=0;UPDATE List_VariableTiming SET price=0;UPDATE List_Wheels SET price=1;");
                }
                else if (MainWindow.main.platform == 4)
                {
                    MainWindow.m.WriteMemory("base+630CA30", "string", "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ");
                    MainWindow.m.WriteBytes("base+630D22F", new byte[] { 0x00 });
                    MainWindow.m.WriteMemory("base+630CA30", "string", "UPDATE List_UpgradeAntiSwayFront SET price=0;UPDATE List_UpgradeAntiSwayRear SET price=0;UPDATE List_UpgradeBrakes SET price=0;UPDATE List_UpgradeCarBodyChassisStiffness SET price=0;UPDATE List_UpgradeCarBody SET price=0;UPDATE List_UpgradeCarBodyTireWidthFront SET price=0;UPDATE List_UpgradeCarBodyTireWidthRear SET price=0;UPDATE List_UpgradeCarBodyTrackSpacingFront SET price=0;UPDATE List_UpgradeCarBodyTrackSpacingRear SET price=0;UPDATE List_UpgradeCarBodyWeight SET price=0;UPDATE List_UpgradeDrivetrain SET price=0;UPDATE List_UpgradeDrivetrainClutch SET price=0;UPDATE List_UpgradeDrivetrainDifferential  SET price=0;UPDATE List_UpgradeDrivetrainDriveline SET price=0;UPDATE List_UpgradeDrivetrainTransmission SET price=0;UPDATE List_UpgradeEngine SET price=0;UPDATE List_UpgradeEngineCamshaft SET price=0;UPDATE List_UpgradeEngineCSC SET price=0;UPDATE List_UpgradeEngineDisplacement SET price=0;UPDATE List_UpgradeEngineDSC SET price=0;UPDATE List_UpgradeEngineExhaust SET price=0;UPDATE List_UpgradeEngineFlywheel SET price=0;UPDATE List_UpgradeEngineFuelSystem SET price=0;UPDATE List_UpgradeEngineIgnition SET price=0;UPDATE List_UpgradeEngineIntake SET price=0;UPDATE List_UpgradeEngineIntercooler SET price=0;UPDATE List_UpgradeEngineManifold SET price=0;UPDATE List_UpgradeEngineOilCooling SET price=0;UPDATE List_UpgradeEnginePistonsCompression SET price=0;UPDATE List_UpgradeEngineRestrictorPlate SET price=0;UPDATE List_UpgradeEngineTurboQuad SET price=0;UPDATE List_UpgradeEngineTurboSingle SET price=0;UPDATE List_UpgradeEngineTurboTwin SET price=0;UPDATE List_UpgradeEngineValves SET price=0;UPDATE List_UpgradeMotor SET price=0;UPDATE List_UpgradeMotorParts SET price=0;UPDATE List_UpgradeSpringDamper SET price=0;UPDATE List_UpgradeTireCompound SET price=0;UPDATE List_VariableTiming SET price=0;UPDATE List_Wheels SET price=1;");
                }
                if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+65C2270", "string", "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ");
                    MainWindow.m.WriteBytes("base+65C251F", new byte[] { 0x00 });
                    MainWindow.m.WriteMemory("base+65C2270", "string", "UPDATE List_UpgradeAntiSwayFront SET price=0;UPDATE List_UpgradeAntiSwayRear SET price=0;UPDATE List_UpgradeBrakes SET price=0;UPDATE List_UpgradeCarBodyChassisStiffness SET price=0;UPDATE List_UpgradeCarBody SET price=0;UPDATE List_UpgradeCarBodyTireWidthFront SET price=0;UPDATE List_UpgradeCarBodyTireWidthRear SET price=0;UPDATE List_UpgradeCarBodyTrackSpacingFront SET price=0;UPDATE List_UpgradeCarBodyTrackSpacingRear SET price=0;UPDATE List_UpgradeCarBodyWeight SET price=0;UPDATE List_UpgradeDrivetrain SET price=0;UPDATE List_UpgradeDrivetrainClutch SET price=0;UPDATE List_UpgradeDrivetrainDifferential  SET price=0;UPDATE List_UpgradeDrivetrainDriveline SET price=0;UPDATE List_UpgradeDrivetrainTransmission SET price=0;UPDATE List_UpgradeEngine SET price=0;UPDATE List_UpgradeEngineCamshaft SET price=0;UPDATE List_UpgradeEngineCSC SET price=0;UPDATE List_UpgradeEngineDisplacement SET price=0;UPDATE List_UpgradeEngineDSC SET price=0;UPDATE List_UpgradeEngineExhaust SET price=0;UPDATE List_UpgradeEngineFlywheel SET price=0;UPDATE List_UpgradeEngineFuelSystem SET price=0;UPDATE List_UpgradeEngineIgnition SET price=0;UPDATE List_UpgradeEngineIntake SET price=0;UPDATE List_UpgradeEngineIntercooler SET price=0;UPDATE List_UpgradeEngineManifold SET price=0;UPDATE List_UpgradeEngineOilCooling SET price=0;UPDATE List_UpgradeEnginePistonsCompression SET price=0;UPDATE List_UpgradeEngineRestrictorPlate SET price=0;UPDATE List_UpgradeEngineTurboQuad SET price=0;UPDATE List_UpgradeEngineTurboSingle SET price=0;UPDATE List_UpgradeEngineTurboTwin SET price=0;UPDATE List_UpgradeEngineValves SET price=0;UPDATE List_UpgradeMotor SET price=0;UPDATE List_UpgradeMotorParts SET price=0;UPDATE List_UpgradeSpringDamper SET price=0;UPDATE List_UpgradeTireCompound SET price=0;UPDATE List_VariableTiming SET price=0;UPDATE List_Wheels SET price=1;");
                }
            }
            else if (!FreePerfBox.Checked)
            {
                var OriginalData = new byte[] { 0x55, 0x50, 0x44, 0x41, 0x54, 0x45, 0x20, 0x25, 0x73, 0x43, 0x61, 0x72, 0x65, 0x65, 0x72, 0x5F, 0x47, 0x61, 0x72, 0x61, 0x67, 0x65, 0x20, 0x53, 0x45, 0x54, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x44, 0x6F, 0x77, 0x6E, 0x66, 0x6F, 0x72, 0x63, 0x65, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x44, 0x6F, 0x77, 0x6E, 0x66, 0x6F, 0x72, 0x63, 0x65, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x69, 0x6E, 0x61, 0x6C, 0x44, 0x72, 0x69, 0x76, 0x65, 0x52, 0x61, 0x74, 0x69, 0x6F, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x69, 0x72, 0x73, 0x74, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x73, 0x65, 0x63, 0x6F, 0x6E, 0x64, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x74, 0x68, 0x69, 0x72, 0x64, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x6F, 0x75, 0x72, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x69, 0x66, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x73, 0x69, 0x78, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x73, 0x65, 0x76, 0x65, 0x6E, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x65, 0x69, 0x67, 0x68, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x6E, 0x69, 0x6E, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x74, 0x65, 0x6E, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x62, 0x72, 0x61, 0x6B, 0x65, 0x42, 0x61, 0x6C, 0x61, 0x6E, 0x63, 0x65, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x62, 0x72, 0x61, 0x6B, 0x65, 0x50, 0x72, 0x65, 0x73, 0x73, 0x75, 0x72, 0x65, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x63, 0x65, 0x6E, 0x74, 0x65, 0x72, 0x54, 0x6F, 0x72, 0x71, 0x75, 0x65, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x54, 0x69, 0x72, 0x65, 0x50, 0x72, 0x65, 0x73, 0x73, 0x75, 0x72, 0x65, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x43, 0x61, 0x6D, 0x62, 0x65, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x54, 0x6F, 0x65, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x43, 0x61, 0x73, 0x74, 0x65, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x53, 0x70, 0x72, 0x69, 0x6E, 0x67, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x53, 0x77, 0x61, 0x79, 0x62, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x52, 0x69, 0x64, 0x65, 0x48, 0x65, 0x69, 0x67, 0x68, 0x74, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x44, 0x61, 0x6D, 0x70, 0x69, 0x6E, 0x67, 0x53, 0x74, 0x69, 0x66, 0x66, 0x6E, 0x65, 0x73, 0x73, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x42, 0x75, 0x6D, 0x70, 0x52, 0x61, 0x74, 0x69, 0x6F, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x41, 0x63, 0x63, 0x65, 0x6C, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x44, 0x65, 0x63, 0x65, 0x6C, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x54, 0x69, 0x72, 0x65, 0x50, 0x72, 0x65, 0x73, 0x73, 0x75, 0x72, 0x65, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x43, 0x61, 0x6D, 0x62, 0x65, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x54, 0x6F, 0x65, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x53, 0x70, 0x72, 0x69, 0x6E, 0x67, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x53, 0x77, 0x61, 0x79, 0x62, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x52, 0x69, 0x64, 0x65, 0x48, 0x65, 0x69, 0x67, 0x68, 0x74, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x44, 0x61, 0x6D, 0x70, 0x69, 0x6E, 0x67, 0x53, 0x74, 0x69, 0x66, 0x66, 0x6E, 0x65, 0x73, 0x73, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x42, 0x75, 0x6D, 0x70, 0x52, 0x61, 0x74, 0x69, 0x6F, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x41, 0x63, 0x63, 0x65, 0x6C, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x44, 0x65, 0x63, 0x65, 0x6C, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x56, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x65, 0x64, 0x54, 0x75, 0x6E, 0x65, 0x49, 0x64, 0x20, 0x3D, 0x20, 0x27, 0x25, 0x73, 0x27, 0x20, 0x57, 0x48, 0x45, 0x52, 0x45, 0x20, 0x49, 0x64, 0x20, 0x3D, 0x20, 0x25, 0x64, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x55, 0x50, 0x44, 0x41, 0x54, 0x45, 0x20, 0x25, 0x73, 0x43, 0x61, 0x72, 0x65, 0x65, 0x72, 0x5F, 0x47, 0x61, 0x72, 0x61, 0x67, 0x65, 0x20, 0x53, 0x45, 0x54, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x44, 0x6F, 0x77, 0x6E, 0x66, 0x6F, 0x72, 0x63, 0x65, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x44, 0x6F, 0x77, 0x6E, 0x66, 0x6F, 0x72, 0x63, 0x65, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x69, 0x6E, 0x61, 0x6C, 0x44, 0x72, 0x69, 0x76, 0x65, 0x52, 0x61, 0x74, 0x69, 0x6F, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x69, 0x72, 0x73, 0x74, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x73, 0x65, 0x63, 0x6F, 0x6E, 0x64, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x74, 0x68, 0x69, 0x72, 0x64, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x6F, 0x75, 0x72, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x69, 0x66, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x73, 0x69, 0x78, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x73, 0x65, 0x76, 0x65, 0x6E, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x65, 0x69, 0x67, 0x68, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x6E, 0x69, 0x6E, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x74, 0x65, 0x6E, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x62, 0x72, 0x61, 0x6B, 0x65, 0x42, 0x61, 0x6C, 0x61, 0x6E, 0x63, 0x65, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x62, 0x72, 0x61, 0x6B, 0x65, 0x50, 0x72, 0x65, 0x73, 0x73, 0x75, 0x72, 0x65, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x63, 0x65, 0x6E, 0x74, 0x65, 0x72, 0x54, 0x6F, 0x72, 0x71, 0x75, 0x65, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x54, 0x69, 0x72, 0x65, 0x50, 0x72, 0x65, 0x73, 0x73, 0x75, 0x72, 0x65, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x43, 0x61, 0x6D, 0x62, 0x65, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x54, 0x6F, 0x65, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x43, 0x61, 0x73, 0x74, 0x65, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x53, 0x70, 0x72, 0x69, 0x6E, 0x67, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x53, 0x77, 0x61, 0x79, 0x62, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x52, 0x69, 0x64, 0x65, 0x48, 0x65, 0x69, 0x67, 0x68, 0x74, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x44, 0x61, 0x6D, 0x70, 0x69, 0x6E, 0x67, 0x53, 0x74, 0x69, 0x66, 0x66, 0x6E, 0x65, 0x73, 0x73, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x42, 0x75, 0x6D, 0x70, 0x52, 0x61, 0x74, 0x69, 0x6F, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x41, 0x63, 0x63, 0x65, 0x6C, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x44, 0x65, 0x63, 0x65, 0x6C, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x54, 0x69, 0x72, 0x65, 0x50, 0x72, 0x65, 0x73, 0x73, 0x75, 0x72, 0x65, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x43, 0x61, 0x6D, 0x62, 0x65, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x54, 0x6F, 0x65, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x53, 0x70, 0x72, 0x69, 0x6E, 0x67, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x53, 0x77, 0x61, 0x79, 0x62, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E };
                ((Telerik.WinControls.Primitives.BorderPrimitive)FreePerfBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = System.Drawing.Color.FromArgb(45, 45, 48);
                FreeVisBox.Enabled = true;
                if (MainWindow.main.platform == 1)
                {
                    MainWindow.m.WriteBytes("base+4DFD1A0", OriginalData);
                }
                else if (MainWindow.main.platform == 2)
                {
                    MainWindow.m.WriteBytes("base+52DA7E0", OriginalData);
                }
                else if (MainWindow.main.platform == 4)
                {
                    MainWindow.m.WriteBytes("base+630CA30", OriginalData);
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteBytes("base+65C2270", OriginalData);
                }
            }
        }
        private void FreeVisBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (FreeVisBox.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)FreeVisBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = ColorTranslator.FromHtml(MainWindow.ThemeColour);
                FreePerfBox.Enabled = false;

                if (MainWindow.main.platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4DFD1A0", "string", "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ");
                    MainWindow.m.WriteBytes("base+4DFD99F", new byte[] { 0x00 });
                    MainWindow.m.WriteMemory("base+4DFD1A0", "string", "UPDATE List_UpgradeCarBody SET price=0;UPDATE List_UpgradeCarBodyFrontBumper SET price=0;UPDATE List_UpgradeCarBodyHood SET price=0;UPDATE List_UpgradeCarBodyRearBumper SET price=0;UPDATE List_UpgradeCarBodySideSkirt SET price=0;UPDATE List_UpgradeRearWing SET price=0;UPDATE List_Wheels SET price=1");
                }
                else if (MainWindow.main.platform == 2)
                {
                    MainWindow.m.WriteMemory("base+52DA7E0", "string", "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ");
                    MainWindow.m.WriteBytes("base+52DAFDF", new byte[] { 0x00 });
                    MainWindow.m.WriteMemory("base+52DA7E0", "string", "UPDATE List_UpgradeCarBody SET price=0;UPDATE List_UpgradeCarBodyFrontBumper SET price=0;UPDATE List_UpgradeCarBodyHood SET price=0;UPDATE List_UpgradeCarBodyRearBumper SET price=0;UPDATE List_UpgradeCarBodySideSkirt SET price=0;UPDATE List_UpgradeRearWing SET price=0;UPDATE List_Wheels SET price=1");
                }
                else if (MainWindow.main.platform == 4)
                {
                    MainWindow.m.WriteMemory("base+630CA30", "string", "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ");
                    MainWindow.m.WriteBytes("base+630D22F", new byte[] { 0x00 });
                    MainWindow.m.WriteMemory("base+630CA30", "string", "UPDATE List_UpgradeCarBody SET price=0;UPDATE List_UpgradeCarBodyFrontBumper SET price=0;UPDATE List_UpgradeCarBodyHood SET price=0;UPDATE List_UpgradeCarBodyRearBumper SET price=0;UPDATE List_UpgradeCarBodySideSkirt SET price=0;UPDATE List_UpgradeRearWing SET price=0;UPDATE List_Wheels SET price=1");
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteMemory("base+65C2270", "string", "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ");
                    MainWindow.m.WriteBytes("base+65C2A6F", new byte[] { 0x00 });
                    MainWindow.m.WriteMemory("base+65C2270", "string", "UPDATE List_UpgradeCarBody SET price=0;UPDATE List_UpgradeCarBodyFrontBumper SET price=0;UPDATE List_UpgradeCarBodyHood SET price=0;UPDATE List_UpgradeCarBodyRearBumper SET price=0;UPDATE List_UpgradeCarBodySideSkirt SET price=0;UPDATE List_UpgradeRearWing SET price=0;UPDATE List_Wheels SET price=1");
                }
            }
            else if (!FreeVisBox.Checked)
            {
                var OriginalData = new byte[] { 0x55, 0x50, 0x44, 0x41, 0x54, 0x45, 0x20, 0x25, 0x73, 0x43, 0x61, 0x72, 0x65, 0x65, 0x72, 0x5F, 0x47, 0x61, 0x72, 0x61, 0x67, 0x65, 0x20, 0x53, 0x45, 0x54, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x44, 0x6F, 0x77, 0x6E, 0x66, 0x6F, 0x72, 0x63, 0x65, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x44, 0x6F, 0x77, 0x6E, 0x66, 0x6F, 0x72, 0x63, 0x65, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x69, 0x6E, 0x61, 0x6C, 0x44, 0x72, 0x69, 0x76, 0x65, 0x52, 0x61, 0x74, 0x69, 0x6F, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x69, 0x72, 0x73, 0x74, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x73, 0x65, 0x63, 0x6F, 0x6E, 0x64, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x74, 0x68, 0x69, 0x72, 0x64, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x6F, 0x75, 0x72, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x69, 0x66, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x73, 0x69, 0x78, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x73, 0x65, 0x76, 0x65, 0x6E, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x65, 0x69, 0x67, 0x68, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x6E, 0x69, 0x6E, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x74, 0x65, 0x6E, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x62, 0x72, 0x61, 0x6B, 0x65, 0x42, 0x61, 0x6C, 0x61, 0x6E, 0x63, 0x65, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x62, 0x72, 0x61, 0x6B, 0x65, 0x50, 0x72, 0x65, 0x73, 0x73, 0x75, 0x72, 0x65, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x63, 0x65, 0x6E, 0x74, 0x65, 0x72, 0x54, 0x6F, 0x72, 0x71, 0x75, 0x65, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x54, 0x69, 0x72, 0x65, 0x50, 0x72, 0x65, 0x73, 0x73, 0x75, 0x72, 0x65, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x43, 0x61, 0x6D, 0x62, 0x65, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x54, 0x6F, 0x65, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x43, 0x61, 0x73, 0x74, 0x65, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x53, 0x70, 0x72, 0x69, 0x6E, 0x67, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x53, 0x77, 0x61, 0x79, 0x62, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x52, 0x69, 0x64, 0x65, 0x48, 0x65, 0x69, 0x67, 0x68, 0x74, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x44, 0x61, 0x6D, 0x70, 0x69, 0x6E, 0x67, 0x53, 0x74, 0x69, 0x66, 0x66, 0x6E, 0x65, 0x73, 0x73, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x42, 0x75, 0x6D, 0x70, 0x52, 0x61, 0x74, 0x69, 0x6F, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x41, 0x63, 0x63, 0x65, 0x6C, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x44, 0x65, 0x63, 0x65, 0x6C, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x54, 0x69, 0x72, 0x65, 0x50, 0x72, 0x65, 0x73, 0x73, 0x75, 0x72, 0x65, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x43, 0x61, 0x6D, 0x62, 0x65, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x54, 0x6F, 0x65, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x53, 0x70, 0x72, 0x69, 0x6E, 0x67, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x53, 0x77, 0x61, 0x79, 0x62, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x52, 0x69, 0x64, 0x65, 0x48, 0x65, 0x69, 0x67, 0x68, 0x74, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x44, 0x61, 0x6D, 0x70, 0x69, 0x6E, 0x67, 0x53, 0x74, 0x69, 0x66, 0x66, 0x6E, 0x65, 0x73, 0x73, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x42, 0x75, 0x6D, 0x70, 0x52, 0x61, 0x74, 0x69, 0x6F, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x41, 0x63, 0x63, 0x65, 0x6C, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x44, 0x65, 0x63, 0x65, 0x6C, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x56, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x65, 0x64, 0x54, 0x75, 0x6E, 0x65, 0x49, 0x64, 0x20, 0x3D, 0x20, 0x27, 0x25, 0x73, 0x27, 0x20, 0x57, 0x48, 0x45, 0x52, 0x45, 0x20, 0x49, 0x64, 0x20, 0x3D, 0x20, 0x25, 0x64, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x55, 0x50, 0x44, 0x41, 0x54, 0x45, 0x20, 0x25, 0x73, 0x43, 0x61, 0x72, 0x65, 0x65, 0x72, 0x5F, 0x47, 0x61, 0x72, 0x61, 0x67, 0x65, 0x20, 0x53, 0x45, 0x54, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x44, 0x6F, 0x77, 0x6E, 0x66, 0x6F, 0x72, 0x63, 0x65, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x44, 0x6F, 0x77, 0x6E, 0x66, 0x6F, 0x72, 0x63, 0x65, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x69, 0x6E, 0x61, 0x6C, 0x44, 0x72, 0x69, 0x76, 0x65, 0x52, 0x61, 0x74, 0x69, 0x6F, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x69, 0x72, 0x73, 0x74, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x73, 0x65, 0x63, 0x6F, 0x6E, 0x64, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x74, 0x68, 0x69, 0x72, 0x64, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x6F, 0x75, 0x72, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x69, 0x66, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x73, 0x69, 0x78, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x73, 0x65, 0x76, 0x65, 0x6E, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x65, 0x69, 0x67, 0x68, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x6E, 0x69, 0x6E, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x74, 0x65, 0x6E, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x62, 0x72, 0x61, 0x6B, 0x65, 0x42, 0x61, 0x6C, 0x61, 0x6E, 0x63, 0x65, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x62, 0x72, 0x61, 0x6B, 0x65, 0x50, 0x72, 0x65, 0x73, 0x73, 0x75, 0x72, 0x65, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x63, 0x65, 0x6E, 0x74, 0x65, 0x72, 0x54, 0x6F, 0x72, 0x71, 0x75, 0x65, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x54, 0x69, 0x72, 0x65, 0x50, 0x72, 0x65, 0x73, 0x73, 0x75, 0x72, 0x65, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x43, 0x61, 0x6D, 0x62, 0x65, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x54, 0x6F, 0x65, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x43, 0x61, 0x73, 0x74, 0x65, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x53, 0x70, 0x72, 0x69, 0x6E, 0x67, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x53, 0x77, 0x61, 0x79, 0x62, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x52, 0x69, 0x64, 0x65, 0x48, 0x65, 0x69, 0x67, 0x68, 0x74, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x44, 0x61, 0x6D, 0x70, 0x69, 0x6E, 0x67, 0x53, 0x74, 0x69, 0x66, 0x66, 0x6E, 0x65, 0x73, 0x73, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x42, 0x75, 0x6D, 0x70, 0x52, 0x61, 0x74, 0x69, 0x6F, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x41, 0x63, 0x63, 0x65, 0x6C, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x44, 0x65, 0x63, 0x65, 0x6C, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x54, 0x69, 0x72, 0x65, 0x50, 0x72, 0x65, 0x73, 0x73, 0x75, 0x72, 0x65, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x43, 0x61, 0x6D, 0x62, 0x65, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x54, 0x6F, 0x65, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x53, 0x70, 0x72, 0x69, 0x6E, 0x67, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x53, 0x77, 0x61, 0x79, 0x62, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E };
                ((Telerik.WinControls.Primitives.BorderPrimitive)FreeVisBox.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = System.Drawing.Color.FromArgb(45, 45, 48);
                FreePerfBox.Enabled = true;

                if (MainWindow.main.platform == 1)
                {
                    MainWindow.m.WriteBytes("base+4DFD1A0", OriginalData);
                }
                else if (MainWindow.main.platform == 2)
                {
                    MainWindow.m.WriteBytes("base+52DA7E0", OriginalData);
                }
                else if (MainWindow.main.platform == 4)
                {
                    MainWindow.m.WriteBytes("base+630CA30", OriginalData);
                }
                else if (MainWindow.main.platform == 5)
                {
                    MainWindow.m.WriteBytes("base+65C2270", OriginalData);
                }
            }
        }

        private void QuickAdd_Click(object sender, EventArgs e)
        {
            QuickAddCars.StartPosition = FormStartPosition.CenterParent;
            QuickAddCars.Show();
            QuickAddCars.Focus();
        }
    }
}
