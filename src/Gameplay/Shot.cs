using BizHawk.Client.Common;
using BizHawk.PSXMMX.Core;
using BizHawk.PSXMMX.Math;

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

    protected override RectInt GetBounds()
    {
        if (DamagerBox.HasWorldBounds()) return DamagerBox.WorldBounds;
        if (CollisionBox.HasWorldBounds()) return CollisionBox.WorldBounds;
        if (DamageableBox.HasWorldBounds()) return DamageableBox.WorldBounds;

        return new RectInt(
            center: Position.Value.ToVector2Int(),
            size: Vector2Int.One * 20
        );
    }
}