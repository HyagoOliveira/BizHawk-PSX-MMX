namespace BizHawk.PSXMMX.Math;

using System;
using System.Runtime.CompilerServices;

/// <summary>
/// A 2D rectangle defined by integer coordinates and dimensions.
/// </summary>
public readonly record struct RectInt : IEquatable<RectInt>
{
    /// <summary>
    /// Gets the X coordinate of the left edge of the rectangle.
    /// </summary>
    public int X { get; }

    /// <summary>
    /// Gets the Y coordinate of the top edge of the rectangle.
    /// </summary>
    public int Y { get; }

    /// <summary>
    /// Gets the width of the rectangle.
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Gets the height of the rectangle.
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Shorthand for writing RectInt(0, 0, 0, 0).
    /// </summary>
    public static readonly RectInt Zero = new(0, 0, 0, 0);

    /// <summary>
    /// Initializes a new instance of the <see cref="RectInt"/> struct with position coordinates and size.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RectInt(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RectInt"/> struct using vector position and size.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RectInt(Vector2Int position, Vector2Int size)
    {
        X = position.X;
        Y = position.Y;
        Width = size.X;
        Height = size.Y;
    }

    /// <summary>
    /// Gets the position of the top-left corner as a <see cref="Vector2Int"/>.
    /// </summary>
    public Vector2Int Position
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(X, Y);
    }

    /// <summary>
    /// Gets the width and height of the rectangle as a <see cref="Vector2Int"/>.
    /// </summary>
    public Vector2Int Size
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Width, Height);
    }

    /// <summary>
    /// Gets the minimum X coordinate (left edge).
    /// </summary>
    public int XMin
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => System.Math.Min(X, X + Width);
    }

    /// <summary>
    /// Gets the maximum X coordinate (right edge).
    /// </summary>
    public int XMax
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => System.Math.Max(X, X + Width);
    }

    /// <summary>
    /// Gets the minimum Y coordinate (top edge).
    /// </summary>
    public int YMin
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => System.Math.Min(Y, Y + Height);
    }

    /// <summary>
    /// Gets the maximum Y coordinate (bottom edge).
    /// </summary>
    public int YMax
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => System.Math.Max(Y, Y + Height);
    }

    /// <summary>
    /// Gets the minimum bounding point (top-left).
    /// </summary>
    public Vector2Int Min
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(XMin, YMin);
    }

    /// <summary>
    /// Gets the maximum bounding point (bottom-right).
    /// </summary>
    public Vector2Int Max
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(XMax, YMax);
    }

    /// <summary>
    /// Gets the center point of the rectangle.
    /// </summary>
    public Vector2Int Center
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(X + (Width / 2), Y + (Height / 2));
    }

    /// <summary>
    /// Creates a rectangle given its minimum and maximum coordinates.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RectInt MinMaxRect(int xMin, int yMin, int xMax, int yMax) =>
        new(xMin, yMin, xMax - xMin, yMax - yMin);

    /// <summary>
    /// Creates a rectangle given its minimum and maximum vector coordinates.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RectInt MinMaxRect(Vector2Int min, Vector2Int max) =>
        MinMaxRect(min.X, min.Y, max.X, max.Y);

    /// <summary>
    /// Checks if a given position lies within the boundaries of this rectangle.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(Vector2Int point) =>
        point.X >= XMin && point.X < XMax && point.Y >= YMin && point.Y < YMax;

    /// <summary>
    /// Checks if a given (x, y) coordinate lies within the boundaries of this rectangle.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(int x, int y) =>
        x >= XMin && x < XMax && y >= YMin && y < YMax;

    /// <summary>
    /// Checks if this rectangle overlaps with another rectangle.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Overlaps(RectInt other) =>
        other.XMax > XMin && other.XMin < XMax && other.YMax > YMin && other.YMin < YMax;

    /// <summary>
    /// Deconstructs the rectangle into its position and size components.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out int x, out int y, out int width, out int height) =>
        (x, y, width, height) = (X, Y, Width, Height);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => $"(X:{X}, Y:{Y}, W:{Width}, H:{Height})";
}