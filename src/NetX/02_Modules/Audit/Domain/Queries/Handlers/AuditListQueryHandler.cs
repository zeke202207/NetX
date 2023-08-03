using Dapper;
using NetX.Ddd.Domain;
using NetX.Audit.Models.Dtos;
using NetX.Common.Attributes;
using NetX.Common.ModuleInfrastructure;
using NetX.Ddd.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Quartz.Util;

namespace NetX.Audit.Domain
{
    [Scoped]
    public class AuditListQueryHandler : DomainQueryHandler<AuditListQuery, ResultModel>
    {
        public AuditListQueryHandler(IDatabaseContext dbContext) : base(dbContext)
        {

        }

        public override async Task<ResultModel> Handle(AuditListQuery request, CancellationToken cancellationToken)
        {
            var list = await GetList(request);
            var total = await GetCount(request);
            return list.ToSuccessPagerResultModel<IEnumerable<AuditListModel>>(total);
        }


        private async Task<IEnumerable<AuditListModel>> GetList(AuditListQuery request)
        {
            string sql = @"SELECT l.*, u.nickname FROM sys_log_audit l left join sys_user u on u.id = l.userid where 1=1 ";
            var param = new DynamicParameters();
            if (!request.NickName.IsNullOrWhiteSpace())
            {
                sql += @" AND u.nickname LIKE CONCAT('%',@nickname,'%')";
                param.Add("nickname", request.NickName);
            }
            sql += " order by l.executiontime desc";
            sql += sql.AppendMysqlPagerSql(request.CurrentPage, request.PageSize);
            return await _dbContext.QueryListAsync<AuditListModel>(sql, param);
        }

        private async Task<int> GetCount(AuditListQuery request)
        {
            string sql = @"SELECT COUNT(0) FROM sys_log_audit l left join sys_user u on u.id = l.userid  where 1=1 ";
            var param = new DynamicParameters();
            if (!request.NickName.IsNullOrWhiteSpace())
            {
                sql += @" AND u.nickname LIKE CONCAT('%',@nickname,'%')";
                param.Add("nickname", request.NickName);
            }
            return await _dbContext.ExecuteScalarAsync<int>(sql, param);
        }
    }
}
