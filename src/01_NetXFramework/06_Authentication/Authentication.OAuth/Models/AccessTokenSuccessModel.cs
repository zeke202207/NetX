using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authentication.OAuth
{
    /// <summary>
    /// 参考 https://tools.ietf.org/html/rfc6749#section-5.1 提供默认属性
    /// </summary>
    public abstract class AccessTokenSuccessModel
    {
        /// <summary>
        /// Token 类型
        /// </summary>
        [JsonProperty("token_type")]
        public virtual string TokenType { get; set; }

        /// <summary>
        /// AccessToken
        /// </summary>
        [JsonProperty("access_token")]
        public virtual string AccessToken { get; set; }

        /// <summary>
        /// 用于刷新 AccessToken 的 Token
        /// </summary>
        [JsonProperty("refresh_token")]
        public virtual string RefreshToken { get; set; }

        /// <summary>
        /// 此 AccessToken 对应的权限
        /// </summary>
        [JsonProperty("scope")]
        public virtual string Scope { get; set; }

        /// <summary>
        /// AccessToken 过期时间
        /// </summary>
        [JsonProperty("expires_in")]
        public virtual dynamic ExpiresIn { get; set; }
    }
}