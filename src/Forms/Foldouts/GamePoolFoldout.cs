using BizHawk.PSXMMX.Forms.Fields;
using BizHawk.PSXMMX.Forms.Toggles;
using BizHawk.PSXMMX.Gameplay;

namespace BizHawk.PSXMMX.Forms.Foldouts;

public sealed class GamePoolFoldout<T> : FoldoutControl where T : GameObject
{
    public int CurrentIndex { get; private set; }

    private readonly GamePoolManager<T> pool;
    private readonly DropdownField objDropdown;
    private readonly ButtonToggle moverToggle = new("Mover");

    public GamePoolFoldout(GamePoolManager<T> pool, bool isExpanded = false) :
        base(pool.ToString(), isExpanded)
    {
        this.pool = pool;
        pool.DisableAllObjectsProperties();

        objDropdown = new("Object", GetDropdownItems());
        objDropdown.Control.Width = 300;
        objDropdown.OnSelected += HandleObjectSelected;
        moverToggle.OnToggled += HandleMoverToggled;

        ControlsPanel.Controls.Add(objDropdown);
        ControlsPanel.Controls.Add(moverToggle);
    }

    public override void UpdateContent()
    {
        base.UpdateContent();
        objDropdown.UpdateItems(GetDropdownItems());
    }

    private string[] GetDropdownItems() => ["None", .. pool.GetAllObjectsNames()];

    private void HandleObjectSelected(int index)
    {
        CurrentIndex = -1;
        if (index < 0) return;
        if (index == 0) pool.DisableAllObjectsProperties();
        else
        {
            CurrentIndex = index - 1;
            pool.ShowObjectProperties(CurrentIndex);
        }
    }

    private void HandleMoverToggled()
    {
        if (CurrentIndex < 0) return;
        pool.ToggleMoverEnabled(CurrentIndex);
    }
}