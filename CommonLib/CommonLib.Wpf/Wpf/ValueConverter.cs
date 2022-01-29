using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;

namespace CommonLib.Wpf
{
    public class BoolToStringConverter : BoolToValueConverter<string> { }

    [ValueConversion(typeof(bool), typeof(bool))]
    public class InverseBoolConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    public class BoolToValueConverter<T> : MarkupExtension, IValueConverter
    {
        public T TrueValue { get; set; }
        public T FalseValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return FalseValue;
            else
                return (bool)value ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? value.Equals(TrueValue) : false;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    /// <summary>
    /// 値が指定した値の場合trueを返します
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValueToBoolConverter<T> : MarkupExtension, IValueConverter
    {
        public T TrueValue { get; set; }
        public T FalseValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return EqualityComparer<T>.Default.Equals((T)value, TrueValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? TrueValue :
                EqualityComparer<T>.Default.Equals(FalseValue, default(T)) ? Binding.DoNothing : FalseValue;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    /// <summary>
    /// EnumをStringValueで定義されている文字列に変換します
    /// </summary>
    public class EnumToStringValueConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;
            return ((Enum)value).GetAttribute<StringValueAttribute>();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    /// <summary>
    /// Enumが指定した値の場合trueに変換する
    /// </summary>
    public class EnumToBoolConverter : MarkupExtension, IValueConverter
    {
        public Enum Value { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() == Value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.Equals(true) == true ? Value : Binding.DoNothing;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    public class DateTimeToStringConverter : MarkupExtension, IValueConverter
    {
        public string DisplayFormat { get; set; }

        public string InputFormat { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            return ((DateTime)value).ToString(DisplayFormat);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            return DateTime.TryParseExact((string)value, InputFormat, null, DateTimeStyles.None, out var dt) ? dt : (DateTime?)null;

        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    public class IntToStringConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            return ((int)value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            return int.TryParse((string)value, out var ret) ? ret : (int?)null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    public class MultiValueEqualityConverter : MarkupExtension, IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values?.All(o => o?.Equals(values[0]) == true) == true || values?.All(o => o == null) == true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
