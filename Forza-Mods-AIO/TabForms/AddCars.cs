﻿using System;
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
        //1 = ms store
        //2 = steam
        //3 = default
        int platform = 3;
        public TabForms.PopupForms.AddCarsGuide AddCarsGuide = new TabForms.PopupForms.AddCarsGuide();
        private void AddCars_Load(object sender, EventArgs e)
        {

        }

        private void AddCars_Shown(object sender, EventArgs e)
        {
            var TargetProcess = Process.GetProcessesByName("ForzaHorizon4")[0];
            if (TargetProcess.MainModule.FileName.Contains("Microsoft.SunriseBaseGame"))
            {
                platform = 1;
            }
            else
            {
                Box_FreeCars.Enabled = false;
                Box_Presets.Enabled = false;
                Box_ThumbsFix.Enabled = false;
                Box_Traffic.Enabled = false;
                Box_AllCars.Enabled = false;
                Box_RareCars.Enabled = false;
                Box_SeriesFix.Enabled = false;
                Box_LegoPaint.Enabled = false;
                Box_Null.Enabled = false;
                Box_ClearGarage.Enabled = false;


                MessageBox.Show("This Tab does not support the Steam release yet", "Steam", MessageBoxButtons.OK, MessageBoxIcon.Error);
                platform = 2;
            }

        }
        private void Box_AllCars_CheckedChanged(object sender, EventArgs e)
        {
            if (Box_AllCars.Checked)
            {
                Box_Null.Enabled = false;
                Box_RareCars.Enabled = false;
                if (platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4C90FC1", "string", "                  ");
                    MainWindow.m.WriteMemory("base+4C90FA9", "string", "                     ");
                    MainWindow.m.WriteMemory("base+4C91011", "string", "                            ");
                    MainWindow.m.WriteMemory("base+4C91059", "string", "                                           ");

                }
                else if (platform == 2)
                {
                }
            }
            else if (!Box_AllCars.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_AllCars.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = System.Drawing.Color.FromArgb(45, 45, 48);
                Box_Null.Enabled = true;
                Box_RareCars.Enabled = true;
                if (platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4C90FC1", "string", "AND NOT IsBarnFind");
                    MainWindow.m.WriteMemory("base+4C90FA9", "string", "AND NOT IsMidnightCar");
                    MainWindow.m.WriteMemory("base+4C91011", "string", "AND NotAvailableInAutoshow=0");
                    MainWindow.m.WriteMemory("base+4C91059", "string", "AND IsCarVisibleAndReleased(Garage.ModelId)");
                }
                else if (platform == 2)
                {
                }
            }
        }

        private void Box_RareCars_CheckedChanged(object sender, EventArgs e)
        {
            if (Box_RareCars.Checked)
            {
                Box_Null.Enabled = false;
                Box_AllCars.Enabled = false;
                if (platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4C90FC1", "string", "                  ");
                    MainWindow.m.WriteMemory("base+4C90FA9", "string", "                     ");
                    MainWindow.m.WriteMemory("base+4C9102B", "string", "=1                                    ");
                }
                else if (platform == 2)
                {
                }
            }
            else if (!Box_RareCars.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_RareCars.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = System.Drawing.Color.FromArgb(45, 45, 48);
                Box_Null.Enabled = true;
                Box_AllCars.Enabled = true;
                if (platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4C90FC1", "string", "AND NOT IsBarnFind");
                    MainWindow.m.WriteMemory("base+4C90FA9", "string", "AND NOT IsMidnightCar");
                    MainWindow.m.WriteMemory("base+4C9102B", "string", "=0                                    ");
                }
                else if (platform == 2)
                {
                }
            }
        }
        private void Box_Null_CheckedChanged(object sender, EventArgs e)
        {
            if (Box_Null.Checked)
            {
                Box_AllCars.Enabled = false;
                Box_RareCars.Enabled = false;
                if (platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4C91849", "string", "           1215=");
                    MainWindow.m.WriteMemory("base+4C90FD9", "string", "      1215=");
                    MainWindow.m.WriteMemory("base+4C91011", "string", "AND Garage.Id=1215          ");
                }
                else if (platform == 2)
                {
                }
            }
            else if (!Box_Null.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_Null.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = System.Drawing.Color.FromArgb(45, 45, 48);
                Box_AllCars.Enabled = true;
                Box_RareCars.Enabled = true;
                if (platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4C91849", "string", "Garage.ModelId!=");
                    MainWindow.m.WriteMemory("base+4C90FD9", "string", "Garage.Id!=");
                    MainWindow.m.WriteMemory("base+4C91011", "string", "AND NotAvailableInAutoshow=0");
                }
                else if (platform == 2)
                {
                }
            }
        }

        private void Box_LegoPaint_CheckedChanged(object sender, EventArgs e)
        {
            //4DE4FC7
            if (Box_LegoPaint.Checked)
            {
                if (platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4DE4FC7", "string", "b");
                }
                else if (platform == 2)
                {
                }
            }
            else if (!Box_LegoPaint.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_LegoPaint.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = System.Drawing.Color.FromArgb(45, 45, 48);
                if (platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4DE4FC7", "string", "H");
                }
                else if (platform == 2)
                {
                }
            }
        }
        private void Box_RemoveCars_CheckedChanged(object sender, EventArgs e)
        {
            if (Box_RemoveCars.Checked)
            {
                if (platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4CD7AE0", "string", "b");
                    MainWindow.m.WriteMemory("base+4C690C8", "string", "b");
                }
                else if (platform == 2)
                {
                }
            }
            else if (!Box_RemoveCars.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_RemoveCars.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = System.Drawing.Color.FromArgb(45, 45, 48);
                if (platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4CD7AE0", "string", "D");
                    MainWindow.m.WriteMemory("base+4C690C8", "string", "I");
                }
                else if (platform == 2)
                {
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

                if (platform==1)
                {
                    MainWindow.m.WriteMemory("base+4DDBD20", "string", "UPDATE Data_Car SET BaseCost = 0 WHERE BaseCost >0                                                                                                                                                                                                                                                                                                                         ");
                }
                else if (platform==2)
                {

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

                if (platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4DDBD20", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");
                }
                else if (platform == 2)
                {

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
                if (platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4DDBD20", "string", "UPDATE Profile0_Career_Garage SET LiveryFileName='', VersionedLiveryId='00000000-0000-0000-0000-000000000000'; UPDATE Profile0_Career_Garage SET OriginalOwner='r/ForzaModding'; UPDATE Profile0_Career_Garage SET NumOwners=69                                                                                                                                            ");
                }
                else if (platform == 2)
                {
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

                if (platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4DDBD20", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");
                }
                else if (platform == 2)
                {
                }
            }
        }



        private void Box_ThumbsFix_CheckedChanged(object sender, EventArgs e)
        {
            if(Box_ThumbsFix.Checked)
            {
                Box_FreeCars.Enabled = false;
                Box_Presets.Enabled = false;
                Box_SeriesFix.Enabled = false;
                Box_Traffic.Enabled = false;

                Box_ClearGarage.Enabled = false;
                if (platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4DDBD20", "string", "UPDATE Profile0_Career_Garage SET Thumbnail=(SELECT Thumbnail FROM Data_Car WHERE Data_Car.Id = Profile0_Career_Garage.CarId); UPDATE Profile0_Career_Garage SET OriginalOwner='r/ForzaModding'; UPDATE Profile0_Career_Garage SET NumOwners=69                                                                                                                            ");
                }
                else if (platform == 2)
                {

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

                if (platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4DDBD20", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");
                }
                else if (platform == 2)
                {

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
                if (platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4DDBD20", "string", "DROP VIEW Drivable_Data_Car; CREATE VIEW Drivable_Data_Car AS SELECT Data_Car.* FROM Data_Car; INSERT INTO Data_Car_Buckets(CarId) SELECT Id FROM Data_Car WHERE Id NOT IN (SELECT CarId FROM Data_Car_Buckets); UPDATE Data_Car_Buckets SET CarBucket=0, BucketHero=0 WHERE CarBucket IS NULL                                                                             ");
                }
                else if (platform == 2)
                {
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

                if (platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4DDBD20", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");
                }
                else if (platform == 2)
                {
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
                if (platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4DDBD20", "string", "UPDATE UpgradePresetPackages SET Purchasable=1 WHERE Purchasable=0                                                                                                                                                                                                                                                                                                         ");
                }
                else if (platform == 2)
                {
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
                if (platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4DDBD20", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");
                }
                else if (platform == 2)
                {
                }
            }
        }

        private void Box_Decals_CheckedChanged(object sender, EventArgs e)
        {
            if (Box_Decals.Checked)
            {
                if (platform == 1)
                {

                    MainWindow.m.WriteMemory("base+4CFF609", "string", "WHERE Id >=0 ORDER BY Id                                                                                                                      ");

                }
                else if (platform == 2)
                {
                }
            }
            else if (!Box_Decals.Checked)
            {
                ((Telerik.WinControls.Primitives.BorderPrimitive)Box_Decals.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(1)).ForeColor = System.Drawing.Color.FromArgb(45, 45, 48);
                if (platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4CFF609", "string", "INNER JOIN Livery_DecalsSortOrder ON (Livery_Decals.ID = Livery_DecalsSortOrder.Livery_DecalID) WHERE MakeID = %d ORDER BY Sequence, AlphaSort");
                }
                else if (platform == 2)
                {
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

                if (platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4DDBD20", "string", "DELETE FROM Profile0_Career_Garage WHERE Id > 0                                                                                                                                                                                                                                                                                                                            ");
                }
                else if (platform == 2)
                {
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

                if (platform == 1)
                {
                    MainWindow.m.WriteMemory("base+4DDBD20", "string", "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");
                }
                else if (platform == 2)
                {
                }
            }
        }

        private void BTN_Help_Click(object sender, EventArgs e)
        {
            AddCarsGuide.Show();
        }
    }
}
