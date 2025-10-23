using MediatR;
using Scharff.Domain.Response.ExchangeRate.GetExchangeRateById;
using Scharff.Domain.Utils.Exceptions;
using Scharff.Infrastructure.PostgreSQL.Queries.ExchangeRate.GetExchangeRateById;

namespace Scharff.Application.Queries.ExchangeRate.GetExchangeRateById
{
    public class GetExchangeRateByIdHandler : IRequestHandler<GetExchangeRateByIdQuery, ResponseGetExchangeRateById>
    {
        private readonly IGetExchangeRateByIdQuery _getExchangeRateByIdQuery;

        public GetExchangeRateByIdHandler(IGetExchangeRateByIdQuery getExchangeRateByIdQuery)
        {
            _getExchangeRateByIdQuery = getExchangeRateByIdQuery;
        }
        public async Task<ResponseGetExchangeRateById> Handle(GetExchangeRateByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _getExchangeRateByIdQuery.GetExchangeRateById(request.id);
            if (result==null)
            {
                throw new NotFoundException("No se encontro tipo de cambio");
            }
            return result;
        }
    }
}
