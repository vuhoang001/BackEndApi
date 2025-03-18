using System.Linq.Expressions;
using BackEndApi.Exceptions;

namespace BackEndApi.Services.Common;

public abstract class IValidationHandler<T> where T : class
{
    private IValidationHandler<T>? _next;

    public IValidationHandler<T> SetNext(IValidationHandler<T> next)
    {
        _next = next;
        return next;
    }

    public virtual async Task HandleAsync(T entity)
    {
        if (entity != null)
            await _next!.HandleAsync(entity);
    }

    protected static void ExceptTwoList<T>(IEnumerable<T> firstList, IEnumerable<T> secondList, string errorMessage,
        string errCode)
    {
        var missingItems = firstList.Except(secondList).ToList();
        if (missingItems.Count == 0) return;

        var finalMessage = string.Join(", ", missingItems);
        throw new BadRequestException(errCode, finalMessage);
    }

    protected static List<T> ValidateAndMapEntities<T>(
        List<string> codes,
        IQueryable<T> dbSet,
        Expression<Func<T, string>> keySelectorExpr,
        string errorMessage,
        string errorCode) where T : class
    {
        if (codes.Count == 0) return [];

        // Convert Expression to Func
        var keySelector = keySelectorExpr.Compile();

        // Query trực tiếp trên database với Expression
        var entities = dbSet
            .Where(ExpressHelper.BuildContainsExpression(keySelectorExpr, codes))
            .ToList();

        ExceptTwoList(codes, entities.Select(keySelector), errorMessage, errorCode);

        return entities;
    }

    private static class ExpressHelper
    {
        public static Expression<Func<T, bool>> BuildContainsExpression<T>(Expression<Func<T, string>> keySelector,
            List<string> codes)
        {
            var param = keySelector.Parameters[0];

            var body = Expression.Call(
                Expression.Constant(codes),
                typeof(List<string>).GetMethod("Contains", [typeof(string)])!, keySelector.Body
            );
            return Expression.Lambda<Func<T, bool>>(body, param);
        }
    }
}