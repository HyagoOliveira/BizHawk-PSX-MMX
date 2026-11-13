using BizHawk.Client.Common;

namespace BizHawk.PSXMMX.Memory;

public sealed class ByteU8Memory(string name, long address, IMemoryApi api, bool hideDisplay = false) :
    AbstractMemory<byte>(name, address, api, hideDisplay)
{
    public override byte ReadValue() => (byte)MemoryApi.ReadU8(Address, DOMAIN);
    public override void WriteValue(byte value) => MemoryApi.WriteU8(Address, value, DOMAIN);
}