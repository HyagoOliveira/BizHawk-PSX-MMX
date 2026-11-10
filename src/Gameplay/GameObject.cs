using BizHawk.Client.Common;
using BizHawk.PSXMMX.Gameplay.Components;
using BizHawk.PSXMMX.Math;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Gameplay;

public class GameObject
{
    public long BaseAddress { get; private set; }
    public Transform Transform { get; private set; }
    public IMemoryApi MemoryApi { get; private set; }

    private readonly List<IComponent> components = [];
    private readonly List<IMemory> variables = new();

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

    public T AddVariable<T>(T variable) where T : IMemory
    {
        if (variable is null) throw new ArgumentNullException(nameof(variable));
        variables.Add(variable);
        return variable;
    }

    public virtual void Update()
    {
        foreach (var variable in variables) variable.Update();
        foreach (var component in components) component.Update();
    }

    public void DrawVariables(IGuiApi gui, Vector2Int position)
    {
        foreach (var variable in variables)
        {
            variable.Draw(position, gui);
            position = new Vector2Int(position.X, position.Y + 20);
        }
    }

    protected virtual void AddInitialComponents() { }
}