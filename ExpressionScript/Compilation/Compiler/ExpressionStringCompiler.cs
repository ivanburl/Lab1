using System.Data;
using ExpressionScript.Data.Model;
using ExpressionScript.Parsing;
using ExpressionScript.PreProcessing;
using ExpressionScript.Validation;
using ExpressionScript.Validation.Model;
using ExpressionScript.Validation.Validator;

namespace ExpressionScript.Compilation.Compiler;

public class ExpressionStringCompiler : ICompiler<string>
{
    private readonly ICompiler<List<ExpressionElement>> _compiler =
        new ExpressionCompiler();

    private readonly IParser<List<ExpressionElement>> _parser =
        new ExpressionParser();

    private readonly IPreprocessing<ExpressionElement, List<ExpressionElement>> _preprocessing =
        new ExpressionPreprocessing();

    private readonly IValidator<string, SyntaxError> _validator =
        new ExpressionSyntaxValidator();

    public IDictionary<ExpressionElement, ExpressionElement> Constants { get; set; }

    public ExpressionStringCompiler(IDictionary<ExpressionElement, ExpressionElement> constants)
    {
        Constants = constants;
    }

    public Func<object> Compile(string expression)
    {
        var error = _validator.ValidationResult(expression);

        if (error.IsError)
            throw new SyntaxErrorException(
                $"{error.ErrorDescription} Start char: {error.StartChar} End char: {error.EndChar}");

        var expressionElementsList = _parser.parse(expression);

        expressionElementsList = _preprocessing.ParseConstants(expressionElementsList, Constants);

        return _compiler.Compile(expressionElementsList);
    }
}