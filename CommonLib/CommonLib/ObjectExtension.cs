using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public static class ObjectExtension
    {
        public static bool PropertyValueEquals<T>(this T obj, T other)
        {
            if (other == null)
                return obj == null;

            var props = obj.GetType().GetRuntimeProperties();
            foreach (var prop in props)
            {
                var value = prop.GetValue(obj);
                if (value == null && prop.GetValue(other) != null)
                    return false;
                var type = prop.PropertyType;
                if (type == typeof(string))
                {
                    if ((string)value != (string)prop.GetValue(other))
                        return false;
                }
                else if (type.IsClass)
                {
                    if (!Convert.ChangeType(value, type).PropertyValueEquals(Convert.ChangeType(prop.GetValue(other), type)))
                        return false;
                }
                else
                {
                    if (!Convert.ChangeType(value, type).Equals(Convert.ChangeType(prop.GetValue(other), type)))
                        return false;
                }
            }
            return true;
        }
    }
}
