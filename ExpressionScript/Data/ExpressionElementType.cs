namespace ExpressionScript.Data;

public enum ExpressionElementType
{
    UndefinedElement = -1,
    PartiallyDefinedElement = 0,
    Function = 1,
    Operator = 2,
    Boolean = 3,
    Number = 4,
    Constant = 5
}