using System.Drawing;
using BizHawk.PSXMMX.Math;

namespace BizHawk.PSXMMX.Gameplay.Components.Collisions;

public sealed class BoundBox(long baseAddress, GameObject gameObject, Color lineColor) :
    AbstractBoundBox(baseAddress, gameObject, lineColor)
{
    protected override RectInt CalculateWorldBounds(RectInt rawBounds, int x, int y, uint facing)
    {
        var facingRight = (facing & 0x40) != 0;
        var anchorX = x + (facingRight ? -rawBounds.X : rawBounds.X);
        return new RectInt(
            facingRight ? anchorX - rawBounds.Width : anchorX,
            y + rawBounds.Y,
            rawBounds.Width,
            rawBounds.Height
        );
    }
}
