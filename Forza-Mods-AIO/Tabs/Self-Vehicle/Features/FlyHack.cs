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
    private static float _originalGrav;
    private static float _rotSpeed = 1f;
    private static float _moveSpeed = 1f;
    
    public static void SetRotSpeed(double? newValue) => _rotSpeed = ToSingle(newValue);
    public static void SetMoveSpeed(double? newValue) => _moveSpeed = ToSingle(newValue);
    

    public static void Run()
    {
        Shp.Dispatcher.Invoke(() => Shp.GravitySetSwitch.IsEnabled = !Shp.GravitySetSwitch.IsEnabled);
        if (!Shp.Dispatcher.Invoke(() => Shp.FlyHackSwitch.IsOn))
        {
            Gravity = _originalGrav;
            return;
        }

        Shp.Dispatcher.Invoke(() => Shp.GravitySetSwitch.IsOn = false);
        
        var count = 0;
        while (_originalGrav == 0 && count < 50)
        {
            ++count;
            _originalGrav = Gravity;
            Task.Delay(5).Wait();
        }
        
        Gravity = 0f;
        
        bool aDown = false, dDown = false;
        bool wDown = false, sDown = false, shiftDown = false, controlDown = false;

        while (true)
        {
            if (!IsProcessValid() || !Shp.Dispatcher.Invoke(() => Shp.FlyHackSwitch.IsOn))
            {
                return;
            }

            var cleanVector = new Vector3(0);
            LinearVelocity = cleanVector;
            AngularVelocity = cleanVector;
                
            if (Mw.Gvp.Process.MainWindowHandle != GetForegroundWindow())
            {
                Task.Delay(10).Wait();
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
                HandleRotation(aDown);
            }

            HandleMovement(wDown, sDown, shiftDown, controlDown);
            Task.Delay(10).Wait();
        }
    }
    
    private static void HandleRotation(bool aDown)
    {
        var angle = (aDown ? -1 : 1) * _rotSpeed / 25;
        var rotationQuaternion = Quaternion.CreateFromAxisAngle(Vector3.UnitY, angle);
        var rotationMatrix = Matrix4x4.CreateFromQuaternion(rotationQuaternion);
        Rotation *= rotationMatrix;
    }

    private static Vector3 _flyHackVelocity = Vector3.Zero;
    private const float FrictionFactor = 0.95f;
    private const float FlyHackSpeed = 1.25f;
    
    private static void HandleMovement(bool wDown, bool sDown, bool shiftDown, bool controlDown)
    {
        float xComp = 0f, zComp = 0f;

        if (wDown)
        {
            HandleForwardMovement(ref xComp, ref zComp);
        }
        else if (sDown)
        {
            HandleBackwardMovement(ref xComp, ref zComp);
        }

        var speed = _moveSpeed * FlyHackSpeed;
        var newVelocity = new Vector3(speed * xComp, 0f, speed * zComp);

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
            _flyHackVelocity += new Vector3(0f, speed, 0f);
        }
        else if (controlDown)
        {
            _flyHackVelocity += new Vector3(0f, -speed, 0f);
        }
        
        var maxSpeed = _moveSpeed * 2;
        var currentSpeed = _flyHackVelocity.Length();
        if (currentSpeed > maxSpeed)
        {
            _flyHackVelocity *= maxSpeed / currentSpeed;
        }

        Position += _flyHackVelocity;
    }

    private static void HandleForwardMovement(ref float xComp, ref float zComp)
    {
        var angle = GetAngle();
        switch (angle)
        {
            case < 90:
            {
                xComp = -ToSingle(Sin(PI * angle / 180));
                zComp = ToSingle(Cos(PI * angle / 180));
                break;
            }
            case > 90 and < 180:
            {
                xComp = -ToSingle(Sin(PI * (180 - angle) / 180));
                zComp = -ToSingle(Cos(PI * (180 - angle) / 180));
                break;
            }
            case > 180 and < 270:
            {
                xComp = ToSingle(Cos(PI * (270 - angle) / 180));
                zComp = -ToSingle(Sin(PI * (270 - angle) / 180));
                break;
            }
            case > 270:
            {
                xComp = ToSingle(Sin(PI * (360 - angle) / 180));
                zComp = ToSingle(Cos(PI * (360 - angle) / 180));
                break;
            }
        }
    }

    private static void HandleBackwardMovement(ref float xComp, ref float zComp)
    {
        var angle = GetAngle();
        switch (angle)
        {
            case < 90:
            {
                xComp = ToSingle(Sin(PI * angle / 180));
                zComp = -ToSingle(Cos(PI * angle / 180));
                break;
            }
            case > 90 and < 180:
            {
                xComp = ToSingle(Sin(PI * (180 - angle) / 180));
                zComp = ToSingle(Cos(PI * (180 - angle) / 180));
                break;
            }
            case > 180 and < 270:
            {
                xComp = -ToSingle(Cos(PI * (270 - angle) / 180));
                zComp = ToSingle(Sin(PI * (270 - angle) / 180));
                break;
            }
            case > 270:
            {
                xComp = -ToSingle(Sin(PI * (360 - angle) / 180));
                zComp = -ToSingle(Cos(PI * (360 - angle) / 180));
                break;
            }
        }
    }

    private static float GetAngle()
    {
        return ToSingle(Atan2(Rotation.M13, Rotation.M11) * (180 / PI));
    }
}
