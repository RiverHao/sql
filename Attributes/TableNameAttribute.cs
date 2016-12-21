using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sqlHelper.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class TableNameAttribute : Attribute
    {
        private String _tableName = "";

        public String TableName
        {
            get
            {
                return _tableName;
            }
            set
            {
                _tableName = value;
            }
        }

        public TableNameAttribute(String name)
        {
            _tableName = name;
        }
    }
}
