using BizHawk.Client.Common;
using BizHawk.PSXMMX.Math;

namespace BizHawk.PSXMMX.Memory;

public interface IMemory
{
    string Name { get; }
    long Address { get; }
    bool IsFrozen { get; }

    void Update();
    void Draw(Vector2Int position, IGuiApi gui);
    void ToggleFreeze();
}
