using static Forza_Mods_AIO.Resources.Memory;

namespace Forza_Mods_AIO.Cheats.ForzaHorizon5;

public class CarCheats : CheatsUtilities, ICheatsBase
{
    private const int LocalPlayerOffset = 0x11;
    
    public async Task<UIntPtr> GetLocalPlayer()
    {
        if (LocalPlayerHookDetourAddress == 0)
        {
            await CheatLocalPlayer();
        }

        return LocalPlayerHookDetourAddress == 0
            ? 0
            : GetInstance().ReadMemory<UIntPtr>(LocalPlayerHookDetourAddress + LocalPlayerOffset);
    }
    
    public UIntPtr LocalPlayerHookAddress, LocalPlayerHookDetourAddress;
    public UIntPtr AccelAddress, AccelDetourAddress;
    public UIntPtr GravityAddress, GravityDetourAddress;
    public UIntPtr WaypointAddress, WaypointDetourAddress;

    public async Task CheatLocalPlayer()
    {
        LocalPlayerHookAddress = 0;
        LocalPlayerHookDetourAddress = 0;

        const string sig = "F3 0F ? ? ? 49 8B ? 49 8B ? 0F 28";
        LocalPlayerHookAddress = await SmartAobScan(sig);
        if (LocalPlayerHookAddress > 0)
        {
            if (Resources.Cheats.GetClass<Bypass>().CrcFuncDetourAddress == 0)
            {
                await Resources.Cheats.GetClass<Bypass>().DisableCrcChecks();
            }

            if (Resources.Cheats.GetClass<Bypass>().CrcFuncDetourAddress <= 0) return;

            var asm = new byte[] { 0xF3, 0x0F, 0x10, 0x4F, 0x24, 0x48, 0x89, 0x3D, 0x05, 0x00, 0x00, 0x00 };
            LocalPlayerHookDetourAddress = GetInstance().CreateDetour(LocalPlayerHookAddress, asm, 5);
            return;
        }
        
        ShowError("Local Player Hook", sig);
    }

    public async Task CheatAccel()
    {
        AccelAddress = 0;
        AccelDetourAddress = 0;

        const string sig = "F3 0F ? ? ? 41 0F ? ? 0F C6 DB ? 41 0F";
        AccelAddress = await SmartAobScan(sig);
        if (AccelAddress > 0)
        {
            if (LocalPlayerHookDetourAddress == 0)
            {
                await CheatLocalPlayer();
            }

            if (LocalPlayerHookDetourAddress == 0)
            {
                return;
            }
            
            var localPlayerAddr = BitConverter.GetBytes(LocalPlayerHookDetourAddress + LocalPlayerOffset);
            var asm = new byte[]
            {
                0xF3, 0x0F, 0x10, 0x5D, 0x0C, 0x80, 0x3D, 0x20, 0x00, 0x00, 0x00, 0x01, 0x75, 0x19, 0x50, 0x48, 0xB8,
                localPlayerAddr[0], localPlayerAddr[1], localPlayerAddr[2], localPlayerAddr[3], localPlayerAddr[4],
                localPlayerAddr[5], localPlayerAddr[6], localPlayerAddr[7], 0x48, 0x39, 0x28, 0x75, 0x08, 0xF3, 0x0F,
                0x10, 0x1D, 0x07, 0x00, 0x00, 0x00, 0x58
            };
            AccelDetourAddress = GetInstance().CreateDetour(AccelAddress, asm, 5);
            return;
        }
        
        ShowError("Accel", sig);
    }
    
    public async Task CheatGravity()
    {
        GravityAddress = 0;
        GravityDetourAddress = 0;

        const string sig = "F3 0F ? ? ? F3 0F ? ? ? ? ? ? F3 0F ? ? ? ? ? ? 45 84 ? 74";
        GravityAddress = await SmartAobScan(sig);
        if (GravityAddress > 0)
        {
            if (LocalPlayerHookDetourAddress == 0)
            {
                await CheatLocalPlayer();
            }

            if (LocalPlayerHookDetourAddress == 0)
            {
                return;
            }
            
            var localPlayerAddr = BitConverter.GetBytes(LocalPlayerHookDetourAddress + LocalPlayerOffset);
            var asm = new byte[]
            {
                0x80, 0x3D, 0x27, 0x00, 0x00, 0x00, 0x01, 0x75, 0x1B, 0x50, 0x48, 0xB8, localPlayerAddr[0],
                localPlayerAddr[1], localPlayerAddr[2], localPlayerAddr[3], localPlayerAddr[4], localPlayerAddr[5],
                localPlayerAddr[6], localPlayerAddr[7], 0x48, 0x39, 0x18, 0x58, 0x75, 0x0A, 0xF3, 0x0F, 0x59, 0x0D,
                0x0D, 0x00, 0x00, 0x00, 0xEB, 0x05, 0xF3, 0x0F, 0x59, 0x4B, 0x08
            };
            GravityDetourAddress = GetInstance().CreateDetour(GravityAddress, asm, 5);
            return;
        }
        
        ShowError("Gravity", sig);
    }

    public async Task CheatWaypoint()
    {
        WaypointAddress = 0;
        WaypointDetourAddress = 0;

        const string sig = "0F 10 ? ? ? ? ? 0F 28 ? 0F C2 ? 00 0F 50";
        WaypointAddress = await SmartAobScan(sig);
        if (WaypointAddress > 0)
        {
            if (LocalPlayerHookDetourAddress == 0)
            {
                await CheatLocalPlayer();
            }

            if (LocalPlayerHookDetourAddress == 0)
            {
                return;
            }

            var localPlayerAddr = BitConverter.GetBytes(LocalPlayerHookDetourAddress + LocalPlayerOffset);
            var asm = new byte[]
            {
                0x0F, 0x10, 0x97, 0x30, 0x02, 0x00, 0x00, 0x80, 0x3D, 0x24, 0x00, 0x00, 0x00, 0x01, 0x75, 0x1D, 0x50,
                0x48, 0xB8, localPlayerAddr[0], localPlayerAddr[1], localPlayerAddr[2], localPlayerAddr[3],
                localPlayerAddr[4], localPlayerAddr[5], localPlayerAddr[6], localPlayerAddr[7], 0x48, 0x8B, 0x00, 0x48,
                0x85, 0xC0, 0x74, 0x09, 0x0F, 0x11, 0x50, 0x50, 0x44, 0x0F, 0x11, 0x78, 0x20, 0x58
            };
            
            WaypointDetourAddress = GetInstance().CreateDetour(WaypointAddress, asm, 7);
            return;
        }
        
        ShowError("Waypoint", sig);
    }
    
    public void Cleanup()
    {
        var memInstance = GetInstance();
        
        if (AccelDetourAddress > 0)
        {
            memInstance.WriteArrayMemory(AccelAddress, new byte[] { 0xF3, 0x0F, 0x10, 0x5D, 0x0C });
            Free(AccelDetourAddress);
        }

        if (GravityDetourAddress > 0)
        {
            memInstance.WriteArrayMemory(GravityAddress, new byte[] { 0xF3, 0x0F, 0x59, 0x4B, 0x08 });
            Free(GravityDetourAddress);
        }

        if (WaypointAddress > 0)
        {
            memInstance.WriteArrayMemory(WaypointAddress, new byte[] { 0x0F, 0x10, 0x97, 0x30, 0x02, 0x00, 0x00 });
            Free(WaypointDetourAddress);
        }
        
        if (LocalPlayerHookDetourAddress <= 0) return;
        memInstance.WriteArrayMemory(LocalPlayerHookAddress, new byte[] { 0xF3, 0x0F, 0x10, 0x4F, 0x24 });
        Free(LocalPlayerHookDetourAddress);
    }

    public void Reset()
    {
        var fields = typeof(CarCheats).GetFields().Where(f => f.FieldType == typeof(nuint));
        foreach (var field in fields)
        {
            field.SetValue(this, UIntPtr.Zero);
        }
    }
}