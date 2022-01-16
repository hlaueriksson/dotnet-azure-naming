public static class Validator
{
    /// <summary>
    /// Resource name must start with an alphanumeric character
    /// </summary>
    /// <param name="resourceName">Name of the resource.</param>
    /// <returns>Validation result if invalid, otherwise true.</returns>
    public static ValidationResult startWithAlphanumeric(string resourceName)
    {
        // doesn't start with alphanumeric characters
        if (!"^[a-zA-Z0-9]".test(resourceName))
        {
            return new ValidationResult
            {
                ValidatorName = "startWithAlphanumeric",
                Message = "Resource name must start with an alphanumeric character",
            };
        }

        return true;
    }

    /// <summary>
    /// Resource name must start with letter.
    /// </summary>
    /// <param name="resourceName">Name of the resource.</param>
    /// <returns>Validation result if invalid, otherwise true.</returns>
    public static ValidationResult startWithLetter(string resourceName)
    {
        // doesn't start with letter
        if (!"^[a-zA-Z]".test(resourceName))
        {
            return new ValidationResult
            {
                ValidatorName = "startWithLetter",
                Message = "Resource name must start with letter.",
            };
        }

        return true;
    }

    /// <summary>
    /// Resource name must include at least 2 labels.
    /// </summary>
    /// <param name="resourceName">Name of the resource.</param>
    /// <returns>Validation result if invalid, otherwise true.</returns>
    public static ValidationResult atLeast2Labels(string resourceName)
    {
        // if the string has one dot `.` it has two labels
        if (!resourceName.Contains('.'))
        {
            return new ValidationResult
            {
                ValidatorName = "atLeast2Labels",
                Message = "name must include at least 2 labels",
            };
        }

        return true;
    }

    /// <summary>
    /// Resource name must end with alphanumeric.
    /// </summary>
    /// <param name="resourceName">Name of the resource.</param>
    /// <returns>Validation result if invalid, otherwise true.</returns>
    public static ValidationResult endWithAlphanumeric(string resourceName)
    {
        // if the last character is not alphanumeric
        if (!"[a-zA-Z0-9]$".test(resourceName))
        {
            return new ValidationResult
            {
                ValidatorName = "endWithAlphanumeric",
                Message = "Resource name must end with an alphanumeric character",
            };
        }

        return true;
    }

    /// <summary>
    /// Resource name must end with alphanumeric or underscore.
    /// </summary>
    /// <param name="resourceName">Name of the resource.</param>
    /// <returns>Validation result if invalid, otherwise true.</returns>
    public static ValidationResult endWithAlphanumericOrUnderscore(string resourceName)
    {
        // if the last character is not alphanumeric or underscore
        if (!"[a-zA-Z0-9_]$".test(resourceName))
        {
            return new ValidationResult
            {
                ValidatorName = "endWithAlphanumericOrUnderscore",
                Message = "Resource name must end with alphanumeric or underscore",
            };
        }

        return true;
    }

    /// <summary>
    /// Create a validator for max length validation.
    /// </summary>
    /// <param name="maxLength">Maximum length of the resource name.</param>
    /// <returns>A function that validates resource name against the max length.</returns>
    public static Validate maxLengthValidator(int maxLength)
    {
        return resourceName =>
        {
            if (resourceName.Length > maxLength)
            {
                return new ValidationResult
                {
                    ValidatorName = $"{maxLength}characterLimit",
                    Message = $"Resource name must be at most {maxLength} characters",
                };
            }

            return true;
        };
    }

    /// <summary>
    /// Create a validator for min length validation.
    /// </summary>
    /// <param name="minLength">Minimum length of the resource name.</param>
    /// <returns>A function that validates resource name against the min length.</returns>
    public static Validate minLengthValidator(int minLength)
    {
        return resourceName =>
        {
            if (resourceName.Length < minLength)
            {
                return new ValidationResult
                {
                    ValidatorName = $"atLeast{minLength}Characters",
                    Message = $"Resource name must be at least {minLength} characters",
                };
            }

            return true;
        };
    }

    private static readonly Dictionary<string, Validate> _validators = new()
    {
        { nameof(startWithAlphanumeric), startWithAlphanumeric },
        { nameof(startWithLetter), startWithLetter },
        { nameof(atLeast2Labels), atLeast2Labels },
        { nameof(endWithAlphanumeric), endWithAlphanumeric },
        { nameof(endWithAlphanumericOrUnderscore), endWithAlphanumericOrUnderscore },
        { "128characterLimit", maxLengthValidator(128) },
        { "15characterLimit", maxLengthValidator(15) },
        { "23characterLimit", maxLengthValidator(23) },
        { "24characterLimit", maxLengthValidator(24) },
        { "260characterLimit", maxLengthValidator(260) },
        { "44characterLimit", maxLengthValidator(44) },
        { "50characterLimit", maxLengthValidator(50) },
        { "59characterLimit", maxLengthValidator(59) },
        { "60characterLimit", maxLengthValidator(60) },
        { "62characterLimit", maxLengthValidator(62) },
        { "63characterLimit", maxLengthValidator(63) },
        { "64characterLimit", maxLengthValidator(64) },
        { "80characterLimit", maxLengthValidator(80) },
        { "atLeast2Characters", minLengthValidator(2) },
        { "atLeast3Characters", minLengthValidator(3) },
        { "atLeast4Characters", minLengthValidator(4) },
        { "atLeast5Characters", minLengthValidator(5) },
        { "atLeast6Characters", minLengthValidator(6) },
    };

    /// <summary>
    /// Validate resource name of the specific resource type.
    /// </summary>
    /// <param name="resourceName">The resource name after it has been transformed.</param>
    /// <param name="resourceType">The resource type whose naming rules we validate with.</param>
    /// <returns>Validation result if invalid, otherwise true.</returns>
    public static ValidationResult Validate(string resourceName, AzureResourceType resourceType)
    {
        var validations = resourceType.Validations;

        // for all validations on the resource type
        for (var i = 0; i < validations.Length; i += 1)
        {
            var validation = validations[i];
            // if validator exists
            if (_validators.ContainsKey(validation))
            {
                // find a validator
                var validatorFn = _validators[validation];
                // validate the resource name
                var validationResult = validatorFn(resourceName);
                // if invalid
                if (validationResult != ValidationResult.Success)
                {
                    return validationResult;
                }
            }
        }

        return true;
    }
}
