using MediatR;
using Scharff.Domain.Response.ExchangeRate.GetExchangeRateBroadCast;
using Scharff.Domain.Utils.Exceptions;
using Scharff.Infrastructure.PostgreSQL.Queries.ExchangeRate.GetExchangeRateBroadCast;

namespace Scharff.Application.Queries.ExchangeRate.GetExchangeRateBroadCast
{
    public class GetExchangeRateBroadCastHandler : IRequestHandler<GetExchangeRateBroadCastQuery, ResponseGetExchangeRateBroadCast>
    {
        private readonly IGetExchangeRateBroadCast _getExchangeRateBroadCast;

        public GetExchangeRateBroadCastHandler(IGetExchangeRateBroadCast getExchangeRateBroadCast)
        {
            _getExchangeRateBroadCast = getExchangeRateBroadCast;
        }
        public async Task<ResponseGetExchangeRateBroadCast> Handle(GetExchangeRateBroadCastQuery request, CancellationToken cancellationToken)
        {
            var result = await _getExchangeRateBroadCast.GetExchangeRateByBroadCast(request.broadCast);
            if (result == null) { throw new BadRequestException("No se encontro el Tipo de Cambio."); }



            return result;
        }
    }
}
