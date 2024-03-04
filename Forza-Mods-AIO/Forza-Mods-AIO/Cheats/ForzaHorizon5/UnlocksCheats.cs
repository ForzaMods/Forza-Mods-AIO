namespace Forza_Mods_AIO.Cheats.ForzaHorizon5;

public class UnlocksCheats : CheatsUtilities, ICheatsBase
{
    public UIntPtr CreditsAddress, CreditsDetourAddress;
    public UIntPtr XpPointsAddress, XpPointsDetourAddress;
    public UIntPtr XpAddress, XpDetourAddress;
    public UIntPtr SpinsAddress, SpinsDetourAddress;
    public UIntPtr SkillPointsAddress, SkillPointsDetourAddress;
    public UIntPtr SeriesAddress, SeriesDetourAddress;
    public UIntPtr SeasonalAddress, SeasonalDetourAddress;

    public async Task CheatCredits()
    {
        CreditsAddress = 0;
        CreditsDetourAddress = 0;
        
        const string sig = "89 84 ? ? ? ? ? 4C 8D ? ? ? ? ? 48 8B ? 48 8D ? ? ? ? ? ? 48 8B";
        CreditsAddress = await SmartAobScan(sig);

        if (CreditsAddress > 0)
        {
            if (Resources.Cheats.GetClass<Bypass>().CrcFuncDetourAddress == 0)
            {
                await Resources.Cheats.GetClass<Bypass>().DisableCrcChecks();
            }
            
            if (Resources.Cheats.GetClass<Bypass>().CrcFuncDetourAddress <= 0) return;

            var asm = new byte[]
            {
                0x80, 0x3D, 0x3E, 0x00, 0x00, 0x00, 0x01, 0x75, 0x30, 0x80, 0x7F, 0xB4, 0x43, 0x75, 0x2A, 0x80, 0x7F,
                0xB5, 0x72, 0x75, 0x24, 0x80, 0x7F, 0xB6, 0x65, 0x75, 0x1E, 0x80, 0x7F, 0xB7, 0x64, 0x75, 0x18, 0x80,
                0x7F, 0xB8, 0x69, 0x75, 0x12, 0x80, 0x7F, 0xB9, 0x74, 0x75, 0x0C, 0x80, 0x7F, 0xBA, 0x73, 0x75, 0x06,
                0x8B, 0x05, 0x0D, 0x00, 0x00, 0x00, 0x89, 0x84, 0x24, 0x80, 0x00, 0x00, 0x00
            };

            CreditsDetourAddress = Resources.Memory.GetInstance().CreateDetour(CreditsAddress, asm, 7);
            return;
        }
        
        ShowError("Credits", sig);
    }

    public async Task CheatXp()
    {
        XpPointsAddress = 0;
        XpPointsDetourAddress = 0;
        XpAddress = 0;
        XpDetourAddress = 0;

        const string sig = "44 89 ? ? 8B 89 ? ? ? ? 85 C9";
        XpPointsAddress = await SmartAobScan(sig) + 4;
        if (XpPointsAddress > 4)
        {
            if (Resources.Cheats.GetClass<Bypass>().CrcFuncDetourAddress == 0)
            {
                await Resources.Cheats.GetClass<Bypass>().DisableCrcChecks();
            }
            
            if (Resources.Cheats.GetClass<Bypass>().CrcFuncDetourAddress <= 0) return;
            
            XpAddress = XpPointsAddress + 14;
            var pointsAsm = new byte[]
            {
                0x80, 0x3D, 0x14, 0x00, 0x00, 0x00, 0x01, 0x75, 0x07, 0xC6, 0x81, 0x88, 0x00, 0x00, 0x00, 0x01, 0x8B,
                0x89, 0x88, 0x00, 0x00, 0x00
            };

            var asm = new byte[]
            {
                0x41, 0x8B, 0x87, 0x8C, 0x00, 0x00, 0x00, 0x80, 0x3D, 0x0D, 0x00, 0x00, 0x00, 0x01, 0x75, 0x06, 0x8B,
                0x05, 0x06, 0x00, 0x00, 0x00
            };

            XpPointsDetourAddress = Resources.Memory.GetInstance().CreateDetour(XpPointsAddress, pointsAsm, 6);
            XpDetourAddress = Resources.Memory.GetInstance().CreateDetour(XpAddress, asm, 7);
            return;
        }
        
        ShowError("Xp", sig);
    }

    public async Task CheatSpins()
    {
        SpinsAddress = 0;
        SpinsDetourAddress = 0;

        const string sig = "48 89 5C 24 08 57 48 83 EC 20 48 8B FA 33 D2 48 8B 4F 10";
        SpinsAddress = await SmartAobScan(sig) + 28;

        if (SpinsAddress > 28)
        {
            if (Resources.Cheats.GetClass<Bypass>().CrcFuncDetourAddress == 0)
            {
                await Resources.Cheats.GetClass<Bypass>().DisableCrcChecks();
            }
            
            if (Resources.Cheats.GetClass<Bypass>().CrcFuncDetourAddress <= 0) return;
            
            var asm = new byte[]
            {
                0x80, 0x3D, 0x15, 0x00, 0x00, 0x00, 0x01, 0x75, 0x09, 0x8B, 0x15, 0x0E, 0x00, 0x00, 0x00, 0x89, 0x57,
                0x08, 0x33, 0xD2, 0x8B, 0x5F, 0x08
            };

            SpinsDetourAddress = Resources.Memory.GetInstance().CreateDetour(SpinsAddress, asm, 5);
            return;
        }
        
        ShowError("Spins", sig);
    }
    
    public async Task CheatSkillPoints()
    {
        SkillPointsAddress = 0;
        SkillPointsDetourAddress = 0;

        const string sig = "85 D2 78 32 48 89 5C 24 08 57 48 83 EC 20 8B DA 48 8B F9 48 8B 49 48";
        SkillPointsAddress = await SmartAobScan(sig) + 34;

        if (SkillPointsAddress > 34)
        {
            if (Resources.Cheats.GetClass<Bypass>().CrcFuncDetourAddress == 0)
            {
                await Resources.Cheats.GetClass<Bypass>().DisableCrcChecks();
            }
            
            if (Resources.Cheats.GetClass<Bypass>().CrcFuncDetourAddress <= 0) return;

            var asm = new byte[]
            {
                0x80, 0x3D, 0x12, 0x00, 0x00, 0x00, 0x01, 0x75, 0x06, 0x8B, 0x1D, 0x0B, 0x00, 0x00, 0x00, 0x33, 0xD2,
                0x89, 0x5F, 0x40
            };

            SkillPointsDetourAddress = Resources.Memory.GetInstance().CreateDetour(SkillPointsAddress, asm, 5);
            return;
        }
        
        ShowError("Skill points", sig);
    }
    
    public void Cleanup()
    {
        var mem = Resources.Memory.GetInstance();
        
        if (CreditsAddress > 0)
        {
            mem.WriteArrayMemory(CreditsAddress, new byte[] { 0x89, 0x84, 0x24, 0x80, 0x00, 0x00, 0x00 });
            Free(CreditsDetourAddress);
        }

        if (XpPointsAddress > 4)
        {
            mem.WriteArrayMemory(XpPointsAddress, new byte[] { 0x8B, 0x89, 0x88, 0x00, 0x00, 0x00 });
            Free(XpPointsDetourAddress);
        }

        if (XpAddress > 0)
        {
            mem.WriteArrayMemory(XpAddress, new byte[] { 0x41, 0x8B, 0x87, 0x8C, 0x00, 0x00, 0x00 });
            Free(XpDetourAddress);
        }

        if (SpinsAddress > 28)
        {
            mem.WriteArrayMemory(SpinsAddress, new byte[] { 0x33, 0xD2, 0x8B, 0x5F, 0x08 });
            Free(SpinsDetourAddress);
        }

        if (SkillPointsAddress > 34)
        {
            mem.WriteArrayMemory(SkillPointsAddress, new byte[] { 0x33, 0xD2, 0x89, 0x5F, 0x40 });
            Free(SkillPointsDetourAddress);
        }
    }

    public void Reset()
    {
        var fields = typeof(UnlocksCheats).GetFields().Where(f => f.FieldType == typeof(nuint));
        foreach (var field in fields)
        {
            field.SetValue(this, UIntPtr.Zero);
        }
    }
}