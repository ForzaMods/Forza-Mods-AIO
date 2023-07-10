using Forza_Mods_AIO.Overlay;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Keys = System.Windows.Forms.Keys;
using System.Windows.Threading;
using IniParser;
using IniParser.Model;

namespace Forza_Mods_AIO.Tabs.Keybinds
{
    internal class KeybindsHandling
    {
        [DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(Int32 vKey);
        static string PathToKeybindings = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Forza Mods Tool\Keybindings.ini";
        static FileIniDataParser Parser = new FileIniDataParser();
        static IniData IniData = new IniData();
        static Dispatcher dispatcher = Application.Current.Dispatcher;

        public static void KeyGrabber(Button sender)
        {
            Keybinds.Grabbing = true;

            string keyBuffer = string.Empty;
            while (keyBuffer.Length == 0)
            {
                foreach (Int32 i in Enum.GetValues(typeof(Keys)))
                {
                    int x = GetAsyncKeyState(i);
                    if ((x == 1) || (x == Int16.MinValue))
                    {
                        if (i != 0 && i != 1 && i != 2 && i != 3 && i != 4 && i != 12)
                        {
                            keyBuffer += Enum.GetName(typeof(Keys), i);
                        }
                    }
                }
            }

            dispatcher.BeginInvoke((Action)delegate()
            {
                sender.Content = keyBuffer;
                RegisterKeybind("Overlay",keyBuffer, sender);
                
                if (sender.Name == "UpButton")
                    OverlayHandling.Up = (Keys)Enum.Parse(typeof(Keys), keyBuffer);

                if (sender.Name == "DownButton") 
                    OverlayHandling.Down = (Keys)Enum.Parse(typeof(Keys), keyBuffer);

                if (sender.Name == "LeftButton")
                    OverlayHandling.Left = (Keys)Enum.Parse(typeof(Keys), keyBuffer);

                if (sender.Name == "RightButton")
                    OverlayHandling.Right = (Keys)Enum.Parse(typeof(Keys), keyBuffer);

                if (sender.Name == "ConfirmButton")
                    OverlayHandling.Confirm = (Keys)Enum.Parse(typeof(Keys), keyBuffer);

                if (sender.Name == "LeaveButton")
                    OverlayHandling.Leave = (Keys)Enum.Parse(typeof(Keys), keyBuffer);
                    
                if (sender.Name == "VisibilityButton") 
                    OverlayHandling.OverlayVisibility = (Keys)Enum.Parse(typeof(Keys), keyBuffer);
            });
        }

        public static void UpdateKeybindingOnLaunch()
        {
            #region  First time open / somebody deleted file
            if (!File.Exists(PathToKeybindings) || new FileInfo(PathToKeybindings).Length == 0)
            {
                using (File.Create(PathToKeybindings)) {};
                IniData["Overlay"]["Up"] = OverlayHandling.Up.ToString();
                IniData["Overlay"]["Down"] = OverlayHandling.Down.ToString();
                IniData["Overlay"]["Left"] = OverlayHandling.Left.ToString();
                IniData["Overlay"]["Right"] = OverlayHandling.Right.ToString();
                IniData["Overlay"]["Confirm"] = OverlayHandling.Confirm.ToString();
                IniData["Overlay"]["Leave"] = OverlayHandling.Leave.ToString();
                IniData["Overlay"]["Visibility"] = OverlayHandling.OverlayVisibility.ToString();
                Parser.WriteFile(PathToKeybindings, IniData);
            }
            #endregion
            #region Parsing
            else
            {
                IniData = Parser.ReadFile(PathToKeybindings);

                Enum.TryParse(IniData["Overlay"]["Up"], out OverlayHandling.Up);
                Enum.TryParse(IniData["Overlay"]["Down"], out OverlayHandling.Down);
                Enum.TryParse(IniData["Overlay"]["Left"], out OverlayHandling.Left);
                Enum.TryParse(IniData["Overlay"]["Right"], out OverlayHandling.Right);
                Enum.TryParse(IniData["Overlay"]["Confirm"], out OverlayHandling.Confirm);
                Enum.TryParse(IniData["Overlay"]["Leave"], out OverlayHandling.Leave);
                Enum.TryParse(IniData["Overlay"]["Visibility"], out OverlayHandling.OverlayVisibility);
            }
            #endregion
            
            Keybinds.K.UpButton.Content = OverlayHandling.Up;
            Keybinds.K.DownButton.Content = OverlayHandling.Down;
            Keybinds.K.LeftButton.Content = OverlayHandling.Left;
            Keybinds.K.RightButton.Content = OverlayHandling.Right;
            Keybinds.K.ConfirmButton.Content = OverlayHandling.Confirm;
            Keybinds.K.LeaveButton.Content = OverlayHandling.Leave;
            Keybinds.K.VisibilityButton.Content = OverlayHandling.OverlayVisibility;
        }

        private static void RegisterKeybind(string part,string key, Button sender)
        {
            IniData[part][Regex.Replace(sender.Name, "Button$", "")] = key ;
            Parser.WriteFile(PathToKeybindings, IniData);
        } 
    }
}
