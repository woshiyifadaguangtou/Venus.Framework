using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Venus.Data;
using Venus.Util;
using Venus.Util.Ioc;

namespace Venus.Data.EF
{
    public class Database : IDatabase
    {

        #region 构造函数

        public Database(string connString, string DbType)
        {
            if (DbType == "")
            {
                dbContext = (DbContext)UnityIocHelper.DBInstance.GetService<IDbContext>(new ParameterOverride(
                          "connString", connString));
            }
            else
            {
                dbContext = (DbContext)UnityIocHelper.DBInstance.GetService<IDbContext>(DbType, new ParameterOverride(
                           "connString", connString));
            }
        }

        #endregion 

        #region 属性
        public DbContext dbContext { get; set; }
        public DbTransaction dbTransaction { get; set; }

        #endregion
        public IDatabase BeginTrans()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            dbContext.Dispose();

        }

        public int Commit()
        {
            try
            {
                int returnValue = dbContext.SaveChanges();
                if (dbTransaction != null)
                {
                    dbTransaction.Commit();
                    this.Close();
                }
                return returnValue;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.InnerException is SqlException)
                {
                    SqlException sqlEx = ex.InnerException.InnerException as SqlException;
                    string msg = ExceptionMessage.GetSqlExceptionMessage(sqlEx.Number);
                    throw DataAccessException.ThrowDataAccessException(sqlEx, msg);
                }
                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }

        public int Delete<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public int Delete<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            IEnumerable<T> entities = dbContext.Set<T>().Where(condition).ToList();
            return entities.Count() > 0 ? Delete(entities) : 0;
        }

        public int Delete<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                dbContext.Set<T>().Attach(entity);
                dbContext.Set<T>().Remove(entity);

             //   dbContext.Entry(entities).State = EntityState.Deleted;
            }

            return dbTransaction == null ? Commit() : 0;
        }

        public int Delete<T>(object KeyValue) where T : class
        {
           var entity = dbContext.Set<T>().Find(KeyValue);
            return Delete<T>(entity);
        }

        public int Delete<T>(object[] KeyValue) where T : class
        {
            for (int i = 0; i < KeyValue.Length; i++)
            {
                var entity = dbContext.Set<T>().Find(KeyValue[i]);
                if (entity != null)
                    dbContext.Entry(entity).State = EntityState.Deleted;
            }
            return dbTransaction == null ? Commit() : 0;
        }

        public int Delete<T>(T entity) where T : class
        {
            dbContext.Set<T>().Attach(entity);
            dbContext.Set<T>().Remove(entity);
            return dbTransaction == null ? Commit() : 0;
        }

        public int Delete<T>(object propertyValue, string propertyName) where T : class
        {
            throw new NotImplementedException();
        }

        public int ExecuteByProc(string procName)
        {
            throw new NotImplementedException();
        }

        public int ExecuteByProc(string procName, DbParameter[] dbParameter)
        {
            throw new NotImplementedException();
        }

        public int ExecuteBySql(string strSql)
        {
            throw new NotImplementedException();
        }

        public int ExecuteBySql(string strSql, params DbParameter[] dbParameter)
        {
            throw new NotImplementedException();
        }

        public T FindEntity<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return dbContext.Set<T>().Where(condition).FirstOrDefault();
        }

        public T FindEntity<T>(object KeyValue) where T : class
        {
            return dbContext.Set<T>().Find(KeyValue);
        }

        public IEnumerable<T> FindList<T>() where T : class, new()
        {
            return dbContext.Set<T>().ToList();
        }

        public IEnumerable<T> FindList<T>(string strSql) where T : class
        {
            return FindList<T>(strSql, null);
        }

        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return dbContext.Set<T>().Where(condition).ToList();
        }

        public IEnumerable<T> FindList<T>(Func<T, object> orderby) where T : class, new()
        {
            return dbContext.Set<T>().OrderBy(orderby).ToList();
        }

        public IEnumerable<T> FindList<T>(string strSql, DbParameter[] dbParameter) where T : class
        {
            using (var dbConnection = dbContext.Database.Connection)
            {
                var IDataReader = new DbHelper(dbConnection).ExecuteReader(CommandType.Text, strSql, dbParameter);
                return ConvertExtension.IDataReaderToList<T>(IDataReader);
            }
        }

        public IEnumerable<T> FindList<T>(string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class, new()
        {
            string[] _order = orderField.Split(',');
            MethodCallExpression resultExp = null;
            var tempData = dbContext.Set<T>().AsQueryable();
            foreach (string item in _order)
            {
                string _orderPart = item;
                _orderPart = Regex.Replace(_orderPart, @"\s+", " ");
                string[] _orderArry = _orderPart.Split(' ');
                string _orderField = _orderArry[0];
                bool sort = isAsc;
                if (_orderArry.Length == 2)
                {
                    isAsc = _orderArry[1].ToUpper() == "ASC" ? true : false;
                }
                var parameter = Expression.Parameter(typeof(T), "t");
                var property = typeof(T).GetProperty(_orderField);
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(T), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
            }
            tempData = tempData.Provider.CreateQuery<T>(resultExp);
            total = tempData.Count();
            tempData = tempData.Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize).AsQueryable();
            return tempData.ToList();
        }

        public IEnumerable<T> FindList<T>(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class
        {
            return FindList<T>(strSql, null, orderField, isAsc, pageSize, pageIndex, out total);
        }

        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class, new()
        {
            string[] _order = orderField.Split(',');
            MethodCallExpression resultExp = null;
            var tempData = dbContext.Set<T>().Where(condition).AsQueryable();
            foreach (string item in _order)
            {
                string _orderPart = item; // fieldName (ASC) 
                _orderPart = Regex.Replace(_orderPart, @"\s+", " "); //替换空白、回车、换行
                string[] _orderArry = _orderPart.Split(' ');
                string _orderField = _orderArry[0];
                bool sort = isAsc;
                if (_orderArry.Length == 2)
                {
                    isAsc = _orderArry[1].ToUpper() == "ASC" ? true : false;
                }
                var parameter = Expression.Parameter(typeof(T), "t");
                var property = typeof(T).GetProperty(_orderField);
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(T), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
            }
            tempData = tempData.Provider.CreateQuery<T>(resultExp);
            total = tempData.Count();
            tempData = tempData.Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize).AsQueryable();
            return tempData.ToList();
        }

        public IEnumerable<T> FindList<T>(string strSql, DbParameter[] dbParameter, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class
        {
            using (var dbConnection = dbContext.Database.Connection)
            {
                StringBuilder sb = new StringBuilder();
                if (pageIndex == 0)
                {
                    pageIndex = 1;
                }
                int num = (pageIndex - 1) * pageSize;
                int num1 = (pageIndex) * pageSize;
                string OrderBy = "";

                if (!string.IsNullOrEmpty(orderField))
                {
                    if (orderField.ToUpper().IndexOf("ASC") + orderField.ToUpper().IndexOf("DESC") > 0)
                    {
                        OrderBy = "Order By " + orderField;
                    }
                    else
                    {
                        OrderBy = "Order By " + orderField + " " + (isAsc ? "ASC" : "DESC");
                    }
                }
                else
                {
                    OrderBy = "order by (select 0)";
                }
                sb.Append("Select * From (Select ROW_NUMBER() Over (" + OrderBy + ")");
                sb.Append(" As rowNum, * From (" + strSql + ") As T ) As N Where rowNum > " + num + " And rowNum <= " + num1 + "");
                total = Convert.ToInt32(new DbHelper(dbConnection).ExecuteScalar(CommandType.Text, "Select Count(1) From (" + strSql + ") As t", dbParameter));
                var IDataReader = new DbHelper(dbConnection).ExecuteReader(CommandType.Text, sb.ToString(), dbParameter);
                return ConvertExtension.IDataReaderToList<T>(IDataReader);
            }
        }

        public object FindObject(string strSql)
        {
            throw new NotImplementedException();
        }

        public object FindObject(string strSql, DbParameter[] dbParameter)
        {
            throw new NotImplementedException();
        }

        public DataTable FindTable(string strSql)
        {
            throw new NotImplementedException();
        }

        public DataTable FindTable(string strSql, DbParameter[] dbParameter)
        {
            throw new NotImplementedException();
        }

        public DataTable FindTable(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex, out int total)
        {
            throw new NotImplementedException();
        }

        public DataTable FindTable(string strSql, DbParameter[] dbParameter, string orderField, bool isAsc, int pageSize, int pageIndex, out int total)
        {
            throw new NotImplementedException();
        }

        public int Insert<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                dbContext.Entry<T>(entity).State = EntityState.Added;
            }
            return dbTransaction == null ? this.Commit() : 0;
        }

        public int Insert<T>(T entity) where T : class
        {
            dbContext.Entry<T>(entity).State = EntityState.Added;
            return dbTransaction == null ? this.Commit() : 0;
        }

        public IQueryable<T> IQueryable<T>() where T : class, new()
        {
            return dbContext.Set<T>();
        }

        public IQueryable<T> IQueryable<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return dbContext.Set<T>().Where(condition);
        }

        public void Rollback()
        {
            this.dbTransaction.Rollback();
            this.dbTransaction.Dispose();
            this.Close();
        }

        public int Update<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public int Update<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                Update<T>(entity);
            }

            return dbTransaction == null ? this.Commit() : 0;
        }

        public int Update<T>(T entity) where T : class
        {

            dbContext.Set<T>().Attach(entity);
            Hashtable props = ConvertExtension.GetPropertyInfo<T>(entity);
            foreach (string item in props.Keys)
            {
                object value = dbContext.Entry(entity).Property(item).CurrentValue;
                if (value != null)
                {
                    if (value.ToString() == "&nbsp;")
                        dbContext.Entry(entity).Property(item).CurrentValue = null;
                    dbContext.Entry(entity).Property(item).IsModified = true;
                }
            }
            return dbTransaction == null ? this.Commit() : 0;
        }
    }
}
