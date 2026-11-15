using BizHawk.Client.Common;
using BizHawk.PSXMMX.Input;
using BizHawk.PSXMMX.Math;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Gameplay;

public sealed class Player : MainObject
{
    public KeyboardInput Input { get; }

    // Variables
    public BoolU8Memory IsAttacking { get; }      // attacking
    public SByteS8Memory ShotFired { get; }      // shot_fired
    public SByteS8Memory AttackEnded { get; }    // attack_ended
    public ByteU8Memory AttackPoseTimer { get; } // attack_pose_timer
    public ByteU8Memory ShotCooldown { get; }   // shot_cooldown
    public SByteS8Memory Weapon { get; }         // weapon
    public SByteS8Memory ShotType0 { get; }      // shot_types[0]
    public SByteS8Memory ShotType1 { get; }      // shot_types[1]
    public SByteS8Memory ShotType { get; }       // shot_type
    public SByteS8Memory LastShotType { get; }   // last_shot_type
    public SByteS8Memory ShotCount { get; }      // shot_count
    public SByteS8Memory SpecialShotCount { get; } // special_shot_count
    public SByteS8Memory ShotCycle { get; }      // shot_cycle
    public UIntS8Memory Character { get; }      // cur_character

    public Player(long baseAddress, ApiContainer apis, KeyboardInput input) : base(baseAddress, apis)
    {
        Input = input;
        IsDrawEnabled = true;
        IsPropertiesEnabled = true;

        IsAttacking = AddVariable(
            new BoolU8Memory(
                "Is Attacking",
                baseAddress + 0x8E,
                APIs.Memory,
                hideDisplay: true
            )
        );
        ShotFired = AddVariable(
            new SByteS8Memory(
                "Shot Fired",
                baseAddress + 0x8F,
                APIs.Memory,
                hideDisplay: true
            )
        );
        AttackEnded = AddVariable(
            new SByteS8Memory(
                "Attack Ended",
                baseAddress + 0x90,
                APIs.Memory,
                hideDisplay: true
            )
        );
        AttackPoseTimer = AddVariable(
            new ByteU8Memory(
                "Attack Pose Timer",
                baseAddress + 0x91,
                APIs.Memory,
                hideDisplay: true
            )
        );
        ShotCooldown = AddVariable(
            new ByteU8Memory(
                "Shot Cooldown",
                baseAddress + 0x92,
                APIs.Memory,
                hideDisplay: true
            )
        );
        Weapon = AddVariable(
            new SByteS8Memory(
                "Weapon",
                baseAddress + 0x93,
                APIs.Memory,
                hideDisplay: true
            )
        );
        ShotType0 = AddVariable(
            new SByteS8Memory(
                "Shot Type 0",
                baseAddress + 0x94,
                APIs.Memory,
                hideDisplay: true
            )
        );
        ShotType1 = AddVariable(
            new SByteS8Memory(
                "Shot Type 1",
                baseAddress + 0x95,
                APIs.Memory,
                hideDisplay: true
            )
        );
        ShotType = AddVariable(
            new SByteS8Memory(
                "Shot Type",
                baseAddress + 0x96,
                APIs.Memory,
                hideDisplay: true
            )
        );
        LastShotType = AddVariable(
            new SByteS8Memory(
                "Last Shot Type",
                baseAddress + 0x97,
                APIs.Memory,
                hideDisplay: true
            )
        );
        ShotCount = AddVariable(
            new SByteS8Memory(
                "Shot Count",
                baseAddress + 0x98,
                APIs.Memory,
                hideDisplay: true
            )
        );
        SpecialShotCount = AddVariable(
            new SByteS8Memory(
                "Special Shot Count",
                baseAddress + 0x99,
                APIs.Memory,
                hideDisplay: true
            )
        );
        ShotCycle = AddVariable(
            new SByteS8Memory(
                "Shot Cycle",
                baseAddress + 0x9A,
                APIs.Memory,
                hideDisplay: true
            )
        );
        Character = AddVariable(
            new UIntS8Memory(
                "Char",
                baseAddress + 0x3093B,
                APIs.Memory,
                hideDisplay: true
            )
        );
    }

    public override void Update()
    {
        base.Update();
    }

    protected override void UpdateName()
    {
        var playerName = (PlayerName)Character.Value;
        Name = $"Player:\t{playerName}";
    }

    protected override Vector2Int GetPropertiesPosition() => new(
        2,
        10 // Camera.SCREEN_SIZE.Y - 900
    );

    private enum PlayerName
    {
        X = 0,
        Zero = 1
    }
}