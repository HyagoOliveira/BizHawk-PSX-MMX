using System.Drawing;
using BizHawk.Client.Common;
using BizHawk.PSXMMX.Math;

namespace BizHawk.PSXMMX.Memory;

public abstract class AbstractMemory<T>(string name, long address, IMemoryApi api)
{
    public string Name { get; } = name;
    public long Address { get; } = address;
    public IMemoryApi MemoryApi { get; } = api;

    public bool IsFrozen { get; private set; } = false;
    public T Value { get; private set; } = default!;
    public T FrozenValue { get; private set; } = default!;

    public void Update()
    {
        if (IsFrozen) WriteValue(FrozenValue);
        Value = ReadValue();
    }

    public void Draw(Vector2Int position, IGuiApi gui)
    {
        var color = IsFrozen ? Color.Cyan : Color.White;
        gui.DrawText(position, ToString(), color);
    }

    public void ToggleFreeze()
    {
        if (IsFrozen) Unfreeze();
        else Freeze();
    }

    public void Freeze() => Freeze(ReadValue());

    public void Freeze(T value)
    {
        IsFrozen = true;
        FrozenValue = value;
    }

    public void Unfreeze()
    {
        IsFrozen = false;
        FrozenValue = default!;
    }

    protected void WriteValueAndUpdate(T value)
    {
        WriteValue(value);
        Value = value;
        if (IsFrozen) FrozenValue = value;
    }

    public override string ToString() => $"{Name}: {Value}";

    protected abstract T ReadValue();
    protected abstract void WriteValue(T value);
}