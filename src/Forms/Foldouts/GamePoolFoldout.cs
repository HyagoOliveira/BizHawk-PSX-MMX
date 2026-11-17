using BizHawk.PSXMMX.Forms.Fields;
using BizHawk.PSXMMX.Gameplay;

namespace BizHawk.PSXMMX.Forms.Foldouts;

public sealed class GamePoolFoldout<T> : FoldoutControl where T : GameObject
{
    private readonly GamePoolManager<T> pool;
    private readonly DropdownField objDropdown;

    public GamePoolFoldout(GamePoolManager<T> pool, bool isExpanded = false) :
        base(pool.ToString(), isExpanded)
    {
        this.pool = pool;

        objDropdown = new("Object", items: pool.GetAllObjectsNames());

        objDropdown.Control.Width = 300;
        objDropdown.OnSelected += HandleObjectSelected;

        ControlsPanel.Controls.Add(objDropdown);
    }

    public override void UpdateContent()
    {
        base.UpdateContent();
        objDropdown.UpdateItems(pool.GetAllObjectsNames());
    }

    private void HandleObjectSelected(int index) => pool.ShowObjectProperties(index);
}