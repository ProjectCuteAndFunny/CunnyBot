namespace CunnyBot;

public static class GlobalTasks
{
    public static readonly HttpClient HttpClient = new();

    public static RequestOptions Options { get; } = new()
    {
        Timeout = 3000,
        RetryMode = RetryMode.AlwaysRetry,
        UseSystemClock = false
    };
}