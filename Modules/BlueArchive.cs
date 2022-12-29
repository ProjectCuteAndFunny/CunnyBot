namespace CunnyBot.Modules;

public sealed partial class TopLevel
{
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
        [Choice("Arisu", "aris_(blue_archive)")]
        [Choice("Atsuko", "atsuko_(blue_archive)")]
        [Choice("Azusa", "azusa_(blue_archive)")]
        [Choice("Fuuka", "fuuka_(blue_archive)")]
        [Choice("Hifumi", "hifumi_(blue_archive)")]
        [Choice("Hoshino", "hoshino_(blue_archive)")]
        [Choice("Iroha", "iroha_(blue_archive)")]
        [Choice("Junko", "junko_(blue_archive)")]
        [Choice("Kayoko", "kayoko_(blue_archive)")]
        [Choice("Koharu", "koharu_(blue_archive)")]
        [Choice("Mari", "mari_(blue_archive)")]
        [Choice("Midori", "midori_(blue_archive)")]
        [Choice("Momoi", "momoi_(blue_archive)")]
        [Choice("Mutsuki", "mutsuki_(blue_archive)")]
        [Choice("Reisa", "reisa_(blue_archive)  ")]
        [Choice("Serika", "serika_(blue_archive)")]
        [Choice("Serina", "serina_(blue_archive)")]
        [Choice("Sora", "sora_(blue_archive)")]
        [Choice("Yuuka", "yuuka_(blue_archive)")]
        [Choice("Yuzu", "yuzu_(blue_archive)")] string character = "fuuka_(blue_archive)")
        => await GetImages(site, character, images);
}