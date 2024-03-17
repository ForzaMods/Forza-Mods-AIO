using static Forza_Mods_AIO.Resources.Cheats;
using static Forza_Mods_AIO.Resources.Memory;

namespace Forza_Mods_AIO.Cheats.ForzaHorizon5;

public class EnvironmentCheats : CheatsUtilities, ICheatsBase
{
    private UIntPtr _sunRgbAddress;
    public UIntPtr SunRgbDetourAddress;

    public async Task CheatSunRgb()
    {
        _sunRgbAddress = 0;
        SunRgbDetourAddress = 0;

        const string sig = "41 0F ? ? 48 83 C4 ? 41 ? C3 48 8D";
        _sunRgbAddress = await SmartAobScan(sig);

        if (_sunRgbAddress > 0)
        {
            if (GetClass<Bypass>().CrcFuncDetourAddress == 0)
            {
                await GetClass<Bypass>().DisableCrcChecks();
            }
            
            if (GetClass<Bypass>().CrcFuncDetourAddress <= 0) return;

            var asm = new byte[]
            {
                0x80, 0x3D, 0x2B, 0x00, 0x00, 0x00, 0x01, 0x75, 0x1C, 0x48, 0x83, 0xEC, 0x10, 0xF3, 0x0F, 0x7F, 0x14,
                0x24, 0x0F, 0x10, 0x15, 0x1A, 0x00, 0x00, 0x00, 0x0F, 0x59, 0xDA, 0xF3, 0x0F, 0x6F, 0x14, 0x24, 0x48,
                0x83, 0xC4, 0x10, 0x41, 0x0F, 0x11, 0x1E, 0x48, 0x83, 0xC4, 0x20
            };

            SunRgbDetourAddress = GetInstance().CreateDetour(_sunRgbAddress, asm, 8);
            return;
        }
        
        ShowError("Sun rgb", sig);
    }
    
    public void Cleanup()
    {
        var mem = GetInstance();
        
        if (_sunRgbAddress > 0)
        {
            mem.WriteArrayMemory(_sunRgbAddress, new byte[] { 0x41, 0x0F, 0x11, 0x1E, 0x48, 0x83, 0xC4, 0x20 });
            Free(SunRgbDetourAddress);
        }
    }

    public void Reset()
    {
        var fields = typeof(EnvironmentCheats).GetFields().Where(f => f.FieldType == typeof(UIntPtr));
        foreach (var field in fields)
        {
            field.SetValue(this, UIntPtr.Zero);
        }
    }
}