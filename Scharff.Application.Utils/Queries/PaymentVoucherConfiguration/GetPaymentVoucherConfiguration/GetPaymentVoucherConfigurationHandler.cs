using MediatR;
using Scharff.Domain.Response.PaymentVoucherConfiguration;
using Scharff.Infrastructure.PostgreSQL.Queries.PaymentVoucherConfiguration.GetPaymentVoucherConfiguration;

namespace Scharff.Application.Queries.PaymentVoucherConfiguration.GetPaymentVoucherConfiguration
{
    public class GetPaymentVoucherConfigurationHandler : IRequestHandler<GetPaymentVoucherConfigurationQuery,ResponsePaymentVoucherConfiguration>
    {
        private readonly IGetPaymentVoucherConfigurationQuery _GetPaymentVoucherConfigurationQuery;
        public GetPaymentVoucherConfigurationHandler(IGetPaymentVoucherConfigurationQuery getStoreByBusinessUnitIdQuery)
        {
            _GetPaymentVoucherConfigurationQuery = getStoreByBusinessUnitIdQuery;
        }

        public async Task<ResponsePaymentVoucherConfiguration> Handle(GetPaymentVoucherConfigurationQuery request, CancellationToken cancellationToken)
        {
            var result = await _GetPaymentVoucherConfigurationQuery.GetPaymentVoucherConfiguration(request.Id_Payment_Voucher_Type);
            return result;
        }
    }
}
