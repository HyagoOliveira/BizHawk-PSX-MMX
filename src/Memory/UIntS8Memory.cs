using BizHawk.Client.Common;

namespace BizHawk.PSXMMX.Memory;

public sealed class UIntS8Memory(string name, long address, IMemoryApi api, bool hideDisplay = false) :
    AbstractMemory<uint>(name, address, api, hideDisplay)
{
    public override uint ReadValue() => MemoryApi.ReadU8(Address, DOMAIN);
    public override void WriteValue(uint value) => MemoryApi.WriteU8(Address, value, DOMAIN);

    public static UIntS8Memory operator +(UIntS8Memory memory, uint amount)
    {
        if (memory.IsFrozen) return memory;

        var value = amount >= byte.MaxValue - memory.Value ?
            byte.MaxValue :
            memory.Value + amount;
        memory.ForceToWriteValue(value);
        return memory;
    }

    public static UIntS8Memory operator -(UIntS8Memory memory, uint amount)
    {
        if (memory.IsFrozen) return memory;

        var value = amount >= memory.Value ? 0 : memory.Value - amount;
        memory.ForceToWriteValue(value);
        return memory;
    }

    public static UIntS8Memory operator ++(UIntS8Memory memory) => memory + 1;
    public static UIntS8Memory operator --(UIntS8Memory memory) => memory - 1;
}