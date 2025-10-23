using MediatR;
using Scharff.Domain.Response.Parameter.GetBranchOfficeByCompanyId;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetBranchOfficeByCompanyId;
using System.ComponentModel.DataAnnotations;

namespace Scharff.Application.Queries.Parameter.GetBranchOfficeByCompanyId
{
    public class GetBranchOfficeByCompanyIdHandler : IRequestHandler<GetBranchOfficeByCompanyIdQuery, List<ResponseGetBranchOfficeByCompanyId>>
    {
        private readonly IGetBranchOfficeByCompanyIdQuery _getBranchOfficeByCompanyIdQuery;

        public GetBranchOfficeByCompanyIdHandler(IGetBranchOfficeByCompanyIdQuery getBranchOfficeByCompanyIdQuery)
        {
            _getBranchOfficeByCompanyIdQuery = getBranchOfficeByCompanyIdQuery;
        }
        public async Task<List<ResponseGetBranchOfficeByCompanyId>> Handle(GetBranchOfficeByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _getBranchOfficeByCompanyIdQuery.GetBranchOfficeByCompanyId(request.company_id);

            if (result == null || result.Count == 0)
            {
                throw new ValidationException("No se encontraron sucursales por la empresa seleccionada.");
            }

            return result;
        }
    }
}
