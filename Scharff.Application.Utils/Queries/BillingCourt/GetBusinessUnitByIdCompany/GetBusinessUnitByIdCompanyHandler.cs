using MediatR;
using Scharff.Domain.Response.BillingCourt;
using Scharff.Domain.Utils.Exceptions;
using Scharff.Infrastructure.PostgreSQL.Queries.BillingCourt.GetBusinessUnitByIdCompany;

namespace Scharff.Application.Queries.BillingCourt.GetBusinessUnitByIdCompany
{
    public class GetBusinessUnitByIdCompanyHandler : IRequestHandler<GetBusinessUnitByIdCompanyQuery, List<ResponseGetBusinessUnitByIdCompany>>
    {
        private readonly IGetBusinessUnitByIdCompanyQuery  _Repository;

        public GetBusinessUnitByIdCompanyHandler(IGetBusinessUnitByIdCompanyQuery GetAllCountryRegionQuery)
        {
            _Repository = GetAllCountryRegionQuery;
        }

        public async Task<List<ResponseGetBusinessUnitByIdCompany>> Handle(GetBusinessUnitByIdCompanyQuery request, CancellationToken cancellationToken)
        {
            var result = await _Repository.GetBusinessUnitByIdCompany(request.Id_Company);
            if (!result.Any())
            {
                throw new NotFoundException("No se encontro la unidad de negocio");
            }
            return result;
        }
    }
}
