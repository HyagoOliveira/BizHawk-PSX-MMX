using BizHawk.Client.Common;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Gameplay;

public sealed class Enemy : GameCharacter
{
    public Enemy(long baseAddress, ApiContainer apis) : base(baseAddress, apis)
    {
    }
}