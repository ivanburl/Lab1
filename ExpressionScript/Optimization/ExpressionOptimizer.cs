using System.Text.RegularExpressions;
using ExpressionScript.Validation;

namespace ExpressionScript.Optimization;

public class ExpressionOptimizer
{
    public ExpressionOptimizer()
    {
        _complexExpressionElementValidator = new ComplexExpressionElementValidator();
        _simpleExpressionElementValidator = new SimpleExpressionElementValidator();
    }

    private readonly ComplexExpressionElementValidator _complexExpressionElementValidator;
    private readonly SimpleExpressionElementValidator _simpleExpressionElementValidator;
    
    public List<KeyValuePair<String, ExpressionElement>> OptimizeExpression(String expression)
    {
        var cleanExpression = CleanExpression(expression)+' ';
        var res = new List<KeyValuePair<String, ExpressionElement>>();
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
                currentElement = currentElement.Substring(0, currentElement.Length - 1);
                if (simpleValidation >= (ExpressionElement)1)
                    res.Add(new(currentElement, simpleValidation));
                else if (complexValidation >= (ExpressionElement)1)
                    res.Add(new(currentElement, complexValidation));
                else throw new Exception("Undefined part of expression " + currentElement);
                
                currentElement = String.Empty;
                if (cleanExpression[i] != ' ') i--;
                simpleValidationFlag = ExpressionElement.UndefinedElement;
                complexValidationFlag = ExpressionElement.UndefinedElement;
            }
            
            simpleValidation = simpleValidationFlag;
            complexValidation = complexValidationFlag;
        }
        return res;
    }

    public String CleanExpression(String expression)
    {
        Regex.Replace(expression, @"\s{2,}", " ");
        return expression;
    }
}