namespace BizHawk.PSXMMX.Gameplay.Components;

public abstract class AbstractComponent(GameObject gameObject)
{
    public GameObject GameObject { get; } = gameObject;

    public abstract void Update();
    public abstract void Draw();
}