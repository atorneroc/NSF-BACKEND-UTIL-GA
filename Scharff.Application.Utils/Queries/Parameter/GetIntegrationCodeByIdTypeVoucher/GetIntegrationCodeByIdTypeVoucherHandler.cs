using MediatR;
using Scharff.Domain.Response.Parameter.GetIntegrationCodeByIdTypeVoucher;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetIntegrationCodeByIdTypeVoucher;

namespace Scharff.Application.Queries.Parameter.GetIntegrationCodeByIdTypeVoucher
{
    public class GetIntegrationCodeByIdTypeVoucherHandler : IRequestHandler<GetIntegrationCodeByIdTypeVoucherQuery, List<ResponseGetIntegrationCodeByIdTypeVoucher>>
    {
        private readonly IGetIntegrationCodeByIdTypeVoucherQuery _getIntegrationCodeByIdTypeVoucherQuery;

        public GetIntegrationCodeByIdTypeVoucherHandler(IGetIntegrationCodeByIdTypeVoucherQuery getIntegrationCodeByIdTypeVoucherQuery)
        {
            _getIntegrationCodeByIdTypeVoucherQuery = getIntegrationCodeByIdTypeVoucherQuery;
        }

        public async Task<List<ResponseGetIntegrationCodeByIdTypeVoucher>> Handle(GetIntegrationCodeByIdTypeVoucherQuery request, CancellationToken cancellationToken)
        {
            var result = await _getIntegrationCodeByIdTypeVoucherQuery.GetIntegrationCodeByIdTypeVoucher(request.idTypeVoucher);
            return result;
        }
    }
}