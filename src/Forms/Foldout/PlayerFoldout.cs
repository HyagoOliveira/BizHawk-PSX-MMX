using System.Drawing;
using System.Windows.Forms;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Forms.Foldout;

public sealed class PlayerFoldout : FoldoutControl
{
    private readonly Button freezePlayerHpButton = new();
    private UIntMemory? playerHP;

    public PlayerFoldout() : base("Player", isExpanded: true)
    {
        ContentHeight = 64;

        freezePlayerHpButton.Location = new Point(10, 8);
        freezePlayerHpButton.AutoSize = true;
        freezePlayerHpButton.Enabled = false;

        var shortcutLabel = new Label
        {
            AutoSize = true,
            Location = new Point(10, 36),
            Text = "T: Freeze or unfreeze Player HP"
        };

        ContentPanel.Controls.Add(freezePlayerHpButton);
        ContentPanel.Controls.Add(shortcutLabel);

        freezePlayerHpButton.Click += (_, _) => ToggleFreeze();
        UpdateFreezeButton();
    }

    public void SetMemory(UIntMemory? memory)
    {
        playerHP = memory;
        freezePlayerHpButton.Enabled = memory is not null;
        UpdateFreezeButton();
    }

    public void UpdateFreezeButton()
    {
        freezePlayerHpButton.Text = playerHP?.IsFrozen == true ? "Unfreeze HP" : "Freeze HP";
    }

    public void ToggleFreeze()
    {
        if (playerHP is null) return;

        playerHP.ToggleFreeze();
        UpdateFreezeButton();
    }
}
