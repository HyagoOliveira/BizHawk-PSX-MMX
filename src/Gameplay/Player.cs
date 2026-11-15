using BizHawk.Client.Common;
using BizHawk.PSXMMX.Input;
using BizHawk.PSXMMX.Math;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Gameplay;

public sealed class Player : MainObject
{
    public KeyboardInput Input { get; }

    // Variables
    public SByteS8Memory CharacterMode { get; }       // unk2
    public SByteS8Memory Action { get; }        // unk5
    public SByteS8Memory ActionSubstate { get; }      // unk6
    public BoolS8Memory IsSpikeImmune { get; }          // spike_immune
    public SByteS8Memory AirAction { get; }           // air_action
    public SByteS8Memory ChargeState0 { get; }         // charge_state[0]
    public SByteS8Memory ChargeState1 { get; }         // charge_state[1]
    public ByteU8Memory ChargeTimer { get; }           // charge_timer
    public ByteU8Memory SpecialChargeTimer { get; }    // special_charge_timer
    public SByteS8Memory StockCharge { get; }          // stock_charge
    public ByteU8Memory ArmorParts { get; }            // armor_parts
    public SByteS8Memory[] WeaponEnergy { get; }       // weapon_energy[0x10]
    public ByteU8Memory ArmType { get; }               // arm_type
    public ByteU8Memory BossFlags { get; }            // boss_flags
    public SByteS8Memory ZeroCombo { get; }               // combo
    public BoolS8Memory IsNovaStrikeActive { get; }    // nova_strike_active
    public SByteS8Memory NovaStrikeTimer { get; }      // nova_strike_timer
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

        CharacterMode = AddVariable(
            new SByteS8Memory(
                "Character Mode",
                baseAddress + 0x02,
                APIs.Memory,
                hideDisplay: true
            )
        );
        Action = AddVariable(
            new SByteS8Memory(
                "Action",
                baseAddress + 0x05,
                APIs.Memory
            )
        );
        ActionSubstate = AddVariable(
            new SByteS8Memory(
                "Action Substate",
                baseAddress + 0x06,
                APIs.Memory,
                hideDisplay: true
            )
        );
        IsSpikeImmune = AddVariable(
            new BoolS8Memory(
                "Spike Immune",
                baseAddress + 0x7A,
                APIs.Memory
            )
        );
        AirAction = AddVariable(
            new SByteS8Memory(
                "Air Action",
                baseAddress + 0x86,
                APIs.Memory,
                hideDisplay: true
            )
        );
        ChargeState0 = AddVariable(
            new SByteS8Memory(
                "Charge State 0",
                baseAddress + 0x9B,
                APIs.Memory,
                hideDisplay: true
            )
        );
        ChargeState1 = AddVariable(
            new SByteS8Memory(
                "Charge State 1",
                baseAddress + 0x9C,
                APIs.Memory,
                hideDisplay: true
            )
        );
        ChargeTimer = AddVariable(
            new ByteU8Memory(
                "Charge Timer",
                baseAddress + 0x9D,
                APIs.Memory,
                hideDisplay: true
            )
        );
        SpecialChargeTimer = AddVariable(
            new ByteU8Memory(
                "Special Charge Timer",
                baseAddress + 0x9E,
                APIs.Memory,
                hideDisplay: true
            )
        );
        StockCharge = AddVariable(
            new SByteS8Memory(
                "Stock Charge",
                baseAddress + 0xA6,
                APIs.Memory,
                hideDisplay: true
            )
        );
        ArmorParts = AddVariable(
            new ByteU8Memory(
                "Armor Parts",
                baseAddress + 0xA7,
                APIs.Memory,
                hideDisplay: true
            )
        );
        WeaponEnergy = new SByteS8Memory[0x10];
        for (var index = 0; index < WeaponEnergy.Length; index++)
        {
            WeaponEnergy[index] = AddVariable(
                new SByteS8Memory(
                    $"Weapon Energy [{index}]",
                    baseAddress + 0xA8 + index,
                    APIs.Memory,
                    hideDisplay: true
                )
            );
        }
        ArmType = AddVariable(
            new ByteU8Memory(
                "Arm Type",
                baseAddress + 0xB8,
                APIs.Memory,
                hideDisplay: true
            )
        );
        BossFlags = AddVariable(
            new ByteU8Memory(
                "Boss Flags",
                baseAddress + 0xB9,
                APIs.Memory,
                hideDisplay: true
            )
        );
        ZeroCombo = AddVariable(
            new SByteS8Memory(
                "Zero Combo",
                baseAddress + 0xBB,
                APIs.Memory,
                hideDisplay: true
            )
        );
        IsNovaStrikeActive = AddVariable(
            new BoolS8Memory(
                "Nova Strike Active",
                baseAddress + 0xE2,
                APIs.Memory,
                hideDisplay: true
            )
        );
        NovaStrikeTimer = AddVariable(
            new SByteS8Memory(
                "Nova Strike Timer",
                baseAddress + 0xE3,
                APIs.Memory,
                hideDisplay: true
            )
        );
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