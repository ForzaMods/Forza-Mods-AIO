using System;
using System.Numerics;
using static Forza_Mods_AIO.MainWindow;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.Entities;

public abstract class CarEntity
{
    private static UIntPtr PlayerCarEntity;

    #region Floats
    
    public static float Gravity
    {
        get
        {
            PlayerCarEntity = mw.m.ReadMemory<UIntPtr>(Self_Vehicle_ASM.CodeCave7 + 65);
            return mw.m.ReadMemory<float>(PlayerCarEntity + 0x8);  
        }
        set
        {
            PlayerCarEntity = mw.m.ReadMemory<UIntPtr>(Self_Vehicle_ASM.CodeCave7 + 65);
            mw.m.WriteMemory(PlayerCarEntity + 0x8, value);
        }
    }

    public static float Acceleration
    {
        get
        {
            PlayerCarEntity = mw.m.ReadMemory<UIntPtr>(Self_Vehicle_ASM.CodeCave7 + 65);
            return mw.m.ReadMemory<float>(PlayerCarEntity + 0xC);  
        }
        set
        {
            PlayerCarEntity = mw.m.ReadMemory<UIntPtr>(Self_Vehicle_ASM.CodeCave7 + 65);
            mw.m.WriteMemory(PlayerCarEntity + 0xC, value);
        }
    }

    public static float Yaw
    {
        get
        {
            PlayerCarEntity = mw.m.ReadMemory<UIntPtr>(Self_Vehicle_ASM.CodeCave7 + 65);
            return mw.gvp.Name == "Forza Horizon 4" ? 
                mw.m.ReadMemory<float>(PlayerCarEntity + 0x164) :
                mw.m.ReadMemory<float>(PlayerCarEntity + 0xF0);
        }
        set
        {
            PlayerCarEntity = mw.m.ReadMemory<UIntPtr>(Self_Vehicle_ASM.CodeCave7 + 65);
            _ = mw.gvp.Name == "Forza Horizon 4" ?                 
                mw.m.WriteMemory(PlayerCarEntity + 0x164, value) :
                mw.m.WriteMemory(PlayerCarEntity + 0xF0, value);
        }
    }
    
    public static float Roll
    {
        get
        {
            PlayerCarEntity = mw.m.ReadMemory<UIntPtr>(Self_Vehicle_ASM.CodeCave7 + 65);
            return mw.gvp.Name == "Forza Horizon 4" ? 
                mw.m.ReadMemory<float>(PlayerCarEntity + 0x148) :
                mw.m.ReadMemory<float>(PlayerCarEntity + 0xF4); 
        }
        set
        {
            PlayerCarEntity = mw.m.ReadMemory<UIntPtr>(Self_Vehicle_ASM.CodeCave7 + 65);
            _ = mw.gvp.Name == "Forza Horizon 4" ?                 
                mw.m.WriteMemory(PlayerCarEntity + 0x148, value) :
                mw.m.WriteMemory(PlayerCarEntity + 0xF4, value);
        }
    }
    
    public static float Pitch
    {
        get
        {
            PlayerCarEntity = mw.m.ReadMemory<UIntPtr>(Self_Vehicle_ASM.CodeCave7 + 65);
            return mw.gvp.Name == "Forza Horizon 4" ? 
                mw.m.ReadMemory<float>(PlayerCarEntity + 0x150) :
                mw.m.ReadMemory<float>(PlayerCarEntity + 0x108); 
        }
        set
        {
            PlayerCarEntity = mw.m.ReadMemory<UIntPtr>(Self_Vehicle_ASM.CodeCave7 + 65);
            _ = mw.gvp.Name == "Forza Horizon 4" ?                 
                mw.m.WriteMemory(PlayerCarEntity + 0x150, value) :
                mw.m.WriteMemory(PlayerCarEntity + 0x108, value);
        }
    }
    #endregion
    
    #region Vectors

    
    public static Vector3 LinearVelocity
    {
        get
        {
            PlayerCarEntity = mw.m.ReadMemory<UIntPtr>(Self_Vehicle_ASM.CodeCave7 + 65);
            return mw.m.ReadMemory<Vector3>(PlayerCarEntity + 0x20);
        }
        set
        {
            PlayerCarEntity = mw.m.ReadMemory<UIntPtr>(Self_Vehicle_ASM.CodeCave7 + 65);
            mw.m.WriteMemory(PlayerCarEntity + 0x20, value);
        }
    }
    
    public static Vector3 AngularVelocity
    {
        get
        {
            PlayerCarEntity = mw.m.ReadMemory<UIntPtr>(Self_Vehicle_ASM.CodeCave7 + 65);
            return mw.m.ReadMemory<Vector3>(PlayerCarEntity + 0x30);
        }
        set
        {
            PlayerCarEntity = mw.m.ReadMemory<UIntPtr>(Self_Vehicle_ASM.CodeCave7 + 65);
            mw.m.WriteMemory(PlayerCarEntity + 0x30, value);
        }
    }
    
    public static Vector3 Position
    {
        get
        {
            PlayerCarEntity = mw.m.ReadMemory<UIntPtr>(Self_Vehicle_ASM.CodeCave7 + 65);

            return mw.gvp.Name == "Forza Horizon 5" ? 
                mw.m.ReadMemory<Vector3>(PlayerCarEntity + 0x50) :
                mw.m.ReadMemory<Vector3>(PlayerCarEntity + 0x40);
        }
        set
        {
            PlayerCarEntity = mw.m.ReadMemory<UIntPtr>(Self_Vehicle_ASM.CodeCave7 + 65);
            _ = mw.gvp.Name == "Forza Horizon 5" ?                 
                mw.m.WriteMemory(PlayerCarEntity + 0x50, value) :
                mw.m.WriteMemory(PlayerCarEntity + 0x40, value);
        }
    }

    public static Vector3 Rotation
    {
        get
        {
            PlayerCarEntity = mw.m.ReadMemory<UIntPtr>(Self_Vehicle_ASM.CodeCave7 + 65);
            return new Vector3
            {
                X = mw.m.ReadMemory<float>(PlayerCarEntity + 0x88),
                Y = mw.m.ReadMemory<float>(PlayerCarEntity + 0x80),
                Z = mw.m.ReadMemory<float>(PlayerCarEntity + 0x94)
            };
        }
        set
        {
            PlayerCarEntity = mw.m.ReadMemory<UIntPtr>(Self_Vehicle_ASM.CodeCave7 + 65);
            mw.m.WriteMemory(PlayerCarEntity + 0x88, value.X);
            mw.m.WriteMemory(PlayerCarEntity + 0x80, value.Y);
            mw.m.WriteMemory(PlayerCarEntity + 0x94, value.Z);
        }
    }

    public static Vector4 WheelSpeed
    {
        get
        {
            PlayerCarEntity = mw.m.ReadMemory<UIntPtr>(Self_Vehicle_ASM.CodeCave7 + 65);
            
            if (mw.gvp.Name == "Forza Horizon 5")
            {
                return new Vector4
                {
                    X = mw.m.ReadMemory<float>(PlayerCarEntity + 0x26C0),
                    Y = mw.m.ReadMemory<float>(PlayerCarEntity + 0x3180),
                    Z = mw.m.ReadMemory<float>(PlayerCarEntity + 0x4700),
                    W = mw.m.ReadMemory<float>(PlayerCarEntity + 0x3C40)
                };
            }
            
            return new Vector4
            {
                X = mw.m.ReadMemory<float>(PlayerCarEntity + 0x3C40),
                Y = mw.m.ReadMemory<float>(PlayerCarEntity + 0x339C),
                Z = mw.m.ReadMemory<float>(PlayerCarEntity + 0x5F5C),
                W = mw.m.ReadMemory<float>(PlayerCarEntity + 0x497C)
            };
        }
        set
        {
            PlayerCarEntity = mw.m.ReadMemory<UIntPtr>(Self_Vehicle_ASM.CodeCave7 + 65);
            
            if (mw.gvp.Name == "Forza Horizon 5")
            {
                mw.m.WriteMemory(PlayerCarEntity + 0x26C0, value.X);
                mw.m.WriteMemory(PlayerCarEntity + 0x3180, value.Y);
                mw.m.WriteMemory(PlayerCarEntity + 0x4700, value.Z);
                mw.m.WriteMemory(PlayerCarEntity + 0x3C40, value.W);
                return;
            }
            
            mw.m.WriteMemory(PlayerCarEntity + 0x3C40, value.X);
            mw.m.WriteMemory(PlayerCarEntity + 0x339C, value.Y);
            mw.m.WriteMemory(PlayerCarEntity + 0x5F5C, value.Z);
            mw.m.WriteMemory(PlayerCarEntity + 0x497C, value.W);
        }
    }
    
    #endregion
}