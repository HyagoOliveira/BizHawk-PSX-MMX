namespace BizHawk.PSXMMX.Memory;

public sealed class GameMemoryAddress
{
    public string GameName { get; }
    public Player Player { get; }
    public Camera Camera { get; }
    public PlayerDamage PlayerDamage { get; }
    public Enemies Enemies { get; }
    public EnemyItems EnemyItems { get; }

    internal GameMemoryAddress(
        string gameName,
        Player player,
        Camera camera,
        PlayerDamage playerDamage,
        Enemies enemies,
        EnemyItems enemyItems)
    {
        GameName = gameName;
        Player = player;
        Camera = camera;
        PlayerDamage = playerDamage;
        Enemies = enemies;
        EnemyItems = enemyItems;
    }
}

public sealed class Player
{
    public long Base { get; }
    public long Action { get; }
    public long SubAction { get; }
    public long PositionX { get; }
    public long PositionY { get; }
    public long Facing { get; }
    public long LastPositionX { get; }
    public long LastPositionY { get; }
    public long SpeedX { get; }
    public long SpeedY { get; }
    public long Hp { get; }

    internal Player(
        long baseAddress,
        long action,
        long subAction,
        long positionX,
        long positionY,
        long facing,
        long lastPositionX,
        long lastPositionY,
        long speedX,
        long speedY,
        long hp)
    {
        Base = baseAddress;
        Action = action;
        SubAction = subAction;
        PositionX = positionX;
        PositionY = positionY;
        Facing = facing;
        LastPositionX = lastPositionX;
        LastPositionY = lastPositionY;
        SpeedX = speedX;
        SpeedY = speedY;
        Hp = hp;
    }
}

public sealed class Camera
{
    public long Base { get; }
    public long PositionX { get; }
    public long PositionY { get; }
    public long Hud { get; }

    internal Camera(long baseAddress, long positionX, long positionY, long hud)
    {
        Base = baseAddress;
        PositionX = positionX;
        PositionY = positionY;
        Hud = hud;
    }
}

public sealed class PlayerDamage
{
    public long Base { get; }
    public long Disabled { get; }

    internal PlayerDamage(long baseAddress, long disabled)
    {
        Base = baseAddress;
        Disabled = disabled;
    }
}

public sealed class Enemies
{
    public long Base { get; }
    public long Disabled { get; }

    internal Enemies(long baseAddress, long disabled)
    {
        Base = baseAddress;
        Disabled = disabled;
    }
}

public sealed class EnemyItems
{
    public long Base { get; }
    public long Disabled { get; }

    internal EnemyItems(long baseAddress, long disabled)
    {
        Base = baseAddress;
        Disabled = disabled;
    }
}
