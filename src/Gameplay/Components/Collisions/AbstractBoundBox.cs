using System.Drawing;
using BizHawk.PSXMMX.Math;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Gameplay.Components.Collisions;

public abstract class AbstractBoundBox : AbstractComponent
{
    public long BaseAddress { get; }
    public Color LineColor { get; }
    public RectInt? RawBounds { get; private set; } = null!;
    public RectInt? WorldBounds { get; private set; } = null!;

    public const string DOMAIN = GameMemoryAddress.DOMAIN;

    public AbstractBoundBox(long baseAddress, GameObject gameObject, Color lineColor) : base(gameObject)
    {
        LineColor = lineColor;
        BaseAddress = baseAddress;
    }

    protected abstract RectInt CalculateWorldBounds(RectInt rawBounds, int x, int y, uint facing);

    public override void Update()
    {
        var memory = GameObject.APIs.Memory;
        var facing = memory.ReadU8(GameObject.BaseAddress + 0x15, DOMAIN);
        var pointer = memory.ReadU8(BaseAddress, DOMAIN)
            | (memory.ReadU8(BaseAddress + 1, DOMAIN) << 8)
            | (memory.ReadU8(BaseAddress + 2, DOMAIN) << 16)
            | (memory.ReadU8(BaseAddress + 3, DOMAIN) << 24);
        var offset = pointer & 0x001FFFFFu;
        var offsetX = memory.ReadS8(offset, DOMAIN);
        var offsetY = memory.ReadS8(offset + 1, DOMAIN);
        var width = memory.ReadU8(offset + 2, DOMAIN);
        var height = memory.ReadU8(offset + 3, DOMAIN);
        var positionAddress = GameObject.Position.Address;
        var x = (int)memory.ReadU8(positionAddress + 2, DOMAIN)
            | (memory.ReadS8(positionAddress + 3, DOMAIN) << 8);
        var y = (int)memory.ReadU8(positionAddress + 6, DOMAIN)
            | (memory.ReadS8(positionAddress + 7, DOMAIN) << 8);

        RawBounds = new RectInt(offsetX, offsetY, width, height);
        WorldBounds = CalculateWorldBounds(RawBounds.Value, x, y, facing);
    }

    public override void Draw()
    {
        if (WorldBounds is not RectInt bounds) return;

        var screenPosition = Camera.Main.WorldToScreenPoint(bounds.GetPosition());
        var screenBounds = new RectInt(
            screenPosition.X,
            screenPosition.Y,
            bounds.Width,
            bounds.Height
        );

        GameObject.APIs.DrawBox(screenBounds, LineColor);
    }
}