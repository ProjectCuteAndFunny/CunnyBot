namespace CunnyBot.Modules;

public sealed partial class TopLevel
{
    /// <summary>
    /// Gets anime images based on tags from the CunnyAPI
    /// </summary>
    /// <param name="tags">Tags to parse for</param>
    /// <param name="images">Amount of images to post</param>
    /// <param name="skip">Skip a page(e.g:1 will skip to the second page and start indexing from it)</param>
    /// <param name="site">Site to parse from</param>
    [RateLimit]
    [ApiCheck]
    [SlashCommand("cunny", "Returns the anime images")]
    public async Task Cunny(
        [Summary(description: "You can combine multiple tags like this: 1girl blonde_hair blue_eyes")] string tags,
        [MinValue(1)] [MaxValue(100)] int images, int skip = 0,
        [Choice("Danbooru", "danbooru")]
        [Choice("Gelbooru", "gelbooru")]
        [Choice("Konachan", "konachan")]
        [Choice("Safebooru", "safebooru")]
        [Choice("Yandere", "yandere")]
        string site = "safebooru")
        => await DeferAsync(true, GlobalTasks.Options)
            .ContinueWith(async _ => await GetImages(site, tags, images, skip));
}