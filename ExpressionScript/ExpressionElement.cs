using System.Linq.Expressions;

namespace ExpressionScript;
public readonly struct ExpressionElement
{
    public ExpressionElement(string expression, ExpressionElementType expressionType)
    {
        Expression = expression;
        ExpressionType = expressionType;
    }

    public String Expression { get; init; }
    public ExpressionElementType ExpressionType { get; init; }

    public override string ToString()
    {
        return $" [ {Expression} , {ExpressionType} ]";
    }
}