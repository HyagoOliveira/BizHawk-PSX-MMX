using BizHawk.Client.Common;
using BizHawk.PSXMMX.Input;
using BizHawk.PSXMMX.Math;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Gameplay;

public sealed class Player : GameCharacter
{
    public KeyboardInput Input { get; }

    // Variables

    public Player(long baseAddress, ApiContainer apis, KeyboardInput input) : base(baseAddress, apis)
    {
        Input = input;
        IsDrawEnabled = true;
    }

    public override void Update()
    {
        base.Update();
        UpdateHealthByInput(); //TODO remove later
    }

    protected override Vector2Int GetVarsPosition() => new(
        2,
        Camera.SCREEN_SIZE.Y - 90
    );

    private void UpdateHealthByInput()
    {
        if (Input.GetKeyDown("T")) Health.ToggleFreeze();
        if (Input.GetKey("KeypadAdd")) Health++;
        else if (Input.GetKey("KeypadSubtract")) Health--;
    }
}