using BizHawk.Client.Common;

namespace BizHawk.PSXMMX.Memory;

public sealed class UIntU16Memory(string name, long address, IMemoryApi api, bool hideDisplay = false) :
    AbstractMemory<uint>(name, address, api, hideDisplay)
{
    public override uint ReadValue() => MemoryApi.ReadU16(Address, DOMAIN);
    public override void WriteValue(uint value) => MemoryApi.WriteU16(Address, value, DOMAIN);

    public static UIntU16Memory operator +(UIntU16Memory memory, uint amount)
    {
        if (memory.IsFrozen) return memory;

        var value = amount >= ushort.MaxValue - memory.Value ?
            ushort.MaxValue :
            memory.Value + amount;
        memory.ForceToWriteValue(value);
        return memory;
    }

    public static UIntU16Memory operator -(UIntU16Memory memory, uint amount)
    {
        if (memory.IsFrozen) return memory;

        var value = amount >= memory.Value ? 0 : memory.Value - amount;
        memory.ForceToWriteValue(value);
        return memory;
    }

    public static UIntU16Memory operator ++(UIntU16Memory memory) => memory + 1;
    public static UIntU16Memory operator --(UIntU16Memory memory) => memory - 1;
}