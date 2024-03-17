using Forza_Mods_AIO.Resources;
using static Forza_Mods_AIO.Resources.Cheats;
using static Forza_Mods_AIO.Resources.Memory;

namespace Forza_Mods_AIO.Cheats.ForzaHorizon5;

public class MiscCheats : CheatsUtilities, ICheatsBase
{
    private UIntPtr _nameAddress;
    public UIntPtr NameDetourAddress;
    private UIntPtr _sellFactorAddress;
    public UIntPtr SellFactorDetourAddress;
    private UIntPtr _prizeScaleAddress;
    public UIntPtr PrizeScaleDetourAddress;
    private UIntPtr _skillScoreMultiplierAddress;
    public UIntPtr SkillScoreMultiplierDetourAddress;
    private UIntPtr _driftScoreMultiplierAddress;
    public UIntPtr DriftScoreMultiplierDetourAddress;
    private UIntPtr _skillTreeWideEditAddress;
    public UIntPtr SkillTreeWideEditDetourAddress;
    private UIntPtr _skillTreePerksCostAddress;
    public UIntPtr SkillTreePerksCostDetourAddress;
    private UIntPtr _missionTimeScaleAddress;
    public UIntPtr MissionTimeScaleDetourAddress;
    private UIntPtr _trailblazerTimeScaleAddress;
    public UIntPtr TrailblazerTimeScaleDetourAddress;
    private UIntPtr _speedZoneMultiplierAddress;
    public UIntPtr SpeedZoneMultiplierDetourAddress;
    private UIntPtr _unbreakableSkillScoreAddress;
    public UIntPtr UnbreakableSkillScoreDetourAddress;
    private UIntPtr _removeBuildCapAddress;
    public UIntPtr RemoveBuildCapDetourAddress;

    public async Task CheatName()
    {
        _nameAddress = 0;
        NameDetourAddress = 0;

        var urtcbaseHandle = Imports.GetModuleHandle("ucrtbase.dll");
        _nameAddress = Imports.GetProcAddress(urtcbaseHandle, "wcsncpy_s") + 0x98;
        
        if (_nameAddress > 0x98)
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

            NameDetourAddress = GetInstance().CreateDetour(_nameAddress, asm, 7);
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
        _prizeScaleAddress = 0;
        PrizeScaleDetourAddress = 0;

        const string sig = "F3 0F ? ? ? 33 D2 48 8B ? ? E8 ? ? ? ? 90 48 85 ? 74 ? 8B C5";
        _prizeScaleAddress = await SmartAobScan(sig);

        if (_prizeScaleAddress > 0)
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

            PrizeScaleDetourAddress = GetInstance().CreateDetour(_prizeScaleAddress, asm, 5);
            return;
        }
        
        ShowError("Spin prize scale", sig);
    }

    public async Task CheatSellFactor()
    {
        _sellFactorAddress = 0;
        SellFactorDetourAddress = 0;

        const string sig = "44 8B ? ? ? ? ? 33 D2 48 8B ? ? ? ? ? E8 ? ? ? ? 90";
        _sellFactorAddress = await SmartAobScan(sig);

        if (_sellFactorAddress > 0)
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

            SellFactorDetourAddress = GetInstance().CreateDetour(_sellFactorAddress, asm, 7);
            return;
        }
        
        ShowError("Sell factor", sig);
    }

    public async Task CheatSkillScoreMultiplier()
    {
        _skillScoreMultiplierAddress = 0;
        SkillScoreMultiplierDetourAddress = 0;
        
        const string sig = "8B 78 ? 48 8B ? ? 48 85 ? 74 ? 41 8B";
        _skillScoreMultiplierAddress = await SmartAobScan(sig);

        if (_skillScoreMultiplierAddress > 0)
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

            SkillScoreMultiplierDetourAddress = GetInstance().CreateDetour(_skillScoreMultiplierAddress, asm, 7);
            return;
        }
        
        ShowError("Skill score multiplier", sig);
    }
    
    public async Task CheatDriftScoreMultiplier()
    {
        _driftScoreMultiplierAddress = 0;
        DriftScoreMultiplierDetourAddress = 0;
        
        const string sig = "E8 ? ? ? ? F3 0F ? ? 0F 28 ? ? ? 0F 28";
        _driftScoreMultiplierAddress = await SmartAobScan(sig) + 5;

        if (_driftScoreMultiplierAddress > 5)
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

            DriftScoreMultiplierDetourAddress = GetInstance().CreateDetour(_driftScoreMultiplierAddress, asm, 9);
            return;
        }
        
        ShowError("Drift score multiplier", sig);
    }
    
    public async Task CheatSkillTreeWideEdit()
    {
        _skillTreeWideEditAddress = 0;
        SkillTreeWideEditDetourAddress = 0;
        
        const string sig = "40 ? 48 83 EC ? 48 8B ? ? 33 D2 0F 29";
        _skillTreeWideEditAddress = await SmartAobScan(sig) + 32;

        if (_skillTreeWideEditAddress > 32)
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

            SkillTreeWideEditDetourAddress = GetInstance().CreateDetour(_skillTreeWideEditAddress, asm, 5);
            return;
        }
        
        ShowError("Skill tree wide edit", sig);
    }
    
    public async Task CheatSkillTreePerksCost()
    {
        _skillTreePerksCostAddress = 0;
        SkillTreePerksCostDetourAddress = 0;
        
        const string sig = "48 89 5C 24 08 57 48 83 EC 20 48 8B 79 18 33 D2 48 8B 4F 30";
        _skillTreePerksCostAddress = await SmartAobScan(sig) + 29;

        if (_skillTreePerksCostAddress > 29)
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

            SkillTreePerksCostDetourAddress = GetInstance().CreateDetour(_skillTreePerksCostAddress, asm, 5);
            return;
        }
        
        ShowError("Skill tree perks cost", sig);
    }
    
    public async Task CheatMissionTimeScale()
    {
        _missionTimeScaleAddress = 0;
        MissionTimeScaleDetourAddress = 0;
        
        const string sig = "F3 0F ? ? F3 0F ? ? ? ? ? ? 0F 2F ? 0F 87 ? ? ? ? C7 ? ? ? ? ? 00 00 00 00";
        _missionTimeScaleAddress = await SmartAobScan(sig);

        if (_missionTimeScaleAddress > 0)
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

            MissionTimeScaleDetourAddress = GetInstance().CreateDetour(_missionTimeScaleAddress, asm, 12);
            return;
        }
        
        ShowError("Mission time scale", sig);
    }
    
    public async Task CheatSpeedZoneMultiplier()
    {
        _speedZoneMultiplierAddress = 0;
        SpeedZoneMultiplierDetourAddress = 0;
        
        const string sig = "F3 41 ? ? ? ? ? ? ? 0F 28 ? 0F 28 ? ? ? 48 83 C4";
        _speedZoneMultiplierAddress = await SmartAobScan(sig);

        if (_speedZoneMultiplierAddress > 0)
        {
            if (GetClass<Bypass>().CrcFuncDetourAddress == 0)
            {
                await GetClass<Bypass>().DisableCrcChecks();
            }
            
            if (GetClass<Bypass>().CrcFuncDetourAddress == 0) return;

            var asm = new byte[]
            {
                0x80, 0x3D, 0x18, 0x00, 0x00, 0x00, 0x01, 0x75, 0x08, 0xF3, 0x0F, 0x59, 0x35, 0x0F, 0x00, 0x00, 0x00,
                0xF3, 0x41, 0x0F, 0x5E, 0xB7, 0xE8, 0x00, 0x00, 0x00
            };

            SpeedZoneMultiplierDetourAddress = GetInstance().CreateDetour(_speedZoneMultiplierAddress, asm, 9);
            return;
        }
        
        ShowError("Trailblazer time scale", sig);
    }
    
    public async Task CheatTrailblazerTimeScale()
    {
        _trailblazerTimeScaleAddress = 0;
        TrailblazerTimeScaleDetourAddress = 0;
        
        const string sig = "F3 0F ? ? F3 0F ? ? ? ? ? ? 4C 8D ? ? ? ? ? F3 0F ? ? 33 D2";
        _trailblazerTimeScaleAddress = await SmartAobScan(sig);

        if (_trailblazerTimeScaleAddress > 0)
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

            TrailblazerTimeScaleDetourAddress = GetInstance().CreateDetour(_trailblazerTimeScaleAddress, asm, 12);
            return;
        }
        
        ShowError("Trailblazer time scale", sig);
    }
    
    public async Task CheatUnbreakableSkillScore()
    {
        _unbreakableSkillScoreAddress = 0;
        UnbreakableSkillScoreDetourAddress = 0;
        
        const string sig = "0F B6 ? 40 38 ? ? ? ? ? 74 ? 84 C0";
        _unbreakableSkillScoreAddress = await SmartAobScan(sig);

        if (_unbreakableSkillScoreAddress > 0)
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

            UnbreakableSkillScoreDetourAddress = GetInstance().CreateDetour(_unbreakableSkillScoreAddress, asm, 10);
            return;
        }
        
        ShowError("Unbreakable skill score", sig);
    }
    
    
    public async Task CheatRemoveBuildCap()
    {
        _removeBuildCapAddress = 0;
        RemoveBuildCapDetourAddress = 0;
        
        const string sig = "E8 ? ? ? ? F3 0F ? ? ? 48 8B ? ? ? 48 8B";
        _removeBuildCapAddress = await SmartAobScan(sig) + 5;

        if (_removeBuildCapAddress > 5)
        {
            if (GetClass<Bypass>().CrcFuncDetourAddress == 0)
            {
                await GetClass<Bypass>().DisableCrcChecks();
            }
            
            if (GetClass<Bypass>().CrcFuncDetourAddress == 0) return;

            var asm = new byte[]
            {
                0x80, 0x3D, 0x0F, 0x00, 0x00, 0x00, 0x01, 0x75, 0x03, 0x0F, 0x57, 0xC0, 0xF3, 0x0F, 0x11, 0x43, 0x44
            };

            RemoveBuildCapDetourAddress = GetInstance().CreateDetour(_removeBuildCapAddress, asm, 5);
            return;
        }
        
        ShowError("Unbreakable skill score", sig);
    }
    
    public void Cleanup()
    {
        var mem = GetInstance();
        
        if (_nameAddress > 0)
        {
            mem.WriteArrayMemory(_nameAddress, new byte[] { 0x0F, 0xB7, 0x04, 0x13, 0x48, 0x8B, 0xF7 });
            Free(NameDetourAddress);
        }

        if (_prizeScaleAddress > 0)
        {
            mem.WriteArrayMemory(_prizeScaleAddress, new byte[] { 0xF3, 0x0F, 0x10, 0x73, 0x10 });
            Free(PrizeScaleDetourAddress);
        }

        if (_sellFactorAddress > 0)
        {
            mem.WriteArrayMemory(_sellFactorAddress, new byte[] { 0x44, 0x8B, 0xB3, 0x80, 0x00, 0x00, 0x00 });
            Free(SellFactorDetourAddress);
        }

        if (_skillScoreMultiplierAddress > 0)
        {
            mem.WriteArrayMemory(_skillScoreMultiplierAddress, new byte[] { 0x8B, 0x78, 0x08, 0x48, 0x8B, 0x4D, 0x60 });
            Free(SkillScoreMultiplierDetourAddress);
        }

        if (_driftScoreMultiplierAddress > 5)
        {
            mem.WriteArrayMemory(_driftScoreMultiplierAddress, new byte[] { 0xF3, 0x0F, 0x58, 0xF7, 0x0F, 0x28, 0x7C, 0x24, 0x20 });
            Free(DriftScoreMultiplierDetourAddress);
        }

        if (_skillTreeWideEditAddress > 32)
        {
            mem.WriteArrayMemory(_skillTreeWideEditAddress, new byte[] { 0xF3, 0x0F, 0x10, 0x73, 0x48 });
            Free(SkillTreeWideEditDetourAddress);
        }

        if (_skillTreePerksCostAddress > 32)
        {
            // ReSharper disable once UseUtf8StringLiteral
            mem.WriteArrayMemory(_skillTreePerksCostAddress, new byte[] { 0x33, 0xD2, 0x8B, 0x5F, 0x28 });
            Free(SkillTreePerksCostDetourAddress);
        }

        if (_missionTimeScaleAddress > 0)
        {
            mem.WriteArrayMemory(_missionTimeScaleAddress, new byte[] { 0xF3, 0x0F, 0x5C, 0xC7, 0xF3, 0x0F, 0x11, 0x87, 0x0C, 0x04, 0x00, 0x00 });
            Free(MissionTimeScaleDetourAddress);
        }

        if (_trailblazerTimeScaleAddress > 0)
        {
            mem.WriteArrayMemory(_trailblazerTimeScaleAddress, new byte[] { 0xF3, 0x0F, 0x58, 0xF8, 0xF3, 0x0F, 0x11, 0xBF, 0xAC, 0x03, 0x00, 0x00 });
            Free(TrailblazerTimeScaleDetourAddress);
        }

        if (_speedZoneMultiplierAddress > 0)
        {
            mem.WriteArrayMemory(_speedZoneMultiplierAddress, new byte[] { 0xF3, 0x41, 0x0F, 0x5E, 0xB7, 0xE8, 0x00, 0x00, 0x00 });
            Free(SpeedZoneMultiplierDetourAddress);
        }

        if (_unbreakableSkillScoreAddress > 0)
        {
            mem.WriteArrayMemory(_unbreakableSkillScoreAddress, new byte[] { 0x0F, 0xB6, 0xF0, 0x40, 0x38, 0xAF, 0x74, 0x02, 0x00, 0x00 });
            Free(UnbreakableSkillScoreDetourAddress);
        }

        if (_removeBuildCapAddress <= 5) return;
        mem.WriteArrayMemory(_removeBuildCapAddress, new byte[] { 0xF3, 0x0F, 0x11, 0x43, 0x44 });
        Free(RemoveBuildCapDetourAddress);
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