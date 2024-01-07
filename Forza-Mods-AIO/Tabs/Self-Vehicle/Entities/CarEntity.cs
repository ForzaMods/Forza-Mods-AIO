using System;
using System.Numerics;
using System.Threading.Tasks;
using Forza_Mods_AIO.Resources;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.SelfVehicleAddresses;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.Entities;

public abstract class CarEntity
{
    public static UIntPtr PlayerCarEntity { get; private set; }
    public static readonly Detour BaseDetour = new();
    private const string BaseFh5 = "48 81 E9 70 05 00 00 48 89 0D 0C 00 00 00 48 81 C1 70 05 00 00";
    private const string BaseFh4 = "48 81 E9 60 05 00 00 48 89 0D 0C 00 00 00 48 81 C1 60 05 00 00";
    private const string BaseFm8 = "48 81 E9 D0 04 00 00 48 89 0D 0C 00 00 00 48 81 C1 D0 04 00 00";
    private const string OrigFh5 = "F3 0F 10 81 18 15 00 00";
    private const string OrigFh4 = "F3 0F 10 81 90 01 00 00";
    private const string OrigFm8 = "0F 2F B1 A0 71 00 00";
    
    public static async void Hook()
    {
        if (!BaseDetour.IsHooked)
        {
            var baseDetourBytes = Mw.Gvp.Name switch
            {
                { } name when name.Contains('8') => BaseFm8,
                { } name when name.Contains('5') => BaseFh5,
                _ => BaseFh4
            };
            
            var orig = Mw.Gvp.Name switch
            {
                { } name when name.Contains('8') => OrigFm8,
                { } name when name.Contains('5') => OrigFh5,
                _ => OrigFh4
            };
            
            var replace = Mw.Gvp.Name.Contains('8') ? 7 : 8;
            BaseDetour.Setup(BaseAddrHook, orig, baseDetourBytes, replace, true, 0, true);

            if (!Mw.Gvp.Name.Contains('8'))
            {
                return;
            }
            
            Bypass.AddProtectAddress(GravityProtectAddr);
            Mw.M.WriteArrayMemory(GravityProtectAddr, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 });
            Mw.M.WriteArrayMemory(AccelProtectAddr, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 });
        }

        var taskCompletionSource = new TaskCompletionSource<bool>();
        
        await Task.Run(() =>
        {
            while ((PlayerCarEntity = BaseDetour.ReadVariable<UIntPtr>()) == 0)
            {
                Task.Delay(1).Wait();
            }
            
            taskCompletionSource.SetResult(true);
        });

        await taskCompletionSource.Task;
    }
    
    #region Floats
    
    private const int GravityOffset = 0x8; 
    public static float Gravity
    {
        get
        {
            Hook();
            return Mw.M.ReadMemory<float>(PlayerCarEntity + GravityOffset);  
        }
        set
        {
            Hook();
            Mw.M.WriteMemory(PlayerCarEntity + GravityOffset, value);
        }
    }

    private const int AccelOffset = 0xC;
    public static float Acceleration
    {
        get
        {
            Hook();
            return Mw.M.ReadMemory<float>(PlayerCarEntity + AccelOffset);  
        }
        set
        {
            Hook();
            Mw.M.WriteMemory(PlayerCarEntity + AccelOffset, value);
        }
    }

    #endregion
    
    #region Vectors

    private const int LinVelOffset = 0x20;
    private const int LinVelOffsetFm8 = 0x10;
    public static Vector3 LinearVelocity
    {
        get
        {
            Hook();
            return Mw.Gvp.Name switch
            {
                { } name when name.Contains('8') => Mw.M.ReadMemory<Vector3>(PlayerCarEntity + LinVelOffsetFm8),
                _ => Mw.M.ReadMemory<Vector3>(PlayerCarEntity + LinVelOffset)
            };
        }
        set
        {
            Hook();
            _ = Mw.Gvp.Name switch
            {
                { } name when name.Contains('8') => Mw.M.WriteMemory(PlayerCarEntity + LinVelOffsetFm8, value),
                _ => Mw.M.WriteMemory(PlayerCarEntity + LinVelOffset, value)
            };
        }
    }
    
    private const int AngVelOffset = 0x30;
    private const int AngVelOffsetFm8 = 0x20;
    public static Vector3 AngularVelocity
    {
        set
        {
            Hook();
            _ = Mw.Gvp.Name switch
            {
                { } name when name.Contains('8') => Mw.M.WriteMemory(PlayerCarEntity + AngVelOffsetFm8, value),
                _ => Mw.M.WriteMemory(PlayerCarEntity + AngVelOffset, value)
            };
        }
    }

    private const int PosOffsetFh5 = 0x50;
    private const int PosOffsetFh4 = 0x40;
    private const int PosOffsetFm8 = 0x30;
    public static Vector3 Position
    {
        get
        {
            Hook();
            return Mw.Gvp.Name switch
            {
                { } name when name.Contains('8') => Mw.M.ReadMemory<Vector3>(PlayerCarEntity + PosOffsetFm8),
                { } name when name.Contains('5') => Mw.M.ReadMemory<Vector3>(PlayerCarEntity + PosOffsetFh5),
                _ => Mw.M.ReadMemory<Vector3>(PlayerCarEntity + PosOffsetFh4)
            };
        }
        set
        {
            Hook();
            _ = Mw.Gvp.Name switch
            {
                { } name when name.Contains('8') => Mw.M.WriteMemory(PlayerCarEntity + PosOffsetFm8, value),
                { } name when name.Contains('5') => Mw.M.WriteMemory(PlayerCarEntity + PosOffsetFh5, value),
                _ => Mw.M.WriteMemory(PlayerCarEntity + PosOffsetFh4, value)
            };
        }
    }

    private const int RotationOffsetFh5 = 0x80;
    private const int RotationOffsetFh4 = 0xF0;
    private const int RotationOffsetFm8 = 0x50;
    public static Matrix4x4 Rotation
    {
        get
        {
            Hook();
            return Mw.Gvp.Name switch
            {
                { } name when name.Contains('8') => Mw.M.ReadMemory<Matrix4x4>(PlayerCarEntity + RotationOffsetFm8),
                { } name when name.Contains('5') => Mw.M.ReadMemory<Matrix4x4>(PlayerCarEntity + RotationOffsetFh5),
                _ => Mw.M.ReadMemory<Matrix4x4>(PlayerCarEntity + RotationOffsetFh4)
            };
        }
        set
        {
            Hook();
            _ = Mw.Gvp.Name switch
            {
                { } name when name.Contains('8') => Mw.M.WriteMemory(PlayerCarEntity + RotationOffsetFm8, value),
                { } name when name.Contains('5') => Mw.M.WriteMemory(PlayerCarEntity + RotationOffsetFh5, value),
                _ => Mw.M.WriteMemory(PlayerCarEntity + RotationOffsetFh4, value)
            };
        }
    }

    private const int FrontLeftWheelSpeedOffsetFh5 = 0x26C0;
    private const int FrontRightWheelSpeedOffsetFh5 = 0x3180;
    private const int RearLeftWheelSpeedOffsetFh5 = 0x4700;
    private const int RearRightWheelSpeedOffsetFh5 = 0x3C40;
    
    private const int FrontLeftWheelSpeedOffsetFm8 = 0x1E14;
    private const int FrontRightWheelSpeedOffsetFm8 = 0x26A4;
    private const int RearLeftWheelSpeedOffsetFm8 = 0x37C4;
    private const int RearRightWheelSpeedOffsetFm8 = 0x2F34;
    
    private const int FrontLeftWheelSpeedOffsetFh4 = 0x3C40;
    private const int FrontRightWheelSpeedOffsetFh4 = 0x339C;
    private const int RearLeftWheelSpeedOffsetFh4 = 0x5F5C;
    private const int RearRightWheelSpeedOffsetFh4 = 0x497C;
    
    public static Vector4 WheelSpeed
    {
        get
        {
            Hook();
            return Mw.Gvp.Name switch
            {
                { } name when name.Contains('8') => new Vector4
                {
                    X = Mw.M.ReadMemory<float>(PlayerCarEntity + FrontLeftWheelSpeedOffsetFm8),
                    Y = Mw.M.ReadMemory<float>(PlayerCarEntity + FrontRightWheelSpeedOffsetFm8),
                    Z = Mw.M.ReadMemory<float>(PlayerCarEntity + RearLeftWheelSpeedOffsetFm8),
                    W = Mw.M.ReadMemory<float>(PlayerCarEntity + RearRightWheelSpeedOffsetFm8)
                },
                { } name when name.Contains('5') => new Vector4
                {
                    X = Mw.M.ReadMemory<float>(PlayerCarEntity + FrontLeftWheelSpeedOffsetFh5),
                    Y = Mw.M.ReadMemory<float>(PlayerCarEntity + FrontRightWheelSpeedOffsetFh5),
                    Z = Mw.M.ReadMemory<float>(PlayerCarEntity + RearLeftWheelSpeedOffsetFh5),
                    W = Mw.M.ReadMemory<float>(PlayerCarEntity + RearRightWheelSpeedOffsetFh5)
                },
                _ => new Vector4
                {
                    X = Mw.M.ReadMemory<float>(PlayerCarEntity + FrontLeftWheelSpeedOffsetFh4),
                    Y = Mw.M.ReadMemory<float>(PlayerCarEntity + FrontRightWheelSpeedOffsetFh4),
                    Z = Mw.M.ReadMemory<float>(PlayerCarEntity + RearLeftWheelSpeedOffsetFh4),
                    W = Mw.M.ReadMemory<float>(PlayerCarEntity + RearRightWheelSpeedOffsetFh4)
                }
            };
        }
        set
        {
            Hook();
            switch (Mw.Gvp.Name)
            {
                case { } name when name.Contains('8'):
                {
                    Mw.M.WriteMemory(PlayerCarEntity + FrontLeftWheelSpeedOffsetFm8, value.X);
                    Mw.M.WriteMemory(PlayerCarEntity + FrontRightWheelSpeedOffsetFm8, value.Y);
                    Mw.M.WriteMemory(PlayerCarEntity + RearLeftWheelSpeedOffsetFm8, value.Z);
                    Mw.M.WriteMemory(PlayerCarEntity + RearRightWheelSpeedOffsetFm8, value.W);
                    break;
                }
                case { } name when name.Contains('5'):
                {
                    Mw.M.WriteMemory(PlayerCarEntity + FrontLeftWheelSpeedOffsetFh5, value.X);
                    Mw.M.WriteMemory(PlayerCarEntity + FrontRightWheelSpeedOffsetFh5, value.Y);
                    Mw.M.WriteMemory(PlayerCarEntity + RearLeftWheelSpeedOffsetFh5, value.Z);
                    Mw.M.WriteMemory(PlayerCarEntity + RearRightWheelSpeedOffsetFh5, value.W);
                    break;
                }
                default:
                {
                    Mw.M.WriteMemory(PlayerCarEntity + FrontLeftWheelSpeedOffsetFh4, value.X);
                    Mw.M.WriteMemory(PlayerCarEntity + FrontRightWheelSpeedOffsetFh4, value.Y);
                    Mw.M.WriteMemory(PlayerCarEntity + RearLeftWheelSpeedOffsetFh4, value.Z);
                    Mw.M.WriteMemory(PlayerCarEntity + RearRightWheelSpeedOffsetFh4, value.W);
                    break;
                }
            }
        }
    }
    
    #endregion
}