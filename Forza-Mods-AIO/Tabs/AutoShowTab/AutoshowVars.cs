using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;
using System.IO.Pipes;
using System.Threading;
using System.IO;
using Lunar;
using System.Resources;
using System.Globalization;
using Forza_Mods_AIO.Resources;
using System.Diagnostics.Eventing.Reader;
using Forza_Mods_AIO.Properties;

namespace Forza_Mods_AIO.Tabs.AutoShowTab
{
    internal class AutoshowVars
    {
        #region Address Vars
        public string sql1;  //FH4 NOT Garage.NotAvailableInAutoshow AS PurchasableCar,
        public string sql2;  // IsBarnFind
        public string sql3;  // AND NOT IsBarnFind 
        public string sql4;  // Garage.Id!=
        public string sql5;  //FH4 AND NotAvailableInAutoshow=0
        public string sql6;  //FH4 (basically just above address + 26)
        public string sql7;  // AND IsCarVisibleAndReleased(Garage.ModelId) 
        public string sql8;  // Garage.ModelId!=
        public string sql9;  // AND UnobtainableCars.Ordinal IS NULL
        public string sql10; // INNER JOIN Livery_DecalsSortOrder ON (Livery_Decals.ID = Livery_DecalsSortOrder.Livery_DecalID) WHERE MakeID = %d ORDER BY Sequence, AlphaSort
        public string sql11; // DoNotAllowRemovalFromGarage
        public string sql12; // UPDATE %s SET TopSpeed=%f
        public static string sql13; // UPDATE %sCareer_Garage SET Tuning_frontDownforce = %1.8e, Tuning_rearDownforce = %1.8e,
        public static string sql14; // sql13 but +2047
        public string sql15; //FH4 HideNormalColors for lego cars
        public string sql16; //FH4 HideSpecialColors for lego cars
        public string sql17; //FH4 AND NOT IsMidnightCar 
        public string sql18; //FH5 AND CanBuyNewCar(Garage.Id, Garage.NotAvailableInAutoshow)
        public string sql19; //FH5 CanBuyNewCar(Garage.Id, Garage.NotAvailableInAutoshow) AS PurchasableCar,
        public bool read = true;
        public bool write = true;
        public bool exec = false;
        public bool IsFirstTime = true;
        #endregion

        public async Task Scan()
        {
            if (MainWindow.mw.gvp.Name == "Forza Horizon 4")
            {
                sql2 = (await MainWindow.mw.m.AoBScan(MainWindow.mw.gvp.Process.MainModule.BaseAddress, MainWindow.mw.gvp.Process.MainModule.BaseAddress + MainWindow.mw.gvp.Process.MainModule.ModuleMemorySize, "49 73 42 61 72 6E 46 69 6E 64", read, write, exec)).FirstOrDefault().ToString("X");
                UpdateUi.AddProgress(17, 1, AutoShow.AS.AOBProgressBar);
                sql3 = (await MainWindow.mw.m.AoBScan(MainWindow.mw.gvp.Process.MainModule.BaseAddress, MainWindow.mw.gvp.Process.MainModule.BaseAddress + MainWindow.mw.gvp.Process.MainModule.ModuleMemorySize, "41 4E 44 20 4E 4F 54 20 49 73 42 61 72 6E 46 69 6E 64", read, write, exec)).FirstOrDefault().ToString("X");
                UpdateUi.AddProgress(17, 2, AutoShow.AS.AOBProgressBar);
                sql4 = (await MainWindow.mw.m.AoBScan(MainWindow.mw.gvp.Process.MainModule.BaseAddress, MainWindow.mw.gvp.Process.MainModule.BaseAddress + MainWindow.mw.gvp.Process.MainModule.ModuleMemorySize, "47 61 72 61 67 65 2E 49 64 21 3D", read, write, exec)).FirstOrDefault().ToString("X");
                UpdateUi.AddProgress(17, 3, AutoShow.AS.AOBProgressBar);
                sql7 = (await MainWindow.mw.m.AoBScan(MainWindow.mw.gvp.Process.MainModule.BaseAddress, MainWindow.mw.gvp.Process.MainModule.BaseAddress + MainWindow.mw.gvp.Process.MainModule.ModuleMemorySize, "41 4E 44 20 49 73 43 61 72 56 69 73 69 62 6C 65 41 6E 64 52 65 6C 65 61 73 65 64 28 47 61 72 61 67 65 2E 4D 6F 64 65 6C 49 64 29 00 00 00 00 20", read, write, exec)).FirstOrDefault().ToString("X");
                UpdateUi.AddProgress(17, 4, AutoShow.AS.AOBProgressBar);
                sql8 = (await MainWindow.mw.m.AoBScan(MainWindow.mw.gvp.Process.MainModule.BaseAddress, MainWindow.mw.gvp.Process.MainModule.BaseAddress + MainWindow.mw.gvp.Process.MainModule.ModuleMemorySize, "47 61 72 61 67 65 2E 4D 6F 64 65 6C 49 64 21 3D", read, write, exec)).FirstOrDefault().ToString("X");
                UpdateUi.AddProgress(17, 5, AutoShow.AS.AOBProgressBar);
                sql9 = (await MainWindow.mw.m.AoBScan(MainWindow.mw.gvp.Process.MainModule.BaseAddress, MainWindow.mw.gvp.Process.MainModule.BaseAddress + MainWindow.mw.gvp.Process.MainModule.ModuleMemorySize, "41 4E 44 20 55 6E 6F 62 74 61 69 6E 61 62 6C 65 43 61 72 73 2E 4F 72 64 69 6E 61 6C 20 49 53 20 4E 55 4C 4C", read, write, exec)).FirstOrDefault().ToString("X");
                UpdateUi.AddProgress(17, 6, AutoShow.AS.AOBProgressBar);
                sql10 = (await MainWindow.mw.m.AoBScan(MainWindow.mw.gvp.Process.MainModule.BaseAddress, MainWindow.mw.gvp.Process.MainModule.BaseAddress + MainWindow.mw.gvp.Process.MainModule.ModuleMemorySize, "49 4E 4E 45 52 20 4A 4F 49 4E 20 4C 69 76 65 72 79 5F 44 65 63 61 6C 73 53 6F 72 74 4F 72 64 65 72 20 4F 4E 20 28 4C 69 76 65 72 79 5F 44 65 63 61 6C 73 2E 49 44 20 3D 20 4C 69 76 65 72 79 5F 44 65 63 61 6C 73 53 6F 72 74 4F 72 64 65 72 2E 4C 69 76 65 72 79 5F 44 65 63 61 6C 49 44 29 20 57 48 45 52 45 20 4D 61 6B 65 49 44 20 3D 20 25 64 20 4F 52 44 45 52 20 42 59 20 53 65 71 75 65 6E 63 65 2C 20 41 6C 70 68 61 53 6F 72 74", read, write, exec)).FirstOrDefault().ToString("X");
                UpdateUi.AddProgress(17, 7, AutoShow.AS.AOBProgressBar);
                sql11 = (await MainWindow.mw.m.AoBScan(MainWindow.mw.gvp.Process.MainModule.BaseAddress, MainWindow.mw.gvp.Process.MainModule.BaseAddress + MainWindow.mw.gvp.Process.MainModule.ModuleMemorySize, "44 6F 4E 6F 74 41 6C 6C 6F 77 52 65 6D 6F 76 61 6C 46 72 6F 6D 47 61 72 61 67 65 00 00 00 00 00", read, write, exec)).FirstOrDefault().ToString("X");
                UpdateUi.AddProgress(17, 8, AutoShow.AS.AOBProgressBar);
                sql12 = (await MainWindow.mw.m.AoBScan(MainWindow.mw.gvp.Process.MainModule.BaseAddress, MainWindow.mw.gvp.Process.MainModule.BaseAddress + MainWindow.mw.gvp.Process.MainModule.ModuleMemorySize, "55 50 44 41 54 45 20 25 73 20 53 45 54 20 54 6F 70 53 70 65 65 64 3D 25 66", read, write, exec)).FirstOrDefault().ToString("X");
                UpdateUi.AddProgress(17, 9, AutoShow.AS.AOBProgressBar);
                sql13 = (await MainWindow.mw.m.AoBScan(MainWindow.mw.gvp.Process.MainModule.BaseAddress, MainWindow.mw.gvp.Process.MainModule.BaseAddress + MainWindow.mw.gvp.Process.MainModule.ModuleMemorySize, "55 50 44 41 54 45 20 25 73 43 61 72 65 65 72 5F 47 61 72 61 67 65 20 53 45 54 20 54 75 6E 69 6E 67 5F 66 72 6F 6E 74 44 6F 77 6E 66 6F 72 63 65 20 3D 20 25 31 2E 38 65 2C 20 54 75 6E 69 6E 67 5F 72 65 61 72 44 6F 77 6E 66 6F 72 63 65 20 3D 20 25 31 2E 38 65 2C", read, write, exec)).FirstOrDefault().ToString("X");
                UpdateUi.AddProgress(17, 10, AutoShow.AS.AOBProgressBar);
                sql14 = ((await MainWindow.mw.m.AoBScan(MainWindow.mw.gvp.Process.MainModule.BaseAddress, MainWindow.mw.gvp.Process.MainModule.BaseAddress + MainWindow.mw.gvp.Process.MainModule.ModuleMemorySize, "55 50 44 41 54 45 20 25 73 43 61 72 65 65 72 5F 47 61 72 61 67 65 20 53 45 54 20 54 75 6E 69 6E 67 5F 66 72 6F 6E 74 44 6F 77 6E 66 6F 72 63 65 20 3D 20 25 31 2E 38 65 2C 20 54 75 6E 69 6E 67 5F 72 65 61 72 44 6F 77 6E 66 6F 72 63 65 20 3D 20 25 31 2E 38 65 2C", read, write, exec)).FirstOrDefault() + 2047).ToString("X");
                UpdateUi.AddProgress(17, 11, AutoShow.AS.AOBProgressBar);
                sql1 = (await MainWindow.mw.m.AoBScan(MainWindow.mw.gvp.Process.MainModule.BaseAddress, MainWindow.mw.gvp.Process.MainModule.BaseAddress + MainWindow.mw.gvp.Process.MainModule.ModuleMemorySize, "4E 4F 54 20 47 61 72 61 67 65 2E 4E 6F 74 41 76 61 69 6C 61 62 6C 65 49 6E 41 75 74 6F 73 68 6F 77 20 41 53 20 50 75 72 63 68 61 73 61 62 6C 65 43 61 72 2C", true, true)).FirstOrDefault().ToString("X");
                UpdateUi.AddProgress(17, 12, AutoShow.AS.AOBProgressBar);
                sql5 = (await MainWindow.mw.m.AoBScan(MainWindow.mw.gvp.Process.MainModule.BaseAddress, MainWindow.mw.gvp.Process.MainModule.BaseAddress + MainWindow.mw.gvp.Process.MainModule.ModuleMemorySize, "41 4E 44 20 4E 6F 74 41 76 61 69 6C 61 62 6C 65 49 6E 41 75 74 6F 73 68 6F 77 3D 30", true, true)).FirstOrDefault().ToString("X");
                UpdateUi.AddProgress(17, 13, AutoShow.AS.AOBProgressBar);
                sql6 = ((await MainWindow.mw.m.AoBScan(MainWindow.mw.gvp.Process.MainModule.BaseAddress, MainWindow.mw.gvp.Process.MainModule.BaseAddress + MainWindow.mw.gvp.Process.MainModule.ModuleMemorySize, "41 4E 44 20 4E 6F 74 41 76 61 69 6C 61 62 6C 65 49 6E 41 75 74 6F 73 68 6F 77 3D 30", true, true)).FirstOrDefault() + 26).ToString("X");
                UpdateUi.AddProgress(17, 14, AutoShow.AS.AOBProgressBar);
                sql15 = (await MainWindow.mw.m.AoBScan(MainWindow.mw.gvp.Process.MainModule.BaseAddress, MainWindow.mw.gvp.Process.MainModule.BaseAddress + MainWindow.mw.gvp.Process.MainModule.ModuleMemorySize, "48 69 64 65 4E 6F 72 6D 61 6C 43 6F 6C 6F 72 73 00 00 00 00 00 00 00 00 48 69 64 65 4D 61 6E 75 66 61 63 74 75 72 65 72 43 6F 6C 6F 72 73 00 00", true, true)).FirstOrDefault().ToString("X");
                UpdateUi.AddProgress(17, 15, AutoShow.AS.AOBProgressBar);
                sql16 = ((await MainWindow.mw.m.AoBScan(MainWindow.mw.gvp.Process.MainModule.BaseAddress, MainWindow.mw.gvp.Process.MainModule.BaseAddress + MainWindow.mw.gvp.Process.MainModule.ModuleMemorySize, "48 69 64 65 53 70 65 63 69 61 6C 43 6F 6C 6F 72 73 00 00 00 00 00 00 00 41 6C 6C 6F 77 53 74 6F 63 6B 4D 61 6E 75 66 61 63 74 75 72 65 72 43 6F 6C 6F 72 73 46 6F 72 57 68 65 65 6C 73", true, true)).FirstOrDefault() + 2047).ToString("X");
                UpdateUi.AddProgress(17, 16, AutoShow.AS.AOBProgressBar);
                sql17 = (await MainWindow.mw.m.AoBScan(MainWindow.mw.gvp.Process.MainModule.BaseAddress, MainWindow.mw.gvp.Process.MainModule.BaseAddress + MainWindow.mw.gvp.Process.MainModule.ModuleMemorySize, "41 4E 44 20 4E 4F 54 20 49 73 4D 69 64 6E 69 67 68 74 43 61 72 00 00 20 41 4E 44 20 4E 4F 54 20 49 73 42 61 72 6E 46 69 6E 64 00 00 00 00 00 20", true, true)).FirstOrDefault().ToString("X");
                UpdateUi.AddProgress(17, 17, AutoShow.AS.AOBProgressBar);
            }
            else
            {
                LibraryMapper Mapper;
                UpdateUi.AddProgress(4, 1, AutoShow.AS.AOBProgressBar);
                Memory<byte> DllBytes = Properties.Resources.SQL_DLL;
                UpdateUi.AddProgress(4, 2, AutoShow.AS.AOBProgressBar);
                Mapper = new LibraryMapper(MainWindow.mw.gvp.Process, DllBytes, MappingFlags.DiscardHeaders);
                UpdateUi.AddProgress(4, 3, AutoShow.AS.AOBProgressBar);
                Mapper.MapLibrary();
                UpdateUi.AddProgress(4, 4, AutoShow.AS.AOBProgressBar);
            }
            UpdateUi.UpdateUI(true, AutoShow.AS);
        }

        public static unsafe bool ExecSQL(string SQL)
        {
            using (var Client = new NamedPipeClientStream("PogPipe"))
            {
                int Count = 0;
                byte[] SQLBytes = Encoding.UTF8.GetBytes(SQL);
                while (!Client.IsConnected && Count < 25)
                {
                    Thread.Sleep(10);
                    try
                    { Client.Connect(10); }
                    catch { };
                    Count++;
                }

                if (Count == 25)
                {
                    MessageBox.Show("Failed, sowwy oomfie :3");
                    return false;
                }

                using (StreamWriter sw = new StreamWriter(Client))
                {
                    if (sw.AutoFlush == false)
                        sw.AutoFlush = true;
                    sw.WriteLine(SQL);
                }

                return true;

                //Client.WaitForPipeDrain();
                //Client.Write(SQLBytes, 0, SQLBytes.Length);
                //Client.WaitForPipeDrain();
            }
        }
    }
}
