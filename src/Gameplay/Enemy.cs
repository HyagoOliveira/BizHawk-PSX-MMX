using BizHawk.Client.Common;
using BizHawk.PSXMMX.Math;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Gameplay;

public sealed class Enemy : GameCharacter
{
    public Enemy(long baseAddress, ApiContainer apis) : base(baseAddress, apis)
    {
    }

    public override void Update()
    {
        base.Update();
        UpdateName();
    }

    protected override Vector2Int GetVarsPosition() => HurtBox.WorldBounds.HasValue ?
        Camera.Main.WorldToScreenPoint(HurtBox.WorldBounds.Value.GetTopRight()) :
        Vector2Int.Zero;

    private void UpdateName()
    {
        if (!IsVisible.Value) return;
        var hasName = ID_NAMES.TryGetValue(Id.Value, out var name);
        Name = (hasName ? name : "UNKNOWN ENEMY") + $" (Id: {Id.Value})";
    }

    private static readonly Dictionary<uint, string> ID_NAMES = new()
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
}