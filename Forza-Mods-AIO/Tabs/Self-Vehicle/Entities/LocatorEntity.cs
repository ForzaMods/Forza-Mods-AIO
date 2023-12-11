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
        if (Mw.Gvp.Name.Contains('4'))
        {
            SetupWaypointDetourFh4(sender);
        }
        else
        {
            SetupWaypointDetourFh5(sender);
        }
    }

    private static void SetupWaypointDetourFh4(object? sender)
    {
        if (!WaypointDetour.Setup(sender, WayPointXAsmAddr, "48 89 3D 05 00 00 00", 7, true, 0, true))
        {
            sender?.GetType().GetProperty("IsOn")?.SetValue(sender, false);
            WaypointDetour.Clear();
            MessageBox.Show("Failed");
            return;
        }

        while (WaypointDetour.ReadVariable<UIntPtr>() == 0)
        {
            Task.Delay(5).Wait();
        }

        _waypointEntity = WaypointDetour.ReadVariable<UIntPtr>();
        WaypointDetour.Destroy();
    }
    
    private static void SetupWaypointDetourFh5(object? sender)
    {
        if (!WaypointDetour.Setup(sender, WayPointXAsmAddr, "0F 11 15 05 00 00 00", 7, true, 0, true))
        {
            sender?.GetType().GetProperty("IsOn")?.SetValue(sender, false);
            WaypointDetour.Clear();
            MessageBox.Show("Failed");
            return;
        }

        Task.Delay(25).Wait();
        _waypointEntity = WaypointDetour.VariableAddress;
    }
    
    public static Vector3 WaypointPosition => Mw.M.ReadMemory<Vector3>(_waypointEntity + (UIntPtr)(Mw.Gvp.Name == "Forza Horizon 4" ? 928 : 0));
}