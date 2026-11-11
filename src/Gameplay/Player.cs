using BizHawk.Client.Common;
using BizHawk.PSXMMX.Input;
using BizHawk.PSXMMX.Math;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Gameplay;

public sealed class Player : GameCharacter
{
    public KeyboardInput Input { get; }
    public Fixed16Vector2Memory PreviousPosition { get; }

    private const long BLOCK_SIZE = 0xE4;

    public Player(long baseAddress, ApiContainer apis, KeyboardInput input) : base(baseAddress, apis)
    {
        Input = input;

        // Initialize player-specific variables and components
        PreviousPosition = AddVariable(new Fixed16Vector2Memory(
            "Previous Position",
            BaseAddress + 0x18,
            offset: 0x04,
            APIs.Memory
        ));
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown("T")) Health.ToggleFreeze();
        if (Input.GetKey("KeypadAdd")) Health++;
        else if (Input.GetKey("KeypadSubtract")) Health--;

        DrawVariables(Vector2Int.One * 10);
    }
}