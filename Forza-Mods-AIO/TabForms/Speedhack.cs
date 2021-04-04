using System;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Threading;
using System.Windows.Forms;
using Memory;
using GlobalLowLevelHooks;
using IniParser;
using IniParser.Model;
using Forza_Mods_AIO.TabForms;

namespace Forza_Mods_AIO.TabForms
{
    public partial class Speedhack : Form
    {
        public static bool IsAttached = false;
        bool BreakToggle = false;
        bool StopToggle = false;
        bool BreakStart = false;
        bool VelHackStart = false;
        bool VelHackToggle = false;
        bool SpeedHackToggle = false;
        bool SpeedHackStart = false;
        bool TurnAssistToggle = false;
        bool TurnAssistLeftStart = false;
        bool TurnAssistRightStart = false;
        bool WallNoClipToggle = false;
        bool CarNoClipToggle = false;
        bool CheckPointTPToggle = false;
        public static bool done = false;
        public static bool start = false;
        bool FovIncreaseStart = false; bool FovDecreaseStart = false;
        public static long BaseAddrLong; public static long Base2AddrLong; public static long Base3AddrLong; public static long Car1AddrLong; public static long Car2AddrLong; public static long Wall1AddrLong; public static long Wall2AddrLong; public static long FOVnopOutAddrLong; public static long FOVnopInAddrLong;
        public static long FirstPersonAddrLong; public static long DashAddrLong; public static long FrontAddrLong; public static long LowAddrLong; public static long BonnetAddrLong;
        public static string Base = "43 3a 5c 57 ? 4e 44 4f 57 53 5c 53 59 53 54 45 4d 33 32 5c 44";
        public static string Car1 = "48 89 ? ? ? 44 8B ? 48 89 ? ? ? BA";
        public static string Car2 = "0F 28 ? 41 0F ? ? ? 0F C6 D6 ? 41 0F";
        public static string Wall1 = "F3 0F ? ? ? 0F 59 ? 0F C6 ED ? 0F C6 F6";
        public static string Wall2 = "0F 28 ? 0F C6 C1 ? 0F 28 ? 0F C6 CB ? 41 0F ? ? F3 0F ? ? 41 0F ? ? 0F C6 C0 ? 0F C6 E4";
        public static string FOVOutsig = "4C 8D ? ? ? 0F 29 ? ? ? F3 0F";
        public static string FOVInsig = "48 81 EC ? ? ? ? 48 8B ? E8 ? ? ? ? 48 8B ? ? 48 8B";
        public static string FirstPerson = "80 00 80 82 43";
        public static string Dash = "3F 00 00 80 3F 00 00 80 3F 00 00 80 3F 01 ?? 00 00 00 00 00 00 00 00 A0 40";
        public static string Low = "80 CD CC 4C 3E CD CC CC 3E 9A 99 19 3F 00 00 80 3F";
        public static string Bonnet = "00 80 3E 63 B8 1E 3F 00 00 80 3F";
        public static string Front = "80 3E 7B 14 2E 3F 00 00 80 3F";
        public static string BaseAddr; public static string Base2Addr; public static string Base3Addr;
        public static string Car1Addr; public static string Car2Addr; public static string FOVnopOutAddr; public static string FOVnopInAddr;
        public static string Wall1Addr; public static string Wall2Addr;
        public static string FrontLeftAddr; public static string FrontRightAddr; public static string BackLeftAddr; public static string BackRightAddr;
        public static string OnGroundAddr; public static string InRaceAddr; public static string PastStartAddr;
        public static string xVelocityAddr; public static string yVelocityAddr; public static string zVelocityAddr;
        public static string xAddr; public static string yAddr; public static string zAddr;
        public static string CheckPointxAddr; public static string CheckPointyAddr; public static string CheckPointzAddr;
        public static string YawAddr; public static string RollAddr; public static string PitchAddr; public static string yAngVelAddr;
        public static string GasAddr; public static string FOVHighAddr; public static string FOVInAddr; public static string FirstPersonAddr; public static string DashAddr; public static string FrontAddr; public static string BonnetAddr; public static string LowAddr; public static string LowCompare;
        float xVelocityVal; float yVelocityVal; float zVelocityVal;
        float x; float y; float z;
        float CheckPointx; float CheckPointy; float CheckPointz;
        float BoostSpeed1; float BoostSpeed2; float BoostSpeed3; float BoostLim; //speed
        float TurnRatio; float TurnStrength; public float boost;
        float VelMult = 1; float FOVVal;
        int IncreaseCycles = 0; int DecreaseCycles = 0;
        int times1; int times2; int times3; int times4; //boost
        int BoostInterval1; int BoostInterval2; int BoostInterval3; int BoostInterval4; /*interval*/ int TurnInterval;
        int Velcycles; int NoClipcycles;
        public static int cycles = 0;
        KeyboardHook keyboardHook = new KeyboardHook();
        public Speedhack()
        {
            InitializeComponent();
            keyboardHook.Install();
            keyboardHook.KeyDown += new KeyboardHook.KeyboardHookCallback(keyboardHook_KeyDown);
            keyboardHook.KeyUp += new KeyboardHook.KeyboardHookCallback(keyboardHook_KeyUp);
            CheckForIllegalCrossThreadCalls = false;
        }
        private void keyboardHook_KeyDown(KeyboardHook.VKeys key)
        {
            key.ToString();
            if (key == KeyboardHook.VKeys.SPACE)
            {
                if (BreakToggle)
                {
                    BreakStart = true;
                }
                if (StopToggle)
                {
                    StopAllWheels();
                }
            }
            if (key == KeyboardHook.VKeys.LSHIFT)
            {
                if (VelHackToggle)
                {
                    VelHackStart = true;
                }
                if (SpeedHackToggle)
                {
                    if (SpeedHackStart == false)
                    {
                        boost = (float)Math.Ceiling(ToolInfo.m.ReadFloat(FrontLeftAddr));
                        SpeedHackStart = true;
                    }
                }
            }
            if (key == KeyboardHook.VKeys.KEY_A)
            {
                if (TurnAssistToggle)
                {
                    TurnAssistLeftStart = true;
                }
            }
            if (key == KeyboardHook.VKeys.KEY_D)
            {
                if (TurnAssistToggle)
                {
                    TurnAssistRightStart = true;
                }
            }
            if (key == KeyboardHook.VKeys.NUMPAD6)
            {
                if (FOV.Checked)
                {
                    FovIncreaseStart = true;
                }
            }
            if (key == KeyboardHook.VKeys.NUMPAD4)
            {
                if (FOV.Checked)
                {
                    FovDecreaseStart = true;
                }
            }
        }
        private void keyboardHook_KeyUp(KeyboardHook.VKeys key)
        {
            key.ToString();
            if (key == KeyboardHook.VKeys.SPACE)
            {
                if (BreakToggle)
                {
                    BreakStart = false;
                }
                if (StopToggle)
                {
                    StopAllWheels();
                }
            }
            if (key == KeyboardHook.VKeys.LSHIFT)
            {
                if (VelHackToggle)
                {
                    VelHackStart = false;
                }
                if (SpeedHackToggle)
                {
                    SpeedHackStart = false;
                }
            }
            if (key == KeyboardHook.VKeys.KEY_A)
            {
                if (TurnAssistToggle)
                {
                    TurnAssistLeftStart = false;
                }
            }
            if (key == KeyboardHook.VKeys.KEY_D)
            {
                if (TurnAssistToggle)
                {
                    TurnAssistRightStart = false;
                }
            }
            if (key == KeyboardHook.VKeys.NUMPAD6)
            {
                FovIncreaseStart = false;
                IncreaseCycles = 0;
            }
            if (key == KeyboardHook.VKeys.NUMPAD4)
            {
                FovDecreaseStart = false;
                DecreaseCycles = 0;
            }
        }
        public static void Addresses()
        {
            FrontLeftAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0xD18,0xC");
            FrontRightAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0xD20,0xC");
            BackLeftAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0xD38,0xC");
            BackRightAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0xD30,0xC");
            OnGroundAddr = (BaseAddr + ",0x550,0x260,0x258,0x4B0,0x640,0x368,0x10C");
            InRaceAddr = (Base2Addr + ",0x80,0x8,0x38,0x58,0x28,0x18,0x230");
            xVelocityAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,-0x540");
            yVelocityAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,-0x53C");
            zVelocityAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,-0x538");
            CheckPointxAddr = (Base3Addr + ",0x618,0x2F8,0xE0,0x198,0xA8,0x168,0x118,0xAA0");
            CheckPointyAddr = (Base3Addr + ",0x618,0x2F8,0xE0,0x198,0xA8,0x168,0x118,0xAA4");
            CheckPointzAddr = (Base3Addr + ",0x618,0x2F8,0xE0,0x198,0xA8,0x168,0x118,0xAA8");
            yAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,-0x51C");
            zAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,-0x518");
            xAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,-0x520");
            yAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,-0x51C");
            zAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,-0x518");
            YawAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,-0x3FC");
            RollAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,-0x418");
            PitchAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,-0x410");
            yAngVelAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,-0x52C");
            GasAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,0xD18,-0x53C");
            PastStartAddr = (Base2Addr + ",0x80,0x8,0x38,0x58,0x28,0x18,0x5C");
            FOVHighAddr = (BaseAddr + ",0x568,0x270,0x258,0xB8,0x348,0x70,0x5B0");
        }
        //end of setup
        public void Mainworker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (start)
            {
                if (ToolInfo.m.ReadFloat(PastStartAddr) == 1)
                {
                    if (WallNoClipToggle)
                    {
                        Noclip();
                    }
                    if (CarNoClipToggle)
                    {
                        var Jmp3 = new byte[6] { 0xE9, 0xB6, 0x01, 0x00, 0x00, 0x90 };
                        var Jmp4 = new byte[6] { 0xE9, 0x3B, 0x03, 0x00, 0x00, 0x90 };
                        for (int i = 0; i < 10; i++)
                        {
                            ToolInfo.m.WriteBytes(Car1Addr, Jmp3);
                            ToolInfo.m.WriteBytes(Car2Addr, Jmp4);
                        }
                        CarNoClipToggle = false;
                    }
                    if (VelHackToggle)
                    {
                        SpeedHackVel();
                    }
                    if (SpeedHackToggle)
                    {
                        SpeedHack();
                    }
                    if (TurnAssistToggle)
                    {
                        TurnAssistLeft();
                        TurnAssistRight();
                    }
                    if (BreakToggle)
                    {
                        SuperBreak();
                    }
                    if (FovIncreaseStart)
                    {
                        FOVIncrease();
                    }
                    if (FovDecreaseStart)
                    {
                        FOVdecrease();
                    }
                    if (Mainworker.CancellationPending)
                    {
                        e.Cancel = true;
                        start = false;
                    }
                    Thread.Sleep(1);
                }
                Thread.Sleep(1);
            }
        }
        //break hack methods
        public void SuperBreak()
        {
            if (BreakStart)
            {
                xVelocityVal = ToolInfo.m.ReadFloat(xVelocityAddr) * (float)0.50;
                zVelocityVal = ToolInfo.m.ReadFloat(zVelocityAddr) * (float)0.50;
                ToolInfo.m.WriteMemory(xVelocityAddr, "float", xVelocityVal.ToString());
                ToolInfo.m.WriteMemory(yVelocityAddr, "float", "0");
                ToolInfo.m.WriteMemory(zVelocityAddr, "float", zVelocityVal.ToString());
                ToolInfo.m.WriteMemory(YawAddr, "float", "0");
                Thread.Sleep(50);
            }
        }
        public void StopAllWheels()
        {
            ToolInfo.m.WriteMemory(FrontLeftAddr, "float", "0");
            ToolInfo.m.WriteMemory(FrontRightAddr, "float", "0");
            ToolInfo.m.WriteMemory(BackLeftAddr, "float", "0");
            ToolInfo.m.WriteMemory(BackRightAddr, "float", "0");
        }
        //end of break hacks
        //speed hack methods
        public void SpeedHackVel()
        {
            if (VelHackStart)
            {
                xVelocityVal = ToolInfo.m.ReadFloat(xVelocityAddr) * (float)VelMult;
                zVelocityVal = ToolInfo.m.ReadFloat(zVelocityAddr) * (float)VelMult;
                y = ToolInfo.m.ReadFloat(yAddr) - (float)0.02;
                ToolInfo.m.WriteMemory(xVelocityAddr, "float", xVelocityVal.ToString());
                ToolInfo.m.WriteMemory(zVelocityAddr, "float", zVelocityVal.ToString());
                ToolInfo.m.WriteMemory(yAddr, "float", y.ToString());
                Thread.Sleep(50);
            }
        }
        public void SpeedHack()
        {
            if (SpeedHackStart)
            {
                ToolInfo.m.WriteMemory(GasAddr, "float", "1");
                if (boost < BoostSpeed1)
                {
                    for (int i = 0; i < times1; i++)
                    {
                        boost++;
                    }
                    Thread.Sleep(BoostInterval1);
                }
                else if (boost < BoostSpeed2)
                {
                    for (int i = 0; i < times2; i++)
                    {
                        boost++;
                    }
                    Thread.Sleep(BoostInterval2);
                }
                else if (boost < BoostSpeed3)
                {
                    for (int i = 0; i < times3; i++)
                    {
                        boost++;
                    }
                    Thread.Sleep(BoostInterval3);
                }
                else
                {
                    for (int i = 0; i < times4; i++)
                    {
                        boost++;
                    }
                    Thread.Sleep(BoostInterval4);
                }
                if (boost >= BoostLim)
                {
                    boost = BoostLim;
                }
                ToolInfo.m.WriteMemory(FrontLeftAddr, "float", boost.ToString());
                ToolInfo.m.WriteMemory(FrontRightAddr, "float", boost.ToString());
                ToolInfo.m.WriteMemory(BackLeftAddr, "float", boost.ToString());
                ToolInfo.m.WriteMemory(BackRightAddr, "float", boost.ToString());
            }
        }
        //end of speed hacks
        //Turn assist methods
        public void TurnAssistLeft()
        {
            if (TurnAssistLeftStart)
            {
                float FrontLeft = ToolInfo.m.ReadFloat(FrontLeftAddr);
                float FrontRight = ToolInfo.m.ReadFloat(FrontRightAddr);
                float BackLeft = ToolInfo.m.ReadFloat(BackLeftAddr);
                float BackRight = ToolInfo.m.ReadFloat(BackRightAddr);
                if ((float)Math.Abs(FrontRight - FrontLeft) < (FrontRight / TurnRatio) && (float)Math.Abs(BackRight - FrontLeft) < (BackRight / TurnRatio))
                {
                    FrontLeft = FrontLeft - TurnStrength;
                    BackLeft = BackLeft - TurnStrength;
                    FrontRight = FrontRight + TurnStrength;
                    BackRight = BackRight + TurnStrength;
                    Thread.Sleep(TurnInterval);
                }
                ToolInfo.m.WriteMemory(FrontLeftAddr, "float", FrontLeft.ToString());
                ToolInfo.m.WriteMemory(FrontRightAddr, "float", FrontRight.ToString());
                ToolInfo.m.WriteMemory(BackLeftAddr, "float", BackLeft.ToString());
                ToolInfo.m.WriteMemory(BackRightAddr, "float", BackRight.ToString());
            }
        }
        public void TurnAssistRight()
        {
            if (TurnAssistRightStart)
            {
                float FrontLeft = ToolInfo.m.ReadFloat(FrontLeftAddr);
                float FrontRight = ToolInfo.m.ReadFloat(FrontRightAddr);
                float BackLeft = ToolInfo.m.ReadFloat(BackLeftAddr);
                float BackRight = ToolInfo.m.ReadFloat(BackRightAddr);
                if ((float)Math.Abs(FrontLeft - FrontRight) < (FrontLeft / TurnRatio) && (float)Math.Abs(BackLeft - FrontRight) < (BackLeft / TurnRatio))
                {
                    FrontRight = FrontRight - TurnStrength;
                    BackRight = BackRight - TurnStrength;
                    FrontLeft = FrontLeft + TurnStrength;
                    BackLeft = BackLeft + TurnStrength;
                    Thread.Sleep(TurnInterval);
                }
                ToolInfo.m.WriteMemory(FrontLeftAddr, "float", FrontLeft.ToString());
                ToolInfo.m.WriteMemory(FrontRightAddr, "float", FrontRight.ToString());
                ToolInfo.m.WriteMemory(BackLeftAddr, "float", BackLeft.ToString());
                ToolInfo.m.WriteMemory(BackRightAddr, "float", BackRight.ToString());
            }
        }
        //end of turn assists
        //teleport "script"
        public void CheckPointTPworker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (CheckPointTPToggle)
            {
                float InRace = ToolInfo.m.ReadFloat(InRaceAddr);
                if (InRace == 1)
                {
                    Thread.Sleep(3750);
                    while (InRace == 1)
                    {
                        InRace = ToolInfo.m.ReadFloat(InRaceAddr);
                        CheckPointTP();
                    }
                    ToolInfo.m.UnfreezeValue(yAngVelAddr);
                }
                Thread.Sleep(1);
            }
        }
        public void CheckPointTP()
        {
            Thread.Sleep(25);
            ToolInfo.m.WriteMemory(xAddr, "float", (ToolInfo.m.ReadFloat(CheckPointxAddr)).ToString());
            ToolInfo.m.WriteMemory(yAddr, "float", (ToolInfo.m.ReadFloat(CheckPointyAddr) + 4).ToString());
            ToolInfo.m.WriteMemory(zAddr, "float", (ToolInfo.m.ReadFloat(CheckPointzAddr)).ToString());
            ToolInfo.m.FreezeValue(yAngVelAddr, "float", "100");
        }
        //end of teleport "script"
        //noclip
        public void Noclip()
        {
            var Jmp1 = new byte[6] { 0xE9, 0x2A, 0x02, 0x00, 0x00, 0x90 };
            var Jmp2 = new byte[6] { 0xE9, 0x2B, 0x02, 0x00, 0x00, 0x90 };
            var Jmp1before = new byte[6] { 0x0F, 0x84, 0x29, 0x02, 0x00, 0x00 };
            var Jmp2before = new byte[6] { 0x0F, 0x84, 0x2A, 0x02, 0x00, 0x00 };
            float OnGround = ToolInfo.m.ReadFloat(OnGroundAddr);
            if (OnGround == 0)
            {
                NoClipcycles++;
                if (NoClipcycles % 10 == 0)
                {
                    OnGround = ToolInfo.m.ReadFloat(OnGroundAddr);
                    if (OnGround == 0)
                    {
                        ToolInfo.m.WriteBytes(Wall1Addr, Jmp1before);
                        ToolInfo.m.WriteBytes(Wall2Addr, Jmp2before);
                    }
                    NoClipcycles = 0;
                }
            }
            if (OnGround == 1)
            {
                NoClipcycles++;
                if (NoClipcycles % 10 == 0)
                {
                    ToolInfo.m.WriteBytes(Wall1Addr, Jmp1);
                    ToolInfo.m.WriteBytes(Wall2Addr, Jmp2);
                    NoClipcycles = 0;
                }
            }
        }
        private void TB_SHCarNoClip_CheckedChanged(object sender, EventArgs e)
        {
            var Jmp3before = new byte[6] { 0x0F, 0x84, 0xB5, 0x01, 0x00, 0x00 };
            var Jmp4before = new byte[6] { 0x0F, 0x84, 0x3A, 0x03, 0x00, 0x00 };

            if (TB_SHCarNoClip.Checked == false)
            {
                ToolInfo.m.WriteBytes(Car1Addr, Jmp3before);
                ToolInfo.m.WriteBytes(Car2Addr, Jmp4before);
            }
            else
            {
                CarNoClipToggle = true;
                start = true;
                if (Mainworker.IsBusy == false)
                {
                    Mainworker.RunWorkerAsync();
                }
            }
        }
        private void TB_SHWallNoClip_CheckedChanged(object sender, EventArgs e)
        {
            var Jmp1before = new byte[6] { 0x0F, 0x84, 0x29, 0x02, 0x00, 0x00 };
            var Jmp2before = new byte[6] { 0x0F, 0x84, 0x2A, 0x02, 0x00, 0x00 };
            if (TB_SHWallNoClip.Checked == false)
            {
                WallNoClipToggle = false;
                if (TurnAssistToggle == false
                && SpeedHackToggle == false
                && VelHackToggle == false
                && BreakToggle == false
                && WallNoClipToggle == false)
                {
                    Mainworker.CancelAsync();
                }
                //NoClipworker.CancelAsync();
                ToolInfo.m.WriteBytes(Wall1Addr, Jmp1before);
                ToolInfo.m.WriteBytes(Wall2Addr, Jmp2before);
            }
            else
            {
                WallNoClipToggle = true;
                start = true;
                //NoClipworker.RunWorkerAsync();
                if (Mainworker.IsBusy == false)
                {
                    Mainworker.RunWorkerAsync();
                }
            }
        }
        //end of noclip
        //speedhack buttons
        private void SuperBreakButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SuperBreakButton.Checked == false)
            {
                BreakToggle = false;
                if (TurnAssistToggle == false
                && SpeedHackToggle == false
                && VelHackToggle == false
                && BreakToggle == false
                && WallNoClipToggle == false)
                {
                    Mainworker.CancelAsync();
                }
                //Breakworker.CancelAsync();
            }
            else
            {
                BreakToggle = true;
                start = true;
                if (Mainworker.IsBusy == false)
                {
                    Mainworker.RunWorkerAsync();
                }
                //Breakworker.RunWorkerAsync();
            }
        }
        private void StopAllWheelsButton_CheckedChanged(object sender, EventArgs e)
        {
            if (StopAllWheelsButton.Checked == false)
            {
                StopToggle = false;
            }
            else
            {
                StopToggle = true;
            }
        }
        private void VelHackButton_CheckedChanged(object sender, EventArgs e)
        {
            if (VelHackButton.Checked == false)
            {
                VelHackToggle = false;
                if (TurnAssistToggle == false
                && SpeedHackToggle == false
                && VelHackToggle == false
                && BreakToggle == false
                && WallNoClipToggle == false)
                {
                    Mainworker.CancelAsync();
                }
                //VelHackworker.CancelAsync();
            }
            else
            {
                VelHackToggle = true;
                start = true;
                if (Mainworker.IsBusy == false)
                {
                    Mainworker.RunWorkerAsync();
                }
                //VelHackworker.RunWorkerAsync();
            }
        }
        private void WheelSpeedButton_CheckedChanged(object sender, EventArgs e)
        {
            if (WheelSpeedButton.Checked == false)
            {
                SpeedHackToggle = false;
                if (TurnAssistToggle == false
                && SpeedHackToggle == false
                && VelHackToggle == false
                && BreakToggle == false
                && WallNoClipToggle == false)
                {
                    Mainworker.CancelAsync();
                }
                //SpeedHackworker.CancelAsync();
            }
            else
            {
                SpeedHackToggle = true;
                start = true;
                if (Mainworker.IsBusy == false)
                {
                    Mainworker.RunWorkerAsync();
                }
                //SpeedHackworker.RunWorkerAsync();
            }
        }
        //end of speedhack stuff
        //turnassist button
        private void TurnAssistButton_CheckedChanged(object sender, EventArgs e)
        {
            if (TurnAssistButton.Checked == false)
            {
                TurnAssistToggle = false;
                if (TurnAssistToggle == false
                && SpeedHackToggle == false
                && VelHackToggle == false
                && BreakToggle == false
                && WallNoClipToggle == false)
                {
                    Mainworker.CancelAsync();
                }
                //TurnAssistworker.CancelAsync();
            }
            else
            {
                TurnAssistToggle = true;
                start = true;
                if (Mainworker.IsBusy == false)
                {
                    Mainworker.RunWorkerAsync();
                }
                //TurnAssistworker.RunWorkerAsync();
            }
        }
        //end of turn assist
        //teleports
        private void LST_TeleportLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LST_TeleportLocation.Text == "Festival")
            {
                x = (float)-2753.350098;
                y = (float)349.7218018;
                z = (float)-4357.629883;
            }
            if (LST_TeleportLocation.Text == "Start of Motorway")
            {
                x = (float)2657.887451;
                y = (float)270.7128906;
                z = (float)-4353.087402;
            }
            if (LST_TeleportLocation.Text == "Broadway")
            {
                x = (float)-237.2871857;
                y = (float)239.5045471;
                z = (float)-5816.858398;
            }

            if (LST_TeleportLocation.Text == "Greendale Airstrip")
            {
                x = (float)3409.570068;
                y = (float)159.2418976;
                z = (float)661.2498779;
            }
        }
        private void TPButton_Click(object sender, EventArgs e)
        {
            ToolInfo.m.WriteMemory(xAddr, "float", x.ToString());
            ToolInfo.m.WriteMemory(yAddr, "float", y.ToString());
            ToolInfo.m.WriteMemory(zAddr, "float", z.ToString());
        }
        private void CheckpointBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckpointBox.Checked == false)
            {
                CheckPointTPToggle = false;
                CheckPointTPworker.CancelAsync();
                ToolInfo.m.UnfreezeValue(yAngVelAddr);
            }
            else
            {
                CheckPointTPToggle = true;
                CheckPointTPworker.RunWorkerAsync();
            }
        }
        //end of teleports

        private void SetSpeedhackVal()
        {
            TurnIntervalBox.Value = Convert.ToDecimal(TurnInterval);
            RatioBox.Value = Convert.ToDecimal(TurnRatio);
            TurnStrengthBox.Value = Convert.ToDecimal(TurnStrength);
            VelMultBar.Value = Convert.ToInt32(VelMult);
            VelMultBox.Value = Convert.ToDecimal(VelMult);
            Speed1Box.Value = Convert.ToDecimal(BoostSpeed1);
            Speed2Box.Value = Convert.ToDecimal(BoostSpeed2);
            Speed3Box.Value = Convert.ToDecimal(BoostSpeed3);
            LimitBox.Value = Convert.ToDecimal(BoostLim);
            Interval1Box.Value = Convert.ToDecimal(BoostInterval1);
            Interval2Box.Value = Convert.ToDecimal(BoostInterval2);
            Interval3Box.Value = Convert.ToDecimal(BoostInterval3);
            Interval4Box.Value = Convert.ToDecimal(BoostInterval4);
            Boost1Box.Value = Convert.ToDecimal(times1);
            Boost2Box.Value = Convert.ToDecimal(times2);
            Boost3Box.Value = Convert.ToDecimal(times3);
            Boost4Box.Value = Convert.ToDecimal(times4);
        }
        private void TurnIntervalBox_ValueChanged(object sender, EventArgs e)
        {
            TurnInterval = Decimal.ToInt32(TurnIntervalBox.Value);
        }
        private void RatioBox_ValueChanged(object sender, EventArgs e)
        {
            TurnRatio = Decimal.ToSingle(RatioBox.Value);
        }

        private void TurnStrengthBox_ValueChanged(object sender, EventArgs e)
        {
            TurnStrength = Decimal.ToSingle(TurnStrengthBox.Value);
        }
        private void Speed1Box_ValueChanged(object sender, EventArgs e)
        {
            BoostSpeed1 = Decimal.ToSingle(Speed1Box.Value);
        }

        private void Speed2Box_ValueChanged(object sender, EventArgs e)
        {
            BoostSpeed2 = Decimal.ToSingle(Speed2Box.Value);
        }

        private void Speed3Box_ValueChanged(object sender, EventArgs e)
        {
            BoostSpeed3 = Decimal.ToSingle(Speed3Box.Value);
        }

        private void LimitBox_ValueChanged(object sender, EventArgs e)
        {
            BoostLim = Decimal.ToSingle(LimitBox.Value);
        }

        private void Interval1Box_ValueChanged(object sender, EventArgs e)
        {
            BoostInterval1 = Decimal.ToInt32(Interval1Box.Value);
        }

        private void Interval2Box_ValueChanged(object sender, EventArgs e)
        {
            BoostInterval2 = Decimal.ToInt32(Interval2Box.Value);
        }

        private void Interval3Box_ValueChanged(object sender, EventArgs e)
        {
            BoostInterval3 = Decimal.ToInt32(Interval3Box.Value);
        }

        private void Interval4Box_ValueChanged(object sender, EventArgs e)
        {
            BoostInterval4 = Decimal.ToInt32(Interval4Box.Value);
        }

        private void Boost1Box_ValueChanged(object sender, EventArgs e)
        {
            times1 = Decimal.ToInt32(Boost1Box.Value);
        }

        private void Boost2Box_ValueChanged(object sender, EventArgs e)
        {
            times2 = Decimal.ToInt32(Boost2Box.Value);
        }

        private void Boost3Box_ValueChanged(object sender, EventArgs e)
        {
            times3 = Decimal.ToInt32(Boost3Box.Value);
        }

        private void Boost4Box_ValueChanged(object sender, EventArgs e)
        {
            times4 = Decimal.ToInt32(Boost4Box.Value);
        }

        private void VelMultBar_Scroll(object sender, EventArgs e)
        {
            VelMultBox.Value = Convert.ToDecimal(VelMultBar.Value) / 100;
            VelMult = Decimal.ToSingle(VelMultBox.Value);
        }

        private void VelMultBox_ValueChanged(object sender, EventArgs e)
        {
            VelMultBar.Value = Decimal.ToInt32(VelMultBox.Value * 100);
            VelMult = Decimal.ToSingle(VelMultBox.Value);
        }

        public void SHReset()
        {
            TurnIntervalBox.Value = TurnInterval;
            RatioBox.Value = Convert.ToDecimal(TurnRatio);
            TurnStrengthBox.Value = Convert.ToDecimal(TurnStrength);
            Speed1Box.Value = Convert.ToDecimal(BoostSpeed1);
            Speed2Box.Value = Convert.ToDecimal(BoostSpeed2);
            Speed3Box.Value = Convert.ToDecimal(BoostSpeed3);
            LimitBox.Value = Convert.ToDecimal(BoostLim);
            Interval1Box.Value = Convert.ToDecimal(BoostInterval1);
            Interval2Box.Value = Convert.ToDecimal(BoostInterval2);
            Interval3Box.Value = Convert.ToDecimal(BoostInterval3);
            Interval4Box.Value = Convert.ToDecimal(BoostInterval4);
            Boost1Box.Value = Convert.ToDecimal(times1);
            Boost2Box.Value = Convert.ToDecimal(times2);
            Boost3Box.Value = Convert.ToDecimal(times3);
            Boost4Box.Value = Convert.ToDecimal(times4);
            VelMultBox.Value = Convert.ToDecimal(VelMult);
            VelMultBar.Value = (Convert.ToInt32(VelMult) * 100);
            FOVVal = (float)FOVBar.Value / 100;
        }
        public void ReadSpeedDefaultValues()
        {
            string SHini = "SpeedHackDefault.ini";
            bool Exists = File.Exists(SHini);
            if (Exists == true)
            {
                var SpeedHackparser = new FileIniDataParser();
                IniData SpeedHack = SpeedHackparser.ReadFile("SpeedHackDefault.ini");
                string CarNoClipStr = SpeedHack["No-Clip"]["Car"]; TB_SHCarNoClip.Checked = bool.Parse(CarNoClipStr);
                string WallNoClipStr = SpeedHack["No-Clip"]["Wall"]; TB_SHWallNoClip.Checked = bool.Parse(WallNoClipStr);
                string VelocityToggleStr = SpeedHack["Velocity"]["On"]; VelHackButton.Checked = bool.Parse(VelocityToggleStr);
                string VelocityMultStr = SpeedHack["Velocity"]["Multiplication"]; VelMult = float.Parse(VelocityMultStr);
                string SpeedHackToggleStr = SpeedHack["SpeedHack"]["On"]; WheelSpeedButton.Checked = bool.Parse(SpeedHackToggleStr);
                string Speed1Str = SpeedHack["SpeedHack"]["Speed 1"]; BoostSpeed1 = float.Parse(Speed1Str);
                string Speed2Str = SpeedHack["SpeedHack"]["Speed 2"]; BoostSpeed2 = float.Parse(Speed2Str);
                string Speed3Str = SpeedHack["SpeedHack"]["Speed 3"]; BoostSpeed3 = float.Parse(Speed3Str);
                string LimitStr = SpeedHack["SpeedHack"]["Limit"]; BoostLim = float.Parse(LimitStr);
                string Interval1Str = SpeedHack["SpeedHack"]["Interval up to speed 1"]; BoostInterval1 = Int32.Parse(Interval1Str);
                string Interval2Str = SpeedHack["SpeedHack"]["Interval up to speed 2"]; BoostInterval2 = Int32.Parse(Interval2Str);
                string Interval3Str = SpeedHack["SpeedHack"]["Interval up to speed 3"]; BoostInterval3 = Int32.Parse(Interval3Str);
                string Interval4Str = SpeedHack["SpeedHack"]["Interval above speed 3"]; BoostInterval4 = Int32.Parse(Interval4Str);
                string Boost1Str = SpeedHack["SpeedHack"]["Boost up to speed 1"]; times1 = Int32.Parse(Boost1Str);
                string Boost2Str = SpeedHack["SpeedHack"]["Boost up to speed 2"]; times2 = Int32.Parse(Boost2Str);
                string Boost3Str = SpeedHack["SpeedHack"]["Boost up to speed 3"]; times3 = Int32.Parse(Boost3Str);
                string Boost4Str = SpeedHack["SpeedHack"]["Boost above speed 3"]; times4 = Int32.Parse(Boost4Str);
                string SuperBreakStr = SpeedHack["Break"]["Superbreak on"]; SuperBreakButton.Checked = bool.Parse(SuperBreakStr);
                string StopWheelsStr = SpeedHack["Break"]["Stop all wheels on"]; StopAllWheelsButton.Checked = bool.Parse(StopWheelsStr);
                string TurnToggleStr = SpeedHack["Turn assist"]["On"]; TurnAssistButton.Checked = bool.Parse(TurnToggleStr);
                string TurnStrengthStr = SpeedHack["Turn assist"]["Strength"]; TurnStrength = float.Parse(TurnStrengthStr);
                string TurnRatioStr = SpeedHack["Turn assist"]["Ratio"]; TurnRatio = float.Parse(TurnRatioStr);
                string TurnIntervalStr = SpeedHack["Turn assist"]["Interval"]; TurnInterval = Int32.Parse(TurnIntervalStr);
            }
            else
            {
                CreateSHini();
                ReadSpeedDefaultValues();
            }
        }
        public void WriteSpeedDefaultValues()
        {
            var SpeedHackparser = new FileIniDataParser();
            IniData SpeedHack = new IniData();
            SpeedHack["No-Clip"]["Car"] = TB_SHCarNoClip.Checked.ToString();
            SpeedHack["No-Clip"]["Wall"] = TB_SHWallNoClip.Checked.ToString();
            SpeedHack["Velocity"]["On"] = VelHackButton.Checked.ToString();
            SpeedHack["Velocity"]["Multiplication"] = VelMult.ToString();
            SpeedHack["SpeedHack"]["On"] = WheelSpeedButton.Checked.ToString();
            SpeedHack["SpeedHack"]["Speed 1"] = BoostSpeed1.ToString();
            SpeedHack["SpeedHack"]["Speed 2"] = BoostSpeed2.ToString();
            SpeedHack["SpeedHack"]["Speed 3"] = BoostSpeed3.ToString();
            SpeedHack["SpeedHack"]["Limit"] = BoostLim.ToString();
            SpeedHack["SpeedHack"]["Interval up to speed 1"] = BoostInterval1.ToString();
            SpeedHack["SpeedHack"]["Interval up to speed 2"] = BoostInterval2.ToString();
            SpeedHack["SpeedHack"]["Interval up to speed 3"] = BoostInterval3.ToString();
            SpeedHack["SpeedHack"]["Interval above speed 3"] = BoostInterval4.ToString();
            SpeedHack["SpeedHack"]["Boost up to speed 1"] = times1.ToString();
            SpeedHack["SpeedHack"]["Boost up to speed 2"] = times2.ToString();
            SpeedHack["SpeedHack"]["Boost up to speed 3"] = times3.ToString();
            SpeedHack["SpeedHack"]["Boost above speed 3"] = times4.ToString();
            SpeedHack["Break"]["Superbreak on"] = SuperBreakButton.Checked.ToString();
            SpeedHack["Break"]["Stop all wheels on"] = StopAllWheelsButton.Checked.ToString();
            SpeedHack["Turn assist"]["On"] = TurnAssistButton.Checked.ToString();
            SpeedHack["Turn assist"]["Strength"] = TurnStrength.ToString();
            SpeedHack["Turn assist"]["Ratio"] = TurnRatio.ToString();
            SpeedHack["Turn assist"]["Interval"] = TurnInterval.ToString();
            SpeedHackparser.WriteFile("SpeedHackDefault.ini", SpeedHack);
        }
        public void CreateSHini()
        {
            var SpeedHackparser = new FileIniDataParser();
            IniData SpeedHack = new IniData();
            SpeedHack["No-Clip"]["Car"] = "false";
            SpeedHack["No-Clip"]["Wall"] = "false";
            SpeedHack["Velocity"]["On"] = "false";
            SpeedHack["Velocity"]["Multiplication"] = "1";
            SpeedHack["SpeedHack"]["On"] = "false";
            SpeedHack["SpeedHack"]["Speed 1"] = "0";
            SpeedHack["SpeedHack"]["Speed 2"] = "0";
            SpeedHack["SpeedHack"]["Speed 3"] = "0";
            SpeedHack["SpeedHack"]["Limit"] = "0";
            SpeedHack["SpeedHack"]["Interval up to speed 1"] = "0";
            SpeedHack["SpeedHack"]["Interval up to speed 2"] = "0";
            SpeedHack["SpeedHack"]["Interval up to speed 3"] = "0";
            SpeedHack["SpeedHack"]["Interval above speed 3"] = "0";
            SpeedHack["SpeedHack"]["Boost up to speed 1"] = "0";
            SpeedHack["SpeedHack"]["Boost up to speed 2"] = "0";
            SpeedHack["SpeedHack"]["Boost up to speed 3"] = "0";
            SpeedHack["SpeedHack"]["Boost above speed 3"] = "0";
            SpeedHack["Break"]["Superbreak on"] = "false";
            SpeedHack["Break"]["Stop all wheels on"] = "false";
            SpeedHack["Turn assist"]["On"] = "false";
            SpeedHack["Turn assist"]["Strength"] = "0";
            SpeedHack["Turn assist"]["Ratio"] = "0";
            SpeedHack["Turn assist"]["Interval"] = "0";
            SpeedHackparser.SaveFile("SpeedHackDefault.ini", SpeedHack);
        }
        private void SaveSHDefault_Click(object sender, EventArgs e)
        {
            WriteSpeedDefaultValues();
        }
        private void LoadSHDefault_Click(object sender, EventArgs e)
        {
            ReadSpeedDefaultValues();
            SHReset();
        }
        private void FOVBar_Scroll(object sender, EventArgs e)
        {
            //FOVBar.Value = FOVBar.Value / 100;
            FOVVal = (float)FOVBar.Value / 100;
            if (FOV.Checked == true)
            {
                FOV.Checked = false;
                FOV.Checked = true;
            }
            SHReset();
        }
        private async void FOVScan_BTN_Click(object sender, EventArgs e)
        {
            string FOVHigh = ToolInfo.m.GetCode(FOVHighAddr).ToString();
            FOVScan_BTN.Hide();
            FOVScan_bar.Show();
            bool scan = true;
            cycles = 0;
            while (scan)
            {
                Thread.Sleep(1);
                if (FirstPersonAddr == "FFFFFFFFFFFFFFB5" || FirstPersonAddr == null || FirstPersonAddr == "0")
                {
                    if (cycles < 1)
                    {
                        cycles++;
                        FirstPersonAddrLong = (await ToolInfo.m.AoBScan(0x10000000000, 0x2FFFFFFFFFF, FirstPerson, true, true)).FirstOrDefault() - 75;
                    }
                    FirstPersonAddr = FirstPersonAddrLong.ToString("X");
                }
                else if (DashAddr == "FFFFFFFFFFFFFF45" || DashAddr == null || DashAddr == "0")
                {
                    FOVScan_bar.Value = 20;
                    if (cycles < 2)
                    {
                        cycles++;
                        DashAddrLong = (await ToolInfo.m.AoBScan(0x10000000000, 0x2FFFFFFFFFF, Dash, true, true)).FirstOrDefault() - 187;
                    }
                    DashAddr = DashAddrLong.ToString("X");
                }
                else if (FrontAddr == "FFFFFFFFFFFFFF42" || FrontAddr == null || FrontAddr == "0")
                {
                    FOVScan_bar.Value = 40;
                    if (cycles < 3)
                    {
                        cycles++;
                        FrontAddrLong = (await ToolInfo.m.AoBScan(0x10000000000, 0x2FFFFFFFFFF, Front, true, true)).FirstOrDefault() - 190;
                    }
                    FrontAddr = FrontAddrLong.ToString("X");
                }
                else if (LowAddr == "FFFFFFFFFFFFFF49" || LowAddr == null || LowAddr == "0")
                {
                    FOVScan_bar.Value = 60;
                    if (cycles < 4)
                    {
                        cycles++;
                        LowAddrLong = (await ToolInfo.m.AoBScan(0x10000000000, 0x2FFFFFFFFFF, Low, true, true)).FirstOrDefault() - 183;
                    }
                    LowCompare = LowAddrLong.ToString();
                    if (LowCompare == ToolInfo.m.GetCode(FOVHighAddr).ToString())
                    {
                        LowAddrLong = (await ToolInfo.m.AoBScan(0x10000000000, 0x2FFFFFFFFFF, Low, true, true)).LastOrDefault() - 183;
                    }
                    LowAddr = LowAddrLong.ToString("X");
                }
                else if (BonnetAddr == "FFFFFFFFFFFFFF43" || BonnetAddr == null || DashAddr == "0")
                {
                    FOVScan_bar.Value = 80;
                    if (cycles < 5)
                    {
                        cycles++;
                        BonnetAddrLong = (await ToolInfo.m.AoBScan(0x10000000000, 0x2FFFFFFFFFF, Bonnet, true, true)).FirstOrDefault() - 189;
                    }
                    BonnetAddr = BonnetAddrLong.ToString("X");
                }
                if (FirstPersonAddr == "FFFFFFFFFFFFFFB5" || FirstPersonAddr == null
                    || FrontAddr == "FFFFFFFFFFFFFF42" || FrontAddr == null || FrontAddr == "0"
                    || LowAddr == "FFFFFFFFFFFFFF49" || LowAddr == null || LowAddr == "0"
                    || BonnetAddr == "FFFFFFFFFFFFFF43" || BonnetAddr == null || BonnetAddr == "0"
                    || FirstPersonAddr == "0" || DashAddr == null || DashAddr == "0")
                {
                    ;
                }
                else
                {
                    FOVScan_bar.Value = 100;
                    Thread.Sleep(1000);
                    FOVScan_bar.Hide();
                    //FOVScan_BTN.Enabled = false;
                    FOV.Show();//FOV.Enabled = true;
                    scan = false;
                }
            }
        }
        private void FOV_CheckedChanged(object sender, EventArgs e)
        {
            var nopoutbefore = new byte[4] { 0x0F, 0x11, 0x43, 0x10 };
            var nopinbefore = new byte[4] { 0x0F, 0x11, 0x73, 0x10 };
            var nop = new byte[4] { 0x90, 0x90, 0x90, 0x90 };
            SHReset();
            if (FOV.Checked == false)
            {
                ToolInfo.m.WriteBytes(FOVnopOutAddr, nopoutbefore);
                ToolInfo.m.WriteBytes(FOVnopInAddr, nopinbefore);
                ToolInfo.m.UnfreezeValue(FOVHighAddr);
                ToolInfo.m.UnfreezeValue(FirstPersonAddr);
                ToolInfo.m.UnfreezeValue(DashAddr);
                ToolInfo.m.UnfreezeValue(LowAddr);
                ToolInfo.m.UnfreezeValue(BonnetAddr);
                ToolInfo.m.UnfreezeValue(FrontAddr);
            }
            else
            {
                ToolInfo.m.WriteBytes(FOVnopOutAddr, nop);
                ToolInfo.m.WriteBytes(FOVnopInAddr, nop);
                ToolInfo.m.FreezeValue(FOVHighAddr, "float", FOVVal.ToString());
                ToolInfo.m.FreezeValue(FirstPersonAddr, "float", FOVVal.ToString());
                ToolInfo.m.FreezeValue(DashAddr, "float", FOVVal.ToString());
                ToolInfo.m.FreezeValue(LowAddr, "float", FOVVal.ToString());
                ToolInfo.m.FreezeValue(BonnetAddr, "float", FOVVal.ToString());
                ToolInfo.m.FreezeValue(FrontAddr, "float", FOVVal.ToString());
            }
        }
        private void FOVIncrease()
        {
            IncreaseCycles++;
            if (FOVBar.Value > 149)
                FOVBar.Value = 149;
            FOVBar.Value = FOVBar.Value + 1;
            FOVVal = (float)FOVBar.Value / 100;
            ToolInfo.m.FreezeValue(FOVHighAddr, "float", FOVVal.ToString());
            ToolInfo.m.FreezeValue(FirstPersonAddr, "float", FOVVal.ToString());
            ToolInfo.m.FreezeValue(DashAddr, "float", FOVVal.ToString());
            ToolInfo.m.FreezeValue(LowAddr, "float", FOVVal.ToString());
            ToolInfo.m.FreezeValue(BonnetAddr, "float", FOVVal.ToString());
            ToolInfo.m.FreezeValue(FrontAddr, "float", FOVVal.ToString());
        }
        private void FOVdecrease()
        {
            DecreaseCycles++;
            if (FOVBar.Value < -94)
                FOVBar.Value = -94;
            FOVBar.Value = FOVBar.Value - 1;
            FOVVal = (float)FOVBar.Value / 100;
            ToolInfo.m.FreezeValue(FOVHighAddr, "float", FOVVal.ToString());
            ToolInfo.m.FreezeValue(FirstPersonAddr, "float", FOVVal.ToString());
            ToolInfo.m.FreezeValue(DashAddr, "float", FOVVal.ToString());
            ToolInfo.m.FreezeValue(LowAddr, "float", FOVVal.ToString());
            ToolInfo.m.FreezeValue(BonnetAddr, "float", FOVVal.ToString());
            ToolInfo.m.FreezeValue(FrontAddr, "float", FOVVal.ToString());
        }
    }
}
