
using MediatR;
using Scharff.Domain.Entities;
using Scharff.Domain.Utils.Exceptions;
using Scharff.Infrastructure.PostgreSQL.Commands.ExchangeRate.RegisterExchangeRate;
using Scharff.Infrastructure.PostgreSQL.Queries.ExchangeRate.GetExchangeRateBroadCast;

namespace Scharff.Application.Commands.ExchangeRate.RegisterExchangeRate
{
    public class RegisterExchangeRateCommandHandler : IRequestHandler<RegisterExchangeRateCommand, int>
    {
        private readonly IRegisterExchangeRateCommand _registerExchangeRateCommand;
        private readonly IGetExchangeRateBroadCast _getExchangeRateBroadCast;

        public RegisterExchangeRateCommandHandler(IRegisterExchangeRateCommand registerExchangeRateCommand, IGetExchangeRateBroadCast getExchangeRateBroadCast)
        {
            _registerExchangeRateCommand = registerExchangeRateCommand;
            _getExchangeRateBroadCast = getExchangeRateBroadCast;

        }
        public async Task<int> Handle(RegisterExchangeRateCommand request, CancellationToken cancellationToken)
        {

            ExchangeRateModel model = new()
            {
                change_date = request.change_date,
                bank_purchase = request.bank_purchase,
                bank_sale = request.bank_sale,
                creation_author = request.user
            };
            
            var previousExchangeRate = await _getExchangeRateBroadCast.GetExchangeRateByBroadCast(request.change_date.Date.AddDays(-1));
          
            if (previousExchangeRate == null)
            {
                throw new BadRequestException("No existe un tipo de cambio para el día anterior.");
            }
            var result = await _getExchangeRateBroadCast.GetExchangeRateByBroadCast(request.change_date.Date);
            if (result?.id > 0)
            {
                throw new BadRequestException("Ya existe un tipo de cambio con la fecha ingresada.");
            }

            int serviceOrderId = await _registerExchangeRateCommand.RegisterExchangeRate(model);

            return serviceOrderId;

        }
    }
}
