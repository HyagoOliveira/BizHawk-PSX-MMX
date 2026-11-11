using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using BizHawk.PSXMMX.Forms.Foldout;
using BizHawk.PSXMMX.Gameplay;
using BizHawk.PSXMMX.Input;
using BizHawk.PSXMMX.Math;
using BizHawk.PSXMMX.Memory;
using System.Drawing;
using System.Windows.Forms;

namespace BizHawk.PSXMMX.Forms;

[ExternalTool("PSX MMX Analyzer", Description = "Analyzer Tool for PSOne MMX games")]
[ExternalToolEmbeddedIcon("BizHawkPSXMMX.Icon.ico")]
public sealed class MMXAnalyzerForm : ToolFormBase, IExternalToolForm
{
    public ApiContainer APIs { get; set; } = null!;
    public Player? Player { get; private set; } = null!;
    public KeyboardInput? Input { get; private set; } = null!;
    public GameMemoryAddress? MemoryAddress { get; private set; } = null!;

    protected override string WindowTitleStatic => "PSX MMX Analyzer";

    public MMXAnalyzerForm()
    {
        ClientSize = new Size(600, 160);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterParent;

        SuspendLayout();
        InitializeFoldouts();
        ResumeLayout(performLayout: false);
        PerformLayout();
    }

    public override void Restart()
    {
        base.Restart();
        var gameHash = APIs.Emulation.GetGameInfo()?.Hash ?? string.Empty;
        MemoryAddress = GameMemoryAddress.Get(gameHash);

        Input = new KeyboardInput(APIs.Input);
        Player = new Player(MemoryAddress.Player, APIs, Input);
    }

    protected override void UpdateAfter()
    {
        if (MemoryAddress?.IsInvalid() == true) ShowUnsupportedGameMessage();
        else UpdateGame();
    }

    private void UpdateGame()
    {
        Input?.Update();
        Player?.Update();
    }

    private void InitializeFoldouts()
    {
        /*var sections = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoScroll = true,
            Padding = new Padding(6)
        };

        sections.Controls.Add(playerFoldout);
        Controls.Add(sections);*/
    }

    private void ShowUnsupportedGameMessage() => APIs.DrawText(
        new Vector2Int(10, 10),
        "Failed to initialize!\nSupported games: Mega Man X4, X5 and X6 (USA)",
        Color.Red,
        Color.FromArgb(180, Color.Black)
    );
}