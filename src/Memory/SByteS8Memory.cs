using BizHawk.Client.Common;

namespace BizHawk.PSXMMX.Memory;

public sealed class SByteS8Memory(string name, long address, IMemoryApi api, bool hideDisplay = false) :
    AbstractMemory<sbyte>(name, address, api, hideDisplay)
{
    public const sbyte MIN_VALUE = 0;
    public const sbyte MAX_VALUE = sbyte.MaxValue;

    public override sbyte ReadValue() => (sbyte)MemoryApi.ReadS8(Address, DOMAIN);
    public override void WriteValue(sbyte value) => MemoryApi.WriteS8(Address, value, DOMAIN);

    public static SByteS8Memory operator ++(SByteS8Memory memory)
    {
        if (memory.IsFrozen) return memory;

        var value = memory.Value < MAX_VALUE ? (sbyte)(memory.Value + 1) : MAX_VALUE;
        memory.ForceToWriteValue(value);
        return memory;
    }

    public static SByteS8Memory operator --(SByteS8Memory memory)
    {
        if (memory.IsFrozen) return memory;

        var value = memory.Value > MIN_VALUE ? (sbyte)(memory.Value - 1) : MIN_VALUE;
        memory.ForceToWriteValue(value);
        return memory;
    }
}
