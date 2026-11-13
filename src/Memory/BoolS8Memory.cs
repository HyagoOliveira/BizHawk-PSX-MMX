using BizHawk.Client.Common;

namespace BizHawk.PSXMMX.Memory;

public sealed class BoolS8Memory(string name, long address, IMemoryApi api) : AbstractMemory<bool>(name, address, api)
{
    protected override bool ReadValue() => MemoryApi.ReadS8(Address, DOMAIN) != 0;
    protected override void WriteValue(bool value) => MemoryApi.WriteS8(Address, (sbyte)(value ? 1 : 0), DOMAIN);
}