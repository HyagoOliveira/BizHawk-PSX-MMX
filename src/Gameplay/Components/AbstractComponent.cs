namespace BizHawk.PSXMMX.Gameplay.Components;

public abstract class AbstractComponent
{
    public GameObject GameObject { get; }

    public AbstractComponent(GameObject gameObject)
    {
        GameObject = gameObject;
    }

    public abstract void Update();
}