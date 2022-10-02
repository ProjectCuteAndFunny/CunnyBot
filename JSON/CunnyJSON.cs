using System.Text.Json.Serialization;

namespace CunnyBot.JSON;

public abstract class CunnyJson
{
    public class Root
    {
        public Root(List<string> tags, string ownerName, string imageUrl, string postUrl, string hash, int height, int width, int id)
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

        [JsonPropertyName("tags")]
        public List<string> Tags { get; }

        [JsonPropertyName("owner_name")]
        public string OwnerName { get; }

        [JsonPropertyName("image_url")]
        public string ImageUrl { get; }

        [JsonPropertyName("post_url")]
        public string PostUrl { get; }

        [JsonPropertyName("hash")]
        public string Hash { get; }

        [JsonPropertyName("height")]
        public int Height { get; }

        [JsonPropertyName("width")]
        public int Width { get; }

        [JsonPropertyName("id")]
        public int Id { get; }
    }
}