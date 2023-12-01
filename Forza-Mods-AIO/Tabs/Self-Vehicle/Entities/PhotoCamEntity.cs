using System;
using static Forza_Mods_AIO.MainWindow;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.Entities;

public abstract class PhotoCamEntity
{
    public static UIntPtr MainPhotoCamEntity, Speed, _NoClip, _ShutterSpeed;
    
    public static int Samples
    {
        get => Mw.M.ReadMemory<int>(MainPhotoCamEntity + 0x0);
        set => Mw.M.WriteMemory(MainPhotoCamEntity + 0x0, value);
    }
    
    public static float ApertureScale
    {
        get => Mw.M.ReadMemory<float>(MainPhotoCamEntity + 0x20);
        set => Mw.M.WriteMemory(MainPhotoCamEntity + 0x20, value);
    }
    
    public static float CarInFocus
    {
        get => Mw.M.ReadMemory<float>(MainPhotoCamEntity + 0x30);
        set => Mw.M.WriteMemory(MainPhotoCamEntity + 0x30, value);
    }
    
    public static float TimeSlice
    {
        get => Mw.M.ReadMemory<float>(MainPhotoCamEntity + 0x38);
        set => Mw.M.WriteMemory(MainPhotoCamEntity + 0x38, value);
    }
    
    public static float ShutterSpeed
    {
        get => Mw.M.ReadMemory<float>(_ShutterSpeed);
        set => Mw.M.WriteMemory(_ShutterSpeed, value);
    }
    
    public static float SamplesMultiplier
    {
        set => Mw.M.WriteMemory(MainPhotoCamEntity + 0xC, value);
    }
    
    public static float TurnAndZoomSpeed
    {
        set => Mw.M.WriteMemory(Mw.Gvp.Name == "Forza Horizon 5" ? Speed + 0x2E0 : Speed, value);
    }
    
    public static float MovementSpeed
    {
        set => Mw.M.WriteMemory(Mw.Gvp.Name == "Forza Horizon 5" ? Speed + 0x2DC : Speed + 0x4, value);
    }

    public static bool NoClip
    {
        set => Mw.M.WriteMemory(_NoClip, value ? 0 : 2);
    }
    
    public static bool RemoveMaxHeight
    {
        set => Mw.M.WriteMemory(Mw.Gvp.Name == "Forza Horizon 5" ? _NoClip - 400 : _NoClip - 468, value ? 9999 : 4);
    }
    
    public static bool IncreasedZoom
    {
        set => Mw.M.WriteMemory(Mw.Gvp.Name == "Forza Horizon 5" ? _NoClip - 392 : _NoClip - 376, value ? 0f : 2.25f );
    }
}