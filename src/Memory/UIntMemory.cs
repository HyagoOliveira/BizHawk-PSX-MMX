using BizHawk.Client.Common;

namespace BizHawk.PSXMMX.Memory;

public sealed class UIntMemory(string name, long address, IMemoryApi api) : AbstractMemory<uint>(name, address, api)
{
    protected override uint ReadValue() => MemoryApi.ReadU8(Address);
    protected override void WriteValue(uint value) => MemoryApi.WriteU8(Address, value);
}