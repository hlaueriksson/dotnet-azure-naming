public static class Transformer
{
    /// <summary>
    /// Allow alphanumerics.
    /// </summary>
    /// <param name="str">The string to transform.</param>
    /// <returns>A string with all non-alphanumeric characters removed.</returns>
    private static string alphanumerics(string str)
    {
        return str.replace("[^a-zA-Z0-9]", "");
    }

    /// <summary>
    /// Two dashes in row is one too much.
    /// </summary>
    /// <param name="str">The string to transform.</param>
    /// <returns>A string without two dashes in a row.</returns>
    private static string removeDuplicateHyphens(string str)
    {
        return str.replace("-{2,}", "-");
    }

    /// <summary>
    /// Allow alphanumerics and hyphens.
    /// </summary>
    /// <param name="str">The string to transform.</param>
    /// <returns>A string with all non-alphanumeric characters removed.</returns>
    private static string alphanumericsHyphens(string str)
    {
        return str.replace("[^a-zA-Z0-9-]", "");
    }

    /// <summary>
    /// Allow alphanumerics, hyphens, and underscores.
    /// </summary>
    /// <param name="str">The string to transform.</param>
    /// <returns>A string with all non-alphanumeric characters removed.</returns>
    private static string alphanumericsUnderscoresHyphens(string str)
    {
        return str.replace("[^a-zA-Z0-9_-]", "");
    }

    /// <summary>
    /// Allow alphanumerics and underscores.
    /// </summary>
    /// <param name="str">The string to transform.</param>
    /// <returns>A string with all non-alphanumeric characters removed.</returns>
    private static string alphanumericsUnderscores(string str)
    {
        return str.replace("[^a-zA-Z0-9_]", "");
    }

    /// <summary>
    /// Allow alphanumerics, hyphens, peiods and underscores.
    /// </summary>
    /// <param name="str">The string to transform.</param>
    /// <returns>A string with all non-alphanumeric characters removed.</returns>
    private static string alphanumericsUnderscoresPeriodsHyphens(string str)
    {
        return str.replace("[^a-zA-Z0-9_.-]", "");
    }

    /// <summary>
    /// Transform into labels with alphanumerics, underscores and hyphens.
    /// </summary>
    /// <param name="str">The string to transform.</param>
    /// <returns>A string of labels, with all non-alphanumeric characters removed.</returns>
    private static string labelsAlphanumericsUnderscoresHyphens(string str)
    {
        return alphanumericsUnderscoresHyphens(str).replace("-", ".");
    }

    private static readonly Dictionary<string, Func<string, string>> _transformers = new()
    {
        { nameof(alphanumerics), alphanumerics },
        { nameof(alphanumericsHyphens), alphanumericsHyphens },
        { nameof(alphanumericsUnderscores), alphanumericsUnderscores },
        { nameof(alphanumericsUnderscoresHyphens), alphanumericsUnderscoresHyphens },
        { nameof(alphanumericsUnderscoresPeriodsHyphens), alphanumericsUnderscoresPeriodsHyphens },
        { nameof(labelsAlphanumericsUnderscoresHyphens), labelsAlphanumericsUnderscoresHyphens },
    };

    /// <summary>
    /// Apply transformer
    /// </summary>
    /// <param name="str">The string to transform</param>
    /// <param name="identifier">Name of the transformer</param>
    /// <returns>The transformed string.</returns>
    /// <exception cref="Exception">Transformer not found.</exception>
    public static string Transform(string str, string identifier = null)
    {
        // lowercase the str
        // replace all whitespace with a dash
        var input = str.Trim().ToLower().replace(@"\s+", "-");

        // replace all special characters
        input = CharacterReplacement.ReplaceAll(input);

        // no transformer return it as it is
        if (identifier == null)
        {
            return input;
        }

        if (_transformers.ContainsKey(identifier))
        {
            // find the transformer
            return removeDuplicateHyphens(_transformers[identifier](input));
        }

        throw new Exception($"Transformer {identifier} not found");
    }
}
