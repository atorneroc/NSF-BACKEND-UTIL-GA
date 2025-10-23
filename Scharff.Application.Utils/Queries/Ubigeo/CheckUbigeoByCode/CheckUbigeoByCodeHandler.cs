using MediatR;
using Scharff.Domain.Response.Ubigeo.CheckUbigeoByCode;
using Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.CheckUbigeoByCode;

namespace Scharff.Application.Queries.Ubigeo.CheckUbigeoByCode
{
    public  class CheckUbigeoByCodeHandler : IRequestHandler<CheckUbigeoByCodeQuery, List<ResponseCheckUbigeoByCode>>
    {
        private readonly ICheckUbigeoByCodeQuery _CheckUbigeoByCodeQuery;
        public CheckUbigeoByCodeHandler(ICheckUbigeoByCodeQuery checkUbigeoByCodeQuery)
        {
            _CheckUbigeoByCodeQuery = checkUbigeoByCodeQuery;
        }

        public async Task<List<ResponseCheckUbigeoByCode>> Handle(CheckUbigeoByCodeQuery request, CancellationToken cancellationToken)
        {
            var result = await _CheckUbigeoByCodeQuery.CheckUbigeoByCode(request.Ubigeo_code);
            return result;
        }
    }
}