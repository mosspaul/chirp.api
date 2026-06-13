public static class DateUtility
{
    private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    public static DateTime ConvertTimestampToDate(int unixTimestamp)
    {
        var date = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).UtcDateTime;
        return date;
    }

    public static long ConvertDateToTimestamp(DateTime date)
    {
        TimeSpan elapsedTime = date - Epoch;
        return (long) elapsedTime.TotalSeconds;
    }
}