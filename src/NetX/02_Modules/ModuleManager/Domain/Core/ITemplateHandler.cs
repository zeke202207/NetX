namespace NetX.ModuleManager.Domain
{
    internal interface ITemplateHandler
    {
        /// <summary>
        /// 文件保存
        /// </summary>
        Task<bool> SaveAsync();
    }
}
