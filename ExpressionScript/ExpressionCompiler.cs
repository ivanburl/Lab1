using System.Numerics;

namespace ExpressionScript;

public class ExpressionCompiler
{
    private int i;
    private List<ExpressionElement> expression;
    public Func<Object> Compile(List<ExpressionElement> expression)
    {
        i = -1;
        this.expression = expression;
        return Parse();
    }

    private Func<Object> Parse()
    {
        i++;
        if (i>=expression.Count) return () => { } ;
        var elem = expression[i];


        switch (elem.Expression)
        {
            case "div(":
                return () =>
                {
                    return (BigInteger) Parse().Invoke();
                }
        }
    }
    }
}