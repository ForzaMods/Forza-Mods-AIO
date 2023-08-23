using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO.Pipes;
using System.Threading;
using System.IO;
using Lunar;
using Forza_Mods_AIO.Resources;

namespace Forza_Mods_AIO.Tabs.AutoShowTab
{
    internal class AutoshowVars
    {
        #region Address Vars
        private long BaseAddressSQL = 0; // FH4 SQL Baseaddress
        public static string sql1;  //FH4 NOT Garage.NotAvailableInAutoshow AS PurchasableCar,
        public static string sql2;  // IsBarnFind
        public static string sql3;  // AND NOT IsBarnFind 
        public static string sql4;  // Garage.Id!=
        public static string sql5;  //FH4 AND NotAvailableInAutoshow=0
        public static string sql6;  //FH4 (basically just above address + 26)
        public static string sql7;  // AND IsCarVisibleAndReleased(Garage.ModelId) 
        public static string sql8;  // Garage.ModelId!=
        public static string sql9;  // AND UnobtainableCars.Ordinal IS NULL
        public static string sql10; // INNER JOIN Livery_DecalsSortOrder ON (Livery_Decals.ID = Livery_DecalsSortOrder.Livery_DecalID) WHERE MakeID = %d ORDER BY Sequence, AlphaSort
        public static string sql11; // DoNotAllowRemovalFromGarage
        public static string sql12; // UPDATE %s SET TopSpeed=%f
        public static string sql13; // UPDATE %sCareer_Garage SET Tuning_frontDownforce = %1.8e, Tuning_rearDownforce = %1.8e,
        public static string sql14; // sql13 but +2047
        public static string sql15; //FH4 HideNormalColors for lego cars
        public static string sql16; //FH4 HideSpecialColors for lego cars
        public static string sql17; //FH4 AND NOT IsMidnightCar 
        public static string sql18; //FH5 AND CanBuyNewCar(Garage.Id, Garage.NotAvailableInAutoshow)
        public static string sql19; //FH5 CanBuyNewCar(Garage.Id, Garage.NotAvailableInAutoshow) AS PurchasableCar,
        public bool read = true;
        public bool write = true;
        public bool exec = true;
        public bool IsFirstTime = true;
        #endregion

        public async Task Scan()
        {
            if (MainWindow.mw.gvp.Name == "Forza Horizon 4")
            {
                while (BaseAddressSQL == 0)
                    BaseAddressSQL = (await MainWindow.mw.m.AoBScan(MainWindow.mw.gvp.Process.MainModule.BaseAddress, 
                                                                    MainWindow.mw.gvp.Process.MainModule.BaseAddress + MainWindow.mw.gvp.Process.MainModule.ModuleMemorySize, 
                                                                    "4E 4F 54 20 47 61 72 61 67 65 2E 4E 6F 74 41 76 61 69 6C 61 62 6C 65 49 6E 41 75 74 6F 73 68 6F 77 20 41 53 20 50 75 72 63 68 61 73 61 62 6C 65 43 61 72 2C", 
                                                                    read, write, exec)).FirstOrDefault();

                UpdateUi.AddProgress(2, 1, AutoShow.AS.AOBProgressBar);
                sql1 =   BaseAddressSQL           .ToString("X");
                sql2 =  (BaseAddressSQL - 839960) .ToString("X");
                sql3 =  (BaseAddressSQL - 597151) .ToString("X");
                sql4 =  (BaseAddressSQL - 597127) .ToString("X");
                sql5 =  (BaseAddressSQL - 597071) .ToString("X");
                sql6 =  (BaseAddressSQL - 597045) .ToString("X");
                sql7 =  (BaseAddressSQL - 596999) .ToString("X");
                sql8 =  (BaseAddressSQL - 594967) .ToString("X");
                sql9 =  (BaseAddressSQL - 829295) .ToString("X");
                sql10 = (BaseAddressSQL + 427193) .ToString("X");
                sql11 = (BaseAddressSQL + 10736)  .ToString("X");
                sql12 = (BaseAddressSQL + 2773104).ToString("X");
                sql13 = (BaseAddressSQL + 2774368).ToString("X");
                sql14 = (BaseAddressSQL + 2776415).ToString("X");
                sql15 = (BaseAddressSQL + 2813928).ToString("X");
                sql16 = (BaseAddressSQL + 2813976).ToString("X");
                sql17 = (BaseAddressSQL - 597175) .ToString("X");
                UpdateUi.AddProgress(2, 2, AutoShow.AS.AOBProgressBar);
            }
            else
            {
                UpdateUi.AddProgress(4, 1, AutoShow.AS.AOBProgressBar);
                Memory<byte> DllBytes = Properties.Resources.SQL_DLL;
                UpdateUi.AddProgress(4, 2, AutoShow.AS.AOBProgressBar);
                MainWindow.mw.mapper = new LibraryMapper(MainWindow.mw.gvp.Process, DllBytes, MappingFlags.DiscardHeaders);
                UpdateUi.AddProgress(4, 3, AutoShow.AS.AOBProgressBar);
                try
                {
                    MainWindow.mw.mapper.MapLibrary();
                    MainWindow.mw.Was_Mapped = true;
                }
                catch
                {
                    MainWindow.mw.Was_Mapped = false;
                    MessageBox.Show("Failed, sowwy oomfie :3");
                }
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
