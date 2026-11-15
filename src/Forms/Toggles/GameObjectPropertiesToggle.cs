using System.Drawing;
using System.Windows.Forms;
using BizHawk.PSXMMX.Gameplay;

namespace BizHawk.PSXMMX.Forms.Toggles;

public sealed class GameObjectPropertiesToggle : Button
{
    public GameObject GameObject = null!;

    public GameObjectPropertiesToggle()
    {
        Size = FormConstants.BUTTON_SIZE;
        Click += (_, _) => HandleClick();

        UpdateAppearance();
    }

    public void UpdateAppearance() => Text = GameObject?.IsPropertiesEnabled == true ?
        "Hide Properties" :
        "Show Properties";

    private void HandleClick()
    {
        GameObject?.ToggleShowProperties();
        UpdateAppearance();
    }
}