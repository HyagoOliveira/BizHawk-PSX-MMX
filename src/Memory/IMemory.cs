using BizHawk.Client.Common;
using BizHawk.PSXMMX.Math;

namespace BizHawk.PSXMMX.Memory;

public interface IMemory
{
    bool HideDraw { get; set; }

    void Update();
    void Draw(Vector2Int position, ApiContainer apis);
}