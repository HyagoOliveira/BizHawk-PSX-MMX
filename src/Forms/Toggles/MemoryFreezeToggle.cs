using System.Drawing;
using System.Windows.Forms;
using BizHawk.PSXMMX.Core;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Forms.Toggles;

public sealed class MemoryFreezeToggle : Button
{
    private readonly IMemory memory;

    public MemoryFreezeToggle(IMemory memory)
    {
        this.memory = memory ?? throw new ArgumentNullException(nameof(memory));
        Size = FormConstants.BUTTON_SIZE;
        UseVisualStyleBackColor = false;
        UpdateAppearance();
        Click += (_, _) => HandleClick();
    }

    public void UpdateAppearance()
    {
        Text = $"{(memory.IsFrozen ? "Unfreeze" : "Freeze")} {memory.Name}";
        BackColor = Constants.GetMemoryColor(memory.IsFrozen);
    }

    private void HandleClick()
    {
        memory.ToggleFreeze();
        UpdateAppearance();
    }
}
