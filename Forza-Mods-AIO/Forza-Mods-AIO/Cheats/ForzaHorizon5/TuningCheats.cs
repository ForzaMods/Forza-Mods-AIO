using static Forza_Mods_AIO.Resources.Cheats;
using static Forza_Mods_AIO.Resources.Memory;

namespace Forza_Mods_AIO.Cheats.ForzaHorizon5;

public static class TuningOffsets
{
    public const uint FrontAeroMinOffset = 0x3A4;
    public const uint FrontAeroMaxOffset = 0x3AC;
    public const uint RearAeroMinOffset = 0x404;
    public const uint RearAeroMaxOffset = 0x40C;
    
    public const uint CamberNegOffset = 0x490;
    public const uint CamberPosOffset = 0x494;
    public const uint ToeNegOffset = 0x498;
    public const uint ToePosOffset = 0x49C;
    
    public const uint FrontAntiRollMinOffset = 0x5F0;
    public const uint FrontAntiRollMaxOffset = 0x5F4;
    public const uint RearAntiRollMinOffset = 0x744;
    public const uint RearAntiRollMaxOffset = 0x748;
    
    public const uint FrontReboundStiffnessMinOffset = 0x584;
    public const uint FrontReboundStiffnessMaxOffset = 0x588;
    public const uint RearReboundStiffnessMinOffset = 0x6D8;
    public const uint RearReboundStiffnessMaxOffset = 0x6DC;
    
    public const uint FrontBumpStiffnessMinOffset = 0x560;
    public const uint FrontBumpStiffnessMaxOffset = 0x564;
    public const uint RearBumpStiffnessMinOffset = 0x6B4;
    public const uint RearBumpStiffnessMaxOffset = 0x6B8;
    
    public const uint WheelbaseOffset = 0xD0;
    public const uint FrontWidthOffset = 0xD4;
    public const uint RearWidthOffset = 0xD8;
    public const uint FrontSpacerOffset = 0x9FC;
    public const uint RearSpacerOffset = 0xA00;
    public const uint RimSizeFrontOffset = 0x7D8;
    public const uint RimRadiusFrontOffset = 0x7E0;
    public const uint RimSizeRearOffset = 0x7DC;
    public const uint RimRadiusRearOffset = 0x7E4;
    
    public const uint FinalDriveOffset = 0x125C;
    public const uint ReverseGearOffset = 0x10B8;
    public const uint FirstGearOffset = ReverseGearOffset + 1 * 20;
    public const uint SecondGearOffset = ReverseGearOffset + 2 * 20;
    public const uint ThirdGearOffset = ReverseGearOffset + 3 * 20;
    public const uint FourthGearOffset = ReverseGearOffset + 4 * 20;
    public const uint FifthGearOffset = ReverseGearOffset + 5 * 20;
    public const uint SixthGearOffset = ReverseGearOffset + 6 * 20;
    public const uint SeventhGearOffset = ReverseGearOffset + 7 * 20;
    public const uint EighthGearOffset = ReverseGearOffset + 8 * 20;
    public const uint NinthGearOffset = ReverseGearOffset + 9 * 20;
    public const uint TenthGearOffset = ReverseGearOffset + 10 * 20;
    
    public const uint FrontLeftTirePressureOffset = 0x2D58;
    public const uint FrontRightTirePressureOffset = FrontLeftTirePressureOffset + 1 * 0xAC0;
    public const uint RearLeftTirePressureOffset = FrontLeftTirePressureOffset + 2 * 0xAC0;
    public const uint RearRightTirePressureOffset = FrontLeftTirePressureOffset + 3 * 0xAC0;
    
    public const uint AngleMaxOffset = 0x828;
    public const uint AngleMax2Offset = 0x82C;
    public const uint VelocityStraightOffset = 0x830;
    public const uint VelocityTurningOffset = 0x834;
    public const uint VelocityCountersteerOffset = 0x838;
    public const uint VelocityDynamicPeekOffset = 0x83C;
    public const uint TimeToMaxSteeringOffset = 0x840;
    
    public const uint FrontSpringMinOffset = 0x554;
    public const uint FrontSpringMaxOffset = 0x558;
    public const uint RearSpringMinOffset = 0x6A8;
    public const uint RearSpringMaxOffset = 0x6AC;
    
    public const uint FrontRideHeightMinOffset = 0x534;
    public const uint FrontRideHeightMaxOffset = 0x538;
    public const uint FrontRestrictionOffset = 0x53C;
    public const uint RearRideHeightMinOffset = 0x688;
    public const uint RearRideHeightMaxOffset = 0x68C;
    public const uint RearRestrictionOffset = 0x690;
}

public class TuningCheats : CheatsUtilities, ICheatsBase
{
    public UIntPtr Base1;
    public UIntPtr Base2;
    public UIntPtr Base3;
    public UIntPtr Base4;
    
    public bool WasScanSuccessful;

    public async Task Scan()
    {
        var processMainModule = GetInstance().MProc.Process.MainModule;
        if (processMainModule == null)
        {
            return;
        }
        
        Base1 = 0;
        Base2 = 0;
        Base3 = 0;
        Base4 = 0;

        var successCount = 0;
        
        if (GetClass<CarCheats>().LocalPlayerHookDetourAddress == 0)
        {
            await GetClass<CarCheats>().CheatLocalPlayer();
        }

        if (GetClass<CarCheats>().LocalPlayerHookDetourAddress <= 0)
        {
            goto skipScans;
        }

        ++successCount;

        const string base1Sig = "4C 8B ? ? ? ? ? 49 8B ? ? 4C 8D ? ? 8B 11";
        Base1 = await SmartAobScan(base1Sig);
        if (Base1 <= 0)
        {
            ShowError("Tuning base 1", base1Sig);
            goto skipScans;
        }
        
        var base1Relative = GetInstance().ReadMemory<int>(Base1 + 3);
        Base1 = (nuint)((nint)Base1 + base1Relative + 7);
        ++successCount;

        const string base2Sig = "48 8D ? ? ? ? ? 48 8B ? ? 48 85 ? 74 ? E8 ? ? ? ? 90";
        Base2 = await SmartAobScan(base2Sig);
        if (Base2 <= 0)
        {
            ShowError("Tuning base 2", base2Sig);
            goto skipScans;
        }
        
        var base2Relative = GetInstance().ReadMemory<int>(Base2 + 3);
        Base2 = (nuint)((nint)Base2 + base2Relative + 7);
        ++successCount;

        const string base3Sig = "FF 50 ? 48 8B ? ? ? ? ? 48 85 ? 0F 84 ? ? ? ? 48 85";
        Base3 = await SmartAobScan(base3Sig) + 3;
        if (Base3 <= 3)
        {
            ShowError("Tuning base 3", base3Sig);
            goto skipScans;
        }

        var base3Relative = GetInstance().ReadMemory<int>(Base3 + 3);
        Base3 = (nuint)((nint)Base3 + base3Relative + 7);
        ++successCount;

        const string base4Sig = "3D ? ? ? ? 00 00 ? ? 00 00 5C 42";
        var minRange = processMainModule.BaseAddress;
        var maxRange = minRange + processMainModule.ModuleMemorySize;
        var base4List = await GetInstance().AoBScan(minRange, maxRange, base4Sig, true);
        var base4Enumerable = base4List as UIntPtr[] ?? base4List.ToArray();
        if (base4Enumerable.Length < 1)
        {
            ShowError("Tuning base 4", base4Sig);
            goto skipScans;
        }

        ++successCount;
        Base4 = base4Enumerable.FirstOrDefault() + 13;
        
        skipScans:
        WasScanSuccessful = successCount == 5;
    }
    
    public void Cleanup()
    {
    }

    public void Reset()
    {
        WasScanSuccessful = false;
        var fields = typeof(TuningCheats).GetFields().Where(f => f.FieldType == typeof(UIntPtr));
        foreach (var field in fields)
        {
            field.SetValue(this, UIntPtr.Zero);
        }
    }
}