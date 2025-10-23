using MediatR;
using Scharff.Domain.Response.BillingCourt;

namespace Scharff.Application.Queries.BillingCourt.GetProductByIdBusinessUnitIdCompany
{
    public class GetProductByIdBusinessUnitIdCompanyQuery : IRequest<List<ResponseGetProductByIdBusinessUnitIdCompany>>{

        public int Id_Company { get; set; }
        public int Id_Business_Unit { get; set; }
    }
}
