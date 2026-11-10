using BizHawk.PSXMMX.Math;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Gameplay.Components;

public sealed class Transform : AbstractComponent
{
    public Vector2 Position { get; private set; }

    public Transform(GameObject gameObject, long xAddress, long yAddress) : base(gameObject)
    {

    }

    public override void Update() { }
}