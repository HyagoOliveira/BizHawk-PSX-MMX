using System.Drawing;
using BizHawk.PSXMMX.Math;

namespace BizHawk.PSXMMX.Gameplay.Components.Collisions;

public sealed class TerrainBox(long baseAddress, GameObject gameObject) : AbstractBoundBox(baseAddress, gameObject)
{
    protected override Color GetBoxLineColor() => Color.WhiteSmoke;

    protected override RectInt CalculateWorldBounds(RectInt rawBounds, int x, int y, uint facing)
    {
        var centerX = x + (facing == 0 ? rawBounds.X : -rawBounds.X);
        var centerY = y + rawBounds.Y;
        return new RectInt(
            centerX - rawBounds.Width,
            centerY - rawBounds.Height,
            rawBounds.Width * 2,
            rawBounds.Height * 2
        );
    }
}
