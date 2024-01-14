using System;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Resources.GameVerPlat.GameType;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.Entities;

public abstract class PhotoCamEntity
{
    public static UIntPtr MainPhotoCamEntity { get; set; }
    public static UIntPtr SpeedBase { get; set; }
    public static UIntPtr NoClipBase { get; set; }
    public static UIntPtr ShutterSpeedBase { get; set; }
    
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
        get => Mw.M.ReadMemory<float>(ShutterSpeedBase);
        set => Mw.M.WriteMemory(ShutterSpeedBase, value);
    }
    
    public static float SamplesMultiplier
    {
        set => Mw.M.WriteMemory(MainPhotoCamEntity + 0xC, value);
        get => Mw.M.ReadMemory<float>(MainPhotoCamEntity + 0xC);
    }
    
    public static float TurnAndZoomSpeed
    {
        set => Mw.M.WriteMemory(Mw.Gvp.Type == Fh5 ? SpeedBase + 0x2E0 : SpeedBase, value);
        get => Mw.M.ReadMemory<float>(Mw.Gvp.Type == Fh5 ? SpeedBase + 0x2E0 : SpeedBase);
    }
    
    public static float MovementSpeed
    {
        set => Mw.M.WriteMemory(Mw.Gvp.Type == Fh5 ? SpeedBase + 0x2DC : SpeedBase + 0x4, value);
        get => Mw.M.ReadMemory<float>(Mw.Gvp.Type == Fh5 ? SpeedBase + 0x2DC : SpeedBase + 0x4);
    }

    public static bool NoClip
    {
        set => Mw.M.WriteMemory(NoClipBase, value ? 0 : 2);
    }
    
    public static bool RemoveMaxHeight
    {
        set => Mw.M.WriteMemory(Mw.Gvp.Type == Fh5 ? NoClipBase - 400 : NoClipBase - 468, value ? 9999 : 4);
    }
    
    public static bool IncreasedZoom
    {
        set => Mw.M.WriteMemory(Mw.Gvp.Type == Fh5 ? NoClipBase - 392 : NoClipBase - 376, value ? 0f : 2.25f );
    }
}