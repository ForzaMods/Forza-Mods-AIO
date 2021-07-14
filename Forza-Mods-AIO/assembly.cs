﻿using Forza_Mods_AIO.TabForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Forza_Mods_AIO
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
        private readonly static Dictionary<char, byte> hexmap = new Dictionary<char, byte>()
        {
            { 'a', 0xA },{ 'b', 0xB },{ 'c', 0xC },{ 'd', 0xD },
            { 'e', 0xE },{ 'f', 0xF },{ 'A', 0xA },{ 'B', 0xB },
            { 'C', 0xC },{ 'D', 0xD },{ 'E', 0xE },{ 'F', 0xF },
            { '0', 0x0 },{ '1', 0x1 },{ '2', 0x2 },{ '3', 0x3 },
            { '4', 0x4 },{ '5', 0x5 },{ '6', 0x6 },{ '7', 0x7 },
            { '8', 0x8 },{ '9', 0x9 }
        };

        public void GetCheckXAddr(IntPtr CodeCave, out string CheckpointBaseAddr)
        {
            string CodeCaveAddrString = ((long)CodeCave).ToString("X"); // CodeCave address
            string CodeCavejmpString = ((long)CodeCave - (Speedhack.CheckPointxASMAddrLong + 5)).ToString("X"); // address to calculate jump from
            byte[] CodeCaveAddr = StringToBytes("0" + CodeCavejmpString);
            Array.Reverse(CodeCaveAddr);

            string JmpToCodeCaveCodeString = "E9" + BitConverter.ToString(CodeCaveAddr).Replace("-", String.Empty) + "9090"; // code that replaces original code - 90 = nop, as many as these as you need
            byte[] JmpToCodeCaveCode = StringToBytes(JmpToCodeCaveCodeString);

            byte[] jmpBackBytes = longToByteArray(Speedhack.CheckPointxASMAddrLong + 6 - (long)(CodeCave + 19)); // (address + jmp code) - address of end of code cave
            Array.Reverse(jmpBackBytes);
            string InsideCaveCodeString = "48890D210000000F2" + "88130020000E9" + BitConverter.ToString(jmpBackBytes).Replace("-", String.Empty).Replace("FFFFFFFF", String.Empty); // move reg to address within code cave + original code + jump back
            byte[] InsideCaveCode = StringToBytes(InsideCaveCodeString);

            MainWindow.m.WriteBytes(CodeCaveAddrString, InsideCaveCode);
            MainWindow.m.WriteBytes(Speedhack.CheckPointxASMAddr, JmpToCodeCaveCode);

            byte[] CheckBaseAddrArray = MainWindow.m.ReadBytes(((long)CodeCave + 40).ToString("X"), 8);
            Array.Reverse(CheckBaseAddrArray);
            CheckpointBaseAddr = BitConverter.ToString(CheckBaseAddrArray).Replace("-", String.Empty);

            while (CheckpointBaseAddr == "0000000000000000")
            {
                CheckBaseAddrArray = MainWindow.m.ReadBytes(((long)CodeCave + 40).ToString("X"), 8);
                Array.Reverse(CheckBaseAddrArray);
                CheckpointBaseAddr = BitConverter.ToString(CheckBaseAddrArray).Replace("-", String.Empty);
            }
        }

        public void GetTimeAddr(IntPtr CodeCave2)
        {
            string CodeCaveAddrString = ((long)CodeCave2).ToString("X");
            string CodeCavejmpString = ((long)CodeCave2 - (Speedhack.TimeNOPAddrLong + 5)).ToString("X");
            byte[] CodeCaveAddr = StringToBytes("0" + CodeCavejmpString);
            Array.Reverse(CodeCaveAddr);

            string JmpToCodeCaveCodeString = "E9" + BitConverter.ToString(CodeCaveAddr).Replace("-", String.Empty) + "9090";
            byte[] JmpToCodeCaveCode = StringToBytes(JmpToCodeCaveCodeString);

            byte[] TimeJumpBefore = new byte[9] { 0xF2, 0x0F, 0x11, 0x43, 0x08, 0x48, 0x83, 0xC4, 0x40 }; // bytes at the time adding code

            byte[] jmpBackBytes = longToByteArray(Speedhack.TimeNOPAddrLong + 6 - (long)(CodeCave2 + 19)); ;
            Array.Reverse(jmpBackBytes);
            string InsideCaveCodeString = "48891D21000000F20F1143084883C440E9" + BitConverter.ToString(jmpBackBytes).Replace("-", String.Empty).Replace("FFFFFFFF", String.Empty);
            byte[] InsideCaveCode = StringToBytes(InsideCaveCodeString);

            MainWindow.m.WriteBytes(CodeCaveAddrString, InsideCaveCode);
            MainWindow.m.WriteBytes(Speedhack.TimeNOPAddr, JmpToCodeCaveCode);
            Thread.Sleep(25);
            Speedhack.TimeAddr = (MainWindow.m.ReadLong(((long)CodeCave2 + 40).ToString("X")) + 8).ToString("X");
            //byte[] TimeAddrArray = MainWindow.m.ReadBytes(((long)CodeCave2 + 40).ToString("X"), 8);
            MainWindow.m.WriteBytes(Speedhack.TimeNOPAddr, TimeJumpBefore);
            VirtualFreeEx(Process.GetProcessesByName("ForzaHorizon4")[0].Handle, CodeCave2, 0, MEM_DECOMMIT);
        }
    }
}