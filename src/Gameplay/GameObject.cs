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
    public bool IsDrawEnabled { get; set; } = true;

    // Variables
    public BoolS8Memory IsActive { get; }   // active (whether slot is active)
    public UIntS8Memory Id { get; }         // id (pool identifier)
    public BoolS8Memory IsVisible { get; }  // on_screen (whether is visible)
    public UIntS8Memory MainState { get; }  // state (initialization, behavior or despawn)
    public Vector2S32Memory Position { get; }
    public Vector2S32Memory PreviousPosition { get; }

    public int TotalVariables => variables.Count;
    public int TotalComponents => components.Count;

    private readonly List<IMemory> variables = [];
    private readonly List<AbstractComponent> components = [];

    public GameObject(long baseAddress, ApiContainer apis)
    {
        APIs = apis;
        Name = GetType().Name;
        BaseAddress = baseAddress;

        #region VARIABLES INITIALIZATION
        IsActive = AddVariable(
            new BoolS8Memory(
                "Active",
                BaseAddress + 0x00,
                APIs.Memory,
                hideDisplay: true
            )
        );
        Id = AddVariable(
            new UIntS8Memory(
                "Id",
                BaseAddress + 0x01,
                APIs.Memory,
                hideDisplay: true
            )
        );
        IsVisible = AddVariable(
            new BoolS8Memory(
                "OnScreen",
                BaseAddress + 0x03,
                APIs.Memory
            )
        );
        MainState = AddVariable(
            new UIntS8Memory(
                "State",
                BaseAddress + 0x04,
                APIs.Memory
            )
        );
        Position = AddVariable(
            new Vector2S32Memory(
                "Pos",
                xAddress: BaseAddress + 0x08,
                yAddress: BaseAddress + 0x0C,
                APIs.Memory
            )
        );
        PreviousPosition = AddVariable(
            new Vector2S32Memory(
                "Prev Pos",
                xAddress: BaseAddress + 0x18,
                yAddress: BaseAddress + 0x1C,
                APIs.Memory
            )
        );
        #endregion

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

        if (IsDrawEnabled) Draw();
    }

    public virtual void Draw()
    {
        var position = Camera.Main.WorldToScreenPoint(Position.Value);
        APIs.DrawAxis(position);
    }

    public void DrawVariables(Vector2Int position)
    {
        APIs.DrawText(position, Name);
        foreach (var variable in variables)
        {
            if (variable.HideDisplay) continue;

            position.Y += 10;
            variable.Draw(position, APIs);
        }
    }
}