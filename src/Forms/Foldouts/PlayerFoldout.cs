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

    private readonly ButtonToggle propertiesToggle = new("Properties");
    private readonly ButtonToggle allWeaponsToggle = new("All Weapons");
    private readonly ToggleEnumField<PlayerArmorPart> armorParts = new(string.Empty);
    private readonly NumericUpDownField healthField = new("Health",
        min: SByteS8Memory.MIN_VALUE,
        max: SByteS8Memory.MAX_VALUE
    );

    public PlayerFoldout(Player player, bool isExpanded = false) :
        base(title: "Player", isExpanded)
    {
        Player = player;
        ContentHeight = 100;

        armorParts.Control.IntegralHeight = false;
        armorParts.Control.Height = 80;

        armorParts.OnToggled += Player.EnableArmorPart;
        propertiesToggle.OnToggled += Player.ToggleShowProperties;
        allWeaponsToggle.OnToggled += Player.ToggleAllWeaponsEnabled;

        healthField.AddToggleFreezeButton(Player.Health);
        healthField.Control.ValueChanged += (_, _) => WriteHealth();

        var verticalPanel = GetVerticalPanel();
        verticalPanel.Controls.Add(healthField);
        verticalPanel.Controls.Add(propertiesToggle);
        verticalPanel.Controls.Add(allWeaponsToggle);

        ControlsPanel.Controls.Add(armorParts);
        ControlsPanel.Controls.Add(verticalPanel);
    }

    public override void UpdateContent()
    {
        base.UpdateContent();

        healthField.Control.Value = Player.Health.ReadValue();
        propertiesToggle.UpdateText(Player.IsPropertiesEnabled);
        allWeaponsToggle.UpdateText(Player.IsAllWeaponsEnabled());
    }

    private void WriteHealth()
    {
        var health = (sbyte)healthField.Control.Value;
        if (Player.Health.IsFrozen) Player.Health.Freeze(health);
        else Player.Health.WriteValue(health);
    }
}