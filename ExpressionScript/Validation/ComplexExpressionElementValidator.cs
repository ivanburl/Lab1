namespace ExpressionScript.Validation;

public class ComplexExpressionElementValidator : IValidator<String, ExpressionElement>
{
    public ExpressionElement ValidationResult(string code)
    {
        var complexElementNames = ExpressionElementsData.ComplexElementsNames;
        ExpressionElement res = ExpressionElement.UndefinedElement;
        foreach (var i in complexElementNames)
        {
            if (i.Key.IsMatch(code))
            {
                if (res != ExpressionElement.UndefinedElement) return ExpressionElement.PartiallyDefinedElement;
                res = i.Value;
            }
        }

        return res;
    }
}