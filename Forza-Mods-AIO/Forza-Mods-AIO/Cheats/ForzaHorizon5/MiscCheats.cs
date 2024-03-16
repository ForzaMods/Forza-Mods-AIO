using Forza_Mods_AIO.Resources;
using static Forza_Mods_AIO.Resources.Cheats;
using static Forza_Mods_AIO.Resources.Memory;

namespace Forza_Mods_AIO.Cheats.ForzaHorizon5;

public class MiscCheats : CheatsUtilities, ICheatsBase
{
    public UIntPtr NameAddress, NameDetourAddress;
    public UIntPtr SellFactorAddress, SellFactorDetourAddress;
    public UIntPtr PrizeScaleAddress, PrizeScaleDetourAddress;
    public UIntPtr SkillScoreMultiplierAddress, SkillScoreMultiplierDetourAddress;
    public UIntPtr DriftScoreMultiplierAddress, DriftScoreMultiplierDetourAddress;
    public UIntPtr SkillTreeWideEditAddress, SkillTreeWideEditDetourAddress;
    public UIntPtr SkillTreePerksCostAddress, SkillTreePerksCostDetourAddress;
    public UIntPtr MissionTimeScaleAddress, MissionTimeScaleDetourAddress;
    public UIntPtr TrailblazerTimeScaleAddress, TrailblazerTimeScaleDetourAddress;
    public UIntPtr UnbreakableSkillScoreAddress, UnbreakableSkillScoreDetourAddress;

    public async Task CheatName()
    {
        NameAddress = 0;
        NameDetourAddress = 0;

        var urtcbaseHandle = Imports.GetModuleHandle("ucrtbase.dll");
        NameAddress = Imports.GetProcAddress(urtcbaseHandle, "wcsncpy_s") + 0x98;
        
        if (NameAddress > 0x98)
        {
            var namePtr = await CheatNamePtr();
            if (namePtr == 0) return;

            var ptrBytes = BitConverter.GetBytes(namePtr);
            var asm = new byte[]
            {
                0x0F, 0xB7, 0x04, 0x1A, 0x80, 0x3D, 0x67, 0x00, 0x00, 0x00, 0x01, 0x75, 0x5D, 0x48, 0x8B, 0xF3, 0x48,
                0x01, 0xD6, 0x51, 0x48, 0xB9, ptrBytes[0], ptrBytes[1], ptrBytes[2], ptrBytes[3], ptrBytes[4],
                ptrBytes[5], ptrBytes[6], ptrBytes[7], 0x48, 0x8B, 0x09, 0x48, 0x8B, 0x89, 0xF0, 0x00, 0x00, 0x00, 0x48,
                0x85, 0xC9, 0x74, 0x3C, 0x48, 0x8B, 0x49, 0x30, 0x48, 0x85, 0xC9, 0x74, 0x33, 0x48, 0x8B, 0x49, 0x30,
                0x48, 0x85, 0xC9, 0x74, 0x2A, 0x48, 0x8B, 0x49, 0x08, 0x48, 0x85, 0xC9, 0x74, 0x21, 0x48, 0x39, 0xCE,
                0x72, 0x1C, 0x48, 0x83, 0xC1, 0x40, 0x48, 0x39, 0xCE, 0x77, 0x13, 0x48, 0x83, 0xE9, 0x40, 0x48, 0x29,
                0xCE, 0x48, 0x8D, 0x0D, 0x0F, 0x00, 0x00, 0x00, 0x48, 0x01, 0xF1, 0x8B, 0x01, 0x59, 0x48, 0x8B, 0xF7
            };

            NameDetourAddress = GetInstance().CreateDetour(NameAddress, asm, 7);
            return;
        }
        
        ShowError("Name Spoofer", "No signature, GetProcAddress is used.");
    }

    private static async Task<UIntPtr> CheatNamePtr()
    {
        const string sig = "E8 ? ? ? ? 4C 8B ? 48 8B ? 41 FF ? ? 48 8B ? 48 85 ? 74 ? 48 8B ? 48 8B ? FF 90";
        var scanResult = (IntPtr)await SmartAobScan(sig);

        if (scanResult > 0)
        {
            var relativeAddress = scanResult + 1;
            var relative = GetInstance().ReadMemory<int>((nuint)relativeAddress);
            scanResult = scanResult + relative + 0x5;
            var relativeAddress2 = scanResult + 3;
            var relative2 = GetInstance().ReadMemory<int>((nuint)relativeAddress2);
            return (nuint)(scanResult + relative2 + 0x7);
        }

        ShowError("Name Ptr", sig);
        return 0;
    }

    public async Task CheatPrizeScale()
    {
        PrizeScaleAddress = 0;
        PrizeScaleDetourAddress = 0;

        const string sig = "F3 0F ? ? ? 33 D2 48 8B ? ? E8 ? ? ? ? 90 48 85 ? 74 ? 8B C5";
        PrizeScaleAddress = await SmartAobScan(sig);

        if (PrizeScaleAddress > 0)
        {
            if (GetClass<Bypass>().CrcFuncDetourAddress == 0)
            {
                await GetClass<Bypass>().DisableCrcChecks();
            }
            
            if (GetClass<Bypass>().CrcFuncDetourAddress == 0) return;
                
            var asm = new byte[]
            {
                0xF3, 0x0F, 0x10, 0x73, 0x10, 0x80, 0x3D, 0x0F, 0x00, 0x00, 0x00, 0x01, 0x75, 0x08, 0xF3, 0x0F, 0x10,
                0x35, 0x06, 0x00, 0x00, 0x00
            };

            PrizeScaleDetourAddress = GetInstance().CreateDetour(PrizeScaleAddress, asm, 5);
            return;
        }
        
        ShowError("Spin prize scale", sig);
    }

    public async Task CheatSellFactor()
    {
        SellFactorAddress = 0;
        SellFactorDetourAddress = 0;

        const string sig = "44 8B ? ? ? ? ? 33 D2 48 8B ? ? ? ? ? E8 ? ? ? ? 90";
        SellFactorAddress = await SmartAobScan(sig);

        if (SellFactorAddress > 0)
        {
            if (GetClass<Bypass>().CrcFuncDetourAddress == 0)
            {
                await GetClass<Bypass>().DisableCrcChecks();
            }
            
            if (GetClass<Bypass>().CrcFuncDetourAddress == 0) return;
            
            var asm = new byte[]
            {
                0x44, 0x8B, 0xB3, 0x80, 0x00, 0x00, 0x00, 0x80, 0x3D, 0x0E, 0x00, 0x00, 0x00, 0x01, 0x75, 0x07, 0x44,
                0x8B, 0x35, 0x06, 0x00, 0x00, 0x00
            };

            SellFactorDetourAddress = GetInstance().CreateDetour(SellFactorAddress, asm, 7);
            return;
        }
        
        ShowError("Sell factor", sig);
    }

    public async Task CheatSkillScoreMultiplier()
    {
        SkillScoreMultiplierAddress = 0;
        SkillScoreMultiplierDetourAddress = 0;
        
        const string sig = "8B 78 ? 48 8B ? ? 48 85 ? 74 ? 41 8B";
        SkillScoreMultiplierAddress = await SmartAobScan(sig);

        if (SkillScoreMultiplierAddress > 0)
        {
            if (GetClass<Bypass>().CrcFuncDetourAddress == 0)
            {
                await GetClass<Bypass>().DisableCrcChecks();
            }
            
            if (GetClass<Bypass>().CrcFuncDetourAddress == 0) return;

            var asm = new byte[]
            {
                0x8B, 0x78, 0x08, 0x48, 0x8B, 0x4D, 0x60, 0x80, 0x3D, 0x0E, 0x00, 0x00, 0x00, 0x01, 0x75, 0x07, 0x0F,
                0xAF, 0x3D, 0x06, 0x00, 0x00, 0x00
            };

            SkillScoreMultiplierDetourAddress = GetInstance().CreateDetour(SkillScoreMultiplierAddress, asm, 7);
            return;
        }
        
        ShowError("Skill score multiplier", sig);
    }
    
    public async Task CheatDriftScoreMultiplier()
    {
        DriftScoreMultiplierAddress = 0;
        DriftScoreMultiplierDetourAddress = 0;
        
        const string sig = "E8 ? ? ? ? F3 0F ? ? 0F 28 ? ? ? 0F 28";
        DriftScoreMultiplierAddress = await SmartAobScan(sig) + 5;

        if (DriftScoreMultiplierAddress > 5)
        {
            if (GetClass<Bypass>().CrcFuncDetourAddress == 0)
            {
                await GetClass<Bypass>().DisableCrcChecks();
            }
            
            if (GetClass<Bypass>().CrcFuncDetourAddress == 0) return;

            var asm = new byte[]
            {
                0x80, 0x3D, 0x18, 0x00, 0x00, 0x00, 0x01, 0x75, 0x08, 0xF3, 0x0F, 0x59, 0x3D, 0x0F, 0x00, 0x00, 0x00,
                0xF3, 0x0F, 0x58, 0xF7, 0x0F, 0x28, 0x7C, 0x24, 0x20
            };

            DriftScoreMultiplierDetourAddress = GetInstance().CreateDetour(DriftScoreMultiplierAddress, asm, 9);
            return;
        }
        
        ShowError("Drift score multiplier", sig);
    }
    
    public async Task CheatSkillTreeWideEdit()
    {
        SkillTreeWideEditAddress = 0;
        SkillTreeWideEditDetourAddress = 0;
        
        const string sig = "40 ? 48 83 EC ? 48 8B ? ? 33 D2 0F 29";
        SkillTreeWideEditAddress = await SmartAobScan(sig) + 32;

        if (SkillTreeWideEditAddress > 32)
        {
            if (GetClass<Bypass>().CrcFuncDetourAddress == 0)
            {
                await GetClass<Bypass>().DisableCrcChecks();
            }
            
            if (GetClass<Bypass>().CrcFuncDetourAddress == 0) return;

            var asm = new byte[]
            {
                0xF3, 0x0F, 0x10, 0x73, 0x48, 0x80, 0x3D, 0x0F, 0x00, 0x00, 0x00, 0x01, 0x75, 0x08, 0xF3, 0x0F, 0x10,
                0x35, 0x06, 0x00, 0x00, 0x00
            };

            SkillTreeWideEditDetourAddress = GetInstance().CreateDetour(SkillTreeWideEditAddress, asm, 5);
            return;
        }
        
        ShowError("Skill tree wide edit", sig);
    }
    
    public async Task CheatSkillTreePerksCost()
    {
        SkillTreePerksCostAddress = 0;
        SkillTreePerksCostDetourAddress = 0;
        
        const string sig = "48 89 5C 24 08 57 48 83 EC 20 48 8B 79 18 33 D2 48 8B 4F 30";
        SkillTreePerksCostAddress = await SmartAobScan(sig) + 29;

        if (SkillTreePerksCostAddress > 29)
        {
            if (GetClass<Bypass>().CrcFuncDetourAddress == 0)
            {
                await GetClass<Bypass>().DisableCrcChecks();
            }
            
            if (GetClass<Bypass>().CrcFuncDetourAddress == 0) return;

            var asm = new byte[]
            {
                0x31, 0xD2, 0x8B, 0x5F, 0x28, 0x80, 0x3D, 0x0E, 0x00, 0x00, 0x00, 0x01, 0x75, 0x07, 0x48, 0x8B, 0x1D,
                0x06, 0x00, 0x00, 0x00
            };

            SkillTreePerksCostDetourAddress = GetInstance().CreateDetour(SkillTreePerksCostAddress, asm, 5);
            return;
        }
        
        ShowError("Skill tree perks cost", sig);
    }
    
    public async Task CheatMissionTimeScale()
    {
        MissionTimeScaleAddress = 0;
        MissionTimeScaleDetourAddress = 0;
        
        const string sig = "F3 0F ? ? F3 0F ? ? ? ? ? ? 0F 2F ? 0F 87 ? ? ? ? C7 ? ? ? ? ? 00 00 00 00";
        MissionTimeScaleAddress = await SmartAobScan(sig);

        if (MissionTimeScaleAddress > 0)
        {
            if (GetClass<Bypass>().CrcFuncDetourAddress == 0)
            {
                await GetClass<Bypass>().DisableCrcChecks();
            }
            
            if (GetClass<Bypass>().CrcFuncDetourAddress == 0) return;

            var asm = new byte[]
            {
                0x80, 0x3D, 0x1B, 0x00, 0x00, 0x00, 0x01, 0x75, 0x08, 0xF3, 0x0F, 0x59, 0x3D, 0x12, 0x00, 0x00, 0x00,
                0xF3, 0x0F, 0x5C, 0xC7, 0xF3, 0x0F, 0x11, 0x87, 0x0C, 0x04, 0x00, 0x00
            };

            MissionTimeScaleDetourAddress = GetInstance().CreateDetour(MissionTimeScaleAddress, asm, 12);
            return;
        }
        
        ShowError("Mission time scale", sig);
    }
    
    public async Task CheatTrailblazerTimeScale()
    {
        TrailblazerTimeScaleAddress = 0;
        TrailblazerTimeScaleDetourAddress = 0;
        
        const string sig = "F3 0F ? ? F3 0F ? ? ? ? ? ? 4C 8D ? ? ? ? ? F3 0F ? ? 33 D2";
        TrailblazerTimeScaleAddress = await SmartAobScan(sig);

        if (TrailblazerTimeScaleAddress > 0)
        {
            if (GetClass<Bypass>().CrcFuncDetourAddress == 0)
            {
                await GetClass<Bypass>().DisableCrcChecks();
            }
            
            if (GetClass<Bypass>().CrcFuncDetourAddress == 0) return;

            var asm = new byte[]
            {
                0x80, 0x3D, 0x1B, 0x00, 0x00, 0x00, 0x01, 0x75, 0x08, 0xF3, 0x0F, 0x59, 0x05, 0x12, 0x00, 0x00, 0x00,
                0xF3, 0x0F, 0x58, 0xF8, 0xF3, 0x0F, 0x11, 0xBF, 0xAC, 0x03, 0x00, 0x00
            };

            TrailblazerTimeScaleDetourAddress = GetInstance().CreateDetour(TrailblazerTimeScaleAddress, asm, 12);
            return;
        }
        
        ShowError("Trailblazer time scale", sig);
    }
    
    public async Task CheatUnbreakableSkillScore()
    {
        UnbreakableSkillScoreAddress = 0;
        UnbreakableSkillScoreDetourAddress = 0;
        
        const string sig = "0F B6 ? 40 38 ? ? ? ? ? 74 ? 84 C0";
        UnbreakableSkillScoreAddress = await SmartAobScan(sig);

        if (UnbreakableSkillScoreAddress > 0)
        {
            if (GetClass<Bypass>().CrcFuncDetourAddress == 0)
            {
                await GetClass<Bypass>().DisableCrcChecks();
            }
            
            if (GetClass<Bypass>().CrcFuncDetourAddress == 0) return;

            var asm = new byte[]
            {
                0x80, 0x3D, 0x13, 0x00, 0x00, 0x00, 0x01, 0x75, 0x02, 0x30, 0xC0, 0x0F, 0xB6, 0xF0, 0x40, 0x38, 0xAF,
                0x74, 0x02, 0x00, 0x00
            };

            UnbreakableSkillScoreDetourAddress = GetInstance().CreateDetour(UnbreakableSkillScoreAddress, asm, 10);
            return;
        }
        
        ShowError("Unbreakable skill score", sig);
    }
    
    public void Cleanup()
    {
        var mem = GetInstance();
        
        if (NameAddress > 0)
        {
            mem.WriteArrayMemory(NameAddress, new byte[] { 0x0F, 0xB7, 0x04, 0x13, 0x48, 0x8B, 0xF7 });
            Free(NameDetourAddress);
        }

        if (PrizeScaleAddress > 0)
        {
            mem.WriteArrayMemory(PrizeScaleAddress, new byte[] { 0xF3, 0x0F, 0x10, 0x73, 0x10 });
            Free(PrizeScaleDetourAddress);
        }

        if (SellFactorAddress > 0)
        {
            mem.WriteArrayMemory(SellFactorAddress, new byte[] { 0x44, 0x8B, 0xB3, 0x80, 0x00, 0x00, 0x00 });
            Free(SellFactorDetourAddress);
        }

        if (SkillScoreMultiplierAddress > 0)
        {
            mem.WriteArrayMemory(SkillScoreMultiplierAddress, new byte[] { 0x8B, 0x78, 0x08, 0x48, 0x8B, 0x4D, 0x60 });
            Free(SkillScoreMultiplierDetourAddress);
        }

        if (DriftScoreMultiplierAddress > 5)
        {
            mem.WriteArrayMemory(DriftScoreMultiplierAddress, new byte[] { 0xF3, 0x0F, 0x58, 0xF7, 0x0F, 0x28, 0x7C, 0x24, 0x20 });
            Free(DriftScoreMultiplierDetourAddress);
        }

        if (SkillTreeWideEditAddress > 32)
        {
            mem.WriteArrayMemory(SkillTreeWideEditAddress, new byte[] { 0xF3, 0x0F, 0x10, 0x73, 0x48 });
            Free(SkillTreeWideEditDetourAddress);
        }

        if (SkillTreePerksCostAddress > 32)
        {
            mem.WriteArrayMemory(SkillTreePerksCostAddress, new byte[] { 0x33, 0xD2, 0x8B, 0x5F, 0x28 });
            Free(SkillTreePerksCostDetourAddress);
        }

        if (MissionTimeScaleAddress > 0)
        {
            mem.WriteArrayMemory(MissionTimeScaleAddress, new byte[] { 0xF3, 0x0F, 0x5C, 0xC7, 0xF3, 0x0F, 0x11, 0x87, 0x0C, 0x04, 0x00, 0x00 });
            Free(MissionTimeScaleDetourAddress);
        }

        if (TrailblazerTimeScaleAddress > 0)
        {
            mem.WriteArrayMemory(TrailblazerTimeScaleAddress, new byte[] { 0xF3, 0x0F, 0x58, 0xF8, 0xF3, 0x0F, 0x11, 0xBF, 0xAC, 0x03, 0x00, 0x00 });
            Free(TrailblazerTimeScaleDetourAddress);
        }

        if (UnbreakableSkillScoreAddress > 0)
        {
            mem.WriteArrayMemory(UnbreakableSkillScoreAddress, new byte[] { 0x0F, 0xB6, 0xF0, 0x40, 0x38, 0xAF, 0x74, 0x02, 0x00, 0x00 });
            Free(UnbreakableSkillScoreDetourAddress);
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