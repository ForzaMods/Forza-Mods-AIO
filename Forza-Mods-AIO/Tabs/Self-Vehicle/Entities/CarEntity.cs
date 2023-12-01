using System;
using System.Numerics;
using System.Threading.Tasks;
using Forza_Mods_AIO.Resources;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.SelfVehicleAddresses;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.Entities;

public abstract class CarEntity
{
    public static UIntPtr PlayerCarEntity;
    public static readonly Detour BaseDetour = new();
    private const string BaseDetourBytesFh5 = "48 81 E9 70 05 00 00 48 89 0D 0C 00 00 00 48 81 C1 70 05 00 00";
    private const string BaseDetourBytesFh4 = "48 81 E9 60 05 00 00 48 89 0D 0C 00 00 00 48 81 C1 60 05 00 00";
    
    private static void Hook()
    {
        if (!BaseDetour.IsHooked)
        {
            var baseDetourBytes = Mw.Gvp.Name == "Forza Horizon 5" ? BaseDetourBytesFh5 : BaseDetourBytesFh4;
            BaseDetour.Setup(null, BaseAddrHook, baseDetourBytes, 8, true);
            Task.Delay(25).Wait();
        }

        PlayerCarEntity = BaseDetour.ReadVariable<UIntPtr>();
    }
    
    #region Floats
    
    public static float Gravity
    {
        get
        {
            Hook();
            return Mw.M.ReadMemory<float>(PlayerCarEntity + 0x8);  
        }
        set
        {
            Hook();
            Mw.M.WriteMemory(PlayerCarEntity + 0x8, value);
        }
    }

    public static float Acceleration
    {
        get
        {
            Hook();
            return Mw.M.ReadMemory<float>(PlayerCarEntity + 0xC);  
        }
        set
        {
            Hook();
            Mw.M.WriteMemory(PlayerCarEntity + 0xC, value);
        }
    }

    public static float Yaw
    {
        get
        {
            Hook();
            return Mw.Gvp.Name == "Forza Horizon 4" ? 
                Mw.M.ReadMemory<float>(PlayerCarEntity + 0x164) :
                Mw.M.ReadMemory<float>(PlayerCarEntity + 0xF0);
        }
        set
        {
            Hook();
            _ = Mw.Gvp.Name == "Forza Horizon 4" ?                 
                Mw.M.WriteMemory(PlayerCarEntity + 0x164, value) :
                Mw.M.WriteMemory(PlayerCarEntity + 0xF0, value);
        }
    }
    
    public static float Roll
    {
        get
        {
            Hook();
            return Mw.Gvp.Name == "Forza Horizon 4" ? 
                Mw.M.ReadMemory<float>(PlayerCarEntity + 0x148) :
                Mw.M.ReadMemory<float>(PlayerCarEntity + 0xF4); 
        }
        set
        {
            Hook();
            _ = Mw.Gvp.Name == "Forza Horizon 4" ?                 
                Mw.M.WriteMemory(PlayerCarEntity + 0x148, value) :
                Mw.M.WriteMemory(PlayerCarEntity + 0xF4, value);
        }
    }
    
    public static float Pitch
    {
        get
        {
            Hook();
            return Mw.Gvp.Name == "Forza Horizon 4" ? 
                Mw.M.ReadMemory<float>(PlayerCarEntity + 0x150) :
                Mw.M.ReadMemory<float>(PlayerCarEntity + 0x108); 
        }
        set
        {
            Hook();
            _ = Mw.Gvp.Name == "Forza Horizon 4" ?                 
                Mw.M.WriteMemory(PlayerCarEntity + 0x150, value) :
                Mw.M.WriteMemory(PlayerCarEntity + 0x108, value);
        }
    }
    #endregion
    
    #region Vectors

    
    public static Vector3 LinearVelocity
    {
        get
        {
            Hook();
            return Mw.M.ReadMemory<Vector3>(PlayerCarEntity + 0x20);
        }
        set
        {
            Hook();
            Mw.M.WriteMemory(PlayerCarEntity + 0x20, value);
        }
    }
    
    public static Vector3 AngularVelocity
    {
        get
        {
            Hook();
            return Mw.M.ReadMemory<Vector3>(PlayerCarEntity + 0x30);
        }
        set
        {
            Hook();
            Mw.M.WriteMemory(PlayerCarEntity + 0x30, value);
        }
    }
    
    public static Vector3 Position
    {
        get
        {
            Hook();
            return Mw.Gvp.Name == "Forza Horizon 5" ? 
                Mw.M.ReadMemory<Vector3>(PlayerCarEntity + 0x50) :
                Mw.M.ReadMemory<Vector3>(PlayerCarEntity + 0x40);
        }
        set
        {
            Hook();
            _ = Mw.Gvp.Name == "Forza Horizon 5" ?                 
                Mw.M.WriteMemory(PlayerCarEntity + 0x50, value) :
                Mw.M.WriteMemory(PlayerCarEntity + 0x40, value);
        }
    }

    public static Vector3 Rotation
    {
        get
        {
            Hook();
            return new Vector3
            {
                X = Mw.M.ReadMemory<float>(PlayerCarEntity + 0x88),
                Y = Mw.M.ReadMemory<float>(PlayerCarEntity + 0x80),
                Z = Mw.M.ReadMemory<float>(PlayerCarEntity + 0x94)
            };
        }
        set
        {
            Hook();
            Mw.M.WriteMemory(PlayerCarEntity + 0x88, value.X);
            Mw.M.WriteMemory(PlayerCarEntity + 0x80, value.Y);
            Mw.M.WriteMemory(PlayerCarEntity + 0x94, value.Z);
        }
    }

    public static Vector4 WheelSpeed
    {
        get
        {
            Hook();
            if (Mw.Gvp.Name == "Forza Horizon 5")
            {
                return new Vector4
                {
                    X = Mw.M.ReadMemory<float>(PlayerCarEntity + 0x26C0),
                    Y = Mw.M.ReadMemory<float>(PlayerCarEntity + 0x3180),
                    Z = Mw.M.ReadMemory<float>(PlayerCarEntity + 0x4700),
                    W = Mw.M.ReadMemory<float>(PlayerCarEntity + 0x3C40)
                };
            }
            
            return new Vector4
            {
                X = Mw.M.ReadMemory<float>(PlayerCarEntity + 0x3C40),
                Y = Mw.M.ReadMemory<float>(PlayerCarEntity + 0x339C),
                Z = Mw.M.ReadMemory<float>(PlayerCarEntity + 0x5F5C),
                W = Mw.M.ReadMemory<float>(PlayerCarEntity + 0x497C)
            };
        }
        set
        {
            Hook();
            if (Mw.Gvp.Name == "Forza Horizon 5")
            {
                Mw.M.WriteMemory(PlayerCarEntity + 0x26C0, value.X);
                Mw.M.WriteMemory(PlayerCarEntity + 0x3180, value.Y);
                Mw.M.WriteMemory(PlayerCarEntity + 0x4700, value.Z);
                Mw.M.WriteMemory(PlayerCarEntity + 0x3C40, value.W);
                return;
            }
            
            Mw.M.WriteMemory(PlayerCarEntity + 0x3C40, value.X);
            Mw.M.WriteMemory(PlayerCarEntity + 0x339C, value.Y);
            Mw.M.WriteMemory(PlayerCarEntity + 0x5F5C, value.Z);
            Mw.M.WriteMemory(PlayerCarEntity + 0x497C, value.W);
        }
    }
    
    #endregion
}