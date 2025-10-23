using MediatR;
using Scharff.Domain.Response.LedgerAccount.GetAllLedgerAccount;

namespace Scharff.Application.Queries.Service.GetAllLedgerAccount
{
    public class GetAllLedgerAccountQuery : IRequest<List<ResponseGetAllLedgerAccount>>{}
}
