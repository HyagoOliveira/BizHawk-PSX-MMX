using BizHawk.Client.Common;
using BizHawk.PSXMMX.Core;
using BizHawk.PSXMMX.Math;

namespace BizHawk.PSXMMX.Gameplay;

public sealed class Weapon : MainObject
{
    public Weapon(long baseAddress, ApiContainer apis) :
        base(baseAddress, apis)
    {
    }

    protected override string GetInGameName() => Game.GetWeaponObjectName(Id.Value);

    protected override string GetProperties() =>
        $"{State} | {Substate}\n" +
        $"{Speed} | {Gravity}\n" +
        $"{Position} | {PreviousPosition}\n" +
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