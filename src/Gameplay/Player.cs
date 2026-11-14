using BizHawk.Client.Common;
using BizHawk.PSXMMX.Input;
using BizHawk.PSXMMX.Math;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Gameplay;

public sealed class Player : MainObject
{
    public KeyboardInput Input { get; }

    // Variables
    public UIntS8Memory Character { get; } // cur_character


    public Player(long baseAddress, ApiContainer apis, KeyboardInput input) : base(baseAddress, apis)
    {
        Input = input;
        ShowProperties = true;

        Character = AddVariable(
            new UIntS8Memory(
                "Char",
                baseAddress + 0x3093B,
                APIs.Memory,
                hideDisplay: true
            )
        );
    }

    public override void Update()
    {
        base.Update();
        UpdateHealthByInput(); //TODO remove later
    }

    protected override void UpdateName()
    {
        var playerName = (PlayerName)Character.Value;
        Name = $"Player:\t{playerName}";
    }

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

    private enum PlayerName
    {
        X = 0,
        Zero = 1
    }
}