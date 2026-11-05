using System.Runtime.CompilerServices;

/// <summary>
/// Representation of 2D integer vectors and points.
/// </summary>
public readonly record struct Vector2Int(int X, int Y) : IEquatable<Vector2Int>
{
    /// <summary>
    /// Shorthand for writing Vector2Int(0, 0).
    /// </summary>
    public static readonly Vector2Int Zero = new(0, 0);

    /// <summary>
    /// Shorthand for writing Vector2Int(1, 1).
    /// </summary>
    public static readonly Vector2Int One = new(1, 1);

    /// <summary>
    /// Shorthand for writing Vector2Int(0, 1).
    /// </summary>
    public static readonly Vector2Int Up = new(0, 1);

    /// <summary>
    /// Shorthand for writing Vector2Int(0, -1).
    /// </summary>
    public static readonly Vector2Int Down = new(0, -1);

    /// <summary>
    /// Shorthand for writing Vector2Int(-1, 0).
    /// </summary>
    public static readonly Vector2Int Left = new(-1, 0);

    /// <summary>
    /// Shorthand for writing Vector2Int(1, 0).
    /// </summary>
    public static readonly Vector2Int Right = new(1, 0);

    /// <summary>
    /// Gets the squared length of this vector.
    /// </summary>
    public int SqrMagnitude
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (X * X) + (Y * Y);
    }

    /// <summary>
    /// Gets the length of this vector.
    /// </summary>
    public double Magnitude
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => System.Math.Sqrt(SqrMagnitude);
    }

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

    /// <summary>
    /// Calculates the Manhattan distance between two vectors.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ManhattanDistance(Vector2Int a, Vector2Int b) =>
        Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);

    /// <summary>
    /// Calculates the Euclidean distance between two vectors.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Distance(Vector2Int a, Vector2Int b) => (a - b).Magnitude;

    /// <summary>
    /// Returns a vector made from the smallest components of two vectors.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int Min(Vector2Int a, Vector2Int b) =>
        new(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y));

    /// <summary>
    /// Returns a vector made from the largest components of two vectors.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int Max(Vector2Int a, Vector2Int b) =>
        new(Math.Max(a.X, b.X), Math.Max(a.Y, b.Y));

    /// <summary>
    /// Deconstructs the vector into separate X and Y components.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out int x, out int y) => (x, y) = (X, Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => $"({X}, {Y})";
}