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
        Control.Items.AddRange(items);
        if (Control.Items.Count > 0) Control.SelectedIndex = 0;
        Control.SelectedIndexChanged += (_, _) => OnSelected?.Invoke(Control.SelectedIndex);
    }
}