using Dapper;
using Npgsql;
using Scharff.Domain.Response.LedgerAccount.GetAllLedgerAccount;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.LedgerAccount.GetAllLedgerAccount
{
    public class GetAllLedgerAccountQuery : IGetAllLedgerAccountQuery
    {
        private readonly IDbConnection _connection;

        public GetAllLedgerAccountQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<ResponseGetAllLedgerAccount>> GetAllLedgerAccount()
        {
            using (IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                try
                {
                    string sql = @" select id, numero description from nsf.cuenta_contable order by 2 asc";

                    IEnumerable<ResponseGetAllLedgerAccount> parameters = await connection.QueryAsync<ResponseGetAllLedgerAccount>(sql);
                    return parameters.ToList();
                }
                catch (NpgsqlException err)
                {
                    Console.WriteLine(err);
                    throw;
                }
            }
        }
    }
}
