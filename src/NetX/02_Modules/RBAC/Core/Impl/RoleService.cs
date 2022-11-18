using AutoMapper;
using FreeSql;
using NetX.Common.Attributes;
using NetX.Common.ModuleInfrastructure;
using NetX.EventBus;
using NetX.RBAC.Data.Repositories;
using NetX.RBAC.Models;

namespace NetX.RBAC.Core;

/// <summary>
/// 角色管理服务
/// </summary>
[Scoped]
public class RoleService : RBACBaseService, IRoleService
{
    private readonly IBaseRepository<sys_role> _roleRepository;
    private readonly IEventPublisher _publisher;
    private readonly IMapper _mapper;

    /// <summary>
    /// 角色管理服务实例对象
    /// </summary>
    /// <param name="roleRepository">角色仓储实例</param>
    /// <param name="publisher">事件发布者实例</param>
    /// <param name="mapper">实体对象映射实例</param>
    public RoleService(
        IBaseRepository<sys_role> roleRepository,
        IEventPublisher publisher,
        IMapper mapper)
    {
        this._roleRepository = roleRepository;
        this._mapper = mapper;
        this._publisher = publisher;
    }

    /// <summary>
    /// 获取角色列表
    /// </summary>
    /// <returns></returns>
    public async Task<ResultModel<List<RoleModel>>> GetRoleList()
    {
        return await GetRoleList(new RoleListParam());
    }

    /// <summary>
    /// 获取角色列表
    /// </summary>
    /// <param name="queryParam">查询参数实体</param>
    /// <returns></returns>
    public async Task<ResultModel<List<RoleModel>>> GetRoleList(RoleListParam queryParam)
    {
        int status;
        int.TryParse(queryParam.Status, out status);
        var roles = await ((SysRoleRepository)this._roleRepository)
            .GetRoleListAsync(queryParam.RoleName ?? string.Empty, string.IsNullOrWhiteSpace(queryParam.Status) ? null : int.Parse(queryParam.Status), queryParam.Page, queryParam.PageSize);
        var result = this._mapper.Map<List<RoleModel>>(roles);
        return base.Success<List<RoleModel>>(result);
    }

    /// <summary>
    /// 添加角色
    /// </summary>
    /// <param name="model">角色实体对象</param>
    /// <returns></returns>
    public async Task<ResultModel<bool>> AddRole(RoleRequestModel model)
    {
        var roleEntity = new sys_role()
        {
            id = base.CreateId(),
            createtime = base.CreateInsertTime(),
            rolename = model.RoleName,
            status = int.Parse(model.Status),
            remark = model.Remark ?? "",
            apicheck = int.Parse(model.ApiCheck)
        };
        var result = await ((SysRoleRepository)_roleRepository).AddRoleAsync(roleEntity, model.ToMenuList());
        return base.Success<bool>(result);
    }

    /// <summary>
    /// 更新角色
    /// </summary>
    /// <param name="model">角色实体对象</param>
    /// <returns></returns>
    public async Task<ResultModel<bool>> UpdateRole(RoleRequestModel model)
    {
        var roleEntity = await _roleRepository.Select.Where(p => p.id.Equals(model.Id)).FirstAsync();
        roleEntity.createtime = base.CreateInsertTime();
        roleEntity.rolename = model.RoleName;
        roleEntity.status = int.Parse(model.Status);
        roleEntity.remark = model.Remark ?? string.Empty;
        roleEntity.apicheck = int.Parse(model.ApiCheck);
        var result = await ((SysRoleRepository)_roleRepository).UpdateRoleAsync(roleEntity, model.ToMenuList());
        await RefreshPermissionApiCache(model.Id ?? string.Empty);
        return base.Success<bool>(result);
    }

    /// <summary>
    /// 删除角色
    /// </summary>
    /// <param name="roleId">角色唯一标识</param>
    /// <returns></returns>
    public async Task<ResultModel<bool>> RemoveRole(string roleId)
    {
        var result = await ((SysRoleRepository)_roleRepository).RemoveRoleAsync(roleId);
        await RefreshPermissionApiCache(roleId);
        return base.Success<bool>(result);
    }

    /// <summary>
    /// 更新角色状态
    /// </summary>
    /// <param name="roleId">角色唯一标识</param>
    /// <param name="status">角色启用状态</param>
    /// <returns></returns>
    public async Task<ResultModel<bool>> UpdateRoleStatus(string roleId, string status)
    {
        int intStatus = 0;
        if (!int.TryParse(status, out intStatus))
            return base.Error<bool>("输入状态值无效");
        var roleEntity = await _roleRepository.Select.Where(p => p.id.Equals(roleId)).FirstAsync();
        if (null == roleEntity)
            return base.Error<bool>("角色不存在");
        roleEntity.status = intStatus;
        await _roleRepository.UpdateAsync(roleEntity);
        await RefreshPermissionApiCache(roleId);
        return base.Success<bool>(true);
    }

    /// <summary>
    /// 更新角色后台鉴权状态
    /// </summary>
    /// <param name="roleId">角色唯一标识</param>
    /// <param name="status">角色启动后台api校验状态标识</param>
    /// <returns></returns>
    public async Task<ResultModel<bool>> UpdateRoleApiAuthStatus(string roleId, string status)
    {
        int intStatus = 0;
        if (!int.TryParse(status, out intStatus))
            return base.Error<bool>("输入状态值无效");
        var roleEntity = await _roleRepository.Select.Where(p => p.id.Equals(roleId)).FirstAsync();
        if (null == roleEntity)
            return base.Error<bool>("角色不存在");
        roleEntity.apicheck = intStatus;
        await _roleRepository.UpdateAsync(roleEntity);
        await RefreshPermissionApiCache(roleId);
        return base.Success<bool>(true);
    }

    /// <summary>
    /// 获取角色后台api访问权限集合
    /// </summary>
    /// <param name="roleId">角色唯一标识</param>
    /// <returns></returns>
    public async Task<ResultModel<IEnumerable<string>>> GetApiAuth(string roleId)
    {
        var result = await ((SysRoleRepository)this._roleRepository).GetApiAuth(roleId);
        return base.Success<IEnumerable<string>>(result);
    }

    /// <summary>
    /// 更新后台鉴权api集合列表
    /// </summary>
    /// <param name="roleId">角色唯一标识</param>
    /// <param name="apiIds">访问api集合列表</param>
    /// <returns></returns>
    public async Task<ResultModel<bool>> UpdateRoleApiAuth(string roleId, IEnumerable<string> apiIds)
    {
        var result = await ((SysRoleRepository)this._roleRepository).UpdateRoleApiAuth(roleId, apiIds);
        await RefreshPermissionApiCache(roleId);
        return base.Success<bool>(result);
    }

    /// <summary>
    /// 刷新后台api访问权限集合
    /// </summary>
    /// <param name="roleId">角色唯一标识</param>
    /// <remarks>
    /// 只要有修改，就将缓存清空
    /// 暂时没有区分更新情况
    /// 一次数据库获取，对性能影响不大
    /// </remarks>
    /// <returns></returns>
    private async Task RefreshPermissionApiCache(string roleId)
    {
        if (string.IsNullOrWhiteSpace(roleId))
            return;
        var playload = new PermissionPayload()
        {
            CacheKey = $"{CacheKeys.ACCOUNT_PERMISSIONS}{roleId}",
            OperationType = CacheOperationType.Remove
        };
        await _publisher.PublishAsync(new EventSource(RBACConst.C_RBAC_EVENT_KEY, playload), CancellationToken.None);
    }
}
