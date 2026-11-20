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

    private readonly ButtonToggle allWeaponsToggle = new("Weapons");
    private readonly GameObjectPropertiesToggle propertiesToggle = new();
    private readonly DropdownEnumField<PlayerArmorPart> armorDropdown = new("Armor");
    private readonly NumericUpDownField healthField = new(
        "Health",
        min: SByteS8Memory.MIN_VALUE,
        max: SByteS8Memory.MAX_VALUE
    );

    public PlayerFoldout(Player player, bool isExpanded = false) :
        base(title: "Player", isExpanded)
    {
        Player = player;
        ContentHeight = 68;

        allWeaponsToggle.OnToggled += Player.ToggleAllWeaponsEnabled;
        propertiesToggle.GameObject = Player;
        armorDropdown.OnSelected += Player.EnableArmorPart;

        healthField.AddToggleFreezeButton(Player.Health);
        healthField.Control.ValueChanged += (_, _) => WriteHealth();

        ControlsPanel.Controls.Add(healthField);
        ControlsPanel.Controls.Add(armorDropdown);
        ControlsPanel.Controls.Add(propertiesToggle);
        ControlsPanel.Controls.Add(allWeaponsToggle);
        ControlsPanel.SetFlowBreak(armorDropdown, true);
    }

    public override void UpdateContent()
    {
        base.UpdateContent();

        healthField.Control.Value = Player.Health.ReadValue();
        propertiesToggle.UpdateAppearance();
        allWeaponsToggle.UpdateText(Player.IsAllWeaponsEnabled());
    }

    private void WriteHealth()
    {
        var health = (sbyte)healthField.Control.Value;
        if (Player.Health.IsFrozen) Player.Health.Freeze(health);
        else Player.Health.WriteValue(health);
    }
}