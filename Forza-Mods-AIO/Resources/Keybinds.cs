using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Forza_Mods_AIO.Overlay;
using Forza_Mods_AIO.Tabs.Keybindings.DropDownTabs;

using Button = System.Windows.Controls.Button;

namespace Forza_Mods_AIO.Resources;

public class Keybinds
{
    public void Grab()
    {
        if (!IsClicked || ClickedButton == null)
        {
            return;
        }
        
        IsClicked = false;
        var oldKey = ClickedButton.Content;

        string keyBuffer;
        do
        {
            keyBuffer = Enum.GetValues(typeof(Keys))
                .Cast<Keys>()
                .Where(i => i != Keys.None &&
                            i != Keys.LButton &&
                            i != Keys.RButton &&
                            i != Keys.MButton &&
                            i != Keys.XButton1 &&
                            i != Keys.XButton2)
                .FirstOrDefault(KeyStates.IsKeyPressed)
                .ToString();
            
            Task.Delay(5).Wait();
        } 
        while (string.IsNullOrEmpty(keyBuffer));

        var key = (Keys)Enum.Parse(typeof(Keys), keyBuffer);
        var fields = typeof(OverlayHandling).GetFields().Concat(typeof(HandlingKeybindings).GetFields());
        
        foreach (var field in fields)
        {
            if (field.Name != ClickedButton.Name.Replace("Button", string.Empty))
            {
                continue;
            }

            if (field.DeclaringType == typeof(OverlayHandling))
            {
                field.SetValue(Overlay.Overlay.Oh, key);
                OverlayKeybindings.SaveKeybinds();
            }
            else if (field.DeclaringType == typeof(HandlingKeybindings))
            {
                field.SetValue(HandlingKeybindings.Hk, key);
                HandlingKeybindings.Hk.SaveKeybindings();
            }

            ClickedButton.Content = keyBuffer;
            return;
        }
        
        ClickedButton.Content = oldKey;
    }

    public bool IsClicked;
    public Button? ClickedButton;
}