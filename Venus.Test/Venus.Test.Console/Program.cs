using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Venus.Application.Entity;
using System.Reflection;
using System.Linq.Expressions;

namespace Venus.Test.Console
{
    class Program
    {
        static String connString = ConfigurationManager.ConnectionStrings["testConn"].ConnectionString;
        static Venus.Data.EF.Database db;


        static void Main(string[] args)
        {

            db = new Data.EF.Database(connString, "SqlServer");
            //Insert();
            //Find2();
            //Delete();
            Update();
            System.Console.WriteLine("执行完毕!");
            System.Console.ReadKey();
        }

        private static void Insert()
        {
            List<TestEntity> testList = new List<TestEntity>();
            for (int i = 0; i < 9; i++)
            {
                testList.Add(new TestEntity() { Id = i.ToString(), Name = String.Format("Test{0}", i) });
            }
            db.Insert<TestEntity>(testList);
        }
        private static void Update()
        {
            var result = new TestEntity() { Id = "0", Name = "MyTest" };
            db.Update(result);
        }
        private static void Delete()
        {
            // var result = db.FindEntity<TestEntity>("1");

            //var result1 = new TestEntity() { Id = "2" };
            //db.Delete(result1);


        //    db.Delete<TestEntity>("4");

            object[] id = { "5","6"};
            db.Delete<TestEntity>(id);

        }
        private static void Find()
        {
            var result = db.FindEntity<TestEntity>("1");


            if(result != null)
            System.Console.WriteLine("Name:{0}", result.Name);
        }

        private static void Find2()
        {
            // Expression<Func<TestEntity,int, bool>> expression = (Param,num) => Param.Id == num.ToString();

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
    }
}
