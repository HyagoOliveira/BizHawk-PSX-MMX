namespace BizHawk.PSXMMX.Input;

using BizHawk.Client.Common;

public sealed class KeyboardInput(IInputApi input)
{
    private HashSet<string> currentKeys = new(StringComparer.OrdinalIgnoreCase);
    private HashSet<string> previousKeys = new(StringComparer.OrdinalIgnoreCase);
    private readonly IInputApi input = input ?? throw new ArgumentNullException(nameof(input));

    public void Update()
    {
        previousKeys = currentKeys;
        currentKeys = new HashSet<string>(
            input.GetPressedButtons(),
            StringComparer.OrdinalIgnoreCase
        );
    }

    public bool GetKey(string key)
    {
        ValidateKey(key);
        return currentKeys.Contains(key);
    }

    public bool GetKeyDown(string key)
    {
        ValidateKey(key);
        return currentKeys.Contains(key) && !previousKeys.Contains(key);
    }

    public bool GetKeyUp(string key)
    {
        ValidateKey(key);
        return !currentKeys.Contains(key) && previousKeys.Contains(key);
    }

    private static void ValidateKey(string key)
    {
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("A keyboard key name is required.", nameof(key));
    }
}
