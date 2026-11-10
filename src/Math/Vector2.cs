using System.Runtime.CompilerServices;

namespace BizHawk.PSXMMX.Math;

/// <summary>
/// Representation of 2D floating-point vectors and points.
/// </summary>
public readonly record struct Vector2(float X, float Y) : IEquatable<Vector2>
{
    public static readonly Vector2 Zero = new(0, 0);
    public static readonly Vector2 One = new(1, 1);
    public static readonly Vector2 Up = new(0, 1);
    public static readonly Vector2 Down = new(0, -1);
    public static readonly Vector2 Left = new(-1, 0);
    public static readonly Vector2 Right = new(1, 0);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float GetSqrMagnitude() => (X * X) + (Y * Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => $"({X}, {Y})";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.X + b.X, a.Y + b.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 operator -(Vector2 a, Vector2 b) => new(a.X - b.X, a.Y - b.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 operator -(Vector2 a) => new(-a.X, -a.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 operator *(Vector2 a, float scalar) => new(a.X * scalar, a.Y * scalar);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 operator *(float scalar, Vector2 a) => new(a.X * scalar, a.Y * scalar);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 operator *(Vector2 a, Vector2 b) => new(a.X * b.X, a.Y * b.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 operator /(Vector2 a, float divisor) => new(a.X / divisor, a.Y / divisor);
}
