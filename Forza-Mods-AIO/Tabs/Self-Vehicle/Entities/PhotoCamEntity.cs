
using System;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.Entities;

public abstract class PhotoCamEntity
{
    public static UIntPtr MainPhotoCamEntity, Speed, _NoClip, _ShutterSpeed;
    
    public static int Samples
    {
        get => MainWindow.Mw.M.ReadMemory<int>(MainPhotoCamEntity + 0x0);
        set => MainWindow.Mw.M.WriteMemory(MainPhotoCamEntity + 0x0, value);
    }
    
    public static float ApertureScale
    {
        get => MainWindow.Mw.M.ReadMemory<float>(MainPhotoCamEntity + 0x20);
        set => MainWindow.Mw.M.WriteMemory(MainPhotoCamEntity + 0x20, value);
    }
    
    public static float CarInFocus
    {
        get => MainWindow.Mw.M.ReadMemory<float>(MainPhotoCamEntity + 0x30);
        set => MainWindow.Mw.M.WriteMemory(MainPhotoCamEntity + 0x30, value);
    }
    
    public static float TimeSlice
    {
        get => MainWindow.Mw.M.ReadMemory<float>(MainPhotoCamEntity + 0x38);
        set => MainWindow.Mw.M.WriteMemory(MainPhotoCamEntity + 0x38, value);
    }
    
    public static float ShutterSpeed
    {
        get => MainWindow.Mw.M.ReadMemory<float>(_ShutterSpeed);
        set => MainWindow.Mw.M.WriteMemory(_ShutterSpeed, value);
    }
    
    public static float SamplesMultiplier
    {
        set => MainWindow.Mw.M.WriteMemory(MainPhotoCamEntity + 0xC, value);
    }
    
    public static float TurnAndZoomSpeed
    {
        set => MainWindow.Mw.M.WriteMemory(MainWindow.Mw.Name == "Forza Horizon 5" ? Speed + 0x2E0 : Speed, value);
    }
    
    public static float MovementSpeed
    {
        set => MainWindow.Mw.M.WriteMemory(MainWindow.Mw.Name == "Forza Horizon 5" ? Speed + 0x2DC : Speed + 0x4, value);
    }

    public static bool NoClip
    {
        set => MainWindow.Mw.M.WriteMemory(_NoClip, value ? 0 : 2);
    }
    
    public static bool RemoveMaxHeight
    {
        set => MainWindow.Mw.M.WriteMemory(MainWindow.Mw.Name == "Forza Horizon 5" ? _NoClip - 400 : _NoClip - 468, value ? 9999 : 4);
    }
    
    public static bool IncreasedZoom
    {
        set => MainWindow.Mw.M.WriteMemory(MainWindow.Mw.Name == "Forza Horizon 5" ? _NoClip - 392 : _NoClip - 376, value ? 0f : 2.25f );
    }
}