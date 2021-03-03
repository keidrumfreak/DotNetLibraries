using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonLib
{
    public static class EnumUtil
    {
        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var fi = value.GetType().GetField(value.ToString());
            if (fi == null)
                return default;

            var attributes = fi.GetCustomAttributes(typeof(T), false).Cast<T>();

            return attributes.Any() ? attributes.First() : default;
        }

        public static T GetValue<T> (Func<T, bool> predicate) where T : Enum
        {
            return GetValues<T>().FirstOrDefault(predicate);
        }

        public static IEnumerable<T> GetValues<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
