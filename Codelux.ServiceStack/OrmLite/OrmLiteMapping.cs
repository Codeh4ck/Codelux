using System;
using System.Linq.Expressions;
using System.Reflection;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace Codelux.ServiceStack.OrmLite
{
    public abstract class OrmLiteMapping<TEntity> : IOrmLiteMapping
    {
        protected void Ignore(Expression<Func<TEntity, object>> expression)
        {
            PropertyInfo info = (PropertyInfo)ReflectionHelper.GetMemberInfo(expression);
            info.AddAttributes(new IgnoreAttribute());
        }

        protected void AutoIncrement(Expression<Func<TEntity, object>> expression)
        {
            PropertyInfo info = (PropertyInfo)ReflectionHelper.GetMemberInfo(expression);
            info.AddAttributes(new AutoIncrementAttribute());
        }

        protected void MapToColumn(Expression<Func<TEntity, object>> expression, string columnName)
        {
            PropertyInfo info = (PropertyInfo)ReflectionHelper.GetMemberInfo(expression);
            info.AddAttributes(new AliasAttribute(columnName));
        }

        protected void MapToTable(string tableName)
        {
            typeof(TEntity).AddAttributes(new AliasAttribute(tableName));
        }

        protected void MapToSchema(string schema)
        {
            typeof(TEntity).AddAttributes(new SchemaAttribute(schema));
        }
    }
}
