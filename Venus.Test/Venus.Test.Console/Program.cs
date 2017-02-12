using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Venus.Application.Entity;
using System.Reflection;
using System.Linq.Expressions;
using Venus.Data.Repository;
using Venus.Data.EF;
using Venus.Application.Entity.CBManage;
using Venus.Application.Business.CBManage;

namespace Venus.Test.Console
{
    class Program
    {
        static String connString = ConfigurationManager.ConnectionStrings["BaseDb"].ConnectionString;
        static IRepository db;
       // static Venus.Data.IDatabase db;

        static void Main(string[] args)
        {

            CBTaskBLL cbbll = new CBTaskBLL();

            string keyValue = DateTime.Now.ToString("yyyyMMdd");
            CBTaskEntity entity = new CBTaskEntity()
            {
                TaskId = keyValue,
                cbrq = DateTime.Now,
                fdate = DateTime.Now.ToString("yyyyMM"),
                detail_count = 0,
                cbydm = "0101",
                state = 0,
                xh = "0101",
            };
            cbbll.CreateCBTask(keyValue, entity);

            System.Console.WriteLine("执行完毕!");
            System.Console.ReadKey();

        }

        private static void Test1()
        {
            //   db = new RepositoryFactory().BaseRepository();
            //   db = new Venus.Data.EF.Database(connString, "SqlServer");
            // Insert();
            // Find();
            //Delete();
            //     Update();
            int total = 0;
            FindList<TestEntity>(e => 1 == 1, "Name", out total);
            //ExpressionFun<TestEntity>("Name", "MyTest");
        }

        private static void Insert()
        {
            db = new RepositoryFactory().BaseRepository();
            List<TestEntity> testList = new List<TestEntity>();
            for (int i = 0; i < 9; i++)
            {
                testList.Add(new TestEntity() { Id = i.ToString(), Name = String.Format("Test{0}", i) });
            }
            db.Insert<TestEntity>(testList);
        }
        private static void Update()
        {
            db = new RepositoryFactory().BaseRepository();
            var result = new TestEntity() { Id = "0", Name = "MyTest" };
            db.Update(result);
        }
        private static void Delete()
        {
            // var result = db.FindEntity<TestEntity>("1");

            //var result1 = new TestEntity() { Id = "2" };
            //db.Delete(result1);


            //    db.Delete<TestEntity>("4");
            db = new RepositoryFactory().BaseRepository();
            object[] id = { "5", "6" };
            db.Delete<TestEntity>(id);

        }
        private static void Find()
        {
            
            db = new RepositoryFactory().BaseRepository();
            var result = db.FindEntity<TestEntity>("1");
            if (result != null)
                System.Console.WriteLine("Name:{0}", result.Name);
        }

        private static void Find2()
        {
            // Expression<Func<TestEntity,int, bool>> expression = (Param,num) => Param.Id == num.ToString();
            db = new RepositoryFactory().BaseRepository();
            string key = "1";
            Expression<Func<TestEntity, bool>> expression = param => true;
            Expression< Func < TestEntity,bool>> expression2 = param => param.Id == key;

            Action a = () => { System.Console.WriteLine(key); };
            //var resultexpression = Expression<Func<TestEntity, bool>>.And(expression2,expression);
            key = "2";
            var result = db.FindEntity<TestEntity>(expression2);
            
            if (result != null)
                System.Console.WriteLine("Name:{0}", result.Name);
            a();
        }

        public static List<T> FindList<T>(Expression<Func<T, bool>> condition, string orderField,out int total) where T : class, new()
        {
            SqlServerDbContext dbContext = new SqlServerDbContext(connString);
            string[] orders = orderField.Split(',');
            var tempData = dbContext.Set<T>().AsQueryable();
            MethodCallExpression resultexpression = null;
            foreach (string order in orders)
            {
                var propertyInfo = typeof(T).GetProperty(order);
                ParameterExpression parameter = Expression.Parameter(typeof(T), "t");
                var filed = Expression.MakeMemberAccess(parameter, propertyInfo);
                var result = Expression.Lambda(filed, parameter);
                resultexpression = Expression.Call(typeof(Queryable),"OrderBy",new Type []{ typeof(T),propertyInfo.PropertyType},tempData.Expression,Expression.Quote(result));
              //  resultexpression = Expression.Call();
            }
            tempData = tempData.Provider.CreateQuery<T>(resultexpression);
            total = tempData.Count();
            return tempData.ToList();
            //return null;
        }

        public static void ExpressionFun<T>(string fieldName,string value) where T : class, new()
        {
            
            var Ttype = typeof(T).GetProperty(fieldName);
            Expression condition = null;
            ParameterExpression parameter = Expression.Parameter(typeof(T), "e");
            var constExression = Expression.Constant(value);
            var fieldNameExpression = Expression.MakeMemberAccess(parameter, Ttype);
            var ContainExrepssion = Expression.Call(fieldNameExpression, typeof(string).GetMethod("Contains"), constExression);
            Expression result = Expression.Lambda(ContainExrepssion, parameter);
            Expression<Func<T, object>> result1 = e => ContainExrepssion;
            if (result != result1)
            {

                db = new RepositoryFactory().BaseRepository();
                var result4 = db.FindEntity<TestEntity>(result1);
                if (result4 != null)
                    System.Console.WriteLine("Name:{0}", result4.Id);
                db = new RepositoryFactory().BaseRepository();
                var result3 = db.FindEntity<TestEntity>(result);
                if (result3 != null)
                    System.Console.WriteLine("Name:{0}", result3.Id);

            
            }
          

        }

    }
}
