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
        /// <returns></returns>
        Task<T?> GetTenantAsync();

        /// <summary>
        /// 获取全部组合信息
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllTenantAsync();
    }
}
