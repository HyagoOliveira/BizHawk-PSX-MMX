using BizHawk.Client.Common;
using BizHawk.PSXMMX.Math;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Gameplay;

public sealed class Enemy : GameCharacter
{
    public Enemy(long baseAddress, ApiContainer apis) : base(baseAddress, apis)
    {
    }

    public override void Update()
    {
        base.Update();
        UpdateName();
    }

    protected override Vector2Int GetVarsPosition() => DamageableBox.WorldBounds.HasValue ?
        Camera.Main.WorldToScreenPoint(DamageableBox.WorldBounds.Value.GetTopRight()) :
        Vector2Int.Zero;

    private void UpdateName()
    {
        if (!IsVisible.Value) return;
        Name = GameMemoryAddress.GetMainObjectName(Id.Value) + $" (Id: {Id.Value})";
    }
}