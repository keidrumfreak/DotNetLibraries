using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace CommonLib.Wpf
{
    /// <summary>
    /// 数値入力のみを受け付けるテキストボックス
    /// </summary>
    public partial class NumericTextBox : TextBox
    {

        public NumericTextBox()
        {
            PreviewTextInput += NumericTextBox_PreviewTextInput;
        }

        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !NumRegex().IsMatch(e.Text);
        }

        [GeneratedRegex("[0-9]")]
        private static partial Regex NumRegex();
    }
}
