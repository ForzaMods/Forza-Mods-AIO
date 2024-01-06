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
    private const string BaseDetourBytesFh5 = "48 81 E9 70 05 00 00 48 89 0D 0C 00 00 00 48 81 C1 70 05 00 00";
    private const string BaseDetourBytesFh4 = "48 81 E9 60 05 00 00 48 89 0D 0C 00 00 00 48 81 C1 60 05 00 00";
    
    public static async void Hook()
    {
        if (!BaseDetour.IsHooked)
        {
            var baseDetourBytes = Mw.Gvp.Name == "Forza Horizon 5" ? BaseDetourBytesFh5 : BaseDetourBytesFh4;
            const string fh5 = "F3 0F 10 81 18 15 00 00";
            const string fh4 = "F3 0F 10 81 90 01 00 00";
            var orig = Mw.Gvp.Name.Contains('5') ? fh5 : fh4;
            BaseDetour.Setup(BaseAddrHook, orig, baseDetourBytes, 8, true, 0, true);
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

    private const int LinearVelocityOffset = 0x20;
    public static Vector3 LinearVelocity
    {
        get
        {
            Hook();
            return Mw.M.ReadMemory<Vector3>(PlayerCarEntity + LinearVelocityOffset);
        }
        set
        {
            Hook();
            Mw.M.WriteMemory(PlayerCarEntity + LinearVelocityOffset, value);
        }
    }
    
    private const int AngularVelocityOffset = 0x30;
    public static Vector3 AngularVelocity
    {
        set
        {
            Hook();
            Mw.M.WriteMemory(PlayerCarEntity + AngularVelocityOffset, value);
        }
    }

    private const int PositionOffsetFh5 = 0x50;
    private const int PositionOffsetFh4 = 0x40;
    public static Vector3 Position
    {
        get
        {
            Hook();
            return Mw.Gvp.Name == "Forza Horizon 5" ? 
                Mw.M.ReadMemory<Vector3>(PlayerCarEntity + PositionOffsetFh5) :
                Mw.M.ReadMemory<Vector3>(PlayerCarEntity + PositionOffsetFh4);
        }
        set
        {
            Hook();
            _ = Mw.Gvp.Name == "Forza Horizon 5" ?                 
                Mw.M.WriteMemory(PlayerCarEntity + PositionOffsetFh5, value) :
                Mw.M.WriteMemory(PlayerCarEntity + PositionOffsetFh4, value);
        }
    }

    private const int RotationOffsetFh5 = 0x80;
    private const int RotationOffsetFh4 = 0xF0;
    public static Matrix4x4 Rotation
    {
        get
        {
            Hook();
            return Mw.Gvp.Name == "Forza Horizon 5" ? 
                Mw.M.ReadMemory<Matrix4x4>(PlayerCarEntity + RotationOffsetFh5) :
                Mw.M.ReadMemory<Matrix4x4>(PlayerCarEntity + RotationOffsetFh4);
        }
        set
        {
            Hook();
            _ = Mw.Gvp.Name == "Forza Horizon 5" ?                 
                Mw.M.WriteMemory(PlayerCarEntity + RotationOffsetFh5, value) :
                Mw.M.WriteMemory(PlayerCarEntity + RotationOffsetFh4, value);
        }
    }

    private const int FrontLeftWheelSpeedOffsetFh5 = 0x26C0;
    private const int FrontRightWheelSpeedOffsetFh5 = 0x3180;
    private const int RearLeftWheelSpeedOffsetFh5 = 0x4700;
    private const int RearRightWheelSpeedOffsetFh5 = 0x3C40;
    
    private const int FrontLeftWheelSpeedOffsetFh4 = 0x3C40;
    private const int FrontRightWheelSpeedOffsetFh4 = 0x339C;
    private const int RearLeftWheelSpeedOffsetFh4 = 0x5F5C;
    private const int RearRightWheelSpeedOffsetFh4 = 0x497C;
    
    public static Vector4 WheelSpeed
    {
        get
        {
            Hook();
            if (Mw.Gvp.Name == "Forza Horizon 5")
            {
                return new Vector4
                {
                    X = Mw.M.ReadMemory<float>(PlayerCarEntity + FrontLeftWheelSpeedOffsetFh5),
                    Y = Mw.M.ReadMemory<float>(PlayerCarEntity + FrontRightWheelSpeedOffsetFh5),
                    Z = Mw.M.ReadMemory<float>(PlayerCarEntity + RearLeftWheelSpeedOffsetFh5),
                    W = Mw.M.ReadMemory<float>(PlayerCarEntity + RearRightWheelSpeedOffsetFh5)
                };
            }
            
            return new Vector4
            {
                X = Mw.M.ReadMemory<float>(PlayerCarEntity + FrontLeftWheelSpeedOffsetFh4),
                Y = Mw.M.ReadMemory<float>(PlayerCarEntity + FrontRightWheelSpeedOffsetFh4),
                Z = Mw.M.ReadMemory<float>(PlayerCarEntity + RearLeftWheelSpeedOffsetFh4),
                W = Mw.M.ReadMemory<float>(PlayerCarEntity + RearRightWheelSpeedOffsetFh4)
            };
        }
        set
        {
            Hook();
            if (Mw.Gvp.Name == "Forza Horizon 5")
            {
                Mw.M.WriteMemory(PlayerCarEntity + FrontLeftWheelSpeedOffsetFh5, value.X);
                Mw.M.WriteMemory(PlayerCarEntity + FrontRightWheelSpeedOffsetFh5, value.Y);
                Mw.M.WriteMemory(PlayerCarEntity + RearLeftWheelSpeedOffsetFh5, value.Z);
                Mw.M.WriteMemory(PlayerCarEntity + RearRightWheelSpeedOffsetFh5, value.W);
                return;
            }
            
            Mw.M.WriteMemory(PlayerCarEntity + FrontLeftWheelSpeedOffsetFh4, value.X);
            Mw.M.WriteMemory(PlayerCarEntity + FrontRightWheelSpeedOffsetFh4, value.Y);
            Mw.M.WriteMemory(PlayerCarEntity + RearLeftWheelSpeedOffsetFh4, value.Z);
            Mw.M.WriteMemory(PlayerCarEntity + RearRightWheelSpeedOffsetFh4, value.W);
        }
    }
    
    #endregion
}