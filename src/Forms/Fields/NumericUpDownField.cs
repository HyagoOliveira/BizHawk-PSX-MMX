using System.Windows.Forms;

namespace BizHawk.PSXMMX.Forms.Fields;

public class NumericUpDownField : AbstractBaseField<NumericUpDown>
{
    public NumericUpDownField(string labelText, decimal min = 0, decimal max = 100) :
        base(labelText, new NumericUpDown
        {
            Margin = new Padding(2, 5, 2, 3),
            Width = 64,
            TextAlign = HorizontalAlignment.Right,
            Minimum = min,
            Maximum = max
        }
    )
    { }
}