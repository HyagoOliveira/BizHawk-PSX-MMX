namespace BizHawk.PSXMMX.Core;

public static class Game
{
    public static MemoryAddress Addresses => addressesByGame[currentName];

    private static GameName currentName = GameName.Invalid;
    private static Dictionary<uint, string> MainObjectNames => MainObjects.Names[currentName];
    private static readonly Dictionary<string, GameName> hashesByGame = new(StringComparer.OrdinalIgnoreCase)
    {
        ["314E06A8"] = GameName.MegaManX4,
        ["1C64D6EA"] = GameName.MegaManX5,
        ["F063F536"] = GameName.MegaManX6
    };
    private static readonly Dictionary<GameName, MemoryAddress> addressesByGame = new()
    {
        [GameName.MegaManX4] = new(player: 0x1418C8, camera: 0x1419B0, bossFlags: 0x141981, mainObjectsPool: 0x13BED0, weaponObjectsPool: 0x1406F8),
        [GameName.MegaManX5] = new(player: 0x09A0A0, camera: 0x09A1F8, bossFlags: 0x09A169, mainObjectsPool: 0x092090, weaponObjectsPool: 0x098120),
        [GameName.MegaManX6] = new(player: 0x0970A0, camera: 0x0971F8, bossFlags: 0x097169, mainObjectsPool: 0x08EF48, weaponObjectsPool: 0x0950A0)
    };

    public static void Initialize(string hash) => hashesByGame.TryGetValue(hash, out currentName);
    public static bool Is(GameName name) => currentName == name;
    public static bool IsInvalid() => Is(GameName.Invalid);
    public static string GetMainObjectName(uint id) => MainObjectNames.TryGetValue(id, out var name) ?
        name : "UNKNOWN MAIN OBJECT";
}