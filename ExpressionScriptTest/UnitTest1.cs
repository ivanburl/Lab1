using ExpressionScript.Optimization;
using ExpressionScript.Validation;

namespace ExpressionScriptTest;

public class Tests
{
    private ExpressionOptimizer _optimizer;
    private ExpressionSyntaxValidator _validator;
    [SetUp]
    public void Setup()
    {
        _validator = new ExpressionSyntaxValidator();
        _optimizer = new ExpressionOptimizer();
    }

    [Test]
    public void Test1()
    {
        String code1 = "div(div(mod(10,20),40),mmax(10,20,30))";
        var res = _optimizer.OptimizeExpression(code1);
        Console.WriteLine(res.Count);
        foreach (var i in res)
        {
            Console.WriteLine(i.ToString());
        }
        
        Console.WriteLine(_validator.ValidationResult(code1));
    }
}