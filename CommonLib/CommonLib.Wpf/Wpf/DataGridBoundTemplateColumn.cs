using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CommonLib.Wpf
{
    /// <summary>
    /// DataGridCellのTemplateに設定したDataTemplateでBindingを行えるようにする
    /// </summary>
    public class DataGridBoundTemplateColumn : DataGridBoundColumn
    {
        public DataTemplate CellTemplate { get; set; }
        public DataTemplate CellEditingTemplate { get; set; }

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            return Generate(dataItem, CellEditingTemplate);
        }

        private FrameworkElement Generate(object dataItem, DataTemplate template)
        {
            var contentControl = new ContentControl { ContentTemplate = template, Content = dataItem };
            BindingOperations.SetBinding(contentControl, ContentControl.ContentProperty, Binding);
            return contentControl;
        }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            return Generate(dataItem, CellTemplate);
        }
    }
}
