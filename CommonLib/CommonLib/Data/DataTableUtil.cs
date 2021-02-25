using System.Collections.Generic;
using System.Data;

namespace CommonLib.Data
{
    public static class DataTableUtil
    {
        /// <summary>
        /// オブジェクトの配列をDataTableに変換します
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static DataTable ConvertDataTable<T>(this IEnumerable<T> items)
        {
            var properties = typeof(T).GetProperties();
            var result = new DataTable();

            // テーブルレイアウトの作成
            foreach (var prop in properties)
            {
                result.Columns.Add(prop.Name, prop.PropertyType);
            }

            // 値の投げ込み
            foreach (var item in items)
            {
                var row = result.NewRow();
                foreach (var prop in properties)
                {
                    var itemValue = prop.GetValue(item, new object[] { });
                    row[prop.Name] = itemValue;
                }
                result.Rows.Add(row);
            }
            return result;
        }
    }
}
