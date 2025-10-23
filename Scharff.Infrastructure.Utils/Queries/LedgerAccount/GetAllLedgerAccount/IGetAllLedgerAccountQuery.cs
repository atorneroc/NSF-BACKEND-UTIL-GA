using Scharff.Domain.Response.LedgerAccount.GetAllLedgerAccount;

namespace Scharff.Infrastructure.PostgreSQL.Queries.LedgerAccount.GetAllLedgerAccount
{
    public interface IGetAllLedgerAccountQuery
    {
        Task<List<ResponseGetAllLedgerAccount>> GetAllLedgerAccount();
    }
}
