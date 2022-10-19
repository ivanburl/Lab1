namespace ExpressionScript.Compilation;

public interface ICompiler<in T>
{
    Func<object> Compile(T expression);
}