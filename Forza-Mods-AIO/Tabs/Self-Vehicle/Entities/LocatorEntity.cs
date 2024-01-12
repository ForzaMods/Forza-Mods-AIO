using System;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows;
using Forza_Mods_AIO.Resources;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.SelfVehicleAddresses;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.Entities;

public abstract class LocatorEntity
{
    private static UIntPtr _waypointEntity;
    public static readonly Detour WaypointDetour = new();

    public static void SetupWaypointDetour(object? sender)
    {
        const string originalFh5 = "0F10 97 30020000";
        const string originalFh4 = "0F10 97 A0030000";
        const string detoured = "0F 11 15 05 00 00 00";

        var original = Mw.Gvp.Type == GameVerPlat.GameType.Fh4 ? originalFh4 : originalFh5;
        
        if (!WaypointDetour.Setup(sender, WayPointXAsmAddr,original, detoured, 7, true, 0, true))
        {
            sender?.GetType().GetProperty("IsOn")?.SetValue(sender, false);
            WaypointDetour.Clear();
            MessageBox.Show("Failed");
            return;
        }

        Task.Delay(25).Wait();
        _waypointEntity = WaypointDetour.VariableAddress;
    }
    
    public static Vector3 WaypointPosition => Mw.M.ReadMemory<Vector3>(_waypointEntity);
}