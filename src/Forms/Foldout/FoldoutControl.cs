using System.Windows.Forms;

namespace BizHawk.PSXMMX.Forms.Foldout;

public class FoldoutControl : UserControl
{
    private const int HEADER_HEIGHT = 28;
    private readonly Button headerButton = new();
    private readonly Panel contentPanel = new();
    private bool isExpanded;
    private int contentHeight = 80;

    public string Title { get; }
    protected Panel ContentPanel => contentPanel;

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

        contentPanel.Dock = DockStyle.Fill;
        contentPanel.BorderStyle = BorderStyle.FixedSingle;

        Controls.Add(contentPanel);
        Controls.Add(headerButton);
    }

    public virtual void UpdateContent() { }
}