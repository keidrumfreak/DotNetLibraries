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

            var props = typeof(T).GetRuntimeProperties();
            foreach (var prop in props)
            {
                var value = prop.GetValue(obj);
                if (value == null && prop.GetValue(other) != null)
                    return false;
            }

            return typeof(T).GetRuntimeProperties()
                .All(prop => {
                    var value = prop.GetValue(obj);
                    if (value == null)
                        return prop.GetValue(other) == null;
                    var type = value.GetType();
                    return type != typeof(string) && type.IsClass
                    ? Convert.ChangeType(value, type).PropertyValueEquals(Convert.ChangeType(prop.GetValue(other), type))
                    : Convert.ChangeType(value, type).Equals(Convert.ChangeType(prop.GetValue(other), type));
                });
        }
    }
}
