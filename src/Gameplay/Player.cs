using BizHawk.Client.Common;
using BizHawk.PSXMMX.Gameplay.Components;

namespace BizHawk.PSXMMX.Gameplay;

public sealed class Player : GameCharacter
{
    private const long size = 0xE4;

    public Player(IMemoryApi memoryApi, long baseAddress) : base(memoryApi, baseAddress)
    {
    }
}
