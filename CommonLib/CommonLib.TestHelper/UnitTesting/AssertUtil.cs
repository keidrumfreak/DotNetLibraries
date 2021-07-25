using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonLib.TestHelper.UnitTesting
{
    public static class AssertUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        public static void ArePropertyValueEqual<T>(T expected, T actual)
        {
            if (actual == null)
                Assert.Fail($"<{expected}> が必要ですが、 <null> が指定されました。");

            var props = typeof(T).GetRuntimeProperties();
            foreach (var prop in props)
            {
                var value = prop.GetValue(expected);
                var other = prop.GetValue(actual);
                if (value == null && other != null)
                    Assert.Fail($"<null> が必要ですが、 <{other}> が指定されました。");
                if (value == null && other == null)
                    continue;
                var type = value.GetType();
                if (type != typeof(string) && type.IsClass)
                {
                    ArePropertyValueEqual(Convert.ChangeType(value, type), Convert.ChangeType(other, type));
                }
                else
                {
                    Assert.AreEqual(Convert.ChangeType(value, type), Convert.ChangeType(other, type));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        public static void ArePropertyValueEqualAll<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            if (actual == null)
                Assert.Fail($"<{expected}> が必要ですが、 <null> が指定されました。");

            if (expected.Count() != actual.Count())
                Assert.Fail($"シーケンスの数が一致しません。");

            Assert.IsTrue(expected.All(exp => actual.Any(act => act.PropertyValueEquals(exp))));
        }

        /// <inheritdoc cref="Assert.AreEqual{T}(T, T)"/>
        public static void AreEqual<T>(this T actual, T expected)
        {
            Assert.AreEqual(expected, actual);
        }

        /// <inheritdoc cref="Assert.IsTrue(bool)"/>
        public static void IsTrue(this bool condition)
        {
            Assert.IsTrue(condition);
        }

        /// <inheritdoc cref="Assert.IsFalse(bool)"/>
        public static void IsFalse(this bool condition)
        {
            Assert.IsFalse(condition);
        }
    }
}
