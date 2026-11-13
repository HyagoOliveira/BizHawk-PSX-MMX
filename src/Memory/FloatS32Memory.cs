using BizHawk.Client.Common;

namespace BizHawk.PSXMMX.Memory;

/// <summary>
/// Reads and writes a signed 16.16 fixed-point float stored in a 32-bit integer.
/// </summary>
/// <remarks>
/// The first 16 bits represent the integer part; the last 16 bits represent the fractional part.
/// </remarks>
public sealed class FloatS32Memory(string name, long address, IMemoryApi api) : AbstractMemory<float>(name, address, api)
{
    public const float POINT_SCALE = 65_536f; // 0x10000 (2^16)

    protected override float ReadValue() => MemoryApi.ReadS32(Address, DOMAIN) / POINT_SCALE;
    protected override void WriteValue(float value) => MemoryApi.WriteS32(Address, checked((int)(value * POINT_SCALE)), DOMAIN);
}
