using BizHawk.PSXMMX.Math;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Gameplay.Components;

public sealed class Motion : IComponent
{
    public GameObject GameObject { get; set; }
    public Vector2 Velocity { get; private set; }

    public Motion(long xSpeedAddress, long ySpeedAddress)
    {

    }

    public void Update() { }
}