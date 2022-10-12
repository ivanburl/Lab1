using ExpressionScript.Optimization;
using ExpressionScript.PreProcessing;

namespace ExpressionScript.Validation;

public class ExpressionSyntaxValidator : IValidator<String, String>
{
    public ExpressionSyntaxValidator()
    {
        _complexExpressionElementValidator = new ComplexExpressionElementValidator();
        _simpleExpressionElementValidator = new SimpleExpressionElementValidator();
        _expressionPreprocessing = new ExpressionPreprocessing();
    }

    private readonly IValidator<String, ExpressionElementType> _complexExpressionElementValidator;
    private readonly IValidator<String, ExpressionElementType> _simpleExpressionElementValidator;
    private readonly IPreprocessingExpression _expressionPreprocessing;

    private bool checkClosingElements(string code)
    {
        int sum = 0;

        foreach (var i in code)
        {
            if (i == '(') sum++;
            if (i == ')') sum--;
            if (sum < 0) return false;
        }
        if (sum != 0) return false;
        return true;
    }
    
    public bool ExpressionElementsCheck(String expression)
    {
        var cleanExpression = _expressionPreprocessing.Clean(expression);
        if (cleanExpression[^1] != ' ') cleanExpression += ' ';
        
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
                if (simpleValidation >= (ExpressionElementType) 1 ||
                    complexValidation >= (ExpressionElementType) 1)
                {
                    currentElement = String.Empty;
                    if (cleanExpression[i] != ' ') i--;
                    simpleValidationFlag = ExpressionElementType.UndefinedElement;
                    complexValidationFlag = ExpressionElementType.UndefinedElement;
                } else 
                    return false;
            }
            
            simpleValidation = simpleValidationFlag;
            complexValidation = complexValidationFlag;
        }
        return true;
    }
    
    public String ValidationResult(string code)
    {
        if (!checkClosingElements(code)) return "Problem with '(' and ')' characters";
        if (!ExpressionElementsCheck(code)) return "Problem with expression";
        return "No syntax errors detected";
    }
}