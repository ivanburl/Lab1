using System.Text.RegularExpressions;

namespace ExpressionScript;

public static class ExpressionElementsData
{
    public static List<KeyValuePair<String, ExpressionElementType>> SimpleElementsNames { get; } = new()
    { 
        new ("-", ExpressionElementType.Operator),
        new("+", ExpressionElementType.Operator),
        new(",", ExpressionElementType.Operator),
       new("(", ExpressionElementType.Operator),
       new(")", ExpressionElementType.Operator),
       new("<", ExpressionElementType.Operator),
       new(">", ExpressionElementType.Operator),
       new("=", ExpressionElementType.Operator),
       new("div(", ExpressionElementType.Function),
       new("mod(", ExpressionElementType.Function),
       new("or", ExpressionElementType.Operator),
       new("and", ExpressionElementType.Operator),
       new("not", ExpressionElementType.Operator),
       new("mmax(", ExpressionElementType.Function),
       new("mmin(", ExpressionElementType.Function),
       new ("true", ExpressionElementType.Boolean),
       new ("false", ExpressionElementType.Boolean),
    };
    
    public static List<KeyValuePair<Regex, ExpressionElementType>> ComplexElementsNames { get; } = new()
    {
        new(new Regex(@"^-?\d+$"), ExpressionElementType.Number),
        new (new Regex(@"^[A-Z]+\d+$"), ExpressionElementType.Variable),
    };
}