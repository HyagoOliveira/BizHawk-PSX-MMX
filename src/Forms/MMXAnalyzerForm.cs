namespace BizHawkPSXMMX.Forms;

using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using BizHawkPSXMMX.Math;
using System.Drawing;
using System.Windows.Forms;

[ExternalTool("PSX MMX Analyzer", Description = "Analyzer Tool for PSOne MMX games")]
[ExternalToolEmbeddedIcon("BizHawkPSXMMX.Icon.ico")]
public sealed class MMXAnalyzerForm : ToolFormBase, IExternalToolForm
{
    public ApiContainer? MaybeAPIContainer { get; set; }
    private ApiContainer APIs => MaybeAPIContainer!;

    protected override string WindowTitleStatic => "PSX MMX Analyzer";

    public MMXAnalyzerForm()
    {
        ClientSize = new Size(300, 100);
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
    }

    protected override void UpdateAfter()
    {
        var p0 = new Vector2Int(10, 10);
        var p1 = new Vector2Int(20, 20);
        var p2 = new Vector2Int(40, 40);
        var p3 = new Vector2Int(60, 60);

        APIs.Gui.DrawText(p0, "MMX", Color.Yellow);
        APIs.Gui.DrawLine(p1, p2);
        APIs.Gui.DrawAxis(p3);
    }
}