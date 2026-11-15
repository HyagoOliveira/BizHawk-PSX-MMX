using System.Drawing;

namespace BizHawk.PSXMMX.Core;

public static class Constants
{
    public const string MEMORY_DOMAIN = "MainRAM";

    public static readonly Color MEMORY_FROZEN_COLOR = Color.Cyan;
    public static readonly Color MEMORY_UNFROZEN_COLOR = Color.White;

    public static Color GetMemoryColor(bool isFrozen) => isFrozen ?
        MEMORY_FROZEN_COLOR :
        MEMORY_UNFROZEN_COLOR;
}