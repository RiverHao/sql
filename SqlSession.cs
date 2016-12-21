using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sqlHelper.Attributes;
using System.Linq.Expressions;
using sqlHelper.Common;

namespace sqlHelper
{
    public partial class SqlSession<T> where T : class
    {
        private String _tableName;
        private String _field;
        private List<String> _fieldList = new List<String>();
        private String _where = "";
        private String _select;
        private String _sqlExpression;
        private String _connectionString;
        private String _primaryKey;
        private String _orderby;
        private String _join;
        private Boolean IsDistinct = false;

        public SqlSession()
        {
            _fieldList = EntityHelper.GetFields<T>(false);
            _field = EntityHelper.GetFiledString<T>();
            _tableName = EntityHelper.GetTableName<T>();
            _connectionString = EntityHelper.GetConnectionString<T>();
            _primaryKey = EntityHelper.GetPrimaryKey<T>();
        }

        public String PrimaryKey
        {
            get
            {
                return _primaryKey;
            }
            set
            {
                _primaryKey = value;
            }
        }

        public String TableName
        {
            get
            {
                return _tableName;
            }
        }

        public List<String> Fields 
        {
            get
            {
                return _fieldList;
            }
        }

        public String ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        public String SqlExpression
        {
            get
            {
                if (IsDistinct)
                {
                    _sqlExpression = "SELECT DISTINCT " + _field + " FROM " + _tableName + " ";
                }
                else
                {
                    _sqlExpression = "SELECT " + _field + " FROM " + _tableName + " ";
                }
                
                if (_join != "")
                {
                    _sqlExpression += _join;
                }
                if (_where != "")
                {
                    _sqlExpression += "WHERE " + _where;
                }
                if (_orderby != null)
                {
                    _sqlExpression += "ORDER BY " + _orderby;
                }
                return _sqlExpression;
            }
            set
            {
                _sqlExpression = value;
            }
        }

        public String SelectExpression
        {
            get
            {
                return _select;
            }
            set
            {
                _select = value;
            }
        }
    }


}
