using BizHawk.Client.Common;
using BizHawk.PSXMMX.Gameplay.Components;
using BizHawk.PSXMMX.Math;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Gameplay;

public class GameObject
{
    public long BaseAddress { get; }
    public int PoolIndex { get; internal set; } = -1;
    public ApiContainer APIs { get; }

    public string Name { get; private set; }
    public bool IsDrawEnabled { get; set; }
    public bool IsPropertiesEnabled { get; set; }
    public string? PropertiesVerticalAlign { get; protected set; } = null!;
    public string? PropertiesHorizontalAlign { get; protected set; } = null!;

    // Variables
    public BoolS8Memory IsActive { get; }       // active (whether slot is active)
    public UIntS8Memory Id { get; }             // id (pool identifier)
    public BoolS8Memory IsVisible { get; }      // on_screen (whether is visible)
    public UIntS8Memory MainState { get; }      // state (initialization, behavior or despawn)
    public Vector2S32Memory Position { get; }   // x_pos, y_pos
    public SByteS8Memory State { get; }            // unk5
    public SByteS8Memory Substate { get; }      // unk6
    public Vector2S32Memory PreviousPosition { get; }

    // Components
    public KeypadMover Mover { get; private set; }

    public int TotalVariables => variables.Count;
    public int TotalComponents => components.Count;

    private readonly List<IMemory> variables = [];
    private readonly List<AbstractComponent> components = [];

    public GameObject(long baseAddress, ApiContainer apis)
    {
        APIs = apis;
        BaseAddress = baseAddress;
        Name = GetDeactivatedName();

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
                APIs.Memory,
                hideDisplay: true
            )
        );
        MainState = AddVariable(
            new UIntS8Memory(
                "State",
                BaseAddress + 0x04,
                APIs.Memory,
                hideDisplay: true
            )
        );
        State = AddVariable(
            new SByteS8Memory(
                "State",
                baseAddress + 0x05,
                APIs.Memory
            )
        );
        Substate = AddVariable(
            new SByteS8Memory(
                "Substate",
                baseAddress + 0x06,
                APIs.Memory,
                hideDisplay: true
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

        Mover = AddComponent(new KeypadMover(this));
        // Each implementation should initialize its own variables and components in the constructor.
    }

    public T AddComponent<T>(T component) where T : AbstractComponent
    {
        components.Add(component);
        return component;
    }

    public T AddVariable<T>(T variable) where T : IMemory
    {
        variables.Add(variable);
        return variable;
    }

    public bool IsActivated() => IsActive.Value;
    public bool IsVisibleOnScreen() => IsVisible.Value;
    public bool CanDraw() => IsVisibleOnScreen() && IsDrawEnabled;
    public bool CanDrawProperties() => IsVisibleOnScreen() && IsPropertiesEnabled;

    public override string ToString() => Name;
    public void ToggleDrawEnabled() => IsDrawEnabled = !IsDrawEnabled;
    public void TogglePropertiesEnabled() => IsPropertiesEnabled = !IsPropertiesEnabled;

    public void UpdateAndTryDraw()
    {
        Update();
        TryDraw();
    }

    public void TryDraw()
    {
        if (!IsActivated()) return;
        if (CanDraw()) Draw();
        if (CanDrawProperties()) DrawProperties();
    }

    public virtual void Update()
    {
        UpdateName();
        foreach (var variable in variables) variable.Update();
        foreach (var component in components) component.TryUpdate();
    }

    public virtual void Draw()
    {
        foreach (var component in components) component.TryDraw();

        DrawInGameName();
        APIs.DrawAxis(Camera.Main.WorldToScreenPoint(Position.Value));
    }

    protected virtual void DrawInGameName() => APIs.DrawText(
       GetInGameNamePosition(),
       Name,
       horizontalAlign: "center",
       verticalAlign: "bottom"
   );

    protected virtual string GetInGameName() => string.Empty;
    protected virtual Vector2Int GetInGameNamePosition() => Position.Value.ToVector2Int();

    protected virtual void DrawProperties() => APIs.DrawText(
         GetPropertiesPosition(),
         GetProperties(),
         horizontalAlign: PropertiesHorizontalAlign,
         verticalAlign: PropertiesVerticalAlign
     );

    protected virtual Vector2Int GetPropertiesPosition() =>
        Camera.Main.WorldToScreenPoint(Position.Value);

    protected virtual string GetProperties()
    {
        var properties = string.Empty;
        foreach (var variable in variables)
        {
            if (variable.HideDisplay) continue;
            properties += variable.ToString() + "\n";
        }
        return properties;
    }

    private void UpdateName() => Name = IsActivated() ? GetActivatedName() : GetDeactivatedName();
    private string GetDeactivatedName() => $"{GetType().Name} - DEACTIVATED";
    private string GetActivatedName() => $"{GetInGameName()} | Id:{Id.Value:D2} | S:{PoolIndex:D2}";
}