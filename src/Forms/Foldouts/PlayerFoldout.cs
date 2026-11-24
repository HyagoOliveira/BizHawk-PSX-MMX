using System.Windows.Forms;
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
    private readonly ToggleEnumField<PlayerArmorPart> armorParts = new(string.Empty);
    private readonly NumericUpDownField healthField = new("Health",
        min: SByteS8Memory.MIN_VALUE,
        max: SByteS8Memory.MAX_VALUE
    );
    private readonly Label numpadMoverLabel = FormConstants.BuildNumpadMoverLabel();

    public PlayerFoldout(Player player, bool isExpanded = false) :
        base(title: "Player", isExpanded)
    {
        Player = player;
        ContentHeight = 100;

        armorParts.Control.IntegralHeight = false;
        armorParts.Control.Height = 80;

        armorParts.OnToggled += Player.EnableArmorPart;
        drawToggle.OnToggled += Player.ToggleDrawEnabled;
        moverToggle.OnToggled += Player.Mover.ToggleEnabled;
        propertiesToggle.OnToggled += Player.TogglePropertiesEnabled;
        allWeaponsToggle.OnToggled += Player.ToggleAllWeaponsEnabled;
        Player.Mover.OnEnabledChanged += HandleMoverEnabledChanged;

        healthField.AddToggleFreezeButton(Player.Health);
        healthField.Control.ValueChanged += (_, _) => WriteHealth();

        var verticalPanels = new FlowLayoutPanel[2]
        {
           GetVerticalPanel(),
           GetVerticalPanel()
        };

        verticalPanels[0].Controls.Add(drawToggle);
        verticalPanels[0].Controls.Add(propertiesToggle);
        verticalPanels[0].Controls.Add(allWeaponsToggle);

        verticalPanels[1].Controls.Add(healthField);
        verticalPanels[1].Controls.Add(moverToggle);
        verticalPanels[1].Controls.Add(numpadMoverLabel);

        ControlsPanel.Controls.Add(armorParts);
        ControlsPanel.Controls.Add(verticalPanels[0]);
        ControlsPanel.Controls.Add(verticalPanels[1]);
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