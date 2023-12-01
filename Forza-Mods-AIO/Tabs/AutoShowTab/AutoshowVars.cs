using System;
using System.Linq;
using System.Windows;
using System.IO.Pipes;
using System.IO;
using System.Threading.Tasks;
using Lunar;
using MahApps.Metro.Controls;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Overlay.AutoShowMenu.SubMenus.GarageModifications;
using static Forza_Mods_AIO.Overlay.Overlay;
using static Forza_Mods_AIO.Resources.UiManager;
using static Forza_Mods_AIO.Tabs.AutoShowTab.AutoShow;

namespace Forza_Mods_AIO.Tabs.AutoShowTab;

internal class AutoshowVars
{
    #region Variables
    public static UIntPtr Sql1;  //FH4 NOT Garage.NotAvailableInAutoshow AS PurchasableCar,
    public static UIntPtr Sql2;  // IsBarnFind
    public static UIntPtr Sql3;  // AND NOT IsBarnFind 
    public static UIntPtr Sql4;  // Garage.Id!=
    public static UIntPtr Sql5;  //FH4 AND NotAvailableInAutoshow=0
    public static UIntPtr Sql6;  //FH4 (basically just above address + 26)
    public static UIntPtr Sql7;  // AND IsCarVisibleAndReleased(Garage.ModelId) 
    public static UIntPtr Sql8;  // Garage.ModelId!=
    public static UIntPtr Sql9;  // AND UnobtainableCars.Ordinal IS NULL
    public static UIntPtr Sql10; // DoNotAllowRemovalFromGarage
    public static UIntPtr Sql11; // UPDATE %s SET TopSpeed=%f
    public static UIntPtr Sql12; // UPDATE %sCareer_Garage SET Tuning_frontDownforce = %1.8e, Tuning_rearDownforce = %1.8e,
    public static UIntPtr Sql13; // sql12 but +2047
    public static UIntPtr Sql14; //FH4 HideNormalColors for lego cars
    public static UIntPtr Sql15; //FH4 HideSpecialColors for lego cars
    public static UIntPtr Sql16; //FH4 AND NOT IsMidnightCar 

    private const string Sql1Aob = @"4E 4F 54 20 47 61 72 61 67 65 2E 4E 6F 74 41 76 61 69 6C 61 62 6C 65 49 6E 41 75 74 6F 73 68 6F 77 20 41 53 20 50 75 72 63 68 61 73 61 62 6C 65 43 61 72 2C";
    private const string Sql2Aob = @"49 73 42 61 72 6E 46 69 6E 64";
    private const string Sql3Aob = @"41 4E 44 20 4E 4F 54 20 49 73 42 61 72 6E 46 69 6E 64";
    private const string Sql4Aob = @"47 61 72 61 67 65 2E 49 64 21 3D";
    private const string Sql7Aob = @"41 4E 44 20 49 73 43 61 72 56 69 73 69 62 6C 65 41 6E 64 52 65 6C 65 61 73 65 64 28 47 61 72 61 67 65 2E 4D 6F 64 65 6C 49 64 29 00 00 00 00 20";
    private const string Sql8Aob = @"47 61 72 61 67 65 2E 4D 6F 64 65 6C 49 64 21 3D";
    private const string Sql9Aob = @"41 4E 44 20 55 6E 6F 62 74 61 69 6E 61 62 6C 65 43 61 72 73 2E 4F 72 64 69 6E 61 6C 20 49 53 20 4E 55 4C 4C";
    private const string Sql10Aob = @"44 6F 4E 6F 74 41 6C 6C 6F 77 52 65 6D 6F 76 61 6C 46 72 6F 6D 47 61 72 61 67 65 00 00 00 00 00";
    private const string Sql11Aob = @"55 50 44 41 54 45 20 25 73 20 53 45 54 20 54 6F 70 53 70 65 65 64 3D 25 66";
    private const string Sql14Aob = @"48 69 64 65 4E 6F 72 6D 61 6C 43 6F 6C 6F 72 73 00 00 00 00 00 00 00 00 48 69 64 65 4D 61 6E 75 66 61 63 74 75 72 65 72 43 6F 6C 6F 72 73 00 00";
    private const string Sql15Aob = @"48 69 64 65 53 70 65 63 69 61 6C 43 6F 6C 6F 72 73 00 00 00 00 00 00 00 41 6C 6C 6F 77 53 74 6F 63 6B 4D 61 6E 75 66 61 63 74 75 72 65 72 43 6F 6C 6F 72 73 46 6F 72 57 68 65 65 6C 73";
    private const string Sql16Aob = @"41 4E 44 20 4E 4F 54 20 49 73 4D 69 64 6E 69 67 68 74 43 61 72 00 00 20 41 4E 44 20 4E 4F 54 20 49 73 42 61 72 6E 46 69 6E 64 00 00 00 00 00 20";
        
    private const string SqlBase1Aob = @"55 50 44 41 54 45 20 25 73 43 61 72 65 65 72 5F 47 61 72 61 67 65 20 53 45 54 20 54 75 6E 69 6E 67 5F 66 72 6F 6E 74 44 6F 77 6E 66 6F 72 63 65 20 3D 20 25 31 2E 38 65 2C 20 54 75 6E 69 6E 67 5F 72 65 61 72 44 6F 77 6E 66 6F 72 63 65 20 3D 20 25 31 2E 38 65 2C";
    private const string SqlBase2Aob = @"41 4E 44 20 4E 6F 74 41 76 61 69 6C 61 62 6C 65 49 6E 41 75 74 6F 73 68 6F 77 3D 30";
    #endregion

    public static void Scan()
    {
        var scanIndex = 0;
            
        if (Mw.Gvp.Name == "Forza Horizon 4")
        {
            As.UiManager.Index = 0;
            As.UiManager.ScanAmount = 15;
                
            Sql2 = Mw.M.ScanForSig(Sql2Aob).FirstOrDefault();
            As.UiManager.AddProgress();
                
            Sql3 = Mw.M.ScanForSig(Sql3Aob).FirstOrDefault();
            As.UiManager.AddProgress();
                
            Sql4 = Mw.M.ScanForSig(Sql4Aob).FirstOrDefault();
            As.UiManager.AddProgress();
                
            Sql7 = Mw.M.ScanForSig(Sql7Aob).FirstOrDefault();
            As.UiManager.AddProgress();
                
            Sql8 = Mw.M.ScanForSig(Sql8Aob).FirstOrDefault();
            As.UiManager.AddProgress();
                
            Sql9 = Mw.M.ScanForSig(Sql9Aob).FirstOrDefault();
            As.UiManager.AddProgress();
                
            Sql10 = Mw.M.ScanForSig(Sql10Aob).FirstOrDefault();
            As.UiManager.AddProgress();
                
            Sql11 = Mw.M.ScanForSig(Sql11Aob).FirstOrDefault();
            As.UiManager.AddProgress();
                
            var sqlBase1 = Mw.M.ScanForSig(SqlBase1Aob);
            var enumerable1 = sqlBase1 as UIntPtr[] ?? sqlBase1.ToArray();
            Sql12 = enumerable1.FirstOrDefault();
            Sql13 = enumerable1.FirstOrDefault() + 2047;
            As.UiManager.AddProgress();
                
            Sql1 = Mw.M.ScanForSig(Sql1Aob).FirstOrDefault();
            As.UiManager.AddProgress();
                
            var sqlBase2 = Mw.M.ScanForSig(SqlBase2Aob);
            var enumerable2 = sqlBase2 as UIntPtr[] ?? sqlBase2.ToArray();
            Sql5 = enumerable2.FirstOrDefault();
            Sql6 = enumerable2.FirstOrDefault() + 26;
            As.UiManager.AddProgress();
                
            Sql14 = Mw.M.ScanForSig(Sql14Aob).FirstOrDefault();
            As.UiManager.AddProgress();
                
            Sql15 = Mw.M.ScanForSig(Sql15Aob).FirstOrDefault() + 2047;
            As.UiManager.AddProgress();
                
            Sql16 = Mw.M.ScanForSig(Sql16Aob).FirstOrDefault();
            As.UiManager.AddProgress();
                
            AutoshowGarageOption.IsEnabled = PaintLegoCarsToggle.IsEnabled = true;
            As.UiManager.ToggleUiElements(true);
            return;
        }
        
        As.UiManager.Index = 0;
        As.UiManager.ScanAmount = 1;
            
        Mw.Mapper = new LibraryMapper(Mw.M.MProc.Process, Properties.Resources.SQL_DLL);
        Mw.Mapper.MapLibrary();
            
        As.Dispatcher.Invoke(() =>
        {
            As.PaintLegoCars.IsEnabled = false;
        });    
            
        PaintLegoCarsToggle.IsEnabled = false;
        AutoshowGarageOption.IsEnabled = Mw.Mapper.DllBaseAddress != IntPtr.Zero;
        
        As.UiManager.AddProgress();
        As.UiManager.ToggleUiElements(Mw.Mapper.DllBaseAddress != IntPtr.Zero);
    }

    internal static void ResetMem()
    {
        if (Mw.Gvp.Name != "Forza Horizon 4")
        {
            return;
        }
            
        Mw.M.WriteStringMemory(Sql1, "NOT Garage.NotAvailableInAutoshow AS PurchasableCar,");
        Mw.M.WriteStringMemory(Sql2, "I");
        Mw.M.WriteStringMemory(Sql3, "AND NOT IsBarnFind");
        Mw.M.WriteStringMemory(Sql4, "Garage.Id!=");
        Mw.M.WriteStringMemory(Sql5, "AND NotAvailableInAutoshow=0");
        Mw.M.WriteStringMemory(Sql6, "=0                                    ");
        Mw.M.WriteStringMemory(Sql7, "AND IsCarVisibleAndReleased(Garage.ModelId)");
        Mw.M.WriteStringMemory(Sql8, "Garage.ModelId!=");
        Mw.M.WriteStringMemory(Sql9, "AND UnobtainableCars.Ordinal IS NULL");
        Mw.M.WriteStringMemory(Sql10, "D");
        Mw.M.WriteStringMemory(Sql11, "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ");
        Mw.M.WriteArrayMemory(Sql12, Sql12OriginalBytes);
        Mw.M.WriteStringMemory(Sql14, "HideNormalColors");
        Mw.M.WriteStringMemory(Sql15, "HideSpecialColors");
        Mw.M.WriteStringMemory(Sql16, "AND NOT IsMidnightCar");
    }
        
    public static async void ExecSql(ToggleSwitch button, RoutedEventHandler action, string sql)
    {
        button.GetType().GetProperty("IsEnabled")?.SetValue(button, false);
            
        var retValue = false;
            
        await Task.Run(() =>
        {
            using var pipeClient = new NamedPipeClientStream("PogPipe");
            var count = 0;
            while (!pipeClient.IsConnected && count < 25)
            {
                try
                {
                    pipeClient.Connect(100);
                }
                catch
                {
                    /* ignored */
                }

                ++count;
                Task.Delay(10).Wait();
            }

            if (count == 25)
            {
                MessageBox.Show("Failed, sowwy oomfie :3");
                return;
            }

            using var streamWriter = new StreamWriter(pipeClient);
                
            if (streamWriter.AutoFlush == false)
            {
                streamWriter.AutoFlush = true;
            }

            streamWriter.WriteLine(sql);
          
            retValue = true;
        });

        if (!retValue)
        {
            button.Toggled -= action;
            button.GetType().GetProperty("IsOn")?.SetValue(button, false);
            button.Toggled += action;
        }
            
        button.GetType().GetProperty("IsEnabled")?.SetValue(button, true);
    }
}