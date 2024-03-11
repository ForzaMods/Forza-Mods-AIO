using Forza_Mods_AIO.Resources;

namespace Forza_Mods_AIO.Cheats.ForzaHorizon5;

public class MiscCheats : CheatsUtilities, ICheatsBase
{
    public UIntPtr NameAddress, NameDetourAddress;

    public async Task CheatName()
    {
        NameAddress = 0;
        NameDetourAddress = 0;

        var urtcbaseHandle = Imports.GetModuleHandle("ucrtbase.dll");
        NameAddress = Imports.GetProcAddress(urtcbaseHandle, "wcsncpy_s") + 0x98;
        
        if (NameAddress > 0)
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

            NameDetourAddress = Resources.Memory.GetInstance().CreateDetour(NameAddress, asm, 7);
            return;
        }
        
        ShowError("Name Spoofer", string.Empty);
    }

    private static async Task<UIntPtr> CheatNamePtr()
    {
        const string sig = "E8 ? ? ? ? 4C 8B ? 48 8B ? 41 FF ? ? 48 8B ? 48 85 ? 74 ? 48 8B ? 48 8B ? FF 90";
        var scanResult = (IntPtr)await SmartAobScan(sig);

        if (scanResult > 0)
        {
            var relativeAddress = scanResult + 1;
            var relative = Resources.Memory.GetInstance().ReadMemory<int>((nuint)relativeAddress);
            scanResult = scanResult + relative + 0x5;
            var relativeAddress2 = scanResult + 3;
            var relative2 = Resources.Memory.GetInstance().ReadMemory<int>((nuint)relativeAddress2);
            return (nuint)(scanResult + relative2 + 0x7);
        }

        ShowError("Name Ptr", sig);
        return 0;
    }
    
    public void Cleanup()
    {
        var mem = Resources.Memory.GetInstance();
        
        if (NameAddress > 0)
        {
            mem.WriteArrayMemory(NameAddress, new byte[] { 0x0F, 0xB7, 0x04, 0x13, 0x48, 0x8B, 0xF7 });
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