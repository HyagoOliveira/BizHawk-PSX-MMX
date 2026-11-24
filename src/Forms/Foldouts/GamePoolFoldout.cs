using System.Windows.Forms;
using BizHawk.PSXMMX.Forms.Fields;
using BizHawk.PSXMMX.Forms.Toggles;
using BizHawk.PSXMMX.Gameplay;

namespace BizHawk.PSXMMX.Forms.Foldouts;

public sealed class GamePoolFoldout<T> : FoldoutControl where T : GameObject
{
    public int CurrentIndex { get; private set; }

    private readonly GamePoolManager<T> pool;
    private readonly DropdownField objDropdown;
    private readonly ButtonToggle drawToggle = new("Draw");
    private readonly ButtonToggle moverToggle = new("Mover");
    private readonly ButtonToggle propertiesToggle = new("Properties");
    private readonly Label numpadMoverLabel = FormConstants.BuildNumpadMoverLabel();

    public GamePoolFoldout(GamePoolManager<T> pool, bool isExpanded = false) :
        base(pool.ToString(), isExpanded)
    {
        this.pool = pool;
        ContentHeight = 100;
        pool.DisableAllObjectsProperties();

        objDropdown = new("Object", GetDropdownItems());
        objDropdown.Control.Width = 500;
        objDropdown.OnSelected += HandleObjectSelected;

        moverToggle.OnToggled += HandleMoverToggled;
        drawToggle.OnToggled += HandleDrawToggled;
        propertiesToggle.OnToggled += HandlePropertiesToggled;

        ControlsPanel.Controls.Add(objDropdown);
        ControlsPanel.Controls.Add(moverToggle);
        ControlsPanel.Controls.Add(drawToggle);
        ControlsPanel.Controls.Add(propertiesToggle);
        ControlsPanel.Controls.Add(numpadMoverLabel);
    }

    public override void UpdateContent()
    {
        base.UpdateContent();
        objDropdown.UpdateItems(GetDropdownItems());
        drawToggle.UpdateText(pool.IsDrawEnabled(CurrentIndex));
        propertiesToggle.UpdateText(pool.IsPropertiesEnabled(CurrentIndex));
        numpadMoverLabel.Visible = pool.IsMoverEnabled(CurrentIndex);
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

    private void HandleDrawToggled() => pool.ToggleDrawEnabled(CurrentIndex);
    private void HandlePropertiesToggled() => pool.TogglePropertiesEnabled(CurrentIndex);

    private void HandleMoverToggled()
    {
        pool.ToggleMoverEnabled(CurrentIndex);
        numpadMoverLabel.Visible = pool.IsMoverEnabled(CurrentIndex);
    }
}