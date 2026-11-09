using BizHawk.Client.Common;
using BizHawk.PSXMMX.Gameplay.Components;

namespace BizHawk.PSXMMX.Gameplay;

public sealed class Player : GameCharacter
{

    public Player(IMemoryApi memoryApi, long baseAddress) : base(memoryApi, baseAddress)
    {
    }
}
