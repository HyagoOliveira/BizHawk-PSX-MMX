using System.Windows.Forms;
using BizHawk.PSXMMX.Forms.Fields;
using BizHawk.PSXMMX.Forms.Toggles;
using BizHawk.PSXMMX.Gameplay;

namespace BizHawk.PSXMMX.Forms.Foldouts;

public sealed class CameraFoldout : FoldoutControl
{
    private readonly Camera camera;
    private readonly ToggleListField layerList;
    private readonly ButtonToggle hudToggle = new("HUD");
    private readonly ButtonToggle moverToggle = new("Mover");
    private readonly ButtonToggle propertiesToggle = new("Properties");
    private readonly Label numpadMoverLabel = FormConstants.BuildNumpadMoverLabel();

    public CameraFoldout(Camera camera, bool isExpanded = false) :
        base("Camera", isExpanded)
    {
        this.camera = camera;
        ContentHeight = 100;

        hudToggle.OnToggled += camera.IsHUDActive.Toggle;
        moverToggle.OnToggled += camera.Mover.ToggleEnabled;
        propertiesToggle.OnToggled += camera.TogglePropertiesEnabled;
        camera.Mover.OnEnabledChanged += HandleMoverEnabledChanged;

        layerList = new(string.Empty, [.. camera.Layers.Select(layer => layer.Name)]);
        layerList.OnToggled += camera.SetLayerEnabled;
        layerList.Control.IntegralHeight = false;
        layerList.Control.Height = 80;

        var togglesPanel = GetVerticalPanel();
        togglesPanel.Controls.Add(hudToggle);
        togglesPanel.Controls.Add(moverToggle);
        togglesPanel.Controls.Add(propertiesToggle);

        ControlsPanel.Controls.Add(layerList);
        ControlsPanel.Controls.Add(togglesPanel);
        ControlsPanel.Controls.Add(numpadMoverLabel);
    }

    public override void UpdateContent()
    {
        base.UpdateContent();
        moverToggle.UpdateText(camera.Mover.IsEnabled);
        hudToggle.UpdateText(camera.IsHUDActive.ReadValue());
        propertiesToggle.UpdateText(camera.IsPropertiesEnabled);
        numpadMoverLabel.Visible = camera.Mover.IsEnabled;

        var (indexes, isChecked) = camera.GetLayersData();
        layerList.SetWithoutNotify(indexes, isChecked);
    }

    private void HandleMoverEnabledChanged(bool isEnabled) =>
        numpadMoverLabel.Visible = isEnabled;
}