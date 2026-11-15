using System.Drawing;
using System.Windows.Forms;
using BizHawk.PSXMMX.Gameplay;

namespace BizHawk.PSXMMX.Forms.Toggles;

public sealed class ButtonToggle : Button
{
    private readonly string text;
    private readonly Action toggleAction;
    private readonly Func<bool> isToggled;

    public ButtonToggle(string text, Action toggleAction, Func<bool> isToggled)
    {
        this.text = text;
        this.toggleAction = toggleAction;
        this.isToggled = isToggled;

        Size = new Size(130, 24);
        Click += (_, _) => HandleClick();
        UpdateText();
    }

    public void UpdateText()
    {
        var action = isToggled() ? "Disable" : "Enable";
        Text = $"{action} {text}";
    }

    private void HandleClick()
    {
        toggleAction?.Invoke();
        UpdateText();
    }
}
