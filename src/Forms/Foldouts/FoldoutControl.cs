using System.Windows.Forms;

namespace BizHawk.PSXMMX.Forms.Foldouts;

public class FoldoutControl : UserControl
{
    private const int HEADER_HEIGHT = 28;
    private readonly Button headerButton = new();
    private readonly Panel contentPanel = new();
    private bool isExpanded;
    private int contentHeight = 80;

    public string Title { get; }
    protected Panel ContentPanel => contentPanel;
    protected FlowLayoutPanel ControlsPanel { get; }

    public bool IsExpanded
    {
        get => isExpanded;
        set
        {
            isExpanded = value;
            contentPanel.Visible = value;
            headerButton.Text = $"{(value ? "-" : "+")} {Title}";
            Height = HEADER_HEIGHT + (value ? ContentHeight : 0);
        }
    }

    public int ContentHeight
    {
        get => contentHeight;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
            contentHeight = value;
            Height = HEADER_HEIGHT + (IsExpanded ? value : 0);
        }
    }

    public FoldoutControl(string title, bool isExpanded = false)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("A foldout title is required.", nameof(title));

        Title = title;
        Text = title;
        Width = 560;
        IsExpanded = isExpanded;

        headerButton.Dock = DockStyle.Top;
        headerButton.Height = HEADER_HEIGHT;
        headerButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        headerButton.Click += (_, _) => IsExpanded = !IsExpanded;

        ContentPanel.Dock = DockStyle.Fill;
        ContentPanel.BorderStyle = BorderStyle.FixedSingle;

        Controls.Add(ContentPanel);
        Controls.Add(headerButton);

        ControlsPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(3),
            WrapContents = true
        };
        ContentPanel.Controls.Add(ControlsPanel);
    }

    public virtual void UpdateContent() { }

    protected static FlowLayoutPanel GetVerticalPanel() => new()
    {
        AutoSize = true,
        AutoSizeMode = AutoSizeMode.GrowAndShrink,
        FlowDirection = FlowDirection.TopDown,
        Margin = Padding.Empty,
        WrapContents = false
    };
}