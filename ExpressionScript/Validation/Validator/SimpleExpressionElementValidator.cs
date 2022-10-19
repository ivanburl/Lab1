using DataStructures;
using ExpressionScript.Data;

namespace ExpressionScript.Validation.Validator;

public class SimpleExpressionElementValidator : IValidator<string, ExpressionElementType>
{
    private readonly Bor<char, ExpressionElementType> _bor;


    public SimpleExpressionElementValidator()
    {
        _bor = new Bor<char, ExpressionElementType>();
        foreach (var i in ExpressionElementsData.SimpleElementsNames) _bor.AddElement(i.Key, i.Value);
    }

    public ExpressionElementType ValidationResult(string code)
    {
        return _bor.HasPrefix(code) ? _bor[code] : ExpressionElementType.UndefinedElement;
    }
}