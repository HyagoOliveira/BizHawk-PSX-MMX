using BizHawk.Client.Common;
using BizHawk.PSXMMX.Gameplay.Components;

namespace BizHawk.PSXMMX.Gameplay;

public class GameObject
{
    public long BaseAddress { get; private set; }
    public Transform Transform { get; private set; }
    public IMemoryApi MemoryApi { get; private set; }

    private readonly List<IComponent> components = [];

    public GameObject(IMemoryApi memoryApi, long baseAddress)
    {
        MemoryApi = memoryApi;
        BaseAddress = baseAddress;

        Transform = AddComponent(new Transform(
            BaseAddress + 0, //TODO find offset addresses
            BaseAddress + 0
        ));
        AddInitialComponents();
    }

    public T AddComponent<T>(T component) where T : IComponent
    {
        if (component is null) throw new ArgumentNullException(nameof(component));
        component.GameObject = this;
        components.Add(component);
        return component;
    }

    public T? GetComponent<T>() where T : class, IComponent => components.OfType<T>().FirstOrDefault();

    public virtual void Update()
    {
        foreach (var component in components) component.Update();
    }

    protected virtual void AddInitialComponents() { }
}