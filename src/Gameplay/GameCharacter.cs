using BizHawk.Client.Common;
using BizHawk.PSXMMX.Gameplay.Components.Collisions;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Gameplay;

public class GameCharacter : GameObject
{
    // Variables
    public ByteMemory Facing { get; protected set; }
    public SByteMemory Health { get; protected set; }

    // Components
    public HurtBox HurtBox { get; }
    public TerrainBox TerrainBox { get; }

    public GameCharacter(long baseAddress, ApiContainer apis) : base(baseAddress, apis)
    {
        Facing = AddVariable(new ByteMemory("Dir", BaseAddress + 0x15, APIs.Memory));
        Health = AddVariable(new SByteMemory("HP", BaseAddress + 0x5C, APIs.Memory), displayOrder: 0);

        HurtBox = AddComponent(new HurtBox(BaseAddress + 0x54, this));
        TerrainBox = AddComponent(new TerrainBox(BaseAddress + 0x68, this));
    }

    public override void Draw()
    {
        base.Draw();
        HurtBox.Draw();
        TerrainBox.Draw();
    }
}