using System.Windows.Forms;

namespace BizHawk.PSXMMX.Forms.Fields;

public class ToggleListField : AbstractBaseField<CheckedListBox>
{
    public event Action<int, bool>? OnToggled;

    public ToggleListField(string labelText, params string[] items) :
        base(labelText, new CheckedListBox
        {
            Margin = FormConstants.CONTROL_PADDING,
            CheckOnClick = true
        }
    )
    {
        Control.Items.AddRange(items);
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

    private void HandleItemCheck(object? sender, ItemCheckEventArgs e) =>
        OnToggled?.Invoke(e.Index, e.NewValue == CheckState.Checked);
}
