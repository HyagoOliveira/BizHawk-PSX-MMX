using System.Windows.Forms;

namespace BizHawk.PSXMMX.Forms.Fields;

public class NumericUpDownField : AbstractBaseField<NumericUpDown>
{
    public NumericUpDownField(string labelText, decimal min = 0, decimal max = 100) :
        base(labelText, new NumericUpDown
        {
            Width = 54,
            TextAlign = HorizontalAlignment.Right,
            Margin = FormConstants.CONTROL_PADDING,
            Minimum = min,
            Maximum = max
        }
    )
    { }
}