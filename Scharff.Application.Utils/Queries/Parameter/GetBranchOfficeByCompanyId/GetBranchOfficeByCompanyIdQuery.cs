using MediatR;
using Scharff.Domain.Response.Parameter.GetBranchOfficeByCompanyId;

namespace Scharff.Application.Queries.Parameter.GetBranchOfficeByCompanyId
{
    public class GetBranchOfficeByCompanyIdQuery : IRequest<List<ResponseGetBranchOfficeByCompanyId>>
    {
        public int company_id { get; set; }
    }
}
