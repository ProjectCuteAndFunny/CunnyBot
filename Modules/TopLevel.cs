using System.Net;
using System.Net.Http.Json;
using System.Text;
using CunnyBot.JSON;
using static CunnyBot.GlobalTasks;

namespace CunnyBot.Modules;

public sealed partial class TopLevel : InteractionModuleBase<SocketInteractionContext>
{
    private async Task GetImages(string site, string tags, int images, int skip)
    {
        if (site.Contains("danbooru") && tags.Split(' ').Length > 2)
        {
            await FollowupAsync($"You can only use 2 tags for **{site}**.",
                ephemeral: true,
                options: Options);
            return;
        }

        var request =
            await GlobalTasks.HttpClient.GetAsync(
                $"{Environment.GetEnvironmentVariable("CUNNY_API_URL")}/api/v1/{site}/{tags}/{images};{skip}");

        if (request.StatusCode is not HttpStatusCode.OK)
            await FollowupAsync("Invalid tags.", ephemeral: true, options: Options);

        var response = await request.Content.ReadFromJsonAsync<List<CunnyJson>>();

        if (response is null)
        {
            await FollowupAsync("An error occured", ephemeral: true, options: Options);
            return;
        }

        StringBuilder imageTags = new();
        foreach (var item in response.Where(item => !string.IsNullOrEmpty(item.ImageUrl)))
        {
            imageTags.AppendJoin(", ", item.Tags);
            await FollowupAsync(ephemeral: true, options: Options,
                embed: new EmbedBuilder()
                    .WithColor((uint)Random.Shared.Next(0, 16777215))
                    .WithDescription($"{item.Width}x{item.Height}")
                    .WithFooter(imageTags.Length > 2048 ? $"{imageTags.ToString()[..2045]}..." : imageTags.ToString())
                    .WithImageUrl(item.ImageUrl)
                    .WithTitle(item.Id.ToString())
                    .WithUrl(item.PostUrl)
                    .WithAuthor(item.OwnerName)
                    .Build());
        }
    }
}