using System.Drawing;
using System.Windows.Forms;

namespace BizHawk.PSXMMX.Forms;

public static class FormConstants
{
    public static readonly Size BUTTON_SIZE = new(130, 24);
    public static readonly Padding LABEL_MARGIN = new(2, 8, 2, 2);
    public static readonly Padding BUTTON_MARGIN = new(2, 4, 2, 2);
    public static readonly Padding CONTROL_PADDING = new(2, 5, 2, 3);

    public static Label BuildNumpadMoverLabel() => new()
    {
        Text = "Use Numpad 4, 6, 8 and 2 to move",
        BackColor = Color.Cyan,
        AutoSize = true,
        Margin = LABEL_MARGIN
    };
}