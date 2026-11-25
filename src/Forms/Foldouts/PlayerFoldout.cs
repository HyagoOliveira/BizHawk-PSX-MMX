using System.Windows.Forms;
using BizHawk.PSXMMX.Core;
using BizHawk.PSXMMX.Forms.Fields;
using BizHawk.PSXMMX.Forms.Toggles;
using BizHawk.PSXMMX.Gameplay;
using BizHawk.PSXMMX.Memory;
using static BizHawk.PSXMMX.Gameplay.Player;

namespace BizHawk.PSXMMX.Forms.Foldouts;

public sealed class PlayerFoldout : FoldoutControl
{
    public Player Player { get; }

    private readonly ButtonToggle drawToggle = new("Draw");
    private readonly ButtonToggle moverToggle = new("Mover");
    private readonly ButtonToggle propertiesToggle = new("Properties");
    private readonly ButtonToggle allWeaponsToggle = new("All Weapons");
    private readonly ToggleEnumField<MMX4ArmorParts> mmx4ArmorParts = new(string.Empty);
    private readonly DropdownEnumField<MMX4Armors> mmx4Armors = new("Armor");
    private readonly DropdownEnumField<MMX5Armors> mmx5Armors = new("Armor");
    private readonly DropdownEnumField<MMX6Armors> mmx6Armors = new("Armor");
    private readonly NumericUpDownField healthField = new("Health",
        min: SByteS8Memory.MIN_VALUE,
        max: SByteS8Memory.MAX_VALUE
    );
    private readonly Label numpadMoverLabel = FormConstants.BuildNumpadMoverLabel();

    public PlayerFoldout(Player player, bool isExpanded = false) :
        base(title: "Player", isExpanded)
    {
        Player = player;
        ContentHeight = 120;

        mmx4ArmorParts.Control.IntegralHeight = false;
        mmx4ArmorParts.Control.Width = 168;
        mmx4ArmorParts.Control.Height = 74;

        mmx4ArmorParts.OnToggled += Player.EnableArmor;
        mmx4Armors.OnSelected += Player.EnableArmor;
        mmx5Armors.OnSelected += Player.EnableArmor;
        mmx6Armors.OnSelected += Player.EnableArmor;

        drawToggle.OnToggled += Player.ToggleDrawEnabled;
        moverToggle.OnToggled += Player.Mover.ToggleEnabled;
        propertiesToggle.OnToggled += Player.TogglePropertiesEnabled;
        allWeaponsToggle.OnToggled += Player.ToggleAllWeaponsEnabled;
        Player.Mover.OnEnabledChanged += HandleMoverEnabledChanged;

        healthField.AddToggleFreezeButton(Player.Health);
        healthField.Control.ValueChanged += (_, _) => WriteHealth();

        var verticalPanels = new FlowLayoutPanel[3]
        {
           GetVerticalPanel(),
           GetVerticalPanel(),
           GetVerticalPanel()
        };

        if (Game.Is(GameName.MegaManX4))
        {
            // verticalPanels[0].Controls.Add(mmx4Armors);
            verticalPanels[0].Controls.Add(mmx4ArmorParts);
        }
        // else if (Game.Is(GameName.MegaManX5)) verticalPanels[0].Controls.Add(mmx5Armors);
        // else if (Game.Is(GameName.MegaManX6)) verticalPanels[0].Controls.Add(mmx6Armors);

        verticalPanels[1].Controls.Add(drawToggle);
        verticalPanels[1].Controls.Add(propertiesToggle);
        verticalPanels[1].Controls.Add(allWeaponsToggle);

        verticalPanels[2].Controls.Add(healthField);
        verticalPanels[2].Controls.Add(moverToggle);
        verticalPanels[2].Controls.Add(numpadMoverLabel);

        ControlsPanel.Controls.Add(verticalPanels[0]);
        ControlsPanel.Controls.Add(verticalPanels[1]);
        ControlsPanel.Controls.Add(verticalPanels[2]);
    }

    public override void UpdateContent()
    {
        base.UpdateContent();

        healthField.Control.Value = Player.Health.ReadValue();

        drawToggle.UpdateText(Player.IsDrawEnabled);
        moverToggle.UpdateText(Player.Mover.IsEnabled);
        propertiesToggle.UpdateText(Player.IsPropertiesEnabled);
        allWeaponsToggle.UpdateText(Player.IsAllWeaponsEnabled());
        numpadMoverLabel.Visible = Player.Mover.IsEnabled;
    }

    private void WriteHealth()
    {
        var health = (sbyte)healthField.Control.Value;
        if (Player.Health.IsFrozen) Player.Health.Freeze(health);
        else Player.Health.WriteValue(health);
    }

    private void HandleMoverEnabledChanged(bool isEnabled) =>
        numpadMoverLabel.Visible = isEnabled;
}