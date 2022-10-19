using System.Text.RegularExpressions;

namespace ExpressionScript.Data;

public static class ExpressionElementsData
{
    public static List<KeyValuePair<string, ExpressionElementType>> SimpleElementsNames { get; } = new()
    {
        new KeyValuePair<string, ExpressionElementType>(",", ExpressionElementType.Operator),
        new KeyValuePair<string, ExpressionElementType>("(", ExpressionElementType.Operator),
        new KeyValuePair<string, ExpressionElementType>(")", ExpressionElementType.Operator),
        new KeyValuePair<string, ExpressionElementType>("<", ExpressionElementType.Operator),
        new KeyValuePair<string, ExpressionElementType>(">", ExpressionElementType.Operator),
        new KeyValuePair<string, ExpressionElementType>("=", ExpressionElementType.Operator),
        new KeyValuePair<string, ExpressionElementType>("div(", ExpressionElementType.Function),
        new KeyValuePair<string, ExpressionElementType>("mod(", ExpressionElementType.Function),
        new KeyValuePair<string, ExpressionElementType>("or", ExpressionElementType.Operator),
        new KeyValuePair<string, ExpressionElementType>("and", ExpressionElementType.Operator),
        new KeyValuePair<string, ExpressionElementType>("not", ExpressionElementType.Operator),
        new KeyValuePair<string, ExpressionElementType>("mmax(", ExpressionElementType.Function),
        new KeyValuePair<string, ExpressionElementType>("mmin(", ExpressionElementType.Function),
        new KeyValuePair<string, ExpressionElementType>("true", ExpressionElementType.Boolean),
        new KeyValuePair<string, ExpressionElementType>("false", ExpressionElementType.Boolean)
    };

    public static List<KeyValuePair<Regex, ExpressionElementType>> ComplexElementsNames { get; } = new()
    {
        new KeyValuePair<Regex, ExpressionElementType>(new Regex(@"^-$"), ExpressionElementType.PartiallyDefinedElement),
        new KeyValuePair<Regex, ExpressionElementType>(new Regex(@"^[A-Z]+$"), ExpressionElementType.PartiallyDefinedElement),
        new KeyValuePair<Regex, ExpressionElementType>(new Regex(@"^-?\d+$"), ExpressionElementType.Number),
        new KeyValuePair<Regex, ExpressionElementType>(new Regex(@"^[A-Z]+\d+$"), ExpressionElementType.Constant),
        
    };
}