namespace BizHawk.PSXMMX.Memory;

public sealed class GameMemoryAddress
{
    private static readonly Dictionary<string, GameMemoryAddress> addressesByHash = new(StringComparer.OrdinalIgnoreCase)
    {
        ["314E06A8"] = new(SupportedGames.MegaManX4, playerHP: 0x141924),
        ["1C64D6EA"] = new(SupportedGames.MegaManX5, playerHP: 0x09A0FC),
        ["F063F536"] = new(SupportedGames.MegaManX6, playerHP: 0x0970FC)
    };

    public SupportedGames Game { get; }
    public long PlayerHP { get; }

    public bool IsInvalid => Game == SupportedGames.Invalid;

    private GameMemoryAddress(SupportedGames game, long playerHP)
    {
        Game = game;
        PlayerHP = playerHP;
    }

    public static GameMemoryAddress Get(string gameHash)
    {
        var hasHash = addressesByHash.TryGetValue(gameHash, out var address);
        return hasHash ? address : new(SupportedGames.Invalid, playerHP: 0);
    }
}