
using System;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.Entities;

public abstract class PhotoCamEntity
{
    public static UIntPtr _MainPhotoCamEntity, _Speed, _NoClip, _ShutterSpeed;
    
    public static int Samples
    {
        get => MainWindow.mw.m.ReadMemory<int>(_MainPhotoCamEntity + 0x0);
        set => MainWindow.mw.m.WriteMemory(_MainPhotoCamEntity + 0x0, value);
    }
    
    public static float ApertureScale
    {
        get => MainWindow.mw.m.ReadMemory<float>(_MainPhotoCamEntity + 0x20);
        set => MainWindow.mw.m.WriteMemory(_MainPhotoCamEntity + 0x20, value);
    }
    
    public static float CarInFocus
    {
        get => MainWindow.mw.m.ReadMemory<float>(_MainPhotoCamEntity + 0x30);
        set => MainWindow.mw.m.WriteMemory(_MainPhotoCamEntity + 0x30, value);
    }
    
    public static float TimeSlice
    {
        get => MainWindow.mw.m.ReadMemory<float>(_MainPhotoCamEntity + 0x38);
        set => MainWindow.mw.m.WriteMemory(_MainPhotoCamEntity + 0x38, value);
    }
    
    public static float ShutterSpeed
    {
        get => MainWindow.mw.m.ReadMemory<float>(_ShutterSpeed);
        set => MainWindow.mw.m.WriteMemory(_ShutterSpeed, value);
    }
    
    public static float SamplesMultiplier
    {
        set => MainWindow.mw.m.WriteMemory(_MainPhotoCamEntity + 0xC, value);
    }
    
    public static float TurnAndZoomSpeed
    {
        set => MainWindow.mw.m.WriteMemory(MainWindow.mw.Name == "Forza Horizon 5" ? _Speed + 0x2E0 : _Speed, value);
    }
    
    public static float MovementSpeed
    {
        set => MainWindow.mw.m.WriteMemory(MainWindow.mw.Name == "Forza Horizon 5" ? _Speed + 0x2DC : _Speed + 0x4, value);
    }

    public static bool NoClip
    {
        set => MainWindow.mw.m.WriteMemory(_NoClip, value ? 0 : 2);
    }
    
    public static bool RemoveMaxHeight
    {
        set => MainWindow.mw.m.WriteMemory(MainWindow.mw.Name == "Forza Horizon 5" ? _NoClip - 400 : _NoClip - 468, value ? 9999 : 4);
    }
    
    public static bool IncreasedZoom
    {
        set => MainWindow.mw.m.WriteMemory(MainWindow.mw.Name == "Forza Horizon 5" ? _NoClip - 392 : _NoClip - 376, value ? 0f : 2.25f );
    }
}