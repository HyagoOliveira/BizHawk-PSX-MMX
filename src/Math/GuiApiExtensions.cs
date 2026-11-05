namespace BizHawkPSXMMX.Math;

using BizHawk.Client.Common;
using System.Drawing;
using System.Runtime.CompilerServices;

/// <summary>
/// Provides extension methods for <see cref="IGuiApi"/> enabling coordinate interop with <see cref="Vector2Int"/>.
/// Compatible with BizHawk 2.11.1.
/// </summary>
public static class GuiApiExtensions
{
    private static readonly DisplaySurfaceID DefaultSurfaceID = DisplaySurfaceID.Client;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void DrawText(
        this IGuiApi gui,
        Vector2Int position,
        string message,
        Color? color = null,
        Color? backColor = null,
        int? fontSize = 12,
        string? fontFace = null,
        string? fontStyle = null,
        string? horizontalAlign = null,
        string? verticalAlign = null)
    {
        gui.DrawString(
            position.X,
            position.Y,
            message,
            color,
            backColor,
            fontSize,
            fontFace,
            fontStyle,
            horizontalAlign,
            verticalAlign,
            DefaultSurfaceID
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void DrawLine(
        this IGuiApi gui,
        Vector2Int start,
        Vector2Int end,
        Color? color = null)
    {
        gui.DrawLine(
            start.X,
            start.Y,
            end.X,
            end.Y,
            color,
            DefaultSurfaceID
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void DrawAxis(
        this IGuiApi gui,
        Vector2Int position,
        int size = 6,
        Color? color = null)
    {
        gui.DrawAxis(
            position.X,
            position.Y,
            size,
            color,
            DefaultSurfaceID
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void DrawBox(
        this IGuiApi gui,
        RectInt rect,
        Color? lineColor = null,
        Color? backgroundColor = null)
    {
        // BizHawk IGuiApi.DrawBox requires (x1, y1, x2, y2)
        gui.DrawBox(
            rect.XMin,
            rect.YMin,
            rect.XMax,
            rect.YMax,
            line: lineColor,
            backgroundColor,
            DefaultSurfaceID
        );
    }
}