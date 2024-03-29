﻿using Microsoft.Extensions.DependencyInjection;

namespace Authentication.OAuth
{
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// 添加open auth三方登录认证
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config">配置项</param>
        public static IServiceCollection AddOAuth<TAccessTokenModel, TUserInfoModel>(
            this IServiceCollection services,
            OAuthLoginBase<TAccessTokenModel, TUserInfoModel> oAuth)
        where TAccessTokenModel : AccessTokenSuccessModel
        where TUserInfoModel : UserInfoModelBase
        {
            services.AddSingleton(oAuth);
            return services;
        }
    }
}
