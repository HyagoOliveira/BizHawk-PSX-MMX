namespace BizHawk.PSXMMX.Math;

public readonly record struct Vector2(float X, float Y)
{
    public override string ToString() => $"({X}, {Y})";
}
