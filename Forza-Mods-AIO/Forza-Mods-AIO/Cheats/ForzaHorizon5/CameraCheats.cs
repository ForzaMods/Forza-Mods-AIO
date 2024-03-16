using static Forza_Mods_AIO.Resources.Memory;

namespace Forza_Mods_AIO.Cheats.ForzaHorizon5;

public class CameraCheats : CheatsUtilities, ICheatsBase
{
    public UIntPtr CameraAddress, CameraDetourAddress;
    
    public UIntPtr ChaseAddress;
    public UIntPtr ChaseFarAddress;
    public UIntPtr DriverAddress;
    public UIntPtr HoodAddress;
    public UIntPtr BumperAddress;
    public bool WereLimitersScanned;

    public async Task CheatLimiters()
    {
        WereLimitersScanned = false;
        ChaseAddress = 0;
        ChaseFarAddress = 0;
        DriverAddress = 0;
        HoodAddress = 0;
        BumperAddress = 0;
        
        var processMainModule = GetInstance().MProc.Process.MainModule;
        if (processMainModule == null)
        {
            return;
        }

        var successCount = 0; 
        var minRange = processMainModule.BaseAddress;
        var maxRange = minRange + processMainModule.ModuleMemorySize;

        const string chaseSig = "90 40 CD CC 8C 40 1F 85 2B 3F 00 00 00 40";
        var chaseList = await GetInstance().AoBScan(minRange, maxRange, chaseSig, true);
        var chaseEnumerable = chaseList as UIntPtr[] ?? chaseList.ToArray();
        if (chaseEnumerable.Length != 2)
        {
            ShowError("Chase Camera Limiters", chaseSig);
            goto skipScans;
        }

        ChaseAddress = chaseEnumerable.FirstOrDefault() - 10;
        ChaseFarAddress = chaseEnumerable.LastOrDefault() - 10;
        ++successCount;

        const string driverHoodSig = "CD CC 4C 3E 00 50 43 47 00 00 34 42 00 00 20";
        var driverHoodList = await GetInstance().AoBScan(minRange, maxRange, driverHoodSig, true);
        var driverHoodEnumerable = driverHoodList as UIntPtr[] ?? driverHoodList.ToArray();
        if (driverHoodEnumerable.Length != 2)
        {
            ShowError("Bumper/Hood Camera Limiters", driverHoodSig);
            goto skipScans;
        }

        HoodAddress = driverHoodEnumerable.FirstOrDefault() - 0x24;
        DriverAddress = driverHoodEnumerable.LastOrDefault() - 0x24;
        ++successCount;

        const string bumperSig = "00 CD CC 4C 3E ? ? ? 47 00 ? 54";
        var bumperList = await GetInstance().AoBScan(minRange, maxRange, bumperSig, true);
        var bumperEnumerable = bumperList as UIntPtr[] ?? bumperList.ToArray();
        if (bumperEnumerable.Length == 0)
        {
            ShowError("Bumper Camera Limiter", bumperSig);
            goto skipScans;
        }

        BumperAddress = bumperEnumerable.FirstOrDefault() - 0x23;
        ++successCount;

        skipScans:
        WereLimitersScanned = successCount == 3;
    }

    public async Task CheatCamera()
    {
        CameraAddress = 0;
        CameraDetourAddress = 0;

        const string sig = "0F 10 ? B0 ? 0F 28 ? ? ? F3 0F";
        CameraAddress = await SmartAobScan(sig);

        if (CameraAddress > 0)
        {
            if (Resources.Cheats.GetClass<Bypass>().CrcFuncDetourAddress == 0)
            {
                await Resources.Cheats.GetClass<Bypass>().DisableCrcChecks();
            }
            
            if (Resources.Cheats.GetClass<Bypass>().CrcFuncDetourAddress == 0) return;
            
            var asm = new byte[]
            {
                0x0F, 0x10, 0x01, 0xB0, 0x01, 0x80, 0x3D, 0x4D, 0x00, 0x00, 0x00, 0x01, 0x75, 0x16, 0x81, 0x79, 0x26,
                0xEC, 0x41, 0x00, 0x00, 0x75, 0x02, 0xEB, 0x2A, 0x81, 0x79, 0x26, 0x16, 0x43, 0x00, 0x00, 0x75, 0x0B,
                0xEB, 0x1F, 0x80, 0x3D, 0x33, 0x00, 0x00, 0x00, 0x01, 0x75, 0x27, 0x81, 0x79, 0x26, 0x96, 0x42, 0x67,
                0x7B, 0x75, 0x02, 0xEB, 0x15, 0x81, 0x79, 0x26, 0x54, 0x44, 0x4D, 0xA1, 0x75, 0x13, 0xEB, 0x0A, 0xF3,
                0x0F, 0x10, 0x05, 0x0F, 0x00, 0x00, 0x00, 0xEB, 0x07, 0x0F, 0x10, 0x05, 0x0B, 0x00, 0x00, 0x00
            };

            CameraDetourAddress = GetInstance().CreateDetour(CameraAddress, asm, 5);
            return;
        }
        
        ShowError("Fov and Camera Offset", sig);
    }
    
    public void Cleanup()
    {
        var mem = GetInstance();

        if (CameraAddress > 0)
        {
            mem.WriteArrayMemory(CameraAddress, new byte[] { 0x0F, 0x10, 0x01, 0xB0, 0x01 });
            Free(CameraDetourAddress);
        }
    }

    public void Reset()
    {
        WereLimitersScanned = false;
        var fields = typeof(CameraCheats).GetFields().Where(f => f.FieldType == typeof(UIntPtr));
        foreach (var field in fields)
        {
            field.SetValue(this, UIntPtr.Zero);
        }
    }
}