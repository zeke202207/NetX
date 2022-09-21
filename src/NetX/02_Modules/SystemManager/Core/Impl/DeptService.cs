using FreeSql;
using Microsoft.AspNetCore.Mvc;
using NetX.Authentication.Core;
using NetX.Common;
using NetX.Common.Attributes;
using NetX.SystemManager.Core.Impl;
using NetX.SystemManager.Data.Repositories;
using NetX.SystemManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.Core;

[Scoped]
public class DeptService : BaseService, IDeptService
{
    private readonly IBaseRepository<sys_dept> _deptRepository;

    public DeptService(
        IBaseRepository<sys_dept> deptRepository)
    {
        this._deptRepository = deptRepository;
    }

    /// <summary>
    /// 新增部門
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<bool> AddDept(DeptRequestModel model)
    {
        var deptEntity = new sys_dept()
        {
            id = base.CreateId(),
            createtime = base.CreateInsertTime(),
            deptname = model.DeptName,
            parentid = string.IsNullOrWhiteSpace(model.ParentId)? SystemManagerConst .C_ROOT_ID: model.ParentId,
            orderno = model.OrderNo,
            status = int.Parse(model.Status),
            remark = model.Remark
        };
        await this._deptRepository.InsertAsync(deptEntity);
        return true;
    }

    /// <summary>
    /// update the department info
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<bool> UpdateDept(DeptRequestModel model)
    {
        var deptEntity = await _deptRepository.Select.Where(p => p.id.Equals(model.Id)).FirstAsync();
        deptEntity.deptname = model.DeptName;
        deptEntity.parentid = string.IsNullOrWhiteSpace(model.ParentId) ? SystemManagerConst.C_ROOT_ID : model.ParentId;
        deptEntity.orderno = model.OrderNo;
        deptEntity.status = int.Parse(model.Status);
        deptEntity.remark = model.Remark;
        return await this._deptRepository.UpdateAsync(deptEntity) > 0;
    }

    /// <summary>
    /// hard remove a department
    /// </summary>
    /// <param name="deptId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<bool> RemoveDept(string deptId)
    {
        //1. find all children department
        var ids = await this._deptRepository.Select
            .WithSql($"select id from sys_dept where find_in_set(id,get_child_dept('{deptId}'))")
            .ToListAsync<string>("id");
        //2. delete all
        await this._deptRepository.DeleteAsync(p => ids.Contains(p.id));
        return true;
    }

    /// <summary>
    /// get the deptment list
    /// </summary>
    /// <param name="queryParam"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<List<DeptModel>> GetDeptList([FromQuery] DeptListParam queryParam)
    {
        var depts = await _deptRepository.Select.ToListAsync();
        return ToTree(depts.Select(p => new DeptModel()
        {
            Id = p.id,
            ParentId = p.parentid,
            CreateTime = p.createtime,
            DeptName = p.deptname,
            OrderNo = p.orderno,
            Remark = p.remark,
            Status = p.status.ToString(),
        }).ToList(), "00000000000000000000000000000000");
    }

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
        return currentDepts?.ToList();
    }
}
