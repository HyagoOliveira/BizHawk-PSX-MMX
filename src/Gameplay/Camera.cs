using BizHawk.Client.Common;
using BizHawk.PSXMMX.Math;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Gameplay;

public sealed class Camera : GameObject
{
    public static Camera Main { get; private set; } = null!;
    public static readonly Vector2Int SCREEN_SIZE = new(320, 240);
    private static readonly Vector2Int VARS_POSITION = SCREEN_SIZE / 2 - Vector2Int.One * 40;

    public Camera(long baseAddress, ApiContainer apis) : base(baseAddress, apis)
    {
        Main = this;
        // Camera Previous Position addresses are different
        PreviousPosition.XAddress = BaseAddress + 0x14;
        PreviousPosition.YAddress = BaseAddress + 0x18;

        IsDrawEnabled = false;
    }

    public override void Draw() => DrawVariables(VARS_POSITION);

    public Vector2Int WorldToScreenPoint(Vector2 position) => (position - Position.Value).ToVector2Int();
    public Vector2Int WorldToScreenPoint(Vector2Int position) => WorldToScreenPoint(position.ToVector2());
}