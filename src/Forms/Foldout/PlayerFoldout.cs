using System.Windows.Forms;
using BizHawk.PSXMMX.Gameplay;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Forms.Foldout;

public sealed class PlayerFoldout : FoldoutControl
{
    public Player Player { get; }

    private readonly MemoryFreezeToggle healthToggle;
    private readonly GameObjectPropertiesToggle propertiesToggle;
    private readonly NumericUpDown healthInput = new()
    {
        Minimum = SByteS8Memory.MIN_VALUE,
        Maximum = SByteS8Memory.MAX_VALUE,
        Margin = new Padding(3, 5, 3, 3),
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
            Margin = new Padding(3, 8, 3, 3),
            Text = "Health"
        };
        healthToggle = new MemoryFreezeToggle(Player.Health);
        propertiesToggle = new GameObjectPropertiesToggle(Player);

        healthInput.ValueChanged += (_, _) => WriteHealth();

        var controls = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(3),
            WrapContents = false
        };

        controls.Controls.Add(healthLabel);
        controls.Controls.Add(healthInput);
        controls.Controls.Add(healthToggle);
        controls.Controls.Add(propertiesToggle);

        ContentPanel.Controls.Add(controls);
    }

    public override void UpdateContent()
    {
        base.UpdateContent();

        healthInput.Value = Player.Health.ReadValue();
        healthToggle.UpdateAppearance();
        propertiesToggle.UpdateAppearance();
    }

    private void WriteHealth()
    {
        var health = (sbyte)healthInput.Value;
        if (Player.Health.IsFrozen) Player.Health.Freeze(health);
        else Player.Health.WriteValue(health);
    }
}