using sqlHelper.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sqlHelper.Common
{
    public static class EntityHelper
    {

        public static List<String> GetDTOFields<TD>()
        {
            var fields = typeof(TD).GetProperties();
            var result = new List<String>();
            foreach (var i in fields)
            {
                result.Add(i.Name);
            }
            return result;
        }

        /// <summary>
        /// 获取Entity实体中的字段
        /// </summary>
        /// <typeparam name="TF"></typeparam>
        /// <param name="isFullName">true：字段名前面包含表名</param>
        /// <returns></returns>
        public static List<String> GetFields<TF>(Boolean isFullName)
        {
            var fields = typeof(TF).GetProperties();
            var result = new List<String>();
            if (isFullName)
            {
                var tablename = EntityHelper.GetTableName<TF>();
                foreach (var i in fields)
                {
                    result.Add(tablename + "." + i.Name);
                }
                return result;
            }
            else
            {
                foreach (var i in fields)
                {
                    result.Add(i.Name);
                }
                return result;
            }
            
        }

        public static String GetFiledString<TS>()
        {
            var list = GetFields<TS>(true).ToArray();
            var result = String.Join(",", list);
            return result;
        }

        /// <summary>
        /// 获取实体代表的表名
        /// </summary>
        /// <typeparam name="TD"></typeparam>
        /// <returns></returns>
        public static String GetTableName<TD>()
        {
            var tablename = typeof(TD).GetCustomAttributes(typeof(TableNameAttribute), true);
            return ((TableNameAttribute)tablename[0]).TableName;
        }

        public static String GetTableName(Type entityType)
        {
            try
            {
                var tablename = entityType.GetCustomAttributes(typeof(TableNameAttribute), true);
                return ((TableNameAttribute)tablename[0]).TableName;
            }
            catch
            {
                throw new Exception("没有配置TableName特性！");
            }
            
        }

        public static String GetConnectionString(Type type)
        {
            try
            {
                var dataserver = type.GetCustomAttributes(typeof(DataServerAttribute), true);
                var dataserverstr = ((DataServerAttribute)dataserver[0]).DataServerInfo;
                return dataserverstr;
            }
            catch
            {
                throw new Exception("没有配置DataServer或DataTable特性！");
            }
        }
        /// <summary>
        /// 获取实体代表的数据库
        /// </summary>
        /// <typeparam name="TC"></typeparam>
        /// <returns></returns>
        public static String GetConnectionString<TC>()
        {
            var dataserver = typeof(TC).GetCustomAttributes(typeof(DataServerAttribute), true);
            var dataserverstr = ((DataServerAttribute)dataserver[0]).DataServerInfo;
            return dataserverstr;
        }


        public static String GetPrimaryKey<TP>()
        {
            var primary = typeof(TP).GetCustomAttributes(typeof(PrimaryAttribute), true);
            var pri = typeof(TP).GetProperties();
            foreach (var i in pri)
            {
                var pris = i.GetCustomAttributes(typeof(PrimaryAttribute), true);
                if (pris.Count() >= 1)
                {
                    return i.Name;
                }
            }
            return "";     
        }
    }
}
