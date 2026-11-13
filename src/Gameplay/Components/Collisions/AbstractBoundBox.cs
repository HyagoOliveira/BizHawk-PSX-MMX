using System.Drawing;
using BizHawk.PSXMMX.Math;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Gameplay.Components.Collisions;

public abstract class AbstractBoundBox(long baseAddress, GameObject gameObject) : AbstractComponent(gameObject)
{
    public long BaseAddress { get; } = baseAddress;
    public RectInt? RawBounds { get; private set; } = null!;
    public RectInt? WorldBounds { get; private set; } = null!;

    private const string MEMORY_DOMAIN = AbstractMemory<int>.DOMAIN;

    protected abstract Color GetBoxLineColor();
    protected abstract RectInt CalculateWorldBounds(RectInt rawBounds, int x, int y, uint facing);

    public override void Update()
    {
        var memory = GameObject.APIs.Memory;
        var facing = memory.ReadU8(GameObject.BaseAddress + 0x15, MEMORY_DOMAIN);
        var pointer = memory.ReadU8(BaseAddress, MEMORY_DOMAIN)
            | (memory.ReadU8(BaseAddress + 1, MEMORY_DOMAIN) << 8)
            | (memory.ReadU8(BaseAddress + 2, MEMORY_DOMAIN) << 16)
            | (memory.ReadU8(BaseAddress + 3, MEMORY_DOMAIN) << 24);
        var offset = pointer & 0x001FFFFFu;
        var offsetX = memory.ReadS8(offset, MEMORY_DOMAIN);
        var offsetY = memory.ReadS8(offset + 1, MEMORY_DOMAIN);
        var width = memory.ReadU8(offset + 2, MEMORY_DOMAIN);
        var height = memory.ReadU8(offset + 3, MEMORY_DOMAIN);
        var positionAddress = GameObject.Position.Address;
        var x = (int)memory.ReadU8(positionAddress + 2, MEMORY_DOMAIN)
            | (memory.ReadS8(positionAddress + 3, MEMORY_DOMAIN) << 8);
        var y = (int)memory.ReadU8(positionAddress + 6, MEMORY_DOMAIN)
            | (memory.ReadS8(positionAddress + 7, MEMORY_DOMAIN) << 8);

        RawBounds = new RectInt(offsetX, offsetY, width, height);
        WorldBounds = CalculateWorldBounds(RawBounds.Value, x, y, facing);
    }

    public void Draw()
    {
        if (WorldBounds is not RectInt bounds) return;

        var screenPosition = Camera.Main.WorldToScreenPoint(bounds.GetPosition());
        var screenBounds = new RectInt(
            screenPosition.X,
            screenPosition.Y,
            bounds.Width,
            bounds.Height
        );

        GameObject.APIs.DrawBox(screenBounds, GetBoxLineColor());
    }
}