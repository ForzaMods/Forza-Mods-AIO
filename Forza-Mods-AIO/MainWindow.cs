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

namespace Forza_Mods_AIO
{
    public partial class MainWindow : Form
    {
        Mem m = new Mem();
        KeyboardHook keyboardHook = new KeyboardHook();
        bool IsAttached = false;
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
        bool NoClipToggle = false;
        bool CheckPointTPToggle = false;
        bool done = false;
        bool start = false;
        long BaseAddrLong;      long Base2AddrLong; long Base3AddrLong; long Car1AddrLong; long Car2AddrLong; long Wall1AddrLong; long Wall2AddrLong;
        string Base = "43 3a 5c 57 ? 4e 44 4f 57 53 5c 53 59 53 54 45 4d 33 32 5c 44";
        string Car1 = "48 89 ? ? ? 44 8B ? 48 89 ? ? ? BA";
        string Car2 = "0F 28 ? 41 0F ? ? ? 0F C6 D6 ? 41 0F";
        string Wall1 = "F3 0F ? ? ? 0F 59 ? 0F C6 ED ? 0F C6 F6";
        string Wall2 = "0F 28 ? 0F C6 C1 ? 0F 28 ? 0F C6 CB ? 41 0F ? ? F3 0F ? ? 41 0F ? ? 0F C6 C0 ? 0F C6 E4";
        string BaseAddr;        string Base2Addr;       string Base3Addr;
        string Car1Addr;        string Car2Addr;
        string Wall1Addr;       string Wall2Addr;
        string FrontLeftAddr;   string FrontRightAddr;  string BackLeftAddr;    string BackRightAddr;
        string OnGroundAddr;    string InRaceAddr;
        string xVelocityAddr;   string yVelocityAddr;   string zVelocityAddr;
        string xAddr;           string yAddr;           string zAddr;
        string CheckPointxAddr; string CheckPointyAddr; string CheckPointzAddr;
        string YawAddr;         string RollAddr;        string PitchAddr;      string yAngVelAddr;
        string GasAddr;
        float xVelocityVal;     float yVelocityVal;     float zVelocityVal;
        float x;                float y;                float z;
        float CheckPointx;      float CheckPointy;      float CheckPointz;
        float BoostSpeed1;      float BoostSpeed2;      float BoostSpeed3;      float BoostLim; //speed
        float TurnRatio;        float TurnStrength;
        float VelMult;
        int times1;             int times2;             int times3;             int times4; //boost
        int BoostInterval1;     int BoostInterval2;     int BoostInterval3;     int BoostInterval4; /*interval*/ int TurnInterval;
        int Velcycles; int NoClipcycles;


        public MainWindow()
        {
            InitializeComponent();
            keyboardHook.Install();
            keyboardHook.KeyDown += new KeyboardHook.KeyboardHookCallback(keyboardHook_KeyDown);
            keyboardHook.KeyUp += new KeyboardHook.KeyboardHookCallback(keyboardHook_KeyUp);
            CheckForIllegalCrossThreadCalls = false;
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {

        }
        //dragging functionality
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private void TopPanel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = System.Windows.Forms.Cursor.Position;
            dragFormPoint = this.Location;
        }
        private void TopPanel_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(System.Windows.Forms.Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }
        private void TopPanel_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            dragging = false;
        }
        //end of dragging functionality
        //keyboard hooks
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
                if(VelHackToggle)
                {
                    VelHackStart = true;
                }
                if(SpeedHackToggle)
                {
                    SpeedHackStart = true;
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
        }
        //end of hooks
        //setup
        public void AoBscan()
        {
            var TargetProcess = Process.GetProcessesByName("ForzaHorizon4")[0];
            SigScanSharp Sigscan = new SigScanSharp(TargetProcess.Handle);
            Sigscan.SelectModule(TargetProcess.MainModule);
            if (done == false)
            {
                long lTime;
                if (BaseAddr == "1DD0" || BaseAddr == null || BaseAddr == "0")
                {
                    BaseAddrLong = (long)Sigscan.FindPattern(Base, out lTime) + 7632;
                    BaseAddr = BaseAddrLong.ToString("X");
                    AOBScanProgress.Value = 14;
                }
                else if (Base2Addr == "2F70" || Base2Addr == null || Base2Addr == "0")
                {
                    Base2AddrLong = (long)Sigscan.FindPattern(Base, out lTime) + 12144;
                    Base2Addr = Base2AddrLong.ToString("X");
                    AOBScanProgress.Value = 28;
                }
                else if (Base3Addr == "-D00" || Base3Addr == null || Base3Addr == "0")
                {
                    Base3AddrLong = (long)Sigscan.FindPattern(Base, out lTime) - 3328;
                    Base3Addr = Base3AddrLong.ToString("X");
                    AOBScanProgress.Value = 42;
                }
                else if (Car1Addr == "6A" || Car1Addr == null || Car1Addr == "0")
                {
                    Car1AddrLong = (long)Sigscan.FindPattern(Car1, out lTime) + 106;
                    Car1Addr = Car1AddrLong.ToString("X");
                    AOBScanProgress.Value = 58;
                }
                else if (Car2Addr == "-19B" || Car2Addr == null || Car2Addr == "0")
                {
                    Car2AddrLong = (long)Sigscan.FindPattern(Car2, out lTime) - 411;
                    Car2Addr = Car2AddrLong.ToString("X");
                    AOBScanProgress.Value = 72;
                }
                else if (Wall1Addr == "191" || Wall1Addr == null || Wall1Addr == "0")
                {
                    Wall1AddrLong = (long)Sigscan.FindPattern(Wall1, out lTime) + 401;
                    Wall1Addr = Wall1AddrLong.ToString("X");
                    AOBScanProgress.Value = 86;
                }
                else if (Wall2Addr == "-1BE" || Wall2Addr == null || Wall2Addr == "0")
                {
                    Wall2AddrLong = (long)Sigscan.FindPattern(Wall2, out lTime) - 446;
                    Wall2Addr = Wall2AddrLong.ToString("X");
                    AOBScanProgress.Value = 100;
                }
                if (BaseAddr == "1DD0" || BaseAddr == null || BaseAddr == "0"
                    || Base2Addr == "2F70" || Base2Addr == null || Base2Addr == "0"
                    || Base3Addr == "-D00" || Base3Addr == null || Base3Addr == "0"
                    || Car1Addr == "6A" || Car1Addr == null || Car1Addr == "0"
                    || Car2Addr == "-19B" || Car2Addr == null || Car2Addr == "0"
                    || Wall1Addr == "191" || Wall1Addr == null || Wall1Addr == "0"
                    || Wall2Addr == "-1BE" || Wall2Addr == null || Wall2Addr == "0")
                {
                    ;
                }
                else
                {
                    Addresses();
                    done = true;
                    ReadSpeedDefaultValues();
                }
            }
        }
        public void Addresses()
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
        }
        //end of setup
        //break hack methods + BGworker
        /*
        public void Breakworker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (BreakToggle)
            {
                SuperBreak();
            }
        }
        */
        public void SuperBreak()
        {
            if (BreakStart)
            {
                //xVelocityVal = Math.Abs(m.ReadFloat(xVelocityAddr));
                //zVelocityVal = Math.Abs(m.ReadFloat(zVelocityAddr));
                //if (xVelocityVal < 1 || xVelocityVal < 1)
                //{
                //    xVelocityVal = 0;
                //    zVelocityVal = 0;
                //}
                //else
                //{
                    xVelocityVal = m.ReadFloat(xVelocityAddr) * (float)0.75;
                    zVelocityVal = m.ReadFloat(zVelocityAddr) * (float)0.75;
                //}

                m.WriteMemory(xVelocityAddr, "float", xVelocityVal.ToString());
                //m.WriteMemory(yVelocityAddr, "float", "0");
                m.WriteMemory(zVelocityAddr, "float", zVelocityVal.ToString());
                m.WriteMemory(YawAddr, "float", "0");
                Thread.Sleep(50);
            }
        }
        public void StopAllWheels()
        {
            m.WriteMemory(FrontLeftAddr, "float", "0");
            m.WriteMemory(FrontRightAddr, "float", "0");
            m.WriteMemory(BackLeftAddr, "float", "0");
            m.WriteMemory(BackRightAddr, "float", "0");
        }
        //end of break hacks
        //speed hack methods + BGworkers
        /*
        public void VelHackworker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (VelHackToggle)
            {
                SpeedHackVel();
            }
        }
        */
        public void SpeedHackVel()
        {
            if (VelHackStart)
            {
                Velcycles++;
                xVelocityVal = m.ReadFloat(xVelocityAddr) * (float)VelMult;
                zVelocityVal = m.ReadFloat(zVelocityAddr) * (float)VelMult;
                y = m.ReadFloat(yAddr);
                if (Velcycles % 2 == 0)
                {
                    y = m.ReadFloat(yAddr) - (float)0.01;
                    Velcycles = 0;
                }

                m.WriteMemory(xVelocityAddr, "float", xVelocityVal.ToString());
                m.WriteMemory(zVelocityAddr, "float", zVelocityVal.ToString());
                m.WriteMemory(yAddr, "float", y.ToString());
                Thread.Sleep(50);
            }
        }
        /*
        public void SpeedHackworker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (SpeedHackToggle)
            {
                SpeedHack();
            }
        }
        */
        public void SpeedHack()
        {
            if (SpeedHackStart)
            {
                m.WriteMemory(GasAddr, "float", "1");
                float boost = (float)Math.Ceiling(m.ReadFloat(FrontLeftAddr));
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
                m.WriteMemory(FrontLeftAddr, "float", boost.ToString());
                m.WriteMemory(FrontRightAddr, "float", boost.ToString());
                m.WriteMemory(BackLeftAddr, "float", boost.ToString());
                m.WriteMemory(BackRightAddr, "float", boost.ToString());
            }
        }
        //end of speed hacks
        public void Mainworker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (start)
            {
                if(NoClipToggle == true)
                {
                    Noclip();
                }
                if (VelHackToggle == true)
                {
                    SpeedHackVel();
                }
                if (SpeedHackToggle == true)
                {
                    SpeedHack();
                }
                if (TurnAssistToggle == true)
                {
                    TurnAssistLeft();
                    TurnAssistRight();
                }
                if (BreakToggle == true)
                {
                    SuperBreak();
                }
                if(Mainworker.CancellationPending == true)
                {
                    e.Cancel = true;
                    start = false;
                }
                Thread.Sleep(1);
            }
        }
        //Turn assist methods + workers
        /*
        public void TurnAssistworker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (TurnAssistToggle)
            {
                TurnAssistLeft();
                TurnAssistRight();
            }
        }
        */
        public void TurnAssistLeft()
        {
            if(TurnAssistLeftStart)
            {
                float FrontLeft = m.ReadFloat(FrontLeftAddr);
                float FrontRight = m.ReadFloat(FrontRightAddr);
                float BackLeft = m.ReadFloat(BackLeftAddr);
                float BackRight = m.ReadFloat(BackRightAddr);
                if ((float)Math.Abs(FrontRight - FrontLeft) < (FrontRight / TurnRatio) && (float)Math.Abs(BackRight - FrontLeft) < (BackRight / TurnRatio))
                {
                    FrontLeft = FrontLeft - TurnStrength;
                    BackLeft = BackLeft - TurnStrength;
                    FrontRight = FrontRight + TurnStrength;
                    BackRight = BackRight + TurnStrength;
                    Thread.Sleep(TurnInterval);
                }
                m.WriteMemory(FrontLeftAddr, "float", FrontLeft.ToString());
                m.WriteMemory(FrontRightAddr, "float", FrontRight.ToString());
                m.WriteMemory(BackLeftAddr, "float", BackLeft.ToString());
                m.WriteMemory(BackRightAddr, "float", BackRight.ToString());
            }
        }
        public void TurnAssistRight()
        {
            if(TurnAssistRightStart)
            {
                float FrontLeft = m.ReadFloat(FrontLeftAddr);
                float FrontRight = m.ReadFloat(FrontRightAddr);
                float BackLeft = m.ReadFloat(BackLeftAddr);
                float BackRight = m.ReadFloat(BackRightAddr);
                if ((float)Math.Abs(FrontLeft - FrontRight) < (FrontLeft / TurnRatio) && (float)Math.Abs(BackLeft - FrontRight) < (BackLeft / TurnRatio))
                {
                    FrontRight = FrontRight - TurnStrength;
                    BackRight = BackRight - TurnStrength;
                    FrontLeft = FrontLeft + TurnStrength;
                    BackLeft = BackLeft + TurnStrength;
                    Thread.Sleep(TurnInterval);
                }
                m.WriteMemory(FrontLeftAddr, "float", FrontLeft.ToString());
                m.WriteMemory(FrontRightAddr, "float", FrontRight.ToString());
                m.WriteMemory(BackLeftAddr, "float", BackLeft.ToString());
                m.WriteMemory(BackRightAddr, "float", BackRight.ToString());
            }
        }
        //end of turn assists
        //teleport "script"
        public void CheckPointTPworker_DoWork(object sender, DoWorkEventArgs e)
        {   
            while (CheckPointTPToggle)
            {
                float InRace = m.ReadFloat(InRaceAddr);
                if (InRace == 1)
                {
                    Thread.Sleep(3750);
                    while (InRace == 1)
                    {
                        InRace = m.ReadFloat(InRaceAddr);
                        CheckPointTP();
                    }
                    m.UnfreezeValue(yAngVelAddr);
                }
                Thread.Sleep(1);
            }
        }
        public void CheckPointTP()
        {
            Thread.Sleep(25);
            //CheckPointx = m.ReadFloat(CheckPointxAddr);     CheckPointy = m.ReadFloat(CheckPointyAddr);     CheckPointz = m.ReadFloat(CheckPointzAddr);
            m.WriteMemory(xAddr, "float", (m.ReadFloat(CheckPointxAddr)).ToString());
            m.WriteMemory(yAddr, "float", (m.ReadFloat(CheckPointyAddr)+ 4).ToString());
            m.WriteMemory(zAddr, "float", (m.ReadFloat(CheckPointzAddr)).ToString());
            m.FreezeValue(yAngVelAddr, "float", "100");
        }
        //end of teleport "script"
        //NoClip handler
        /*
        public void NoClipworker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (NoClipToggle)
            {
                Noclip();
            }
        }
        */
        public void Noclip()
        {
            var Jmp1 = new byte[6] { 0xE9, 0x2A, 0x02, 0x00, 0x00, 0x90 };
            var Jmp2 = new byte[6] { 0xE9, 0x2B, 0x02, 0x00, 0x00, 0x90 };
            var Jmp1before = new byte[6] { 0x0F, 0x84, 0x29, 0x02, 0x00, 0x00 };
            var Jmp2before = new byte[6] { 0x0F, 0x84, 0x2A, 0x02, 0x00, 0x00 };
            float OnGround = m.ReadFloat(OnGroundAddr);
            if (OnGround == 0)
            {
                NoClipcycles++;
                if (NoClipcycles % 10 == 0)
                {
                    OnGround = m.ReadFloat(OnGroundAddr);
                    if (OnGround == 0)
                    {
                        m.WriteBytes(Wall1Addr, Jmp1before);
                        m.WriteBytes(Wall2Addr, Jmp2before);
                    }
                    NoClipcycles = 0;
                }
            }
            if (OnGround == 1)
            {
                NoClipcycles++;
                if (NoClipcycles % 10 == 0)
                {
                    m.WriteBytes(Wall1Addr, Jmp1);
                    m.WriteBytes(Wall2Addr, Jmp2);
                    NoClipcycles = 0;
                }
            }
        }
        //end of noclip
        //used to clear all the colours before setting accent and highlight for the tab
        private void ClearColours()
        {
            BTN_TabInfo.BackColor = Color.FromArgb(28, 28, 28);
            Panel_Info.BackColor = Color.FromArgb(28, 28, 28);
            BTN_TabAddCars.BackColor = Color.FromArgb(28, 28, 28);
            Panel_AddCars.BackColor = Color.FromArgb(28, 28, 28);
            BTN_TabStatsEditor.BackColor = Color.FromArgb(28, 28, 28);
            Panel_StatsEditor.BackColor = Color.FromArgb(28, 28, 28);
            BTN_TabSaveswap.BackColor = Color.FromArgb(28, 28, 28);
            Panel_Saveswap.BackColor = Color.FromArgb(28, 28, 28);
            BTN_TabLiveTuning.BackColor = Color.FromArgb(28, 28, 28);
            Panel_LiveTuning.BackColor = Color.FromArgb(28, 28, 28);
            BTN_TabSpeedhack.BackColor = Color.FromArgb(28, 28, 28);
            Panel_Speedhack.BackColor = Color.FromArgb(28, 28, 28);
        }
        private void ClearTabItems()
        {
            Tab_1Info.Hide();
            Tab_2AddCars.Hide();
            Tab_3StatsEditor.Hide();
            Tab_4Saveswap.Hide();
            Tab_5LiveTuning.Hide();
            Tab_6Speedhack.Hide();
        }
        private void DisableButtons()
        {
            BTN_TabAddCars.Enabled = false;
            BTN_TabStatsEditor.Enabled = false;
            BTN_TabLiveTuning.Enabled = false;
            BTN_TabSpeedhack.Enabled = false;

        }
        private void EnableButtons()
        {
            BTN_TabAddCars.Enabled = true;
            BTN_TabStatsEditor.Enabled = true;
            BTN_TabLiveTuning.Enabled = true;
            BTN_TabSpeedhack.Enabled = true;
        }
        private void BTN_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BTN_TabInfo_Click(object sender, EventArgs e)
        {
            ClearColours();
            BTN_TabInfo.BackColor = Color.FromArgb(45, 45, 48);
            Panel_Info.BackColor = Color.FromArgb(150, 11, 166);
            ClearTabItems();
            Tab_1Info.Show();
        }
        private void BTN_TabAddCars_Click(object sender, EventArgs e)
        {
            if (IsAttached)
            {
                //do colours and hide/show ui
                ClearColours();
                BTN_TabAddCars.BackColor = Color.FromArgb(45, 45, 48);
                Panel_AddCars.BackColor = Color.FromArgb(150, 11, 166);
                ClearTabItems();
                Tab_2AddCars.Show();
            }
            else
            {
                ;
            }
        }
        private void BTN_TabStatsEditor_Click(object sender, EventArgs e)
        {
            if (IsAttached)
            {
                ClearColours();
                BTN_TabStatsEditor.BackColor = Color.FromArgb(45, 45, 48);
                Panel_StatsEditor.BackColor = Color.FromArgb(150, 11, 166);
                ClearTabItems();
                Tab_3StatsEditor.Show();
            }
            else
            {
                ;
            }
        }
        private void BTN_TabSaveswap_Click(object sender, EventArgs e)
        {
                ClearColours();
                BTN_TabSaveswap.BackColor = Color.FromArgb(45, 45, 48);
                Panel_Saveswap.BackColor = Color.FromArgb(150, 11, 166);
                ClearTabItems();
                Tab_4Saveswap.Show();

        }
        private void BTN_TabLiveTuning_Click(object sender, EventArgs e)
        {
            if (IsAttached)
            {
                ClearColours();
                BTN_TabLiveTuning.BackColor = Color.FromArgb(45, 45, 48);
                Panel_LiveTuning.BackColor = Color.FromArgb(150, 11, 166);
                ClearTabItems();
                Tab_5LiveTuning.Show();
            }
            else
            {
                ;
            }
        }
        private void BTN_TabSpeedhack_Click(object sender, EventArgs e)
        {
            if (IsAttached)
            {
                ClearColours();
                BTN_TabSpeedhack.BackColor = Color.FromArgb(45, 45, 48);
                Panel_Speedhack.BackColor = Color.FromArgb(150, 11, 166);
                ClearTabItems();
                Tab_6Speedhack.Show();
                //SetSpeedhackVal();
                SHReset();
            }
            else
            {
                ;
            }
        }
        private void BTN_TabInfo_MouseEnter(object sender, EventArgs e)
        {
            if (Tab_1Info.Visible==false)
                Panel_Info.BackColor = Color.FromArgb(93, 93, 100);
        }
        private void BTN_TabInfo_MouseLeave(object sender, EventArgs e)
        {
            if (Tab_1Info.Visible == false)
            Panel_Info.BackColor = Color.FromArgb(28, 28, 28);
        }
        private void BTN_TabAddCars_MouseEnter(object sender, EventArgs e)
        {
            if (Tab_2AddCars.Visible == false)
                Panel_AddCars.BackColor = Color.FromArgb(93, 93, 100);
        }
        private void BTN_TabAddCars_MouseLeave(object sender, EventArgs e)
        {
            if (Tab_2AddCars.Visible == false)
                Panel_AddCars.BackColor = Color.FromArgb(28, 28, 28);
        }
        private void BTN_TabStatsEditor_MouseEnter(object sender, EventArgs e)
        {
            if (Tab_3StatsEditor.Visible == false)
                Panel_StatsEditor.BackColor = Color.FromArgb(93, 93, 100);
        }
        private void BTN_TabStatsEditor_MouseLeave(object sender, EventArgs e)
        {
            if (Tab_3StatsEditor.Visible == false)
                Panel_StatsEditor.BackColor = Color.FromArgb(28, 28, 28);
        }
        private void BTN_TabSaveswap_MouseEnter(object sender, EventArgs e)
        {
            if (Tab_4Saveswap.Visible == false)
                Panel_Saveswap.BackColor = Color.FromArgb(93, 93, 100);
        }
        private void BTN_TabSaveswap_MouseLeave(object sender, EventArgs e)
        {
            if (Tab_4Saveswap.Visible == false)
                Panel_Saveswap.BackColor = Color.FromArgb(28, 28, 28);
        }
        private void BTN_TabLiveTuning_MouseEnter(object sender, EventArgs e)
        {
            if (Tab_5LiveTuning.Visible == false)
                Panel_LiveTuning.BackColor = Color.FromArgb(93, 93, 100);
        }
        private void BTN_TabLiveTuning_MouseLeave(object sender, EventArgs e)
        {
            if (Tab_5LiveTuning.Visible == false)
                Panel_LiveTuning.BackColor = Color.FromArgb(28, 28, 28);
        }
        private void BTN_TabSpeedhack_MouseEnter(object sender, EventArgs e)
        {
            if (Tab_6Speedhack.Visible == false)
                Panel_Speedhack.BackColor = Color.FromArgb(93, 93, 100);
        }
        private void BTN_TabSpeedhack_MouseLeave(object sender, EventArgs e)
        {
            if (Tab_6Speedhack.Visible == false)
                Panel_Speedhack.BackColor = Color.FromArgb(28, 28, 28);
        }
        private void InitialBGworker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if(!m.OpenProcess("ForzaHorizon4"))
                {
                    IsAttached = false;
                    InitialBGworker.ReportProgress(0);
                    Thread.Sleep(1000);
                    continue;
                }
                if (done == false)
                {
                    DisableButtons();
                }
                AoBscan();
                if (done == false)
                {
                    DisableButtons();
                }
                else
                {
                    IsAttached = true;
                    Thread.Sleep(1000);
                    InitialBGworker.ReportProgress(0);
                }
            }
        }
        private void MainWindow_Shown(object sender, EventArgs e)
        {
            InitialBGworker.RunWorkerAsync();
            CheckAttachedworker.RunWorkerAsync();
        }
        private void CheckAttachedworker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                Thread.Sleep(1);
                if (IsAttached == false && Tab_1Info.Visible == false && Tab_4Saveswap.Visible == false)
                {
                   done = false;
                   ClearColours();
                   BTN_TabInfo.BackColor = Color.FromArgb(45, 45, 48);
                   Panel_Info.BackColor = Color.FromArgb(150, 11, 166);
                   ClearTabItems();
                   Tab_1Info.Show();
                }
            }
        }
        private void InitialBGworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (IsAttached)
            {
                //AoBscan();
                if (done == true)
                {
                    LBL_Attached.Text = "Attached to FH4";
                    LBL_Attached.ForeColor = Color.Green;
                    EnableButtons();
                    AOBScanProgress.Hide();
                    
                }
            }
            else
            {
                LBL_Attached.Text = "Not Attached to FH4";
                LBL_Attached.ForeColor = Color.Red;
                DisableButtons();
                AOBScanProgress.Value = 0; AOBScanProgress.Show(); 
            }
        }
        private void InitialBGworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            InitialBGworker.RunWorkerAsync();
        }
        private void TB_ACAutoshow_CheckStateChanged(object sender, EventArgs e)
        {
            if (TB_ACAutoshow.Checked == false)
            {
                m.WriteMemory("base+4C4B7EC","string","0");
            }
            else
            {
                m.WriteMemory("base+4C4B7EC", "string", "1");
            }
        }
        private void Tab_6Speedhack_Paint(object sender, PaintEventArgs e)
        {

        }
        private void label12_Click(object sender, EventArgs e)
        {

        }
        //noclip buttons
        private void TB_SHCarNoClip_CheckedChanged(object sender, EventArgs e)
        {
            var Jmp3before = new byte[6] { 0x0F, 0x84, 0xB5, 0x01, 0x00, 0x00 };
            var Jmp4before = new byte[6] { 0x0F, 0x84, 0x3A, 0x03, 0x00, 0x00 };
            var Jmp3 = new byte[6] { 0xE9, 0xB6, 0x01, 0x00, 0x00, 0x90 };
            var Jmp4 = new byte[6] { 0xE9, 0x3B, 0x03, 0x00, 0x00, 0x90 };

            if (TB_SHCarNoClip.Checked == false)
            {
                m.WriteBytes(Car1Addr, Jmp3before);
                m.WriteBytes(Car2Addr, Jmp4before);
            }
            else
            {
                m.WriteBytes(Car1Addr, Jmp3);
                m.WriteBytes(Car2Addr, Jmp4);
            }
        }
        private void TB_SHWallNoClip_CheckedChanged(object sender, EventArgs e)
        {
            var Jmp1before = new byte[6] { 0x0F, 0x84, 0x29, 0x02, 0x00, 0x00 };
            var Jmp2before = new byte[6] { 0x0F, 0x84, 0x2A, 0x02, 0x00, 0x00 };
            if (TB_SHWallNoClip.Checked == false)
            {
                NoClipToggle = false;
                if (TurnAssistToggle == false
                && SpeedHackToggle == false
                && VelHackToggle == false
                && BreakToggle == false
                && NoClipToggle == false)
                {
                    Mainworker.CancelAsync();
                }
                //NoClipworker.CancelAsync();
                m.WriteBytes(Wall1Addr, Jmp1before);
                m.WriteBytes(Wall2Addr, Jmp2before);
            }
            else
            {
                NoClipToggle = true;
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
                && NoClipToggle == false)
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
                && NoClipToggle == false)
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
                && NoClipToggle == false)
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
                if(TurnAssistToggle == false
                && SpeedHackToggle == false
                && VelHackToggle == false
                && BreakToggle == false
                && NoClipToggle == false)
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
            if(LST_TeleportLocation.Text == "Festival")
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
            m.WriteMemory(xAddr, "float", x.ToString());
            m.WriteMemory(yAddr, "float", y.ToString());
            m.WriteMemory(zAddr, "float", z.ToString());
        }
        private void BTN_MIN_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void CheckpointBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckpointBox.Checked == false)
            {
                CheckPointTPToggle = false;
                CheckPointTPworker.CancelAsync();
                m.UnfreezeValue(yAngVelAddr);
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

        private void SHReset()
        {
            TurnIntervalBox.Value = TurnInterval;
            RatioBox.Value = Convert.ToDecimal(TurnRatio);
            TurnStrengthBox.Value = Convert.ToDecimal(TurnStrength);
            Speed1Box.Value = Convert.ToDecimal(BoostSpeed1);
            Speed2Box.Value = Convert.ToDecimal(BoostSpeed1);
            Speed3Box.Value = Convert.ToDecimal(BoostSpeed1);
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
                string TurnToggleStr = SpeedHack["Turn assist"]["On"]; WheelSpeedButton.Checked = bool.Parse(TurnToggleStr);
                string TurnStrengthStr = SpeedHack["Turn assist"]["Strength"]; BoostSpeed1 = float.Parse(TurnStrengthStr);
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
            SpeedHack["Turn assist"]["On"] = WheelSpeedButton.Checked.ToString();
            SpeedHack["Turn assist"]["Strength"] = BoostSpeed1.ToString();
            SpeedHack["Turn assist"]["Ratio"] = TurnRatio.ToString();
            SpeedHack["Turn assist"]["Interval"] = TurnInterval.ToString();
            SpeedHackparser.SaveFile("SpeedHackDefault.ini", SpeedHack);
        }
        public void CreateSHini()
        {
            var SpeedHackparser = new FileIniDataParser();
            IniData SpeedHack = new IniData();
            SpeedHack["No-Clip"]["Car"] = "false";
            SpeedHack["No-Clip"]["Wall"] = "false";
            SpeedHack["Velocity"]["On"] = "false";
            SpeedHack["Velocity"]["Multiplication"] = "0";
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
    }
}
