using Discord;

namespace CunnyBot.Loggers;

public abstract class Logger : ILogger
{
    public abstract Task Log(LogMessage message);
}