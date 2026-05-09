public class DateUtility
{
    private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    public DateTime ConvertTimestampToDate(int unixTimestamp)
    {
        var date = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).LocalDateTime;
        return date;
    }

    public long ConvertDateToTimestamp(DateTime date)
    {
        TimeSpan elapsedTime = date - Epoch;
        return (long) elapsedTime.TotalSeconds;
    }
}