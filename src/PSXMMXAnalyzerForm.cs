namespace BizHawkExternalToolTemplate;

using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using System.Drawing;
using System.Windows.Forms;

[ExternalTool("PSX MMX", Description = "Analiser Tool for MMX games")]
[ExternalToolEmbeddedIcon("Icon.png")]
public sealed class PSXMMXAnalyzerForm : ToolFormBase, IExternalToolForm
{
    public ApiContainer? MaybeAPIContainer { get; set; }
    private ApiContainer APIs => MaybeAPIContainer!;

    protected override string WindowTitleStatic => "PSX MMX Analyzer";

    public PSXMMXAnalyzerForm()
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