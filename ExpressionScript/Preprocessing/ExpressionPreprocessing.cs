using ExpressionScript.Data;
using ExpressionScript.Data.Model;

namespace ExpressionScript.PreProcessing;

public class ExpressionPreprocessing : IPreprocessing<ExpressionElement, List<ExpressionElement>>
{
    public List<ExpressionElement> ParseConstants(List<ExpressionElement> expression,
        IDictionary<ExpressionElement, ExpressionElement> constants)
    {
        for (var i = 0; i < expression.Count; i++)
            if (expression[i].ExpressionType == ExpressionElementType.Constant)
            {
                if (!constants.ContainsKey(expression[i]))
                    throw new KeyNotFoundException($"{expression[i].Expression} was not found in given constants.");
                expression[i] = constants[expression[i]];
            }

        return expression;
    }
}