using BizHawk.PSXMMX.Input;

namespace BizHawk.PSXMMX.Gameplay.Components;

public sealed class KeypadMover : AbstractComponent
{
    public const float SPEED = 6F;

    public KeypadMover(GameObject gameObject) :
        base(gameObject, isEnabled: false)
    {
    }

    protected override void Update()
    {
        var velocity = KeyboardInput.GetKeypadAxis() * SPEED;
        var position = GameObject.Position.ReadValue() + velocity;
        GameObject.Position.Freeze(position);
    }

    protected override void Draw() { }

    protected override void OnDisabled()
    {
        base.OnDisabled();
        GameObject.Position.Unfreeze();
    }
}