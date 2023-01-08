using Microsoft.Extensions.Hosting;

namespace CunnyBot;

public class RateLimitAttribute : PreconditionAttribute
{
    public enum RateLimitType
    {
        Global,
        User,
        Guild
    }

    private RateLimitType Type { get; }
    private int Seconds { get; }
    private Dictionary<ulong, DateTime> RateLimits { get; }

    public RateLimitAttribute(int seconds = 5, RateLimitType type = RateLimitType.User)
    {
        Type = type;
        Seconds = seconds;
        RateLimits = new Dictionary<ulong, DateTime>();
    }

    public override Task<PreconditionResult> CheckRequirementsAsync(IInteractionContext context,
        ICommandInfo commandInfo,
        IServiceProvider services)
    {
        var now = DateTime.UtcNow;

        if (Type == RateLimitType.Global)
        {
            // Check if the command has been used within the specified time frame
            if (RateLimits.TryGetValue(0, out var value) && (now - value).TotalSeconds < Seconds)
            {
                var errorMessage = FormatRateLimitMessage(now, value, Seconds);
                return Task.FromResult(PreconditionResult.FromError(errorMessage));
            }

            // Update the rate limit information for the global rate limit
            RateLimits[0] = now;
        }
        else if (Type == RateLimitType.User)
        {
            var userId = context.User.Id;

            // Check if the user has used the command within the specified time frame
            if (RateLimits.TryGetValue(userId, out var value) && (now - value).TotalSeconds < Seconds)
            {
                var errorMessage = FormatRateLimitMessage(now, value, Seconds);
                return Task.FromResult(PreconditionResult.FromError(errorMessage));
            }

            // Update the rate limit information for the user
            RateLimits[userId] = now;
        }
        else if (Type == RateLimitType.Guild)
        {
            var guildId = context.Guild.Id;

            // Check if the guild has used the command within the specified time frame
            if (RateLimits.TryGetValue(guildId, out var value) && (now - value).TotalSeconds < Seconds)
            {
                var errorMessage = FormatRateLimitMessage(now, value, Seconds);
                return Task.FromResult(PreconditionResult.FromError(errorMessage));
            }

            // Update the rate limit information for the guild
            RateLimits[guildId] = now;
        }

        return Task.FromResult(PreconditionResult.FromSuccess());
    }

    private static string FormatRateLimitMessage(DateTime now, DateTime value, int seconds)
    {
        // Calculate the remaining time
        var remaining = seconds - (now - value).TotalSeconds;
        // Convert remaining time to unix timestamp
        var unixTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + (long) remaining;

        // Format the rate limit message with the remaining time
        var message = $"You are being rate limited. Try again <t:{unixTimestamp}:R>";
        return message;
    }

    public void CleanRateLimits()
    {
        // Create a list of keys to remove
        var keysToRemove = (from entry in RateLimits
            where (DateTime.UtcNow - entry.Value).TotalSeconds >= Seconds
            select entry.Key).ToList();

        // Remove the keys from the dictionary
        keysToRemove.ForEach(key => RateLimits.Remove(key));
    }
}

public class RateLimitService : IHostedService
{
    private readonly RateLimitAttribute _rateLimitAttribute;
    private Timer _timer;

    public RateLimitService(RateLimitAttribute rateLimitAttribute, Timer timer)
    {
        _rateLimitAttribute = rateLimitAttribute;
        _timer = timer;
    }

    // Implement the StartAsync method of the IHostedService interface
    public Task StartAsync(CancellationToken cancellationToken) =>
        // Start a new timer with an interval of 30 minutes
        // Call the OnTimerElapsed method when the timer elapses
        // Start the timer immediately
        Task.Run(() => _timer = new Timer(OnTimerElapsed, null, TimeSpan.Zero, TimeSpan.FromMinutes(30)),
            cancellationToken);

    // The event handler for the timer's Elapsed event
    private void OnTimerElapsed(object? state) =>
        // Call the CleanRateLimits method of the RateLimit object
        _rateLimitAttribute.CleanRateLimits();

    // Implement the StopAsync method of the IHostedService interface
    public Task StopAsync(CancellationToken cancellationToken) =>
        // Stop the timer
        Task.Run(() => _timer.Change(Timeout.Infinite, 0), cancellationToken);
}