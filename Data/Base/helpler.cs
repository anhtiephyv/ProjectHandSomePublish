using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Data.Base
{
    public static class helpler
    {
        public static Expression<Func<TSource, object>> GetExpression<TSource>(string propertyName)
        {
            var param = Expression.Parameter(typeof(TSource), "x");
            Expression conversion = Expression.Convert(Expression.Property
            (param, propertyName), typeof(object));   //important to use the Expression.Convert
            return Expression.Lambda<Func<TSource, object>>(conversion, param);
        }

        //makes deleget for specific prop
        public static Func<TSource, object> GetFunc<TSource>(string propertyName)
        {
            return GetExpression<TSource>(propertyName).Compile();  //only need compiled expression
        }

        //OrderBy overload
        public static IOrderedEnumerable<TSource>
        OrderByName<TSource>(this IEnumerable<TSource> source, string propertyName)
        {
            return source.OrderBy(GetFunc<TSource>(propertyName));
        }

        //OrderBy overload
        public static IOrderedQueryable<TSource>
        OrderByName<TSource>(this IQueryable<TSource> source, string propertyName, string sortDir)
        {
            if (sortDir == "desc")
            {
                source.OrderByDescending(GetExpression<TSource>(propertyName));
            }
            return source.OrderBy(GetExpression<TSource>(propertyName));
        }
        public static object GetPropertyValue(object obj, string property)
        {
            System.Reflection.PropertyInfo propertyInfo = obj.GetType().GetProperty(property);
            return propertyInfo.GetValue(obj, null);
        }

    }
}