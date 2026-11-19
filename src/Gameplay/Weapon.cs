using BizHawk.Client.Common;
using BizHawk.PSXMMX.Math;

namespace BizHawk.PSXMMX.Gameplay;

public sealed class Weapon : MainObject
{
    public Weapon(long baseAddress, ApiContainer apis) :
        base(baseAddress, apis)
    {
    }

    public override void DrawProperties()
    {
        var position = GetTopRightPosition();
        var properties =
            $"{State} | {Substate}\n" +
            $"{Health} | {InvincibilityTimer} | {Damage}\n" +
            $"{Speed} | {Gravity}\n" +
            $"{Position} | {PreviousPosition}\n" +
            $"{Animation}";
        APIs.DrawText(position, properties);
    }
}