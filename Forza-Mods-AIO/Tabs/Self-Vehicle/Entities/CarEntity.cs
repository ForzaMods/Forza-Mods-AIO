using System;
using System.Linq.Expressions;
using System.Numerics;
using System.Threading.Tasks;
using Forza_Mods_AIO.Resources;

using static System.Math;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Resources.GameVerPlat.GameType;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.SelfVehicleAddresses;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.Entities;

public abstract class CarEntity
{
    public static UIntPtr PlayerCarEntity { get; private set; }
    public static readonly Detour BaseDetour = new();
    private const string BaseFh5 = "48 81 E9 70 05 00 00 48 89 0D 0C 00 00 00 48 81 C1 70 05 00 00";
    private const string BaseFh4 = "48 81 E9 60 05 00 00 48 89 0D 0C 00 00 00 48 81 C1 60 05 00 00";
    private const string BaseFm8 = "48 81 E9 D0 04 00 00 48 89 0D 0C 00 00 00 48 81 C1 D0 04 00 00";
    private const string OrigFh5 = "0F 2F B1 1C BE 00 00";
    private const string OrigFh4 = "F3 0F 10 81 90 01 00 00";
    private const string OrigFm8 = "0F 2F B1 A0 71 00 00";
    
    public static async void Hook()
    {
        if (!BaseDetour.IsHooked)
        {
            SetupHook();    
        }

        while ((PlayerCarEntity = BaseDetour.ReadVariable<UIntPtr>()) == 0)
        {
            await Task.Delay(5);
        }
    }

    private static void SetupHook()
    {
        var baseDetourBytes = Mw.Gvp.Type switch
        {
            Fm8 => BaseFm8,
            Fh5 => BaseFh5,
            Fh4 => BaseFh4,
            _ => string.Empty
        };
            
        var orig = Mw.Gvp.Type switch
        {
            Fm8 => OrigFm8,
            Fh5 => OrigFh5,
            Fh4 => OrigFh4,
            _ => string.Empty
        };

        var isFh4 = Mw.Gvp.Type == Fh4;
        var replace = isFh4 ? 8 : 7;
        if (!BaseDetour.Setup(BaseAddrHook, orig, baseDetourBytes, replace, true, 0, true))
        {
            return;
        }

        var isFm8 = Mw.Gvp.Type == Fm8;
        if (!isFm8)
        {
            return;
        }
            
        Bypass.AddProtectAddress(GravityProtectAddr);
        Mw.M.WriteArrayMemory(GravityProtectAddr, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 });
        Mw.M.WriteArrayMemory(AccelProtectAddr, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 });
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

    public static double CarSpeed => GetCarSpeed();
    public static double AverageWheelSpeed => GetAverageWheelSpeed();
    
    #endregion
    
    #region Vectors

    private const int LinVelOffset = 0x20;
    private const int LinVelOffsetFm8 = 0x10;
    public static Vector3 LinearVelocity
    {
        get
        {
            Hook();
            return Mw.Gvp.Type switch
            {
                Fm8 => Mw.M.ReadMemory<Vector3>(PlayerCarEntity + LinVelOffsetFm8),
                Fh4 or Fh5 => Mw.M.ReadMemory<Vector3>(PlayerCarEntity + LinVelOffset),
                _ => new Vector3()
            };
        }
        set
        {
            Hook();
            _ = Mw.Gvp.Type switch
            {
                Fm8 => Mw.M.WriteMemory(PlayerCarEntity + LinVelOffsetFm8, value),
                Fh4 or Fh5 => Mw.M.WriteMemory(PlayerCarEntity + LinVelOffset, value),
                _ => false
            };
        }
    }
    
    private const int AngVelOffset = 0x30;
    private const int AngVelOffsetFm8 = 0x20;
    public static Vector3 AngularVelocity
    {
        get
        {
            Hook();
            return Mw.Gvp.Type switch
            {
                Fh4 or Fh5 => Mw.M.ReadMemory<Vector3>(PlayerCarEntity + AngVelOffset),
                Fm8 => Mw.M.ReadMemory<Vector3>(PlayerCarEntity + AngVelOffsetFm8),
                _ => throw new IndexOutOfRangeException()
            };
        }
        set
        {
            Hook();
            _ = Mw.Gvp.Type switch
            {
                Fm8 => Mw.M.WriteMemory(PlayerCarEntity + AngVelOffsetFm8, value),
                Fh4 or Fh5 => Mw.M.WriteMemory(PlayerCarEntity + AngVelOffset, value),
                _ => false
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
            return Mw.Gvp.Type switch
            {
                Fm8 => Mw.M.ReadMemory<Vector3>(PlayerCarEntity + PosOffsetFm8),
                Fh5 => Mw.M.ReadMemory<Vector3>(PlayerCarEntity + PosOffsetFh5),
                Fh4 => Mw.M.ReadMemory<Vector3>(PlayerCarEntity + PosOffsetFh4),
                _ => new Vector3()
            };
        }
        set
        {
            Hook();
            _ = Mw.Gvp.Type switch
            {
                Fm8 => Mw.M.WriteMemory(PlayerCarEntity + PosOffsetFm8, value),
                Fh5 => Mw.M.WriteMemory(PlayerCarEntity + PosOffsetFh5, value),
                Fh4 => Mw.M.WriteMemory(PlayerCarEntity + PosOffsetFh4, value),
                _ => false
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
            return Mw.Gvp.Type switch
            {
                Fm8 => Mw.M.ReadMemory<Matrix4x4>(PlayerCarEntity + RotationOffsetFm8),
                Fh5 => Mw.M.ReadMemory<Matrix4x4>(PlayerCarEntity + RotationOffsetFh5),
                Fh4 => Mw.M.ReadMemory<Matrix4x4>(PlayerCarEntity + RotationOffsetFh4),
                _ => new Matrix4x4()
            };
        }
        set
        {
            Hook();
            _ = Mw.Gvp.Type switch
            {
                Fm8 => Mw.M.WriteMemory(PlayerCarEntity + RotationOffsetFm8, value),
                Fh5 => Mw.M.WriteMemory(PlayerCarEntity + RotationOffsetFh5, value),
                Fh4 => Mw.M.WriteMemory(PlayerCarEntity + RotationOffsetFh4, value),
                _  => false
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
            return Mw.Gvp.Type switch
            {
                Fm8 => new Vector4
                {
                    X = Mw.M.ReadMemory<float>(PlayerCarEntity + FrontLeftWheelSpeedOffsetFm8),
                    Y = Mw.M.ReadMemory<float>(PlayerCarEntity + FrontRightWheelSpeedOffsetFm8),
                    Z = Mw.M.ReadMemory<float>(PlayerCarEntity + RearLeftWheelSpeedOffsetFm8),
                    W = Mw.M.ReadMemory<float>(PlayerCarEntity + RearRightWheelSpeedOffsetFm8)
                },
                Fh5 => new Vector4
                {
                    X = Mw.M.ReadMemory<float>(PlayerCarEntity + FrontLeftWheelSpeedOffsetFh5),
                    Y = Mw.M.ReadMemory<float>(PlayerCarEntity + FrontRightWheelSpeedOffsetFh5),
                    Z = Mw.M.ReadMemory<float>(PlayerCarEntity + RearLeftWheelSpeedOffsetFh5),
                    W = Mw.M.ReadMemory<float>(PlayerCarEntity + RearRightWheelSpeedOffsetFh5)
                },
                Fh4 => new Vector4
                {
                    X = Mw.M.ReadMemory<float>(PlayerCarEntity + FrontLeftWheelSpeedOffsetFh4),
                    Y = Mw.M.ReadMemory<float>(PlayerCarEntity + FrontRightWheelSpeedOffsetFh4),
                    Z = Mw.M.ReadMemory<float>(PlayerCarEntity + RearLeftWheelSpeedOffsetFh4),
                    W = Mw.M.ReadMemory<float>(PlayerCarEntity + RearRightWheelSpeedOffsetFh4)
                },
                _ => new Vector4()
            };
        }
        set
        {
            Hook();
            switch (Mw.Gvp.Type)
            {
                case Fm8:
                {
                    Mw.M.WriteMemory(PlayerCarEntity + FrontLeftWheelSpeedOffsetFm8, value.X);
                    Mw.M.WriteMemory(PlayerCarEntity + FrontRightWheelSpeedOffsetFm8, value.Y);
                    Mw.M.WriteMemory(PlayerCarEntity + RearLeftWheelSpeedOffsetFm8, value.Z);
                    Mw.M.WriteMemory(PlayerCarEntity + RearRightWheelSpeedOffsetFm8, value.W);
                    break;
                }
                case Fh5:
                {
                    Mw.M.WriteMemory(PlayerCarEntity + FrontLeftWheelSpeedOffsetFh5, value.X);
                    Mw.M.WriteMemory(PlayerCarEntity + FrontRightWheelSpeedOffsetFh5, value.Y);
                    Mw.M.WriteMemory(PlayerCarEntity + RearLeftWheelSpeedOffsetFh5, value.Z);
                    Mw.M.WriteMemory(PlayerCarEntity + RearRightWheelSpeedOffsetFh5, value.W);
                    break;
                }
                case Fh4:
                {
                    Mw.M.WriteMemory(PlayerCarEntity + FrontLeftWheelSpeedOffsetFh4, value.X);
                    Mw.M.WriteMemory(PlayerCarEntity + FrontRightWheelSpeedOffsetFh4, value.Y);
                    Mw.M.WriteMemory(PlayerCarEntity + RearLeftWheelSpeedOffsetFh4, value.Z);
                    Mw.M.WriteMemory(PlayerCarEntity + RearRightWheelSpeedOffsetFh4, value.W);
                    break;
                }
                case None:
                default:
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }
    }
    
    #endregion
    
    private static double GetCarSpeed()
    {
        var velocitySquaredSum = Pow(LinearVelocity.X, 2) + Pow(LinearVelocity.Y, 2) + Pow(LinearVelocity.Z, 2);
        var speedMetersPerSecond = Sqrt(velocitySquaredSum);
        var speedMilesPerHour = speedMetersPerSecond * 2.23694;
        return speedMilesPerHour;
    }

    private static double GetAverageWheelSpeed()
    {
        var frontLeft = WheelSpeed.X;
        var frontRight = WheelSpeed.Y;
        var rearLeft = WheelSpeed.Z;
        var rearRight = WheelSpeed.W;
        var sum = frontLeft + frontRight + rearLeft + rearRight;
        var average = Convert.ToDouble(sum) / 4;
        return average;
    }
}