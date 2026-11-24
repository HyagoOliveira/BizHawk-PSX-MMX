namespace BizHawk.PSXMMX.Core;

public static class ShotObjects
{
    private static readonly Dictionary<uint, string> mmx4 = new()
    {
        { 00, "DRAGON SHOT" },
        { 01, "DRILL COPTER SHOT" },
        { 02, "GUNNER SHOT" },
        { 03, "EREGION FIREBALL" },
        { 04, "EREGION WING SLASH" },
        { 05, "MECH BOULDER" },
        { 06, "WALL CRAWLER SHOT" },
        { 07, "DROPPED BOMB" },
        { 08, "SHELL CRAWLER SHOT" },
        { 09, "TRIDENT SHOT" },
        { 10, "DRAGON SPREAD SHOT" },
        { 11, "ENEMY BULLET" },
        { 12, "ICE CORE SHOT" },
        { 13, "JET DRONE BULLET" },
        { 14, "DASH GUNNER SHOT" },
        { 15, "TURRET LASER" },
        { 16, "LINKED SPARK" },
        { 17, "DEPTH CHARGE" },
        { 18, "AIMED BULLET" },
        { 19, "TROOPER BOMB" },
        { 20, "CANNON BLAST" },
        { 21, "CANNON SHELL" },
        { 22, "WEB SHOT" },
        { 23, "WEB THREAD" },
        { 24, "TRAIN BOSS SHOT" },
        { 25, "GRENADE" },
        { 26, "SENTRY SHOT" },
        { 27, "JUMP SHOOTER SHOT" },
        { 28, "MELEE HITBOX" },
        { 29, "CANNON MISSILE" },
        { 30, "CANNON SHOT" },
        { 31, "SLASH BEAST CRESCENT" },
        { 32, "FLAME PILLAR" },
        { 33, "FALLING ROCK" },
        { 34, "RAY TRAP" },
        { 35, "WALRUS ICE" },
        { 36, "DRONE BEAM" },
        { 37, "OWL FEATHER" },
        { 38, "OWL CYCLONE" },
        { 39, "MUSHROOM SHOT" },
        { 40, "HATCH BLAST" },
        { 41, "MAGMA FIRE" },
        { 42, "PEACOCK MISSILE" },
        { 43, "GUNSHIP SHOT" },
        { 44, "IRIS SHOT" },
        { 45, "COLONEL SHOT" },
        { 46, "SIGMA SHOT" },
        { 47, "ENEMY RIDE ARMOR SHOT" },
        { 48, "DOUBLE BALL" },
        { 49, "DOUBLE AERIAL" },
        { 50, "DOUBLE BOUNCER" },
        { 51, "DOUBLE MINE" },
        { 52, "DOUBLE MINE SHOT" },
        { 53, "SIGMA HEAD" },
        { 54, "SIGMA FINAL SHOT" },
        { 55, "GENERAL SHOT" },
        { 56, "SIGMA SPIT" },
        { 57, "SIGMA BEAM HITBOX" }
    };

    private static readonly Dictionary<uint, string> mmx5 = new()
    {

    };
    private static readonly Dictionary<uint, string> mmx6 = new()
    {

    };

    public static readonly Dictionary<GameName, Dictionary<uint, string>> Names = new()
    {
        { GameName.Invalid, new Dictionary<uint, string>() },
        { GameName.MegaManX4, mmx4! },
        { GameName.MegaManX5, mmx5! },
        { GameName.MegaManX6, mmx6! }
    };
}