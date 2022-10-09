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
public class DeptService : BaseService, IDeptService
{
    private readonly IBaseRepository<sys_dept> _deptRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// 部门管理服务实例对象
    /// </summary>
    /// <param name="deptRepository"></param>
    /// <param name="mapper"></param>
    public DeptService(
        IBaseRepository<sys_dept> deptRepository,
        IMapper mapper)
    {
        this._deptRepository = deptRepository;
        this._mapper = mapper;
    }

    /// <summary>
    /// 新增部門
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ResultModel<bool>> AddDept(DeptRequestModel model)
    {
        var deptEntity = new sys_dept()
        {
            id = base.CreateId(),
            createtime = base.CreateInsertTime(),
            deptname = model.DeptName,
            parentid = string.IsNullOrWhiteSpace(model.ParentId) ? SystemManagerConst.C_ROOT_ID : model.ParentId,
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
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ResultModel<bool>> UpdateDept(DeptRequestModel model)
    {
        var deptEntity = await _deptRepository.Select.Where(p => p.id.Equals(model.Id)).FirstAsync();
        deptEntity.deptname = model.DeptName;
        deptEntity.parentid = string.IsNullOrWhiteSpace(model.ParentId) ? SystemManagerConst.C_ROOT_ID : model.ParentId;
        deptEntity.orderno = model.OrderNo;
        deptEntity.status = int.Parse(model.Status);
        deptEntity.remark = model.Remark;
        var result = await this._deptRepository.UpdateAsync(deptEntity) > 0;
        return base.Success<bool>(result);
    }

    /// <summary>
    /// hard remove a department
    /// </summary>
    /// <param name="deptId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ResultModel<bool>> RemoveDept(string deptId)
    {
        var result = await ((SysDeptRepository)this._deptRepository).RemoveDeptAsync(deptId);
        return base.Success<bool>(result);
    }

    /// <summary>
    /// get the deptment list
    /// </summary>
    /// <param name="queryParam"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ResultModel<List<DeptModel>>> GetDeptList([FromQuery] DeptListParam queryParam)
    {
        var depts = await _deptRepository.Select.ToListAsync();
        var result = ToTree(this._mapper.Map<List<DeptModel>>(depts), SystemManagerConst.C_ROOT_ID);
        return base.Success<List<DeptModel>>(result);
    }

    /// <summary>
    /// 构建部门树
    /// </summary>
    /// <param name="depts"></param>
    /// <param name="parentId"></param>
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
