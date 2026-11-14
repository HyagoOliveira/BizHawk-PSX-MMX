using System.Drawing;
using BizHawk.Client.Common;
using BizHawk.PSXMMX.Gameplay.Components.Collisions;
using BizHawk.PSXMMX.Math;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Gameplay;

public class GameCharacter : GameObject
{
    // Variables
    public BoolU8Memory IsFacingRight { get; }  // direction
    public Vector2S32Memory Speed { get; }      // x_speed, y_speed
    public FloatS32Memory HorizontalAcceleration { get; }   // x_accel
    public FloatS32Memory Gravity { get; }                  // gravity
    public SByteS8Memory Health { get; protected set; }     // hp
    public UIntS8Memory Damage { get; }                 // contact_damage 
    public UIntS8Memory InvincibilityTimer { get; }     // invincibility_timer 

    // Components
    public BoundBox DamagerBox { get; }         // attack_box
    public BoundBox DamageableBox { get; }      // hurt_box
    public TerrainBox CollisionBox { get; }     // terrain_box
    public AnimationMemory Animation { get; }   // unk17, animation_step

    public GameCharacter(long baseAddress, ApiContainer apis) : base(baseAddress, apis)
    {
        IsFacingRight = AddVariable(
            new BoolU8Memory(
                "Is Right",
                BaseAddress + 0x15,
                APIs.Memory,
                hideDisplay: true
            )
        );
        Speed = AddVariable(
            new Vector2S32Memory(
                "Speed ",
                xAddress: BaseAddress + 0x20,
                yAddress: BaseAddress + 0x24,
                APIs.Memory
            )
        );
        HorizontalAcceleration = AddVariable(
            new FloatS32Memory(
                "Hor Acc",
                BaseAddress + 0x28,
                APIs.Memory,
                hideDisplay: true
            )
        );
        Gravity = AddVariable(
            new FloatS32Memory(
                "Gravity",
                BaseAddress + 0x2C,
                APIs.Memory
            )
        );
        Health = AddVariable(
            new SByteS8Memory(
                "Health",
                BaseAddress + 0x5C,
                APIs.Memory
            )
        );
        Damage = AddVariable(
            new UIntS8Memory(
                "Damage",
                BaseAddress + 0x60,
                APIs.Memory
            )
        );
        InvincibilityTimer = AddVariable(
            new UIntS8Memory(
                "Inv Timer",
                BaseAddress + 0x61,
                APIs.Memory
            )
        );
        Animation = AddVariable(
            new AnimationMemory(
                "Anim",
                indexAddress: BaseAddress + 0x17,
                stepAddress: BaseAddress + 0x44,
                cursorAddress: BaseAddress + 0x34,
                APIs.Memory
            )
        );

        DamagerBox = AddComponent(new BoundBox(BaseAddress + 0x50, this, Color.Red));
        DamageableBox = AddComponent(new BoundBox(BaseAddress + 0x54, this, Color.LimeGreen));
        CollisionBox = AddComponent(new TerrainBox(BaseAddress + 0x68, this));
    }

    public override void Draw()
    {
        base.Draw();
        DrawVariables(GetVarsPosition());
    }

    protected virtual Vector2Int GetVarsPosition() => Vector2Int.Zero;
}