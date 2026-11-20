using BizHawk.Client.Common;
using BizHawk.PSXMMX.Math;

namespace BizHawk.PSXMMX.Gameplay;

public sealed class Camera : GameObject
{
    public static Camera Main { get; private set; } = null!;
    public static readonly Vector2Int SCREEN_SIZE = new(320, 240);
    private static readonly Vector2Int HALF_SCREEN_SIZE = SCREEN_SIZE / 2;

    public Camera(long baseAddress, ApiContainer apis) : base(baseAddress, apis)
    {
        Main = this;
        IsDrawEnabled = true;
        IsPropertiesEnabled = true;
        PropertiesVerticalAlign = "center";
        PropertiesHorizontalAlign = "center";

        // Camera Previous Position addresses are different
        PreviousPosition.XAddress = BaseAddress + 0x14;
        PreviousPosition.YAddress = BaseAddress + 0x18;
    }

    public Vector2Int WorldToScreenPoint(Vector2 position) => (position - Position.Value).ToVector2Int();
    public Vector2Int WorldToScreenPoint(Vector2Int position) => WorldToScreenPoint(position.ToVector2());

    protected override void DrawInGameName() { }
    protected override string GetInGameName() => "Camera";
    protected override Vector2Int GetPropertiesPosition() => HALF_SCREEN_SIZE;
    protected override string GetProperties() => $"Camera\n{Position} | {PreviousPosition}";
}