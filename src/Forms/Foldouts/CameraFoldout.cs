using BizHawk.PSXMMX.Forms.Fields;
using BizHawk.PSXMMX.Forms.Toggles;
using BizHawk.PSXMMX.Gameplay;

namespace BizHawk.PSXMMX.Forms.Foldouts;

public sealed class CameraFoldout : FoldoutControl
{
    private readonly Camera camera;
    private readonly ButtonToggle hudToggle = new("HUD");
    private readonly GameObjectPropertiesToggle propertiesToggle = new();

    public CameraFoldout(Camera camera, bool isExpanded = false) :
        base("Camera", isExpanded)
    {
        this.camera = camera;

        propertiesToggle.GameObject = camera;
        hudToggle.OnToggled += camera.IsHUDActive.Toggle;

        ControlsPanel.Controls.Add(hudToggle);
        ControlsPanel.Controls.Add(propertiesToggle);
    }

    public override void UpdateContent()
    {
        base.UpdateContent();
        propertiesToggle.UpdateAppearance();
        hudToggle.UpdateText(camera.IsHUDActive.ReadValue());
    }
}