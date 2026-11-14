using BizHawk.Client.Common;

namespace BizHawk.PSXMMX.Gameplay;

public sealed class Enemy : MainObject
{
    public Enemy(long baseAddress, ApiContainer apis) : base(baseAddress, apis)
    {
    }
}