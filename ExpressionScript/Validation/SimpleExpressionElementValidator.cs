using DataStructures;

namespace ExpressionScript.Validation;

public class SimpleExpressionElementValidator : IValidator<String, ExpressionElement>
{
    private readonly Bor<char, ExpressionElement> _bor;
    
    
    public SimpleExpressionElementValidator()
    {
        _bor = new Bor<char, ExpressionElement>();
        foreach (var i in ExpressionElementsData.SimpleElementsNames)
        {
            _bor.AddElement(i.Key,i.Value);
        }
    }
    
    public ExpressionElement ValidationResult(string code)
    {
        return _bor.HasPrefix(code) ? _bor[code] : ExpressionElement.UndefinedElement;
    }
}