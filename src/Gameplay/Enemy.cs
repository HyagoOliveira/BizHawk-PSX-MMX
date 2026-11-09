using BizHawk.Client.Common;

namespace BizHawk.PSXMMX.Gameplay;

public sealed class Enemy : GameCharacter
{

    public Enemy(IMemoryApi memoryApi, long baseAddress) : base(memoryApi, baseAddress)
    {
    }
}