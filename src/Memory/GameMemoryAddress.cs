namespace BizHawk.PSXMMX.Memory;

public sealed class GameMemoryAddress
{
    public long PlayerHP { get; }

    internal GameMemoryAddress(long playerHP)
    {
        PlayerHP = playerHP;
    }

    public static GameMemoryAddress? Create(string gameHash)
    {
        return gameHash switch
        {
            "314E06A8" => CreateX4(),
            "1C64D6EA" => CreateX5(),
            "F063F536" => CreateX6(),
            _ => null
        };
    }

    private static GameMemoryAddress CreateX4() => new(playerHP: 0x141924);
    private static GameMemoryAddress CreateX5() => new(playerHP: 0x09A0FC);
    private static GameMemoryAddress CreateX6() => new(playerHP: 0x0970FC);
}