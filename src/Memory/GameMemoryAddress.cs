namespace BizHawk.PSXMMX.Memory;

public sealed class GameMemoryAddress
{
    public long PlayerHP { get; }

    internal GameMemoryAddress(long playerHP)
    {
        PlayerHP = playerHP;
    }

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

    private static GameMemoryAddress CreateX4() => new(playerHP: 0x141924);
    private static GameMemoryAddress CreateX5() => new(playerHP: 0x09A0FC);
    private static GameMemoryAddress CreateX6() => new(playerHP: 0x0970FC);
}