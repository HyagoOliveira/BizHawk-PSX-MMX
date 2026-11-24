using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using BizHawk.PSXMMX.Forms.Foldouts;
using BizHawk.PSXMMX.Gameplay;
using BizHawk.PSXMMX.Input;
using BizHawk.PSXMMX.Math;
using System.Drawing;
using System.Windows.Forms;
using MMXGame = BizHawk.PSXMMX.Core.Game;

namespace BizHawk.PSXMMX.Forms;

[ExternalTool("PSX MMX Analyzer", Description = "Analyzer Tool for PSOne MMX games")]
[ExternalToolEmbeddedIcon("BizHawkPSXMMX.Icon.ico")]
public sealed class MMXAnalyzerForm : ToolFormBase, IExternalToolForm
{
    public ApiContainer APIs { get; set; } = null!;
    public Player? Player { get; private set; } = null!;
    public Camera? Camera { get; private set; } = null!;
    public KeyboardInput? Input { get; private set; } = null!;
    public GamePoolManager<Shot>? ShotPool { get; private set; } = null!;
    public GamePoolManager<Weapon>? WeaponPool { get; private set; } = null!;
    public GamePoolManager<MainObject>? MainObjectsPool { get; private set; } = null!;

    protected override string WindowTitleStatic => "PSX MMX Analyzer";

    private readonly List<FoldoutControl> foldouts = [];
    private readonly StatusStrip statusStrip = new()
    {
        Items = { new ToolStripStatusLabel("F - Frame Pause | F12 - Screenshot | Pause Break - Pause/Unpause") }
    };

    public MMXAnalyzerForm()
    {
        ClientSize = new Size(600, 400);
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
        MMXGame.Initialize(GetGameHash());

        Input = new KeyboardInput(APIs.Input);
        Camera = new Camera(MMXGame.Addresses.Camera, APIs);
        Player = new Player(MMXGame.Addresses.Player, APIs, Input);

        WeaponPool = new GamePoolManager<Weapon>(
            MMXGame.Addresses.WeaponObjectsPool,
            blockSize: 0x9C,
            totalSlots: 16,
            APIs
        );
        ShotPool = new GamePoolManager<Shot>(
            MMXGame.Addresses.ShotObjectsPool,
            blockSize: 0x9C,
            totalSlots: 32,
            APIs
        );
        MainObjectsPool = new GamePoolManager<MainObject>(
            MMXGame.Addresses.MainObjectsPool,
            blockSize: 0x9C,
            totalSlots: 48,
            APIs
        );

        foldouts.Clear();
        foldouts.Add(new PlayerFoldout(Player, isExpanded: true));
        foldouts.Add(new CameraFoldout(Camera));
        foldouts.Add(new GamePoolFoldout<MainObject>(MainObjectsPool));
        foldouts.Add(new GamePoolFoldout<Weapon>(WeaponPool));
        foldouts.Add(new GamePoolFoldout<Shot>(ShotPool));

        InitializeFoldouts();
    }

    protected override void UpdateAfter()
    {
        if (MMXGame.IsInvalid()) ShowUnsupportedGameMessage();
        else UpdateGame();
    }

    private void UpdateGame()
    {
        Input?.Update();
        Camera?.UpdateAndTryDraw();
        Player?.UpdateAndTryDraw();
        MainObjectsPool?.UpdateAndTryDraw();
        WeaponPool?.UpdateAndTryDraw();
        ShotPool?.UpdateAndTryDraw();
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

        Controls.Clear();
        Controls.Add(sections);
        Controls.Add(statusStrip);

        Activated += (_, _) => UpdateFoldouts();
    }

    private void UpdateFoldouts()
    {
        if (MMXGame.IsInvalid()) return;

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

    private string GetGameHash() => APIs.Emulation.GetGameInfo()?.Hash ?? string.Empty;
}