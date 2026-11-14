using BizHawk.Client.Common;
using BizHawk.PSXMMX.Input;
using BizHawk.PSXMMX.Math;

namespace BizHawk.PSXMMX.Gameplay;

public sealed class Player : MainObject
{
    public KeyboardInput Input { get; }

    // Variables

    public Player(long baseAddress, ApiContainer apis, KeyboardInput input) : base(baseAddress, apis)
    {
        Input = input;
        ShowProperties = true;
    }

    public override void Update()
    {
        base.Update();
        UpdateHealthByInput(); //TODO remove later
    }

    protected override void UpdateName() => Name = "Player";

    protected override Vector2Int GetPropertiesPosition() => new(
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