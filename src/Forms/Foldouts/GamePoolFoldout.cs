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
        pool.DisableAllObjectsProperties();

        objDropdown = new("Object", GetDropdownItems());
        objDropdown.Control.Width = 300;
        objDropdown.OnSelected += HandleObjectSelected;

        ControlsPanel.Controls.Add(objDropdown);
    }

    public override void UpdateContent()
    {
        base.UpdateContent();
        objDropdown.UpdateItems(GetDropdownItems());
    }

    private string[] GetDropdownItems() => ["None", .. pool.GetAllObjectsNames()];

    private void HandleObjectSelected(int index)
    {
        if (index < 0) return;
        if (index == 0) pool.DisableAllObjectsProperties();
        else pool.ShowObjectProperties(index - 1);
    }
}