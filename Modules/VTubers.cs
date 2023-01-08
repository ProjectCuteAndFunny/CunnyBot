namespace CunnyBot.Modules;

public sealed partial class TopLevel
{
    /// <summary>
    ///  Returns images of VTubers.
    /// </summary>
    /// <param name="vtuber">VTuber to parse for</param>
    /// <param name="images">Amount of images to post</param>
    /// <param name="skip">Skip a page(e.g:1 will skip to the second page and start indexing from it)</param>
    /// <param name="site">Site to parse from</param>
    [RateLimit]
    [ApiCheck]
    [SlashCommand("vtubers", "Returns images of VTubers")]
    public async Task VTubers(
        [Choice("Gura", "gawr_gura")]
        [Choice("Aqua", "minato_aqua")]
        [Choice("Ayame", "nakiri_ayame")]
        [Choice("Kooyori", "hakui_koyori")]
        [Choice("Laplus", "laplus_darknesss")]
        [Choice("Rushia", "uruha_rushia")]
        [Choice("Sana", "natori_sana ")]
        [Choice("Shion", "murasaki_shion")]
        string vtuber,
        [MinValue(1)] [MaxValue(100)] int images, int skip = 0,
        [Choice("Danbooru", "danbooru")]
        [Choice("Gelbooru", "gelbooru")]
        [Choice("Konachan", "konachan")]
        [Choice("Safebooru", "safebooru")]
        [Choice("Yandere", "yandere")]
        string site = "safebooru")
        => await DeferAsync(true, GlobalTasks.Options)
            .ContinueWith(async _ => await GetImages(site, vtuber, images, skip));
}