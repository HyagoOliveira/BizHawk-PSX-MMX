using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using BizHawk.PSXMMX.Math;
using BizHawk.PSXMMX.Input;
using System.Drawing;
using System.Windows.Forms;

namespace BizHawk.PSXMMX.Forms;

[ExternalTool("PSX MMX Analyzer", Description = "Analyzer Tool for PSOne MMX games")]
[ExternalToolEmbeddedIcon("BizHawkPSXMMX.Icon.ico")]
public sealed class MMXAnalyzerForm : ToolFormBase, IExternalToolForm
{
    public ApiContainer APIs { get; set; } = null!;
    public KeyboardInput Keyboard { get; private set; } = null!;

    protected override string WindowTitleStatic => "PSX MMX Analyzer";

    public MMXAnalyzerForm()
    {
        ClientSize = new Size(600, 100);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterParent;

        SuspendLayout();
        ResumeLayout(performLayout: false);
        PerformLayout();
    }

    public override void Restart()
    {
        base.Restart();
        Keyboard = new KeyboardInput(APIs.Input);
    }

    protected override void UpdateAfter()
    {
        Keyboard.Update();

        var pos = new Vector2Int(10, 10);
        var rect = new RectInt(60, 60, 50, 50);

        APIs.Gui.DrawBox(rect, backgroundColor: Color.Blue);
        if (Keyboard.GetKey("T")) APIs.Gui.DrawText(pos, "T is pressing", Color.Yellow);
    }
}