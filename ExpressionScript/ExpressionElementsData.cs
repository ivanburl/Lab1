using System.Text.RegularExpressions;

namespace ExpressionScript;

public static class ExpressionScriptElementsData
{
    public static List<KeyValuePair<String, ExpressionScriptElementType>> SimpleElementsNames { get; } = new()
    {
        new(",", ExpressionScriptElementType.Operator),
       new("(", ExpressionScriptElementType.Operator),
       new(")", ExpressionScriptElementType.Operator),
       new("<", ExpressionScriptElementType.Operator),
       new(">", ExpressionScriptElementType.Operator),
       new("=", ExpressionScriptElementType.Operator),
       new("div(", ExpressionScriptElementType.Function),
       new("mod(", ExpressionScriptElementType.Function),
       new("or", ExpressionScriptElementType.Operator),
       new("and", ExpressionScriptElementType.Operator),
       new("not", ExpressionScriptElementType.Operator),
       new("mmax", ExpressionScriptElementType.Function),
       new("mmin", ExpressionScriptElementType.Function),
       new ("true", ExpressionScriptElementType.Boolean),
       new ("false", ExpressionScriptElementType.Boolean),
    };
    
    public static List<KeyValuePair<Regex, ExpressionScriptElementType>> ComplexElementsNames { get; } = new()
    {
        new(new Regex(@"^\d+$"), ExpressionScriptElementType.Number),
        new (new Regex(@"^[A-Z]+\d+$"), ExpressionScriptElementType.Variable),
    };
}