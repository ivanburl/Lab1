namespace ExpressionScript.PreProcessing;

public interface IPreprocessing<T, TV>
{
    TV ParseConstants(TV expression, IDictionary<T, T> constants);
}