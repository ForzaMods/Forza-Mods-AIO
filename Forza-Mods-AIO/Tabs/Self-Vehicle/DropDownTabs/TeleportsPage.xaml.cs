using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.Entities.CarEntity;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.Entities.LocatorEntity;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

public partial class TeleportsPage
{
    public static TeleportsPage Teleports { get; private set; } = null!;
    public static bool WaypointDetoured { get; set; }
    private Vector3 _oldPosition, _teleportPosition;
    
    public TeleportsPage()
    {
        InitializeComponent();
        Teleports = this;
    }

    private async void TeleportBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (TeleportBox.SelectedItem == null || !Mw.Attached)
        {
            return;
        }

        if (!TeleportBox.Items.Contains("Undo Teleport"))
        {
            TeleportBox.Items.Insert(0, "Undo Teleport");
        }


        switch (TeleportBox.SelectedItem)
        {
            #region FH4

            case "Adventure Park":
            {
                _teleportPosition = new Vector3 { X = 2267.335449f, Y = 304.2393494f, Z = -2611.638428f };
                break;
            }
            case "Ambleside":
            {
                _teleportPosition = new Vector3 { X = -5112.047363f, Y = 154.1546478f, Z = -3534.503906f };
                break;
            }
            case "Beach":
            {
                _teleportPosition = new Vector3 { X = 4874.382812f, Y = 124.9019775f, Z = -1392.215454f };
                break;
            }
            case "Broadway":
            {
                _teleportPosition = new Vector3 { X = -237.2871857f, Y = 239.5045471f, Z = -5816.858398f };
                break;
            }
            case "Dam":
            {
                _teleportPosition = new Vector3 { X = -854.6953125f, Y = 209.1066284f, Z = -2031.137329f };
                break;
            }
            case "Edinburgh":
            {
                _teleportPosition = new Vector3 { X = 2045.383179f, Y = 204.0559845f, Z = 2511.078613f };
                break;
            }
            case "Festival":
            {
                _teleportPosition = new Vector3 { X = -2753.350098f, Y = 349.7218018f, Z = -4357.629883f };
                break;
            }
            case "Greendale Airstrip":
            {
                _teleportPosition = new Vector3 { X = 3409.570068f, Y = 159.2418976f, Z = 661.2498779f };
                break;
            }
            case "Lake Island":
            {
                _teleportPosition = new Vector3 { X = -4001.890869f, Y = 175.7261353f, Z = -196.6170197f };
                break;
            }
            case "Mortimer Gardens":
            {
                _teleportPosition = new Vector3 { X = -4314.36377f, Y = 153.261261f, Z = 1804.139282f };
                break;
            }
            case "Quarry":
            {
                _teleportPosition = new Vector3 { X = -1569.987305f, Y = 206.0023804f, Z = -2843.05249f };
                break;
            }
            case "Railyard":
            {
                _teleportPosition = new Vector3 { X = -935.0923462f, Y = 161.055069f, Z = 1745.383667f };
                break;
            }
            case "Start of Motorway":
            {
                _teleportPosition = new Vector3 { X = 2657.887451f, Y = 270.7128906f, Z = -4353.087402f };
                break;
            }
            case "Top of Mountain":
            {
                _teleportPosition = new Vector3 { X = -2285.739746f, Y = 364.6417236f, Z = 2576.946533f };
                break;
            }

            #endregion

            #region FH5

            case "Top Of Volcano":
            {
                _teleportPosition = new Vector3 { X = -5594.330078f, Y = 1023.229919f, Z = 2392.037109f };
                break;
            }
            case "Stadium":
            {
                _teleportPosition = new Vector3 { X = -762.8079834f, Y = 169.0338593f, Z = 1615.112183f };
                break;
            }
            case "Guanajuato (Main City)":
            {
                _teleportPosition = new Vector3 { X = 355.9811096f, Y = 258.8370056f, Z = 3135.321533f };
                break;
            }
            case "Bridge":
            {
                _teleportPosition = new Vector3 { X = -5820.825684f, Y = 122.3475876f, Z = -2550.383545f };
                break;
            }
            case "Golf Course":
            {
                _teleportPosition = new Vector3 { X = -8316.630859f, Y = 125.8156357f, Z = -1150.103271f };
                break;
            }
            case "Dunes":
            {
                _teleportPosition = new Vector3 { X = -8615.027344f, Y = 143.9117279f, Z = 1966.912109f };
                break;
            }
            case "Motorway":
            {
                _teleportPosition = new Vector3 { X = 2855.958252f, Y = 195.1608429f, Z = 1465.902954f };
                break;
            }
            case "Airstrip":
            {
                _teleportPosition = new Vector3 { X = -3891.084717f, Y = 174.4389496f, Z = -3841.428467f };
                break;
            }
            case "Mulege":
            {
                _teleportPosition = new Vector3 { X = -4174.963867f, Y = 122.9130783f, Z = -2227.120605f };
                break;
            }
            case "Temple":
            {
                _teleportPosition = new Vector3 { X = 3643.609375f, Y = 230.227066f, Z = -2646.405029f };
                break;
            }
            case "River":
            {
                _teleportPosition = new Vector3 { X = 923.4258423f, Y = 246.7331696f, Z = -2980.020264f };
                break;
            }
            case "Dirt Circuit":
            {
                _teleportPosition = new Vector3 { X = -8344.927734f, Y = 200.0671387f, Z = 3197.32959f };
                break;
            }
            case "Pllaya Azul":
            {
                _teleportPosition = new Vector3 { X = 5550.070801f, Y = 105.1047897f, Z = 497.8027649f };
                break;
            }
            case "Temple Drag":
            {
                _teleportPosition = new Vector3 { X = 751.5328979f, Y = 190.3298645f, Z = -110.3424072f };
                break;
            }

            #endregion
            
            case "Undo Teleport":
            {
                Position = _oldPosition;
                return;
            }

            case "Waypoint":
            {
                if (!WaypointDetoured)
                {
                    await Task.Run(() => SetupWaypointDetour(null));
                    WaypointDetoured = true;
                    return;
                }
                
                if (WaypointPosition.X == 0)
                {
                    MessageBox.Show("Go make a new waypoint");
                    return;
                }

                _teleportPosition = WaypointPosition;
                break;
            }
        }
        
        _oldPosition = Position;
        LinearVelocity = new Vector3 { X = 0f, Z = 0f, Y = 0f };
        Position = _teleportPosition;
    }

    private async void AutoTpToWaypoint_Toggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        Vector3 lastWp = new();

        if (!WaypointDetoured)
        {
            await Task.Run(() => SetupWaypointDetour(sender));
            WaypointDetoured = true;
        }

        Task.Run(() =>
        {
            while (true)
            {
                Thread.Sleep(1000);
                
                var toggled = true;
                Dispatcher.Invoke(() => toggled = AutoTpToWaypoint.IsOn);

                if (!toggled)
                {
                    break;
                }

                try
                {
                    var newWp = WaypointPosition;

                    if (!((lastWp.X != newWp.X || lastWp.Y != newWp.Y || lastWp.Z != newWp.Z)
                          && newWp.X != 0 && newWp.Y != 0 && newWp.Z != 0
                          && newWp.X is < 10000 and > -10000
                          && newWp.Y is < 3000 and > -100
                          && newWp.Z is < 10000 and > -10000))
                    {
                        continue;
                    }

                    _oldPosition = Position;
                    Position = newWp;
                    LinearVelocity = new Vector3 { X = 0f, Z = 0f, Y = 0f };
                    lastWp = newWp;
                }
                catch
                {
                    // ignored
                }
            }
        });
    }
}