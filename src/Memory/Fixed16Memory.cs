using BizHawk.Client.Common;

namespace BizHawk.PSXMMX.Memory;

/// <summary>
/// Reads and writes signed 16.16 fixed-point values, which represent fractional numbers
/// with a 32-bit integer: 16 bits represent the integer part and 16 bits represent the
/// fractional part.
/// </summary>
public sealed class Fixed16Memory(string name, long address, IMemoryApi api) : AbstractMemory<float>(name, address, api)
{
    public const float POINT_SCALE = 65_536f; // 0x10000 (2^16)

    protected override float ReadValue() => MemoryApi.ReadS32(Address, MEMORY_DOMAIN) / POINT_SCALE;
    protected override void WriteValue(float value) => MemoryApi.WriteS32(Address, checked((int)(value * POINT_SCALE)), MEMORY_DOMAIN);
}
