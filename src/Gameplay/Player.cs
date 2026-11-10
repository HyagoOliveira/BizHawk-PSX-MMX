using BizHawk.Client.Common;
using BizHawk.PSXMMX.Gameplay.Components;
using BizHawk.PSXMMX.Input;
using BizHawk.PSXMMX.Math;

namespace BizHawk.PSXMMX.Gameplay;

public sealed class Player : GameCharacter
{
    public KeyboardInput Input { get; }

    private const long BLOCK_SIZE = 0xE4;

    public Player(long baseAddress, ApiContainer apis, KeyboardInput input) : base(baseAddress, apis)
    {
        Input = input;
        //TODO: Initialize player-specific variables and components here.
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown("T")) Health.ToggleFreeze();
        if (Input.GetKey("KeypadAdd")) Health++;
        else if (Input.GetKey("KeypadSubtract")) Health--;

        DrawVariables(new Vector2Int(60, 10));
    }
}