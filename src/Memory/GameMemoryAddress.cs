namespace BizHawk.PSXMMX.Memory;

public sealed class GameMemoryAddress
{
    public const string DOMAIN = "MainRAM";
    private static SupportedGames currentGame = SupportedGames.Invalid;
    private static readonly Dictionary<string, GameMemoryAddress> addressesByHash = new(StringComparer.OrdinalIgnoreCase)
    {
        ["314E06A8"] = new(SupportedGames.MegaManX4, player: 0x1418C8, camera: 0x1419B0, mainObjectsPool: 0x13BED0),
        ["1C64D6EA"] = new(SupportedGames.MegaManX5, player: 0x09A0A0, camera: 0x09A1F8, mainObjectsPool: 0x092090),
        ["F063F536"] = new(SupportedGames.MegaManX6, player: 0x0970A0, camera: 0x0971F8, mainObjectsPool: 0x08EF48)
    };

    public SupportedGames Game { get; }
    public long Player { get; }
    public long Camera { get; }
    public long MainObjectsPool { get; }

    private GameMemoryAddress(
        SupportedGames game,
        long player = 0,
        long camera = 0,
        long mainObjectsPool = 0
    )
    {
        Game = game;
        Player = player;
        Camera = camera;
        MainObjectsPool = mainObjectsPool;
    }

    public bool IsInvalid() => Game == SupportedGames.Invalid;

    public static GameMemoryAddress Get(string gameHash)
    {
        var hasHash = addressesByHash.TryGetValue(gameHash, out var addresses);
        var result = hasHash ? addresses! : new GameMemoryAddress(SupportedGames.Invalid);
        currentGame = result.Game;
        return result;
    }

    public static string GetMainObjectName(uint id) => objectNames[currentGame].TryGetValue(id, out var name) ?
        name : "UNKNOWN MAIN OBJECT";

    private static readonly Dictionary<uint, string> mmx4MainObjectNames = new()
    {
        { 00, "BACKGROUND DRAGON" },
        { 01, "ARMORED WALKER" },
        { 02, "ITEM CARRIER" },
        { 03, "SPIKE MARL" },
        { 04, "DRILL COPTER" },
        { 05, "ROBOT BEE" },
        { 06, "BULLDOZER" },
        { 07, "AMBUSH GUNNER" },
        { 08, "EREGION" },
        { 09, "UNUSED" },
        { 10, "DRAGONFLY" },
        { 11, "WALL CRAWLER" },
        { 12, "HOVER SENTRY" },
        { 13, "HEAVY MECH" },
        { 14, "ICE BIRD" },
        { 15, "SHELL CRAWLER" },
        { 16, "TRIDENT MECH" },
        { 17, "SNOWMAN BOMB" },
        { 18, "ICE CORE" },
        { 19, "SURFACE HOPPER" },
        { 20, "ICE BLOCK" },
        { 21, "FALLING ICICLE" },
        { 22, "BEE HIVE" },
        { 23, "SLOPE SKIER" },
        { 24, "JET DRONE" },
        { 25, "CATERKILLER" },
        { 26, "ICE WALL" },
        { 27, "DASH GUNNER" },
        { 28, "SPAWNER POD" },
        { 29, "POD SPAWNER" },
        { 30, "REGEN TURRET" },
        { 31, "THORN TRAP" },
        { 32, "BOMB BAT" },
        { 33, "SPIKE SLED" },
        { 34, "HIGHWAY TROOPER" },
        { 35, "WHEEL CHARGER" },
        { 36, "LATCHER" },
        { 37, "ROCKET SPIKER" },
        { 38, "TRAIN CANNON" },
        { 39, "FALLING CEILING" },
        { 40, "BREAKABLE TERRAIN" },
        { 41, "DATA HOPPER" },
        { 42, "HOMING ORB" },
        { 43, "WEB SPIDER" },
        { 44, "ANCHORED MINE" },
        { 45, "TRAIN BOSS" },
        { 46, "TRAIN BOSS TURRET" },
        { 47, "TRAIN BOSS ARMOR" },
        { 48, "SENTRY DRONE" },
        { 49, "TRAIN SOLDIER" },
        { 50, "TRAIN CRATE" },
        { 51, "FORTRESS CANNON" },
        { 52, "JUMP SHOOTER" },
        { 53, "WAVE RIDER" },
        { 54, "SLASH BEAST" },
        { 55, "JET STINGRAY FLYBY" },
        { 56, "JET STINGRAY" },
        { 57, "FROST WALRUS" },
        { 58, "BEAM DRONE" },
        { 59, "TIMED EXPLOSION" },
        { 60, "STORM OWL" },
        { 61, "SPLIT MUSHROOM" },
        { 62, "FLAME JET" },
        { 63, "HATCH TURRET" },
        { 64, "CYBER PEACOCK" },
        { 65, "MAGMA DRAGOON" },
        { 66, "IRIS" },
        { 67, "GUNSHIP" },
        { 68, "SIGMA" },
        { 69, "COLONEL" },
        { 70, "DRONE POD" },
        { 71, "RIDE ARMOR PILOT" },
        { 72, "UNUSED RIDE ARMOR" },
        { 73, "DOUBLE" },
        { 74, "SIGMA FINAL" },
        { 75, "GENERAL" },
        { 76, "SPIKE CRAWLER" }
    };

    private static readonly Dictionary<uint, string> mmx5MainObjectNames = [];
    private static readonly Dictionary<uint, string> mmx6MainObjectNames = [];

    private static readonly Dictionary<SupportedGames, Dictionary<uint, string>> objectNames = new()
    {
        { SupportedGames.Invalid, new Dictionary<uint, string>() },
        { SupportedGames.MegaManX4, mmx4MainObjectNames! },
        { SupportedGames.MegaManX5, mmx5MainObjectNames! },
        { SupportedGames.MegaManX6, mmx6MainObjectNames! }
    };
}