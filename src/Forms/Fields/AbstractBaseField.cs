using System.Windows.Forms;
using BizHawk.PSXMMX.Core;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Forms.Fields;

public abstract class AbstractBaseField<T> : FlowLayoutPanel where T : Control
{
    public T Control { get; }

    protected AbstractBaseField(string labelText, T control)
    {
        Control = control ?? throw new ArgumentNullException(nameof(control));
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        Margin = Padding.Empty;
        WrapContents = false;

        var label = new Label
        {
            AutoSize = true,
            Text = labelText,
            Margin = FormConstants.LABEL_MARGIN
        };

        if (string.IsNullOrEmpty(labelText))
            label.Visible = false;

        Controls.Add(label);
        Controls.Add(Control);
    }

    public void AddToggleFreezeButton(IMemory memory)
    {
        var button = new Button
        {
            Text = "F",
            Width = 28,
            Height = 24,
            Margin = FormConstants.BUTTON_MARGIN
        };
        button.Click += (_, _) =>
        {
            memory.ToggleFreeze();
            button.Text = memory.IsFrozen ? "U" : "F";
            button.BackColor = Constants.GetMemoryColor(memory.IsFrozen);
        };

        Controls.Add(button);
    }
}