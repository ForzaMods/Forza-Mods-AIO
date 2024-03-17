namespace Forza_Mods_AIO.Cheats.ForzaHorizon5;

public class CustomizationCheats : CheatsUtilities, ICheatsBase
{
    private UIntPtr _paintAddress;
    public UIntPtr PaintDetourAddress;
    private UIntPtr _headlightColourAddress;
    public UIntPtr HeadlightColourDetourAddress;
    private UIntPtr _cleanlinessAddress;
    public UIntPtr CleanlinessDetourAddress;
    private UIntPtr _forceLodAddress;
    public UIntPtr ForceLodDetourAddress;
    private UIntPtr _backfireTimeAddress;
    public UIntPtr BackfireTimeDetourAddress;

    public async Task CheatGlowingPaint()
    {
        _paintAddress = 0;
        PaintDetourAddress = 0;

        const string sig = "0F 11 0A C6 42 F0 01";
        _paintAddress = await SmartAobScan(sig);
        
        if (_paintAddress > 0)
        {
            if (Resources.Cheats.GetClass<Bypass>().CrcFuncDetourAddress == 0)
            {
                await Resources.Cheats.GetClass<Bypass>().DisableCrcChecks();
            }
            
            if (Resources.Cheats.GetClass<Bypass>().CrcFuncDetourAddress <= 0) return;
            
            var asm = new byte[]
            {
                0x80, 0x3D, 0x2F, 0x00, 0x00, 0x00, 0x01, 0x75, 0x21, 0x48, 0x83, 0xEC, 0x10, 0xF3, 0x0F, 0x7F, 0x04,
                0x24, 0xF3, 0x0F, 0x10, 0x05, 0x1D, 0x00, 0x00, 0x00, 0x0F, 0xC6, 0xC0, 0x00, 0x0F, 0x59, 0xC8, 0xF3,
                0x0F, 0x6F, 0x04, 0x24, 0x48, 0x83, 0xC4, 0x10, 0x0F, 0x11, 0x0A, 0xC6, 0x42, 0xF0, 0x01
            };

            PaintDetourAddress = Resources.Memory.GetInstance().CreateDetour(_paintAddress, asm, 7);
            return;
        }
        
        ShowError("Glowing paint", sig);
    }

    public async Task CheatHeadlightColour()
    {
        _headlightColourAddress = 0;
        HeadlightColourDetourAddress = 0;

        const string sig = "0F 10 ? ? F3 44 ? ? ? ? ? ? ? 83 7B 48";
        _headlightColourAddress = await SmartAobScan(sig);
        
        if (_headlightColourAddress > 0)
        {
            if (Resources.Cheats.GetClass<Bypass>().CrcFuncDetourAddress == 0)
            {
                await Resources.Cheats.GetClass<Bypass>().DisableCrcChecks();
            }
            
            if (Resources.Cheats.GetClass<Bypass>().CrcFuncDetourAddress <= 0) return;
            
            var asm = new byte[]
            {
                0x0F, 0x10, 0x7B, 0x50, 0x80, 0x3D, 0x17, 0x00, 0x00, 0x00, 0x01, 0x75, 0x07, 0x0F, 0x10, 0x3D, 0x0F,
                0x00, 0x00, 0x00, 0xF3, 0x44, 0x0F, 0x10, 0x83, 0x84, 0x00, 0x00, 0x00
            };

            HeadlightColourDetourAddress = Resources.Memory.GetInstance().CreateDetour(_headlightColourAddress, asm, 13);
            return;
        }
        
        ShowError("Headlight colour", sig);
    }

    public async Task CheatCleanliness()
    {
        _cleanlinessAddress = 0;
        CleanlinessDetourAddress = 0;

        const string sig = "F3 0F ? ? ? ? ? ? F3 0F ? ? ? ? B9 ? ? ? ? E8";
        _cleanlinessAddress = await SmartAobScan(sig);

        if (_cleanlinessAddress > 0)
        {
            if (Resources.Cheats.GetClass<Bypass>().CrcFuncDetourAddress == 0)
            {
                await Resources.Cheats.GetClass<Bypass>().DisableCrcChecks();
            }
            
            if (Resources.Cheats.GetClass<Bypass>().CrcFuncDetourAddress <= 0) return;
            
            var asm = new byte[]
            {
                0x80, 0x3D, 0x30, 0x00, 0x00, 0x00, 0x01, 0x75, 0x0C, 0x8B, 0x0D, 0x29, 0x00, 0x00, 0x00, 0x89, 0x88,
                0x04, 0x8A, 0x00, 0x00, 0x80, 0x3D, 0x20, 0x00, 0x00, 0x00, 0x01, 0x75, 0x0C, 0x8B, 0x0D, 0x19, 0x00,
                0x00, 0x00, 0x89, 0x88, 0x08, 0x8A, 0x00, 0x00, 0xF3, 0x0F, 0x10, 0x88, 0x0C, 0x8A, 0x00, 0x00
            };

            CleanlinessDetourAddress = Resources.Memory.GetInstance().CreateDetour(_cleanlinessAddress, asm, 8);
            return;
        }
        
        ShowError("Cleanliness", sig);
    }

    public async Task CheatForceLod()
    {
        _forceLodAddress = 0;
        ForceLodDetourAddress = 0;

        const string sig = "40 88 ? ? ? ? ? 40 84 ? 0F 85";
        _forceLodAddress = await SmartAobScan(sig);

        if (_forceLodAddress > 0)
        {
            if (Resources.Cheats.GetClass<Bypass>().CrcFuncDetourAddress == 0)
            {
                await Resources.Cheats.GetClass<Bypass>().DisableCrcChecks();
            }
            
            if (Resources.Cheats.GetClass<Bypass>().CrcFuncDetourAddress <= 0) return;
            
            var cameraPtr = await CheatForceLodCameraPtr();
            if (cameraPtr == 0) return;

            var cameraBytes = BitConverter.GetBytes(cameraPtr);
            var asm = new byte[]
            {
                0x80, 0x3D, 0x4B, 0x00, 0x00, 0x00, 0x01, 0x75, 0x3D, 0x50, 0x48, 0xB8, cameraBytes[0], cameraBytes[1],
                cameraBytes[2], cameraBytes[3], cameraBytes[4], cameraBytes[5], cameraBytes[6], cameraBytes[7], 0x48,
                0x8B, 0x00, 0x48, 0x85, 0xC0, 0x74, 0x29, 0x80, 0x78, 0x30, 0x00, 0x74, 0x0E, 0x80, 0x78, 0x30, 0x01,
                0x74, 0x08, 0x80, 0x78, 0x30, 0x11, 0x74, 0x02, 0xEB, 0x15, 0x80, 0x3D, 0x1C, 0x00, 0x00, 0x00, 0x00,
                0x74, 0x09, 0x40, 0x8A, 0x35, 0x13, 0x00, 0x00, 0x00, 0xEB, 0x03, 0x40, 0xB6, 0xFF, 0x58, 0x40, 0x88,
                0xB7, 0x06, 0x01, 0x00, 0x00
            };

            ForceLodDetourAddress = Resources.Memory.GetInstance().CreateDetour(_forceLodAddress, asm, 7);
            return;
        }
        
        ShowError("Force Lod", sig);
    }

    private static async Task<UIntPtr> CheatForceLodCameraPtr()
    {
        const string sig = "48 8D ? ? ? ? ? E8 ? ? ? ? 48 8B ? ? ? ? ? 48 81 C2 ? ? ? ? 48 8D ? ? ? ? ? 41 B8";
        var scanResult = await SmartAobScan(sig);

        if (scanResult > 0)
        {
            var relativeAddress = scanResult + 0x3;
            var relative = Resources.Memory.GetInstance().ReadMemory<int>(relativeAddress);
            return scanResult + (nuint)relative + 0x7 + 0x6A0;
        }

        ShowError("Force Lod Camera Ptr", sig);
        return 0;
    }

    public async Task CheatBackfireTime()
    {
        _backfireTimeAddress = 0;
        BackfireTimeDetourAddress = 0;

        const string sig = "F3 0F ? ? ? ? ? ? E8 ? ? ? ? 0F 28 ? F3 0F ? ? ? ? ? ? 48 8B";
        _backfireTimeAddress = await SmartAobScan(sig);

        if (_backfireTimeAddress > 0)
        {
            if (Resources.Cheats.GetClass<Bypass>().CrcFuncDetourAddress == 0)
            {
                await Resources.Cheats.GetClass<Bypass>().DisableCrcChecks();
            }
            
            if (Resources.Cheats.GetClass<Bypass>().CrcFuncDetourAddress <= 0) return;
            
            var asm = new byte[]
            {
                0xF3, 0x0F, 0x10, 0x81, 0x7C, 0x3A, 0x00, 0x00, 0x80, 0x3D, 0x17, 0x00, 0x00, 0x00, 0x01, 0x75, 0x10,
                0xF3, 0x0F, 0x10, 0x05, 0x0E, 0x00, 0x00, 0x00, 0xF3, 0x0F, 0x10, 0x0D, 0x0A, 0x00, 0x00, 0x00
            };

            BackfireTimeDetourAddress = Resources.Memory.GetInstance().CreateDetour(_backfireTimeAddress, asm, 8); 
            return;
        }
        
        ShowError("Backfire Time", sig);
    }
    
    public void Cleanup()
    {
        var mem = Resources.Memory.GetInstance();
        
        if (_paintAddress > 0)
        {
            mem.WriteArrayMemory(_paintAddress, new byte[] { 0x0F, 0x11, 0x0A, 0xC6, 0x42, 0xF0, 0x01 });
            Free(PaintDetourAddress);
        }

        if (_headlightColourAddress > 0)
        {
            mem.WriteArrayMemory(_headlightColourAddress, new byte[] { 0x0F, 0x10, 0x7B, 0x50, 0xF3, 0x44, 0x0F, 0x10, 0x83, 0x84, 0x00, 0x00, 0x00 });
            Free(HeadlightColourDetourAddress);
        }

        if (_cleanlinessAddress > 0)
        {
            mem.WriteArrayMemory(_cleanlinessAddress, new byte[] { 0xF3, 0x0F, 0x10, 0x88, 0x0C, 0x8A, 0x00, 0x00 });
            Free(CleanlinessDetourAddress);
        }

        if (_forceLodAddress > 0)
        {
            mem.WriteArrayMemory(_forceLodAddress, new byte[] { 0x40, 0x88, 0xB7, 0x06, 0x01, 0x00, 0x00 });
            Free(ForceLodDetourAddress);
        }

        if (_backfireTimeAddress <= 0) return;
        mem.WriteArrayMemory(_backfireTimeAddress, new byte[] { 0xF3, 0x0F, 0x10, 0x81, 0x7C, 0x3A, 0x00, 0x00 });
        Free(BackfireTimeDetourAddress);
    }

    public void Reset()
    {
        var fields = typeof(CustomizationCheats).GetFields().Where(f => f.FieldType == typeof(UIntPtr));
        foreach (var field in fields)
        {
            field.SetValue(this, UIntPtr.Zero);
        }
    }
}