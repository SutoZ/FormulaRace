using System.Linq.Expressions;

namespace TeamManagementService.Application.Expressions;

public class ReplaceExpressionVisitor(Expression oldValue, Expression newValue) : ExpressionVisitor
{
    public override Expression Visit(Expression node)
    {
        if (node == oldValue)
            return newValue;

        return base.Visit(node);
    }
}