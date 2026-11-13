using BizHawk.Client.Common;
using BizHawk.PSXMMX.Math;

namespace BizHawk.PSXMMX.Memory;

/// <summary>
/// Reads and writes a signed 16.16 fixed-point Vector2 stored in a 32-bit integer.
/// </summary>
/// <remarks>
/// For each axis, the first 16 bits represent the integer part; the last 16 bits represent the fractional part.
/// </remarks>
public sealed class Vector2S32Memory : AbstractMemory<Vector2>
{
    public long XAddress { get; }
    public long YAddress { get; }

    public Vector2S32Memory(string name, long xAddress, long yAddress, IMemoryApi api) :
        base(name, xAddress, api)
    {
        XAddress = xAddress;
        YAddress = yAddress;
    }

    public override Vector2 ReadValue() => new Vector2(
        MemoryApi.ReadS32(XAddress, DOMAIN),
        MemoryApi.ReadS32(YAddress, DOMAIN)
    ) / FloatS32Memory.POINT_SCALE;

    public override void WriteValue(Vector2 value)
    {
        MemoryApi.WriteS32(XAddress, ToInt(value.X), DOMAIN);
        MemoryApi.WriteS32(YAddress, ToInt(value.Y), DOMAIN);
    }

    private int ToInt(float value) => checked((int)(value * FloatS32Memory.POINT_SCALE));
}