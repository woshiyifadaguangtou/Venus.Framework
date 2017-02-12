using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Venus.Data.EF
{
    public class SqlServerDbContext:DbContext,IDbContext
    {
        /// <summary>
        /// 为数据迁移添加构造函数
        /// </summary>
        public SqlServerDbContext()
        {
            System.Diagnostics.Debug.Write(Database.Connection.ConnectionString);
            System.Console.WriteLine(Database.Connection.ConnectionString);
        }
        public SqlServerDbContext(string connString):base(connString)
        {
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.ValidateOnSaveEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //string assembleFileName = Assembly.GetExecutingAssembly().CodeBase;
            string assembleFileName = Assembly.GetExecutingAssembly().CodeBase.Replace("Venus.Data.EF.DLL", "Venus.Application.Mapping.dll").Replace("file:///", "");
            Assembly asm = Assembly.LoadFile(@assembleFileName);
            var typesToRegister = asm.GetTypes()
                .Where(type => !String.IsNullOrEmpty(type.Namespace))
              //   .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
              .Where(type => type.BaseType != null && type.BaseType.BaseType != null && type.BaseType.BaseType.IsGenericType && type.BaseType.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)).ToList();
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
