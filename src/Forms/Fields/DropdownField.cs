using System.Windows.Forms;

namespace BizHawk.PSXMMX.Forms.Fields;

public class DropdownField : AbstractBaseField<ComboBox>
{
    public event Action<int>? OnSelected;

    public DropdownField(string labelText, params string[] items) :
        base(labelText, new ComboBox
        {
            Margin = FormConstants.CONTROL_PADDING,
            DropDownStyle = ComboBoxStyle.DropDownList
        }
    )
    {
        ResetItems(items);
        if (Control.Items.Count > 0) Control.SelectedIndex = 0;
        Control.SelectedIndexChanged += HandleSelectedIndexChanged;
    }

    public void ResetItems(params string[] items)
    {
        Control.Items.Clear();
        Control.Items.AddRange(items);
    }

    public void SelectWithoutNotify(int index)
    {
        if (Control.Items.Count == 0) return;

        Control.SelectedIndexChanged -= HandleSelectedIndexChanged;
        Control.SelectedIndex = index;
        Control.SelectedIndexChanged += HandleSelectedIndexChanged;
    }

    private void HandleSelectedIndexChanged(object _, EventArgs __) => OnSelected?.Invoke(Control.SelectedIndex);
}