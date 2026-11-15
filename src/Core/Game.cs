namespace BizHawk.PSXMMX.Core;

public static class Game
{
    public static MemoryAddress Addresses => addressesByGame[currentGame];

    private static GameName currentGame = GameName.Invalid;
    private static Dictionary<uint, string> MainObjectNames => MainObjects.Names[currentGame];
    private static readonly Dictionary<string, GameName> hashesByGame = new(StringComparer.OrdinalIgnoreCase)
    {
        ["314E06A8"] = GameName.MegaManX4,
        ["1C64D6EA"] = GameName.MegaManX5,
        ["F063F536"] = GameName.MegaManX6
    };
    private static readonly Dictionary<GameName, MemoryAddress> addressesByGame = new()
    {
        [GameName.MegaManX4] = new(player: 0x1418C8, camera: 0x1419B0, mainObjectsPool: 0x13BED0),
        [GameName.MegaManX5] = new(player: 0x09A0A0, camera: 0x09A1F8, mainObjectsPool: 0x092090),
        [GameName.MegaManX6] = new(player: 0x0970A0, camera: 0x0971F8, mainObjectsPool: 0x08EF48)
    };

    public static void Initialize(string hash) => hashesByGame.TryGetValue(hash, out currentGame);
    public static bool IsInvalid() => currentGame == GameName.Invalid;
    public static string GetMainObjectName(uint id) => MainObjectNames.TryGetValue(id, out var name) ?
        name : "UNKNOWN MAIN OBJECT";
}