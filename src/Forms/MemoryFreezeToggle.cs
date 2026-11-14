using System.Drawing;
using System.Windows.Forms;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Forms;

public sealed class MemoryFreezeToggle : Button
{
    private readonly IMemory memory;

    public MemoryFreezeToggle(IMemory memory)
    {
        this.memory = memory ?? throw new ArgumentNullException(nameof(memory));
        Size = new Size(130, 24);
        UseVisualStyleBackColor = false;
        UpdateAppearance();
        Click += (_, _) => HandleClick();
    }

    public void UpdateAppearance()
    {
        Text = $"{(memory.IsFrozen ? "Unfreeze" : "Freeze")} {memory.Name}";
        BackColor = memory.IsFrozen ? MemoryDisplayColors.FROZEN_COLOR : MemoryDisplayColors.UNFROZEN_COLOR;
    }

    private void HandleClick()
    {
        memory.ToggleFreeze();
        UpdateAppearance();
    }
}
