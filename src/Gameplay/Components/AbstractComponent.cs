namespace BizHawk.PSXMMX.Gameplay.Components;

public abstract class AbstractComponent
{
    public bool IsEnabled { get; private set; }
    public GameObject GameObject { get; }

    public AbstractComponent(GameObject gameObject, bool isEnabled = true)
    {
        IsEnabled = isEnabled;
        GameObject = gameObject;
    }

    public void ToggleEnabled() => SetEnabled(!IsEnabled);

    public void SetEnabled(bool isEnabled)
    {
        if (IsEnabled == isEnabled) return;

        IsEnabled = isEnabled;
        if (IsEnabled) OnEnabled();
        else OnDisabled();
    }

    public void TryUpdate()
    {
        if (IsEnabled) Update();
    }

    public void TryDraw()
    {
        if (IsEnabled) Draw();
    }

    protected abstract void Update();
    protected abstract void Draw();

    protected virtual void OnEnabled() { }
    protected virtual void OnDisabled() { }
}