using BizHawk.PSXMMX.Math;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Gameplay.Components;

public sealed class Motion : AbstractComponent
{
    public Vector2 Velocity { get; private set; }

    public Motion(GameObject gameObject, long xSpeedAddress, long ySpeedAddress) : base(gameObject)
    {

    }

    public override void Update() { }
}