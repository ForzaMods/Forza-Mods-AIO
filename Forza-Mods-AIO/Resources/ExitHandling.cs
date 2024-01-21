using System;
using System.Numerics;
using Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;
using Forza_Mods_AIO.Tabs.Self_Vehicle.Entities;
using Forza_Mods_AIO.Tabs.Self_Vehicle.Features;
using Forza_Mods_AIO.Tabs.Tuning;

using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.SelfVehicleAddresses;

namespace Forza_Mods_AIO.Resources;

public static class ExitHandling
{
    public static void DestroyDetours()
    {
        TuningAsm.Cleanup();
        EncryptedValues.EncryptedValuesDetour.Destroy();
        UnlocksPage.SeasonalDetour.Destroy();   
        UnlocksPage.SeriesDetour.Destroy();   
        UnlocksPage.SpinsDetour.Destroy();
        UnlocksPage.SkillPointsDetour.Destroy();
        CustomizationPage.GlowingPaintDetour.Destroy();
        CustomizationPage.HeadlightDetour.Destroy();
        CustomizationPage.CleanlinessDetour.Destroy();
        CustomizationPage.ForceLodDetour.Destroy();
        CustomizationPage.LodCmpDetour.Destroy();
        CameraPage.CameraDetour.Destroy();
        CarEntity.BaseDetour.Destroy();
        LocatorEntity.WaypointDetour.Destroy();
        EnvironmentPage.TimeDetour.Destroy();
        EnvironmentPage.FreezeAiDetour.Destroy();
        MiscellaneousPage.Build1Detour.Destroy();
        MiscellaneousPage.Build2Detour.Destroy();
        MiscellaneousPage.ScaleDetour.Destroy();
        MiscellaneousPage.SellDetour.Destroy();
        MiscellaneousPage.SkillTreeDetour.Destroy();
        MiscellaneousPage.ScoreDetour.Destroy();
        MiscellaneousPage.SkillCostDetour.Destroy();
        MiscellaneousPage.DriftDetour.Destroy();
        MiscellaneousPage.TimeScaleDetour.Destroy();
        MiscellaneousPage.UnbSkillDetour.Destroy();
        BackFirePage.BackFire.BackfireTimeDetour.Destroy();
        BackFirePage.BackFire.BackfireTypeDetour.Destroy();
    }

    public static void RevertWrites()
    {
        if (Mw.Gvp.Process.MainModule == null)
        {
            return;
        }
        
        if (Mw.Gvp.Type == GameVerPlat.GameType.Fh4 && SuperCarAddr > (UIntPtr)Mw.Gvp.Process.MainModule.BaseAddress)
        {
            Mw.M.WriteArrayMemory(SuperCarAddr + 4, new byte[] { 0x0F, 0x11, 0x41, 0x10 });
            Mw.M.WriteArrayMemory(SuperCarAddr + 12, new byte[] { 0x0F, 0x11, 0x49, 0x20 });
            Mw.M.WriteArrayMemory(SuperCarAddr + 20, new byte[] { 0x0F, 0x11, 0x41, 0x30 });
            Mw.M.WriteArrayMemory(SuperCarAddr + 32, new byte[] { 0x0F, 0x11, 0x49, 0x40 });
        }

        if (SunRedAddr > (UIntPtr)Mw.Gvp.Process.MainModule.BaseAddress)
        {
            Mw.M.WriteArrayMemory(SunRedAddr, new byte[] { 0x81, 0x80, 0x80, 0x3B, 0x81, 0x80, 0x80, 0x3B, 0x81, 0x80, 0x80, 0x3B, 0x81, 0x80, 0x80, 0x3B });
        }

        if (WaterAddr > (UIntPtr)Mw.Gvp.Process.MainModule.BaseAddress)
        {
            Mw.M.WriteMemory(WaterAddr, new Vector3(0f, 3700f, 13500f));
        }

        if (Wall1Addr > (UIntPtr)Mw.Gvp.Process.MainModule.BaseAddress)
        {
            Mw.M.WriteArrayMemory(Wall1Addr, Mw.Gvp.Name switch
            {
                "Forza Horizon 4" => new byte[] { 0x0F, 0x84, 0x29, 0x02, 0x00, 0x00 },
                "Forza Horizon 5" => new byte[] { 0x0F, 0x84, 0x60, 0x02, 0x00, 0x00 },
                _ => new byte[] { 0x0F, 0x84, 0x5E, 0x02, 0x00, 0x00 }
            });
        }

        if (Wall2Addr > (UIntPtr)Mw.Gvp.Process.MainModule.BaseAddress)
        {
            Mw.M.WriteArrayMemory(Wall2Addr, Mw.Gvp.Name switch
            {
                "Forza Horizon 4" => new byte[] { 0x0F, 0x84, 0x2A, 0x02, 0x00, 0x00 },
                "Forza Horizon 5" => new byte[] { 0x0F, 0x84, 0x7E, 0x02, 0x00, 0x00 },
                _ => new byte[] { 0x0F, 0x84, 0x5F, 0x02, 0x00, 0x00 }
            });
        }

        if (Car1Addr > (UIntPtr)Mw.Gvp.Process.MainModule.BaseAddress)
        {
            Mw.M.WriteArrayMemory(Car1Addr, Mw.Gvp.Name switch
            {
                "Forza Horizon 4" => new byte[] { 0x0F, 0x84, 0xB5, 0x01, 0x00, 0x00 },
                "Forza Horizon 5" => new byte[] { 0x0F, 0x84, 0x65, 0x03, 0x00, 0x00 },
                _ => new byte[] { 0x0F, 0x84, 0x6E, 0x03, 0x00, 0x00}
            });
        }

        if (Car2Addr > (UIntPtr)Mw.Gvp.Process.MainModule.BaseAddress)
        {
            Mw.M.WriteArrayMemory(Car2Addr, new byte[] { 0x0F, 0x84, 0x3A, 0x03, 0x00, 0x00 });
        }

        if (WorldCollisionThreshold != 0)
        {
            Mw.M.WriteMemory(WorldCollisionThreshold, 12f);
            Mw.M.WriteMemory(CarCollisionThreshold,12f);
            Mw.M.WriteMemory(SmashAbleCollisionTolerance,22f);
        }

        if (XpAmountAddr > (UIntPtr)Mw.Gvp.Process.MainModule.BaseAddress)
        {
            Mw.M.WriteArrayMemory(XpAmountAddr, Mw.Gvp.Name.Contains('5')
                ? new byte[] { 0x8B, 0x89, 0x88, 0x00, 0x00, 0x00 }
                : new byte[] { 0x8B, 0x89, 0xC0, 0x00, 0x00, 0x00 });
        }

        if (GravityProtectAddr > (UIntPtr)Mw.Gvp.Process.MainModule.BaseAddress)
        {
            Mw.M.WriteArrayMemory(GravityProtectAddr,new byte[] { 0xF3, 0x0F, 0x11, 0x49, 0x08 });
        }
        
        if (AccelProtectAddr > (UIntPtr)Mw.Gvp.Process.MainModule.BaseAddress)
        {
            Mw.M.WriteArrayMemory(AccelProtectAddr,new byte[] { 0xF3, 0x0F, 0x11, 0x41, 0x0C });
        }
    }
}