using BizHawk.Client.Common;
using BizHawk.PSXMMX.Gameplay.Components.Collisions;
using BizHawk.PSXMMX.Input;
using BizHawk.PSXMMX.Math;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Gameplay;

public sealed class Player : GameCharacter
{
    public KeyboardInput Input { get; }

    // Variables
    public Fixed16Vector2Memory PreviousPosition { get; }

    // Components
    public HurtBox HurtBox { get; }
    public TerrainBox TerrainBox { get; }

    private const long BLOCK_SIZE = 0xE4;

    public Player(long baseAddress, ApiContainer apis, KeyboardInput input) : base(baseAddress, apis)
    {
        Input = input;

        PreviousPosition = AddVariable(
            new Fixed16Vector2Memory(
                "Previous Position",
                BaseAddress + 0x18,
                offset: 0x04,
                APIs.Memory
            ),
            displayOrder: 1
        );

        HurtBox = AddComponent(new HurtBox(BaseAddress + 0x54, this));
        TerrainBox = AddComponent(new TerrainBox(BaseAddress + 0x68, this));
    }

    public override void Update()
    {
        base.Update();
        UpdateHealthByInput();
        DrawVariables(Vector2Int.One * 10);
    }

    public override void Draw()
    {
        base.Draw();
        HurtBox.Draw();
        TerrainBox.Draw();
    }

    private void UpdateHealthByInput()
    {
        if (Input.GetKeyDown("T")) Health.ToggleFreeze();
        if (Input.GetKey("KeypadAdd")) Health++;
        else if (Input.GetKey("KeypadSubtract")) Health--;
    }
}