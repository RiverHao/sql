using sqlHelper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace sqlHelper
{
    public partial class SqlSession<T> where T : class
    {
        #region Where操作
        private void WhereSqlFromExpression(Expression func)
        {
            if (_where != "")
            {
                _where = _where + "AND " + ExpressionHelper.GetSqlByExpression(func) + " ";
            }
            else
            {
                _where = ExpressionHelper.GetSqlByExpression(func) + " ";
            }
        }
        /// <summary>
        /// Where操作，适用于单表查询(exp代表的元素的查询)
        /// </summary>
        /// <param name="func">表达式</param>
        public void Where(Expression<Func<T, Boolean>> func)
        {
            WhereSqlFromExpression(func.Body);
        }
        /// <summary>
        /// Where操作，适用于单表查询(Target中元素的查询)
        /// </summary>
        /// <typeparam name="Target">要查询的另一个表的实体</typeparam>
        /// <param name="func"></param>
        public void Where<Target>(Expression<Func<Target, Boolean>> func)
        {
            WhereSqlFromExpression(func.Body);
        }
        /// <summary>
        /// Where操作，适用于联表查询时的where语句(exp和T元素的关系查询)
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <param name="func"></param>
        public void Where<Target>(Expression<Func<Target, T, Boolean>> func)
        {
            WhereSqlFromExpression(func.Body);
        }
        /// <summary>
        /// Where操作，适用于多联表时的where语句（TSource和Target元素之间的关系查询）
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="Target"></typeparam>
        /// <param name="func"></param>
        public void Where<TSource, Target>(Expression<Func<TSource, Target, Boolean>> func)
        {
            WhereSqlFromExpression(func.Body);
        }

        /// <summary>
        /// Where操作，适用于多联表时的where语句（多条件，跨表）
        /// </summary>
        /// <typeparam name="T0"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="func"></param>
        public void Where<T0, T1, T2>(Expression<Func<T0, T1, T2, Boolean>> func)
        {
            WhereSqlFromExpression(func.Body);
        }

        /// <summary>
        /// Where操作，适用于多联表时的where语句（多条件，跨表）
        /// </summary>
        /// <typeparam name="T0"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="func"></param>
        public void Where<T0, T1, T2, T3>(Expression<Func<T0, T1, T2, T3, Boolean>> func)
        {
            WhereSqlFromExpression(func.Body);
        }

        /// <summary>
        /// Where操作，适用于多联表时的where语句（多条件，跨表）
        /// </summary>
        /// <typeparam name="T0"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <param name="func"></param>
        public void Where<T0, T1, T2, T3, T4>(Expression<Func<T0,T1,T2,T3,T4,Boolean>> func)
        {
            WhereSqlFromExpression(func.Body);
        }
        #endregion

        #region 排序
        /// <summary>
        /// 按照DESC排序
        /// </summary>
        /// <param name="func"></param>
        public void OrderByDescending(Expression<Func<T, Object>> func)
        {
            _orderby = ExpressionHelper.GetSqlByExpression(func.Body) + "DESC ";
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="func"></param>
        public void OrderBy(Expression<Func<T, Object>> func)
        {
            _orderby = ExpressionHelper.GetSqlByExpression(func.Body) + " ";
        }
        #endregion

        #region 联表
        /// <summary>
        ///join表链接(exp和Target表相连时使用此方法)
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <param name="func"></param>
        public void Join<Target>(Expression<Func<Target,T,Boolean>> func)
        {
            var targetfields = "," + EntityHelper.GetFiledString<Target>();
            _field += targetfields;
            _join += "INNER JOIN " + EntityHelper.GetTableName<Target>() + " ON ";
            _join += ExpressionHelper.GetSqlByExpression(func.Body) + " ";
        }

        /// <summary>
        /// join表连接(TSource和Target表相连时使用此方法)
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="Target"></typeparam>
        /// <param name="func"></param>
        public void Join<TSource, Target>(Expression<Func<TSource, Target, Boolean>> func)
        {
            if (!_field.Contains(EntityHelper.GetFiledString<TSource>()))
            {
                throw new NotSupportedException("联表时还没有联接: " + EntityHelper.GetTableName<TSource>());
            }
            var targetfields = "," + EntityHelper.GetFiledString<Target>();
            _field += targetfields;
            _join += "INNER JOIN " + EntityHelper.GetTableName<Target>() + " ON ";
            _join += ExpressionHelper.GetSqlByExpression(func.Body) + " ";
        }
        #endregion

        #region 左连接
        /// <summary>
        /// 左连接
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <param name="func"></param>
        public void LeftJoin<Target>(Expression<Func<Target, T, Boolean>> func)
        {
            _field += "," + EntityHelper.GetFiledString<Target>();
            _join += "LEFT JOIN " + EntityHelper.GetTableName<Target>() + " ON ";
            _join += ExpressionHelper.GetSqlByExpression(func.Body) + " ";
        }

        public void LeftJoin<TSource, Target>(Expression<Func<TSource, Target, Boolean>> func)
        {
            if (!_field.Contains(EntityHelper.GetFiledString<TSource>()))
            {
                throw new NotSupportedException("联表时还没有联接: " + EntityHelper.GetTableName<TSource>());
            }
            _field += "," + EntityHelper.GetFiledString<Target>();
            _join += "LEFT JOIN " + EntityHelper.GetTableName<Target>() + " ON ";
            _join += ExpressionHelper.GetSqlByExpression(func.Body) + " ";
        }
        #endregion

        #region 右连接
        /// <summary>
        /// 右连接
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <param name="func"></param>
        public void RightJoin<Target>(Expression<Func<Target, T, Boolean>> func)
        {
            _field += "," + EntityHelper.GetFiledString<Target>();
            _join += "RIGHT JOIN " + EntityHelper.GetTableName<Target>() + " ON ";
            _join += ExpressionHelper.GetSqlByExpression(func.Body) + " ";
        }

        public void RightJoin<TSource, Target>(Expression<Func<TSource, Target, Boolean>> func)
        {
            if (!_field.Contains(EntityHelper.GetFiledString<TSource>()))
            {
                throw new NotSupportedException("联表时还没有联接: " + EntityHelper.GetTableName<TSource>());
            }
            _field += "," + EntityHelper.GetFiledString<Target>();
            _join += "RIGHT JOIN " + EntityHelper.GetTableName<Target>() + " ON ";
            _join += ExpressionHelper.GetSqlByExpression(func.Body) + " ";
        }
        #endregion

        public void Distinct()
        {
            IsDistinct = true;
        }
    }
}
