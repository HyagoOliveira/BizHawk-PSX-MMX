using BizHawk.PSXMMX.Math;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Gameplay.Components;

public sealed class Transform : IComponent
{
    public GameObject GameObject { get; set; }
    public Vector2 Position { get; private set; }

    public Transform(long xAddress, long yAddress)
    {

    }

    public void Update() { }
}