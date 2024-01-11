using System.Numerics;
using System.Windows.Forms;
using System.Threading.Tasks;

using static System.Math;
using static System.Convert;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Resources.KeyStates;
using static Forza_Mods_AIO.Resources.DllImports;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.Entities.CarEntity;
using static Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs.HandlingPage;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.Features;

public abstract class FlyHack : FeatureBase
{
    public static void Run()
    {
        if (!IsProcessValid())
        {
            return;
        }
        
        bool aDown = false, dDown = false;
        bool wDown = false, sDown = false, shiftDown = false, controlDown = false;

        while (true)
        {
            if (!Shp.Dispatcher.Invoke(() => Shp.FlyHackSwitch.IsOn))
            {
                break;
            }

            LinearVelocity = new Vector3 { X = 0f, Z = 0f, Y = 0f };
            AngularVelocity = new Vector3 { X = 0f, Z = 0f, Y = 0f };
                
            if (Mw.Gvp.Process.MainWindowHandle != GetForegroundWindow())
            {
                Task.Delay(25).Wait();
                continue;
            }

            UpdateKeyState(Keys.W, ref wDown);
            UpdateKeyState(Keys.S, ref sDown);
            UpdateKeyState(Keys.A, ref aDown);
            UpdateKeyState(Keys.D, ref dDown);
            UpdateKeyState(Keys.LShiftKey, ref shiftDown);
            UpdateKeyState(Keys.LControlKey, ref controlDown);

            if (aDown || dDown)
            {
                var flyHackRotSpeed = 1f;
                Shp.Dispatcher.Invoke(() => flyHackRotSpeed = ToSingle(Shp.FlyHackRotSpeedNum.Value / 2));
                HandleRotation(flyHackRotSpeed, aDown);
            }

            var angle = (float)((float)Atan2(Rotation.M13, Rotation.M11) * (180 / PI));
            if (angle < 0)
            {
                angle += 360;
            }
                    
            var flyHackMoveSpeed = Shp.Dispatcher.Invoke(() => ToSingle(Shp.FlyHackMoveSpeedNum.Value / 2));
            HandleMovement(angle, flyHackMoveSpeed, wDown, sDown, shiftDown, controlDown);
                
            Task.Delay(10).Wait();
        }
    }
    
    private static void HandleRotation(float speed, bool aDown)
    {
        var angle = (aDown ? -1 : 1) * speed / 25;
        var rotationQuaternion = Quaternion.CreateFromAxisAngle(Vector3.UnitY, angle);
        var rotationMatrix = Matrix4x4.CreateFromQuaternion(rotationQuaternion);
        Rotation *= rotationMatrix;
    }

    private static Vector3 _flyHackVelocity = Vector3.Zero;
    private const float FrictionFactor = 0.95f;
    private const float FlyHackSpeed = 1.25f;
    
    private static void HandleMovement(float angle, float speed, bool wDown, bool sDown, bool shiftDown, bool controlDown)
    {
        float xComp = 0f, zComp = 0f;

        if (wDown)
        {
            switch (angle)
            {
                case < 90:
                {
                    xComp = -(float)Sin(PI * angle / 180);
                    zComp = (float)Cos(PI * angle / 180);
                    break;
                }
                case > 90 and < 180:
                {
                    xComp = -(float)Sin(PI * (180 - angle) / 180);
                    zComp = -(float)Cos(PI * (180 - angle) / 180);
                    break;
                }
                case > 180 and < 270:
                {
                    xComp = (float)Cos(PI * (270 - angle) / 180);
                    zComp = -(float)Sin(PI * (270 - angle) / 180);
                    break;
                }
                case > 270:
                {
                    xComp = (float)Sin(PI * (360 - angle) / 180);
                    zComp = (float)Cos(PI * (360 - angle) / 180);
                    break;
                }
            }
        }
        else if (sDown)
        {
            switch (angle)
            {
                case < 90:
                {
                    xComp = (float)Sin(PI * angle / 180);
                    zComp = -(float)Cos(PI * angle / 180);
                    break;
                }
                case > 90 and < 180:
                {
                    xComp = (float)Sin(PI * (180 - angle) / 180);
                    zComp = (float)Cos(PI * (180 - angle) / 180);
                    break;
                }
                case > 180 and < 270:
                {
                    xComp = -(float)Cos(PI * (270 - angle) / 180);
                    zComp = (float)Sin(PI * (270 - angle) / 180);
                    break;
                }
                case > 270:
                {
                    xComp = -(float)Sin(PI * (360 - angle) / 180);
                    zComp = -(float)Cos(PI * (360 - angle) / 180);
                    break;
                }
            }
        }
        
        var newVelocity = new Vector3(speed * FlyHackSpeed * xComp, 0f, speed * FlyHackSpeed * zComp);

        if (wDown || sDown)
        {
            _flyHackVelocity += newVelocity;
        }
        else
        {
            _flyHackVelocity *= FrictionFactor;
        }
        
        if (shiftDown)
        {
            _flyHackVelocity += new Vector3(0f, speed * FlyHackSpeed, 0f);
        }
        else if (controlDown)
        {
            _flyHackVelocity += new Vector3(0f, -speed * FlyHackSpeed, 0f);
        }
        
        var maxSpeed = speed * 4;
        var currentSpeed = _flyHackVelocity.Length();
        if (currentSpeed > maxSpeed)
        {
            _flyHackVelocity *= maxSpeed / currentSpeed;
        }

        Position += _flyHackVelocity;
    }
}