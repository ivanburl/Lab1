using System.Numerics;
using ExpressionScript.Data;
using ExpressionScript.Data.Model;

namespace ExpressionScript.Compilation.Compiler;

public class ExpressionCompiler : ICompiler<List<ExpressionElement>>
{
    private List<ExpressionElement> _expression = new();
    private int _i;

    public Func<object> Compile(List<ExpressionElement> expression)
    {
        _i = -1;
        _expression = expression;
        return Parse(() => new object());
    }

    private Func<object> Parse(object o)
    {
        _i++;
        if (_i >= _expression.Count) return () => o;
        var elem = _expression[_i];


        switch (elem.ExpressionType)
        {
            case ExpressionElementType.Number:
            {
                return () => Parse(BigInteger.Parse(elem.Expression)).Invoke();
            }
            case ExpressionElementType.Boolean:
            {
                return () => Parse(bool.Parse(elem.Expression)).Invoke();
            }
        }

        switch (elem.Expression)
        {
            case ",":
            {
                return () => o;
            }
            case "div(":
            {
                return () =>
                {
                    var arg1 = Parse(new BigInteger()).Invoke();
                    var arg2 = Parse(new BigInteger()).Invoke();
                    var res = BigInteger.Divide((BigInteger)arg1, (BigInteger)arg2);
                    return Parse(res).Invoke();
                };
            }
            case "mod(":
            {
                return () =>
                {
                    var arg1 = Parse(new BigInteger()).Invoke();
                    var arg2 = Parse(new BigInteger()).Invoke();
                    BigInteger.DivRem((BigInteger)arg1, (BigInteger)arg2, out var res);
                    return Parse(res).Invoke();
                };
            }
            case "(":
            {
                return () =>
                {
                    var insideExpression = Parse(new object()).Invoke();
                    return Parse(insideExpression).Invoke();
                };
            }
            case ")":
            {
                return () => o;
            }
            case "mmax(":
            {
                return () =>
                {
                    BigInteger res = default;
                    while (_expression[_i].Expression != ")")
                    {
                        var argN = Parse(() => new BigInteger()).Invoke();
                        res = BigInteger.Max(res, (BigInteger)argN);
                    }

                    return Parse(res).Invoke();
                };
            }
            case "mmin(":
            {
                return () =>
                {
                    BigInteger res = default;
                    while (_expression[_i].Expression != ")")
                    {
                        var argN = Parse(() => new BigInteger()).Invoke();
                        res = BigInteger.Min(res, (BigInteger)argN);
                    }

                    return Parse(res).Invoke();
                };
            }
        }

        switch (elem.Expression)
        {
            case "or":
            {
                return () => (bool)o || (bool)Parse(new bool()).Invoke();
            }
            case "and":
            {
                return () => (bool)o && (bool)Parse(new bool()).Invoke();
            }
            case "not":
            {
                return () => !(bool)Parse(new bool()).Invoke();
            }
            case "=":
            {
                return () =>
                {
                    var arg1 = Parse(new object()).Invoke();
                    return ((BigInteger)o).Equals(arg1);
                };
            }
            case ">":
            {
                return () =>
                {
                    var arg1 = Parse(new object()).Invoke();
                    return ((BigInteger)o).CompareTo(arg1) > 0;
                };
            }
            case "<":
            {
                return () =>
                {
                    var arg1 = Parse(new object()).Invoke();
                    return ((BigInteger)o).CompareTo(arg1) < 0;
                };
            }
        }

        throw new InvalidOperationException("Unsupported operation or function " + _expression[_i]);
    }
}