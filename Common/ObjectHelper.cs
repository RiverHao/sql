using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sqlHelper.Common
{
    public static class ObjectHelper
    {
        /// <summary>
        /// 获取Entity实例的字段名和值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Dictionary<String,Object> GetKeyValue(Object obj){
            var data = new Dictionary<String, Object>();
            foreach (var i in obj.GetType().GetProperties())
            {
                var value = obj.GetType().GetProperty(i.Name).GetValue(obj,null);
                data.Add(i.Name, value);
            }
            return data;
        }
    }
}
