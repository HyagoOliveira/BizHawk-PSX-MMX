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
            Margin = new Padding(2, 8, 2, 2)
        };

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
            Margin = new Padding(2, 3, 2, 3)
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