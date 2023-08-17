using NetX.Common.ModuleInfrastructure;
using NetX.Ddd.Domain;

namespace NetX.Audit.Domain
{
    public class AuditListQuery : DomainQuery<ResultModel>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public string NickName { get; set; }

        public AuditListQuery(string nickName, int currentPage, int pageSize)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            NickName = nickName;
        }
    }
}
