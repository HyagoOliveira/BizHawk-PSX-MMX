namespace BizHawk.PSXMMX.Memory;

public static class GameMemoryAddressFactory
{
    public static GameMemoryAddress? Create(string gameName)
    {
        return gameName switch
        {
            "Mega Man X4 (USA)" => CreateX4(),
            "Mega Man X5 (USA)" => CreateX5(),
            "Mega Man X6 (USA)" => CreateX6(),
            _ => null
        };
    }

    private static GameMemoryAddress CreateX4() => new(
        "Mega Man X4 (USA)",
        new Player(
            baseAddress: 0x1418C8,
            action: 0x1418CD,
            subAction: 0x1418CE,
            positionX: 0x1418D0,
            positionY: 0x1418D4,
            facing: 0x1418DD,
            lastPositionX: 0x1418E2,
            lastPositionY: 0x1418E6,
            speedX: 0x1418EA,
            speedY: 0x1418EE,
            hp: 0x141924),
        new Camera(
            baseAddress: 0x1419B0,
            positionX: 0x1419BA,
            positionY: 0x1419BE,
            hud: 0x1721DF),
        new PlayerDamage(baseAddress: 0x1406F8, disabled: 0x1721D1),
        new Enemies(baseAddress: 0x13BED0, disabled: 0x1721D2),
        new EnemyItems(baseAddress: 0x13F328, disabled: 0x1721D3));

    private static GameMemoryAddress CreateX5() => new(
        "Mega Man X5 (USA)",
        new Player(
            baseAddress: 0x09A0A0,
            action: 0x09A0A5,
            subAction: 0x09A0A6,
            positionX: 0x09A0AA,
            positionY: 0x09A0AE,
            facing: 0x09A0B5,
            lastPositionX: 0x09A0BA,
            lastPositionY: 0x09A0BE,
            speedX: 0x09A0C2,
            speedY: 0x09A0C6,
            hp: 0x09A0FC),
        new Camera(
            baseAddress: 0x09A1F8,
            positionX: 0x09A202,
            positionY: 0x09A206,
            hud: 0x0D1C1F),
        new PlayerDamage(baseAddress: 0x098120, disabled: 0x0D1C11),
        new Enemies(baseAddress: 0x092090, disabled: 0x0D1C12),
        new EnemyItems(baseAddress: 0x096D98, disabled: 0x0D1C13));

    private static GameMemoryAddress CreateX6() => new(
        "Mega Man X6 (USA)",
        new Player(
            baseAddress: 0x0970A0,
            action: 0x0970A5,
            subAction: 0x0970A6,
            positionX: 0x0970AA,
            positionY: 0x0970AE,
            facing: 0x0970B5,
            lastPositionX: 0x0970BA,
            lastPositionY: 0x0970BE,
            speedX: 0x0970C2,
            speedY: 0x0970C6,
            hp: 0x0970FC),
        new Camera(
            baseAddress: 0x0971F8,
            positionX: 0x097202,
            positionY: 0x097206,
            hud: 0x0CCEEF),
        new PlayerDamage(baseAddress: 0x0950A0, disabled: 0x0CCEE1),
        new Enemies(baseAddress: 0x08EF48, disabled: 0x0CCEE2),
        new EnemyItems(baseAddress: 0x093C98, disabled: 0x0CCEE3));
}
