namespace ExpressionScript.Validation;

public interface IValidator<in T, out TV>
{
    TV ValidationResult(T code);
}