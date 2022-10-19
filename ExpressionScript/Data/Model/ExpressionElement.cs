namespace ExpressionScript.Data.Model;

public readonly struct ExpressionElement
{
    private bool Equals(ExpressionElement other)
    {
        return Expression == other.Expression && ExpressionType == other.ExpressionType;
    }

    public override bool Equals(object? obj)
    {
        return obj is ExpressionElement other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Expression, (int)ExpressionType);
    }

    public ExpressionElement(string expression, ExpressionElementType expressionType)
    {
        Expression = expression;
        ExpressionType = expressionType;
    }

    public string Expression { get; init; }
    public ExpressionElementType ExpressionType { get; init; }

    public override string ToString()
    {
        return $" [ {Expression} , {ExpressionType} ]";
    }
}