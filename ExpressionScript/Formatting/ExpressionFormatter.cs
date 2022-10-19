using System.Text.RegularExpressions;

namespace ExpressionScript.Formatting;

public class ExpressionFormatter : IFormatter
{
    public string Format(string expression)
    {
        return Regex.Replace(expression, @"\s+", "");
    }
}