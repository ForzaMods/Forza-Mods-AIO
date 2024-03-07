namespace Forza_Mods_AIO.Cheats.ForzaHorizon5;

public class CustomizationCheats : CheatsUtilities, ICheatsBase
{
    public UIntPtr PaintAddress, PaintDetourAddress;
    public UIntPtr HeadlightColourAddress, HeadlightColourDetourAddress;
    public UIntPtr CleanlinessAddress, CleanlinessDetourAddress;

    public async Task CheatGlowingPaint()
    {
        PaintAddress = 0;
        PaintDetourAddress = 0;

        const string sig = "0F 11 0A C6 42 F0 01";
        PaintAddress = await SmartAobScan(sig);
        
        if (PaintAddress > 0)
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

            PaintDetourAddress = Resources.Memory.GetInstance().CreateDetour(PaintAddress, asm, 7);
            return;
        }
        
        ShowError("Glowing paint", sig);
    }

    public async Task CheatHeadlightColour()
    {
        HeadlightColourAddress = 0;
        HeadlightColourDetourAddress = 0;

        const string sig = "0F 10 ? ? F3 44 ? ? ? ? ? ? ? 83 7B 48";
        HeadlightColourAddress = await SmartAobScan(sig);
        
        if (HeadlightColourAddress > 0)
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

            HeadlightColourDetourAddress = Resources.Memory.GetInstance().CreateDetour(HeadlightColourAddress, asm, 13);
            return;
        }
        
        ShowError("Headlight colour", sig);
    }

    public async Task CheatCleanliness()
    {
        CleanlinessAddress = 0;
        CleanlinessDetourAddress = 0;

        const string sig = "F3 0F ? ? ? ? ? ? F3 0F ? ? ? ? B9 ? ? ? ? E8";
        CleanlinessAddress = await SmartAobScan(sig);

        if (CleanlinessAddress > 0)
        {
            var asm = new byte[]
            {
                0x80, 0x3D, 0x30, 0x00, 0x00, 0x00, 0x01, 0x75, 0x0C, 0x8B, 0x0D, 0x29, 0x00, 0x00, 0x00, 0x89, 0x88,
                0x04, 0x8A, 0x00, 0x00, 0x80, 0x3D, 0x20, 0x00, 0x00, 0x00, 0x01, 0x75, 0x0C, 0x8B, 0x0D, 0x19, 0x00,
                0x00, 0x00, 0x89, 0x88, 0x08, 0x8A, 0x00, 0x00, 0xF3, 0x0F, 0x10, 0x88, 0x0C, 0x8A, 0x00, 0x00
            };

            CleanlinessDetourAddress = Resources.Memory.GetInstance().CreateDetour(CleanlinessAddress, asm, 8);
            return;
        }
        
        ShowError("Cleanliness", sig);
    }
    
    public void Cleanup()
    {
        var mem = Resources.Memory.GetInstance();
        
        if (PaintAddress > 0)
        {
            mem.WriteArrayMemory(PaintAddress, new byte[] { 0x0F, 0x11, 0x0A, 0xC6, 0x42, 0xF0, 0x01 });
            Free(PaintDetourAddress);
        }

        if (HeadlightColourAddress > 0)
        {
            mem.WriteArrayMemory(HeadlightColourAddress, new byte[] { 0x0F, 0x10, 0x7B, 0x50, 0xF3, 0x44, 0x0F, 0x10, 0x83, 0x84, 0x00, 0x00, 0x00 });
            Free(HeadlightColourDetourAddress);
        }
    }

    public void Reset()
    {
        var fields = typeof(CustomizationCheats).GetFields().Where(f => f.FieldType == typeof(nuint));
        foreach (var field in fields)
        {
            field.SetValue(this, UIntPtr.Zero);
        }
    }
}