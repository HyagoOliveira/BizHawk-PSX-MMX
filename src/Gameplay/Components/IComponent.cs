namespace BizHawk.PSXMMX.Gameplay.Components;

public interface IComponent
{
    public GameObject GameObject { get; internal set; }

    void Update();
}