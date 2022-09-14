namespace NetX.Tenants
{
    /// <summary>
    /// 租户存储接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITenantStore<T>
        where T : Tenant
    {
        /// <summary>
        /// 根据租户身份，获取租户信息
        /// </summary>
        /// <param name="Identifier">租户身份</param>
        /// <returns></returns>
        Task<T> GetTenantAsync(string Identifier);
    }
}
