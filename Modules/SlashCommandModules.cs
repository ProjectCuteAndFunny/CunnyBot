using System.Net.Http.Json;
using CunnyBot.JSON;
using Discord;
using Discord.Interactions;

namespace CunnyBot.Modules;

public class NormalCommandModule : InteractionModuleBase<SocketInteractionContext>
{
	private HttpClient HttpClient { get; } = new();
	
	private RequestOptions Options { get; } = new()
	{
		Timeout = 3000, 
		RetryMode = RetryMode.RetryRatelimit | RetryMode.Retry502 | RetryMode.RetryTimeouts,
		UseSystemClock = false
	};
	
	/// <summary>
	/// Gets anime images based on tags from the CunnyAPI
	/// </summary>
	/// <param name="site">Site to parse from</param>
	/// <param name="tags">Tags to parse for</param>
	/// <param name="images">Amount of images to post</param>
	[SlashCommand("cunny", "Returns the anime images")]
	public async Task Cunny(
		[Choice("Danbooru", "danbooru")]
		[Choice("Gelbooru", "gelbooru")]
		[Choice("Konachan", "konachan")]
		[Choice("Safebooru", "safebooru")]
		[Choice("Yandere", "yandere")] string site,
		[Summary(description: "You can combine multiple tags like this: blue_eyes skirt")] string tags,
		[MinValue(1)] [MaxValue(100)] int images) => await GetImages(site, tags, images);

	/// <summary>
	/// Gets images for an anime gacha game "Blue Archive" from a selected site.
	/// </summary>
	/// <param name="site">Site to parse from</param>
	/// <param name="character">Character to parse for</param>
	/// <param name="images">Amount of images to post</param>
	[SlashCommand("blue-archive", "Returns images of blue archive")]
	public async Task Blue(
		
		[Choice("Danbooru", "danbooru")]
		[Choice("Gelbooru", "gelbooru")]
		[Choice("Konachan", "konachan")]
		[Choice("Konachan", "konachan")]
		[Choice("Safebooru", "safebooru")]
		[Choice("Yandere", "yandere")] string site,
		[MinValue(1)] [MaxValue(100)] int images,
		[Choice("Alice", "arisu_(blue_archive)")]
		[Choice("Aru", "aru_(blue_archive)")]
		[Choice("Hanae", "hanae_(blue_archive)")]
		[Choice("Hanko", "yuzu_(blue_archive)")]
		[Choice("Hifumi", "hifumi_(blue_archive)")]
		[Choice("Hibiki", "hibiki_(blue_archive)")]
		[Choice("Hina", "hina_(blue_archive)")]
		[Choice("Hoshino", "hoshino_(blue_archive)")]
		[Choice("Iroha", "iroha_(blue_archive)")]
		[Choice("Junko", "junko_(blue_archive)")]
		[Choice("Koharu", "koharu_(blue_archive)")]
		[Choice("Kokona", "kokona_(blue_archive)")]
		[Choice("Midori", "midori_(blue_archive)")]
		[Choice("Miyu", "miyu_(blue_archive)")]
		[Choice("Momoi", "momoi_(blue_archive)")]
		[Choice("Mutsuki", "mutsuki_(blue_archive)")]
		[Choice("Natsu", "natsu_(blue_archive)")]
		[Choice("Neru", "neru_(blue_archive)")]
		[Choice("Nonomi", "nonomi_(blue_archive)")]
		[Choice("Serika", "serika_(blue_archive)")] string character = "hibiki_(blue_archive)") => await GetImages(site, character, images);

	/// <summary>
	///  Returns images of VTubers.
	/// </summary>
	/// <param name="site">Site to parse from</param>
	/// <param name="vtuber">VTuber to parse for</param>
	/// <param name="images">Amount of images to post</param>
	[SlashCommand("vtuber", "Returns images of VTubers")]
	public async Task VTubers(
		[Choice("Danbooru", "danbooru")]
		[Choice("Gelbooru", "gelbooru")]
		[Choice("Konachan", "konachan")]
		[Choice("Safebooru", "safebooru")]
		[Choice("Yandere", "yandere")] string site,
		[Choice("Gura", "gawr_gura")]
		[Choice("Aqua", "minato_aqua")]
		[Choice("Ayame", "nakiri_ayame")]
		[Choice("Kooyori", "hakui_koyori")]
		[Choice("Laplus", "laplus_darknesss")]
		[Choice("Rushia", "uruha_rushia")] 
		[Choice("Shion", "murasaki_shion")] string vtuber,
		[MinValue(1)] [MaxValue(100)] int images) => await GetImages(site, vtuber, images);

	/// <summary>
	/// Returns images of Genshin Impact Characters
	/// </summary>
	/// <param name="site">Site to parse from</param>
	/// <param name="character">Character to parse for</param>
	/// <param name="images">Amount of images to post</param>
	[SlashCommand("genshin-impact", "Returns images of Genshin Impact")]
	public async Task GenshinImpact(
		[Choice("Danbooru", "danbooru")]
		[Choice("Gelbooru", "gelbooru")]
		[Choice("Konachan", "konachan")]
		[Choice("Safebooru", "safebooru")]
		[Choice("Yandere", "yandere")]
		string site,
		[Choice("Diona", "diona_(genshin_impact)")]
		[Choice("Dori", "dori_(genshin_impact)")]
		[Choice("Klee", "klee_(genshin_impact)")]
		[Choice("Nahida", "nahida_(genshin_impact)")]
		[Choice("Qiqi", "qiqi_(genshin_impact)")]
		[Choice("Sayu", "sayu_(genshin_impact)")]
		string character,
		[MinValue(1)] [MaxValue(100)] int images) => await GetImages(site, character, images);

	private async Task GetImages(string site, string tags, int images)
	{
		await DeferAsync(ephemeral: true, options: Options);

		if (site.Contains("danbooru") && tags.Split(' ').Length > 2)
		{
			await FollowupAsync($"You can only have 2 tags for **{site}**.\n*Please try again with 2 tags*",
				options: Options,
				ephemeral: true);
			return;
		}

		List<CunnyJson.Root>? response;
		try
		{
			response = await HttpClient.GetFromJsonAsync<List<CunnyJson.Root>>($"{Environment.GetEnvironmentVariable("CUNNY_API_URL")}{site}/{tags}/{images}");
		}
		catch (HttpRequestException e)
		{
			if (e.Message.Contains("404")) await FollowupAsync("Invalid tags", options: Options, ephemeral: true);
			else
				await FollowupAsync(
					$"Couldn't post the {(images == 1 ? "image" : "images")}.\n" +
					$"This is either because **{site}** is down, the **CunnyAPI** is down or you have entered invalid tags." +
					"Please try again later*",
					options: Options,
					ephemeral: true);
			return;
		}
		catch (Exception)
		{
			await FollowupAsync($"Could not fetch images from {site}", options: Options, ephemeral: true);
			return;
		}

		foreach (var item in response!)
			await FollowupAsync(embed: new EmbedBuilder()
					.WithColor((uint)new Random().Next(0, 16777215))
					.WithFooter($"{item.Width}x{item.Height}")
					.WithImageUrl(item.ImageUrl)
					.WithTitle(item.Id.ToString())
					.WithUrl(item.PostUrl)
					.Build(),
				options: Options,
				ephemeral: true);
	}
}
