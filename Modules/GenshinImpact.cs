namespace CunnyBot.Modules;

public sealed partial class TopLevel
{
    /// <summary>
    /// Returns images of Genshin Impact Characters
    /// </summary>
    /// <param name="character">Character to parse for</param>
    /// <param name="images">Amount of images to post</param>
    /// <param name="skip">Skip a page(e.g:1 will skip to the second page and start indexing from it)</param>
    /// <param name="site">Site to parse from</param>
    [RateLimit]
    [ApiCheck]
    [SlashCommand("genshin-impact", "Returns images of Genshin Impact")]
    public async Task GenshinImpact(
        [Choice("Amber", "amber_(genshin_impact)")]
        [Choice("Barbara", "barbara_(genshin_impact)")]
        [Choice("Collei", "collei_(genshin_impact)")]
        [Choice("Hu Tao", "hu_tao_(genshin_impact)")]
        [Choice("Keqing", "keqing_(genshin_impact)")]
        [Choice("Klee", "klee_(genshin_impact)")]
        [Choice("Layla", "layla_(genshin_impact)")]
        [Choice("Mona", "mona_(genshin_impact)")]
        [Choice("Nahida", "nahida_(genshin_impact)")]
        [Choice("Nilou", "nilou_(genshin_impact)")]
        [Choice("Qiqi", "qiqi_(genshin_impact)")]
        [Choice("Sayu", "sayu_(genshin_impact)")]
        [Choice("Sucrose", "sucrose_(genshin_impact)")]
        [Choice("Yun Jin", "yun_jin_(genshin_impact)")]
        string character,
        [MinValue(1)] [MaxValue(100)] int images, int skip = 0,
        [Choice("Danbooru", "danbooru")]
        [Choice("Gelbooru", "gelbooru")]
        [Choice("Konachan", "konachan")]
        [Choice("Safebooru", "safebooru")]
        [Choice("Yandere", "yandere")]
        string site = "safebooru")
        => await DeferAsync(true, GlobalTasks.Options)
            .ContinueWith(async _ => await GetImages(site, character, images, skip));
}