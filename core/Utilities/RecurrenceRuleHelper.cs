using Ical.Net.DataTypes;

namespace core.Utilities;

public static class RecurrenceRuleHelper
{
    /// <summary>
    /// Validates if an RRule string is properly formatted according to RFC 5545
    /// </summary>
    public static bool IsValidRRule(string rRuleString)
    {
        if (string.IsNullOrWhiteSpace(rRuleString))
            return false;

        try
        {
            _ = new RecurrencePattern(rRuleString);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Generates a simple RRule string for common patterns
    /// Example: GenerateRRule("DAILY", interval: 1) => "FREQ=DAILY"
    /// </summary>
    public static string GenerateRRule(
        string frequency,
        int? interval = null,
        DateTime? until = null,
        int? count = null)
    {
        var parts = new List<string> { $"FREQ={frequency.ToUpper()}" };

        if (interval.HasValue && interval > 1)
            parts.Add($"INTERVAL={interval}");

        if (until.HasValue)
            parts.Add($"UNTIL={until:yyyyMMddTHHmmssZ}");

        if (count.HasValue)
            parts.Add($"COUNT={count}");

        return string.Join(";", parts);
    }

    /// <summary>
    /// Parses an RRule pattern and extracts basic frequency information
    /// </summary>
    public static string? GetFrequency(string rRuleString)
    {
        try
        {
            var pattern = new RecurrencePattern(rRuleString);
            return pattern.Frequency.ToString();
        }
        catch
        {
            return null;
        }
    }
}
