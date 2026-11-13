using BizHawk.Client.Common;

namespace BizHawk.PSXMMX.Memory;

public sealed class ByteU8Memory(string name, long address, IMemoryApi api) : AbstractMemory<byte>(name, address, api)
{
    protected override byte ReadValue() => (byte)MemoryApi.ReadU8(Address, DOMAIN);
    protected override void WriteValue(byte value) => MemoryApi.WriteU8(Address, value, DOMAIN);
}