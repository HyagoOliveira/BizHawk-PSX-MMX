using BizHawk.Client.Common;
using BizHawk.PSXMMX.Gameplay.Components.Collisions;
using BizHawk.PSXMMX.Math;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Gameplay;

public class GameCharacter : GameObject
{
    // Variables
    public BoolU8Memory IsFacingRight { get; protected set; }
    public SByteS8Memory Health { get; protected set; }

    // Components
    public HurtBox HurtBox { get; }
    public TerrainBox TerrainBox { get; }

    public GameCharacter(long baseAddress, ApiContainer apis) : base(baseAddress, apis)
    {
        IsFacingRight = AddVariable(new BoolU8Memory("Is Right", BaseAddress + 0x15, APIs.Memory));
        Health = AddVariable(new SByteS8Memory("HP", BaseAddress + 0x5C, APIs.Memory), displayOrder: 0);

        HurtBox = AddComponent(new HurtBox(BaseAddress + 0x54, this));
        TerrainBox = AddComponent(new TerrainBox(BaseAddress + 0x68, this));
    }

    public override void Draw()
    {
        base.Draw();
        HurtBox.Draw();
        TerrainBox.Draw();

        DrawVariables(GetVarsPosition());
    }

    protected virtual Vector2Int GetVarsPosition() => Vector2Int.Zero;
}