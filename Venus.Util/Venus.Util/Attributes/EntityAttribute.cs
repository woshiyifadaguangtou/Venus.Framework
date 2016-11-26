using System;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace Venus.Util.Attributes
{
    public class EntityAttribute
    {
        /// <summary>
        /// 获取实体主键的字段名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetEntityKey<T>()
        {
            Type type = typeof(T);
            PropertyInfo[] propertyInfos = type.GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                foreach (System.Attribute attribute in propertyInfo.GetCustomAttributes(true))
                {
                    KeyAttribute key = attribute as KeyAttribute;
                    if (key != null)
                    {
                        return propertyInfo.Name;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 获取实体对象的表名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetEntityTable<T>()
        {
            Type objTye = typeof(T);
            string entityName = "";
            var tableAttribute = objTye.GetCustomAttributes(true).OfType<TableAttribute>();
            var descriptionAttributes = tableAttribute as TableAttribute[] ?? tableAttribute.ToArray();
            if (descriptionAttributes.Any())
                entityName = descriptionAttributes.ToList()[0].Name;
            else
            {
                entityName = objTye.Name;
            }
            return entityName;
        }


    }
}
