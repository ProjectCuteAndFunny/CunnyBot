using System.Net;
using System.Net.Http.Json;
using CunnyBot.JSON;

namespace CunnyBot.Modules;

public sealed partial class TopLevel : InteractionModuleBase<SocketInteractionContext>
{
    private HttpClient HttpClient { get; } = new();
    private RequestOptions Options { get; } = new()
    {
        Timeout = 3000,
        RetryMode = RetryMode.AlwaysRetry,
        UseSystemClock = false
    };

    private async Task GetImages(string site, string tags, int images)
    {
        await DeferAsync(ephemeral: true, options: Options);

        if (site.Contains("danbooru") && tags.Split(' ').Length > 2)
        {
            await FollowupAsync($"You can only use 2 tags for **{site}**.",
                ephemeral: true,
                options: Options);
            return;
        }

        var request =
            await HttpClient.GetAsync($"{Environment.GetEnvironmentVariable("CUNNY_API_URL")}/{site}/{tags}/{images}");

        if (request.StatusCode is HttpStatusCode.NotFound)
        {
            await FollowupAsync($"**API IS DOWN** - 404",
                ephemeral: true,
                options: Options);
            return;
        }

        if (request.StatusCode is not HttpStatusCode.Accepted)
        {
            await FollowupAsync("An error occured", ephemeral: true, options: Options);
            return;
        }

        var response = await request.Content.ReadFromJsonAsync<List<CunnyJson.Root>>();

        foreach (var item in response!)
            await FollowupAsync(ephemeral: true,
                options: Options,
                embed: new EmbedBuilder()
                    .WithColor((uint)new Random().Next(0, 16777215))
                    .WithFooter($"{item.Width}x{item.Height}")
                    .WithImageUrl(item.ImageUrl)
                    .WithTitle(item.Id.ToString())
                    .WithUrl(item.PostUrl)
                    .Build());
    }
}