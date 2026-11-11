using System.Drawing;
using System.Runtime.CompilerServices;

namespace BizHawk.PSXMMX.Math;

/// <summary>
/// Representation of 2D integer vectors and points.
/// </summary>
public record struct Vector2Int(int X, int Y) : IEquatable<Vector2Int>
{
    public static readonly Vector2Int Zero = new(0, 0);
    public static readonly Vector2Int One = new(1, 1);
    public static readonly Vector2Int Up = new(0, 1);
    public static readonly Vector2Int Down = new(0, -1);
    public static readonly Vector2Int Left = new(-1, 0);
    public static readonly Vector2Int Right = new(1, 0);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int GetSqrMagnitude() => (X * X) + (Y * Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override readonly string ToString() => $"({X}, {Y})";

    public Point ToPoint() => new(X, Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int operator +(Vector2Int a, Vector2Int b) => new(a.X + b.X, a.Y + b.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int operator -(Vector2Int a, Vector2Int b) => new(a.X - b.X, a.Y - b.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int operator -(Vector2Int a) => new(-a.X, -a.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int operator *(Vector2Int a, int scalar) => new(a.X * scalar, a.Y * scalar);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int operator *(int scalar, Vector2Int a) => new(a.X * scalar, a.Y * scalar);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int operator *(Vector2Int a, Vector2Int b) => new(a.X * b.X, a.Y * b.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int operator /(Vector2Int a, int divisor) => new(a.X / divisor, a.Y / divisor);
}