using System.Globalization;

namespace BizHawk.PSXMMX.Math;

/// <summary>
/// Representation of 2D floating-point vectors and points.
/// </summary>
public record struct Vector2(float X, float Y) : IEquatable<Vector2>
{
    public static readonly Vector2 Zero = new(0, 0);
    public static readonly Vector2 One = new(1, 1);
    public static readonly Vector2 Up = new(0, 1);
    public static readonly Vector2 Down = new(0, -1);
    public static readonly Vector2 Left = new(-1, 0);
    public static readonly Vector2 Right = new(1, 0);

    public readonly float GetSqrMagnitude() => (X * X) + (Y * Y);

    public readonly Vector2Int ToVector2Int() => new((int)X, (int)Y);
    public override readonly string ToString() => string.Format(CultureInfo.InvariantCulture, "({0}, {1})", X, Y);

    public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.X + b.X, a.Y + b.Y);
    public static Vector2 operator -(Vector2 a, Vector2 b) => new(a.X - b.X, a.Y - b.Y);
    public static Vector2 operator -(Vector2 a) => new(-a.X, -a.Y);
    public static Vector2 operator *(Vector2 a, float scalar) => new(a.X * scalar, a.Y * scalar);
    public static Vector2 operator *(float scalar, Vector2 a) => new(a.X * scalar, a.Y * scalar);
    public static Vector2 operator *(Vector2 a, int scalar) => a * (float)scalar;
    public static Vector2 operator *(int scalar, Vector2 a) => a * (float)scalar;
    public static Vector2 operator *(Vector2 a, Vector2 b) => new(a.X * b.X, a.Y * b.Y);
    public static Vector2 operator /(Vector2 a, float divisor) => new(a.X / divisor, a.Y / divisor);
}