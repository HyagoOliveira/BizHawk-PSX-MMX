using BizHawk.Client.Common;
using BizHawk.PSXMMX.Memory;

namespace BizHawk.PSXMMX.Gameplay;

public class GameCharacter : GameObject
{
    public ByteMemory Facing { get; protected set; }
    public SByteMemory Health { get; protected set; }

    public GameCharacter(long baseAddress, ApiContainer apis) : base(baseAddress, apis)
    {
        Facing = AddVariable(new ByteMemory("Facing", BaseAddress + 0x15, APIs.Memory));
        Health = AddVariable(new SByteMemory("HP", BaseAddress + 0x5C, APIs.Memory));
    }
}