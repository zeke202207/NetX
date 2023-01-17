using AutoMapper;
using Dapper;
using Netx.Ddd.Domain;
using NetX.Common.Attributes;
using NetX.Common.ModuleInfrastructure;
using NetX.RBAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Domain;

[Scoped]
public class DeptPagerListQueryHandler : DomainQueryHandler<DeptPagerListQuery, ResultModel>
{
    private readonly IMapper _mapper;

    public DeptPagerListQueryHandler(
        IDatabaseContext dbContext,
        IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }

    public override async Task<ResultModel> Handle(DeptPagerListQuery request, CancellationToken cancellationToken)
    {
        var result = await GetList(request);
        var count = await GetCount(request);
        return result.ToSuccessPagerResultModel(count);
    }

    private async Task<List<DeptModel>> GetList(DeptPagerListQuery request)
    {
        string sql = "SELECT * FROM sys_dept where 1=1";
        var param = new DynamicParameters();
        if (!request.ContainDisabled)
        {
            sql += " AND status =@status";
            param.Add("status", (int)Status.Enable);
        }
        if (!string.IsNullOrWhiteSpace(request.DeptName))
        {
            sql += " AND deptname LIKE CONCAT('%',@deptname,'%')";
            param.Add("deptname", request.DeptName);
        }
        if (!string.IsNullOrWhiteSpace(request.Status))
        {
            sql += " AND status = @status1";
            param.Add("status1", request.Status);
        }
        var depts = await base._dbContext.QueryListAsync<sys_dept>(sql,param);
        return ToTree(this._mapper.Map<List<DeptModel>>(depts), RBACConst.C_ROOT_ID);
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

    private async Task<int> GetCount(DeptPagerListQuery request)
    {
        string sql = "SELECT COUNT(0) FROM sys_dept where 1=1";
        var param = new DynamicParameters();
        if (!request.ContainDisabled)
        {
            sql += " AND status =@status";
            param.Add("status", (int)Status.Enable);
        }
        if (!string.IsNullOrWhiteSpace(request.DeptName))
        {
            sql += " AND deptname LIKE CONCAT('%',@deptname,'%')";
            param.Add("deptname", request.DeptName);
        }
        if (!string.IsNullOrWhiteSpace(request.Status))
        {
            sql += " AND status = @status1";
            param.Add("status1", request.Status);
        }
        return await base._dbContext.ExecuteScalarAsync<int>(sql, param);
    }
}
