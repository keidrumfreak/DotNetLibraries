using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace CommonLib.Wpf
{
    /// <summary>
    /// 数値入力のみを受け付けるテキストボックス
    /// </summary>
    public class NumericTextBox : TextBox
    {

        public NumericTextBox()
        {
            PreviewTextInput += NumericTextBox_PreviewTextInput;
        }

        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !new Regex("[0-9]").IsMatch(e.Text);
        }
    }
}
