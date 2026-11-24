using BizHawk.Client.Common;
using BizHawk.PSXMMX.Core;

namespace BizHawk.PSXMMX.Gameplay;

public sealed class Shot : MainObject
{
    public Shot(long baseAddress, ApiContainer apis) :
        base(baseAddress, apis)
    {
    }

    protected override string GetInGameName() => Game.GetShotObjectName(Id.Value);

    protected override string GetProperties() =>
        $"{Position} | {Speed}\n" +
        $"{Animation}";
}