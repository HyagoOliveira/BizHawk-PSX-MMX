using System.Drawing;
using System.Windows.Forms;
using BizHawk.PSXMMX.Gameplay;

namespace BizHawk.PSXMMX.Forms;

public sealed class GameObjectPropertiesToggle : Button
{
    private readonly GameObject gameObject;

    public GameObjectPropertiesToggle(GameObject gameObject)
    {
        this.gameObject = gameObject ?? throw new ArgumentNullException(nameof(gameObject));
        Size = new Size(130, 24);
        UpdateAppearance();
        Click += (_, _) => HandleClick();
    }

    public void UpdateAppearance() => Text = gameObject.IsPropertiesEnabled ? "Hide Properties" : "Show Properties";

    private void HandleClick()
    {
        gameObject.ToggleShowProperties();
        UpdateAppearance();
    }
}
