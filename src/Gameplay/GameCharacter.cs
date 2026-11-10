using BizHawk.Client.Common;
using BizHawk.PSXMMX.Input;
using BizHawk.PSXMMX.Math;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Gameplay;

public class GameCharacter : GameObject
{
    public SByteMemory Health { get; private set; }
    public ByteMemory Facing { get; private set; }

    public GameCharacter(IMemoryApi memoryApi, long baseAddress) : base(memoryApi, baseAddress)
    {
        Health = new SByteMemory("HP", baseAddress + 0x5C, memoryApi);
        Facing = new ByteMemory("Facing", baseAddress + 0x15, memoryApi);
    }

    public override void Update()
    {
        base.Update();
        Health.Update();
        Facing.Update();
    }

    public void Update(KeyboardInput input, IGuiApi gui)
    {
        if (input.GetKeyDown("T")) Health.ToggleFreeze();
        if (input.GetKey("KeypadAdd")) Health++;
        else if (input.GetKey("KeypadSubtract")) Health--;

        Update();
        Health.Draw(new Vector2Int(60, 10), gui);
        Facing.Draw(new Vector2Int(60, 30), gui);
    }
}
