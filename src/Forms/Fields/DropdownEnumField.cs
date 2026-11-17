using System.Windows.Forms;

namespace BizHawk.PSXMMX.Forms.Fields;

public class DropdownEnumField<T> : AbstractBaseField<ComboBox> where T : struct, Enum
{
    public event Action<T>? OnSelected;

    public DropdownEnumField(string labelText) :
        base(labelText, new ComboBox
        {
            Margin = FormConstants.CONTROL_PADDING,
            DropDownStyle = ComboBoxStyle.DropDownList
        }
    )
    {
        Control.Items.AddRange(Enum.GetNames(typeof(T)));
        if (Control.Items.Count > 0) Control.SelectedIndex = 0;

        Control.SelectedIndexChanged += (_, _) =>
        {
            if (Control.SelectedItem is string selected)
                OnSelected?.Invoke((T)Enum.Parse(typeof(T), selected));
        };
    }
}