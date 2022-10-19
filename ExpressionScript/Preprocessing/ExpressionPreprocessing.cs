using System.Text.RegularExpressions;

namespace ExpressionScript.PreProcessing;

public class ExpressionPreprocessing : IPreprocessingExpression
{
    public string Clean(string expression)
    {
        Regex.Replace(expression, @"\s{2,}", " "); 
        return expression;
    }

    public void ParseConstants(List<ExpressionElement> expression, IDictionary<String, ExpressionElement> constants) 
    {
        for (int i=0;i<expression.Count;i++)
        {
            if (expression[i].ExpressionType == ExpressionElementType.Variable)
            {
                expression[i] = constants[expression[i].Expression];
            }
        }
    }
}