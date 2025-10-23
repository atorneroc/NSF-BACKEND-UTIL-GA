using MediatR;
using Scharff.Domain.Response.BillingCourt;

namespace Scharff.Application.Queries.BillingCourt.GetBusinessUnitByIdCompany
{
    public class GetBusinessUnitByIdCompanyQuery : IRequest<List<ResponseGetBusinessUnitByIdCompany>>{

        public int Id_Company { get; set; }
    }
}
