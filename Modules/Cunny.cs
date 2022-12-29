namespace CunnyBot.Modules;

public sealed partial class TopLevel
{
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
        [Summary(description: "You can combine multiple tags like this: 1girl, blue_eyes blonde_hair")] string tags,
        [MinValue(1)] [MaxValue(100)] int images) => await GetImages(site, tags, images);
}