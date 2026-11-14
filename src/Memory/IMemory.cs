using BizHawk.Client.Common;
using BizHawk.PSXMMX.Math;

namespace BizHawk.PSXMMX.Memory;

public interface IMemory
{
    string Name { get; }
    bool HideDisplay { get; set; }
    bool IsFrozen { get; }

    void ToggleFreeze();
    void Update();
    void Draw(Vector2Int position, ApiContainer apis);
}