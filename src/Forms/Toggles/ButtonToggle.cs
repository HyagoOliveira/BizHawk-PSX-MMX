using System.Windows.Forms;

namespace BizHawk.PSXMMX.Forms.Toggles;

public sealed class ButtonToggle : Button
{
    public event Action OnToggled = null!;
    public bool IsEnabled { get; private set; }
    private readonly string text;

    public ButtonToggle(string text, bool isEnabled = false)
    {
        this.text = text;

        Size = FormConstants.BUTTON_SIZE;
        Click += (_, _) => HandleClick();

        UpdateText(isEnabled);
    }

    public void UpdateText(bool isEnabled)
    {
        IsEnabled = isEnabled;
        var action = IsEnabled ? "Disable" : "Enable";
        Text = $"{action} {text}";
    }

    private void HandleClick()
    {
        OnToggled?.Invoke();
        UpdateText(!IsEnabled);
    }
}