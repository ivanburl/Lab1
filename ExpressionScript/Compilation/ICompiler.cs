using ExpressionScript.Parsing;
using ExpressionScript.Validation;

namespace ExpressionScript.Compilation;

public interface ICompiler<T>
{
    Func<object> Compile(T expression);
}