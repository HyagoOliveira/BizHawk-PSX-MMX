using System.Drawing;
using BizHawk.Client.Common;
using BizHawk.PSXMMX.Math;

namespace BizHawk.PSXMMX.Memory;

public abstract class AbstractMemory<T> : IMemory
{
    public string Name { get; }
    public long Address { get; }
    public IMemoryApi MemoryApi { get; }

    public bool HideDraw { get; set; }
    public bool IsFrozen { get; private set; }

    public T Value { get; private set; } = default!;
    public T FrozenValue { get; private set; } = default!;

    public const string DOMAIN = GameMemoryAddress.DOMAIN;

    public AbstractMemory(string name, long address, IMemoryApi api)
    {
        Name = name;
        MemoryApi = api;
        Address = address;
    }

    public void Update()
    {
        if (IsFrozen) WriteValue(FrozenValue);
        Value = ReadValue();
    }

    public void Draw(Vector2Int position, ApiContainer apis)
    {
        var color = IsFrozen ? Color.Cyan : Color.White;
        apis.DrawText(position, ToString(), color);
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

    public override string ToString() => $"{Name}: {Value}";

    protected void ForceToWriteValue(T value)
    {
        WriteValue(value);
        Value = value;
        if (IsFrozen) FrozenValue = value;
    }

    protected abstract T ReadValue();
    protected abstract void WriteValue(T value);
}