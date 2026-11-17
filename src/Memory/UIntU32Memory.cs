using BizHawk.Client.Common;

namespace BizHawk.PSXMMX.Memory;

public sealed class UIntU32Memory(string name, long address, IMemoryApi api, bool hideDisplay = false) :
    AbstractMemory<uint>(name, address, api, hideDisplay)
{
    public override uint ReadValue() => MemoryApi.ReadU32(Address, DOMAIN);
    public override void WriteValue(uint value) => MemoryApi.WriteU32(Address, value, DOMAIN);

    public static UIntU32Memory operator +(UIntU32Memory memory, uint amount)
    {
        if (memory.IsFrozen) return memory;

        var value = amount >= uint.MaxValue - memory.Value ?
            uint.MaxValue :
            memory.Value + amount;
        memory.ForceToWriteValue(value);
        return memory;
    }

    public static UIntU32Memory operator -(UIntU32Memory memory, uint amount)
    {
        if (memory.IsFrozen) return memory;

        var value = amount >= memory.Value ? 0 : memory.Value - amount;
        memory.ForceToWriteValue(value);
        return memory;
    }

    public static UIntU32Memory operator ++(UIntU32Memory memory) => memory + 1;
    public static UIntU32Memory operator --(UIntU32Memory memory) => memory - 1;
}