using DataStructures;

namespace ExpressionScript.Validation;

public class SimpleExpressionElementValidator : IValidator<String, ExpressionElementType>
{
    private readonly Bor<char, ExpressionElementType> _bor;
    
    
    public SimpleExpressionElementValidator()
    {
        _bor = new Bor<char, ExpressionElementType>();
        foreach (var i in ExpressionElementsData.SimpleElementsNames)
        {
            _bor.AddElement(i.Key,i.Value);
        }
    }
    
    public ExpressionElementType ValidationResult(string code)
    {
        return _bor.HasPrefix(code) ? _bor[code] : ExpressionElementType.UndefinedElement;
    }
}