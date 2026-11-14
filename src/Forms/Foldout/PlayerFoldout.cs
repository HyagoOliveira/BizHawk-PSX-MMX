using System.Drawing;
using System.Windows.Forms;
using BizHawk.PSXMMX.Gameplay;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Forms.Foldout;

public sealed class PlayerFoldout : FoldoutControl
{
    public Player Player { get; }

    private readonly NumericUpDown healthInput = new()
    {
        Minimum = SByteS8Memory.MIN_VALUE,
        Maximum = SByteS8Memory.MAX_VALUE,
        Location = new Point(72, 6),
        Width = 100,
        TextAlign = HorizontalAlignment.Right
    };

    public PlayerFoldout(Player player, bool isExpanded = false) :
        base(title: "Player", isExpanded)
    {
        Player = player;
        ContentHeight = 38;

        var healthLabel = new Label
        {
            AutoSize = true,
            Location = new Point(8, 10),
            Text = "Health"
        };

        healthInput.Enabled = player is not null;
        healthInput.ValueChanged += (_, _) => WriteHealth();

        ContentPanel.Controls.Add(healthLabel);
        ContentPanel.Controls.Add(healthInput);
    }

    public override void UpdateContent()
    {
        base.UpdateContent();
        healthInput.Value = Player.Health.ReadValue();
    }

    private void WriteHealth()
    {
        var health = (sbyte)healthInput.Value;
        if (Player.Health.IsFrozen) Player.Health.Freeze(health);
        else Player.Health.WriteValue(health);
    }
}