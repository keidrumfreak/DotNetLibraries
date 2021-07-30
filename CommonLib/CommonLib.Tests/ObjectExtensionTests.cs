using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.TestHelper.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonLib.Tests
{
    [TestClass]
    public class ObjectExtensionTests
    {
        [TestMethod]
        public void PropertyValueEquals()
        {
            var item1 = new ValueClass
            {
                Item1 = "value1",
                Item2 = 1,
                item3 = new ValueClass
                {
                    Item1 = "value2",
                    Item2 = 2,
                    item3 = null
                }
            };

            var item2 = new ValueClass
            {
                Item1 = "value1",
                Item2 = 1,
                item3 = new ValueClass
                {
                    Item1 = "value2",
                    Item2 = 2,
                    item3 = null
                }
            };

            var item3 = new ValueClass
            {
                Item1 = "value1",
                Item2 = 1,
                item3 = null
            };

            var item4 = new ValueClass
            {
                Item1 = "value1",
                Item2 = 1,
                item3 = new ValueClass
                {
                    Item1 = "value3",
                    Item2 = 2,
                    item3 = null
                }
            };

            var item5 = new ValueClass
            {
                Item1 = "value1",
                Item2 = 1,
                item3 = new ValueClass
                {
                    Item1 = "value2",
                    Item2 = 2,
                    item3 = new ValueClass()
                }
            };

            var item6 = new ValueClass
            {
                Item1 = "value1",
                Item2 = 3,
                item3 = new ValueClass
                {
                    Item1 = "value2",
                    Item2 = 2,
                    item3 = new ValueClass()
                }
            };

            var item7 = new ValueClass { Item1 = "value2", Item2 = 2, item3 = null };
            var item8 = new ValueClass { Item1 = "value3", Item2 = 2, item3 = null };

            ValueClass item9 = null;
            ValueClass item10 = null;

            item9.PropertyValueEquals(item10).IsTrue();

            item7.PropertyValueEquals(item8).IsFalse();

            item1.PropertyValueEquals(item2).IsTrue();
            item1.PropertyValueEquals(item3).IsFalse();
            item1.PropertyValueEquals(item5).IsFalse();
            item1.PropertyValueEquals(item6).IsFalse();
            item1.PropertyValueEquals(item4).IsFalse();
        }

        class ValueClass
        {
            public string Item1 { get; set; }

            public int Item2 { get; set; }

            public ValueClass item3 { get; set; }
        }
    }
}
