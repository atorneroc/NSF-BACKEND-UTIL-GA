using MediatR;
using Scharff.Domain.Response.Parameter.GetAllCompanies;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetAllCompanies;

namespace Scharff.Application.Queries.Parameter.GetAllCompanies
{
    public class GetAllCompaniesHandler : IRequestHandler<GetAllCompaniesQuery, List<ResponseGetAllCompanies>>
    {
        private readonly IGetAllCompaniesQuery _getAllCompaniesQuery;

        public GetAllCompaniesHandler(IGetAllCompaniesQuery getAllCompaniesQuery)
        {
            _getAllCompaniesQuery = getAllCompaniesQuery;
        }

        public async Task<List<ResponseGetAllCompanies>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            var result = await _getAllCompaniesQuery.GetAllCompanies();



            return result;
        }
    }
}
