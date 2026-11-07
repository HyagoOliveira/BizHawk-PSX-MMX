using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using BizHawk.PSXMMX.Memory;
using BizHawk.PSXMMX.Math;
using System.Drawing;
using System.Windows.Forms;

namespace BizHawk.PSXMMX.Forms;

[ExternalTool("PSX MMX Analyzer", Description = "Analyzer Tool for PSOne MMX games")]
[ExternalToolEmbeddedIcon("BizHawkPSXMMX.Icon.ico")]
public sealed class MMXAnalyzerForm : ToolFormBase, IExternalToolForm
{
    public ApiContainer APIs { get; set; } = null!;
    public GameMemoryAddress? MemoryAddress { get; private set; }

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
        var gameName = APIs.Emulation.GetGameInfo()?.Name ?? string.Empty;
        MemoryAddress = GameMemoryAddressFactory.Create(gameName);
    }

    protected override void UpdateAfter()
    {
        if (MemoryAddress is { } address) UpdateGame(address);
        else ShowUnsupportedGameMessage();
    }

    private void UpdateGame(GameMemoryAddress address)
    {
        var memory = APIs.Memory;
        var domain = memory.MainMemoryName;
        var playerPosition = new Vector2(
            memory.ReadQ16_16(address.Player.PositionX, domain),
            memory.ReadQ16_16(address.Player.PositionY, domain)
        );
        var message =
            $"Player HP: {memory.ReadU8(address.Player.Hp, domain)} | {domain}@0x{address.Player.Hp:X}\n" +
            $"Player Position: {playerPosition}";

        APIs.Gui.DrawText(
            new Vector2Int(10, 10),
            message,
            Color.Yellow,
            Color.FromArgb(180, Color.Black)
        );
        APIs.Gui.DrawAxis(playerPosition, color: Color.Lime);
    }

    private void ShowUnsupportedGameMessage() => APIs.Gui.DrawText(
        new Vector2Int(10, 10),
        "Failed to initialize!\nSupported games: Mega Man X4, X5 and X6 (USA)",
        Color.Red,
        Color.FromArgb(180, Color.Black)
    );
}