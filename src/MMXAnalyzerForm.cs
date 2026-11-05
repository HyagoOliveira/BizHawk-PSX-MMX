namespace BizHawkExternalToolTemplate;

using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
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
        APIs.Gui.DrawString(
            10,
            10,
            "MMX",
            surfaceID: DisplaySurfaceID.Client
        );
    }
}