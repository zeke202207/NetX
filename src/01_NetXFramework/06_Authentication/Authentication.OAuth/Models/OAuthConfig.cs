using Microsoft.Extensions.Configuration;

namespace Authentication.OAuth
{
    public class OAuthConfig
    {
        /// <summary>
        /// AppId
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// Secret key
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        /// 回调地址
        /// </summary>
        public string RedirectUri { get; set; }

        /// <summary>
        /// 权限范围
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// 从 IConfiguration 中读取
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static OAuthConfig Load(IConfiguration configuration, string prefix)
        {
            var appId = configuration[prefix + ":app_id"];
            var appKey = configuration[prefix + ":app_key"];
            var redirectUri = configuration[prefix + ":redirect_uri"];
            var scope = configuration[prefix + ":scope"];
            return new OAuthConfig()
            {
                AppId = appId,
                AppKey = appKey,
                RedirectUri = redirectUri,
                Scope = scope
            };
        }
    }
}
