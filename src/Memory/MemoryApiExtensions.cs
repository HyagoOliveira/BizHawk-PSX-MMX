using BizHawk.Client.Common;

namespace BizHawk.PSXMMX.Memory;

public static class MemoryApiExtensions
{
    /// <summary>
    /// Reads a signed 32-bit Q16.16 fixed-point value from the specified memory domain
    /// and converts it to a floating-point value by dividing the stored integer by 2^16
    /// (65,536). In Q16.16, the lower 16 bits represent the fractional part.
    /// </summary>
    public static float ReadQ16(this IMemoryApi memory, long address, string domain) =>
        memory.ReadS32(address, domain) / 65536f;
}
