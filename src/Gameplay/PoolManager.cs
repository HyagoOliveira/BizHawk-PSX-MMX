using BizHawk.Client.Common;

namespace BizHawk.PSXMMX.Gameplay;

public sealed class PoolManager<T> where T : GameObject
{
    private readonly T[] pool;

    public PoolManager(long basseAddress, long blockSize, uint totalSlots, ApiContainer apis)
    {
        pool = new T[totalSlots];

        for (int i = 0; i < totalSlots; i++)
        {
            pool[i] = (T)Activator.CreateInstance(typeof(T), basseAddress + i * blockSize, apis)!;
        }
    }

    public void Update()
    {
        foreach (var obj in pool)
        {
            obj.IsActive.Update();
            if (obj.IsActivated()) obj.UpdateAndDraw();
        }
    }
}