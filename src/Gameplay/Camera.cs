using BizHawk.Client.Common;
using BizHawk.PSXMMX.Math;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Gameplay;

public sealed class Camera : GameObject
{
    public static Camera Main { get; private set; } = null!;
    public Fixed16Vector2Memory PreviousPosition { get; }

    public Camera(long baseAddress, ApiContainer apis) : base(baseAddress, apis)
    {
        Main = this;

        PreviousPosition = AddVariable(new Fixed16Vector2Memory(
            "Previous Position",
            BaseAddress + 0x14,
            offset: 0x04,
            APIs.Memory
        ));
    }

    public override void Draw()
    {
        base.Draw();
        DrawVariables(new Vector2Int(320, 240) / 2);
    }
}