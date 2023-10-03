using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;
using Forza_Mods_AIO.Tabs.Tuning;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle
{
    class assembly
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
                    bytesArr[x] = (byte)((Hexmap[left] << 4) | Hexmap[right]);
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
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        public const int PROCESS_CREATE_THREAD = 0x0002;
        public const int PROCESS_QUERY_INFORMATION = 0x0400;
        public const int PROCESS_VM_OPERATION = 0x0008;
        public const int PROCESS_VM_WRITE = 0x0020;
        public const int PROCESS_VM_READ = 0x0010;

        public const uint MEM_COMMIT = 0x00001000;
        public const uint MEM_RESERVE = 0x00002000;
        public const uint PAGE_READWRITE = 0x4;
        public const uint PAGE_EXECUTE_READWRITE = 0x40;

        [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint dwFreeType);

        public const uint MEM_DECOMMIT = 0x00004000;
        public const uint MEM_RELEASE = 0x00008000;

        private static readonly Dictionary<char, byte> Hexmap = new Dictionary<char, byte>()
        {
            { 'a', 0xA },{ 'b', 0xB },{ 'c', 0xC },{ 'd', 0xD },
            { 'e', 0xE },{ 'f', 0xF },{ 'A', 0xA },{ 'B', 0xB },
            { 'C', 0xC },{ 'D', 0xD },{ 'E', 0xE },{ 'F', 0xF },
            { '0', 0x0 },{ '1', 0x1 },{ '2', 0x2 },{ '3', 0x3 },
            { '4', 0x4 },{ '5', 0x5 },{ '6', 0x6 },{ '7', 0x7 },
            { '8', 0x8 },{ '9', 0x9 }
        };

        /*public void GetCheckXAddr(IntPtr CodeCave, out string CheckpointBaseAddr)
        {
            string CodeCaveAddrString = ((long)CodeCave).ToString("X"); // CodeCave address
            string CodeCavejmpString = ((long)CodeCave - (Self_Vehicle_Addrs.CheckPointxASMAddrLong + 5)).ToString("X"); // address to calculate jump from
            if (CodeCavejmpString.Length % 2 != 0)
                CodeCavejmpString = "0" + CodeCavejmpString;
            byte[] CodeCaveAddr = StringToBytes(CodeCavejmpString);
            Array.Reverse(CodeCaveAddr);

            string JmpToCodeCaveCodeString = "E9" + BitConverter.ToString(CodeCaveAddr).Replace("-", String.Empty) + "9090"; // code that replaces original code - 90 = nop, as many as these as you need
            byte[] JmpToCodeCaveCode = StringToBytes(JmpToCodeCaveCodeString);

            byte[] jmpBackBytes = longToByteArray(Self_Vehicle_Addrs.CheckPointxASMAddrLong + 6 - (long)(CodeCave + 19)); // (address + jmp code) - address of end of code cave
            Array.Reverse(jmpBackBytes);
            string InsideCaveCodeString = "48890D21000000" + "0F288960020000E9" + BitConverter.ToString(jmpBackBytes).Replace("-", String.Empty).Replace("FFFFFFFF", String.Empty); // move reg to address within code cave + original code + jump back
            if (MainWindow.mw.gvp.Plat == "Forza Horizon 5")
                InsideCaveCodeString = "48890D21000000" + "0F108960020000E9" + BitConverter.ToString(jmpBackBytes).Replace("-", String.Empty).Replace("FFFFFFFF", String.Empty);
            byte[] InsideCaveCode = StringToBytes(InsideCaveCodeString);

            MainWindow.mw.m.WriteBytes(CodeCaveAddrString, InsideCaveCode);
            MainWindow.mw.m.WriteBytes(Self_Vehicle_Addrs.CheckPointxASMAddr, JmpToCodeCaveCode);

            byte[] CheckBaseAddrArray = MainWindow.mw.m.ReadBytes(((long)CodeCave + 40).ToString("X"), 8);
            Array.Reverse(CheckBaseAddrArray);
            CheckpointBaseAddr = BitConverter.ToString(CheckBaseAddrArray).Replace("-", String.Empty);

            while (CheckpointBaseAddr == "0000000000000000")
            {
                CheckBaseAddrArray = MainWindow.mw.m.ReadBytes(((long)CodeCave + 40).ToString("X"), 8);
                Array.Reverse(CheckBaseAddrArray);
                CheckpointBaseAddr = BitConverter.ToString(CheckBaseAddrArray).Replace("-", String.Empty);
                if (Self_Vehicle.s.CheckPointTPworker.CancellationPending)
                    break;
            }
        }*/

        /*public void GetAIXAddr(IntPtr CodeCave6)
        {
            string CodeCaveAddrString = ((long)CodeCave6).ToString("X"); // CodeCave address
            string CodeCavejmpString = ((long)CodeCave6 - (Self_Vehicle_Addrs.AIXAobAddrLong + 5)).ToString("X"); // address to calculate jump from
            if (CodeCavejmpString.Length % 2 != 0)
                CodeCavejmpString = "0" + CodeCavejmpString;
            byte[] CodeCaveAddr = StringToBytes(CodeCavejmpString);
            Array.Reverse(CodeCaveAddr);

            string JmpToCodeCaveCodeString = "E9" + BitConverter.ToString(CodeCaveAddr).Replace("-", String.Empty) + "9090"; // code that replaces original code - 90 = nop, as many as these as you need
            byte[] JmpToCodeCaveCode = StringToBytes(JmpToCodeCaveCodeString);

            byte[] jmpBackBytes = longToByteArray(Self_Vehicle_Addrs.AIXAobAddrLong + 7 - (long)(CodeCave6 + 30)); // (address + jmp code) - address of end of code cave
            Array.Reverse(jmpBackBytes);
            string InsideCaveCodeString = "58488B052D000000488B00483B414075040F114140488BFA50E9" + BitConverter.ToString(jmpBackBytes).Replace("-", String.Empty).Replace("FFFFFFFF", String.Empty); // move reg to address within code cave + original code + jump back
            if (MainWindow.mw.gvp.Plat == "Forza Horizon 5")
                //FH5
                InsideCaveCodeString = "58488B052D000000488B00483B415075040F1141500F28EB50E9" + BitConverter.ToString(jmpBackBytes).Replace("-", String.Empty).Replace("FFFFFFFF", String.Empty);
            byte[] InsideCaveCode = StringToBytes(InsideCaveCodeString);

            MainWindow.mw.m.WriteBytes(CodeCaveAddrString, InsideCaveCode);
            MainWindow.mw.m.WriteBytes(Self_Vehicle_Addrs.AIXAobAddr, JmpToCodeCaveCode);

            while (Self_Vehicle.s.FreezeAIBox.Checked)
            {
                if (Self_Vehicle.s.FreezeAIWorker.CancellationPending)
                    break;
                try
                {
                    byte[] MyAddr = StringToBytes("0" + ((long)MainWindow.mw.m.Get64BitCode(Self_Vehicle_Addrs.xAddr)).ToString("X"));
                    Array.Reverse(MyAddr);
                    MainWindow.mw.m.WriteBytes(((long)CodeCave6 + 53).ToString("X"), MyAddr);
                }
                catch { }
                Thread.Sleep(10);
            }
        }*/

        public void CosmeticUnlocker(IntPtr CodeCave7)
        {
            string CodeCaveAddrString = ((long)CodeCave7).ToString("X"); // CodeCave address
            string CodeCavejmpString = ((long)CodeCave7 - (Self_Vehicle_Addrs.CosmeticUnlockAddrLong + 5)).ToString("X"); // address to calculate jump from
            if (CodeCavejmpString.Length % 2 != 0)
                CodeCavejmpString = "0" + CodeCavejmpString;
            byte[] CodeCaveAddr = StringToBytes(CodeCavejmpString);
            Array.Reverse(CodeCaveAddr);

            string JmpToCodeCaveCodeString = "E9" + BitConverter.ToString(CodeCaveAddr).Replace("-", String.Empty); // code that replaces original code - 90 = nop, as many as these as you need
            if (JmpToCodeCaveCodeString.Length == 8)
                JmpToCodeCaveCodeString += "00";
            byte[] JmpToCodeCaveCode = StringToBytes(JmpToCodeCaveCodeString);

            byte[] jmpBackBytes = longToByteArray(Self_Vehicle_Addrs.CosmeticUnlockAddrLong + 5 - (long)(CodeCave7 + 17)); // (address + jmp code) - address of end of code cave
            Array.Reverse(jmpBackBytes);
            string InsideCaveCodeString = "57BF8C0000008BF75F8B4364E9" + BitConverter.ToString(jmpBackBytes).Replace("-", String.Empty).Replace("FFFFFFFF", String.Empty); // move reg to address within code cave + original code + jump back
            byte[] InsideCaveCode = StringToBytes(InsideCaveCodeString);

            MainWindow.mw.m.WriteBytes(CodeCaveAddrString, InsideCaveCode);
            MainWindow.mw.m.WriteBytes(Self_Vehicle_Addrs.CosmeticUnlockAddr, JmpToCodeCaveCode);
        }

        /*public void HornUnlocker(IntPtr CodeCave8)
        {
            string CodeCaveAddrString = ((long)CodeCave8).ToString("X"); // CodeCave address
            string CodeCavejmpString = ((long)CodeCave8 - (Self_Vehicle.HornAsmAddrLong + 5)).ToString("X"); // address to calculate jump from
            if (CodeCavejmpString.Length % 2 != 0)
                CodeCavejmpString = "0" + CodeCavejmpString;
            byte[] CodeCaveAddr = StringToBytes(CodeCavejmpString);
            Array.Reverse(CodeCaveAddr);

            string JmpToCodeCaveCodeString = "E9" + BitConverter.ToString(CodeCaveAddr).Replace("-", String.Empty); // code that replaces original code - 90 = nop, as many as these as you need
            if (JmpToCodeCaveCodeString.Length == 8)
                JmpToCodeCaveCodeString += "00";
            byte[] JmpToCodeCaveCode = StringToBytes(JmpToCodeCaveCodeString);

            while(Horn.h.HornEnableBox.Checked)
            {
                int HornID = Horn.h.HornList[Horn.h.HornComboBox.SelectedIndex].ID;
                string HornIDHex = HornID.ToString("X8");
                byte[] HornIDHexBytes = StringToBytes(HornIDHex);
                Array.Reverse(HornIDHexBytes);
                HornIDHex = BitConverter.ToString(HornIDHexBytes).Replace("-", String.Empty);

                byte[] jmpBackBytes = longToByteArray(Self_Vehicle.HornAsmAddrLong + 5 - (long)(CodeCave8 + 15)); // (address + jmp code) - address of end of code cave
                Array.Reverse(jmpBackBytes);
                string InsideCaveCodeString = "BA" + HornIDHex + "8BFA488BD9E9" + BitConverter.ToString(jmpBackBytes).Replace("-", String.Empty).Replace("FFFFFFFF", String.Empty); // move reg to address within code cave + original code + jump back
                byte[] InsideCaveCode = StringToBytes(InsideCaveCodeString);

                MainWindow.mw.m.WriteBytes(CodeCaveAddrString, InsideCaveCode);
                MainWindow.mw.m.WriteBytes(Self_Vehicle.HornAsmAddr, JmpToCodeCaveCode);
                Thread.Sleep(25);
            }
        }*/
        /*public void GetWayPointXAddr(IntPtr CodeCave4, out string WayPointBaseAddr)
        {
            string CodeCaveAddrString = ((long)CodeCave4).ToString("X"); // CodeCave address
            string CodeCavejmpString = ((long)CodeCave4 - (Self_Vehicle_Addrs.WayPointxASMAddrLong + 5)).ToString("X"); // address to calculate jump from
            if (CodeCavejmpString.Length % 2 != 0)
                CodeCavejmpString = "0" + CodeCavejmpString;
            byte[] CodeCaveAddr = StringToBytes(CodeCavejmpString);
            Array.Reverse(CodeCaveAddr);

            string JmpToCodeCaveCodeString = "E9" + BitConverter.ToString(CodeCaveAddr).Replace("-", String.Empty) + "9090"; // code that replaces original code - 90 = nop, as many as these as you need
            byte[] JmpToCodeCaveCode = StringToBytes(JmpToCodeCaveCodeString);

            byte[] jmpBackBytes = longToByteArray(Self_Vehicle_Addrs.WayPointxASMAddrLong + 7 - (long)(CodeCave4 + 19)); // (address + jmp code) - address of end of code cave
            Array.Reverse(jmpBackBytes);
            string InsideCaveCodeString = "48893D22000000" + "0F1097A0030000E9" + BitConverter.ToString(jmpBackBytes).Replace("-", String.Empty).Replace("FFFFFFFF", String.Empty); // move reg to address within code cave + original code + jump back
            if (MainWindow.mw.gvp.Plat == "Forza Horizon 5")
                InsideCaveCodeString = "48893D22000000" + "0F109750020000E9" + BitConverter.ToString(jmpBackBytes).Replace("-", String.Empty).Replace("FFFFFFFF", String.Empty);
            byte[] InsideCaveCode = StringToBytes(InsideCaveCodeString);

            MainWindow.mw.m.WriteBytes(CodeCaveAddrString, InsideCaveCode);
            MainWindow.mw.m.WriteBytes(Self_Vehicle_Addrs.WayPointxASMAddr, JmpToCodeCaveCode);

            Thread.Sleep(25);
            byte[] WayBaseAddrArray = MainWindow.mw.m.ReadBytes(((long)CodeCave4 + 41).ToString("X"), 6);
            Array.Reverse(WayBaseAddrArray);
            WayPointBaseAddr = BitConverter.ToString(WayBaseAddrArray).Replace("-", String.Empty);
            if (WayPointBaseAddr == "000000000000" && Self_Vehicle.s.AutoWayPoint.Checked == false)
                MessageBox.Show("Make a new waypoint");

            while ((WayPointBaseAddr == "000000000000" || WayPointBaseAddr == null) && !Self_Vehicle.s.WayPointWorker.CancellationPending)
            {
                Thread.Sleep(10);
                try
                {
                    WayBaseAddrArray = MainWindow.mw.m.ReadBytes(((long)CodeCave4 + 41).ToString("X"), 6);
                    Array.Reverse(WayBaseAddrArray);
                    WayPointBaseAddr = BitConverter.ToString(WayBaseAddrArray).Replace("-", String.Empty);
                }
                catch { }
            }
            if (!Self_Vehicle.s.WayPointWorker.CancellationPending)
            {

                Self_Vehicle_Addrs.WayPointxAddr = (Int64.Parse(WayPointBaseAddr, NumberStyles.HexNumber) + 928).ToString("X");
                Self_Vehicle_Addrs.WayPointyAddr = (Int64.Parse(WayPointBaseAddr, NumberStyles.HexNumber) + 932).ToString("X");
                Self_Vehicle_Addrs.WayPointzAddr = (Int64.Parse(WayPointBaseAddr, NumberStyles.HexNumber) + 936).ToString("X");
                if (MainWindow.mw.gvp.Plat == "Forza Horizon 5")
                {
                    Self_Vehicle_Addrs.WayPointxAddr = (Int64.Parse(WayPointBaseAddr, NumberStyles.HexNumber) + 592).ToString("X");
                    Self_Vehicle_Addrs.WayPointyAddr = (Int64.Parse(WayPointBaseAddr, NumberStyles.HexNumber) + 596).ToString("X");
                    Self_Vehicle_Addrs.WayPointzAddr = (Int64.Parse(WayPointBaseAddr, NumberStyles.HexNumber) + 600).ToString("X");
                }
                float WayPointX = MainWindow.mw.m.ReadFloat(Self_Vehicle_Addrs.WayPointxAddr, round: false);
                float WayPointY = MainWindow.mw.m.ReadFloat(Self_Vehicle_Addrs.WayPointyAddr, round: false) + 3;
                float WayPointZ = MainWindow.mw.m.ReadFloat(Self_Vehicle_Addrs.WayPointzAddr, round: false);
                if ((WayPointX != 0 && WayPointY != 0 && WayPointZ != 0
                    && WayPointX < 10000 && WayPointX > -10000
                    && WayPointY < 1000 && WayPointY > -1000
                    && WayPointZ < 10000 && WayPointZ > -10000) && !Self_Vehicle.s.AutoWayPoint.Checked)
                {
                    MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.xAddr, "float", WayPointX.ToString());
                    MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.yAddr, "float", WayPointY.ToString());
                    MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.zAddr, "float", WayPointZ.ToString());
                }
            }

            byte[] WayPointCodeBefore = new byte[7] { 0x0F, 0x10, 0xA0, 0x90, 0x03, 0x00, 0x00 };
            if (MainWindow.mw.gvp.Plat == "Forza Horizon 5")
                WayPointCodeBefore = new byte[7] { 0x0F, 0x10, 0x97, 0x50, 0x02, 0x00, 0x00 };
            MainWindow.mw.m.WriteBytes(Self_Vehicle_Addrs.WayPointxASMAddr, WayPointCodeBefore);
        }*/

        public void GetTimeAddr(IntPtr CodeCave2)
        {
            string CodeCaveAddrString = ((long)CodeCave2).ToString("X");
            string CodeCavejmpString = ((long)CodeCave2 - (Self_Vehicle_Addrs.TimeNOPAddrLong + 5)).ToString("X");
            if (CodeCavejmpString.Length % 2 != 0)
                CodeCavejmpString = "0" + CodeCavejmpString;
            byte[] CodeCaveAddr = StringToBytes(CodeCavejmpString);
            Array.Reverse(CodeCaveAddr);

            string JmpToCodeCaveCodeString = "E9" + BitConverter.ToString(CodeCaveAddr).Replace("-", String.Empty) + "9090";
            byte[] JmpToCodeCaveCode = StringToBytes(JmpToCodeCaveCodeString);

            byte[] TimeJumpBefore = new byte[9] { 0xF2, 0x0F, 0x11, 0x43, 0x08, 0x48, 0x83, 0xC4, 0x40 }; // bytes at the time adding code

            byte[] jmpBackBytes = longToByteArray(Self_Vehicle_Addrs.TimeNOPAddrLong + 6 - (long)(CodeCave2 + 19)); ;
            Array.Reverse(jmpBackBytes);
            string InsideCaveCodeString = "48891D21000000F20F1143084883C440E9" + BitConverter.ToString(jmpBackBytes).Replace("-", String.Empty).Replace("FFFFFFFF", String.Empty);
            byte[] InsideCaveCode = StringToBytes(InsideCaveCodeString);

            MainWindow.mw.m.WriteBytes(CodeCaveAddrString, InsideCaveCode);
            MainWindow.mw.m.WriteBytes(Self_Vehicle_Addrs.TimeNOPAddr, JmpToCodeCaveCode);
            Thread.Sleep(25);
            Self_Vehicle_Addrs.TimeAddr = (MainWindow.mw.m.ReadLong(((long)CodeCave2 + 40).ToString("X")) + 8).ToString("X");
            MainWindow.mw.m.WriteBytes(Self_Vehicle_Addrs.TimeNOPAddr, TimeJumpBefore);
        }

        public static void StartXPtool(IntPtr CodeCave3)
        {
            byte[] OnePoint = { 0xB9, 0x01, 0x00, 0x00, 0x00, 0x90 };
            string CodeCaveAddrString = ((long)CodeCave3).ToString("X");
            string CodeCavejmpString = ((long)CodeCave3 - (Self_Vehicle_Addrs.XPaddrLong + 5)).ToString("X");
            if (CodeCavejmpString.Length % 2 != 0)
                CodeCavejmpString = "0" + CodeCavejmpString;
            byte[] CodeCaveAddr = StringToBytes(CodeCavejmpString);
            Array.Reverse(CodeCaveAddr);

            string XPGiveCodeString = "E9" + BitConverter.ToString(CodeCaveAddr).Replace("-", String.Empty) + "9090";
            byte[] XPGiveCode = StringToBytes(XPGiveCodeString);

            string XPAmountHex = Convert.ToInt64(UnlocksPage.Up.XpNum.Value).ToString("X8");
            byte[] XPAmountArray = StringToBytes(XPAmountHex);
            Array.Reverse(XPAmountArray);

            byte[] jmpBackBytes = longToByteArray(Self_Vehicle_Addrs.XPaddrLong + 7 - (long)(CodeCave3 + 16));
            Array.Reverse(jmpBackBytes);
            string InsideCaveCodeString = "F30F2CC6C745B8" + BitConverter.ToString(XPAmountArray).Replace("-", String.Empty) + "E9" + (BitConverter.ToString(jmpBackBytes).Replace("-", String.Empty)).Replace("FFFFFFFF", String.Empty);
            if (MainWindow.mw.gvp.Plat == "Forza Horizon 5")
                InsideCaveCodeString = "F30F2CC6C745B0" + BitConverter.ToString(XPAmountArray).Replace("-", String.Empty) + "E9" + (BitConverter.ToString(jmpBackBytes).Replace("-", String.Empty)).Replace("FFFFFFFF", String.Empty);
            byte[] InsideCaveCode = StringToBytes(InsideCaveCodeString);

            MainWindow.mw.m.WriteBytes(Self_Vehicle_Addrs.XPAmountaddr, OnePoint);
            MainWindow.mw.m.WriteBytes(CodeCaveAddrString, InsideCaveCode);
            MainWindow.mw.m.WriteBytes(Self_Vehicle_Addrs.XPaddr, XPGiveCode);
        }

        public static void GlowingPaint(IntPtr CodeCave9)
        {
            var CodeCaveAddrString = CodeCave9.ToString("X");
            var CodeCavejmpString = (CodeCave9 - (Self_Vehicle_Addrs.GlowingPaintAddrLong + 5)).ToString("X");
            if (CodeCavejmpString.Length % 2 != 0)
                CodeCavejmpString = "0" + CodeCavejmpString;
            var CodeCaveAddr = StringToBytes(CodeCavejmpString);
            Array.Reverse(CodeCaveAddr);

            var JmpToCodeCaveCodeString = "E9" + BitConverter.ToString(CodeCaveAddr).Replace("-", String.Empty);
            var jmpBackBytes = longToByteArray(Self_Vehicle_Addrs.GlowingPaintAddrLong + 5 - (CodeCave9 + 17));
            Array.Reverse(jmpBackBytes);

            var InsideCaveCodeString = "0F590D49000000410F114A10E9" + BitConverter.ToString(jmpBackBytes).Replace("-", String.Empty).Replace("FFFFFFFF", String.Empty);

            if (MainWindow.mw.gvp.Name == "Forza Horizon 5")
            {
                JmpToCodeCaveCodeString = "E9" + BitConverter.ToString(CodeCaveAddr).Replace("-", String.Empty) + "9090";
                InsideCaveCodeString = "0F590D490000000F110AC642F001E9" + BitConverter.ToString(jmpBackBytes).Replace("-", String.Empty).Replace("FFFFFFFF", String.Empty);
            }
            
            var JmpToCodeCaveCode = StringToBytes(JmpToCodeCaveCodeString);
            var InsideCaveCode = StringToBytes(InsideCaveCodeString);
            MainWindow.mw.m.WriteBytes(CodeCaveAddrString, InsideCaveCode);
            MainWindow.mw.m.WriteBytes(Self_Vehicle_Addrs.GlowingPaintAddr, JmpToCodeCaveCode);
        }

        public static void RemoveBuildCap(IntPtr CodeCave10, IntPtr CodeCave11)
        {
            string CodeCave1AddrString = ((long)CodeCave10).ToString("X");
            string CodeCave1jmpString = ((long)CodeCave10 - (Self_Vehicle_Addrs.BuildCapAddrASM1Long + 5)).ToString("X");
            if (CodeCave1jmpString.Length % 2 != 0) 
                CodeCave1jmpString = "0" + CodeCave1jmpString;
            byte[] CodeCave1Addr = StringToBytes(CodeCave1jmpString);
            Array.Reverse(CodeCave1Addr);

            string CodeCave2AddrString = ((long)CodeCave11).ToString("X");
            string CodeCave2jmpString = ((long)CodeCave11 - (Self_Vehicle_Addrs.BuildCapAddrASM2Long + 5)).ToString("X");
            if (CodeCave2jmpString.Length % 2 != 0) 
                CodeCave2jmpString = "0" + CodeCave2jmpString;
            byte[] CodeCave2Addr = StringToBytes(CodeCave2jmpString);
            Array.Reverse(CodeCave2Addr);

            byte[] JmpBackBytes1 = longToByteArray(Self_Vehicle_Addrs.BuildCapAddrASM1Long + 8 - (long)(CodeCave10 + 23));
            byte[] JmpBackBytes2 = longToByteArray(Self_Vehicle_Addrs.BuildCapAddrASM2Long + 5 - (long)(CodeCave11 + 17));
            Array.Reverse(JmpBackBytes1);
            Array.Reverse(JmpBackBytes2);
            string JmpToCodeCaveCode1String = "E9" + BitConverter.ToString(CodeCave1Addr).Replace("-", String.Empty) + "909090";
            byte[] JmpToCodeCaveCode1 = StringToBytes(JmpToCodeCaveCode1String);

            string JmpToCodeCaveCode2String = "E9" + BitConverter.ToString(CodeCave2Addr).Replace("-", String.Empty);
            byte[] JmpToCodeCaveCode2 = StringToBytes(JmpToCodeCaveCode2String);

            string InsideCodeCave1String = "F30F11834C040000C7834C04000000000000E9" + BitConverter.ToString(JmpBackBytes1).Replace("-", String.Empty).Replace("FFFFFFFF", String.Empty);
            string InsideCodeCave2String = "F30F114344C7434400000000E9" + BitConverter.ToString(JmpBackBytes2).Replace("-", String.Empty).Replace("FFFFFFFF", String.Empty);

            if (MainWindow.mw.gvp.Name == "Forza Horizon 4")
            {
                InsideCodeCave1String = "F30F11B3DC030000C783DC03000000000000E9" + BitConverter.ToString(JmpBackBytes1).Replace("-", String.Empty).Replace("FFFFFFFF", String.Empty);
                InsideCodeCave2String = "F30F114330C7433000000000E9" + BitConverter.ToString(JmpBackBytes2).Replace("-", String.Empty).Replace("FFFFFFFF", String.Empty);
            }

            byte[] InsideCaveCode1 = StringToBytes(InsideCodeCave1String);
            byte[] InsideCaveCode2 = StringToBytes(InsideCodeCave2String);

            MainWindow.mw.m.WriteBytes(CodeCave1AddrString, InsideCaveCode1);
            MainWindow.mw.m.WriteBytes(CodeCave2AddrString, InsideCaveCode2);
            MainWindow.mw.m.WriteBytes(Self_Vehicle_Addrs.BuildCapAddrASM1, JmpToCodeCaveCode1);
            MainWindow.mw.m.WriteBytes(Self_Vehicle_Addrs.BuildCapAddrASM2, JmpToCodeCaveCode2);
        }

        public static byte[] OriginalBaseAddressHookBytes;

        public static void GetBaseAddress(IntPtr CodeCave12)
        {
            string CodeCaveAddrString = ((long)CodeCave12).ToString("X");
            string CodeCavejmpString = ((long)CodeCave12 - (Self_Vehicle_Addrs.BaseAddrHookLong + 5)).ToString("X");
            if (CodeCavejmpString.Length % 2 != 0)
                CodeCavejmpString = "0" + CodeCavejmpString;
            byte[] CodeCaveAddr = StringToBytes(CodeCavejmpString);
            Array.Reverse(CodeCaveAddr);

            string JmpToCodeCaveCodeString = "E9" + BitConverter.ToString(CodeCaveAddr).Replace("-", String.Empty) + "909090";
            byte[] JmpToCodeCaveCode = StringToBytes(JmpToCodeCaveCodeString);

            byte[] JmpBackBytes = longToByteArray(Self_Vehicle_Addrs.BaseAddrHookLong + 6 - (long)(CodeCave12 + 32)); ;
            Array.Reverse(JmpBackBytes);

            string InsideCaveCodeString = (BitConverter.ToString(OriginalBaseAddressHookBytes) + "4881E97005000048890D2B0000004881C170050000E9" + BitConverter.ToString(JmpBackBytes)).Replace("-", String.Empty).Replace("FFFFFFFF", String.Empty);
            byte[] InsideCaveCode = StringToBytes(InsideCaveCodeString);

            MainWindow.mw.m.WriteBytes(CodeCaveAddrString, InsideCaveCode);
            MainWindow.mw.m.WriteBytes(Self_Vehicle_Addrs.BaseAddrHook, JmpToCodeCaveCode);

            while (MainWindow.mw.Attached)
            {
                Thread.Sleep(50);
                long Addr = MainWindow.mw.m.ReadLong(((long)CodeCave12 + 65).ToString("X"));

                if (MainWindow.mw.m.ReadFloat((Addr + 0x50).ToString("X")) != 0
                    && !float.IsInfinity(MainWindow.mw.m.ReadFloat((Addr + 0x50).ToString("X")))
                    && MainWindow.mw.m.ReadFloat((Addr + 0x50).ToString("X")) > -10000000
                    && MainWindow.mw.m.ReadFloat((Addr + 0x50).ToString("X")) < 10000000
                    && MainWindow.mw.m.ReadFloat((Addr + 0x54).ToString("X")) > -10000
                    && MainWindow.mw.m.ReadFloat((Addr + 0x54).ToString("X")) < 10000
                    && MainWindow.mw.m.ReadFloat((Addr + 0x58).ToString("X")) > -10000000
                    && MainWindow.mw.m.ReadFloat((Addr + 0x58).ToString("X")) < 10000000
                    && MainWindow.mw.m.ReadFloat((Addr + 0xC).ToString("X")) < 0.2
                    && MainWindow.mw.m.ReadFloat((Addr + 0xC).ToString("X")) > 0.01)
                {
                    Self_Vehicle_Addrs.BaseAddr = MainWindow.mw.m.ReadLong(Addr.ToString("X")).ToString("X");
                    Self_Vehicle_Addrs.BaseAddrLong = MainWindow.mw.m.ReadLong(Addr.ToString("X"));
                    Self_Vehicle_Addrs.AddressesFive();
                }
            }
        }

        public static void FH4TuningAddressesHook(IntPtr TuningCodeCave1, IntPtr TuningCodeCave2, IntPtr TuningCodeCave3, IntPtr TuningCodeCave4)
        {
            #region Hook 1

            string CodeCaveAddrString = ((long)TuningCodeCave1).ToString("X");
            string CodeCavejmpString = ((long)TuningCodeCave1 - (Tuning_Addresses.TuningTableHookBase1 + 5)).ToString("X");
            if (CodeCavejmpString.Length % 2 != 0) CodeCavejmpString = "0" + CodeCavejmpString;
            byte[] CodeCaveAddr = StringToBytes(CodeCavejmpString);
            Array.Reverse(CodeCaveAddr);

            string JmpToCodeCaveCodeString = "E9" + BitConverter.ToString(CodeCaveAddr).Replace("-", String.Empty);
            byte[] JmpToCodeCaveCode = StringToBytes(JmpToCodeCaveCodeString);

            byte[] JmpBackBytes = longToByteArray(Tuning_Addresses.TuningTableHookBase1 + 5 - (long)(TuningCodeCave1 + 17));
            Array.Reverse(JmpBackBytes);

            string InsideCaveCodeString = "4C893549000000498B068BD6E9" + BitConverter.ToString(JmpBackBytes).Replace("-", String.Empty).Replace("FFFFFFFF", String.Empty);
            byte[] InsideCaveCode = StringToBytes(InsideCaveCodeString);

            MainWindow.mw.m.WriteBytes(CodeCaveAddrString, InsideCaveCode);
            MainWindow.mw.m.WriteBytes(Tuning_Addresses.TuningTableHookBase1.ToString("X"), JmpToCodeCaveCode);

            #endregion

            #region Hook 2

            string CodeCave2AddrString = ((long)TuningCodeCave2).ToString("X");
            string CodeCave2jmpString = ((long)TuningCodeCave2 - (Tuning_Addresses.TuningTableHookBase2 + 5)).ToString("X");
            if (CodeCave2jmpString.Length % 2 != 0)
                CodeCave2jmpString = "0" + CodeCave2jmpString;
            byte[] CodeCave2Addr = StringToBytes(CodeCave2jmpString);
            Array.Reverse(CodeCave2Addr);

            string JmpToCodeCaveCode2String = "E9" + BitConverter.ToString(CodeCave2Addr).Replace("-", String.Empty) + "90909090";
            byte[] JmpToCodeCaveCode2 = StringToBytes(JmpToCodeCaveCode2String);

            byte[] JmpBackBytes2 = longToByteArray(Tuning_Addresses.TuningTableHookBase2 + 5 - (long)(TuningCodeCave2 + 17));
            Array.Reverse(JmpBackBytes2);

            string InsideCaveCodeString2 = "4C892549000000498B0424488D542420E9" + BitConverter.ToString(JmpBackBytes2).Replace("-", String.Empty).Replace("FFFFFFFF", String.Empty);
            byte[] InsideCaveCode2 = StringToBytes(InsideCaveCodeString2);

            MainWindow.mw.m.WriteBytes(CodeCave2AddrString, InsideCaveCode2);
            MainWindow.mw.m.WriteBytes(Tuning_Addresses.TuningTableHookBase2.ToString("X"), JmpToCodeCaveCode2);

            #endregion

            #region Hook 3

            string CodeCave3AddrString = ((long)TuningCodeCave3).ToString("X");
            string CodeCave3jmpString = ((long)TuningCodeCave3 - (Tuning_Addresses.TuningTableHookBase3 + 5)).ToString("X");
            if (CodeCave3jmpString.Length % 2 != 0) CodeCave3jmpString = "0" + CodeCave3jmpString;
            byte[] CodeCave3Addr = StringToBytes(CodeCave3jmpString);
            Array.Reverse(CodeCave3Addr);

            string JmpToCodeCaveCode3String = "E9" + BitConverter.ToString(CodeCave3Addr).Replace("-", String.Empty) + "9090";
            byte[] JmpToCodeCaveCode3 = StringToBytes(JmpToCodeCaveCode3String);

            byte[] JmpBackBytes3 = longToByteArray(Tuning_Addresses.TuningTableHookBase3 + 5 - (long)(TuningCodeCave3 + 22));
            Array.Reverse(JmpBackBytes3);

            string InsideCaveCodeString3 = "51488BC848890D45000000590F28CEF30F1010E9" + BitConverter.ToString(JmpBackBytes3).Replace("-", String.Empty).Replace("FFFFFFFF", String.Empty);
            byte[] InsideCaveCode3 = StringToBytes(InsideCaveCodeString3);

            MainWindow.mw.m.WriteBytes(CodeCave3AddrString, InsideCaveCode3);
            MainWindow.mw.m.WriteBytes(Tuning_Addresses.TuningTableHookBase3.ToString("X"), JmpToCodeCaveCode3);

            #endregion

            #region Hook 4

            string CodeCave4AddrString = ((long)TuningCodeCave4).ToString("X");
            string CodeCave4jmpString = ((long)TuningCodeCave4 - (Tuning_Addresses.TuningTableHookBase4 + 5)).ToString("X");
            if (CodeCave4jmpString.Length % 2 != 0) 
                CodeCave4jmpString = "0" + CodeCave4jmpString;
            byte[] CodeCave4Addr = StringToBytes(CodeCave4jmpString);
            Array.Reverse(CodeCave4Addr);

            string JmpToCodeCaveCode4String = "E9" + BitConverter.ToString(CodeCave4Addr).Replace("-", String.Empty) + "9090909090";
            byte[] JmpToCodeCaveCode4 = StringToBytes(JmpToCodeCaveCode4String);

            byte[] JmpBackBytes4 = longToByteArray(Tuning_Addresses.TuningTableHookBase4 + 10 - (long)(TuningCodeCave4 + 22));
            Array.Reverse(JmpBackBytes4);

            string InsideCaveCodeString4 = "488B0748893D46000000488D9560020000E9" + BitConverter.ToString(JmpBackBytes4).Replace("-", String.Empty).Replace("FFFFFFFF", String.Empty);
            byte[] InsideCaveCode4 = StringToBytes(InsideCaveCodeString4);

            MainWindow.mw.m.WriteBytes(CodeCave4AddrString, InsideCaveCode4);
            MainWindow.mw.m.WriteBytes(Tuning_Addresses.TuningTableHookBase4.ToString("X"), JmpToCodeCaveCode4);

            #endregion

            // Address reading
            while (MainWindow.mw.Attached)
            {
                Tuning_Addresses.TuningTableBase1FH4 = MainWindow.mw.m.ReadLong(((long)TuningCodeCave1 + 0x50).ToString("X"));
                Tuning_Addresses.TuningTableBase2FH4 = MainWindow.mw.m.ReadLong(((long)TuningCodeCave2 + 0x50).ToString("X"));
                Tuning_Addresses.TuningTableBase3FH4 = MainWindow.mw.m.ReadLong(((long)TuningCodeCave3 + 0x50).ToString("X"));
                Tuning_Addresses.TuningTableBase4FH4 = MainWindow.mw.m.ReadLong(((long)TuningCodeCave4 + 0x50).ToString("X")) + 400;
                Tuning_Addresses.AddressesFH4();
                Thread.Sleep(1000);
            }
        }

        // I am so glad the function is identical on both games bc I'd kill myself if it wasnt
        public static void Credits(IntPtr CodeCave13)
        {
            var codeCaveAddrString = ((long)CodeCave13).ToString("X");
            var codeCavejmpString = ((long)CodeCave13 - (Self_Vehicle_Addrs.CreditsHookAddrLong + 5)).ToString("X");
            if (codeCavejmpString.Length % 2 != 0)
                codeCavejmpString = "0" + codeCavejmpString;
            var codeCaveAddr = StringToBytes(codeCavejmpString);
            Array.Reverse(codeCaveAddr);

            var jmpBackBytes = longToByteArray(Self_Vehicle_Addrs.CreditsHookAddrLong + 7 - (long)(CodeCave13 + 19));
            Array.Reverse(jmpBackBytes);
            var jmpToCodeCaveCodeString = "E9" + BitConverter.ToString(codeCaveAddr).Replace("-", String.Empty) + "9090";
            var jmpToCodeCaveCode = StringToBytes(jmpToCodeCaveCodeString);

            // mov rax,[hook+35]
            // mov [rsp+80],eax
            // jmp back bytes
            var insideCodeCaveString = "488B052E00000089842480000000E9" + BitConverter.ToString(jmpBackBytes).Replace("-", String.Empty).Replace("FFFFFFFF", String.Empty);
            var insideCaveCode = StringToBytes(insideCodeCaveString);
            MainWindow.mw.m.WriteBytes(codeCaveAddrString, insideCaveCode);
            MainWindow.mw.m.WriteBytes(Self_Vehicle_Addrs.CreditsHookAddr, jmpToCodeCaveCode);
        }

        public static void GetUnbreakableSkillComboAddr(IntPtr CodeCave14)
        {
            var codeCaveAddrString = ((long)CodeCave14).ToString("X");
            var codeCavejmpString = (CodeCave14 - (Self_Vehicle_Addrs.UnbSkillHookLong + 5)).ToString("X");
            if (codeCavejmpString.Length % 2 != 0)
                codeCavejmpString = "0" + codeCavejmpString;
            var codeCaveAddr = StringToBytes(codeCavejmpString);
            Array.Reverse(codeCaveAddr);

            var jmpBackBytes = longToByteArray(Self_Vehicle_Addrs.UnbSkillHookLong + 6 - (CodeCave14 + 18));
            Array.Reverse(jmpBackBytes);
            var jmpToCodeCaveCodeString = "E9" + BitConverter.ToString(codeCaveAddr).Replace("-", String.Empty) + "90";
            var jmpToCodeCaveCode = StringToBytes(jmpToCodeCaveCodeString);

            // mov [hook+50],rbx
            // mov rdx,[rax]
            // mov rcx,rax
            // jmp back bytes
            var insideCodeCaveString = "48891D49000000488B10488BC8E9" + BitConverter.ToString(jmpBackBytes).Replace("-", String.Empty).Replace("FFFFFFFF", String.Empty);
            var insideCaveCode = StringToBytes(insideCodeCaveString);
            MainWindow.mw.m.WriteBytes(codeCaveAddrString, insideCaveCode);
            MainWindow.mw.m.WriteBytes(Self_Vehicle_Addrs.UnbSkillHookAddr, jmpToCodeCaveCode);

            Task.Run(() =>
            {
                Self_Vehicle_Addrs.UnbSkillAddrLong = 0x0;

                while (Self_Vehicle_Addrs.UnbSkillAddrLong == 0x0)
                {
                    Self_Vehicle_Addrs.UnbSkillAddrLong = MainWindow.mw.m.ReadLong((CodeCave14 + 0x50).ToString("X"));
                    Thread.Sleep(50);
                }

                Self_Vehicle_Addrs.UnbSkillAddrLong += 0x2C;
                Self_Vehicle_Addrs.UnbSkillAddr = Self_Vehicle_Addrs.UnbSkillAddrLong.ToString("X");
                MainWindow.mw.m.WriteBytes(Self_Vehicle_Addrs.UnbSkillHookAddr, new byte[] { 0x48, 0x8B, 0x10, 0x48, 0x8B, 0xC8 });
                
                MainWindow.mw.m.WriteMemory(Self_Vehicle_Addrs.UnbSkillAddr, "float", "9999999999");
                MainWindow.mw.m.WriteMemory((Self_Vehicle_Addrs.UnbSkillAddrLong + 4).ToString("X"), "float", "9999999999");
                MainWindow.mw.m.WriteMemory((Self_Vehicle_Addrs.UnbSkillAddrLong + 8).ToString("X"), "float", "9999999999");
            });
        }
    }
}