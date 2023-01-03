namespace NetX.RBAC.Models
{
    /// <summary>
    /// api访问控制实体对象
    /// </summary>
    public class ApiPermissionModel
    {
        /// <summary>
        /// 是否进行pai验证
        /// </summary>
        public bool CheckApi { get; set; }

        /// <summary>
        /// api权限列表集合
        /// </summary>
        public List<string> Apis { get; set; }
    }
}
