using BizHawk.Client.Common;

namespace BizHawk.PSXMMX.Memory;

public sealed class SByteS8Memory(string name, long address, IMemoryApi api) : AbstractMemory<sbyte>(name, address, api)
{
    protected override sbyte ReadValue() => (sbyte)MemoryApi.ReadS8(Address, DOMAIN);
    protected override void WriteValue(sbyte value) => MemoryApi.WriteS8(Address, value, DOMAIN);

    public static SByteS8Memory operator ++(SByteS8Memory memory)
    {
        if (memory is null) throw new ArgumentNullException(nameof(memory));
        if (memory.IsFrozen) return memory;

        var value = memory.Value < sbyte.MaxValue ? (sbyte)(memory.Value + 1) : sbyte.MaxValue;
        memory.ForceToWriteValue(value);
        return memory;
    }

    public static SByteS8Memory operator --(SByteS8Memory memory)
    {
        if (memory is null) throw new ArgumentNullException(nameof(memory));
        if (memory.IsFrozen) return memory;

        var value = memory.Value > sbyte.MinValue ? (sbyte)(memory.Value - 1) : sbyte.MinValue;
        memory.ForceToWriteValue(value);
        return memory;
    }
}
