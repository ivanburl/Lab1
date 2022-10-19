namespace ExpressionScript.Parsing;

public interface IParser<T>
{
    T parse(string expression);
}