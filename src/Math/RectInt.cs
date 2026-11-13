using System.Runtime.CompilerServices;

namespace BizHawk.PSXMMX.Math;

/// <summary>
/// A 2D rectangle defined by integer coordinates and dimensions.
/// </summary>
public readonly record struct RectInt : IEquatable<RectInt>
{
    public int X { get; }
    public int Y { get; }
    public int Width { get; }
    public int Height { get; }

    public static readonly RectInt Zero = new(0, 0, 0, 0);

    public RectInt(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public RectInt(int x, int y, uint width, uint height) :
        this(x, y, (int)width, (int)height)
    {
    }

    public override string ToString() => $"(X:{X}, Y:{Y}, W:{Width}, H:{Height})";

    public Vector2Int GetPosition() => new(X, Y);
    public Vector2Int GetSize() => new(Width, Height);

    public int GetXMin() => System.Math.Min(X, X + Width);
    public int GetXMax() => System.Math.Max(X, X + Width);
    public int GetYMin() => System.Math.Min(Y, Y + Height);
    public int GetYMax() => System.Math.Max(Y, Y + Height);

    public Vector2Int GetMin() => new(GetXMin(), GetYMin());
    public Vector2Int GetMax() => new(GetXMax(), GetYMax());
    public Vector2Int GetTopRight() => new(GetXMax(), GetYMin());
    public Vector2Int GetCenter() => new(X + (Width / 2), Y + (Height / 2));
}