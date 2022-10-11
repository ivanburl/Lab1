using ExpressionScript.Optimization;

namespace ExpressionScript.Validation;

public class ExpressionSyntaxValidator : IValidator<String, String> //TODO improve Error Validation
{
    public ExpressionSyntaxValidator()
    {
        _complexExpressionElementValidator = new ComplexExpressionElementValidator();
        _simpleExpressionElementValidator = new SimpleExpressionElementValidator();
        _expressionOptimizer = new ExpressionOptimizer();
    }

    private readonly ComplexExpressionElementValidator _complexExpressionElementValidator;
    private readonly SimpleExpressionElementValidator _simpleExpressionElementValidator;
    private readonly ExpressionOptimizer _expressionOptimizer;

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
        var cleanExpression = _expressionOptimizer.CleanExpression(expression) + ' ';
        var currentElement = String.Empty;
        var simpleValidation = ExpressionElement.UndefinedElement;
        var complexValidation = ExpressionElement.UndefinedElement;
        
        for (int i = 0; i < cleanExpression.Length; i++)
        {
            currentElement += cleanExpression[i];
            ExpressionElement simpleValidationFlag = _simpleExpressionElementValidator.ValidationResult(currentElement);
            ExpressionElement complexValidationFlag = _complexExpressionElementValidator.ValidationResult(currentElement);

            if (simpleValidationFlag == (ExpressionElement)(-1) && 
                complexValidationFlag == (ExpressionElement)(-1))
            {
                if (simpleValidation >= (ExpressionElement) 1 ||
                    complexValidation >= (ExpressionElement) 1)
                {
                    currentElement = String.Empty;
                    if (cleanExpression[i] != ' ') i--;
                    simpleValidationFlag = ExpressionElement.UndefinedElement;
                    complexValidationFlag = ExpressionElement.UndefinedElement;
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