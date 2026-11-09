using BizHawk.Client.Common;

namespace BizHawk.PSXMMX.Memory;

public sealed class SByteMemory(string name, long address, IMemoryApi api) : AbstractMemory<sbyte>(name, address, api)
{
    protected override sbyte ReadValue() => (sbyte)MemoryApi.ReadS8(Address, MEMORY_DOMAIN);
    protected override void WriteValue(sbyte value) => MemoryApi.WriteS8(Address, value, MEMORY_DOMAIN);

    public static SByteMemory operator ++(SByteMemory memory)
    {
        if (memory is null) throw new ArgumentNullException(nameof(memory));

        var value = memory.Value < sbyte.MaxValue ? (sbyte)(memory.Value + 1) : sbyte.MaxValue;
        memory.WriteValueAndUpdate(value);
        return memory;
    }

    public static SByteMemory operator --(SByteMemory memory)
    {
        if (memory is null) throw new ArgumentNullException(nameof(memory));

        var value = memory.Value > sbyte.MinValue ? (sbyte)(memory.Value - 1) : sbyte.MinValue;
        memory.WriteValueAndUpdate(value);
        return memory;
    }
}
