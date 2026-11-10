using BizHawk.Client.Common;
using BizHawk.PSXMMX.Math;

namespace BizHawk.PSXMMX.Memory;

/// <summary>
/// Reads and writes signed 16.16 Vector2 fixed-point values, which represent fractional numbers
/// with a 32-bit integer: 16 bits represent the integer part and 16 bits represent the
/// fractional part.
/// </summary>
public sealed class Fixed16Vector2Memory(string name, long address, long offset, IMemoryApi api) : AbstractMemory<Vector2>(name, address, api)
{
    public long Offset { get; } = offset;

    protected override Vector2 ReadValue() => new Vector2(
        MemoryApi.ReadS32(GetXAddress(), MEMORY_DOMAIN),
        MemoryApi.ReadS32(GetYAddress(), MEMORY_DOMAIN)
    ) / Fixed16Memory.POINT_SCALE;

    protected override void WriteValue(Vector2 value)
    {
        MemoryApi.WriteS32(GetXAddress(), ToInt(value.X), MEMORY_DOMAIN);
        MemoryApi.WriteS32(GetYAddress(), ToInt(value.Y), MEMORY_DOMAIN);
    }

    private int ToInt(float value) => checked((int)(value * Fixed16Memory.POINT_SCALE));

    private long GetXAddress() => Address;
    private long GetYAddress() => Address + Offset;
}