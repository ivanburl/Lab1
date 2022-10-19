using ExpressionScript.Data;

namespace ExpressionScript.Validation.Validator;

public class ComplexExpressionElementValidator : IValidator<string, ExpressionElementType>
{
    public ExpressionElementType ValidationResult(string code)
    {
        var complexElementNames = ExpressionElementsData.ComplexElementsNames;
        var res = ExpressionElementType.UndefinedElement;
        foreach (var i in complexElementNames)
            if (i.Key.IsMatch(code))
            {
                if (res != ExpressionElementType.UndefinedElement) return ExpressionElementType.PartiallyDefinedElement;
                res = i.Value;
            }

        return res;
    }
}