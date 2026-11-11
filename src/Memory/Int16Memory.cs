using BizHawk.Client.Common;

namespace BizHawk.PSXMMX.Memory;

public sealed class Int16Memory(string name, long address, IMemoryApi api) : AbstractMemory<short>(name, address, api)
{
    protected override short ReadValue() => (short)MemoryApi.ReadS16(Address, MEMORY_DOMAIN);
    protected override void WriteValue(short value) => MemoryApi.WriteS16(Address, value, MEMORY_DOMAIN);

    public static Int16Memory operator ++(Int16Memory memory)
    {
        if (memory is null) throw new ArgumentNullException(nameof(memory));
        if (memory.IsFrozen) return memory;

        var value = memory.Value < short.MaxValue ? (short)(memory.Value + 1) : short.MaxValue;
        memory.WriteValueAndUpdate(value);
        return memory;
    }

    public static Int16Memory operator --(Int16Memory memory)
    {
        if (memory is null) throw new ArgumentNullException(nameof(memory));
        if (memory.IsFrozen) return memory;

        var value = memory.Value > short.MinValue ? (short)(memory.Value - 1) : short.MinValue;
        memory.WriteValueAndUpdate(value);
        return memory;
    }
}
