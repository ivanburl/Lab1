using ExpressionScript.Data;
using ExpressionScript.Data.Model;
using ExpressionScript.Formatting;
using ExpressionScript.Validation;
using ExpressionScript.Validation.Validator;

namespace ExpressionScript.Optimization;

public class ExpressionOptimizer : IOptimizer<string, List<ExpressionElement>>
{
    private readonly IValidator<string, ExpressionElementType> _complexExpressionElementValidator;
    private readonly IFormatter _expressionPreprocessing;
    private readonly IValidator<string, ExpressionElementType> _simpleExpressionElementValidator;

    public ExpressionOptimizer()
    {
        _complexExpressionElementValidator = new ComplexExpressionElementValidator();
        _simpleExpressionElementValidator = new SimpleExpressionElementValidator();
        _expressionPreprocessing = new ExpressionFormatter();
    }

    public List<ExpressionElement> Optimize(string expression)
    {
        var cleanExpression = _expressionPreprocessing.Format(expression);
        if (cleanExpression[^1] != ' ') cleanExpression += ' ';

        var res = new List<ExpressionElement>();
        var currentElement = string.Empty;
        var simpleValidation = ExpressionElementType.UndefinedElement;
        var complexValidation = ExpressionElementType.UndefinedElement;

        for (var i = 0; i < cleanExpression.Length; i++)
        {
            currentElement += cleanExpression[i];
            var simpleValidationFlag = _simpleExpressionElementValidator.ValidationResult(currentElement);
            var complexValidationFlag = _complexExpressionElementValidator.ValidationResult(currentElement);

            if (simpleValidationFlag == (ExpressionElementType)(-1) &&
                complexValidationFlag == (ExpressionElementType)(-1))
            {
                currentElement = currentElement.Substring(0, currentElement.Length - 1);
                if (simpleValidation >= (ExpressionElementType)1)
                    res.Add(new ExpressionElement(currentElement, simpleValidation));
                else if (complexValidation >= (ExpressionElementType)1)
                    res.Add(new ExpressionElement(currentElement, complexValidation));
                else throw new InvalidOperationException("Undefined part of expression " + currentElement + " " + i);

                currentElement = string.Empty;
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