namespace ExpressionScript.Validation;

public class ComplexExpressionElementValidator : IValidator<String, ExpressionElementType>
{
    public ExpressionElementType ValidationResult(string code)
    {
        var complexElementNames = ExpressionElementsData.ComplexElementsNames;
        ExpressionElementType res = ExpressionElementType.UndefinedElement;
        foreach (var i in complexElementNames)
        {
            if (i.Key.IsMatch(code))
            {
                if (res != ExpressionElementType.UndefinedElement) return ExpressionElementType.PartiallyDefinedElement;
                res = i.Value;
            }
        }

        return res;
    }
}