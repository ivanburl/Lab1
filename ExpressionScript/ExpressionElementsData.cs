using System.Text.RegularExpressions;

namespace ExpressionScript;

public static class ExpressionElementsData
{
    public static List<KeyValuePair<String, ExpressionElement>> SimpleElementsNames { get; } = new()
    {
        new(",", ExpressionElement.Operator),
       new("(", ExpressionElement.Operator),
       new(")", ExpressionElement.Operator),
       new("<", ExpressionElement.Operator),
       new(">", ExpressionElement.Operator),
       new("=", ExpressionElement.Operator),
       new("div(", ExpressionElement.Function),
       new("mod(", ExpressionElement.Function),
       new("or", ExpressionElement.Operator),
       new("and", ExpressionElement.Operator),
       new("not", ExpressionElement.Operator),
       new("mmax", ExpressionElement.Function),
       new("mmin", ExpressionElement.Function),
       new ("true", ExpressionElement.Boolean),
       new ("false", ExpressionElement.Boolean),
    };
    
    public static List<KeyValuePair<Regex, ExpressionElement>> ComplexElementsNames { get; } = new()
    {
        new(new Regex(@"^\d+$"), ExpressionElement.Number),
        new (new Regex(@"^[A-Z]+\d+$"), ExpressionElement.Variable),
    };
}