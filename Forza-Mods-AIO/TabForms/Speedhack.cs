using System;
using System.IO;
using System.Diagnostics;
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
using System.Runtime.InteropServices;

namespace Forza_Mods_AIO.TabForms
{

    public partial class Speedhack : Form
    {
        KeyboardHook keyboardHook = new KeyboardHook();
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
        public static bool Velstart = false; public static bool NCstart = false; public static bool FOVstart = false; public static bool Timestart = false; public static bool Breakstart = false; public static bool Speedstart = false; public static bool Turnstart = false;
        bool FovIncreaseStart = false; bool FovDecreaseStart = false;
        bool TimeToggle = false;  bool TimeForwardStart = false; bool TimeBackStart = false;
        public static long TimeNOPAddrLong;
        public static long BaseAddrLong; public static long Base2AddrLong; public static long Base3AddrLong; public static long Base4AddrLong; public static long Car1AddrLong; public static long Car2AddrLong; public static long Wall1AddrLong; public static long Wall2AddrLong; public static long FOVnopOutAddrLong; public static long FOVnopInAddrLong;
        public static long FirstPersonAddrLong; public static long DashAddrLong; public static long FrontAddrLong; public static long LowAddrLong; public static long BonnetAddrLong;
        public static string Base = "43 3a 5c 57 ? 4e 44 4f 57 53 5c 53 59 53 54 45 4d 33 32 5c 44";
        public static string Car1 = "48 89 ? ? ? 44 8B ? 48 89 ? ? ? BA";
        public static string Car2 = "0F 28 ? 41 0F ? ? ? 0F C6 D6 ? 41 0F";
        public static string Wall1 = "F3 0F ? ? ? 0F 59 ? 0F C6 ED ? 0F C6 F6";
        public static string Wall2 = "0F 28 ? 0F C6 C1 ? 0F 28 ? 0F C6 CB ? 41 0F ? ? F3 0F ? ? 41 0F ? ? 0F C6 C0 ? 0F C6 E4";
        public static string FOVOutsig = "4C 8D ? ? ? 0F 29 ? ? ? F3 0F";
        public static string FOVInsig = "48 81 EC ? ? ? ? 48 8B ? E8 ? ? ? ? 48 8B ? ? 48 8B";
        public static string Timesig = "F2 0F 11 43 08 48 83";
        public static string FirstPerson = "80 00 80 82 43";
        public static string Dash = "3F 00 00 80 3F 00 00 80 3F 00 00 80 3F 01 ?? 00 00 00 00 00 00 00 00 A0 40";
        public static string Low = "80 CD CC 4C 3E CD CC CC 3E 9A 99 19 3F 00 00 80 3F";
        public static string Bonnet = "00 80 3E 63 B8 1E 3F 00 00 80 3F";
        //public static string Front = "A0 41 01 00 8C 42 00 00 11 43 00 00 3E 43 00 00 00 80 00 00 00 80 00 00 80 3E 7B 14 2E 3F";
        public static string Front = "80 3E 7B 14 2E 3F 00 00 80 3F";
        public static string GravityAddr; public static string WeirdAddr;
        public static string BaseAddr; public static string Base2Addr; public static string Base3Addr; public static string Base4Addr;
        public static string Car1Addr; public static string Car2Addr; public static string FOVnopOutAddr; public static string FOVnopInAddr;
        public static string TimeNOPAddr; public static string TimeAddr;
        public static string Wall1Addr; public static string Wall2Addr;
        public static string FrontLeftAddr; public static string FrontRightAddr; public static string BackLeftAddr; public static string BackRightAddr;
        public static string OnGroundAddr; public static string InRaceAddr; public static string PastStartAddr;
        public static string xVelocityAddr; public static string yVelocityAddr; public static string zVelocityAddr;
        public static string xAddr; public static string yAddr; public static string zAddr;
        public static string CheckPointxAddr; public static string CheckPointyAddr; public static string CheckPointzAddr;
        public static string YawAddr; public static string RollAddr; public static string PitchAddr; public static string yAngVelAddr;
        public static string GasAddr; public static string FOVHighAddr; public static string FOVInAddr; public static string FirstPersonAddr; public static string DashAddr; public static string FrontAddr; public static string BonnetAddr; public static string LowAddr; public static string LowCompare;
        public static IntPtr InjectAddress;
        public static string TimeAddrAddr;
        public static string allocationstring;
        float xVelocityVal; float yVelocityVal; float zVelocityVal;
        float x; float y; float z;
        float CheckPointx; float CheckPointy; float CheckPointz;
        float BoostSpeed1; float BoostSpeed2; float BoostSpeed3; float BoostLim; //speed
        float TurnRatio; float TurnStrength; public float boost;
        float VelMult = 1; float FOVVal;
        public int StorageAddress;
        int IncreaseCycles = 0; int DecreaseCycles = 0;
        int times1; int times2; int times3; int times4; //boost
        int BoostInterval1; int BoostInterval2; int BoostInterval3; int BoostInterval4; /*interval*/ int TurnInterval;
        int Velcycles; int NoClipcycles;
        float WeirdVal; float NewWeirdVal; float GravityVal; float NewGravityVal;
        long ScanStartAddr;
        long ScanEndAddr;
        public static int cycles = 0;
        private readonly static Dictionary<char, byte> hexmap = new Dictionary<char, byte>()
        {
            { 'a', 0xA },{ 'b', 0xB },{ 'c', 0xC },{ 'd', 0xD },
            { 'e', 0xE },{ 'f', 0xF },{ 'A', 0xA },{ 'B', 0xB },
            { 'C', 0xC },{ 'D', 0xD },{ 'E', 0xE },{ 'F', 0xF },
            { '0', 0x0 },{ '1', 0x1 },{ '2', 0x2 },{ '3', 0x3 },
            { '4', 0x4 },{ '5', 0x5 },{ '6', 0x6 },{ '7', 0x7 },
            { '8', 0x8 },{ '9', 0x9 }
        };

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
            Debug.WriteLine("KeyDown:" + key);
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
            if (key == KeyboardHook.VKeys.LSHIFT || key == KeyboardHook.VKeys.RIGHT || key == KeyboardHook.VKeys.LEFT)
            {
                if (VelHackToggle)
                {
                    VelHackStart = true;
                }
                if (SpeedHackToggle)
                {
                    if (SpeedHackStart == false)
                    {
                        boost = (float)Math.Ceiling(MainWindow.m.ReadFloat(FrontLeftAddr));
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
            if (key == KeyboardHook.VKeys.NUMPAD6 || key == KeyboardHook.VKeys.RIGHT)
            {
                if (FOV.Checked)
                {
                    FovIncreaseStart = true;
                }
                if (TimeToggle)
                {
                    TimeForwardStart = true;
                }
            }
            if (key == KeyboardHook.VKeys.NUMPAD4 || key == KeyboardHook.VKeys.LEFT)
            {
                if (FOV.Checked)
                {
                    FovDecreaseStart = true;
                }
                if (TimeToggle)
                {
                    TimeBackStart = true;
                }
            }
        }
        private void keyboardHook_KeyUp(KeyboardHook.VKeys key)
        {
            key.ToString();
            Debug.WriteLine("KeyUP:" + key);
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
            if (key == KeyboardHook.VKeys.NUMPAD6 || key == KeyboardHook.VKeys.RIGHT)
            {
                FovIncreaseStart = false;
                if (TimeToggle)
                {
                    TimeForwardStart = false;
                }
            }
            if (key == KeyboardHook.VKeys.NUMPAD4 || key == KeyboardHook.VKeys.LEFT)
            {
                FovDecreaseStart = false;
                if (TimeToggle)
                {
                    TimeBackStart = false;
                }
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
            WeirdAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,-0x554");
            GravityAddr = (BaseAddr + ",0x2E0,0x58,0x60,0x1A0,0x60,-0x558");
        }
        //end of setup

        //BG Workers
        public void VelHackWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (Velstart)
            {
                float PastStart = MainWindow.m.ReadFloat(PastStartAddr);
                if (PastStart == 1)
                {
                    if (VelHackToggle)
                    {
                        SpeedHackVel();
                    }
                    if (VelHackWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        Velstart = false;
                    }
                    Thread.Sleep(1);
                }
                Thread.Sleep(1);
            }
        }
        public void NoClipWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (NCstart)
            {
                float PastStart = MainWindow.m.ReadFloat(PastStartAddr);
                if (PastStart == 1)
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
                            MainWindow.m.WriteBytes(Car1Addr, Jmp3);
                            MainWindow.m.WriteBytes(Car2Addr, Jmp4);
                        }
                        CarNoClipToggle = false;
                    }
                    if (NoClipWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        NCstart = false;
                    }
                    Thread.Sleep(1);
                }
                Thread.Sleep(1);
            }
        }
        public void SpeedHackWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (Speedstart)
            {
                float PastStart = MainWindow.m.ReadFloat(PastStartAddr);
                if (PastStart == 1)
                {
                    if (SpeedHackToggle)
                    {
                        SpeedHack();
                    }
                    if (SpeedHackWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        Speedstart = false;
                    }
                    Thread.Sleep(1);
                }
                Thread.Sleep(1);
            }
        }
        public void TurnWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (Turnstart)
            {
                float PastStart = MainWindow.m.ReadFloat(PastStartAddr);
                if (PastStart == 1)
                {
                    if (TurnAssistToggle)
                    {
                        TurnAssistLeft();
                        TurnAssistRight();
                    }
                    if (TurnWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        Turnstart = false;
                    }
                    Thread.Sleep(1);
                }
                Thread.Sleep(1);
            }
        }
        public void FOVWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (FOVstart)
            {
                float PastStart = MainWindow.m.ReadFloat(PastStartAddr);
                if (PastStart == 1)
                {
                    if (FovIncreaseStart)
                    {
                        FOVIncrease();
                    }
                    if (FovDecreaseStart)
                    {
                        FOVdecrease();
                    }
                    if (FOVWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        FOVstart = false;
                    }
                    Thread.Sleep(1);
                }
                Thread.Sleep(1);
            }
        }
        public void SuperBreakWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (Breakstart)
            {
                float PastStart = MainWindow.m.ReadFloat(PastStartAddr);
                if (PastStart == 1)
                {
                    if (BreakToggle)
                    {
                        SuperBreak();
                    }
                    if (SuperBreakWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        Breakstart = false;
                    }
                    Thread.Sleep(1);
                }
                Thread.Sleep(1);
            }
        }
        public void TimeWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (Timestart)
            {
                float PastStart = MainWindow.m.ReadFloat(PastStartAddr);
                if (PastStart == 1)
                {
                    if (TimeToggle)
                    {
                        TimeForward();
                        TimeBack();
                    }
                    if (TimeWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        Timestart = false;
                    }
                    Thread.Sleep(1);
                }
                Thread.Sleep(1);
            }
        }
        //end of BG Workers

        //break hack methods
        public void SuperBreak()
        {
            if (BreakStart)
            {
                xVelocityVal = MainWindow.m.ReadFloat(xVelocityAddr) * (float)0.50;
                zVelocityVal = MainWindow.m.ReadFloat(zVelocityAddr) * (float)0.50;
                MainWindow.m.WriteMemory(xVelocityAddr, "float", xVelocityVal.ToString());
                MainWindow.m.WriteMemory(yVelocityAddr, "float", "0");
                MainWindow.m.WriteMemory(zVelocityAddr, "float", zVelocityVal.ToString());
                MainWindow.m.WriteMemory(YawAddr, "float", "0");
                Thread.Sleep(50);
            }
        }
        public void StopAllWheels()
        {
            MainWindow.m.WriteMemory(FrontLeftAddr, "float", "0");
            MainWindow.m.WriteMemory(FrontRightAddr, "float", "0");
            MainWindow.m.WriteMemory(BackLeftAddr, "float", "0");
            MainWindow.m.WriteMemory(BackRightAddr, "float", "0");
        }
        //end of break hacks

        //speed hack methods
        public void SpeedHackVel()
        {
            if (VelHackStart)
            {
                xVelocityVal = MainWindow.m.ReadFloat(xVelocityAddr) * (float)VelMult;
                zVelocityVal = MainWindow.m.ReadFloat(zVelocityAddr) * (float)VelMult;
                y = MainWindow.m.ReadFloat(yAddr) - (float)0.02;
                MainWindow.m.WriteMemory(xVelocityAddr, "float", xVelocityVal.ToString());
                MainWindow.m.WriteMemory(zVelocityAddr, "float", zVelocityVal.ToString());
                MainWindow.m.WriteMemory(yAddr, "float", y.ToString());
                Thread.Sleep(50);
            }
        }
        public void SpeedHack()
        {
            if (SpeedHackStart)
            {
                MainWindow.m.WriteMemory(GasAddr, "float", "1");
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
                MainWindow.m.WriteMemory(FrontLeftAddr, "float", boost.ToString());
                MainWindow.m.WriteMemory(FrontRightAddr, "float", boost.ToString());
                MainWindow.m.WriteMemory(BackLeftAddr, "float", boost.ToString());
                MainWindow.m.WriteMemory(BackRightAddr, "float", boost.ToString());
            }
        }
        //end of speed hacks

        //Turn assist methods
        public void TurnAssistLeft()
        {
            if (TurnAssistLeftStart)
            {
                float FrontLeft = MainWindow.m.ReadFloat(FrontLeftAddr);
                float FrontRight = MainWindow.m.ReadFloat(FrontRightAddr);
                float BackLeft = MainWindow.m.ReadFloat(BackLeftAddr);
                float BackRight = MainWindow.m.ReadFloat(BackRightAddr);
                if ((float)Math.Abs(FrontRight - FrontLeft) < (FrontRight / TurnRatio) && (float)Math.Abs(BackRight - FrontLeft) < (BackRight / TurnRatio))
                {
                    FrontLeft = FrontLeft - TurnStrength;
                    BackLeft = BackLeft - TurnStrength;
                    FrontRight = FrontRight + TurnStrength;
                    BackRight = BackRight + TurnStrength;
                    Thread.Sleep(TurnInterval);
                }
                MainWindow.m.WriteMemory(FrontLeftAddr, "float", FrontLeft.ToString());
                MainWindow.m.WriteMemory(FrontRightAddr, "float", FrontRight.ToString());
                MainWindow.m.WriteMemory(BackLeftAddr, "float", BackLeft.ToString());
                MainWindow.m.WriteMemory(BackRightAddr, "float", BackRight.ToString());
            }
        }
        public void TurnAssistRight()
        {
            if (TurnAssistRightStart)
            {
                float FrontLeft = MainWindow.m.ReadFloat(FrontLeftAddr);
                float FrontRight = MainWindow.m.ReadFloat(FrontRightAddr);
                float BackLeft = MainWindow.m.ReadFloat(BackLeftAddr);
                float BackRight = MainWindow.m.ReadFloat(BackRightAddr);
                if ((float)Math.Abs(FrontLeft - FrontRight) < (FrontLeft / TurnRatio) && (float)Math.Abs(BackLeft - FrontRight) < (BackLeft / TurnRatio))
                {
                    FrontRight = FrontRight - TurnStrength;
                    BackRight = BackRight - TurnStrength;
                    FrontLeft = FrontLeft + TurnStrength;
                    BackLeft = BackLeft + TurnStrength;
                    Thread.Sleep(TurnInterval);
                }
                MainWindow.m.WriteMemory(FrontLeftAddr, "float", FrontLeft.ToString());
                MainWindow.m.WriteMemory(FrontRightAddr, "float", FrontRight.ToString());
                MainWindow.m.WriteMemory(BackLeftAddr, "float", BackLeft.ToString());
                MainWindow.m.WriteMemory(BackRightAddr, "float", BackRight.ToString());
            }
        }
        //end of turn assists

        //Time
        public void GetTimeAddr()
        {
            static byte[] StringToBytes(string hex)
            {
                if (string.IsNullOrWhiteSpace(hex))
                    throw new ArgumentException("Hex cannot be null/empty/whitespace");

                if (hex.Length % 2 != 0)
                    throw new FormatException("Hex must have an even number of characters");

                bool startsWithHexStart = hex.StartsWith("0x", StringComparison.OrdinalIgnoreCase);

                if (startsWithHexStart && hex.Length == 2)
                    throw new ArgumentException("There are no characters in the hex string");


                int startIndex = startsWithHexStart ? 2 : 0;

                byte[] bytesArr = new byte[(hex.Length - startIndex) / 2];

                char left;
                char right;

                try
                {
                    int x = 0;
                    for (int i = startIndex; i < hex.Length; i += 2, x++)
                    {
                        left = hex[i];
                        right = hex[i + 1];
                        bytesArr[x] = (byte)((hexmap[left] << 4) | hexmap[right]);
                    }
                    return bytesArr;
                }
                catch (KeyNotFoundException)
                {
                    throw new FormatException("Hex string has non-hex character");
                }
            }

            static byte[] longToByteArray(long data)
            {
                return new byte[] {
                (byte)((data >> 56) & 0xff),
                (byte)((data >> 48) & 0xff),
                (byte)((data >> 40) & 0xff),
                (byte)((data >> 32) & 0xff),
                (byte)((data >> 24) & 0xff),
                (byte)((data >> 16) & 0xff),
                (byte)((data >> 8 ) & 0xff),
                (byte)((data >> 0) & 0xff),
                };
            }

            [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
            static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

            const int PROCESS_CREATE_THREAD = 0x0002;
            const int PROCESS_QUERY_INFORMATION = 0x0400;
            const int PROCESS_VM_OPERATION = 0x0008;
            const int PROCESS_VM_WRITE = 0x0020;
            const int PROCESS_VM_READ = 0x0010;

            const uint MEM_COMMIT = 0x00001000;
            const uint MEM_RESERVE = 0x00002000;
            const uint PAGE_READWRITE = 0x4;
            const uint PAGE_EXECUTE_READWRITE = 0x40;

            [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
            static extern IntPtr VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint dwFreeType);

            const uint MEM_DECOMMIT = 0x00004000;
            const uint MEM_RELEASE = 0x00008000;

            IntPtr CodeCave = VirtualAllocEx(Process.GetProcessesByName("ForzaHorizon4")[0].Handle, IntPtr.Zero, 0x256, MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE); // returns the pointer to the allocated memory
            string CodeCaveAddrString = ((long)CodeCave).ToString("X"); // converts the pointer to hex string ready for easy conversion to bytes
            byte[] CodeCaveAddr = StringToBytes("0" + CodeCaveAddrString); // converts said string to bytes - "0" there to make the bit count even as ther is 15 bits to the address
            Array.Reverse(CodeCaveAddr); // makes it little-endian, what assembly uses

            byte[] TimeJumpBefore = new byte[12] { 0xF2, 0x0F, 0x11, 0x43, 0x08, 0x48, 0x83, 0xC4, 0x40, 0x5B, 0xC3, 0xCC }; // bytes at the time adding code
            string TimeJumpCodeString = "48B8" + BitConverter.ToString(CodeCaveAddr).Replace("-", String.Empty) + "0000" + "FFE0"; // basically code that puts the codecave address in rax and jumps to it
            byte[] TimeJumpCode = StringToBytes(TimeJumpCodeString); // converting the easily managable string to bytes

            string TimeAddrAddrString = ((long)CodeCave + 37).ToString("X"); // sets where i want the TimeAddr (rbx) to be "dumped" to, just an offset thats within the codecave
            byte[] TimeAddrAddr = StringToBytes("00000" + TimeAddrAddrString); // adding seamingly neccesary 0's to not confuse the assembly, without them, the opcodes would fuck up
            Array.Reverse(TimeAddrAddr); // again, endianess

            byte[] TimeNOPAddrBytes = longToByteArray(TimeNOPAddrLong + 12); // jump back address, where i jumped from + the amount of bits that were replaced
            Array.Reverse(TimeNOPAddrBytes); // again, endianess
            string InsideCaveCodeString = "48B8" + BitConverter.ToString(TimeAddrAddr).Replace("-", String.Empty) + "488918F20F1143084883C4405BC3CC48B8" + BitConverter.ToString(TimeNOPAddrBytes).Replace("-", String.Empty) + "FFE0"; // custom asm in code cave - breakpoint to find the code cave address and look inside in cheat engine
            byte[] InsideCaveCode = StringToBytes(InsideCaveCodeString); // converting the easily managable string to bytes again

            MainWindow.m.WriteBytes(CodeCaveAddrString, InsideCaveCode); // write the bytes inside codecave before jumping to it
            MainWindow.m.WriteBytes(TimeNOPAddr, TimeJumpCode); // write code to jump to codecave
            Thread.Sleep(25); Thread.Sleep(25);
            TimeAddr = (MainWindow.m.ReadLong(TimeAddrAddrString) + 8).ToString("X");// get address from dumped memory
            MainWindow.m.WriteBytes(TimeNOPAddr, TimeJumpBefore); // replace jump bytes, so if it day work, less chance of crash
            VirtualFreeEx(Process.GetProcessesByName("ForzaHorizon4")[0].Handle, CodeCave, 0, MEM_DECOMMIT); // free memory, think it might need to be MEM_RELEASE as atm, the allocated memory just stays
        }
        private void TimeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            float PastStart = MainWindow.m.ReadFloat(PastStartAddr);
            if (TimeAddr == null && PastStart == 1)
                GetTimeAddr();
            var NOP = new byte[5] { 0x90, 0x90, 0x90, 0x90, 0x90 };
            var NOPBefore = new byte[5] { 0xF2, 0x0F, 0x11, 0x43, 0x08 };
            if (TimeCheckBox.Checked == false)
            {
                MainWindow.m.WriteBytes(TimeNOPAddr, NOPBefore);
                TimeToggle = false;
                if (TimeToggle == false)
                {
                    TimeWorker.CancelAsync();
                }
            }
            else
            {
                MainWindow.m.WriteBytes(TimeNOPAddr, NOP);
                TimeToggle = true;
                Timestart = true;
                if (TimeWorker.IsBusy == false)
                {
                    TimeWorker.RunWorkerAsync();
                }
            }
        }
        private void TimeBack()
        {
            if(TimeBackStart)
            {
                Thread.Sleep(75);
                double TimeValDouble = MainWindow.m.ReadDouble(TimeAddr);
                string TimeVal = (TimeValDouble - 100).ToString();
                MainWindow.m.WriteMemory(TimeAddr, "double", TimeVal);
            }
        }
        private void TimeForward()
        {
            if (TimeForwardStart)
            {
                Thread.Sleep(75);
                double TimeValDouble = MainWindow.m.ReadDouble(TimeAddr);
                string TimeVal = (TimeValDouble + 100).ToString();
                MainWindow.m.WriteMemory(TimeAddr, "double", TimeVal);
            }
        }
        //end of time

        //teleport "script"
        public void CheckPointTPworker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (CheckPointTPToggle)
            {
                float InRace = MainWindow.m.ReadFloat(InRaceAddr);
                if (InRace == 1)
                {
                    Thread.Sleep(3750);
                    while (InRace == 1)
                    {
                        InRace = MainWindow.m.ReadFloat(InRaceAddr);
                        CheckPointTP();
                    }
                    MainWindow.m.UnfreezeValue(yAngVelAddr);
                }
                Thread.Sleep(1);
            }
        }
        public void CheckPointTP()
        {
            Thread.Sleep(25);
            MainWindow.m.WriteMemory(xAddr, "float", (MainWindow.m.ReadFloat(CheckPointxAddr)).ToString());
            MainWindow.m.WriteMemory(yAddr, "float", (MainWindow.m.ReadFloat(CheckPointyAddr) + 4).ToString());
            MainWindow.m.WriteMemory(zAddr, "float", (MainWindow.m.ReadFloat(CheckPointzAddr)).ToString());
            MainWindow.m.FreezeValue(yAngVelAddr, "float", "100");
        }
        //end of teleport "script"

        //noclip
        public void Noclip()
        {
            var Jmp1 = new byte[6] { 0xE9, 0x2A, 0x02, 0x00, 0x00, 0x90 };
            var Jmp2 = new byte[6] { 0xE9, 0x2B, 0x02, 0x00, 0x00, 0x90 };
            var Jmp1before = new byte[6] { 0x0F, 0x84, 0x29, 0x02, 0x00, 0x00 };
            var Jmp2before = new byte[6] { 0x0F, 0x84, 0x2A, 0x02, 0x00, 0x00 };
            float OnGround = MainWindow.m.ReadFloat(OnGroundAddr);
            if (OnGround == 0)
            {
                NoClipcycles++;
                if (NoClipcycles % 10 == 0)
                {
                    OnGround = MainWindow.m.ReadFloat(OnGroundAddr);
                    if (OnGround == 0)
                    {
                        MainWindow.m.WriteBytes(Wall1Addr, Jmp1before);
                        MainWindow.m.WriteBytes(Wall2Addr, Jmp2before);
                    }
                    NoClipcycles = 0;
                }
            }
            if (OnGround == 1)
            {
                NoClipcycles++;
                if (NoClipcycles % 10 == 0)
                {
                    MainWindow.m.WriteBytes(Wall1Addr, Jmp1);
                    MainWindow.m.WriteBytes(Wall2Addr, Jmp2);
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
                MainWindow.m.WriteBytes(Car1Addr, Jmp3before);
                MainWindow.m.WriteBytes(Car2Addr, Jmp4before);
            }
            else
            {
                CarNoClipToggle = true;
                NCstart = true;
                if (NoClipWorker.IsBusy == false)
                {
                    NoClipWorker.RunWorkerAsync();
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
                if (WallNoClipToggle == false)
                {
                    NoClipWorker.CancelAsync();
                }
                //NoClipworker.CancelAsync();
                MainWindow.m.WriteBytes(Wall1Addr, Jmp1before);
                MainWindow.m.WriteBytes(Wall2Addr, Jmp2before);
            }
            else
            {
                WallNoClipToggle = true;
                NCstart = true;
                //NoClipworker.RunWorkerAsync();
                if (NoClipWorker.IsBusy == false)
                {
                    NoClipWorker.RunWorkerAsync();
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
                if (BreakToggle == false)
                {
                    SuperBreakWorker.CancelAsync();
                }
                //Breakworker.CancelAsync();
            }
            else
            {
                BreakToggle = true;
                Breakstart = true;
                //if (SuperBreakWorker.IsBusy == false)
                //{
                    SuperBreakWorker.RunWorkerAsync();
                //}
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
                if (VelHackToggle == false)
                {
                    VelHackWorker.CancelAsync();
                }
            }
            else
            {
                VelHackToggle = true;
                Velstart = true;
                if (VelHackWorker.IsBusy == false)
                {
                    VelHackWorker.RunWorkerAsync();
                }
            }
        }
        private void WheelSpeedButton_CheckedChanged(object sender, EventArgs e)
        {
            if (WheelSpeedButton.Checked == false)
            {
                SpeedHackToggle = false;
                if (SpeedHackToggle == false)
                {
                    SpeedHackWorker.CancelAsync();
                }
            }
            else
            {
                SpeedHackToggle = true;
                Speedstart = true;
                if (SpeedHackWorker.IsBusy == false)
                {
                    SpeedHackWorker.RunWorkerAsync();
                }
            }
        }
        //end of speedhack stuff

        //turnassist button
        private void TurnAssistButton_CheckedChanged(object sender, EventArgs e)
        {
            if (TurnAssistButton.Checked == false)
            {
                TurnAssistToggle = false;
                if (TurnAssistToggle == false)
                {
                    TurnWorker.CancelAsync();
                }
            }
            else
            {
                TurnAssistToggle = true;
                Turnstart = true;
                if (TurnWorker.IsBusy == false)
                {
                    TurnWorker.RunWorkerAsync();
                }
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
            MainWindow.m.WriteMemory(xAddr, "float", x.ToString());
            MainWindow.m.WriteMemory(yAddr, "float", y.ToString());
            MainWindow.m.WriteMemory(zAddr, "float", z.ToString());
        }
        private void CheckpointBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckpointBox.Checked == false)
            {
                CheckPointTPToggle = false;
                CheckPointTPworker.CancelAsync();
                MainWindow.m.UnfreezeValue(yAngVelAddr);
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
            VelMultBar.Value = Decimal.ToInt32(VelMultBox.Value * 100);
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
            ScanStartAddr = (long)MainWindow.m.GetCode(FOVHighAddr) - 2100000000;
            ScanEndAddr = (long)MainWindow.m.GetCode(FOVHighAddr) + 2100000000;
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
                        FirstPersonAddrLong = (await MainWindow.m.AoBScan(ScanStartAddr, ScanEndAddr, FirstPerson, true, true)).FirstOrDefault() - 75;
                    }
                    FirstPersonAddr = FirstPersonAddrLong.ToString("X");
                }
                else if (DashAddr == "FFFFFFFFFFFFFF45" || DashAddr == null || DashAddr == "0")
                {
                    FOVScan_bar.Value = 20;
                    if (cycles < 2)
                    {
                        cycles++;
                        DashAddrLong = (await MainWindow.m.AoBScan(ScanStartAddr, ScanEndAddr, Dash, true, true)).FirstOrDefault() - 187;
                    }
                    DashAddr = DashAddrLong.ToString("X");
                }
                else if (FrontAddr == "FFFFFFFFFFFFFF42" || FrontAddr == null || FrontAddr == "0")
                {
                    FOVScan_bar.Value = 40;
                    if (cycles < 3)
                    {
                        cycles++;
                        FrontAddrLong = (await MainWindow.m.AoBScan(ScanStartAddr, ScanEndAddr, Front, true, true)).FirstOrDefault() - 190;
                    }
                    FrontAddr = FrontAddrLong.ToString("X");
                }
                else if (LowAddr == "FFFFFFFFFFFFFF49" || LowAddr == null || LowAddr == "0")
                {
                    FOVScan_bar.Value = 60;
                    if (cycles < 4)
                    {
                        cycles++;
                        LowAddrLong = (await MainWindow.m.AoBScan(ScanStartAddr, ScanEndAddr, Low, true, true)).FirstOrDefault() - 183;

                    }
                    LowCompare = LowAddrLong.ToString();
                    if (LowCompare == MainWindow.m.GetCode(FOVHighAddr).ToString())
                    {
                        LowAddrLong = (await MainWindow.m.AoBScan(ScanStartAddr, ScanEndAddr, Low, true, true)).LastOrDefault() - 183;
                    }
                    LowAddr = LowAddrLong.ToString("X");
                }
                else if (BonnetAddr == "FFFFFFFFFFFFFF43" || BonnetAddr == null || DashAddr == "0")
                {
                    FOVScan_bar.Value = 80;
                    if (cycles < 5)
                    {
                        cycles++;
                        BonnetAddrLong = (await MainWindow.m.AoBScan(ScanStartAddr, ScanEndAddr, Bonnet, true, true)).FirstOrDefault() - 189;
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
                MainWindow.m.WriteBytes(FOVnopOutAddr, nopoutbefore);
                MainWindow.m.WriteBytes(FOVnopInAddr, nopinbefore);
                MainWindow.m.UnfreezeValue(FOVHighAddr);
                MainWindow.m.UnfreezeValue(FirstPersonAddr);
                MainWindow.m.UnfreezeValue(DashAddr);
                MainWindow.m.UnfreezeValue(LowAddr);
                MainWindow.m.UnfreezeValue(BonnetAddr);
                MainWindow.m.UnfreezeValue(FrontAddr);
            }
            else
            {
                MainWindow.m.WriteBytes(FOVnopOutAddr, nop);
                MainWindow.m.WriteBytes(FOVnopInAddr, nop);
                MainWindow.m.FreezeValue(FOVHighAddr, "float", FOVVal.ToString());
                MainWindow.m.FreezeValue(FirstPersonAddr, "float", FOVVal.ToString());
                MainWindow.m.FreezeValue(DashAddr, "float", FOVVal.ToString());
                MainWindow.m.FreezeValue(LowAddr, "float", FOVVal.ToString());
                MainWindow.m.FreezeValue(BonnetAddr, "float", FOVVal.ToString());
                MainWindow.m.FreezeValue(FrontAddr, "float", FOVVal.ToString());
            }
        }
        private void FOVIncrease()
        {
            IncreaseCycles++;
            if (FOVBar.Value > 149)
                FOVBar.Value = 149;
            FOVBar.Value = FOVBar.Value + 1;
            FOVVal = (float)FOVBar.Value / 100;
            MainWindow.m.FreezeValue(FOVHighAddr, "float", FOVVal.ToString());
            MainWindow.m.FreezeValue(FirstPersonAddr, "float", FOVVal.ToString());
            MainWindow.m.FreezeValue(DashAddr, "float", FOVVal.ToString());
            MainWindow.m.FreezeValue(LowAddr, "float", FOVVal.ToString());
            MainWindow.m.FreezeValue(BonnetAddr, "float", FOVVal.ToString());
            MainWindow.m.FreezeValue(FrontAddr, "float", FOVVal.ToString());
        }
        private void FOVdecrease()
        {
            DecreaseCycles++;
            if (FOVBar.Value < -94)
                FOVBar.Value = -94;
            FOVBar.Value = FOVBar.Value - 1;
            FOVVal = (float)FOVBar.Value / 100;
            MainWindow.m.FreezeValue(FOVHighAddr, "float", FOVVal.ToString());
            MainWindow.m.FreezeValue(FirstPersonAddr, "float", FOVVal.ToString());
            MainWindow.m.FreezeValue(DashAddr, "float", FOVVal.ToString());
            MainWindow.m.FreezeValue(LowAddr, "float", FOVVal.ToString());
            MainWindow.m.FreezeValue(BonnetAddr, "float", FOVVal.ToString());
            MainWindow.m.FreezeValue(FrontAddr, "float", FOVVal.ToString());
        }
        public void WeirdPullVal()
        {
            WeirdVal = MainWindow.m.ReadFloat(WeirdAddr, round: false);
            WeirdBox.Value = (decimal)WeirdVal;
            NewWeirdVal = WeirdVal;
        }
        public void GravityPullVal()
        {
            GravityVal = MainWindow.m.ReadFloat(GravityAddr);
            GravityBox.Value = (decimal)GravityVal;
            NewGravityVal = GravityVal;
        }
        private void WeirdPull_Click(object sender, EventArgs e)
        {
            WeirdPullVal();
        }

        private void WeirdSet_Click(object sender, EventArgs e)
        {
            MainWindow.m.WriteMemory(WeirdAddr, "float", NewWeirdVal.ToString());
        }

        private void GravityPull_Click(object sender, EventArgs e)
        {
            GravityPullVal();
        }

        private void GravitySet_Click(object sender, EventArgs e)
        {
            MainWindow.m.WriteMemory(GravityAddr, "float", NewGravityVal.ToString());
        }

        private void WeirdBox_ValueChanged(object sender, EventArgs e)
        {
            NewWeirdVal = (float)WeirdBox.Value;
        }

        private void GravityBox_ValueChanged(object sender, EventArgs e)
        {
            NewGravityVal = (float)GravityBox.Value;
        }
    }
}
