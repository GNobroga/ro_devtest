using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RO.DevTest.Domain.Models;

namespace RO.DevTest.Persistence.Extensions;

public static class IQueryableExtensions {

    public static IQueryable<T> WhereContainsIgnoreCaseMultiple<T>(this IQueryable<T> query, object value, params string[] properties) {
        var parameter = Expression.Parameter(typeof(T), "e");

        var bodyExpressions = properties.Select(property => {
            Expression propertyExpression = Expression.Property(parameter, property);

            if (propertyExpression.Type != typeof(string)) {
                propertyExpression =  Expression.Call(propertyExpression, "ToString", null);
            }
            
            var containsMethod = typeof(string).GetMethod("Contains", [typeof(string)])!;
            var constantValue = Expression.Constant(value.ToString(), typeof(string));
            
            var callContains = Expression.Call(
                Expression.Call(propertyExpression, "ToLower", null),
                containsMethod,
                Expression.Call(constantValue, "ToLower", null)
            );
            
            return callContains;
        });
     
        var combinedBody = bodyExpressions.Cast<Expression>().Aggregate(Expression.OrElse);

        var lambda = Expression.Lambda<Func<T, bool>>(combinedBody, parameter);

        return query.Where(lambda);
    }

    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int page, int pageSize) {
        page = page < 1 ? 1 : page; 
        pageSize = pageSize < 1 ? 100 : pageSize; 
        return query.Skip((page - 1) * pageSize).Take(pageSize);
    }

    public static IQueryable<T> OrderByProperty<T>(this IQueryable<T> query, string propertyName, bool ascending = true) {
        var parameter = Expression.Parameter(typeof(T), "e");
        var property = Expression.Property(parameter, propertyName);
        var lambda = Expression.Lambda(property, parameter);
        var methodName = ascending ? "OrderBy" : "OrderByDescending";
        var method = typeof(Queryable).GetMethods()
            .First(m => m.Name == methodName && m.GetParameters().Length == 2);
        
        var genericMethod = method.MakeGenericMethod(typeof(T), property.Type);
        var orderedQuery = genericMethod.Invoke(null, [ query, lambda ])!;
        return (IQueryable<T>)orderedQuery;
    }
}