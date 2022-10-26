using AutoMapper;
using FreeSql;
using Microsoft.AspNetCore.Mvc;
using NetX.Common.Attributes;
using NetX.Common.Models;
using NetX.RBAC.Data.Repositories;
using NetX.RBAC.Models;

namespace NetX.RBAC.Core;

/// <summary>
/// 部门管理服务
/// </summary>
[Scoped]
public class DeptService : RBACBaseService, IDeptService
{
    private readonly IBaseRepository<sys_dept> _deptRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// 部门管理服务实例对象
    /// </summary>
    /// <param name="deptRepository">部门仓储实例</param>
    /// <param name="mapper">对象映射实例</param>
    public DeptService(
        IBaseRepository<sys_dept> deptRepository,
        IMapper mapper)
    {
        this._deptRepository = deptRepository;
        this._mapper = mapper;
    }

    /// <summary>
    /// 新增部门
    /// </summary>
    /// <param name="model">部门实体对象</param>
    /// <returns></returns>
    public async Task<ResultModel<bool>> AddDept(DeptRequestModel model)
    {
        var deptEntity = new sys_dept()
        {
            id = base.CreateId(),
            createtime = base.CreateInsertTime(),
            deptname = model.DeptName,
            parentid = string.IsNullOrWhiteSpace(model.ParentId) ? RBACConst.C_ROOT_ID : model.ParentId,
            orderno = model.OrderNo,
            status = int.Parse(model.Status),
            remark = model.Remark
        };
        await this._deptRepository.InsertAsync(deptEntity);
        return base.Success<bool>(true);
    }

    /// <summary>
    /// update the department info
    /// </summary>
    /// <param name="model">部门实体对象</param>
    /// <returns></returns>
    public async Task<ResultModel<bool>> UpdateDept(DeptRequestModel model)
    {
        var deptEntity = await _deptRepository.Select.Where(p => p.id.Equals(model.Id)).FirstAsync();
        deptEntity.deptname = model.DeptName;
        deptEntity.parentid = string.IsNullOrWhiteSpace(model.ParentId) ? RBACConst.C_ROOT_ID : model.ParentId;
        deptEntity.orderno = model.OrderNo;
        deptEntity.status = int.Parse(model.Status);
        deptEntity.remark = model.Remark;
        var result = await this._deptRepository.UpdateAsync(deptEntity) > 0;
        return base.Success<bool>(result);
    }

    /// <summary>
    /// hard remove a department
    /// </summary>
    /// <param name="deptId">部门唯一标识</param>
    /// <returns></returns>
    public async Task<ResultModel<bool>> RemoveDept(string deptId)
    {
        var result = await ((SysDeptRepository)this._deptRepository).RemoveDeptAsync(deptId);
        return base.Success<bool>(result);
    }

    /// <summary>
    /// get the deptment list
    /// </summary>
    /// <param name="queryParam">部门查询条件实体</param>
    /// <returns></returns>
    public async Task<ResultModel<List<DeptModel>>> GetDeptList([FromQuery] DeptListParam queryParam)
    {
        var depts = await _deptRepository.Select.WhereIf(!queryParam.ContainDisabled, p => p.status == (int)Status.Enable).ToListAsync();
        var result = ToTree(this._mapper.Map<List<DeptModel>>(depts), RBACConst.C_ROOT_ID);
        return base.Success<List<DeptModel>>(result);
    }

    /// <summary>
    /// 构建部门树
    /// </summary>
    /// <param name="depts">部门集合</param>
    /// <param name="parentId">父部门编号</param>
    /// <returns></returns>
    private List<DeptModel> ToTree(List<DeptModel> depts, string parentId)
    {
        var currentDepts = depts.Where(p => p.ParentId == parentId);
        foreach (var dept in currentDepts)
        {
            var children = ToTree(depts, dept.Id);
            if (children?.Count > 0)
            {
                dept.Children = new List<DeptModel>();
                dept.Children.AddRange(children);
            }
        }
        return currentDepts.ToList();
    }
}
