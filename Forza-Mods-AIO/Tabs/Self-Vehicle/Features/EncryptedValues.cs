using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Forza_Mods_AIO.Resources;

using static Forza_Mods_AIO.Tabs.Self_Vehicle.SelfVehicleAddresses;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.Features;

public abstract class EncryptedValues
{
    public static readonly Detour EncryptedValuesDetour = new();
    public static readonly List<EncryptedEntry> EncryptedEntries = new();
    private const string Original = "89 84 24 80 00 00 00";
    private const string EncValuesBytes = "52 51 56 53 41 51 41 52 41 50 41 53 48 8D 4F B4 48 8D 35 68 00 00 00 48 8B " +
                                          "D6 83 3A 00 74 48 48 31 DB 4D 31 C0 4C 8B CA 4C 8B DA 41 80 39 00 74 08 49 " +
                                          "FF C1 48 FF C3 EB F2 4C 8B 11 45 38 13 75 1D 48 FF C1 49 FF C3 49 FF C0 49 " +
                                          "39 D8 74 02 EB E8 80 7C 1A 01 01 75 0F 8B 44 1A 02 EB 09 48 01 DE 48 83 C6 " +
                                          "06 EB B0 41 5B 41 58 41 5A 41 59 5B 5E 59 5A 89 84 24 80 00 00 00";
    
    public static void Setup(object sender, RoutedEventHandler action)
    {
        if (EncryptedValuesDetour.IsSetup)
        {
            return;
        }

        if (EncryptedValuesDetour.Setup(sender, EncValuesHookAddr, Original, EncValuesBytes, 7, true))
        {
            return;
        }
        
        Detour.FailedHandler(sender, action);
        EncryptedValuesDetour.Clear();
    }
    
    public class EncryptedEntry
    {
        public EncryptedEntry(string name)
        {
            SetToDefault();
            _name = name;
            EncryptedEntries.Add(this);
        }

        private readonly string _name;
        private UIntPtr _address;
        private byte _isEnabled;
        private int _value;

        public void SetValue(int newValue)
        {
            _value = newValue;
            Write();
        } 
        
        public void SetState(bool isEnabled)
        {
            _isEnabled = isEnabled ? (byte)1 : (byte)0;
            Write();
        }

        private void Write()
        {
            if (!EncryptedValuesDetour.IsHooked)
            {
                return;
            }
            
            Setup();
            
            MainWindow.Mw.M.WriteStringMemory(_address, _name);
            MainWindow.Mw.M.WriteMemory(_address + (UIntPtr)_name.Length, (byte)0);
            MainWindow.Mw.M.WriteMemory(_address + (UIntPtr)_name.Length + 1, _isEnabled);
            MainWindow.Mw.M.WriteMemory(_address + (UIntPtr)_name.Length + 2, _value);
        }

        private void Setup()
        {
            if (!EncryptedValuesDetour.IsSetup || _address != 0)
            {
                return;
            }
            
            var address = EncryptedValuesDetour.VariableAddress;
            if (MainWindow.Mw.M.ReadMemory<byte>(address) == 0x0)
            {
                _address = address;
                return;
            }

            _address = EncryptedEntries.Where(ent => ent != this).Aggregate(address, (curr, ent) => curr + (UIntPtr)ent.Size());
        }

        private int Size()
        {
            return _name.Length + sizeof(byte) * 2 + sizeof(int);
        }

        private void SetToDefault()
        {
            _address = 0;
            _isEnabled = 1;
            _value = 0;
        }
    }
}