using BizHawk.Client.Common;

namespace BizHawk.PSXMMX.Memory;

/// <summary>
/// Reads the current animation index and the 4-byte AnimationStep copied from animation_cursor.
/// </summary>
public sealed class AnimationMemory : AbstractMemory<AnimationMemory.Step>
{
    public readonly record struct Step(
        byte Index,
        sbyte Duration,
        sbyte TotalFrames,
        sbyte Event,
        sbyte RelativeStep,
        byte SpriteIndex
    )
    {
        // Duration counts down from TotalDuration to 1 while the step is displayed.
        public int CurrentFrame => TotalFrames - Duration + 1;

        // What the game does when this step's duration ends.
        public string NextAction => RelativeStep switch
        {
            > 0 => $"next step (+{RelativeStep})",
            < 0 => $"loop back {-RelativeStep} step(s)",
            _ => "repeat this step"
        };
    }

    public long StepAddress { get; }
    public long CursorAddress { get; }

    private const uint PS1_RAM_MASK = 0x001FFFFF;

    public AnimationMemory(string name, long indexAddress, long stepAddress, long cursorAddress, IMemoryApi api) :
        base(name, indexAddress, api)
    {
        StepAddress = stepAddress;
        CursorAddress = cursorAddress;
    }

    public override Step ReadValue() => new(
        (byte)MemoryApi.ReadU8(Address, DOMAIN),
        (sbyte)MemoryApi.ReadS8(StepAddress, DOMAIN),
        ReadTotalDuration(),
        (sbyte)MemoryApi.ReadS8(StepAddress + 1, DOMAIN),
        (sbyte)MemoryApi.ReadS8(StepAddress + 2, DOMAIN),
        (byte)MemoryApi.ReadU8(StepAddress + 3, DOMAIN)
    );

    // Read-only: the animation state is driven by the game.
    public override void WriteValue(Step value) { }

    public override string ToString() =>
        $"{Name}: {Value.Index} | " +
        $"Frame {Value.CurrentFrame}/{Value.TotalFrames} | " +
        $"Sprite {Value.SpriteIndex} | " +
        $"Ev {Value.Event} | " +
        $"Then: {Value.NextAction}";

    // The cursor points to the original step, whose duration is not decremented.
    private sbyte ReadTotalDuration()
    {
        var pointer = MemoryApi.ReadU32(CursorAddress, DOMAIN);
        return (sbyte)MemoryApi.ReadS8(pointer & PS1_RAM_MASK, DOMAIN);
    }
}
