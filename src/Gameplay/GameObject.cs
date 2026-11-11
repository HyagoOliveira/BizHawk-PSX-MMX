using System.Drawing;
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
    public Fixed16Vector2Memory Position { get; private set; }

    private readonly List<IMemory> variables = [];
    private readonly List<AbstractComponent> components = [];

    public GameObject(long baseAddress, ApiContainer apis)
    {
        APIs = apis;
        Name = GetType().Name;
        BaseAddress = baseAddress;

        Position = AddVariable(new Fixed16Vector2Memory(
            "Position",
            BaseAddress + 0x08,
            offset: 0x04,
            APIs.Memory
        ));
        // Each implementation should initialize its own variables and components in the constructor.
    }

    public T AddComponent<T>(T component) where T : AbstractComponent
    {
        if (component is null) throw new ArgumentNullException(nameof(component));
        components.Add(component);
        return component;
    }

    public T AddVariable<T>(T variable, int? displayOrder = null) where T : IMemory
    {
        if (variable is null) throw new ArgumentNullException(nameof(variable));

        if (displayOrder is int index && (index < 0 || index > variables.Count))
        {
            throw new ArgumentOutOfRangeException(nameof(displayOrder));
        }

        if (displayOrder is int displayIndex) variables.Insert(displayIndex, variable);
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
        //TODO draw Position
        /*var position = Position.Value.ToVector2Int();
        position.Y = 40;
        APIs.DrawAxis(position);*/
    }

    public virtual void DrawVariables(Vector2Int position)
    {
        APIs.DrawText(position, Name);
        position.X += 4;

        foreach (var variable in variables)
        {
            position.Y += 10;
            variable.Draw(position, APIs);
        }
    }

    protected void DrawInsideEmulator(Vector2Int position, Color color)
    {
        // var clientPosition = APIs.EmuClient.TransformPoint(new Point(position.X, position.Y));
        // APIs.Gui.DrawAxis(clientPosition, color, surfaceID: DisplaySurfaceID.Client);
    }
}