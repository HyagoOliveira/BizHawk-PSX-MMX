using BizHawk.Client.Common;

namespace BizHawk.PSXMMX.Gameplay;

public sealed class GamePoolManager<T> where T : GameObject
{
    private readonly T[] pool;

    public GamePoolManager(long basseAddress, long blockSize, uint totalSlots, ApiContainer apis)
    {
        pool = new T[totalSlots];
        for (int i = 0; i < totalSlots; i++)
        {
            pool[i] = (T)Activator.CreateInstance(typeof(T), basseAddress + i * blockSize, apis)!;
            pool[i].PoolIndex = i;
        }
    }

    public bool IsDrawEnabled(int currentIndex)
    {
        if (currentIndex < 0 || currentIndex >= pool.Length) return false;
        return pool[currentIndex].IsDrawEnabled;
    }

    public bool IsPropertiesEnabled(int currentIndex)
    {
        if (currentIndex < 0 || currentIndex >= pool.Length) return false;
        return pool[currentIndex].IsPropertiesEnabled;
    }

    public bool IsMoverEnabled(int currentIndex)
    {
        if (currentIndex < 0 || currentIndex >= pool.Length) return false;
        return pool[currentIndex].Mover.IsEnabled;
    }

    public void UpdateAndTryDraw()
    {
        foreach (var obj in pool)
        {
            obj.UpdateAndTryDraw();
        }
    }

    public void DisableAllObjectsProperties() => SetObjectsPropertiesEnabled(false);

    public void ShowObjectProperties(int index)
    {
        DisableAllObjectsProperties();
        pool[index].IsPropertiesEnabled = true;
    }

    public void ToggleMoverEnabled(int index)
    {
        if (index < 0 || index >= pool.Length) return;
        pool[index].Mover.ToggleEnabled();
    }
    public void ToggleDrawEnabled(int currentIndex)
    {
        if (currentIndex < 0 || currentIndex >= pool.Length) return;
        pool[currentIndex].ToggleDrawEnabled();
    }

    public void TogglePropertiesEnabled(int currentIndex)
    {
        if (currentIndex < 0 || currentIndex >= pool.Length) return;
        pool[currentIndex].TogglePropertiesEnabled();
    }

    public override string ToString() => $"{typeof(T).Name} Pool";

    public string[] GetAllObjectsNames() => [.. pool.Select(obj => obj.Name)];
    public string[] GetActivatedObjectsNames() => [.. GetActivatedObjects().Select(obj => obj.Name)];
    public T GetFirstActivatedObject() => GetActivatedObjects().FirstOrDefault();
    public IEnumerable<T> GetActivatedObjects() => pool.Where(obj => obj.IsActivated());

    private void SetObjectsPropertiesEnabled(bool isEnabled)
    {
        foreach (var obj in pool)
        {
            obj.IsPropertiesEnabled = isEnabled;
        }
    }
}