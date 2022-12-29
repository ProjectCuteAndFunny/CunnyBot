namespace CunnyBot.Modules;

public sealed partial class TopLevel
{
    /// <summary>
    ///  Returns images of VTubers.
    /// </summary>
    /// <param name="site">Site to parse from</param>
    /// <param name="vtuber">VTuber to parse for</param>
    /// <param name="images">Amount of images to post</param>
    [SlashCommand("vtubers", "Returns images of VTubers")]
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
}