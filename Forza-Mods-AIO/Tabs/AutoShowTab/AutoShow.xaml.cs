using System;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;
using Forza_Mods_AIO.Resources;

using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Tabs.AutoShowTab.AutoshowVars;

namespace Forza_Mods_AIO.Tabs.AutoShowTab;

/// <summary>
///     Interaction logic for AutoShow.xaml
/// </summary>
public partial class AutoShow
{
    #region Variables

    public static AutoShow As { get; private set; } = null!;
    public readonly UiManager UiManager;
    private string _clearGarageString = "DELETE FROM Profile0_Career_Garage WHERE Id > 0;";
    private const string FreePerfString = "UPDATE List_UpgradeAntiSwayFront SET price=0;UPDATE List_UpgradeAntiSwayRear SET price=0;UPDATE List_UpgradeBrakes SET price=0;UPDATE List_UpgradeCarBodyChassisStiffness SET price=0;UPDATE List_UpgradeCarBody SET price=0;UPDATE List_UpgradeCarBodyTireWidthFront SET price=0;UPDATE List_UpgradeCarBodyTireWidthRear SET price=0;UPDATE List_UpgradeCarBodyTrackSpacingFront SET price=0;UPDATE List_UpgradeCarBodyTrackSpacingRear SET price=0;UPDATE List_UpgradeCarBodyWeight SET price=0;UPDATE List_UpgradeDrivetrain SET price=0;UPDATE List_UpgradeDrivetrainClutch SET price=0;UPDATE List_UpgradeDrivetrainDifferential  SET price=0;UPDATE List_UpgradeDrivetrainDriveline SET price=0;UPDATE List_UpgradeDrivetrainTransmission SET price=0;UPDATE List_UpgradeEngine SET price=0;UPDATE List_UpgradeEngineCamshaft SET price=0;UPDATE List_UpgradeEngineCSC SET price=0;UPDATE List_UpgradeEngineDisplacement SET price=0;UPDATE List_UpgradeEngineDSC SET price=0;UPDATE List_UpgradeEngineExhaust SET price=0;UPDATE List_UpgradeEngineFlywheel SET price=0;UPDATE List_UpgradeEngineFuelSystem SET price=0;UPDATE List_UpgradeEngineIgnition SET price=0;UPDATE List_UpgradeEngineIntake SET price=0;UPDATE List_UpgradeEngineIntercooler SET price=0;UPDATE List_UpgradeEngineManifold SET price=0;UPDATE List_UpgradeEngineOilCooling SET price=0;UPDATE List_UpgradeEnginePistonsCompression SET price=0;UPDATE List_UpgradeEngineRestrictorPlate SET price=0;UPDATE List_UpgradeEngineTurboQuad SET price=0;UPDATE List_UpgradeEngineTurboSingle SET price=0;UPDATE List_UpgradeEngineTurboTwin SET price=0;UPDATE List_UpgradeEngineValves SET price=0;UPDATE List_UpgradeMotor SET price=0;UPDATE List_UpgradeMotorParts SET price=0;UPDATE List_UpgradeSpringDamper SET price=0;UPDATE List_UpgradeTireCompound SET price=0;UPDATE List_Wheels SET price=1;";
    private const string FreeVisualString = "UPDATE List_UpgradeCarBody SET price=0;UPDATE List_UpgradeCarBodyFrontBumper SET price=0;UPDATE List_UpgradeCarBodyHood SET price=0;UPDATE List_UpgradeCarBodyRearBumper SET price=0;UPDATE List_UpgradeCarBodySideSkirt SET price=0;UPDATE List_UpgradeRearWing SET price=0;UPDATE List_Wheels SET price=1;";
    private const string ShowTrafficHsNullString = "DROP VIEW Drivable_Data_Car; CREATE VIEW Drivable_Data_Car AS SELECT Data_Car.* FROM Data_Car; INSERT INTO Data_Car_Buckets(CarId) SELECT Id FROM Data_Car WHERE Id NOT IN (SELECT CarId FROM Data_Car_Buckets); UPDATE Data_Car_Buckets SET CarBucket=0, BucketHero=0 WHERE CarBucket IS NULL";
    private const string ShowTrafficHsNullRevertString = "UPDATE Data_Car_Buckets SET CarBucket=NULL, BucketHero=NULL WHERE CarBucket=0 AND BucketHero=0; DELETE FROM Data_Car_Buckets WHERE CarId IN (SELECT Id FROM Data_Car WHERE Id NOT IN (SELECT CarId FROM Data_Car_Buckets)); DROP VIEW Drivable_Data_Car;";
    private const string FixThumbnailsString = "UPDATE Profile0_Career_Garage SET Thumbnail=(SELECT Thumbnail FROM Data_Car WHERE Data_Car.Id = Profile0_Career_Garage.CarId); UPDATE Profile0_Career_Garage; UPDATE Profile0_Career_Garage SET NumOwners=69";
    private const string ClearTagString = "UPDATE Profile0_Career_Garage SET HasCurrentOwnerViewedCar = 1;";
    private const string UnlockPresetsString = "CREATE TABLE UpgradePresetPackagesOrig AS SELECT * FROM UpgradePresetPackages; UPDATE UpgradePresetPackages SET Purchasable = 1 WHERE Purchasable = 0;";
    private const string UnlockPresetsRevertString = "UPDATE UpgradePresetPackages SET Purchasable = (SELECT Purchasable FROM UpgradePresetPackagesOrig WHERE UpgradePresetPackages.Id == UpgradePresetPackagesOrig.Id); DROP TABLE UpgradePresetPackagesOrig;";
    
    #endregion
    
    public AutoShow()
    {
        InitializeComponent();
        As = this;
        
        UiManager = new UiManager(this, AobProgressBar);
        UiManager.ToggleUiElements(false);
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached || ScanButton == null)
        {
            return;
        }

        Task.Run(() => Scan());
        ScanButton.IsEnabled = false;
    }

    private void AllCars_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached || ToggleAllCars == null)
        {
            return;
        }

        ToggleRareCars.IsEnabled = !ToggleRareCars.IsEnabled;
        switch (ToggleAllCars.IsOn)
        {
            case true:
            {
                ExecSql(sender, AllCars_OnToggled, "CREATE TABLE AutoshowTable(Id INT, NotAvailableInAutoshow INT); INSERT INTO AutoshowTable SELECT Id, NotAvailableInAutoshow FROM Data_Car; UPDATE Data_Car SET NotAvailableInAutoshow = 0;");
                break;
            }
            case false:
            {
                ExecSql(sender, AllCars_OnToggled, "UPDATE Data_Car SET NotAvailableInAutoshow = (SELECT NotAvailableInAutoshow FROM AutoshowTable WHERE Data_Car.Id == AutoshowTable.Id); DROP TABLE AutoshowTable;");
                break;
            }
        }
    }

    private void RareCars_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached || ToggleRareCars == null)
        {
            return;
        }

        ToggleAllCars.IsEnabled = !ToggleAllCars.IsEnabled;
            
        switch (ToggleRareCars.IsOn)
        {
            case true:
            {
                ExecSql(sender, RareCars_OnToggled, "CREATE TABLE AutoshowTable(Id INT, NotAvailableInAutoshow INT); INSERT INTO AutoshowTable SELECT Id, NotAvailableInAutoshow FROM Data_Car; UPDATE Data_Car SET NotAvailableInAutoshow = (NotAvailableInAutoshow-1)* -1;");
                break;
            }
            case false:
            {
                ExecSql(sender, RareCars_OnToggled, "UPDATE Data_Car SET NotAvailableInAutoshow = (SELECT NotAvailableInAutoshow FROM AutoshowTable WHERE Data_Car.Id == AutoshowTable.Id); DROP TABLE AutoshowTable;");
                break;
            }
        }
    }

    private void FreeCars_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached || ToggleFreeCars == null)
        {
            return;
        }
            
        switch (ToggleFreeCars.IsOn)
        {
            case true:
            {
                ExecSql(sender, FreeCars_OnToggled ,"CREATE TABLE CostTable(Id INT, BaseCost INT); INSERT INTO CostTable(Id, BaseCost) SELECT Id, BaseCost FROM Data_car; UPDATE Data_Car SET BaseCost = 0;");
                break;
            }
            case false:
            {
                ExecSql(sender, FreeCars_OnToggled ,"UPDATE Data_Car SET BaseCost = (SELECT BaseCost FROM CostTable WHERE Id = Data_Car.Id); DROP TABLE CostTable;");
                break;
            }
        }
    }


    private void RemoveAnyCar_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached || RemoveAnyCar == null)
        {
            return;
        }
        
        switch (RemoveAnyCar.IsOn)
        {
            case true:
            {
                ExecSql(sender, FreeCars_OnToggled ,"CREATE TABLE BarnFindsTable AS SELECT * FROM Profile0_BarnFinds; DELETE FROM Profile0_BarnFinds;");
                break;
            }
            case false:
            {
                ExecSql(sender, FreeCars_OnToggled ,"INSERT INTO Profile0_BarnFinds SELECT * FROM BarnFindsTable; DROP TABLE BarnFindsTable;");
                break;
            }
        }
    }

    private void ClearGarage_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached || ClearGarage == null)
        {
            return;
        }

        ExecSql(sender, ClearGarage_OnToggled, _clearGarageString);
    }

    public void FixThumbnails_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached || FixThumbnails == null)
        {
            return;
        }
            
        ExecSql(sender, FixThumbnails_OnToggled, FixThumbnailsString);
    }

    public void AddRareCars_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached || QuickAddRareCars == null)
        {
            return;
        }

        var addCarsString = "INSERT INTO ContentOffersMapping (OfferId, ContentId, ContentType, IsPromo, IsAutoRedeem, ReleaseDateUTC, Quantity) SELECT 3, Id, 1, 0, 1, NULL, 1 FROM Data_Car WHERE Id NOT IN (SELECT ContentId AS Id FROM ContentOffersMapping WHERE ContentId IS NOT NULL);" +
                            " INSERT INTO Profile0_FreeCars SELECT Id, 1 FROM Data_Car WHERE Id NOT IN (SELECT CarId AS Id FROM Profile0_FreeCars WHERE CarID IS NOT NULL);" +
                            " UPDATE ContentOffersMapping SET Quantity = 9999 ;" +
                            " UPDATE Profile0_FreeCars SET FreeCount = 1;" +
                            " UPDATE ContentOffersMapping SET IsAutoRedeem = 1;" +
                            " UPDATE ContentOffersMapping SET IsAutoRedeem = 0 WHERE ContentId IN(\"3300\");" +
                            " UPDATE ContentOffersMapping SET IsAutoRedeem = 0 WHERE ContentId IN(SELECT Id AS ContentId FROM Data_Car WHERE NotAvailableInAutoshow = 0);" +
                            " UPDATE ContentOffersMapping SET IsAutoRedeem = 0 WHERE ContentId IN(SELECT ContentId FROM ContentOffersMapping WHERE ReleaseDateUTC > '" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00' OR ReleaseDateUTC IS NULL)" ;
            
        ExecSql(sender, AddRareCars_OnToggled, addCarsString);
    }

    public void AddAllCars_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached || QuickAddAllCars == null)
        {
            return;
        }

        var addCarsString = "INSERT INTO ContentOffersMapping (OfferId, ContentId, ContentType, IsPromo, IsAutoRedeem, ReleaseDateUTC, Quantity) SELECT 3, Id, 1, 0, 1, NULL, 1 FROM Data_Car WHERE Id NOT IN (SELECT ContentId AS Id FROM ContentOffersMapping WHERE ContentId IS NOT NULL);" +
                            " INSERT INTO Profile0_FreeCars SELECT ContentId, 1 FROM ContentOffersMapping;" +
                            " UPDATE ContentOffersMapping SET IsAutoRedeem = 1 WHERE ContentId NOT IN(SELECT ContentId FROM ContentOffersMapping WHERE ReleaseDateUTC > '" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00' OR ReleaseDateUTC IS NULL)" +
                            " UPDATE ContentOffersMapping SET Quantity = 1;" +
                            " UPDATE ContentOffersMapping SET IsAutoRedeem = 0 WHERE ContentId IN(\"3300\");" +
                            " UPDATE ContentOffersMapping SET IsAutoRedeem = 0 WHERE ContentId IN(SELECT CarId AS ContentId FROM Profile0_Career_Garage WHERE CarId IS NOT NULL);";
            
        ExecSql(sender, AddAllCars_OnToggled, addCarsString);
    }

    public void FreeVisualUpgrades_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached || FreeVisualUpgrades == null)
        {
            return;
        }

        ExecSql(sender,FreeVisualUpgrades_OnToggled, FreeVisualString);
    }

    public void FreePerfUpgrades_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached || FreePerfUpgrades == null)
        {
            return;
        }

        ExecSql(sender, FreePerfUpgrades_OnToggled, FreePerfString);
    }

    private void ShowTrafficHSNull_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached || ShowTrafficHsNull == null)
        {
            return;
        }
            
        switch (ShowTrafficHsNull.IsEnabled)
        {
            case true:
            {
                ExecSql(sender, ShowTrafficHSNull_OnToggled, ShowTrafficHsNullString);
                break;
            }
            case false:
            {                
                ExecSql(sender, ShowTrafficHSNull_OnToggled, ShowTrafficHsNullRevertString);
                break;
            }
        }
    }

    private void UnlockHiddenPresets_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached || UnlockHiddenPresets == null)
        {
            return;
        }
            
        switch (UnlockHiddenPresets.IsEnabled)
        {
            case true:
            {
                ExecSql(sender, UnlockHiddenPresets_OnToggled, UnlockPresetsString);
                break;
            }
            case false:
            {
                ExecSql(sender, UnlockHiddenPresets_OnToggled, UnlockPresetsRevertString);
                break;
            }
        }
    }

    private void ClearGarageBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ClearGarageBox == null)
        {
            return;
        }
        
        _clearGarageString = ClearGarageBox.SelectedIndex switch
        {
            0 => // All
                "DELETE FROM Profile0_Career_Garage WHERE Id > 0;",
            1 => // Dupes
                "DELETE FROM Profile0_Career_Garage WHERE Id NOT IN (select min(Id) from Profile0_Career_Garage group by CarId);",
            2 => // Non favorites
                "DELETE FROM Profile0_Career_Garage WHERE IsFavorite IS NOT 1;",
            3 => // Rare cars
                "DELETE FROM Profile0_Career_Garage WHERE CarId NOT IN (SELECT Id FROM Data_Car WHERE NotAvailableInAutoshow = 0);",
            4 => // Autoshow cars
                "DELETE FROM Profile0_Career_Garage WHERE CarId NOT IN (SELECT Id FROM Data_Car WHERE NotAvailableInAutoshow = 1);",
            5 => // Only untuned
                "DELETE FROM Profile0_Career_Garage WHERE VersionedTuneId IS \"00000000-0000-0000-0000-000000000000\";",
            6 => // Only unpainted
                "DELETE FROM Profile0_Career_Garage WHERE VersionedLiveryId IS \"00000000-0000-0000-0000-000000000000\";",
            _ => _clearGarageString
        };
    }

    public void ClearTag_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }
            
        ExecSql(sender, ClearTag_OnToggled, ClearTagString);
    }
}