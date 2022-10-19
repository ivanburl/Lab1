using ExpressionScript.Data;
using ExpressionScript.Formatting;
using ExpressionScript.Validation.Model;

namespace ExpressionScript.Validation.Validator;

public class ExpressionSyntaxValidator : IValidator<string, SyntaxError>
{
    private readonly IValidator<string, ExpressionElementType> _complexExpressionElementValidator;
    private readonly IFormatter _preprocessing;
    private readonly IValidator<string, ExpressionElementType> _simpleExpressionElementValidator;

    public ExpressionSyntaxValidator()
    {
        _complexExpressionElementValidator = new ComplexExpressionElementValidator();
        _simpleExpressionElementValidator = new SimpleExpressionElementValidator();
        _preprocessing = new ExpressionFormatter();
    }

    public SyntaxError ValidationResult(string code)
    {
        var error = checkClosingElements(code);
        if (error.IsError) return error;
        error = ExpressionElementsCheck(code);
        if (error.IsError) return error;

        return error;
    }

    private SyntaxError checkClosingElements(string expression)
    {
        var sum = 0;

        for (var i = 0; i < expression.Length; i++)
        {
            if (expression[i] == '(') sum++;
            if (expression[i] == ')') sum--;
            if (sum < 0) return new SyntaxError(true, SyntaxErrorDescription.TooManyClosingBrackets, i, i + 1);
        }

        if (sum > 0)
            return new SyntaxError(true,
                SyntaxErrorDescription.NotEnoughBrackets,
                expression.Length - 1,
                expression.Length);

        return new SyntaxError(false, SyntaxErrorDescription.OK, -1, -1);
    }

    private SyntaxError ExpressionElementsCheck(string expression)
    {
        var cleanExpression = _preprocessing.Format(expression);
        if (cleanExpression[^1] != ' ') cleanExpression += ' ';

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
                if (simpleValidation >= (ExpressionElementType)1 ||
                    complexValidation >= (ExpressionElementType)1)
                {
                    currentElement = string.Empty;
                    if (cleanExpression[i] != ' ') i--;
                    simpleValidationFlag = ExpressionElementType.UndefinedElement;
                    complexValidationFlag = ExpressionElementType.UndefinedElement;
                }
                else
                {
                    return new SyntaxError(false,
                        SyntaxErrorDescription.UnexpectedElement,
                        i - currentElement.Length,
                        i + 1); //TODO need to return appropriate indexes to original one
                }
            }

            simpleValidation = simpleValidationFlag;
            complexValidation = complexValidationFlag;
        }

        return new SyntaxError(false, SyntaxErrorDescription.OK, -1, -1);
    }

    private class SyntaxErrorDescription
    {
        public static readonly string OK = "No syntax errors detected. ";
        public static readonly string TooManyClosingBrackets = "Too many closing brackets. ";
        public static readonly string NotEnoughBrackets = "Not enough closing brackets. ";
        public static readonly string UnexpectedElement = "Unexpected expression element was found. ";
    }
}