/// <summary>
/// A validator that will validate the resource name.
/// </summary>
public delegate ValidationResult Validate(string resourceName);

/// <summary>
/// Result for a validation.
/// </summary>
public record ValidationResult
{
    /// <summary>
    /// Name of the validator.
    /// </summary>
    public string ValidatorName { get; set; }

    /// <summary>
    /// Validation message.
    /// </summary>
    public string Message { get; set; }

    public static implicit operator ValidationResult(bool value)
    {
        return value ? Success : new ValidationResult();
    }

    public static implicit operator bool(ValidationResult value)
    {
        return value == Success;
    }

    public static readonly ValidationResult Success;
}
