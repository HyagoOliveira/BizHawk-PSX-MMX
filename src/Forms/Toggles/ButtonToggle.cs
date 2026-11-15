using System.Windows.Forms;

namespace BizHawk.PSXMMX.Forms.Toggles;

public sealed class ButtonToggle : Button
{
    private readonly string text;

    public event Action OnToggle = null!;
    public Func<bool> IsToggledFunction = null!;

    public ButtonToggle(string text)
    {
        this.text = text;

        Size = FormConstants.BUTTON_SIZE;
        Click += (_, _) => HandleClick();

        UpdateText();
    }

    public void UpdateText()
    {
        var action = IsToggledFunction?.Invoke() == true ? "Disable" : "Enable";
        Text = $"{action} {text}";
    }

    private void HandleClick()
    {
        OnToggle?.Invoke();
        UpdateText();
    }
}