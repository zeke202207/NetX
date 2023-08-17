using Newtonsoft.Json;

namespace Authentication.OAuth
{
    public abstract class UserInfoModelBase
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("avatar_url")]
        public string Avatar { get; set; }
    }
}
