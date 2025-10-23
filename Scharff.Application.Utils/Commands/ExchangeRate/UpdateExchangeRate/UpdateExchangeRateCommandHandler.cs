using MediatR;
using Scharff.Domain.Entities;
using Scharff.Domain.Utils.Exceptions;
using Scharff.Infrastructure.Http.Queries.PaymentVoucher.GetPaymentVoucherByExchangeRate;
using Scharff.Infrastructure.PostgreSQL.Commands.ExchangeRate.UpdateExchangeRate;

namespace Scharff.Application.Commands.ExchangeRate.UpdateExchangeRate
{
    public class UpdateExchangeRateCommandHandler : IRequestHandler<UpdateExchangeRateCommand, int>
    {
        private readonly IUpdateExchangeRateCommand _updateExchangeRateCommand;
        private readonly IGetPaymentVoucherByExchangeRateQuery _getPaymentVoucherByExchangeRateQuery;

        public UpdateExchangeRateCommandHandler(IUpdateExchangeRateCommand updateExchangeRateCommand, IGetPaymentVoucherByExchangeRateQuery getPaymentVoucherByExchangeRateQuery)
        {
            _updateExchangeRateCommand = updateExchangeRateCommand;
            _getPaymentVoucherByExchangeRateQuery = getPaymentVoucherByExchangeRateQuery;
        }
        public async Task<int> Handle(UpdateExchangeRateCommand request, CancellationToken cancellationToken)
        {

            ExchangeRateModel model = new()
            {
                id = request.id,
                bank_purchase = request.bank_purchase,
                bank_sale = request.bank_sale,
                modification_author = request.user
            };

            var result = await _getPaymentVoucherByExchangeRateQuery.GetPaymentVoucherByExchangeRate(request.change_date.ToString("MM-dd-yyyy"));

            if (result > 0)
            {
                throw new BadRequestException("Tipo de cambio usado en comprobante de pago.");
            }
            int serviceOrderId = await _updateExchangeRateCommand.UpdateExchangeRate(model);

            return serviceOrderId;

        }
    }
}
