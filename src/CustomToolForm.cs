namespace BizHawkExternalToolTemplate;

using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using System.Drawing;
using System.Windows.Forms;

[ExternalTool("Custom Tool Template", Description = "Clean template for BizHawk 2.11.1 external tools")]
public sealed class CustomToolForm : ToolFormBase, IExternalToolForm
{
    public ApiContainer? MaybeAPIContainer { get; set; }
    private ApiContainer APIs => MaybeAPIContainer!;

    protected override string WindowTitleStatic => "Custom Tool Template";

    private readonly Label statusLabel = new()
    {
        Text = "Template Loaded",
        Location = new Point(20, 15),
        AutoSize = true,
        Font = new Font(FontFamily.GenericSansSerif, 9.5f, FontStyle.Bold)
    };

    private readonly Label frameCounterLabel = new()
    {
        Text = "Current Frame: 0",
        Location = new Point(20, 45),
        AutoSize = true
    };

    public CustomToolForm()
    {
        ClientSize = new Size(300, 100);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterParent;

        SuspendLayout();
        Controls.Add(statusLabel);
        Controls.Add(frameCounterLabel);
        ResumeLayout(performLayout: false);
        PerformLayout();
    }

    public override void Restart()
    {
        base.Restart();
        frameCounterLabel.Text = "Current Frame: 0";
    }

    protected override void UpdateAfter()
    {
        if (MaybeAPIContainer is null || APIs.Emulation.GetGameInfo() is null) return;

        var currentFrame = APIs.Emulation.FrameCount();
        frameCounterLabel.Text = $"Current Frame: {currentFrame}";

        DrawRectangleText(20, 20, frameCounterLabel.Text);
    }

    private void DrawRectangleText(int x, int y, string text)
    {
        var outlineColor = Color.FromArgb(0, 122, 204);
        var fillColor = Color.FromArgb(64, 0, 122, 204);
        var textColor = Color.White;
        var textBackgroundColor = Color.FromArgb(180, 0, 0, 0);

        APIs.Gui.DrawRectangle(
            x,
            y,
            160,
            30,
            outlineColor,
            fillColor,
            DisplaySurfaceID.Client
        );

        APIs.Gui.DrawString(
            x + 5,
            y + 6,
            text,
            textColor,
            textBackgroundColor,
            fontsize: 11,
            surfaceID: DisplaySurfaceID.Client
        );
    }
}