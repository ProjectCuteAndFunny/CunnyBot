using Newtonsoft.Json;

namespace CunnyBot.JSON;

public class CunnyJson
{
    [JsonProperty("tags")]
    public List<string> Tags;

    [JsonProperty("owner_name")]
    public string OwnerName;

    [JsonProperty("image_url")]
    public string ImageUrl;

    [JsonProperty("post_url")]
    public string PostUrl;

    [JsonProperty("hash")]
    public string Hash;

    [JsonProperty("height")]
    public int Height;

    [JsonProperty("width")]
    public int Width;

    [JsonProperty("id")]
    public int Id;

    public CunnyJson(List<string> tags, string ownerName, string imageUrl, string postUrl, string hash, int height, int width, int id)
    {
        Tags = tags;
        OwnerName = ownerName;
        ImageUrl = imageUrl;
        PostUrl = postUrl;
        Hash = hash;
        Height = height;
        Width = width;
        Id = id;
    }
}