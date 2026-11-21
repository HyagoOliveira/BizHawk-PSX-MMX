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
    private readonly GameObjectPropertiesToggle propertiesToggle = new();

    public CameraFoldout(Camera camera, bool isExpanded = false) :
        base("Camera", isExpanded)
    {
        this.camera = camera;

        propertiesToggle.GameObject = camera;
        hudToggle.OnToggled += camera.IsHUDActive.Toggle;

        layerList = new(string.Empty, [.. camera.Layers.Select(layer => layer.Name)]);
        layerList.OnToggled += camera.SetLayerEnabled;
        layerList.Control.IntegralHeight = false;
        layerList.Control.Height = GetHeight(layerList.Control.Margin.Vertical);

        var togglesPanel = new FlowLayoutPanel
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            FlowDirection = FlowDirection.TopDown,
            Margin = Padding.Empty,
            WrapContents = false
        };
        togglesPanel.Controls.Add(hudToggle);
        togglesPanel.Controls.Add(propertiesToggle);

        ControlsPanel.Controls.Add(layerList);
        ControlsPanel.Controls.Add(togglesPanel);
    }

    public override void UpdateContent()
    {
        base.UpdateContent();
        propertiesToggle.UpdateAppearance();
        hudToggle.UpdateText(camera.IsHUDActive.ReadValue());

        var (indexes, isChecked) = camera.GetLayersData();
        layerList.SetWithoutNotify(indexes, isChecked);
    }

    private int GetHeight(int verticalMargin) => ControlsPanel.ClientSize.Height - ControlsPanel.Padding.Vertical - verticalMargin;
}