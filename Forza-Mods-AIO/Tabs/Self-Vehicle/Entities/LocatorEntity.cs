using System;
using System.Numerics;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.Entities;

public abstract class LocatorEntity
{
    private static UIntPtr WaypointEntity;
    //private static UIntPtr CheckpointEntity;

    public static Vector3 WaypointPosition
    {
        get
        {
            WaypointEntity = MainWindow.mw.m.ReadMemory<UIntPtr>(Self_Vehicle_ASM.CodeCave3 + 41);
            return new Vector3
            {
                X = MainWindow.mw.m.ReadMemory<float>((UIntPtr)(WaypointEntity + MainWindow.mw.gvp.Name == "Forza Horizon 4" ? 928 : 560)),
                Y = MainWindow.mw.m.ReadMemory<float>((UIntPtr)(WaypointEntity + MainWindow.mw.gvp.Name == "Forza Horizon 4" ? 932 : 564)),
                Z = MainWindow.mw.m.ReadMemory<float>((UIntPtr)(WaypointEntity + MainWindow.mw.gvp.Name == "Forza Horizon 4" ? 936 : 568))
            };
        }
    }
    
    // TODO; Add Checkpoint Back
}