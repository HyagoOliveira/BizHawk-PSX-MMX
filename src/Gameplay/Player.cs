using BizHawk.Client.Common;
using BizHawk.PSXMMX.Input;
using BizHawk.PSXMMX.Math;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Gameplay;

public sealed class Player : GameCharacter
{
    public KeyboardInput Input { get; }

    // Variables
    public Vector2S32Memory PreviousPosition { get; }

    private const int VARS_COUNT = 4;
    private static readonly Vector2Int VARS_POSITION = new(2, Camera.SCREEN_SIZE.Y - VARS_COUNT * 12);

    public Player(long baseAddress, ApiContainer apis, KeyboardInput input) : base(baseAddress, apis)
    {
        Input = input;

        PreviousPosition = AddVariable(
            new Vector2S32Memory(
                "Prev Pos",
                BaseAddress + 0x18,
                offset: 0x04,
                APIs.Memory
            ),
            displayOrder: 1
        );
    }

    public override void Update()
    {
        base.Update();
        UpdateHealthByInput(); //TODO remove later
    }

    public override void Draw()
    {
        base.Draw();
        DrawVariables(VARS_POSITION);
    }

    private void UpdateHealthByInput()
    {
        if (Input.GetKeyDown("T")) Health.ToggleFreeze();
        if (Input.GetKey("KeypadAdd")) Health++;
        else if (Input.GetKey("KeypadSubtract")) Health--;
    }
}