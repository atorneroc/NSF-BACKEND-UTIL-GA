using MediatR;
using Scharff.Domain.Response.Ubigeo.CheckUbigeoBySapCodes;
using Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.CheckUbigeoBySapCodes;

namespace Scharff.Application.Queries.Ubigeo.CheckUbigeoBySapCodes
{
    public class CheckUbigeoBySapCodesHandler : IRequestHandler<CheckUbigeoBySapCodesQuery, List<ResponseUbigeoLocationBySapCodes>>
    {
        private readonly ICheckUbigeoBySapCodesQuery _checkUbigeoBySapCodesQuery;

        public CheckUbigeoBySapCodesHandler(ICheckUbigeoBySapCodesQuery checkUbigeoBySapCodesQuery)
        {
            _checkUbigeoBySapCodesQuery = checkUbigeoBySapCodesQuery;
        }

        public async Task<List<ResponseUbigeoLocationBySapCodes>> Handle(CheckUbigeoBySapCodesQuery request, CancellationToken cancellationToken)
        {
            return await _checkUbigeoBySapCodesQuery.CheckUbigeoBySapCodes(request.SapCountryCode, request.SapRegionCode);
        }
    }
}