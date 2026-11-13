using BizHawk.Client.Common;
using BizHawk.PSXMMX.Gameplay.Components;
using BizHawk.PSXMMX.Math;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Gameplay;

public class GameObject
{
    public long BaseAddress { get; }
    public ApiContainer APIs { get; }

    public string Name { get; protected set; }
    public Vector2S32Memory Position { get; private set; }

    private readonly List<IMemory> variables = [];
    private readonly List<AbstractComponent> components = [];

    public GameObject(long baseAddress, ApiContainer apis)
    {
        APIs = apis;
        Name = GetType().Name;
        BaseAddress = baseAddress;

        Position = AddVariable(
            new Vector2S32Memory(
                "Pos",
                BaseAddress + 0x08,
                offset: 0x04,
                APIs.Memory
            )
        );
        // Each implementation should initialize its own variables and components in the constructor.
    }

    public T AddComponent<T>(T component) where T : AbstractComponent
    {
        components.Add(component);
        return component;
    }

    public T AddVariable<T>(T variable, int? displayOrder = null) where T : IMemory
    {
        if (displayOrder is int index) variables.Insert(index, variable);
        else variables.Add(variable);
        return variable;
    }

    public virtual void Update()
    {
        foreach (var variable in variables) variable.Update();
        foreach (var component in components) component.Update();
        Draw();
    }

    public virtual void Draw()
    {
        var position = Camera.Main.WorldToScreenPoint(Position.Value);
        APIs.DrawAxis(position);
    }

    public void DrawVariables(Vector2Int position)
    {
        APIs.DrawText(position, Name);
        position.X += 4;

        foreach (var variable in variables)
        {
            if (variable.HideDraw) continue;

            position.Y += 10;
            variable.Draw(position, APIs);
        }
    }
}