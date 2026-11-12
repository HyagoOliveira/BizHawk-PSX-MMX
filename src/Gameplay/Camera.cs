using BizHawk.Client.Common;
using BizHawk.PSXMMX.Math;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Gameplay;

public sealed class Camera : GameObject
{
    private const int LEFT_BORDER_OFFSET = 86;

    public Fixed16Vector2Memory PreviousPosition { get; }

    public static Camera Main { get; private set; } = null!;
    public static readonly Vector2Int SCREEN_SIZE = new(320, 240);

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
        //DrawVariables(SCREEN_SIZE / 2);
    }

    public Vector2Int WorldToScreenPoint(Vector2 position) => (position - Position.Value).ToVector2Int();
    public Vector2Int WorldToScreenPoint(Vector2Int position) => WorldToScreenPoint(position.ToVector2());
}