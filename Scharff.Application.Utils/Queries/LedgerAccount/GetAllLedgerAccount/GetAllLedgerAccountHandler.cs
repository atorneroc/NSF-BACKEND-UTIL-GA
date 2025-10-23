using MediatR;
using Scharff.Domain.Response.LedgerAccount.GetAllLedgerAccount;
using Scharff.Infrastructure.PostgreSQL.Queries.LedgerAccount.GetAllLedgerAccount;

namespace Scharff.Application.Queries.Service.GetAllLedgerAccount
{
    public class GetAllLedgerAccountHandler : IRequestHandler<GetAllLedgerAccountQuery, List<ResponseGetAllLedgerAccount>>
    {
        private readonly IGetAllLedgerAccountQuery _GetAllLedgerAccountQuery;

        public GetAllLedgerAccountHandler(IGetAllLedgerAccountQuery GetAllLedgerAccountQuery)
        {
            _GetAllLedgerAccountQuery = GetAllLedgerAccountQuery;
        }

        public async Task<List<ResponseGetAllLedgerAccount>> Handle(GetAllLedgerAccountQuery request, CancellationToken cancellationToken)
        {
            var result = await _GetAllLedgerAccountQuery.GetAllLedgerAccount();           

            return result;
        }
    }
}
