using BizHawk.Client.Common;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace BizHawk.PSXMMX.Math;

/// <summary>
/// Draws graphical elements on the BizHawk client surface using game-display coordinates.
/// </summary>
/// <remarks>
/// Positions are measured in framebuffer pixels from the top-left of the game image, with X increasing
/// to the right and Y increasing downwards. They are screen coordinates, not game-world coordinates.
/// A horizontal offset of 18 framebuffer pixels is applied before BizHawk transforms positions to client
/// coordinates. This offset was observed in the USA versions of Mega Man X4, X5, and X6 using Nymashock;
/// different video or overscan settings may require a different offset.
/// Text and axis sizes are in client pixels and do not scale with the game image.
/// </remarks>
public static class ApiContainerExtensions
{
    public const int DEFAULT_FONT_SIZE = 12;
    // Rendering using Client Mode to get a better resolution.
    public const DisplaySurfaceID DEFAULT_RENDER_MODE = DisplaySurfaceID.Client;
    public static readonly Size FRAME_BUFFER_INITIAL_OFFSET = new(18, 0);

    /// <summary>Draws text at a game-display position.</summary>
    /// <param name="apis">The BizHawk API container.</param>
    /// <param name="position">The text position in game-display pixels.</param>
    /// <param name="message">The text to draw.</param>
    /// <param name="color">The foreground color, or null to use BizHawk's default.</param>
    /// <param name="backColor">The background color, or null to use BizHawk's default.</param>
    /// <param name="fontSize">The font size in client pixels.</param>
    /// <param name="fontFace">The font family, or null to use BizHawk's default.</param>
    /// <param name="fontStyle">The font style, or null to use BizHawk's default.</param>
    /// <param name="horizontalAlign">The horizontal text alignment, or null to use BizHawk's default.</param>
    /// <param name="verticalAlign">The vertical text alignment, or null to use BizHawk's default.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void DrawText(
        this ApiContainer apis,
        Vector2Int position,
        string message,
        Color? color = null,
        Color? backColor = null,
        int fontSize = DEFAULT_FONT_SIZE,
        string? fontFace = null,
        string? fontStyle = null,
        string? horizontalAlign = null,
        string? verticalAlign = null
    )
    {
        var clientPosition = ToClientPosition(apis.EmuClient, position);
        apis.Gui.DrawString(
            clientPosition.X,
            clientPosition.Y,
            message,
            color,
            backColor,
            fontSize,
            fontFace,
            fontStyle,
            horizontalAlign,
            verticalAlign,
            DEFAULT_RENDER_MODE
        );
    }

    /// <summary>Draws a line between two game-display positions.</summary>
    /// <param name="apis">The BizHawk API container.</param>
    /// <param name="start">The start position in game-display pixels.</param>
    /// <param name="end">The end position in game-display pixels.</param>
    /// <param name="color">The line color, or null to use BizHawk's default.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void DrawLine(
        this ApiContainer apis,
        Vector2Int start,
        Vector2Int end,
        Color? color = null)
    {
        var clientStart = ToClientPosition(apis.EmuClient, start);
        var clientEnd = ToClientPosition(apis.EmuClient, end);
        apis.Gui.DrawLine(
            clientStart.X,
            clientStart.Y,
            clientEnd.X,
            clientEnd.Y,
            color,
            DEFAULT_RENDER_MODE
        );
    }

    /// <summary>Draws a cross centered on a game-display position.</summary>
    /// <param name="apis">The BizHawk API container.</param>
    /// <param name="position">The center position in game-display pixels.</param>
    /// <param name="color">The cross color, or null to use BizHawk's default.</param>
    /// <param name="size">The distance from the center to each endpoint in client pixels.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void DrawAxis(
        this ApiContainer apis,
        Vector2Int position,
        Color? color = null,
        int size = 6
    )
    {
        var clientPosition = ToClientPosition(apis.EmuClient, position);
        apis.Gui.DrawAxis(
            clientPosition.X,
            clientPosition.Y,
            size,
            color,
            DEFAULT_RENDER_MODE
        );
    }

    /// <summary>Draws a rectangle defined in game-display coordinates.</summary>
    /// <param name="apis">The BizHawk API container.</param>
    /// <param name="rect">The rectangle in game-display pixels.</param>
    /// <param name="lineColor">The outline color, or null to use BizHawk's default.</param>
    /// <param name="backgroundColor">The fill color, or null to use BizHawk's default.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void DrawBox(
        this ApiContainer apis,
        RectInt rect,
        Color? lineColor = null,
        Color? backgroundColor = null
    )
    {
        var clientMin = ToClientPosition(apis.EmuClient, new Vector2Int(rect.GetXMin(), rect.GetYMin()));
        var clientMax = ToClientPosition(apis.EmuClient, new Vector2Int(rect.GetXMax(), rect.GetYMax()));
        // BizHawk IGuiApi.DrawBox requires (x1, y1, x2, y2)
        apis.Gui.DrawBox(
            clientMin.X,
            clientMin.Y,
            clientMax.X,
            clientMax.Y,
            lineColor,
            backgroundColor,
            DEFAULT_RENDER_MODE
        );
    }

    private static Point ToClientPosition(IEmuClientApi client, Vector2Int position) =>
        client.TransformPoint(Point.Add(position.ToPoint(), FRAME_BUFFER_INITIAL_OFFSET));
}