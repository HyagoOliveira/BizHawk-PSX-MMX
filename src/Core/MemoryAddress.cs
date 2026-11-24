namespace BizHawk.PSXMMX.Core;

public sealed class MemoryAddress
{
    public long Player { get; }
    public long BossFlags { get; }
    public long MainObjectsPool { get; }
    public long ShotObjectsPool { get; }
    public long WeaponObjectsPool { get; }

    public CameraAddress Camera { get; }

    public MemoryAddress(
        long player = 0,
        long bossFlags = 0,
        long mainObjectsPool = 0,
        long shotObjectsPool = 0,
        long weaponObjectsPool = 0,
        CameraAddress? camera = null
    )
    {
        Player = player;
        BossFlags = bossFlags;
        MainObjectsPool = mainObjectsPool;
        ShotObjectsPool = shotObjectsPool;
        WeaponObjectsPool = weaponObjectsPool;
        Camera = camera ?? new CameraAddress();
    }

    public sealed class CameraAddress
    {
        public long Base { get; }
        public long HUD { get; }

        public const long SIZE = 0x54;

        public CameraAddress(long @base = 0, long hud = 0)
        {
            Base = @base;
            HUD = hud;
        }
    }
}