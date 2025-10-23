using MediatR;
using Scharff.Domain.Response.Company.GetSunatRegimenById;
using Scharff.Infrastructure.PostgreSQL.Queries.Company.GetSunatRegimenById;

namespace Scharff.Application.Queries.Company.GetSunatRegimenById
{
    public class GetSunatRegimenByIdHandler : IRequestHandler<GetSunatRegimenByIdQuery, ResponseGetSunatRegimenById>
    {
        private readonly IGetSunatRegimenByIdQuery _getSunatRegimenById;

        public GetSunatRegimenByIdHandler(IGetSunatRegimenByIdQuery getSunatRegimenById)
        {
            _getSunatRegimenById = getSunatRegimenById;
        }
        public async Task<ResponseGetSunatRegimenById> Handle(GetSunatRegimenByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _getSunatRegimenById.GetSunatRegimenById(request.Id_Company);

            return result;
        }
    }
}
