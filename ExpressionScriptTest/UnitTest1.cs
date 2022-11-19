using ExpressionScript.Compilation;
using ExpressionScript.Compilation.Compiler;
using ExpressionScript.Data;
using ExpressionScript.Data.Model;
using ExpressionScript.Optimization;
using ExpressionScript.Validation.Validator;

namespace ExpressionScriptTest;

public class Tests
{
    private ExpressionStringCompiler _compiler;
    private Dictionary<ExpressionElement, ExpressionElement> constants = new ();
    [SetUp]
    public void Setup()
    {
        constants.Add(new ExpressionElement("A11", ExpressionElementType.Constant), new ExpressionElement("10", ExpressionElementType.Number));
        constants.Add(new ExpressionElement("C2", ExpressionElementType.Constant), new ExpressionElement("20", ExpressionElementType.Number));
        _compiler = new ExpressionStringCompiler(constants);
    }

    [Test]
    public void Test1()
    {
        String code1 = "div(A11,2)";
        var res = _compiler.Compile(code1);
        Console.WriteLine(res.Invoke());
    }

    [Test]
    public void Test2()
    {
        String code2 = "(div(4,1)>2) = true";
        var res = _compiler.Compile(code2);
        Console.WriteLine(res.Invoke());
    }
    
    [Test]
    public void TestSimple()
    {
        String code2 = "div(100,mod(10,20))";
        var res = _compiler.Compile(code2);
        Console.WriteLine(res.Invoke());
    }

    [Test]
    public void Test3()
    {
        String code2 = "mmax(10,20,-10, 1000000000)";
        var res = _compiler.Compile(code2);
        Console.WriteLine(res.Invoke());
    }
    
    [Test]
    public void Test4()
    {
        String code2 = "-100";
        var res = _compiler.Compile(code2);
        Console.WriteLine(res.Invoke());
    }
}