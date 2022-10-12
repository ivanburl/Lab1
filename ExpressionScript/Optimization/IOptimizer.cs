namespace ExpressionScript.Optimization;

public interface IOptimizer<in T, out TV>
{
    TV Optimize(T expression);
}