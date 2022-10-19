namespace ExpressionScript.Validation.Model;

public readonly record struct SyntaxError(bool IsError, string ErrorDescription, int StartChar, int EndChar);