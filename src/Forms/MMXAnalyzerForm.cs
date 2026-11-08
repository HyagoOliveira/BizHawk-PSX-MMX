using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using BizHawk.PSXMMX.Memory;
using BizHawk.PSXMMX.Math;
using System.Drawing;
using System.Windows.Forms;
using BizHawk.PSXMMX.Input;

namespace BizHawk.PSXMMX.Forms;

[ExternalTool("PSX MMX Analyzer", Description = "Analyzer Tool for PSOne MMX games")]
[ExternalToolEmbeddedIcon("BizHawkPSXMMX.Icon.ico")]
public sealed class MMXAnalyzerForm : ToolFormBase, IExternalToolForm
{
    public ApiContainer APIs { get; set; } = null!;
    public KeyboardInput? Input { get; private set; } = null!;
    public GameMemoryAddress? MemoryAddress { get; private set; } = null!;

    protected override string WindowTitleStatic => "PSX MMX Analyzer";

    public UIntMemory PlayerHP { get; private set; } = default!;

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
        var gameName = APIs.Emulation.GetGameInfo()?.Name ?? string.Empty;
        MemoryAddress = GameMemoryAddress.Create(gameName);

        Input = new KeyboardInput(APIs.Input);
        PlayerHP = new UIntMemory("Player HP", MemoryAddress?.PlayerHP ?? 0, APIs.Memory);
    }

    protected override void UpdateAfter()
    {
        if (MemoryAddress == null) ShowUnsupportedGameMessage();
        else UpdateGame();
    }

    private void UpdateGame()
    {
        Input.Update();

        PlayerHP.Update();
        PlayerHP.Draw(new Vector2Int(60, 10), APIs.Gui);

        if (Input.GetKeyDown("T")) PlayerHP.ToggleFreeze();
    }

    private void ShowUnsupportedGameMessage() => APIs.Gui.DrawText(
        new Vector2Int(10, 10),
        "Failed to initialize!\nSupported games: Mega Man X4, X5 and X6 (USA)",
        Color.Red,
        Color.FromArgb(180, Color.Black)
    );
}