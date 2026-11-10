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
        Health = AddVariable(new SByteMemory("HP", baseAddress + 0x5C, memoryApi));
        Facing = AddVariable(new ByteMemory("Facing", baseAddress + 0x15, memoryApi));
    }

    public void Update(KeyboardInput input, IGuiApi gui)
    {
        if (input.GetKeyDown("T")) Health.ToggleFreeze();
        if (input.GetKey("KeypadAdd")) Health++;
        else if (input.GetKey("KeypadSubtract")) Health--;

        Update();
        DrawVariables(gui, new Vector2Int(60, 10));
    }
}