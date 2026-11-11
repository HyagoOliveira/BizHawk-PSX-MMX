namespace BizHawk.PSXMMX.Memory;

public sealed class GameMemoryAddress
{
    private static readonly Dictionary<string, GameMemoryAddress> addressesByHash = new(StringComparer.OrdinalIgnoreCase)
    {
        ["314E06A8"] = new(SupportedGames.MegaManX4, player: 0x1418C8, camera: 0x1419B0),
        ["1C64D6EA"] = new(SupportedGames.MegaManX5, player: 0x09A0A0, camera: 0x09A1F8),
        ["F063F536"] = new(SupportedGames.MegaManX6, player: 0x0970A0, camera: 0x0971F8)
    };

    public SupportedGames Game { get; }
    public long Player { get; }
    public long Camera { get; }

    private GameMemoryAddress(
        SupportedGames game,
        long player = 0,
        long camera = 0
    )
    {
        Game = game;
        Player = player;
        Camera = camera;
    }

    public bool IsInvalid() => Game == SupportedGames.Invalid;

    public static GameMemoryAddress Get(string gameHash)
    {
        var hasHash = addressesByHash.TryGetValue(gameHash, out var address);
        return hasHash ? address : new(SupportedGames.Invalid);
    }
}