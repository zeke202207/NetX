using Flurl;
using Flurl.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text.Encodings.Web;
using static System.Net.WebRequestMethods;

namespace Authentication.OAuth
{
    /// <summary>
    /// OAuth基类
    /// </summary>
    /// <typeparam name="TAccessTokenModel"></typeparam>
    /// <typeparam name="TUserInfoModel"></typeparam>
    public abstract class OAuthLoginBase<TAccessTokenModel, TUserInfoModel>
        where TAccessTokenModel : AccessTokenSuccessModel
        where TUserInfoModel : UserInfoModelBase
    {
        protected readonly OAuthConfig oAuthConfig;
        protected string DEFAULT_USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 Edg/87.0.664.66";

        public OAuthLoginBase(OAuthConfig oAuthConfig)
        {
            this.oAuthConfig = oAuthConfig;
        }

        /// <summary>
        /// 授权 URL
        /// </summary>
        protected abstract string AuthorizeUrl { get; }

        /// <summary>
        /// 访问token URL
        /// </summary>
        protected abstract string AccessTokenUrl { get; }

        /// <summary>
        /// 用户信息 URL
        /// </summary>
        protected abstract string UserInfoUrl { get; }

        protected virtual Dictionary<string, string> BuildAuthorizeParams(string state)
        {
            return new Dictionary<string, string>()
            {
                ["response_type"] = "code",
                ["client_id"] = $"{oAuthConfig.AppId}",
                ["redirect_uri"] = $"{oAuthConfig.RedirectUri}",
                ["scope"] = $"{oAuthConfig.Scope}",
                ["state"] = $"{state}"
            };
        }

        protected virtual Dictionary<string,string> BuildAccessTokenParams(Dictionary<string,string> authorizeCallbackParams)
        {
            return new Dictionary<string, string>()
            {
                ["grant_type"] = "authorization_code",
                ["code"] = $"{authorizeCallbackParams["code"]}",
                ["client_id"] = $"{oAuthConfig.AppId}",
                ["client_secret"] = $"{oAuthConfig.AppKey}",
                ["redirect_uri"] = $"{oAuthConfig.RedirectUri}"
            };
        }

        /// <summary>
        /// 构造获取用户信息参数
        /// </summary>
        /// <param name="accessTokenModel"></param>
        /// <returns></returns>
        protected virtual Dictionary<string, string> BuildUserInfoParams(TAccessTokenModel accessTokenModel)
        {
            return new Dictionary<string, string>()
            {
                ["access_token"] = accessTokenModel.AccessToken
            };
        }

        /// <summary>
        /// 获取访问token
        /// 
        /// flurl <see cref="https://flurl.dev/"/>
        /// <code>
        /// var person = await "https://api.com"
        /// .AppendPathSegment("person")
        /// .SetQueryParams(new { a = 1, b = 2 })
        /// .WithOAuthBearerToken("my_oauth_token")
        /// .PostJsonAsync(new
        /// {
        /// first_name = "Claire",
        /// last_name = "Underwood"
        /// })
        /// .ReceiveJson<Person>();
        /// </code>
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<TAccessTokenModel> GetAccessTokenAsync(Dictionary<string, string> queryParam)
        {
            var accessToken = await AccessTokenUrl
                    .WithHeaders(new { Accept = "application/json", User_Agent = DEFAULT_USER_AGENT })
                    .SetQueryParams(queryParam)
                    .PostAsync()
                    .ReceiveJson<TAccessTokenModel>();
            return accessToken;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        protected virtual async Task<TUserInfoModel> GetUserInfoAsync(TAccessTokenModel accessToken)
        {
            var userInfo = await UserInfoUrl
                    .WithHeaders(new { Accept = "application/json", User_Agent = DEFAULT_USER_AGENT })
                    .SetQueryParams(BuildUserInfoParams(accessToken))
                    .GetAsync()
                    .ReceiveJson<TUserInfoModel>();
            return userInfo;
        }

        /// <summary>
        /// 构造一个用于跳转授权的 URL
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public virtual Task<string> GetAuthorizeUrl(string state = "")
        {
            var param = BuildAuthorizeParams(state);
            param.RemoveEmptyValueItems();
            return Task.FromResult($"{AuthorizeUrl}{(AuthorizeUrl.Contains("?") ? "&" : "?")}{param.ToQueryString()}");
        }

        /// <summary>
        /// TODO：扩展支持外部定义POST GET方法
        /// </summary>
        /// <param name="authorizeCallbackParams"></param>
        /// <returns></returns>
        public async Task<OAuthResult<TAccessTokenModel, TUserInfoModel>> GetOAuth(Dictionary<string, string> authorizeCallbackParams)
        {
            var accessToken = await GetAccessTokenAsync(BuildAccessTokenParams(authorizeCallbackParams));
            var useInfo = await GetUserInfoAsync(accessToken);
            OAuthResult<TAccessTokenModel, TUserInfoModel> result = new OAuthResult<TAccessTokenModel, TUserInfoModel>(accessToken, useInfo);
            return result;
        }

    }
}