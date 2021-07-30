using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Data;
using CommonLib.TestHelper.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonLib.Tests.Data
{
    [TestClass]
    public class DataTableUtilTest
    {
        [TestMethod]
        public void ConvertDataTable()
        {
            var array = new[] { new ValueItem { Item1 = "value1", Item2 = "value2" }, new ValueItem { Item1 = "value3", Item2 = "value4" } };
            var table = array.ConvertDataTable();

            table.Columns[0].ColumnName.AreEqual("Item1");
            table.Columns[1].ColumnName.AreEqual("Item2");
            table.Rows[0][0].AreEqual("value1");
            table.Rows[0][1].AreEqual("value2");
            table.Rows[1][0].AreEqual("value3");
            table.Rows[1][1].AreEqual("value4");
        }

        class ValueItem
        {
            public string Item1 { get; set; }

            public string Item2 { get; set; }
        }
    }
}
