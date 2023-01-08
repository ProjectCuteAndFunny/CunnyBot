using System.Net;

namespace CunnyBot;

public class ApiCheckAttribute : PreconditionAttribute
{
    public override string ErrorMessage => "**CunnyAPI** is down";

    public override Task<PreconditionResult> CheckRequirementsAsync(IInteractionContext context,
        ICommandInfo commandInfo, IServiceProvider services)
    {
        return Task.FromResult(
            GlobalTasks.HttpClient.GetAsync($"{Environment.GetEnvironmentVariable("CUNNY_API_URL")}/api/version").Result.StatusCode
                is HttpStatusCode.OK
                ? PreconditionResult.FromSuccess()
                : PreconditionResult.FromError(ErrorMessage));
    }
}