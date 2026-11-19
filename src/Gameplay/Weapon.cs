using BizHawk.Client.Common;

namespace BizHawk.PSXMMX.Gameplay;

public sealed class Weapon : MainObject
{
    public Weapon(long baseAddress, ApiContainer apis) :
        base(baseAddress, apis)
    {
    }

    protected override string GetProperties() =>
        $"{State} | {Substate}\n" +
        $"{Speed} | {Gravity}\n" +
        $"{Position} | {PreviousPosition}\n" +
        $"{Animation}";
}