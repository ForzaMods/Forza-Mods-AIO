namespace Forza_Mods_AIO.Cheats.ForzaHorizon5;

public class MiscCheats : CheatsUtilities, ICheatsBase
{
    public UIntPtr NameAddress, NameDetourAddress;

    public async Task CheatName()
    {
        NameAddress = 0;
        NameDetourAddress = 0;

        const string sig = "4C 8B ? 48 8B ? 41 FF ? ? 48 8B ? 48 85 ? 74 ? 48 8B ? 48 8B ? FF 90";
        NameAddress = await SmartAobScan(sig);

        if (NameAddress > 0)
        {
            if (Resources.Cheats.GetClass<Bypass>().CrcFuncDetourAddress == 0)
            {
                await Resources.Cheats.GetClass<Bypass>().DisableCrcChecks();
            }

            if (Resources.Cheats.GetClass<Bypass>().CrcFuncDetourAddress == 0) return;
            
            var asm = new byte[]
            {
                0x4C, 0x8B, 0x10, 0x80, 0x3D, 0x54, 0x00, 0x00, 0x00, 0x01, 0x75, 0x4A, 0x48, 0x8B, 0x88, 0xF0, 0x00,
                0x00, 0x00, 0x48, 0x85, 0xC9, 0x74, 0x3E, 0x48, 0x8B, 0x49, 0x30, 0x48, 0x85, 0xC9, 0x74, 0x35, 0x48,
                0x8B, 0x49, 0x28, 0x48, 0x85, 0xC9, 0x74, 0x2C, 0x48, 0x8B, 0x49, 0x08, 0x48, 0x85, 0xC9, 0x74, 0x23,
                0x56, 0x52, 0x41, 0x55, 0x48, 0x31, 0xF6, 0x48, 0x8D, 0x15, 0x1E, 0x00, 0x00, 0x00, 0x44, 0x8A, 0x2C,
                0x32, 0x44, 0x88, 0x2C, 0x31, 0x48, 0xFF, 0xC6, 0x48, 0x83, 0xFE, 0x40, 0x76, 0xEF, 0x41, 0x5D, 0x5A,
                0x5E, 0x48, 0x8B, 0xC8
            };

            NameDetourAddress = Resources.Memory.GetInstance().CreateDetour(NameAddress, asm, 6);
            return;
        }
        
        ShowError("Name spoofer", sig);
    }
    
    public void Cleanup()
    {
        var mem = Resources.Memory.GetInstance();
        
        if (NameAddress > 0)
        {
            mem.WriteArrayMemory(NameAddress, new byte[] { 0x4C, 0x8B, 0x10, 0x48, 0x8B, 0xC8 });
            Free(NameDetourAddress);
        }
    }

    public void Reset()
    {
        var fields = typeof(MiscCheats).GetFields().Where(f => f.FieldType == typeof(UIntPtr));
        foreach (var field in fields)
        {
            field.SetValue(this, UIntPtr.Zero);
        }
    }
}