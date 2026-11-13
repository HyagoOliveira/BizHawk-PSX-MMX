using BizHawk.Client.Common;

namespace BizHawk.PSXMMX.Memory;

public sealed class BoolU8Memory(string name, long address, IMemoryApi api, bool hideDisplay = false) :
    AbstractMemory<bool>(name, address, api, hideDisplay)
{
    public override bool ReadValue() => MemoryApi.ReadU8(Address, DOMAIN) != 0;
    public override void WriteValue(bool value) => MemoryApi.WriteU8(Address, (byte)(value ? 1 : 0), DOMAIN);
}