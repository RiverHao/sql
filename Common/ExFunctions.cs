using sqlHelper.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sqlHelper.Common
{
    public static class ExFunctions
    {
        /// <summary>
        /// 获取枚举值的描述
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumitem"></param>
        /// <returns></returns>
        public static String GetDescription<T>(this T enumitem)
        {
            var item = enumitem.GetType().GetField(enumitem.ToString()).GetCustomAttributes(typeof(DescriptionAttribute),true);
            var result = ((DescriptionAttribute)item[0]).Description;
            return result;
        }
    }
}
