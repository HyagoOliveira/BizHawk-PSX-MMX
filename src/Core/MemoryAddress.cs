namespace BizHawk.PSXMMX.Core;

public sealed class MemoryAddress
{
    public long Player { get; }
    public long Camera { get; }
    public long BossFlags { get; }
    public long MainObjectsPool { get; }

    public MemoryAddress(
        long player = 0,
        long camera = 0,
        long bossFlags = 0,
        long mainObjectsPool = 0
    )
    {
        Player = player;
        Camera = camera;
        BossFlags = bossFlags;
        MainObjectsPool = mainObjectsPool;
    }
}