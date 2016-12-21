using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Linq.Expressions;
using sqlHelper.Common;

namespace sqlHelper
{
    public static partial class DbUtility
    {
        private static SqlConnection conn;
        private static SqlDataAdapter da;

        /// <summary>
        /// 为通过反射生成的实例赋值
        /// </summary>
        /// <typeparam name="T">实例的类型</typeparam>
        /// <param name="obj">实例</param>
        /// <param name="value">值</param>
        /// <param name="key">成员名称</param>
        private static void SetValue<T>(ref T obj, Object value, String key) where T : class
        {
            var property = obj.GetType().GetProperty(key);
            var type = property.PropertyType.Name;
            if (value is System.DBNull)
            {
                property.SetValue(obj, null, null);
                return;
            }
            if (type == "Int32")
            {
                property.SetValue(obj, int.Parse(value.ToString()), null);
            }
            else if(type == "String")
            {
                property.SetValue(obj, value.ToString(), null);
            }
            else if (type == "DateTime")
            {
                property.SetValue(obj, (DateTime)value, null);
            }
            else
            {
                property.SetValue(obj, value, null);
            }
        }

        /// <summary>
        /// 获得SQLSession实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static SqlSession<T> GetSqlExpression<T>() where T : class
        {
            var temp = new SqlSession<T>();
            conn = new SqlConnection(EntityHelper.GetConnectionString<T>());
            return temp;
        }
    }
}
