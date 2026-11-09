using BizHawk.Client.Common;

namespace BizHawk.PSXMMX.Memory;

public sealed class UIntMemory(string name, long address, IMemoryApi api) : AbstractMemory<uint>(name, address, api)
{
    protected override uint ReadValue() => MemoryApi.ReadU8(Address, MEMORY_DOMAIN);
    protected override void WriteValue(uint value) => MemoryApi.WriteU8(Address, value, MEMORY_DOMAIN);

    public static UIntMemory operator +(UIntMemory memory, uint amount)
    {
        if (memory is null) throw new ArgumentNullException(nameof(memory));
        if (memory.IsFrozen) return memory;

        var value = amount >= byte.MaxValue - memory.Value
            ? byte.MaxValue
            : memory.Value + amount;
        memory.WriteValueAndUpdate(value);
        return memory;
    }

    public static UIntMemory operator -(UIntMemory memory, uint amount)
    {
        if (memory is null) throw new ArgumentNullException(nameof(memory));
        if (memory.IsFrozen) return memory;

        var value = amount >= memory.Value ? 0 : memory.Value - amount;
        memory.WriteValueAndUpdate(value);
        return memory;
    }

    public static UIntMemory operator ++(UIntMemory memory) => memory + 1;
    public static UIntMemory operator --(UIntMemory memory) => memory - 1;
}