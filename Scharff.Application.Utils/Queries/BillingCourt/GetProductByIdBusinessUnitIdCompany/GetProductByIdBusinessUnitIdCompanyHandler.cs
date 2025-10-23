using MediatR;
using Scharff.Domain.Response.BillingCourt;
using Scharff.Domain.Utils.Exceptions;
using Scharff.Infrastructure.PostgreSQL.Queries.BillingCourt.GetProductByIdBusinessUnitIdCompany;

namespace Scharff.Application.Queries.BillingCourt.GetProductByIdBusinessUnitIdCompany
{
    public class GetProductByIdBusinessUnitIdCompanyHandler : IRequestHandler<GetProductByIdBusinessUnitIdCompanyQuery, List<ResponseGetProductByIdBusinessUnitIdCompany>>
    {
        private readonly IGetProductByIdBusinessUnitIdCompanyQuery  _Repository;

        public GetProductByIdBusinessUnitIdCompanyHandler(IGetProductByIdBusinessUnitIdCompanyQuery GetAllCountryRegionQuery)
        {
            _Repository = GetAllCountryRegionQuery;
        }

        public async Task<List<ResponseGetProductByIdBusinessUnitIdCompany>> Handle(GetProductByIdBusinessUnitIdCompanyQuery request, CancellationToken cancellationToken)
        {
            var result = await _Repository.GetProductByIdBusinessUnitIdCompany(request.Id_Company, request.Id_Business_Unit);
            if (!result.Any())
            {
                throw new NotFoundException("No se encontro el producto");
            }
            return result;
        }
    }
}
