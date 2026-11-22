using System.Windows.Forms;

namespace BizHawk.PSXMMX.Forms.Fields;

public class ToggleEnumField<T> : AbstractBaseField<CheckedListBox> where T : struct, Enum
{
    public event Action<T, bool>? OnToggled;

    public ToggleEnumField(string labelText) :
        base(labelText, new CheckedListBox
        {
            Margin = FormConstants.CONTROL_PADDING,
            CheckOnClick = true
        }
    )
    {
        Control.Items.AddRange(Enum.GetNames(typeof(T)));
        Control.ItemCheck += HandleItemCheck;
    }

    public void SetWithoutNotify(int[] indexes, bool[] isChecked)
    {
        Control.ItemCheck -= HandleItemCheck;
        try
        {
            for (int i = 0; i < indexes.Length; i++)
            {
                Control.SetItemChecked(indexes[i], isChecked[i]);
            }
        }
        finally
        {
            Control.ItemCheck += HandleItemCheck;
        }
    }

    private void HandleItemCheck(object? _, ItemCheckEventArgs e)
    {
        if (Control.Items[e.Index] is string selected)
        {
            var isChecked = e.NewValue == CheckState.Checked;
            var enumValue = (T)Enum.Parse(typeof(T), selected);
            OnToggled?.Invoke(enumValue, isChecked);
        }
    }
}
