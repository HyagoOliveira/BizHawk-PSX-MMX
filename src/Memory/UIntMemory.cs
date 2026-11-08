using BizHawk.Client.Common;

namespace BizHawk.PSXMMX.Memory;

public sealed class UIntMemory(string name, long address, IMemoryApi api) : AbstractMemory<uint>(name, address, api)
{
    protected override uint ReadValue() => MemoryApi.ReadU8(Address);
    protected override void WriteValue(uint value) => MemoryApi.WriteU8(Address, value);

    public static UIntMemory operator +(UIntMemory memory, uint amount)
    {
        if (memory is null) throw new ArgumentNullException(nameof(memory));

        var value = amount >= byte.MaxValue - memory.Value
            ? byte.MaxValue
            : memory.Value + amount;
        memory.WriteValueAndUpdate(value);
        return memory;
    }

    public static UIntMemory operator -(UIntMemory memory, uint amount)
    {
        if (memory is null) throw new ArgumentNullException(nameof(memory));

        var value = amount >= memory.Value ? 0 : memory.Value - amount;
        memory.WriteValueAndUpdate(value);
        return memory;
    }

    public static UIntMemory operator ++(UIntMemory memory) => memory + 1;
    public static UIntMemory operator --(UIntMemory memory) => memory - 1;
}