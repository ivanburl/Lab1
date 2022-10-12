using System.Text.RegularExpressions;
using ExpressionScript.PreProcessing;
using ExpressionScript.Validation;

namespace ExpressionScript.Optimization;

public class ExpressionOptimizer : IOptimizer<String,List<ExpressionElement>>
{
    public ExpressionOptimizer()
    {
        _complexExpressionElementValidator = new ComplexExpressionElementValidator();
        _simpleExpressionElementValidator = new SimpleExpressionElementValidator();
        _expressionPreprocessing = new ExpressionPreprocessing();
    }

    private readonly IValidator<String, ExpressionElementType> _complexExpressionElementValidator;
    private readonly IValidator<String, ExpressionElementType> _simpleExpressionElementValidator;
    private readonly IPreprocessingExpression _expressionPreprocessing;
    
    public List<ExpressionElement> Optimize(String expression)
    {
        var cleanExpression = _expressionPreprocessing.Clean(expression);
        if (cleanExpression[^1] != ' ') cleanExpression += ' ';
        
        var res = new List<ExpressionElement>();
        var currentElement = String.Empty;
        var simpleValidation = ExpressionElementType.UndefinedElement;
        var complexValidation = ExpressionElementType.UndefinedElement;
        
        for (int i = 0; i < cleanExpression.Length; i++)
        {
            currentElement += cleanExpression[i];
            ExpressionElementType simpleValidationFlag = _simpleExpressionElementValidator.ValidationResult(currentElement);
            ExpressionElementType complexValidationFlag = _complexExpressionElementValidator.ValidationResult(currentElement);

            if (simpleValidationFlag == (ExpressionElementType)(-1) && 
                complexValidationFlag == (ExpressionElementType)(-1))
            {
                currentElement = currentElement.Substring(0, currentElement.Length - 1);
                if (simpleValidation >= (ExpressionElementType)1)
                    res.Add(new(currentElement, simpleValidation));
                else if (complexValidation >= (ExpressionElementType)1)
                    res.Add(new(currentElement, complexValidation));
                else throw new Exception("Undefined part of expression " + currentElement);
                
                currentElement = String.Empty;
                if (cleanExpression[i] != ' ') i--;
                simpleValidationFlag = ExpressionElementType.UndefinedElement;
                complexValidationFlag = ExpressionElementType.UndefinedElement;
            }
            
            simpleValidation = simpleValidationFlag;
            complexValidation = complexValidationFlag;
        }
        return res;
    }
}