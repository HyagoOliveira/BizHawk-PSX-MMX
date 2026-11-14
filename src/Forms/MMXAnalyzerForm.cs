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
    public Camera? Camera { get; private set; } = null!;
    public KeyboardInput? Input { get; private set; } = null!;
    public GamePoolManager<MainObject>? MainObjectsPool { get; private set; } = null!;
    public GameMemoryAddress? MemoryAddress { get; private set; } = null!;

    protected override string WindowTitleStatic => "PSX MMX Analyzer";

    private readonly List<FoldoutControl> foldouts = [];

    public MMXAnalyzerForm()
    {
        ClientSize = new Size(600, 160);
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
        var gameHash = APIs.Emulation.GetGameInfo()?.Hash ?? string.Empty;
        MemoryAddress = GameMemoryAddress.Get(gameHash);

        Input = new KeyboardInput(APIs.Input);
        Camera = new Camera(MemoryAddress.Camera, APIs);
        Player = new Player(MemoryAddress.Player, APIs, Input);
        MainObjectsPool = new GamePoolManager<MainObject>(
            MemoryAddress.MainObjectsPool,
            blockSize: 0x9C,
            totalSlots: 48,
            APIs
        );

        foldouts.Add(new PlayerFoldout(Player, isExpanded: true));
        InitializeFoldouts();
    }

    protected override void UpdateAfter()
    {
        if (MemoryAddress?.IsInvalid() == true) ShowUnsupportedGameMessage();
        else UpdateGame();
    }

    private void UpdateGame()
    {
        Input?.Update();
        Camera?.UpdateAndDraw();
        Player?.UpdateAndDraw();
        MainObjectsPool?.Update();
    }

    private void InitializeFoldouts()
    {
        Activated -= (_, _) => UpdateFoldouts();

        var sections = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoScroll = true,
            Padding = new Padding(6)
        };

        foreach (var foldout in foldouts)
        {
            sections.Controls.Add(foldout);
        }
        Controls.Add(sections);

        Activated += (_, _) => UpdateFoldouts();
    }

    private void UpdateFoldouts()
    {
        if (MemoryAddress?.IsInvalid() == true) return;

        foreach (var foldout in foldouts)
        {
            foldout.UpdateContent();
        }
    }

    private void ShowUnsupportedGameMessage() => APIs.DrawText(
        new Vector2Int(10, 10),
        "Failed to initialize!\nSupported games: Mega Man X4, X5 and X6 (USA)",
        Color.Red,
        Color.FromArgb(180, Color.Black)
    );
}