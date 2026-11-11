namespace BizHawk.PSXMMX.Math;

/// <summary>
/// Camera bounds represented by signed 16-bit minimum and maximum coordinates.
/// </summary>
public readonly record struct Bounds(short MinX, short MaxX, short MinY, short MaxY);
