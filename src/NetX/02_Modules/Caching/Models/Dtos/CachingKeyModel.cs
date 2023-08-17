namespace NetX.Caching.Models.Dtos
{
    public class CachingKeyModel
    {
        public string CacheType { get; set; }
        public string CachingPrefixKey { get; set; }

        public List<CachingKey> Keys { get; set; } = new List<CachingKey>();
    }

    public class CachingKey
    {
        public string Key { get; set; }
    }
}
