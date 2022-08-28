using CunnyBot.JSON;
using Discord;
using Discord.Interactions;
using Newtonsoft.Json;

namespace CunnyBot.Modules;

// ReSharper disable once UnusedType.Global
public class SlashCommandModules : InteractionModuleBase<SocketInteractionContext>
{
	private HttpClient Client { get; } = new();
	private EmbedBuilder Embed { get; } = new();
	private readonly RequestOptions _options = new()
	{
		Timeout = 3000,
		RetryMode = RetryMode.AlwaysRetry,
		UseSystemClock = false,
	};

	/// <summary>
	/// Allows the owner of the bot to, shut down the bot.
	/// </summary>
	[SlashCommand("shutdown", "Shutdowns the bot")]
	[RequireOwner]
	// ReSharper disable once UnusedMember.Global
	public async Task Shutdown()
	{
		await DeferAsync(ephemeral: true);
		await FollowupAsync("Shutting down...", ephemeral: true);
		Environment.Exit(0);
	}
	
	/// <summary>
	/// Gets anime images based on tags from the CunnyAPI
	/// </summary>
	/// <param name="site">Site to parse from</param>
	/// <param name="tags">Tags to parse for</param>
	/// <param name="images">Amount of images to post</param>
	[SlashCommand("cunny", "Returns the anime images")]
	// ReSharper disable once UnusedMember.Global
	public async Task Cunny(
		[Choice("Gelbooru", "gelbooru")]
		[Choice("Safebooru", "safebooru")]
		[Choice("Danbooru", "danbooru")]
		[Choice("Konachan", "konachan")]
		[Choice("Yandere", "yandere")]
		string site, string tags,
		[MinValue(1)] [MaxValue(100)]
		int images)
	{
		await DeferAsync(ephemeral: true, options: _options);
		var url = $"https://cunnyapi.breadwas.uber.space/api/v1/{site}/{tags}/{images}";
		var response = await Client.GetStringAsync(url);
		var jsonResponse = JsonConvert.DeserializeObject<List<CunnyJson>>(response);
		foreach (var item in jsonResponse!)
			await FollowupAsync(embed: Embed.WithAuthor($"Poster: {item.OwnerName}")
				.WithColor((uint)new Random().Next(0, 16777215))
				.WithDescription($"W: {item.Width}H: {item.Height}")
				.WithImageUrl(item.ImageUrl)
				.WithTitle(item.Id.ToString())
				.WithUrl(item.PostUrl)
				.Build(),
				options: _options,
				ephemeral: true);
	}
	
	/// <summary>
	/// Gets images for an anime gacha game "Blue Archive" from a selected site.
	/// </summary>
	/// <param name="site">Site to parse from</param>
	/// <param name="character">Character to parse for</param>
	/// <param name="images">Amount of images to post</param>
	[SlashCommand("blue", "Returns images of blue archive")]
	// ReSharper disable once UnusedMember.Global
	public async Task Blue(
		[Choice($"Gelbooru", "gelbooru")]
		[Choice("Safebooru", "safebooru")]
		[Choice("Danbooru", "danbooru")]
		[Choice("Konachan", "konachan")]
		[Choice("Yandere", "yandere")] string site,
		[Choice("Alice", "alice_(blue_archive)")] 
		[Choice("Aru", "aru_(blue_archive)")]
		[Choice("Hanae", "hanae_(blue_archive)")]
		[Choice("Hanko", "hanko_(blue_archive)")]
		[Choice("Hifumi", "hifumi_(blue_archive)")]
		[Choice("Hina", "hina_(blue_archive)")]
		[Choice("Hoshino", "hoshino_(blue_archive)")]
		[Choice("Iroha", "iroha_(blue_archive)")]
		[Choice("Junko", "kunko_(blue_archive)")]
		[Choice("Koharu", "koharu_(blue_archive)")]
		[Choice("Kotori", "kotori_(blue_archive)")]
		[Choice("Mari", "mari_(blue_archive)")]
		[Choice("Midori", "midori_(blue_archive)")]
		[Choice("Miyu", "miyu_(blue_archive)")]
		[Choice("Momoi", "momoi_(blue_archive)")]
		[Choice("Mutsuki", "mutsuki_(blue_archive)")]
		[Choice("Natsu", "natsu_(blue_archive)")]
		[Choice("Neru", "neru_(blue_archive)")]
		[Choice("Nonomi", "nonomi_(blue_archive)")]
		[Choice("Pina", "pina_(blue_archive)")]
		[Choice("Serika", "serika_(blue_archive)")] string character, 
		[MinValue(1)] [MaxValue(100)]
		int images
		) 
	{
		await DeferAsync(ephemeral: true, options: _options);
		var url = $"https://cunnyapi.breadwas.uber.space/api/v1/{site}/{character}/{images}";
		var response = await Client.GetStringAsync(url);
		var jsonResponse = JsonConvert.DeserializeObject<List<CunnyJson>>(response);
		foreach (var item in jsonResponse!)
			await FollowupAsync(embed: Embed.WithAuthor($"Poster: {item.OwnerName}")
				.WithColor((uint)new Random().Next(0, 16777215))
				.WithDescription($"W: {item.Width}H: {item.Height}")
				.WithImageUrl(item.ImageUrl)
				.WithTitle(item.Id.ToString())
				.WithUrl(item.PostUrl)
				.Build(),
				options: _options,
				ephemeral: true); 
	}
}
